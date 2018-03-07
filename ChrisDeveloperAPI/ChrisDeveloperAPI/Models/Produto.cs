using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace JetSoluctionAPI.Models {

    [Table("produto")]
    public class Produto {
        [Key]
        public int produto_id { get; set; }
        public string produto_nome { get; set; }
        public float produto_des { get; set; }
        public bool produto_ativo { get; set; }
        public float produto_preco { get; set; }
        public float produto_precoPromo { get; set; }
    }

    public class ProdutoDTO {

        public int produto_id { get; set; }
        public string produto_nome { get; set; }
        public float produto_des { get; set; }
        public bool produto_ativo { get; set; }
        public float produto_preco { get; set; }
        public float produto_precoPromo { get; set; }
    }

    public class ProdutoDetailDTO {

        public int produto_id { get; set; }
        public string produto_nome { get; set; }
        public float produto_des { get; set; }
        public bool produto_ativo { get; set; }
        public float produto_preco { get; set; }
        public float produto_precoPromo { get; set; }
    }
}