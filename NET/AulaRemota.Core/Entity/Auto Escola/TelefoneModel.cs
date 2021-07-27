namespace AulaRemota.Core.Entity.Auto_Escola
{
    public class TelefoneModel
    {
        public int Id { get; set; }
        public string Telefone { get; set; }

        public virtual EdrivingModel Edriving { get; set; }
        public virtual ParceiroModel Parceiro { get; set; }
        public virtual AutoEscolaModel AutoEscola { get; set; }
        public virtual AdministrativoModel Administrativo { get; set; }
        public virtual InstrutorModel Instrutor { get; set; }
        public virtual AlunoModel Aluno { get; set; }
    }
}
