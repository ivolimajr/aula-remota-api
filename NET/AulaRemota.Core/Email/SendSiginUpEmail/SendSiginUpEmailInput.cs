using MediatR;

namespace AulaRemota.Core.Email.SendSiginUpEmail
{
    public class SendSiginUpEmailInput : IRequest<bool>
    {
        public string Para { get; set; }
        public string Senha { get; set; }
    }
}
