using AulaRemota.Infra.Entity;
using MediatR;
using System.Collections.Generic;

namespace AulaRemota.Core.File.RemoveFromAzure
{
    public class RemoveFromAzureInput : IRequest<bool>
    {
        public string TypeUser { get; set; }

        public ICollection<FileModel> Files { get; set; }
    }
}