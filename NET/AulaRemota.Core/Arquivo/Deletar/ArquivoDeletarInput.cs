using AulaRemota.Infra.Models;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.Arquivo.Deletar
{
    class ArquivoDeletarInput : IRequest<bool>
    {
        public string TipoUsuario { get; set; }
        public List<ArquivoModel> Arquivos { get; set; }
    }
}