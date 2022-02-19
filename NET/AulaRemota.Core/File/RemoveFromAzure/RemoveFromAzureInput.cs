using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.File.RemoveFromAzure
{
    class RemoveFromAzureInput : IRequest<bool>
    {
        public string TipoUsuario { get; set; }
        public List<FileModel> Arquivos { get; set; }
    }
}