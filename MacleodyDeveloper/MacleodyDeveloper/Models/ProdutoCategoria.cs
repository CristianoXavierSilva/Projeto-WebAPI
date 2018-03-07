using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacleodyDeveloper.Models {

    [Table("produtoCategoria")]
    public class ProdutoCategoria {

        [Key, Column(Order = 0)]
        public int categoria_id { get; set; }

        [Key, Column(Order = 1)]
        public int produto_id { get; set; }
        public DateTime produtoCategoria_dataCadastro { get; set; }

        [ForeignKey("categoria_id")] public virtual Categoria categoria { get; set; }
        [ForeignKey("produto_id")] public virtual Produto produto { get; set; }
    }

    public class ProdutoCategoriaDTO {

        public int categoria_id { get; set; }
        public int produto_id { get; set; }
        public DateTime produtoCategoria_dataCadastro { get; set; }
    }

    public class ProdutoCategoriaDetailDTO {

        public int categoria_id { get; set; }
        public int produto_id { get; set; }
        public DateTime produtoCategoria_dataCadastro { get; set; }
    }
}