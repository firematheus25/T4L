using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T4L.Domain.Entities
{
    [Table("produto_grupo")]
    public class ProdutoGrupo
    {
        [Key]
        public long Cod { get; set; }

        [StringLength(50, ErrorMessage = "'Nome' deve conter no máximo {1} caracteres")]
        [Required(ErrorMessage = "Informe o nome do grupo.")]
        public string Nome { get; set; }
    }
}
