using AulaRemota.Infra.Entity;
using System.Collections.Generic;

namespace AulaRemota.Core.User.UploadFile
{
    public class UploadFileResponse
    {
        public ICollection<FileModel> Files { get; set; }
    }
}