using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Queries.GetClienteById
{
    public record GetClienteByIdQuery(int id) : IRequest<CreateClienteDTO>
    {
    }
}
