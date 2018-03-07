using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace ChrisDeveloperAPI.Models
{
    // É possível adicionar dados do perfil do usuário adicionando mais propriedades na sua classe ApplicationUser, visite https://go.microsoft.com/fwlink/?LinkID=317594 para obter mais informações.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // authenticationType deve corresponder a um definido em CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Adicione declarações de usuários aqui
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("ChrisDeveloperDB", throwIfV1Schema: false) {

            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<JetSoluctionAPI.Models.Carrinho> Carrinhos { get; set; }

        public System.Data.Entity.DbSet<JetSoluctionAPI.Models.Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<JetSoluctionAPI.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<JetSoluctionAPI.Models.ProdutoCategoria> ProdutoCategorias { get; set; }

        public System.Data.Entity.DbSet<JetSoluctionAPI.Models.Produto> Produtos { get; set; }

        public System.Data.Entity.DbSet<JetSoluctionAPI.Models.CarrinhoItens> CarrinhoItens { get; set; }

        public System.Data.Entity.DbSet<JetSoluctionAPI.Models.Pedido> Pedidos { get; set; }

        public System.Data.Entity.DbSet<JetSoluctionAPI.Models.PedidoItens> PedidoItens { get; set; }
    }
}