using Models.Contracts;

namespace TestProject
{
    public class ClienteTest
    {
        private readonly ICliente _cliente;

        public ClienteTest(ICliente cliente)
        {
            _cliente = cliente;
        }

        [Fact]
        public void AddCustomer()
        {
            var result = _cliente.AddCustomer(new Models.Dtos.RequestCustomer
            {

            });

            Assert.NotNull(result);

        }
    }
}