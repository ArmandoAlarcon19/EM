using RAUL.Data;
using RAUL.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RAUL.ApiPrueba.Controllers
{

    [AllowAnonymous]
    [RoutePrefix("api/clientes")]
    public class ClientesController : ApiController
    {
        private Repository<AppContext, Cliente> ClientesRepository;
        public ClientesController()
        {
            ClientesRepository = new Repository<AppContext, Cliente>();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetClientes()
        {
            try
            {
                var clientes = await ClientesRepository.GetAll()
                                                       .ToListAsync();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetCliente(int Id)
        {
            try
            {
                var cliente = await ClientesRepository.Query(c => c.Id == Id).FirstOrDefaultAsync();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{Nombre}")]
        public async Task<IHttpActionResult> GetClienteByNombre(string Nombre)
        {
            try
            {
                var cliente = await ClientesRepository.Query(c => c.Nombre == Nombre).FirstOrDefaultAsync();

                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody]Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Error de datos");
                }
                var _cliente = ClientesRepository.Add(cliente);
                await ClientesRepository.SaveAsync();
                return Ok(_cliente);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Update([FromBody]Cliente cliente)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Error de datos");
                }
                var _cliente = ClientesRepository.Update(cliente);
                await ClientesRepository.SaveAsync();
                return Ok(_cliente);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                
                var _cliente = ClientesRepository.Query(c => c.Id == id).FirstOrDefault();
                if (_cliente != null)
                {
                    ClientesRepository.Delete(_cliente);
                    await ClientesRepository.SaveAsync();
                }
                return Ok(_cliente);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
