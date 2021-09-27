using MediatR;

namespace AulaRemota.Core.Arquivo.Deletar
{
    class ArquivoDeletarInput : IRequest<bool>
    {
        public int NivelAcesso { get; set; }
        public string NomeArquivo { get; set; }
    }
}