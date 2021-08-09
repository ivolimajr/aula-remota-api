using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Usuario.AtualizarSenhaPorEmail
{
    public class UsuarioAtualizarSenhaPorEmailInput : IRequest<bool>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string SenhaAtual { get; set; }

        [Required]
        [MaxLength(100)]
        public string NovaSenha { get; set; }
    }
}
