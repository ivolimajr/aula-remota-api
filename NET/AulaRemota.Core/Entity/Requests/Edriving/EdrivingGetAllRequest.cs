namespace AulaRemota.Api.Models.Requests
{
    public class EdrivingGetAllRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public int CargoId { get; set; }
        public string Cargo { get; set; }
        public int NivelAcesso { get; set; }
        public int Status { get; set; }
    }
}
