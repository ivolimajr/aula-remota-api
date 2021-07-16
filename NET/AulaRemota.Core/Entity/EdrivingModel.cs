﻿using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Core.Entity
{
    public class EdrivingModel
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string FullName { get; set; }

        [Column(TypeName = "varchar(14)")]
        public string Cpf { get; set; }

        [Column(TypeName = "varchar(70)")]
        public string Email { get; set; }

        [Column(TypeName = "varchar(15)")] 
        public string Telefone { get; set; }

        public int CargoId { get; set; }
        public EdrivingCargoModel Cargo { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
