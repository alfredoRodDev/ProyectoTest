using devsu.project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Cuentas.Commands.CreateCuenta
{
    public record CreateCuentaDTO
    {
        public int Id { get; set; }
        public string NumeroCuenta { get; set; } = string.Empty;
        public string TipoDeCuenta { get; set; } = string.Empty;
        public decimal SaldoInicial { get; set; }
        public decimal SaldoActual { get; set; }
        public bool Estado { get; set; } = true;
        public string Cliente { get; set; } = string.Empty;
    }
}
