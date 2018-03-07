using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JetSoluctionAPI.Models {

    [Table("categoria")]
    public class Categoria {
        [Key]
        public int categoria_id { get; set; }
        public string categoria_nome { get; set; }
        public bool categoria_ativo { get; set; }
        public DateTime categoria_dataCadastro { get; set; }
    }

    public class CategoriaDTO {
        public int categoria_id { get; set; }
        public string categoria_nome { get; set; }
        public bool categoria_ativo { get; set; }
        public DateTime categoria_dataCadastro { get; set; }
    }

    public class CategoriaDetailDTO {

        public int categoria_id { get; set; }
        public string categoria_nome { get; set; }
        public bool categoria_ativo { get; set; }
        public DateTime categoria_dataCadastro { get; set; }
    }
}