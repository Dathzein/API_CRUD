using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Contracts;
using Models.Data;
using Models.Dtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //readonly form bussines
        private readonly ICliente _cliente;
        public CustomerController( ICliente cliente)
        {
            _cliente = cliente;
        }
        [HttpGet("customer-id")]
        public Clientes GetCustomerById( int id)
        {
            Clientes cliente = _cliente.GetCustomersById(id);
            return cliente;
        }
        [HttpGet("customers")]
        public List<Clientes> GetAllCustomer()
        {
            List<Clientes> clientes = _cliente.GetCustomers();
            return clientes;
        }

        [HttpPost("add-customer")]
        public ResponseCustomer AddCustomer( RequestCustomer customer )
        {
            ResponseCustomer res = _cliente.AddCustomer( customer );
            return res;
        }
        [HttpPut("update-customer")]
        public ResponseCustomer UpdateCustomer( RequestCustomer customer )
        {
            ResponseCustomer res = _cliente.UpdateCustomer(customer);
            return res;
        }
        [HttpDelete("delete-customer")]
        public ResponseCustomer DeleteCustomer( int id )
        {
            ResponseCustomer res = _cliente.DeleteCustomer(id);
            return res;
        }
    }
}
