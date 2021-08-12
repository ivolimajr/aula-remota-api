using AulaRemota.Infra.Models;
using System.Collections.Generic;

namespace AulaRemota.Core.Arquivo.Upload
{
    public class ArquivoUploadResponse
    {
        public List<ArquivoModel> Arquivos { get; set; }
    }
}
