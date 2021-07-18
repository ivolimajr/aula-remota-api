using AulaRemota.Core.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Edriving.Deletar
{
    public class EdrivingDeletarHandler : IRequestHandler<EdrivingDeletarInput, bool>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;

        public EdrivingDeletarHandler(IRepository<EdrivingModel> edrivingRepository, IRepository<UsuarioModel> usuarioRepository)
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioRepository;
        } 

        public async Task<bool> Handle(EdrivingDeletarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            EdrivingModel edriving = await _edrivingRepository.GetByIdAsync(request.Id);
            if (edriving == null) throw new HttpClientCustomException("Não encontrado");

            UsuarioModel usuario = await _usuarioRepository.GetByIdAsync(edriving.UsuarioId);

            //REMOVE O OBJETO
            _edrivingRepository.Delete(edriving);
            _usuarioRepository.Delete(usuario);

            return true;
        }
    }
}
