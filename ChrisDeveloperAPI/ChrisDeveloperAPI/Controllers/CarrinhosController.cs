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
using ChrisDeveloperAPI.Models;
using JetSoluctionAPI.Models;

namespace ChrisDeveloperAPI.Controllers
{
    public class CarrinhosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Carrinhos
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