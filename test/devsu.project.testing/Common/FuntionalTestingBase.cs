using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace devsu.project.testing.Common
{
    [TestFixture]
    public class FuntionalTestingBase
    {
        private static WebApplicationFactory<Program> _factory = null!;
        private static IServiceScopeFactory _scopeFactory = null!;

        [OneTimeSetUp]
        public void RunBefore()
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();

        }


        public HttpClient GetClient()
        {
            using var scope = _scopeFactory.CreateScope();
            return _factory.CreateClient();
        }
    }
}
