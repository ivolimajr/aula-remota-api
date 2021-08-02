using AulaRemota.Infra.Entity;
using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Parceiro.Deletar
{
    public class ParceiroDeletarHandler : IRequestHandler<ParceiroDeletarInput, bool>
    {
        private readonly IRepository<ParceiroModel> _parceiroRepository;
        private readonly IRepository<UsuarioModel> _usuarioRepository;
        private readonly IRepository<EnderecoModel> _enderecoRepository;

        public ParceiroDeletarHandler(IRepository<ParceiroModel> parceiroRepository, IRepository<UsuarioModel> usuarioRepository, IRepository<EnderecoModel> enderecoRepository)
        {
            _parceiroRepository = parceiroRepository;
            _usuarioRepository = usuarioRepository;
            _enderecoRepository = enderecoRepository;
        } 

        public async Task<bool> Handle(ParceiroDeletarInput request, CancellationToken cancellationToken)
        {
            if (request.Id == 0) throw new HttpClientCustomException("Id Inválido");

            ParceiroModel parceiro = await _parceiroRepository.GetByIdAsync(request.Id);
            if (parceiro == null) throw new HttpClientCustomException("Não encontrado");

            UsuarioModel usuario = await _usuarioRepository.GetByIdAsync(parceiro.UsuarioId);
            EnderecoModel endereco = await _enderecoRepository.GetByIdAsync(parceiro.EnderecoId);

            //REMOVE O OBJETO
            _parceiroRepository.Delete(parceiro);
            _usuarioRepository.Delete(usuario);
            _enderecoRepository.Delete(endereco);

            return true;
        }
    }
}
