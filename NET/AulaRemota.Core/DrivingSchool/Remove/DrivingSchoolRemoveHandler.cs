using AulaRemota.Core.File.RemoveFromAzure;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Models;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Shared.Helpers.Constants;

namespace AulaRemota.Core.DrivingSchool.Remove
{
    public class DrivingSchoolRemoveHandler : IRequestHandler<DrivingSchoolRemoveInput, bool>
    {
        private readonly IRepository<DrivingSchoolModel> _autoEscolaRepository;
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;
        private readonly IRepository<AddressModel> _enderecoRepository;
        private readonly IRepository<FileModel> _arquivoRepository;
        private readonly IMediator _mediator;

        public DrivingSchoolRemoveHandler(
            IRepository<DrivingSchoolModel> autoEscolaRepository,
            IRepository<UserModel> usuarioRepository,
            IRepository<PhoneModel> telefoneRepository,
            IRepository<AddressModel> enderecoRepository,
            IRepository<FileModel> arquivoRepository,
            IMediator mediator
            )
        {
            _autoEscolaRepository = autoEscolaRepository;
            _usuarioRepository = usuarioRepository;
            _telefoneRepository = telefoneRepository;
            _enderecoRepository = enderecoRepository;
            _arquivoRepository = arquivoRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DrivingSchoolRemoveInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            try
            {
                _autoEscolaRepository.CreateTransaction();
                var autoEscola = await _autoEscolaRepository.Context
                    .Set<DrivingSchoolModel>()
                    .Include(e => e.Usuario)
                    .Include(e => e.Telefones)
                    .Include(e => e.Endereco)
                    .Include(e => e.Arquivos)
                    .Where(e => e.Id == request.Id)
                    .FirstOrDefaultAsync();

                if (autoEscola == null) throw new CustomException("Não encontrado");

                _autoEscolaRepository.Delete(autoEscola);
                _usuarioRepository.Delete(autoEscola.Usuario);
                _enderecoRepository.Delete(autoEscola.Endereco);

                if (autoEscola.Arquivos.Count > 0)
                {
                    var result = await _mediator.Send(new RemoveFromAzureInput
                    {
                        Arquivos = autoEscola.Arquivos,
                        TipoUsuario = Constants.Roles.AUTOESCOLA
                    });
                    if (!result) throw new CustomException("Problema ao remover arquivos de contrato.");
                }

                foreach (var item in autoEscola.Arquivos)
                {
                    item.AutoEscola = null;
                    _arquivoRepository.Delete(item);
                }

                foreach (var item in autoEscola.Telefones)
                {
                    item.Edriving = null;
                    _telefoneRepository.Delete(item);
                }
                foreach (var item in autoEscola.Arquivos)
                {
                    item.AutoEscola = null;
                    _arquivoRepository.Delete(item);
                }

                _autoEscolaRepository.Save();
                _autoEscolaRepository.Commit();
                return true;
            }
            catch (CustomException e)
            {
                _autoEscolaRepository.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(DrivingSchoolRemoveHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
            finally
            {
                _autoEscolaRepository.Context.Dispose();
            }
        }
    }
}
