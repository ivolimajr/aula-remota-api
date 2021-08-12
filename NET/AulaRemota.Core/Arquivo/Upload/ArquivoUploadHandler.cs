using AulaRemota.Core.Helpers;
using AulaRemota.Infra.Models;
using AulaRemota.Infra.Repository;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public ArquivoUploadHandler(IRepository<ArquivoModel> arquivoRepository, IHttpContextAccessor context)
        {
            _arquivoRepository = arquivoRepository;
            _context = context;
            _basePath = Directory.GetCurrentDirectory() + "\\UploadDir\\AutoEscola\\";
        }

        public async Task<ArquivoUploadResponse> Handle(ArquivoUploadInput request, CancellationToken cancellationToken)
        {
            var listaArquivos = new List<ArquivoModel>();

            foreach (var item in request.Arquivos)
            {
                var fileType = Path.GetExtension(item.FileName);
                var baseUrl = _context.HttpContext.Request.Host;

                if (fileType.ToLower() == ".pdf" || fileType.ToLower() == ".jpg" || fileType.ToLower() == ".jpeg")
                {
                    var docName = Path.GetFileName(item.FileName);
                    var destino = Path.Combine(_basePath, "", docName);

                    var arquivo = new ArquivoModel()
                    {
                        Nome = Guid.NewGuid().ToString("N").Substring(0,5) + docName,
                        Formato = fileType,
                        Destino = Path.Combine(baseUrl + "/api/v1/autoescola/" + docName)
                    };

                    using var stream = new FileStream(destino, FileMode.Create);
                    await item.CopyToAsync(stream);

                    listaArquivos.Add(arquivo);
                }
                else
                {
                    throw new HttpClientCustomException("Formato de arquivo inválido");
                }
            }

            return new ArquivoUploadResponse() { Arquivos = listaArquivos };
        }
    }
}
