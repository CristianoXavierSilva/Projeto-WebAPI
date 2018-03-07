using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JetSoluctionAPI.Models {

    [Table("cliente")]
    public class Cliente {
        [Key]
        public int cliente_id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime dataCadastro { get; set; }
    }
    
    public class ClienteDTO {

        public int cliente_id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime dataCadastro { get; set; }
    }

    public class ClienteDetailDTO {

        public int cliente_id { get; set; }
        public string nome { get; set; }
        public string email { get; set; }
        public DateTime dataCadastro { get; set; }
    }
}