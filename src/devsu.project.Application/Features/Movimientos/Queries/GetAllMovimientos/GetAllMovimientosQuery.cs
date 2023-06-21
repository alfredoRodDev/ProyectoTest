using devsu.project.Application.Features.Movimientos.Commands.CreateMovDeposito;
using devsu.project.Application.WrappersModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Queries.GetAllMovimientos
{
    public class GetAllMovimientosQuery : IRequest<PaginationResponse<CreateMovimientoDTO>>
    {
        public string BaseUrl { get; set; } = string.Empty; //desde el frontEnd enviar string vacio
        public int PageNumber { get; set; } = 1;
        public int CustomerId { get; set; } = 0; //si es cero no se filtra por id
        public int CuentaId { get; set; } = 0; //si es cero no se filtra por id
        public int TipoDeMovimiento { get; set; } = 0; // si es cero, no se filtra por tipo de cuenta, 1 == retiro, 2 == deposito
        public int Estado { get; set; } = 0; // si es cero, no se filtra por estado, si es 1 = true, si es 2 = false
        public DateTime? FechaDesde { get; set; } //si es null, no se filtra por fecha
        public DateTime? FechaHasta { get; set; } //si es null, no se filtra por fecha
    }
}
