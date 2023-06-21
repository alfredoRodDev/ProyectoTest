using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
using devsu.project.Application.Features.Cuentas.Commands.DeleteCuenta;
using devsu.project.Application.Features.Cuentas.Commands.UpdateCuenta;
using devsu.project.Application.Features.Cuentas.Queries.GetAllCuentas;
using devsu.project.Application.Features.Cuentas.Queries.GetCuentaByNumber;
using devsu.project.Application.Features.Cuentas.Queries.GetSaldoActual;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace devsu.project.API.Controllers.v1
{
    [Route("api/v1/cuentas")]
    [ApiController]
    public class CuentaController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCuentasQuery query)
        {
            query.BaseUrl = "http://" + HttpContext.Request.Host.ToString() + "/api/v1/cuentas";
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("getCuentaByNumber")]
        public async Task<IActionResult> GetCuentaByNumber([FromQuery] GetCuentaByNumberQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("getSaldoActual/{cuentaId}")]
        public async Task<IActionResult> getSaldoActual(int cuentaId)
        {
            return Ok(await Mediator.Send(new GetSaldoActualQuery(cuentaId)));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCuentaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCuentaCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCuentaCommand(id)));
        }
    }
}
