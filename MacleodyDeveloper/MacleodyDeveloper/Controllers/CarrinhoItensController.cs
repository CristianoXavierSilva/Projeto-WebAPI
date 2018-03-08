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
    public class CarrinhoItensController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CarrinhoItens
        /// <summary>
        /// Documentação do método GET
        /// </summary>
        /// <returns> Retorna uma lista de itens adicionados num carrinho, 
        /// além da quantidade de cada item, seu preço unitário, somatória
        /// de todos os valores e data das compras</returns>
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
        /// <summary>
        /// Documentação do método GET com parâmetro
        /// </summary>
        /// <param name="id"> Identifica o registro que será recuperado </param>
        /// <returns> Exibe um conjunto de itens adicionados num carrinho o registro especificado </returns>
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
        /// <summary>
        /// Documentação do método PUT
        /// </summary>
        /// <param name="id"> Identificador, especificado pelo usuário, do registro que será atualizado </param>
        /// <param name="carrinhoItens"> Objeto contendo todo o conteúdo atualizado do registro </param>
        /// <returns> Confirma se a atualização do registro foi realizada ou não. </returns>
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
        /// <summary>
        /// Documentação do método POST
        /// </summary>
        /// <param name="carrinhoItens"> Trás o objeto contendo todos os dados do registro para este ser inserido na base de dados </param>
        /// <returns> Confirma se a criação do registro foi realizada ou não. </returns>
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
        /// <summary>
        /// Documentação do método DELETE
        /// </summary>
        /// <param name="id"> Identifica o registro que será deletado </param>
        /// <returns> Confirma se a remoção do registro da base de dados foi realizada ou não. </returns>
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