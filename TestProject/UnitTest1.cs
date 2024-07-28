using Bussines;
using DataAcces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Models.Data;
using Models.Dtos;

namespace TestProject
{
    public class UnitTest1
    {

        private readonly BDContext _context;
        private RequestCustomer _testCliente;
        public UnitTest1()
        {
            var options = new DbContextOptionsBuilder<BDContext>().UseInMemoryDatabase("Api_Crud").Options;
            _context = new BDContext(options);
        }

        [Fact]
        public void Test1_NullableObjects()
        {
            Log log = new Log(_context);
            Cliente _cliente = new Cliente(_context, log);
            
            var result = _cliente.AddCustomer(_testCliente, 0);
            _testCliente = null;
            //Debe generar un error ya que debe recibir campos
            Assert.Equal(-1,result.idError);

        }

        [Fact]
        public void Test2_SaveData()
        {
            Log log = new Log(_context);
            Cliente _cliente = new Cliente(_context, log);
            _testCliente = new RequestCustomer
            {
                nombre = "JUAN PEREZ",
                codigo_identificacion = 1,
                numero_identificacion = "1073707567",
                correo_electronico = "juan.perez@pruebas.com",
                edad = 20,
                codigo_pais = 1,
                numero_telefono = "3005874748",
                active = true

            };
            var result = _cliente.AddCustomer(_testCliente, 0);

            //deberia registrar
            Assert.Equal(0, result.idError);

        }
        [Fact]
        public void Test3_AlreadyExist()
        {
            Log log = new Log(_context);
            Cliente _cliente = new Cliente(_context, log);
            //Si se cambia un valor del numero de identificacion este genera error porque ya va enviar un 0 como si no se hubiera registrado antes
            _testCliente = new RequestCustomer
            {
                nombre = "JUAN PEREZ",
                codigo_identificacion = 1,
                numero_identificacion = "1073707567",
                correo_electronico = "juan.perez@pruebas.com",
                edad = 20,
                codigo_pais = 1,
                numero_telefono = "3005874748",
                active = true

            };
            _cliente.AddCustomer(_testCliente, 0);
            _testCliente = new RequestCustomer
            {
                nombre = "JUAN PEREZ",
                codigo_identificacion = 1,
                numero_identificacion = "1073707567",
                correo_electronico = "juan.perez@pruebas.com",
                edad = 20,
                codigo_pais = 1,
                numero_telefono = "3005874748",
                active = true

            };
            var result = _cliente.AddCustomer(_testCliente,0);

            //deberia generar error al registrar porque ya existe
            Assert.Equal(2, result.idError);

        }

        [Fact]
        public void Test4_UpdateData()
        {
            Log log = new Log(_context);
            Cliente _cliente = new Cliente(_context, log);

            _testCliente = new RequestCustomer
            {
                nombre = "JUAN PEREZz",
                codigo_identificacion = 1,
                numero_identificacion = "10737075675",
                correo_electronico = "juan.perez@pruebas.com",
                edad = 20,
                codigo_pais = 1,
                numero_telefono = "3005874748",
                active = true

            };
            var result = _cliente.UpdateCustomer(_testCliente, 0);

            //deberia actualizar
            Assert.Equal(0, result.idError);

        }

        [Fact]
        public void Test5_GetCustomer()
        {
            Log log = new Log(_context);
            Cliente _cliente = new Cliente(_context, log);
            _testCliente = new RequestCustomer
            {
                nombre = "JUAN PEREZ",
                codigo_identificacion = 1,
                numero_identificacion = "1073707567",
                correo_electronico = "juan.perez@pruebas.com",
                edad = 20,
                codigo_pais = 1,
                numero_telefono = "3005874748",
                active = true

            };
            _cliente.AddCustomer(_testCliente, 0);
            _testCliente = new RequestCustomer
            {
                nombre = "JUAN PEREZ",
                codigo_identificacion = 1,
                numero_identificacion = "1073707567",
                correo_electronico = "juan.perez@pruebas.com",
                edad = 20,
                codigo_pais = 1,
                numero_telefono = "3005874748",
                active = true

            };
            var result = _cliente.GetCustomersById(_testCliente.numero_identificacion,0);

            //deberia Encontrar
            Assert.NotNull(result);

        }

        [Fact]
        public void Test6_GetCustomers()
        {
            Log log = new Log(_context);
            Cliente _cliente = new Cliente(_context, log);

            var result = _cliente.GetCustomers(0);

            //no deberia Encontrar
            Assert.Null(result);

        }

        [Fact]
        public void Test7_DeleteCustomer()
        {
            Log log = new Log(_context);
            Cliente _cliente = new Cliente(_context, log);
            Clientes clienteTest = new Clientes
            {
                Id = 1,
                nombre = "JUAN PEREZ",
                codigo_identificacion = 1,
                numero_identificacion = "10737075672",
                correo_electronico = "juan.perez@pruebas.com",
                edad = 20,
                codigo_pais = 1,
                numero_telefono = "3005874748",
                active = true

            };
            var result = _cliente.DeleteCustomer(clienteTest.Id, 0);

            //deberia Encontrar
            Assert.Equal(0, result.idError);

        }
    }
}