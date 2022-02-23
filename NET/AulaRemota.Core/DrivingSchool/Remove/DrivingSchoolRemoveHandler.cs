using AulaRemota.Core.File.RemoveFromAzure;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AulaRemota.Shared.Helpers.Constants;
using System.Net;
using AulaRemota.Infra.Repository.UnitOfWorkConfig;
using System.Collections.Generic;

namespace AulaRemota.Core.DrivingSchool.Remove
{
    public class DrivingSchoolRemoveHandler : IRequestHandler<DrivingSchoolRemoveInput, bool>
    {
        private readonly IRepository<DrivingSchoolModel, int> _autoEscolaRepository;
        private readonly IRepository<UserModel, int> _usuarioRepository;
        private readonly IRepository<PhoneModel, int> _telefoneRepository;
        private readonly IRepository<AddressModel, int> _enderecoRepository;
        private readonly IRepository<FileModel, int> _arquivoRepository;
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMediator _mediator;

        public DrivingSchoolRemoveHandler(
            IRepository<DrivingSchoolModel, int> autoEscolaRepository,
            IRepository<UserModel, int> usuarioRepository,
            IRepository<PhoneModel, int> telefoneRepository,
            IRepository<AddressModel, int> enderecoRepository,
            IRepository<FileModel, int> arquivoRepository,
            IUnitOfWork _unitOfWork,
            IMediator mediator
            )
        {
            _autoEscolaRepository = autoEscolaRepository;
            _usuarioRepository = usuarioRepository;
            _telefoneRepository = telefoneRepository;
            _enderecoRepository = enderecoRepository;
            _arquivoRepository = arquivoRepository;
            UnitOfWork = _unitOfWork;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DrivingSchoolRemoveInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            using (var transaction = UnitOfWork.BeginTransaction())
            {
                var fileList = new List<FileModel>();
                try
                {
                    var autoEscola = await UnitOfWork.DrivingSchool.Context
                        .Set<DrivingSchoolModel>()
                        .Include(e => e.User)
                        .Include(e => e.PhonesNumbers)
                        .Include(e => e.Address)
                        .Include(e => e.Files)
                        .Where(e => e.Id == request.Id)
                        .FirstOrDefaultAsync();

                    if (autoEscola == null) throw new CustomException("Não encontrado", HttpStatusCode.NotFound);

                    UnitOfWork.DrivingSchool.Delete(autoEscola);
                    //UnitOfWork.User.Delete(autoEscola.User);
                    //UnitOfWork.Address.Delete(autoEscola.Address);

                    if (autoEscola.Files.Count > 0)
                    {
                        var result = await _mediator.Send(new RemoveFromAzureInput
                        {
                            Files = autoEscola.Files,
                            TypeUser = Constants.Roles.AUTOESCOLA
                        });
                        if (!result) throw new CustomException("Problema ao remover arquivos de contrato.", HttpStatusCode.InternalServerError);
                    }

                    //foreach (var item in autoEscola.Files)
                    //{
                    //    item.DrivingSchool = null;
                    //    UnitOfWork.File.Delete(item);
                    //}

                    //foreach (var item in autoEscola.PhonesNumbers)
                    //{
                    //    item.Edriving = null;
                    //    UnitOfWork.Phone.Delete(item);
                    //}

                    UnitOfWork.SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch (CustomException e)
                {
                    transaction.Rollback();

                    throw new CustomException(new ResponseModel
                    {
                        UserMessage = e.Message,
                        ModelName = nameof(DrivingSchoolRemoveHandler),
                        Exception = e,
                        InnerException = e.InnerException,
                        StatusCode = e.ResponseModel.StatusCode
                    });
                }
            }
        }
    }
}
