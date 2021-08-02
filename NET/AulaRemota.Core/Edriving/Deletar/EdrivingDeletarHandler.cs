using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using System;
using AulaRemota.Infra.Entity.Auto_Escola;
using System.Collections.Generic;
using System.Linq;

namespace AulaRemota.Core.Edriving.Deletar
{
    public class EdrivingDeletarHandler : IRequestHandler<EdrivingDeletarInput, bool>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<TelefoneModel> _telefoneRepository;

        public EdrivingDeletarHandler(IRepository<EdrivingModel> edrivingRepository, IRepository<UsuarioModel> usuarioRepository, IRepository<TelefoneModel> telefoneRepository)
        {
            _edrivingRepository = edrivingRepository;
            _telefoneRepository = telefoneRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(EdrivingDeletarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");
            try
            {
                UnitOfWork.Current.CreateTransaction();
                var edriving = await _edrivingRepository.GetByIdAsync(request.Id);
                if (edriving == null) throw new HttpClientCustomException("Não encontrado");

                var usuario = await _usuarioRepository.GetByIdAsync(edriving.UsuarioId);
                var telefones = _telefoneRepository.GetWhere(e => e.Edriving.Id == request.Id).ToList();

                edriving.Telefones = telefones;
                edriving.Usuario = usuario;
                //REMOVE O OBJETO
                _edrivingRepository.Delete(edriving);
                _edrivingRepository.Context.SaveChanges();

                _usuarioRepository.Delete(usuario);
                _usuarioRepository.Context.SaveChanges();

                foreach (var item in telefones)
                {
                    item.Edriving = null;
                    _telefoneRepository.Delete(item);
                }
                _telefoneRepository.Context.SaveChanges();
                               
                UnitOfWork.Current.Save();
                UnitOfWork.Current.Commit();
                return true;
            }
            catch (Exception e)
            {
                UnitOfWork.Current.Rollback();
                throw new Exception(e.Message);
            }
            finally
            {
                UnitOfWork.Current.Dispose();
            }
        }
    }
}
