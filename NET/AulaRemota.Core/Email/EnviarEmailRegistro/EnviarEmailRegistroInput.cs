using MediatR;

namespace AulaRemota.Core.Email.EnviarEmailRegistro
{
    public class EnviarEmailRegistroInput : IRequest<bool>
    {
        public string Para { get; set; }
        public string Senha { get; set; }
    }
}
