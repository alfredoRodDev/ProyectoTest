using devsu.project.Domain.Common;
using devsu.project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Domain.Entities
{
    public class Movimiento : BaseEntity<int>
    {
        public TipoDeMovimiento TipoDeMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }
        public bool Estado { get; set; }
        public int CuentaId { get; set; }
        public Cuenta Cuenta { get; set; } = null!;
    }
}
