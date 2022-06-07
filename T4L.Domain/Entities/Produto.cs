using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4L.Domain.Entities
{
    [Table("produto")]
    public class Produto
    {
        [Key]
        public int Cod { get; set; }

        [StringLength(100,ErrorMessage = "'Descrição' deve conter no máximo {1} caracteres")]
        [Required(ErrorMessage = "Informe a Descrição do produto.")]
        public string Descricao { get; set; }
      
        [Required(ErrorMessage = "Informe o grupo do produto.")]
        public int CodGrupo { get; set; }

        [StringLength(10, ErrorMessage = "'Código de barra' deve conter no máximo {1} caracteres")]    
        public string CodBarra { get; set; }

        [Required(ErrorMessage = "Informe preço de custo do produto.")]
        public decimal PrecoCusto { get; set; }

        [Required(ErrorMessage = "Informe preço de venda do produto.")]
        public decimal PrecoVenda { get; set; }

        [Required(ErrorMessage = "Informe a situação do produto.")]
        public int Ativo { get; set; }

        [Required(ErrorMessage = "Informe a data e hora do cadastro do produto.")]
        public DateTime DataHoraCadastro { get; set; }
    }
}
