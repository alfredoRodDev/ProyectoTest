using devsu.project.Application.Common.Exceptions;
using devsu.project.Application.Features.Clientes.Commands.UpdateCliente;
using devsu.project.testing.Common;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.IntegrationTest.Clientes.Commands
{
    public class UpdateClienteIntegrationTest : Testing
    {

        [Test]
        public async Task Update_ShouldBeErrorRepeated()
        {
            var primerId = Guid.NewGuid().ToString().Substring(0, 15).ToUpper();
            await CreateClienteIntegrationTest.HandleCreate(primerId);

            //crear otro cliente con diferente identificacion
            var segundoId = Guid.NewGuid().ToString().Substring(0, 15).ToUpper();
            var segundoCliente = await CreateClienteIntegrationTest.HandleCreate(segundoId);

            //con segundo cliente cambiar identificacion por la del primer cliente
            var command = new UpdateClienteCommand
            {
                Id = segundoCliente.Data.Id,
                NombreYApellido = "TESTING NAME",
                Identificacion = primerId,
                Edad = 20,
                Direccion = "TEST ADDRESS",
                Telefono = "123456",
                Password = "1234",
                Estado = true,
                Genero = Domain.Enums.Genero.Femenino,
            };

            //Act
            var updateResponse = await SendAsync(command);

            updateResponse.Should().NotBeNull();
            updateResponse.IsSuccess.Should().BeFalse();

        }


        [Test]
        public async Task Update_ShouldThrow404BeNotFound()
        {

            var command = new UpdateClienteCommand
            {
                Id = 25000,
                NombreYApellido = "TESTING NAME",
                Identificacion = "TEST",
                Edad = 20,
                Direccion = "TEST ADDRESS AA",
                Telefono = "123456",
                Password = "1234",
                Estado = true,
                Genero = Domain.Enums.Genero.Femenino,
            };

            //Act
            try
            {
                var response = await SendAsync(command);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                var typeOfexeption = ex.GetType();

                Assert.AreEqual(typeof(NotFoundException), typeOfexeption);
            }



        }


        [Test]
        public async Task Update_ShouldCreateSuccessful()
        {
            var id = Guid.NewGuid().ToString().Substring(0, 15).ToUpper();
            var cliente = await CreateClienteIntegrationTest.HandleCreate(id);

            var command = new UpdateClienteCommand
            {
                Id = cliente.Data.Id,
                NombreYApellido = "TESTING NAME-test",
                Identificacion = id,
                Edad = 20,
                Direccion = "TEST ADDRESS",
                Telefono = "123456",
                Password = "1234",
                Estado = true,
                Genero = Domain.Enums.Genero.Femenino,
            };

        }
    }
}
