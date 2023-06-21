using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Queries.GetClienteBySearch
{
    public record GetClienteBySearchQuery(string searchParam) : IRequest<IEnumerable<CreateClienteDTO>>
    {
    }
}
