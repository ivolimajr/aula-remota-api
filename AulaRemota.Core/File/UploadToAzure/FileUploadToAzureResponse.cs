using AulaRemota.Infra.Entity;
using System.Collections.Generic;

namespace AulaRemota.Core.File.UploadToAzure
{
    public class FileUploadToAzureResponse
    {
        public ICollection<FileModel> Files { get; set; }
    }
}
