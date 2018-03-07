using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JetSoluctionAPI.Models {

    [Table("pedidoItens")]
    public class PedidoItens {
        [Key]
        public int pedidoItens_id { get; set; }
        public int pedidoItens_pedido_id { get; set; }
        public int pedidoItens_produto_id { get; set; }
        public float pedidoItens_valorUnidade { get; set; }
        public float pedidoItens_valorTotal { get; set; }
        public int pedidoItens_quantidade { get; set; }
        public DateTime pedidoItens_dataCadastro { get; set; }

        [ForeignKey("pedidoItens_pedido_id")] public virtual Carrinho pedido { get; set; }
        [ForeignKey("pedidoItens_produto_id")] public virtual Produto produto { get; set; }
    }

    public class PedidoItensDTO {

        public int pedidoItens_id { get; set; }
        public int pedidoItens_pedido_id { get; set; }
        public int pedidoItens_produto_id { get; set; }
        public float pedidoItens_valorUnidade { get; set; }
        public float pedidoItens_valorTotal { get; set; }
        public int pedidoItens_quantidade { get; set; }
        public DateTime pedidoItens_dataCadastro { get; set; }
    }

    public class PedidoItensDetailDTO {

        public int pedidoItens_id { get; set; }
        public int pedidoItens_pedido_id { get; set; }
        public int pedidoItens_produto_id { get; set; }
        public float pedidoItens_valorUnidade { get; set; }
        public float pedidoItens_valorTotal { get; set; }
        public int pedidoItens_quantidade { get; set; }
        public DateTime pedidoItens_dataCadastro { get; set; }
    }
}