using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Models;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AulaRemota.Core.Arquivo.Upload
{
    public class ArquivoUploadHandler : IRequestHandler<ArquivoUploadInput, ArquivoUploadResponse>
    {
        private readonly string _basePath;
        private readonly IRepository<ArquivoModel> _arquivoRepository;
        private readonly IHttpContextAccessor _context;

        public ArquivoUploadHandler(IRepository<ArquivoModel> arquivoRepository, IHttpContextAccessor context )
        {
            _arquivoRepository = arquivoRepository;
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\";
        }

        public async Task<ArquivoUploadResponse> Handle(ArquivoUploadInput request, CancellationToken cancellationToken)
        {
            var fileType = Path.GetExtension(request.Arquivo.FileName);
            var baseUrl = _context.HttpContext.Request.Host;

            if(fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".jpeg")
            {
                var docName = Path.GetFileName(request.Arquivo.FileName);
                var destino = Path.Combine(_basePath, "", docName);

                var arquivo = new ArquivoModel()
                {
                    Nome = docName,
                    Formato = fileType,
                    Destino = Path.Combine(baseUrl + "/api/v1/autoescola/" + docName)
                };

                using var stream = new FileStream(destino, FileMode.Create);
                await request.Arquivo.CopyToAsync(stream);



                return new ArquivoUploadResponse()
                {
                    Nome = arquivo.Nome,
                    Formato = arquivo.Formato,
                    Destino = arquivo.Destino
                };
            }
            throw new HttpClientCustomException("Formato de arquivo inválido");
        }
    }
}
