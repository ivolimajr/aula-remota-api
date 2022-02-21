using AulaRemota.Infra.Entity;
using AulaRemota.Infra.Repository;
using AulaRemota.Shared.Helpers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace AulaRemota.Core.Edriving.Update
{
    public class EdrivingUpdateHandler : IRequestHandler<EdrivingUpdateInput, EdrivingUpdateResponse>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<EdrivingLevelModel> _cargoRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;

        public EdrivingUpdateHandler(
            IRepository<EdrivingModel> edrivingRepository,
            IRepository<UserModel> usuarioRepository,
            IRepository<EdrivingLevelModel> cargoRepository,
            IRepository<PhoneModel> telefoneRepository
            )
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioRepository;
            _cargoRepository = cargoRepository;
            _telefoneRepository = telefoneRepository;
        }

        public async Task<EdrivingUpdateResponse> Handle(EdrivingUpdateInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");

            try
            {
                //INICIA UMA TRANSAÇÃO
                _edrivingRepository.CreateTransaction();

                //BUSCA O OBJETO A SER ATUALIZADO
                var entity = _edrivingRepository.Context
                            .Set<EdrivingModel>()
                            .Include(e => e.User)
                            .Include(e => e.Level)
                            .Include(e => e.PhonesNumbers)
                            .Where(e => e.Id == request.Id)
                            .FirstOrDefault();

                //SE FOR NULO RETORNA NÃO ENCONTRADO
                if (entity == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                //ATUALIZA EMAIL SE INFORMADO - TANTO DO USUÁRIO COMO DO EDRIVING
                if (request.Email != null && request.Email != entity.Email)
                {
                    //VERIFICA SE O EMAIL JÁ ESTÁ EM USO POR OUTRO USUÁRIO
                    var emailUnique = await _usuarioRepository.FindAsync(u => u.Email == request.Email);
                    if (emailUnique != null && emailUnique.Id != request.Id) throw new CustomException("Email em uso");

                    //SE NÃO ESTPA EM USO SET OS ATRIBUTOS
                    entity.User.Email = request.Email.ToUpper();
                    entity.Email = request.Email.ToUpper();

                }
                //ATUALIZA NOME SE INFORMADO - TANTO DO USUÁRIO COMO DO EDRIVING
                if (request.Name != null && request.Name != entity.Name)
                {
                    entity.User.Name = request.Name.ToUpper();
                    entity.Name = request.Name.ToUpper();
                }
                //ATUALIZA O CPF SE INFORMADO
                if (request.Cpf != null && request.Cpf != entity.Cpf)
                {
                    //VERIFICA SE O CPF JÁ ESTÁ EM USO
                    var cpfUnique = await _edrivingRepository.FindAsync(u => u.Cpf == request.Cpf);
                    if (cpfUnique != null && cpfUnique.Id != request.Id) throw new CustomException("Cpf já existe em nossa base de dados");

                    //SE FOR NULO ELE ATUALIZA O CPF
                    entity.Cpf = request.Cpf.ToUpper();
                }
                //SE FOR INFORMADO UM NOVO Level, O Level ATUAL SERÁ ATUALIZADO
                if (request.LevelId > 0 && request.LevelId != entity.LevelId)
                {
                    //VERIFICA SE O Level INFORMADO EXISTE
                    var Level = await _cargoRepository.GetByIdAsync(request.LevelId);
                    if (Level == null) throw new CustomException("Level Não Encontrado", HttpStatusCode.NotFound);

                    //SE O Level EXISTE, O Level SERÁ ATUALIZADO
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
                            var telefoneResult = await _telefoneRepository.FindAsync(e => e.PhoneNumber == item.PhoneNumber);

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

                _edrivingRepository.Update(entity);

                _edrivingRepository.Save();
                _edrivingRepository.Commit();

                return new EdrivingUpdateResponse
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Email = entity.Email,
                    Cpf = entity.Cpf,
                    PhonesNumbers = entity.PhonesNumbers,
                    LevelId = entity.LevelId,
                    UserId = entity.UserId,
                    Level = entity.Level,
                    User = entity.User
                };
            }
            catch (CustomException e)
            {
                _edrivingRepository.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(EdrivingUpdateHandler),
                    Exception = e,
                    InnerException = e.InnerException,
                    StatusCode = e.ResponseModel.StatusCode
                });
            }
            finally
            {
                _edrivingRepository.Context.Dispose();
            }
        }
    }
}