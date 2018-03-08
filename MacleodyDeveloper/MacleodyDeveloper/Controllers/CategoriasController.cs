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
    public class CategoriasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Categorias
        /// <summary>
        /// Documentação do método GET
        /// </summary>
        /// <returns> Retorna uma lista de categorias com suas informações, tais como:
        /// O nome de cada categoria, seus status, isto é, se está ativa ou não e sua
        /// data de criação </returns>
        public IQueryable<CategoriaDTO> GetCategorias() {
            var categoria =
                from cat in db.Categorias
                select new CategoriaDTO() {
                    categoria_id = cat.categoria_id,
                    categoria_nome = cat.categoria_nome,
                    categoria_ativo = cat.categoria_ativo,
                    categoria_dataCadastro = cat.categoria_dataCadastro
                };
            return categoria;
        }

        // GET: api/Categorias/5
        /// <summary>
        /// Documentação do método GET com parâmetro
        /// </summary>
        /// <param name="id"> Identifica o registro que será recuperado </param>
        /// <returns> Exibe a categoria correspondente a identificação passada no parâmetro </returns>
        [ResponseType(typeof(CategoriaDetailDTO))]
        public async Task<IHttpActionResult> GetCategoria(int id) {
            var categoria =
                await db.Categorias.Select(cat => new CategoriaDetailDTO() {
                    categoria_id = cat.categoria_id,
                    categoria_nome = cat.categoria_nome,
                    categoria_ativo = cat.categoria_ativo,
                    categoria_dataCadastro = cat.categoria_dataCadastro
                }).SingleOrDefaultAsync(cat => cat.categoria_id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        // PUT: api/Categorias/5
        /// <summary>
        /// Documentação do método PUT
        /// </summary>
        /// <param name="id"> Identificador, especificado pelo usuário, do registro que será atualizado </param>
        /// <param name="categoria"> Objeto contendo todo o conteúdo atualizado do registro </param>
        /// <returns> Confirma se a atualização do registro foi realizada ou não. </returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategoria(int id, Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.categoria_id)
            {
                return BadRequest();
            }

            db.Entry(categoria).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        /// <summary>
        /// Documentação do método POST
        /// </summary>
        /// <param name="categoria"> Trás o objeto contendo todos os dados do registro para este ser inserido na base de dados </param>
        /// <returns> Confirma se a criação do registro foi realizada ou não. </returns
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> PostCategoria(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categorias.Add(categoria);
            await db.SaveChangesAsync();

            var dto = new CategoriaDTO() {
                categoria_id = categoria.categoria_id,
                categoria_nome = categoria.categoria_nome,
                categoria_ativo = categoria.categoria_ativo,
                categoria_dataCadastro = categoria.categoria_dataCadastro
            };

            return CreatedAtRoute("DefaultApi", new { id = categoria.categoria_id }, dto);
        }

        // DELETE: api/Categorias/5
        /// <summary>
        /// Documentação do método DELETE
        /// </summary>
        /// <param name="id"> Identifica o registro que será deletado </param>
        /// <returns> Confirma se a remoção do registro da base de dados foi realizada ou não. </returns>
        [ResponseType(typeof(Categoria))]
        public async Task<IHttpActionResult> DeleteCategoria(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            db.Categorias.Remove(categoria);
            await db.SaveChangesAsync();

            return Ok(categoria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoriaExists(int id)
        {
            return db.Categorias.Count(e => e.categoria_id == id) > 0;
        }
    }
}