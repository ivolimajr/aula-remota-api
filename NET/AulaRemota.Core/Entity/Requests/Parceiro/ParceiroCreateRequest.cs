namespace AulaRemota.Api.Models.Requests
{
    public class ParceiroCreateRequest
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Cnpj { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Descricao { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string EnderecoLogradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public string Senha { get; set; }
        public int CargoId { get; set; }
    }
}
