using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Commands.UpdateCliente
{
    public record UpdateClienteCommand : IRequest<Response<CreateClienteDTO>>
    {
        public int Id { get; set; }
        public string NombreYApellido { get; set; } = string.Empty;
        public Genero Genero { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }
}
