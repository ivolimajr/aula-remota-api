﻿using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Entity.DrivingSchool;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AulaRemota.Core.Partnner.Remove
{
    public class RemovePartnnerHandler : IRequestHandler<RemovePartnnerInput, bool>
    {
        private readonly IRepository<PartnnerModel, int> _parceiroRepository;
        private readonly IRepository<UserModel, int> _usuarioRepository;
        private readonly IRepository<AddressModel, int> _enderecoRepository;
        private readonly IRepository<PhoneModel, int> _telefoneRepository;

        public RemovePartnnerHandler(
            IRepository<PartnnerModel, int> parceiroRepository, 
            IRepository<UserModel, int> usuarioRepository, 
            IRepository<AddressModel, int> enderecoRepository,
            IRepository<PhoneModel, int> telefoneRepository
            )
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _enderecoRepository = enderecoRepository;
            _telefoneRepository = telefoneRepository;
        } 

        public async Task<bool> Handle(RemovePartnnerInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            try
            {
                _parceiroRepository.CreateTransaction();
                var parceiro = await _parceiroRepository.Context
                                        .Set<PartnnerModel>()
                                        .Include(e => e.User)
                                        .Include(e => e.Address)
                                        .Include(e => e.PhonesNumbers)
                                        .Where(e => e.Id == request.Id)
                                        .FirstOrDefaultAsync();

                if (parceiro == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                _parceiroRepository.Delete(parceiro);
                _usuarioRepository.Delete(parceiro.User);
                _enderecoRepository.Delete(parceiro.Address);
                foreach (var item in parceiro.PhonesNumbers)
                {
                    item.Edriving = null;
                    _telefoneRepository.Delete(item);
                }

                _parceiroRepository.Save();
                _parceiroRepository.Commit();
                return true;
            }
            catch (CustomException e)
            {
                _parceiroRepository.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(RemovePartnnerHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
            finally
            {
                _parceiroRepository.Context.Dispose();
            }
        }
    }
}
