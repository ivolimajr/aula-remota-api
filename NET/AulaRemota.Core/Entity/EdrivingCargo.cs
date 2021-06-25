using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AulaRemota.Core.Entity
{
    public class EdrivingCargo
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Cargo { get; set; }
    }
}
