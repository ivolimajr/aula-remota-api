using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace AulaRemota.Core.AuthUser.Criar
{
    public class AuthUserDeletarInput : IRequest<string>
    {
        public int Id { get; set; }
    }
}
