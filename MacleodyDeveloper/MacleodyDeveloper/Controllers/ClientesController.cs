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

namespace ChrisDeveloperAPI.Controllers {

    public class ClientesController : ApiController {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Clientes
        /// <summary>
        /// Documentação do método GET
        /// </summary>
        /// <returns> Retorna uma lista de cliente com seus dados de contato</returns>
        public IQueryable<ClienteDTO> GetClientes() {
            var clientes = 
                from c in db.Clientes
                select new ClienteDTO() {
                    cliente_id = c.cliente_id,
                    nome = c.nome,
                    email = c.email,
                    dataCadastro = c.dataCadastro
                };
            return clientes;
        }

        // GET: api/Clientes/5
        /// <summary>
        /// Documentação do método GET com parâmetro
        /// </summary>
        /// <param name="id"> Identifica o registro que será recuperado </param>
        /// <returns> Retorna um determinado cliente com seus dados de contato, de acordo
        /// com sua identificação especificada como parâmetro</returns>
        [ResponseType(typeof(ClienteDetailDTO))]
        public async Task<IHttpActionResult> GetCliente(int id) {
            var cliente =
                await db.Clientes.Select(c =>
                new ClienteDetailDTO() {
                    cliente_id = c.cliente_id,
                    nome = c.nome,
                    email = c.email,
                    dataCadastro = c.dataCadastro
                }).SingleOrDefaultAsync(c => c.cliente_id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        /// <summary>
        /// Documentação do método PUT
        /// </summary>
        /// <param name="id"> Identificador, especificado pelo usuário, do registro que será atualizado </param>
        /// <param name="cliente"> Objeto contendo todo o conteúdo atualizado do registro </param>
        /// <returns> Confirma se a atualização do registro foi realizada ou não. </returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.cliente_id)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        /// <summary>
        /// Documentação do método POST
        /// </summary>
        /// <param name="cliente"> Trás o objeto contendo todos os dados do registro para este ser inserido na base de dados </param>
        /// <returns> Confirma se a criação do registro foi realizada ou não. </returns
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> PostCliente(Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clientes.Add(cliente);
            await db.SaveChangesAsync();

            var dto = new ClienteDTO() {
                cliente_id = cliente.cliente_id,
                nome = cliente.nome,
                email = cliente.email,
                dataCadastro = cliente.dataCadastro
            };

            return CreatedAtRoute("DefaultApi", new { id = cliente.cliente_id }, dto);
        }

        // DELETE: api/Clientes/5
        /// <summary>
        /// Documentação do método DELETE
        /// </summary>
        /// <param name="id"> Identifica o registro que será deletado </param>
        /// <returns> Confirma se a remoção do registro da base de dados foi realizada ou não. </returns>
        [ResponseType(typeof(Cliente))]
        public async Task<IHttpActionResult> DeleteCliente(int id)
        {
            Cliente cliente = await db.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            db.Clientes.Remove(cliente);
            await db.SaveChangesAsync();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Clientes.Count(e => e.cliente_id == id) > 0;
        }
    }
}