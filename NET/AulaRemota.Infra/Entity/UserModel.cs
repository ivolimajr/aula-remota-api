using AulaRemota.Infra.Entity.DrivingSchool;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class UserModel
    {
        public UserModel()
        {
            Roles = new List<RolesModel>();
        }

        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Nome { get; set; }
        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }

        [JsonIgnore]
        [Column(TypeName = "varchar(150)")]
        public string Password { get; set; }
        public int status { get; set; } // 0 -> DELETADO (DELETE) | 1 -> ATIVO | 2 ->INATIVADO
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public virtual ICollection<RolesModel> Roles { get; set; }

        [JsonIgnore]
        public virtual StudentModel Aluno { get; set; }
        [JsonIgnore]
        public virtual DrivingSchoolModel AutoEscola { get; set; }
        [JsonIgnore]
        public virtual InstructorModel Instrutor { get; set; }
        [JsonIgnore]
        public virtual AdministrativeModel Administrativo { get; set; }
        [JsonIgnore]
        public virtual EdrivingModel Edriving { get; set; }
        [JsonIgnore]
        public virtual PartnnerModel Parceiro { get; set; }

        public string GerarSenhas()
        {
            int Tamanho = 10; // Numero de digitos da senha
            string senha = "Edriv@";
            for (int i = senha.Length; i < Tamanho; i++)
            {
                Random random = new Random();
                int codigo = Convert.ToInt32(random.Next(48, 122).ToString());

                if ((codigo >= 48 && codigo <= 57) || (codigo >= 97 && codigo <= 122))
                {
                    string _char = ((char)codigo).ToString();
                    if (!senha.Contains(_char))
                    {
                        senha += _char;
                    }
                    else
                    {
                        i--;
                    }
                }
                else
                {
                    i--;
                }
            }
            return senha;
        }
    }
}
