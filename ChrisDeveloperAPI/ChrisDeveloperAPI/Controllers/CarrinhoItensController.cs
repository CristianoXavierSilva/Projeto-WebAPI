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
    public class CarrinhoItensController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CarrinhoItens
        public IQueryable<CarrinhoItensDTO> GetCarrinhoItens() {
            var carrinhoItens =
                from ci in db.CarrinhoItens
                select new CarrinhoItensDTO() {
                    carrinhoItens_id = ci.carrinhoItens_id,
                    carrinhoItens_carrinho_id = ci.carrinhoItens_carrinho_id,
                    carrinhoItens_produto_id = ci.carrinhoItens_produto_id,
                    carrinhoItens_valorUnitario = ci.carrinhoItens_valorUnitario,
                    carrinhoItens_quantidade = ci.carrinhoItens_quantidade,
                    carrinhoItens_totalItem = ci.carrinhoItens_totalItem,
                    carrinhoItens_dataCadastro = ci.carrinhoItens_dataCadastro
                };
            return carrinhoItens;
        }

        // GET: api/CarrinhoItens/5
        [ResponseType(typeof(CarrinhoItensDetailDTO))]
        public async Task<IHttpActionResult> GetCarrinhoItens(int id) {
            var carrinhoItens =
                await db.CarrinhoItens.Select(ci =>
                new CarrinhoItensDetailDTO() {
                    carrinhoItens_id = ci.carrinhoItens_id,
                    carrinhoItens_carrinho_id = ci.carrinhoItens_carrinho_id,
                    carrinhoItens_produto_id = ci.carrinhoItens_produto_id,
                    carrinhoItens_valorUnitario = ci.carrinhoItens_valorUnitario,
                    carrinhoItens_quantidade = ci.carrinhoItens_quantidade,
                    carrinhoItens_totalItem = ci.carrinhoItens_totalItem,
                    carrinhoItens_dataCadastro = ci.carrinhoItens_dataCadastro
                }).SingleOrDefaultAsync(ci => ci.carrinhoItens_id == id);

            if (carrinhoItens == null)
            {
                return NotFound();
            }

            return Ok(carrinhoItens);
        }

        // PUT: api/CarrinhoItens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCarrinhoItens(int id, CarrinhoItens carrinhoItens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != carrinhoItens.carrinhoItens_id)
            {
                return BadRequest();
            }

            db.Entry(carrinhoItens).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarrinhoItensExists(id))
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

        // POST: api/CarrinhoItens
        [ResponseType(typeof(CarrinhoItens))]
        public async Task<IHttpActionResult> PostCarrinhoItens(CarrinhoItens carrinhoItens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CarrinhoItens.Add(carrinhoItens);
            await db.SaveChangesAsync();

            var dto = new CarrinhoItensDTO() {
                carrinhoItens_id = carrinhoItens.carrinhoItens_id,
                carrinhoItens_carrinho_id = carrinhoItens.carrinhoItens_carrinho_id,
                carrinhoItens_produto_id = carrinhoItens.carrinhoItens_produto_id,
                carrinhoItens_valorUnitario = carrinhoItens.carrinhoItens_valorUnitario,
                carrinhoItens_quantidade = carrinhoItens.carrinhoItens_quantidade,
                carrinhoItens_totalItem = carrinhoItens.carrinhoItens_totalItem,
                carrinhoItens_dataCadastro = carrinhoItens.carrinhoItens_dataCadastro
            };

            return CreatedAtRoute("DefaultApi", new { id = carrinhoItens.carrinhoItens_id }, dto);
        }

        // DELETE: api/CarrinhoItens/5
        [ResponseType(typeof(CarrinhoItens))]
        public async Task<IHttpActionResult> DeleteCarrinhoItens(int id)
        {
            CarrinhoItens carrinhoItens = await db.CarrinhoItens.FindAsync(id);
            if (carrinhoItens == null)
            {
                return NotFound();
            }

            db.CarrinhoItens.Remove(carrinhoItens);
            await db.SaveChangesAsync();

            return Ok(carrinhoItens);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CarrinhoItensExists(int id)
        {
            return db.CarrinhoItens.Count(e => e.carrinhoItens_id == id) > 0;
        }
    }
}