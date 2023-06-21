using devsu.project.Application.Features.Movimientos.Commands.CreateMovDeposito;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Features.Movimientos.Commands.UpdateMovimiento
{
    public record UpdateMovimientoCommand : IRequest<Response<CreateMovimientoDTO>>
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
    }
}
