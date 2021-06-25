namespace AulaRemota.Api.Models.Requests
{
    public class EdrivingCreateRequest
    {
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }
        public int CargoId { get; set; }
    }
}
