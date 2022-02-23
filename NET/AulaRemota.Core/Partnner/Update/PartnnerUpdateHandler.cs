using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Entity.DrivingSchool;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace AulaRemota.Core.Partnner.Update
{
    public class PartnnerUpdateHandler : IRequestHandler<PartnnerUpdateInput, PartnnerUpdateResponse>
    {
        private readonly IRepository<PartnnerModel, int> _parceiroRepository;
        private readonly IRepository<UserModel, int>_usuarioRepository;
        private readonly IRepository<PartnnerLevelModel, int>_cargoRepository;
        private readonly IRepository<PhoneModel, int> _telefoneRepository;

        public PartnnerUpdateHandler(
                IRepository<PartnnerModel, int> parceiroRepository,
                IRepository<UserModel, int>usuarioRepository,
                IRepository<PartnnerLevelModel, int>cargoRepository,
                IRepository<PhoneModel, int> telefoneRepository
         )
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<PartnnerUpdateResponse> Handle(PartnnerUpdateInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                _parceiroRepository.CreateTransaction();
                //BUSCA O OBJETO A SER ATUALIZADO
                var entity = await _parceiroRepository.Context
                        .Set<PartnnerModel>()
                        .Include(e => e.Level)
                        .Include(e => e.User)
                        .Include(e => e.Address)
                        .Include(e => e.PhonesNumbers)
                        .Where(e => e.Id == request.Id)
                        .FirstOrDefaultAsync();

                if (entity == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                //SE FOR INFORMADO UM NOVO Level, O Level ATUAL SERÁ ATUALIZADO
                if (request.LevelId > 0 && request.LevelId != entity.LevelId)
                {
                    //VERIFICA SE O Level INFORMADO EXISTE
                    var Level = await _cargoRepository.FindAsync(request.LevelId);
                    if (Level == null) throw new CustomException("Level Não Encontrado", HttpStatusCode.NotFound);

                    //SE O Level EXISTE, O OBJETO SERÁ ATUALIZADO
                    entity.LevelId = Level.Id;
                    entity.Level = Level;
                }

                //ATUALIZA O TELEFONE SE VIER NA LISTA DO REQUEST
                if (request.PhonesNumbers.Count > 0)
                {
                    foreach (var item in request.PhonesNumbers)
                    {
                        //VERIFICA SE JÁ NÃO É O MESMO QUE ESTÁ CADASTRADO
                        if (!entity.PhonesNumbers.Any(e => e.PhoneNumber == item.PhoneNumber))
                        {
                            //VERIFICA SE JÁ EXISTEM UM TELEFONE NO BANCO EM USO
                            var telefoneResult = await _telefoneRepository.FirstOrDefaultAsync(e => e.PhoneNumber == item.PhoneNumber);

                            //SE O TELEFONE NÃO TIVER ID, É UM TELEFONE NOVO. CASO CONTRÁRIO É ATUALIZADO.
                            if (item.Id == 0)
                            {
                                //ESSA CONDIÇÃO RETORNA ERRO CASO O TELEFONE ESTEJA EM USO
                                if (telefoneResult != null) throw new CustomException("Telefone: " + telefoneResult.PhoneNumber + " já em uso");
                                entity.PhonesNumbers.Add(item);
                            }
                            else
                            {
                                //ESSA CONDIÇÃO RETORNA ERRO CASO O TELEFONE ESTEJA EM USO
                                if (telefoneResult != null) throw new CustomException("Telefone: " + telefoneResult.PhoneNumber + " já em uso");
                                _telefoneRepository.Update(item);
                            }
                        }
                    }
                }

                //FAZ O SET DO User
                if (request.Name != null) entity.User.Name = request.Name.ToUpper();
                if (request.Email != null) entity.User.Email = request.Email.ToUpper();


                // FAZ O SET DO PARCEIRO
                if (request.Name != null) entity.Name = request.Name.ToUpper();
                if (request.Email != null) entity.Email = request.Email.ToUpper();
                if (request.Cnpj != null) entity.Cnpj = request.Cnpj.ToUpper();
                if (request.Description != null) entity.Description = request.Description.ToUpper();

                // FAZ O SET DOS ATRIBUTOS A SER ATUALIZADO 
                if (request.Uf != null) entity.Address.Uf = request.Uf.ToUpper();
                if (request.Cep != null) entity.Address.Cep = request.Cep.ToUpper();
                if (request.Address != null) entity.Address.Address = request.Address.ToUpper();
                if (request.District != null) entity.Address.District = request.District.ToUpper();
                if (request.City != null) entity.Address.City = request.City.ToUpper();
                if (request.Number != null) entity.Address.Number = request.Number.ToUpper();

                _parceiroRepository.Update(entity);

                _parceiroRepository.Save();
                _parceiroRepository.Commit();

                return new PartnnerUpdateResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Email = entity.Email,
                    Cnpj = entity.Cnpj,
                    Description = entity.Description,
                    PhonesNumbers = entity.PhonesNumbers,
                    LevelId = entity.LevelId,
                    UserId = entity.UserId,
                    Level = entity.Level,
                    User = entity.User,
                    AddressId = entity.AddressId,
                    Address = entity.Address
                };
            }
            catch (CustomException e)
            {
                _parceiroRepository.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(PartnnerUpdateHandler),
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
