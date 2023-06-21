using devsu.project.Domain.Common;
using devsu.project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Domain.Entities
{
    public class Cuenta : BaseEntity<int>
    {

        public Cuenta()
        {
            Movimientos = new HashSet<Movimiento>();
        }

        public string NumeroCuenta { get; set; } = string.Empty;
        public TipoDeCuenta TipoDeCuenta { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoActual { get; set; }
        public bool Estado { get; set; } = true;
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;
        public IEnumerable<Movimiento> Movimientos { get; set; }
    }
}
