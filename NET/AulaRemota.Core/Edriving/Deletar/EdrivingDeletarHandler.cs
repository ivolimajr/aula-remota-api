using AulaRemota.Core.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Core.Interfaces.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Edriving.Deletar
{
    public class EdrivingDeletarHandler : IRequestHandler<EdrivingDeletarInput, string>
    {
        private readonly IRepository<EdrivingModel> _edrivingRepository;
        private readonly IRepository<Usuario> _usuarioRepository;

        public EdrivingDeletarHandler(IRepository<EdrivingModel> edrivingRepository, IRepository<Usuario> usuarioRepository)
        {
            _edrivingRepository = edrivingRepository;
            _usuarioRepository = usuarioRepository;
        } 

        public async Task<string> Handle(EdrivingDeletarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            EdrivingModel edriving = await _edrivingRepository.GetByIdAsync(request.Id);
            if (edriving == null) throw new HttpClientCustomException("Não encontrado");

            Usuario usuario = await _usuarioRepository.GetByIdAsync(edriving.UsuarioId);

            //REMOVE O OBJETO
            _edrivingRepository.Delete(edriving);
            _usuarioRepository.Delete(usuario);

            return "Removido Com Sucesso";
        }
    }
}
