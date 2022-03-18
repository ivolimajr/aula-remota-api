using MediatR;

namespace AulaRemota.Core.Email.SendSiginUpEmail
{
    public class SendSiginUpEmailInput : IRequest<bool>
    {
        public string To { get; set; }
        public string Password { get; set; }
    }
}
