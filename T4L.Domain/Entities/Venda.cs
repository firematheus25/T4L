using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4L.Domain.Entities
{
    [Table("venda")]
    public class Venda
    {
        [Key]
        public long Cod { get; set; }

        [StringLength(18, ErrorMessage = "'Documento Cliente' deve conter no máximo {1} caracteres.")]        
        public string ClienteDocumento { get; set; }

        [StringLength(50, ErrorMessage = "'Nome Cliente' deve conter no máximo {1} caracteres.")]
        public int ClienteNome { get; set; }

        [StringLength(300, ErrorMessage = "'Obs' deve conter no máximo {1} caracteres.")]
        public string Obs { get; set; }

        [Required(ErrorMessage = "Informe o valor total da venda.")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "Informe a data e hora da venda.")]
        public DateTime DataHora { get; set; }
    }
}
