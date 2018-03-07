using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JetSoluctionAPI.Models {

    [Table("carrinho")]
    public class Carrinho {
        [Key]
        public int carrinho_id { get; set; }
        public int cliente_id { get; set;  }
        public DateTime carrinho_dataCadastro { get; set; }
        public float carrinho_total { get; set; }

        [ForeignKey("cliente_id")] public virtual Cliente cliente { get; set; }
    }

    public class CarrinhoDTO {

        public int carrinho_id { get; set; }
        public int cliente_id { get; set; }
        public DateTime carrinho_dataCadastro { get; set; }
        public float carrinho_total { get; set; }
    }

    public class CarrinhoDetailDTO {

        public int carrinho_id { get; set; }
        public int cliente_id { get; set; }
        public DateTime carrinho_dataCadastro { get; set; }
        public float carrinho_total { get; set; }
    }
}