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

namespace ChrisDeveloperAPI.Controllers {

    public class PedidoItensController : ApiController {

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PedidoItens
        public IQueryable<PedidoItensDTO> GetPedidoItens() {
            var pedidoItens =
                from p in db.PedidoItens
                select new PedidoItensDTO() {
                    pedidoItens_id = p.pedidoItens_id,
                    pedidoItens_pedido_id = p.pedidoItens_pedido_id,
                    pedidoItens_produto_id = p.pedidoItens_produto_id,
                    pedidoItens_valorUnidade = p.pedidoItens_valorUnidade,
                    pedidoItens_quantidade = p.pedidoItens_quantidade,
                    pedidoItens_valorTotal = p.pedidoItens_valorTotal,
                    pedidoItens_dataCadastro = p.pedidoItens_dataCadastro
                };
            return pedidoItens;
        }

        // GET: api/PedidoItens/5
        [ResponseType(typeof(PedidoItensDetailDTO))]
        public async Task<IHttpActionResult> GetPedidoItens(int id) {
            var pedidoItens =
                await db.PedidoItens.Select(p =>
                new PedidoItensDetailDTO() {
                    pedidoItens_id = p.pedidoItens_id,
                    pedidoItens_pedido_id = p.pedidoItens_pedido_id,
                    pedidoItens_produto_id = p.pedidoItens_produto_id,
                    pedidoItens_valorUnidade = p.pedidoItens_valorUnidade,
                    pedidoItens_quantidade = p.pedidoItens_quantidade,
                    pedidoItens_valorTotal = p.pedidoItens_valorTotal,
                    pedidoItens_dataCadastro = p.pedidoItens_dataCadastro
                }).SingleOrDefaultAsync(p => p.pedidoItens_id == id);
            if (pedidoItens == null)
            {
                return NotFound();
            }

            return Ok(pedidoItens);
        }

        // PUT: api/PedidoItens/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPedidoItens(int id, PedidoItens pedidoItens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedidoItens.pedidoItens_id)
            {
                return BadRequest();
            }

            db.Entry(pedidoItens).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoItensExists(id))
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

        // POST: api/PedidoItens
        [ResponseType(typeof(PedidoItens))]
        public async Task<IHttpActionResult> PostPedidoItens(PedidoItens pedidoItens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PedidoItens.Add(pedidoItens);
            await db.SaveChangesAsync();

            var dto = new PedidoItensDTO() {

                pedidoItens_id = pedidoItens.pedidoItens_id,
                pedidoItens_pedido_id = pedidoItens.pedidoItens_pedido_id,
                pedidoItens_produto_id = pedidoItens.pedidoItens_produto_id,
                pedidoItens_valorUnidade = pedidoItens.pedidoItens_valorUnidade,
                pedidoItens_quantidade = pedidoItens.pedidoItens_quantidade,
                pedidoItens_valorTotal = pedidoItens.pedidoItens_valorTotal,
                pedidoItens_dataCadastro = pedidoItens.pedidoItens_dataCadastro
            };

            return CreatedAtRoute("DefaultApi", new { id = pedidoItens.pedidoItens_id }, dto);
        }

        // DELETE: api/PedidoItens/5
        [ResponseType(typeof(PedidoItens))]
        public async Task<IHttpActionResult> DeletePedidoItens(int id)
        {
            PedidoItens pedidoItens = await db.PedidoItens.FindAsync(id);
            if (pedidoItens == null)
            {
                return NotFound();
            }

            db.PedidoItens.Remove(pedidoItens);
            await db.SaveChangesAsync();

            return Ok(pedidoItens);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidoItensExists(int id)
        {
            return db.PedidoItens.Count(e => e.pedidoItens_id == id) > 0;
        }
    }
}