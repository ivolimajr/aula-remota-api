﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AulaRemota.Infra.Entity.Auto_Escola
{
    public class TurmaModel
    {
        public TurmaModel()
        {
            this.Alunos = new List<AlunoModel>();
            this.Cursos = new List<CursoModel>();
        }
        public int Id { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string Codigo { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public int AutoEscolaId { get; set; }
        public AutoEscolaModel AutoEscola { get; set; }

        public int InstrutorId { get; set; }
        public InstrutorModel Instrutor { get; set; }

        public virtual List<AlunoModel> Alunos { get; set; }
        public virtual List<CursoModel> Cursos { get; set; }
    }
}
