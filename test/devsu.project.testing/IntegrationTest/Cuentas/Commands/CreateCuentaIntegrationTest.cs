using devsu.project.Application.Features.Cuentas.Commands.CreateCuenta;
using devsu.project.Application.WrappersModels;
using devsu.project.testing.Common;
using devsu.project.testing.IntegrationTest.Clientes.Commands;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.IntegrationTest.Cuentas.Commands
{
    public class CreateCuentaIntegrationTest : Testing
    {


        [Test]
        public async Task Create_ShouldBeErrorRepeated()
        {
            var numeroCuenta = Guid.NewGuid().ToString().Substring(0, 15).ToUpper();
            await HandleCreate(numeroCuenta);

            //Act
            var response = await HandleCreate(numeroCuenta);

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


        public async static Task<Response<CreateCuentaDTO>> HandleCreate(string numeroCuenta)
        {

            //crear cliente
            var clienteResponse = await CreateClienteIntegrationTest.HandleCreate(Guid.NewGuid().ToString().Substring(0, 15).ToUpper());


            var command = new CreateCuentaCommand
            {
                NumeroCuenta = numeroCuenta,
                ClienteId = clienteResponse.Data.Id,
                SaldoInicial = 100,
                TipoDeCuenta = Domain.Enums.TipoDeCuenta.Ahorro,
            };

            return await SendAsync(command);
        }
    }
}
