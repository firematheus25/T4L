using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace T4L.Domain.Entities.Vw
{
    public class VwConsulta
    {
        [Key]
        public int Cod { get; set; }
        public string Descricao { get; set; }

        public string Grupo { get; set; }

        public decimal PrecoCusto { get; set; }

        public decimal PrecoVenda { get; set; }
    }
}
