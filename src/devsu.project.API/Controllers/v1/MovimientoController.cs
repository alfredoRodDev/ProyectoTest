using devsu.project.Application.Features.Movimientos.Commands.CreateMovDeposito;
using devsu.project.Application.Features.Movimientos.Commands.CreateMovRetiro;
using devsu.project.Application.Features.Movimientos.Commands.DeleteMovimiento;
using devsu.project.Application.Features.Movimientos.Commands.UpdateMovimiento;
using devsu.project.Application.Features.Movimientos.Queries.GetAllMovimientos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace devsu.project.API.Controllers.v1
{
    [Route("api/v1/movimientos")]
    [ApiController]
    public class MovimientoController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllMovimientosQuery query)
        {
            query.BaseUrl = "http://" + HttpContext.Request.Host.ToString() + "/api/v1/movimientos";
            return Ok(await Mediator.Send(query));
        }


        [HttpPost("deposito")]
        public async Task<IActionResult> CreateDeposito(CreateMovDepositoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }  
        
        [HttpPost("retiro")]
        public async Task<IActionResult> CreateRetiro(CreateMovRetiroCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateMovimientoCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteMovimientoCommand(id)));
        }
    }
}
