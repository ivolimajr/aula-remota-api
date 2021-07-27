
using AulaRemota.Core.Entity.Auto_Escola;

namespace AulaRemota.Core.Models
{
    public class ArquivoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Formato { get; set; }
        public string Destino { get; set; }

        public virtual AutoEscolaModel AutoEscola { get; set; }
        public virtual InstrutorModel Instrutor { get; set; }
    }
}
