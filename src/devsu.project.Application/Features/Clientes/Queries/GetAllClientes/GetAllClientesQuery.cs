using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.WrappersModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Queries.GetAllClientes
{
    public record GetAllClientesQuery : IRequest<PaginationResponse<CreateClienteDTO>>
    {
        public string BaseUrl { get; set; } = string.Empty; //desde el frontEnd enviar string vacio
        public int PageNumber { get; set; } = 1;
        public int CustomerId { get; set; } = 0; //si es cero no se filtra por id
        public int Estado { get; set; } = 0; // si es cero, no se filtra por estado, si es 1 = true, si es 2 = false
        public int Genero { get; set; } = 0; // si es cero, no se filtra por genero
        public int MinEdad { get; set; } = 0;
        public int MaxEdad { get; set; } = 0;


    }
}
