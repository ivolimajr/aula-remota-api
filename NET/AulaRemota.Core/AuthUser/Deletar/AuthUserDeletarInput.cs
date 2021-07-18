using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserDeletarInput : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
