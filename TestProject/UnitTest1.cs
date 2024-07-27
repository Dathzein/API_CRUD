using Bussines;
using DataAcces;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;

namespace TestProject
{
    public class UnitTest1
    {

        private readonly BDContext _context;

        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<BDContext>().UseSqlServer("Api_Crud").Options;
            _context = new BDContext(options);         
        }

        [Fact]
        public void Test1()
        {
            Cliente _cliente = new Cliente(_context);
            var result = _cliente.AddCustomer(new RequestCustomer
            {
                nombre = "JUAN GONZALES",
                codigo_identificacion = 1,
                numero_identificacion = "1024589632",
                correo_electronico = "juan.perez@pruebas.com",
                edad = 20,
                codigo_pais = 1,
                numero_telefono = "3005874748",
                active = true

            });


            Assert.Equal("", result.message);

        }
    }
}