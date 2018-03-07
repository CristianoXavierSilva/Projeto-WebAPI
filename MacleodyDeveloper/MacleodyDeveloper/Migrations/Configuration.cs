namespace MacleodyDeveloper.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using MacleodyDeveloper.Models;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MacleodyDeveloper.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MacleodyDeveloper.Models.ApplicationDbContext context)
        {
            context.Clientes.AddOrUpdate(x => x.cliente_id,
                new Cliente() { nome = "Cristiano Xavier Silva", email = "cristiano.anime93@gmail.com", dataCadastro = DateTime.Now },
                new Cliente() { nome = "João da Silva", email = "joao.silva@gmail.com", dataCadastro = DateTime.Now }
            );

            context.Categorias.AddOrUpdate(x => x.categoria_id,
                new Categoria() { categoria_nome = "Produtos", categoria_ativo = true, categoria_dataCadastro = DateTime.Now },
                new Categoria() { categoria_nome = "Institucional", categoria_ativo = true, categoria_dataCadastro = DateTime.Now },
                new Categoria() { categoria_nome = "Serviços", categoria_ativo = true, categoria_dataCadastro = DateTime.Now },
                new Categoria() { categoria_nome = "Calçados", categoria_ativo = true, categoria_dataCadastro = DateTime.Now },
                new Categoria() { categoria_nome = "Cama mesa e banho", categoria_ativo = true, categoria_dataCadastro = DateTime.Now },
                new Categoria() { categoria_nome = "Eletrodomésticos", categoria_ativo = true, categoria_dataCadastro = DateTime.Now },
                new Categoria() { categoria_nome = "Eletroeletrônicos", categoria_ativo = true, categoria_dataCadastro = DateTime.Now },
                new Categoria() { categoria_nome = "Roupas Masculinas", categoria_ativo = true, categoria_dataCadastro = DateTime.Now },
                new Categoria() { categoria_nome = "Roupas Femininas", categoria_ativo = true, categoria_dataCadastro = DateTime.Now },
                new Categoria() { categoria_nome = "Roupas Infantis", categoria_ativo = true, categoria_dataCadastro = DateTime.Now }
            );

            context.Produtos.AddOrUpdate(x => x.produto_id,
                new Produto() { produto_nome = "Sapa-tênis Freeway", produto_des = 15, produto_ativo = true, produto_preco = 279.90F, produto_precoPromo = 259.90F },
                new Produto() { produto_nome = "Rasteirinha Riachuelo", produto_des = 18, produto_ativo = true, produto_preco = 89.90F, produto_precoPromo = 69.90F },
                new Produto() { produto_nome = "Chinelo Havaianas", produto_des = 25, produto_ativo = true, produto_preco = 79.90F, produto_precoPromo = 49.90F },
                new Produto() { produto_nome = "Camiseta Masc. Quick Silver", produto_des = 20, produto_ativo = true, produto_preco = 119.90F, produto_precoPromo = 99.90F },
                new Produto() { produto_nome = "Camiseta Femi. Quick Silver", produto_des = 20, produto_ativo = true, produto_preco = 129.90F, produto_precoPromo = 109.90F }
            );

            context.ProdutoCategorias.AddOrUpdate(x => new { x.categoria_id, x.produto_id },
                new ProdutoCategoria() { categoria_id = 4, produto_id = 1, produtoCategoria_dataCadastro = DateTime.Now },
                new ProdutoCategoria() { categoria_id = 4, produto_id = 2, produtoCategoria_dataCadastro = DateTime.Now },
                new ProdutoCategoria() { categoria_id = 4, produto_id = 3, produtoCategoria_dataCadastro = DateTime.Now },
                new ProdutoCategoria() { categoria_id = 8, produto_id = 4, produtoCategoria_dataCadastro = DateTime.Now },
                new ProdutoCategoria() { categoria_id = 9, produto_id = 5, produtoCategoria_dataCadastro = DateTime.Now }
            );
        }
    }
}
