using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MacleodyDeveloper.Models {

    [Table("pedido")]
    public class Pedido {
        [Key]
        public int pedido_id { get; set; }
        public string pedido_valor { get; set; }
        public int carrinhoItens_id { get; set; }
        public DateTime pedido_dataCadastro { get; set; }

        [ForeignKey("carrinhoItens_id")]public virtual CarrinhoItens carrinhoItens { get; set; }
    }

    public class PedidoDTO {

        public int pedido_id { get; set; }
        public string pedido_valor { get; set; }
        public int carrinhoItens_id { get; set; }
        public DateTime pedido_dataCadastro { get; set; }
    }

    public class PedidoDetailDTO {

        public int pedido_id { get; set; }
        public string pedido_valor { get; set; }
        public int carrinhoItens_id { get; set; }
        public DateTime pedido_dataCadastro { get; set; }
    }
}