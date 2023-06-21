using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.Features.Clientes.Commands.DeleteCliente;
using devsu.project.Application.Features.Clientes.Commands.UpdateCliente;
using devsu.project.Application.Features.Clientes.Queries.GetAllClientes;
using devsu.project.Application.Features.Clientes.Queries.GetClienteById;
using devsu.project.Application.Features.Clientes.Queries.GetClienteBySearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace devsu.project.API.Controllers.v1
{
    [Route("api/v1/clientes")]
    [ApiController]
    public class ClienteController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllClientesQuery query)
        {
            query.BaseUrl = "http://" + HttpContext.Request.Host.ToString() + "/api/v1/clientes";
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("getById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetClienteByIdQuery(id)));
        }

        [HttpGet("getBySearch")]
        public async Task<IActionResult> GetBySearch([FromQuery] GetClienteBySearchQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateClienteCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteClienteCommand(id)));
        }
    }
}
