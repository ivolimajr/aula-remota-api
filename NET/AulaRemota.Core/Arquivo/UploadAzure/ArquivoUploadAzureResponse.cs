using AulaRemota.Infra.Models;
using System.Collections.Generic;

namespace AulaRemota.Core.Arquivo.UploadAzure
{
    public class ArquivoUploadAzureResponse
    {
        public List<ArquivoModel> Arquivos { get; set; }
    }
}
