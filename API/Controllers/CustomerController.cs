using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Contracts;
using Models.Data;
using Models.Data.Views;
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
        public object GetCustomerById( string id)
        {
            Clientes cliente = _cliente.GetCustomersById(id);
            return new
            {
                Cliente = cliente,
            };
        }
        [HttpGet("customers")]
        public List<Vw_Clientes> GetAllCustomer()
        {
            List<Vw_Clientes> clientes = _cliente.GetCustomers();
            return clientes;
        }

        [HttpPost("add-customer")]
        public ResponseCustomer AddCustomer( RequestCustomer customer )
        {
            if(ModelState.IsValid)
            {
                ResponseCustomer res = _cliente.AddCustomer( customer );
                return res;
            }
            return null;
        }
        [HttpPut("update-customer")]
        public ResponseCustomer UpdateCustomer( RequestCustomer customer )
        {
            if(!customer.numero_identificacion.IsNullOrEmpty())
            {
                ResponseCustomer res = _cliente.UpdateCustomer(customer);
                return res;
            }
            else
            {
                return new ResponseCustomer
                {
                    idError = 1,
                    message = "Numero de identificacion es necesario"
                };
            }
        }
        [HttpDelete("delete-customer")]
        public ResponseCustomer DeleteCustomer( int id )
        {
            ResponseCustomer res = _cliente.DeleteCustomer(id);
            return res;
        }
    }
}
