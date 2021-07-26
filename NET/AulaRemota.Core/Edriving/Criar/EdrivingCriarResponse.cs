﻿using AulaRemota.Core.Entity.Auto_Escola;
using System.Collections.Generic;

namespace AulaRemota.Core.Entity.Edriving.Criar
{
    public class EdrivingCriarResponse
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Cpf { get; set; }

        public string Email { get; set; }

        public List<TelefoneModel> Telefone { get; set; }

        public int CargoId { get; set; }
        public EdrivingCargoModel Cargo { get; set; }

        public int UsuarioId { get; set; }
        public UsuarioModel Usuario { get; set; }
    }
}
