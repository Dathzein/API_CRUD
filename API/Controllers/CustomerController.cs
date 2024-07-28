using Microsoft.AspNetCore.Authorization;
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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerController( ICliente cliente, IHttpContextAccessor httpContextAccessor)
        {
            _cliente = cliente;
            _httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        [HttpGet("customer-id")]
        public ResponseGetCustomers GetCustomerById( string identificacion)
        {
            var idUsuario = _httpContextAccessor.GetUserIdFromClaims();

            ResponseGetCustomers res = _cliente.GetCustomersById(identificacion, Int32.Parse(idUsuario));
            
            return res;
        }
        [Authorize]
        [HttpGet("customers")]
        public ResponseGetCustomers GetAllCustomer()
        {
            var idUsuario = _httpContextAccessor.GetUserIdFromClaims();

            ResponseGetCustomers res = _cliente.GetCustomers(Int32.Parse(idUsuario));

            return res;
        }
        [Authorize]
        [HttpPost("add-customer")]
        public ResponseCustomer AddCustomer( RequestCustomer customer )
        {
            var idUsuario = _httpContextAccessor.GetUserIdFromClaims();

            if (ModelState.IsValid)
            {
                ResponseCustomer res = _cliente.AddCustomer( customer, Int32.Parse(idUsuario));
                return res;
            }
            return null;
        }
        [Authorize]
        [HttpPut("update-customer")]
        public ResponseCustomer UpdateCustomer( RequestCustomer customer )
        {
            var idUsuario = _httpContextAccessor.GetUserIdFromClaims();

            if (!customer.numero_identificacion.IsNullOrEmpty())
            {
                ResponseCustomer res = _cliente.UpdateCustomer(customer, Int32.Parse(idUsuario));
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
        [Authorize]
        [HttpDelete("delete-customer")]
        public ResponseCustomer DeleteCustomer( int id )
        {
            var idUsuario = _httpContextAccessor.GetUserIdFromClaims();

            ResponseCustomer res = _cliente.DeleteCustomer(id, Int32.Parse(idUsuario));
            return res;
        }
    }
}
