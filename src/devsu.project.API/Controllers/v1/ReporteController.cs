using devsu.project.Application.Features.Movimientos.Queries.GetAllMovimientos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace devsu.project.API.Controllers.v1
{
    [Route("api/v1/reportes")]
    [ApiController]
    public class ReporteController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMovimientosQuery query)
        {
            query.BaseUrl = "http://" + HttpContext.Request.Host.ToString() + "/api/v1/reportes";
            return Ok(await Mediator.Send(query));
        }
    }
}
