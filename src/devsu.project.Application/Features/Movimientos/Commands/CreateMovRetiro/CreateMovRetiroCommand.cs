using devsu.project.Application.Features.Movimientos.Commands.CreateMovDeposito;
using devsu.project.Application.WrappersModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Commands.CreateMovRetiro
{
    public record CreateMovRetiroCommand : IRequest<Response<CreateMovimientoDTO>>
    {
        public decimal Valor { get; set; }
        public int CuentaId { get; set; }
    }
}
