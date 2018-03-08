using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MacleodyDeveloper.Models;

namespace ChrisDeveloperAPI.Controllers
{
    public class ProdutosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Produtos
        /// <summary>
        /// Documentação do método GET
        /// </summary>
        /// <returns> Retorna uma lista de produtos com suas informações,
        /// tais como: nome, seus status, preço normal, desconto e preço promocional</returns>
        public IQueryable<ProdutoDTO> GetProdutos() {
            var produtos =
                from p in db.Produtos
                select new ProdutoDTO() {
                    produto_id = p.produto_id,
                    produto_nome = p.produto_nome,
                    produto_ativo = p.produto_ativo,
                    produto_des = p.produto_des,
                    produto_preco = p.produto_preco,
                    produto_precoPromo = p.produto_precoPromo
                };
            return produtos;
        }

        // GET: api/Produtos/5
        /// <summary>
        /// Documentação do método GET com parâmetro
        /// </summary>
        /// <param name="id"> Identifica o registro que será recuperado </param>
        /// <returns> Exibe um produto de acordo com o registro especificado </returns>
        [ResponseType(typeof(ProdutoDetailDTO))]
        public async Task<IHttpActionResult> GetProduto(int id) {
            var produto =
                await db.Produtos.Select(p =>
                new ProdutoDetailDTO() {
                    produto_id = p.produto_id,
                    produto_nome = p.produto_nome,
                    produto_ativo = p.produto_ativo,
                    produto_des = p.produto_des,
                    produto_preco = p.produto_preco,
                    produto_precoPromo = p.produto_precoPromo
                }).SingleOrDefaultAsync(p => p.produto_id == id);

            if (produto == null) {
                return NotFound();
            }

            return Ok(produto);
        }

        // PUT: api/Produtos/5
        /// <summary>
        /// Documentação do método PUT
        /// </summary>
        /// <param name="id"> Identificador, especificado pelo usuário, do registro que será atualizado </param>
        /// <param name="produto"> Objeto contendo todo o conteúdo atualizado do registro </param>
        /// <returns> Confirma se a atualização do registro foi realizada ou não. </returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProduto(int id, Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produto.produto_id)
            {
                return BadRequest();
            }

            db.Entry(produto).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Produtos
        /// <summary>
        /// Documentação do método POST
        /// </summary>
        /// <param name="produto"> Trás o objeto contendo todos os dados do registro para este ser inserido na base de dados </param>
        /// <returns> Confirma se a criação do registro foi realizada ou não. </returns>
        [ResponseType(typeof(Produto))]
        public async Task<IHttpActionResult> PostProduto(Produto produto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Produtos.Add(produto);
            await db.SaveChangesAsync();

            var dto = new ProdutoDTO() {
                produto_id = produto.produto_id,
                produto_nome = produto.produto_nome,
                produto_ativo = produto.produto_ativo,
                produto_des = produto.produto_des,
                produto_preco = produto.produto_preco,
                produto_precoPromo = produto.produto_precoPromo
            };

            return CreatedAtRoute("DefaultApi", new { id = produto.produto_id }, dto);
        }

        // DELETE: api/Produtos/5
        /// <summary>
        /// Documentação do método DELETE
        /// </summary>
        /// <param name="id"> Identifica o registro que será deletado </param>
        /// <returns> Confirma se a remoção do registro da base de dados foi realizada ou não. </returns>
        [ResponseType(typeof(Produto))]
        public async Task<IHttpActionResult> DeleteProduto(int id)
        {
            Produto produto = await db.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }

            db.Produtos.Remove(produto);
            await db.SaveChangesAsync();

            return Ok(produto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutoExists(int id)
        {
            return db.Produtos.Count(e => e.produto_id == id) > 0;
        }
    }
}