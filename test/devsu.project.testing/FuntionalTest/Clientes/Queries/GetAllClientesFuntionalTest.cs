using devsu.project.Application.Features.Clientes.Commands.CreateCliente;
using devsu.project.Application.Features.Clientes.Commands.DeleteCliente;
using devsu.project.Application.Features.Clientes.Commands.UpdateCliente;
using devsu.project.Application.WrappersModels;
using devsu.project.Domain.Entities;
using devsu.project.testing.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.FuntionalTest.Clientes.Queries
{

    public class GetAllClientesFuntionalTest : FuntionalTestingBase
    {

        private readonly string _url = "http://localhost:5041/api/v1/clientes";

        [Test]
        public async Task GetAll()
        {
            //crear cliente
            var cliente = await HandlerCreate_Post();

            var _client = GetClient();

            if (cliente.IsSuccess)
            {
                //get
                var nuevoUrl = _url + $"?baseUrl={""}&pageNumber={1}&customerId={0}&estado={0}&genero={0}&minEdad={0}&maxEdad={0}";
                var responseQuery = await _client.GetAsync(nuevoUrl);

                var responseQueryRead = await responseQuery.Content.ReadAsStringAsync();

                var objQueryResponse = JsonConvert.DeserializeObject<PaginationResponse<CreateClienteDTO>>(responseQueryRead);

                if (objQueryResponse.Data.Any())
                {
                    Assert.IsTrue(true);
                }
                else
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }

        }

        [Test]
        public async Task Create_Post()
        {
            var response = await HandlerCreate_Post();

            if (response.IsSuccess)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task Update_Put()
        {
            var responseAdd = await HandlerCreate_Post();

            var _client = GetClient();

            if (responseAdd.IsSuccess)
            {
                var command = new UpdateClienteCommand
                {
                    Id = responseAdd.Data.Id,
                    NombreYApellido = "TESTING NAME-test",
                    Identificacion = responseAdd.Data.Identificacion,
                    Edad = 20,
                    Direccion = "TEST ADDRESS",
                    Telefono = "123456",
                    Password = "1234",
                    Estado = true,
                    Genero = Domain.Enums.Genero.Femenino,
                };

                var response = await _client.PutAsJsonAsync(_url, command);

                if (response.IsSuccessStatusCode)
                {
                    var responseRead = await response.Content.ReadAsStringAsync();
                    var objResponse = JsonConvert.DeserializeObject<Response<CreateClienteDTO>>(responseRead);

                    if (objResponse.IsSuccess)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.Fail();
                    }

                }
                else
                {
                    Assert.Fail();
                }

            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task Delete()
        {
            var responseAdd = await HandlerCreate_Post();

            var _client = GetClient();

            if (responseAdd.IsSuccess)
            {
                var url = $"{_url}/{responseAdd.Data.Id}";

                var response = await _client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var responseRead = await response.Content.ReadAsStringAsync();
                    var objResponse = JsonConvert.DeserializeObject<Response>(responseRead);

                    if (objResponse.IsSuccess)
                    {
                        Assert.IsTrue(true);
                    }
                    else
                    {
                        Assert.Fail();
                    }

                }
                else
                {
                    Assert.Fail();
                }

            }
            else
            {
                Assert.Fail();
            }
        }

        private async Task<Response<CreateClienteDTO>> HandlerCreate_Post()
        {
            var _client = GetClient();

            var command = new CreateClienteCommand
            {
                NombreYApellido = "TESTING NAME",
                Identificacion = Guid.NewGuid().ToString().Substring(0, 15).ToUpper(),
                Edad = 20,
                Direccion = "TEST ADDRESS",
                Telefono = "123456",
                Password = "1234",
                Estado = true,
                Genero = Domain.Enums.Genero.Femenino,
            };

            //post
            var responseAdd = await _client.PostAsJsonAsync(_url, command);

            if (responseAdd.IsSuccessStatusCode)
            {

                var responseAddRead = await responseAdd.Content.ReadAsStringAsync();
                var objAddResponse = JsonConvert.DeserializeObject<Response<CreateClienteDTO>>(responseAddRead);

                return objAddResponse;

            }
            else
            {
                return Response<CreateClienteDTO>.Failure("", null);
            }

        }
    }
}
