using devsu.project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Clientes.Commands.CreateCliente
{
    public record CreateClienteDTO
    {
        public int Id { get; set; }
        public string NombreYApellido { get; set; } = string.Empty;
        public Genero Genero { get; set; }
        public string GeneroToString => Genero.ToString();
        public string Identificacion { get; set; } = string.Empty;
        public int Edad { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool Estado { get; set; } = true;
    }
}
