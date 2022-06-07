using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4L.Domain.Entities
{
    [Table("venda_produto")]
    public class VendaProduto
    {
        [Key]
        public long Cod { get; set; }

        [Required(ErrorMessage = "Informe o código da venda.")]
        public int CodVenda { get; set; }

        [Required(ErrorMessage = "Informe o código do produto.")]
        public int CodProduto { get; set; }       

        [Required(ErrorMessage = "Informe o preço de venda do produto.")]
        public decimal PrecoVenda { get; set; }

        [Required(ErrorMessage = "Informe a quantidade do produto.")]
        public decimal Quantidade { get; set; }
    }
}
