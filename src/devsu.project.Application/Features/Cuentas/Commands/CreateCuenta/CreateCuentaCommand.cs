using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Commands.CreateCuenta
{
    public record CreateCuentaCommand : IRequest<Response<CreateCuentaDTO>>
    {
        public string NumeroCuenta { get; set; } = string.Empty;
        public TipoDeCuenta TipoDeCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; } = true;
        public int ClienteId { get; set; }
    }
}
