namespace AulaRemota.Core.Entity
{
    public class EnderecoModel
    {
        public int Id { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string EnderecoLogradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        
    }
}
