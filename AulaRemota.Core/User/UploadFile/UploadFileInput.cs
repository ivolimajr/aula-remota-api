using AulaRemota.Infra.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace AulaRemota.Core.User.UploadFile
{
    public class UploadFileInput : IRequest<List<FileModel>>
    {
        public ICollection<IFormFile> Files { get; set; }
        public int UserId { get; set; }
    }
}