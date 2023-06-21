using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.WrappersModels;
using devsu.project.testing.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.IntegrationTest.Clientes.Commands
{
    public class CreateClienteIntegrationTest : Testing
    {


        [Test]
        public async Task Create_ShouldBeErrorRepeated()
        {
            var identificacion = Guid.NewGuid().ToString().Substring(0, 15).ToUpper();
            await HandleCreate(identificacion);

            //Act
            var response = await HandleCreate(identificacion);

            //Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeFalse();


        }


        [Test]
        public async Task Create_ShouldCreateSuccessful()
        {

            //Act
            var response = await HandleCreate(Guid.NewGuid().ToString().Substring(0, 15).ToUpper());

            //Assert
            response.Should().NotBeNull();
            response.IsSuccess.Should().BeTrue();
            response.Data.Id.Should().BeGreaterThanOrEqualTo(1);
        }


        public async static Task<Response<CreateClienteDTO>> HandleCreate(string identificacion)
        {
            var command = new CreateClienteCommand
            {
                NombreYApellido = "TESTING NAME",
                Identificacion = identificacion,
                Edad = 20,
                Direccion = "TEST ADDRESS",
                Telefono = "123456",
                Password = "1234",
                Estado = true,
                Genero = Domain.Enums.Genero.Femenino,
            };

            return await SendAsync(command);
        }

    }
}
