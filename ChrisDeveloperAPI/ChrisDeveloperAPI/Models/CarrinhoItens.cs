using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JetSoluctionAPI.Models {

    [Table("carrinhoItens")]
    public class CarrinhoItens {
        [Key]
        public int carrinhoItens_id { get; set; }
        public int carrinhoItens_carrinho_id { get; set; }
        public int carrinhoItens_produto_id { get; set; }
        public float carrinhoItens_valorUnitario { get; set; }
        public int carrinhoItens_totalItem { get; set; }
        public int carrinhoItens_quantidade { get; set; }
        public DateTime carrinhoItens_dataCadastro { get; set; }

        [ForeignKey("carrinhoItens_carrinho_id")]public virtual Carrinho carrinho { get; set; }
        [ForeignKey("carrinhoItens_produto_id")]public virtual Produto produto { get; set; }
    }

    public class CarrinhoItensDTO {

        public int carrinhoItens_id { get; set; }
        public int carrinhoItens_carrinho_id { get; set; }
        public int carrinhoItens_produto_id { get; set; }
        public float carrinhoItens_valorUnitario { get; set; }
        public int carrinhoItens_totalItem { get; set; }
        public int carrinhoItens_quantidade { get; set; }
        public DateTime carrinhoItens_dataCadastro { get; set; }
    }

    public class CarrinhoItensDetailDTO {

        public int carrinhoItens_id { get; set; }
        public int carrinhoItens_carrinho_id { get; set; }
        public int carrinhoItens_produto_id { get; set; }
        public float carrinhoItens_valorUnitario { get; set; }
        public int carrinhoItens_totalItem { get; set; }
        public int carrinhoItens_quantidade { get; set; }
        public DateTime carrinhoItens_dataCadastro { get; set; }
    }
}