using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AulaRemota.Core.Usuario.AtualizarSenha
{
    public class UsuarioAtualizarSenhaInput : IRequest<bool>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string SenhaAtual { get; set; }

        [Required]
        [MaxLength(100)]
        public string NovaSenha { get; set; }
    }
}
