namespace AulaRemota.Core.Entity.Edriving.Criar
{
    public class EdrivingCriarResponse
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public int CargoId { get; set; }
        public EdrivingCargoModel Cargo { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
