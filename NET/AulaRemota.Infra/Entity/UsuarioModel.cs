using AulaRemota.Infra.Entity.Auto_Escola;
using System;
using System.Text.Json.Serialization;

namespace AulaRemota.Infra.Entity
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int status { get; set; } // 0 -> DELETADO (DELETE) | 1 -> ATIVO | 2 ->INATIVADO
        public int NivelAcesso { get; set; } // 10 a 19 -> PLATAFORMA | 20 a 29 -> PARCEIRO | 30 a 39 -> CFC | 40 a 49 -> ALUNO 

        [JsonIgnore]
        public virtual AlunoModel Aluno { get; set; }
        [JsonIgnore]
        public virtual AutoEscolaModel AutoEscola { get; set; }
        [JsonIgnore]
        public virtual InstrutorModel Instrutor { get; set; }
        [JsonIgnore]
        public virtual AdministrativoModel Administrativo { get; set; }
        [JsonIgnore]
        public virtual EdrivingModel Edriving { get; set; }
        [JsonIgnore]
        public virtual ParceiroModel Parceiro { get; set; }

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
