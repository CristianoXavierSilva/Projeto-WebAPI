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
    public class ProdutoCategoriasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProdutoCategorias
        /// <summary>
        /// Documentação do método GET
        /// </summary>
        /// <returns> Retorna uma lista de categorias de produtos com suas datas de criação </returns>
        public IQueryable<ProdutoCategoriaDTO> GetProdutoCategorias() {
            var produtoCategoria =
                from pc in db.ProdutoCategorias
                select new ProdutoCategoriaDTO() {
                    categoria_id = pc.categoria_id,
                    produto_id = pc.produto_id,
                    produtoCategoria_dataCadastro = pc.produtoCategoria_dataCadastro
                };
            return produtoCategoria;
        }

        // GET: api/ProdutoCategorias/5
        /// <summary>
        /// Documentação do método GET com parâmetro
        /// </summary>
        /// <param name="id"> Identifica o registro que será recuperado </param>
        /// <returns> Exibe uma categoria de produtos de compras correspondente a identificação passada no parâmetro </returns
        [ResponseType(typeof(ProdutoCategoriaDetailDTO))]
        public async Task<IHttpActionResult> GetProdutoCategoria(int id) {
            var produtoCategoria =
                await db.ProdutoCategorias.Select(pc =>
                new ProdutoCategoriaDetailDTO() {
                    categoria_id = pc.categoria_id,
                    produto_id = pc.produto_id,
                    produtoCategoria_dataCadastro = pc.produtoCategoria_dataCadastro
                }).SingleOrDefaultAsync(pc => pc.categoria_id == id);

            if (produtoCategoria == null)
            {
                return NotFound();
            }

            return Ok(produtoCategoria);
        }

        // PUT: api/ProdutoCategorias/5
        /// <summary>
        /// Documentação do método PUT
        /// </summary>
        /// <param name="id"> Identificador, especificado pelo usuário, do registro que será atualizado </param>
        /// <param name="produtoCategoria"> Objeto contendo todo o conteúdo atualizado do registro </param>
        /// <returns> Confirma se a atualização do registro foi realizada ou não. </returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProdutoCategoria(int id, ProdutoCategoria produtoCategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != produtoCategoria.categoria_id)
            {
                return BadRequest();
            }

            db.Entry(produtoCategoria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdutoCategoriaExists(id))
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

        // POST: api/ProdutoCategorias
        /// <summary>
        /// Documentação do método POST
        /// </summary>
        /// <param name="produtoCategoria"> Trás o objeto contendo todos os dados do registro para este ser inserido na base de dados </param>
        /// <returns> Confirma se a criação do registro foi realizada ou não. </returns>
        [ResponseType(typeof(ProdutoCategoria))]
        public async Task<IHttpActionResult> PostProdutoCategoria(ProdutoCategoria produtoCategoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProdutoCategorias.Add(produtoCategoria);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProdutoCategoriaExists(produtoCategoria.categoria_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var dto = new ProdutoCategoriaDTO(){
                categoria_id = produtoCategoria.categoria_id,
                produto_id = produtoCategoria.produto_id,
                produtoCategoria_dataCadastro = produtoCategoria.produtoCategoria_dataCadastro
            };

            return CreatedAtRoute("DefaultApi", new { id = produtoCategoria.categoria_id }, dto);
        }

        // DELETE: api/ProdutoCategorias/5
        /// <summary>
        /// Documentação do método DELETE
        /// </summary>
        /// <param name="id"> Identifica o registro que será deletado </param>
        /// <returns> Confirma se a remoção do registro da base de dados foi realizada ou não. </returns>
        [ResponseType(typeof(ProdutoCategoria))]
        public async Task<IHttpActionResult> DeleteProdutoCategoria(int id)
        {
            ProdutoCategoria produtoCategoria = await db.ProdutoCategorias.FindAsync(id);
            if (produtoCategoria == null)
            {
                return NotFound();
            }

            db.ProdutoCategorias.Remove(produtoCategoria);
            await db.SaveChangesAsync();

            return Ok(produtoCategoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProdutoCategoriaExists(int id)
        {
            return db.ProdutoCategorias.Count(e => e.categoria_id == id) > 0;
        }
    }
}