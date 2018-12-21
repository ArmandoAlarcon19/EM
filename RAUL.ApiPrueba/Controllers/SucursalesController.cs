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
    [RoutePrefix("api/sucursales")]
    public class SucursalesController : ApiController
    {
        private Repository<AppContext, Sucursal> SucursalRepository;
        public SucursalesController()
        {
            SucursalRepository = new Repository<AppContext, Sucursal>();
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetSucursales()
        {
            try
            {
                var sucursales = await SucursalRepository.GetAll().ToListAsync();
                return Ok(sucursales);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetSucursal(int id)
        {
            try
            {
                var sucursal = await SucursalRepository.Query(s => s.Id == id).FirstOrDefaultAsync();
                return Ok(sucursal);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
