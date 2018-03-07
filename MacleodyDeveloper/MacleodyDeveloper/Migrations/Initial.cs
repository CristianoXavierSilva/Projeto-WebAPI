namespace MacleodyDeveloper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.carrinhoItens",
                c => new
                    {
                        carrinhoItens_id = c.Int(nullable: false, identity: true),
                        carrinhoItens_carrinho_id = c.Int(nullable: false),
                        carrinhoItens_produto_id = c.Int(nullable: false),
                        carrinhoItens_valorUnitario = c.Single(nullable: false),
                        carrinhoItens_totalItem = c.Int(nullable: false),
                        carrinhoItens_quantidade = c.Int(nullable: false),
                        carrinhoItens_dataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.carrinhoItens_id)
                .ForeignKey("dbo.carrinho", t => t.carrinhoItens_carrinho_id, cascadeDelete: true)
                .ForeignKey("dbo.produto", t => t.carrinhoItens_produto_id, cascadeDelete: true)
                .Index(t => t.carrinhoItens_carrinho_id)
                .Index(t => t.carrinhoItens_produto_id);
            
            CreateTable(
                "dbo.carrinho",
                c => new
                    {
                        carrinho_id = c.Int(nullable: false, identity: true),
                        cliente_id = c.Int(nullable: false),
                        carrinho_dataCadastro = c.DateTime(nullable: false),
                        carrinho_total = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.carrinho_id)
                .ForeignKey("dbo.cliente", t => t.cliente_id, cascadeDelete: true)
                .Index(t => t.cliente_id);
            
            CreateTable(
                "dbo.cliente",
                c => new
                    {
                        cliente_id = c.Int(nullable: false, identity: true),
                        nome = c.String(),
                        email = c.String(),
                        dataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.cliente_id);
            
            CreateTable(
                "dbo.produto",
                c => new
                    {
                        produto_id = c.Int(nullable: false, identity: true),
                        produto_nome = c.String(),
                        produto_des = c.Single(nullable: false),
                        produto_ativo = c.Boolean(nullable: false),
                        produto_preco = c.Single(nullable: false),
                        produto_precoPromo = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.produto_id);
            
            CreateTable(
                "dbo.categoria",
                c => new
                    {
                        categoria_id = c.Int(nullable: false, identity: true),
                        categoria_nome = c.String(),
                        categoria_ativo = c.Boolean(nullable: false),
                        categoria_dataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.categoria_id);
            
            CreateTable(
                "dbo.pedidoItens",
                c => new
                    {
                        pedidoItens_id = c.Int(nullable: false, identity: true),
                        pedidoItens_pedido_id = c.Int(nullable: false),
                        pedidoItens_produto_id = c.Int(nullable: false),
                        pedidoItens_valorUnidade = c.Single(nullable: false),
                        pedidoItens_valorTotal = c.Single(nullable: false),
                        pedidoItens_quantidade = c.Int(nullable: false),
                        pedidoItens_dataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.pedidoItens_id)
                .ForeignKey("dbo.carrinho", t => t.pedidoItens_pedido_id, cascadeDelete: true)
                .ForeignKey("dbo.produto", t => t.pedidoItens_produto_id, cascadeDelete: true)
                .Index(t => t.pedidoItens_pedido_id)
                .Index(t => t.pedidoItens_produto_id);
            
            CreateTable(
                "dbo.pedido",
                c => new
                    {
                        pedido_id = c.Int(nullable: false, identity: true),
                        pedido_valor = c.String(),
                        carrinhoItens_id = c.Int(nullable: false),
                        pedido_dataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.pedido_id)
                .ForeignKey("dbo.carrinhoItens", t => t.carrinhoItens_id, cascadeDelete: true)
                .Index(t => t.carrinhoItens_id);
            
            CreateTable(
                "dbo.produtoCategoria",
                c => new
                    {
                        categoria_id = c.Int(nullable: false),
                        produto_id = c.Int(nullable: false),
                        produtoCategoria_dataCadastro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.categoria_id, t.produto_id })
                .ForeignKey("dbo.categoria", t => t.categoria_id, cascadeDelete: true)
                .ForeignKey("dbo.produto", t => t.produto_id, cascadeDelete: true)
                .Index(t => t.categoria_id)
                .Index(t => t.produto_id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.produtoCategoria", "produto_id", "dbo.produto");
            DropForeignKey("dbo.produtoCategoria", "categoria_id", "dbo.categoria");
            DropForeignKey("dbo.pedido", "carrinhoItens_id", "dbo.carrinhoItens");
            DropForeignKey("dbo.pedidoItens", "pedidoItens_produto_id", "dbo.produto");
            DropForeignKey("dbo.pedidoItens", "pedidoItens_pedido_id", "dbo.carrinho");
            DropForeignKey("dbo.carrinhoItens", "carrinhoItens_produto_id", "dbo.produto");
            DropForeignKey("dbo.carrinhoItens", "carrinhoItens_carrinho_id", "dbo.carrinho");
            DropForeignKey("dbo.carrinho", "cliente_id", "dbo.cliente");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.produtoCategoria", new[] { "produto_id" });
            DropIndex("dbo.produtoCategoria", new[] { "categoria_id" });
            DropIndex("dbo.pedido", new[] { "carrinhoItens_id" });
            DropIndex("dbo.pedidoItens", new[] { "pedidoItens_produto_id" });
            DropIndex("dbo.pedidoItens", new[] { "pedidoItens_pedido_id" });
            DropIndex("dbo.carrinho", new[] { "cliente_id" });
            DropIndex("dbo.carrinhoItens", new[] { "carrinhoItens_produto_id" });
            DropIndex("dbo.carrinhoItens", new[] { "carrinhoItens_carrinho_id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.produtoCategoria");
            DropTable("dbo.pedido");
            DropTable("dbo.pedidoItens");
            DropTable("dbo.categoria");
            DropTable("dbo.produto");
            DropTable("dbo.cliente");
            DropTable("dbo.carrinho");
            DropTable("dbo.carrinhoItens");
        }
    }
}
