using devsu.project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Commands.CreateMovDeposito
{
    public class CreateMovimientoDTO
    {
        public int Id { get; set; }
        public string FechaMovimiento { get; set; } = string.Empty;
        public string DescripcionMovimiento { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public string NumeroCuenta { get; set; } = string.Empty;
    }
}
