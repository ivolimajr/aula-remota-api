using AulaRemota.Infra.Entity;
using AulaRemota.Shared.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace AulaRemota.Core.Edriving.Remove
{
    public class EdrivingRemoveHandler : IRequestHandler<EdrivingRemoveInput, bool>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UserModel> _usuarioRepository;
        private readonly IRepository<PhoneModel> _telefoneRepository;

        public EdrivingRemoveHandler(
            IRepository<EdrivingModel> edrivingRepository, 
            IRepository<UserModel> usuarioRepository, 
            IRepository<PhoneModel> telefoneRepository
            )
        {
            _edrivingRepository = edrivingRepository;
            _telefoneRepository = telefoneRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> Handle(EdrivingRemoveInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new CustomException("Busca Inválida");
            try
            {
                _edrivingRepository.CreateTransaction();
                var edriving = await _edrivingRepository.Context
                                        .Set<EdrivingModel>()
                                        .Include(e => e.User)
                                        .Include(e => e.PhonesNumbers)
                                        .Where(e => e.Id == request.Id)
                                        .FirstOrDefaultAsync();
                if (edriving == null) throw new CustomException("Não Encontrado", HttpStatusCode.NotFound);

                _edrivingRepository.Delete(edriving);
                _usuarioRepository.Delete(edriving.User);

                foreach (var item in edriving.PhonesNumbers)
                {
                    item.Edriving = null;
                    _telefoneRepository.Delete(item);
                }

                _edrivingRepository.Save();
                _edrivingRepository.Commit();
                return true;
            }
            catch (CustomException e)
            {
                _edrivingRepository.Rollback();
                throw new CustomException(new ResponseModel
                {
                    UserMessage = e.Message,
                    ModelName = nameof(EdrivingRemoveInput),
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
