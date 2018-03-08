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
    public class PedidosController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Pedidos
        /// <summary>
        /// Documentação do método GET
        /// </summary>
        /// <returns> Retorna uma lista de pedidos de compras, 
        /// além de recuperar o registro de cada carrinho vinculado a um determinado pedido, 
        /// o valor total das compras e data da realização dessas compras </returns>
        public IQueryable<PedidoDTO> GetPedidos() {
            var pedidos =
                from p in db.Pedidos
                select new PedidoDTO() {
                    pedido_id = p.pedido_id,
                    carrinhoItens_id = p.carrinhoItens_id,
                    pedido_valor = p.pedido_valor,
                    pedido_dataCadastro = p.pedido_dataCadastro
                };
            return pedidos;
        }

        // GET: api/Pedidos/5
        /// <summary>
        /// Documentação do método GET com parâmetro
        /// </summary>
        /// <param name="id"> Identifica o registro que será recuperado </param>
        /// <returns> Exibe um pedido de compras correspondente a identificação passada no parâmetro </returns
        [ResponseType(typeof(PedidoDetailDTO))]
        public async Task<IHttpActionResult> GetPedido(int id) {
            var pedido =
                await db.Pedidos.Select(p =>
                new PedidoDetailDTO() {
                    pedido_id = p.pedido_id,
                    carrinhoItens_id = p.carrinhoItens_id,
                    pedido_valor = p.pedido_valor,
                    pedido_dataCadastro = p.pedido_dataCadastro
                }).SingleOrDefaultAsync(p => p.pedido_id == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        // PUT: api/Pedidos/5
        /// <summary>
        /// Documentação do método PUT
        /// </summary>
        /// <param name="id"> Identificador, especificado pelo usuário, do registro que será atualizado </param>
        /// <param name="pedido"> Objeto contendo todo o conteúdo atualizado do registro </param>
        /// <returns> Confirma se a atualização do registro foi realizada ou não. </returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPedido(int id, Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pedido.pedido_id)
            {
                return BadRequest();
            }

            db.Entry(pedido).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/Pedidos
        /// <summary>
        /// Documentação do método POST
        /// </summary>
        /// <param name="pedido"> Trás o objeto contendo todos os dados do registro para este ser inserido na base de dados </param>
        /// <returns> Confirma se a criação do registro foi realizada ou não. </returns>
        [ResponseType(typeof(Pedido))]
        public async Task<IHttpActionResult> PostPedido(Pedido pedido)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pedidos.Add(pedido);
            await db.SaveChangesAsync();

            var dto = new PedidoDTO() {
                pedido_id = pedido.pedido_id,
                carrinhoItens_id = pedido.carrinhoItens_id,
                pedido_valor = pedido.pedido_valor,
                pedido_dataCadastro = pedido.pedido_dataCadastro
            };

            return CreatedAtRoute("DefaultApi", new { id = pedido.pedido_id }, dto);
        }

        // DELETE: api/Pedidos/5
        /// <summary>
        /// Documentação do método DELETE
        /// </summary>
        /// <param name="id"> Identifica o registro que será deletado </param>
        /// <returns> Confirma se a remoção do registro da base de dados foi realizada ou não. </returns>
        [ResponseType(typeof(Pedido))]
        public async Task<IHttpActionResult> DeletePedido(int id)
        {
            Pedido pedido = await db.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            db.Pedidos.Remove(pedido);
            await db.SaveChangesAsync();

            return Ok(pedido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidoExists(int id)
        {
            return db.Pedidos.Count(e => e.pedido_id == id) > 0;
        }
    }
}