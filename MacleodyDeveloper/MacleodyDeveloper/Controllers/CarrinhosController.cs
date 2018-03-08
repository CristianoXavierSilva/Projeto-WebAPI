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
    public class CarrinhosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Carrinhos
        /// <summary>
        /// Documentação do método GET
        /// </summary>
        /// <returns> Retorna uma lista de carrinhos de compras, 
        /// além de recuperar o registro de cada cliente vinculado a um determinado carrinho, 
        /// o valor total das compras e data da realização dessas compras </returns>
        public IQueryable<CarrinhoDTO> GetCarrinhos() {

            var carrinhos =
                from c in db.Carrinhos
                select new CarrinhoDTO() {
                    carrinho_id = c.carrinho_id,
                    cliente_id = c.cliente_id,
                    carrinho_total = c.carrinho_total,
                    carrinho_dataCadastro = c.carrinho_dataCadastro
                };
            return carrinhos;
        }

        // GET: api/Carrinhos/5
        /// <summary>
        /// Documentação do método GET com parâmetro
        /// </summary>
        /// <param name="id"> Identifica o registro que será recuperado </param>
        /// <returns> Exibe um carrinho de compras correspondente a identificação passada no parâmetro </returns>
        [ResponseType(typeof(CarrinhoDetailDTO))]
        public async Task<IHttpActionResult> GetCarrinho(int id) {
            var carrinho =
                await db.Carrinhos.Select(c =>
                new CarrinhoDetailDTO() {
                    carrinho_id = c.carrinho_id,
                    cliente_id = c.cliente_id,
                    carrinho_total = c.carrinho_total,
                    carrinho_dataCadastro = c.carrinho_dataCadastro
                }).SingleOrDefaultAsync(c => c.carrinho_id == id);

            if (carrinho == null)
            {
                return NotFound();
            }

            return Ok(carrinho);
        }

        // PUT: api/Carrinhos/5
        /// <summary>
        /// Documentação do método PUT
        /// </summary>
        /// <param name="id"> Identificador, especificado pelo usuário, do registro que será atualizado </param>
        /// <param name="carrinho"> Objeto contendo todo o conteúdo atualizado do registro </param>
        /// <returns> Confirma se a atualização do registro foi realizada ou não. </returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCarrinho(int id, Carrinho carrinho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carrinho.carrinho_id)
            {
                return BadRequest();
            }

            db.Entry(carrinho).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrinhoExists(id))
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

        // POST: api/Carrinhos
        /// <summary>
        /// Documentação do método POST
        /// </summary>
        /// <param name="carrinho"> Trás o objeto contendo todos os dados do registro para este ser inserido na base de dados </param>
        /// <returns> Confirma se a criação do registro foi realizada ou não. </returns>
        [ResponseType(typeof(Carrinho))]
        public async Task<IHttpActionResult> PostCarrinho(Carrinho carrinho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Carrinhos.Add(carrinho);
            await db.SaveChangesAsync();

            var dto = new CarrinhoDTO() {
                carrinho_id = carrinho.carrinho_id,
                cliente_id = carrinho.cliente_id,
                carrinho_total = carrinho.carrinho_total,
                carrinho_dataCadastro = carrinho.carrinho_dataCadastro
            };

            return CreatedAtRoute("DefaultApi", new { id = carrinho.carrinho_id }, dto);
        }

        // DELETE: api/Carrinhos/5
        /// <summary>
        /// Documentação do método DELETE
        /// </summary>
        /// <param name="id"> Identifica o registro que será deletado </param>
        /// <returns> Confirma se a remoção do registro da base de dados foi realizada ou não. </returns>
        [ResponseType(typeof(Carrinho))]
        public async Task<IHttpActionResult> DeleteCarrinho(int id)
        {
            Carrinho carrinho = await db.Carrinhos.FindAsync(id);
            if (carrinho == null)
            {
                return NotFound();
            }

            db.Carrinhos.Remove(carrinho);
            await db.SaveChangesAsync();

            return Ok(carrinho);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarrinhoExists(int id)
        {
            return db.Carrinhos.Count(e => e.carrinho_id == id) > 0;
        }
    }
}