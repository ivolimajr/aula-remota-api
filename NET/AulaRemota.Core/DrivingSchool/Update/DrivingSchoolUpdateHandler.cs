using AulaRemota.Core.File.UploadToAzure;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Infra.Models;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using AulaRemota.Shared.Helpers.Constants;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AulaRemota.Core.File.RemoveFromAzure;

namespace AulaRemota.Core.DrivingSchool.Update
{
    public class DrivingSchoolUpdateHandler : IRequestHandler<DrivingSchoolUpdateInput, DrivingSchoolModel>
    {
        private readonly IRepository<DrivingSchoolModel> _autoEscolaRepository;
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;
        private readonly IRepository<FileModel> _arquivoRepository;
        private readonly IMediator _mediator;

        public DrivingSchoolUpdateHandler(
            IRepository<DrivingSchoolModel> autoEscolaRepository,
            IRepository<UserModel> usuarioRepository,
            IRepository<PhoneModel> telefoneRepository,
            IRepository<FileModel> arquivoRepository,
            IMediator mediator
            )
        {
            _autoEscolaRepository = autoEscolaRepository;
            _usuarioRepository = usuarioRepository;
            _telefoneRepository = telefoneRepository;
            _arquivoRepository = arquivoRepository;
            _mediator = mediator;
        }
        public async Task<DrivingSchoolModel> Handle(DrivingSchoolUpdateInput request, CancellationToken cancellationToken)
        {
            //Cria uma lista para receber os Files
            var fileList = new List<FileModel>();
            try
            {
                _autoEscolaRepository.CreateTransaction();

                var autoEscolaDb = _autoEscolaRepository.Context.Set<DrivingSchoolModel>().Where(e => e.Id.Equals(request.Id))
                                                .Include(e => e.PhonesNumbers).Include(e => e.Files).Include(e => e.User).Include(e => e.Address).FirstOrDefault();

                if (autoEscolaDb == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                if (!String.IsNullOrWhiteSpace(request.CorporateName)) autoEscolaDb.CorporateName = request.CorporateName;
                if (!String.IsNullOrWhiteSpace(request.FantasyName))
                {
                    autoEscolaDb.FantasyName = request.FantasyName;
                    autoEscolaDb.User.Name = request.FantasyName;
                }
                if (!String.IsNullOrWhiteSpace(request.Description)) autoEscolaDb.Description = request.Description;
                if (!String.IsNullOrWhiteSpace(request.Site)) autoEscolaDb.Site = request.Site;
                if (!String.IsNullOrWhiteSpace(request.Cep)) autoEscolaDb.Address.Cep = request.Cep;
                if (!String.IsNullOrWhiteSpace(request.Uf)) autoEscolaDb.Address.Uf = request.Uf;
                if (!String.IsNullOrWhiteSpace(request.Address)) autoEscolaDb.Address.Address = request.Address;
                if (!String.IsNullOrWhiteSpace(request.District)) autoEscolaDb.Address.District = request.District;
                if (!String.IsNullOrWhiteSpace(request.City)) autoEscolaDb.Address.City = request.City;
                if (!String.IsNullOrWhiteSpace(request.Number)) autoEscolaDb.Address.Number = request.Number;
                if (request.DataFundacao >= DateTime.Today) throw new CustomException("Data da fundação inválida", HttpStatusCode.BadRequest);
                if (request.DataFundacao.Year > 1700) autoEscolaDb.FoundingDate = request.DataFundacao;


                if (!String.IsNullOrWhiteSpace(request.StateRegistration) && !request.StateRegistration.Equals(autoEscolaDb.StateRegistration))
                    if (_autoEscolaRepository.Exists(e => e.StateRegistration == request.StateRegistration))
                        throw new CustomException("Inscrição estadual já em uso.", HttpStatusCode.BadRequest);
                if (!String.IsNullOrWhiteSpace(request.Cnpj) && !request.Cnpj.Equals(autoEscolaDb.Cnpj))
                    if (_autoEscolaRepository.Exists(e => e.Cnpj == request.Cnpj))
                        throw new CustomException("CNPJ já em uso.", HttpStatusCode.BadRequest);
                if (!String.IsNullOrWhiteSpace(request.Email) && !request.Email.Equals(autoEscolaDb.Email))
                    if (_autoEscolaRepository.Exists(e => e.Email == request.Email))
                        throw new CustomException("Email estadual já em uso.", HttpStatusCode.BadRequest);

                if (!String.IsNullOrWhiteSpace(request.StateRegistration)) autoEscolaDb.StateRegistration = request.StateRegistration;
                if (!String.IsNullOrWhiteSpace(request.Cnpj)) autoEscolaDb.Cnpj = request.Cnpj;
                if (!String.IsNullOrWhiteSpace(request.Email)) autoEscolaDb.Email = request.Email;

                /*Faz o upload dos Files no azure e tem como retorno uma lista com os dados do upload
                 * @return nome, formato e destino
                 */

                if (request.Files != null)
                {
                    var fileResult = await _mediator.Send(new FileUploadToAzureInput
                    {
                        Files = request.Files,
                        TypeUser = Constants.Roles.AUTOESCOLA
                    });
                    //Salva no banco todas as informações dos Files do upload
                    foreach (var item in fileResult.Files)
                    {
                        var arquivo = await _arquivoRepository.CreateAsync(item);
                        fileList.Add(item);
                    }
                    foreach (var item in fileList)
                    {
                        autoEscolaDb.Files.Add(item);
                    }
                }

                //VERIFICA SE O TELEFONE JÁ ESTÁ EM USO
                if (request.PhonesNumbers != null && request.PhonesNumbers.Count > 0)
                {
                    foreach (var item in request.PhonesNumbers)
                    {
                        var result = autoEscolaDb.PhonesNumbers.Where(e => e.PhoneNumber.Equals(item.PhoneNumber)).FirstOrDefault();
                        if (result == null)
                        {
                            var phoneResult = await _telefoneRepository.FindAsync(u => u.PhoneNumber == item.PhoneNumber);
                            if (phoneResult != null) throw new CustomException("Telefone: " + phoneResult.PhoneNumber + " já em uso");
                            autoEscolaDb.PhonesNumbers.Add(item);
                        }
                    }
                }

                _autoEscolaRepository.Update(autoEscolaDb);
                _autoEscolaRepository.Commit();
                _autoEscolaRepository.Save();

                return autoEscolaDb;
            }
            catch (CustomException e)
            {
                _autoEscolaRepository.Rollback();
                await _mediator.Send(new RemoveFromAzureInput()
                {
                    TypeUser = Constants.Roles.AUTOESCOLA,
                    Files = fileList
                });
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(DrivingSchoolUpdateHandler),
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
