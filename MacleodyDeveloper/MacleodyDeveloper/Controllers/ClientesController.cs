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