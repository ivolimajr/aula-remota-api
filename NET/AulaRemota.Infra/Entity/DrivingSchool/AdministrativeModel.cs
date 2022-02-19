using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.DrivingSchool
{
    public class AdministrativeModel
    {
        public AdministrativeModel()
        {
            this.Telefones = new List<PhoneModel>();
        }

        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Nome { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }
        [Column(TypeName = "varchar(14)")]
        public string Cpf { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string Identidade { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Orgão { get; set; }
        public DateTime Aniversario { get; set; }

        public int EnderecoId { get; set; }
        public AddressModel Endereco { get; set; }
        public int UsuarioId { get; set; }
        public UserModel Usuario { get; set; }
        public int AutoEscolaId { get; set; }
        public DrivingSchoolModel AutoEscola { get; set; }

        public ICollection<PhoneModel> Telefones { get; set; }
    }
}
