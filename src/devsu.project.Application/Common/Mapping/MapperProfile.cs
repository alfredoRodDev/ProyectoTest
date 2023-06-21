using AutoMapper;
using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
using devsu.project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.Application.Common.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            //cliente DTO Mapper
            CreateMap<CreateClienteCommand, Cliente>();
            CreateMap<Cliente, CreateClienteDTO>();

            //cuenta DTO Mapper
            CreateMap<CreateCuentaCommand, Cuenta>();
            CreateMap<Cuenta, CreateCuentaDTO>()
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(x => x.Cliente.NombreYApellido));
        }
    }
}
