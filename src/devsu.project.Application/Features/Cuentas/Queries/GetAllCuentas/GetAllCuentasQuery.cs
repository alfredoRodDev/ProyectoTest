using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Queries.GetAllCuentas
{
    public record GetAllCuentasQuery : IRequest<PaginationResponse<CreateCuentaDTO>>
    {
        public string BaseUrl { get; set; } = string.Empty; //desde el frontEnd enviar string vacio
        public int PageNumber { get; set; } = 1;
        public int CustomerId { get; set; } = 0; //si es cero no se filtra por id
        public int TipoDeCuenta { get; set; } = 0; // si es cero, no se filtra por tipo de cuenta, 1 == ahorro, 2 == corriente
        public int Estado { get; set; } = 0; // si es cero, no se filtra por estado, si es 1 = true, si es 2 = false
    }
}
