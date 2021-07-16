using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class EnderecoModel
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(2)")]
        public string Uf { get; set; }

        [Column(TypeName = "varchar(12)")]
        public string Cep { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string EnderecoLogradouro { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Bairro { get; set; }

        [Column(TypeName = "varchar(150)")]
        public string Cidade { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string Numero { get; set; }
        
    }
}
