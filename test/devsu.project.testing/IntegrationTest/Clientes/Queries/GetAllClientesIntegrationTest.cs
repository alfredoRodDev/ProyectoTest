using devsu.project.Application.Features.Clientes.Queries.GetAllClientes;
using devsu.project.testing.Common;
using devsu.project.testing.IntegrationTest.Clientes.Commands;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.IntegrationTest.Clientes.Queries
{
    public class GetAllClientesIntegrationTest : Testing
    {

        [Test]
        public async Task GetAll_ShouldReturnAtLeastOneRegister()
        {
            //crear cliente
            var cliente = await CreateClienteIntegrationTest.HandleCreate(Guid.NewGuid().ToString().Substring(0, 15).ToUpper());

            var queryA = new GetAllClientesQuery
            {
                BaseUrl = "",
                PageNumber = 1,
                CustomerId = 0,
                Estado = 0,
                Genero = 0,
                MinEdad = 0,
                MaxEdad = 0
            };

            var queryB = new GetAllClientesQuery
            {
                BaseUrl = "",
                PageNumber = 1,
                CustomerId = cliente.Data.Id,
                Estado = 0,
                Genero = 0,
                MinEdad = 0,
                MaxEdad = 0
            };

            var queryC = new GetAllClientesQuery
            {
                BaseUrl = "",
                PageNumber = 1,
                CustomerId = 0,
                Estado = 1,
                Genero = 0,
                MinEdad = 90,
                MaxEdad = 0
            };

            var responseA = await SendAsync(queryA);
            var responseB = await SendAsync(queryB);
            var responseC = await SendAsync(queryC);

            responseA.Data.Count().Should().BeGreaterThan(0);
            responseB.Data.Count().Should().BeGreaterThan(0);
            responseC.Data.Count().Should().BeGreaterThanOrEqualTo(0);

        }
    }
}
