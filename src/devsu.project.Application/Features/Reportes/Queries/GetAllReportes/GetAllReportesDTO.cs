using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Reportes.Queries.GetAllReportes
{
    public record GetAllReportesDTO
    {
        public int Id { get; set; }
        public string Cliente { get; set; }
        public string NumeroCuenta { get; set; }
        public string FechaMovimiento { get; set; }
        public string TipoDeMovimiento { get; set; }
        public decimal Valor { get; set; }
        public string DescripcionMovimiento { get; set; }
        public decimal SaldoInicial { get; set; }
        public decimal SaldoFinal { get; set; }
        public bool Estado { get; set; }
    }
}
