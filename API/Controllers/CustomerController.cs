using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models.Contracts;
using Models.Data;
using Models.Data.Views;
using Models.Dtos;

namespace API.Controllers
{
    [EnableCors]
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
            
            var idRole = _httpContextAccessor.GetContainerRoleFromClaims();
            ResponseGetCustomers res = new ResponseGetCustomers();

            if (idRole == "1" || idRole == "2")
            {
                res = _cliente.GetCustomersById(identificacion, Int32.Parse(idUsuario));
                return res;                
            }

            res.message = "No esta autorizado ha hacer eso";
            res.idError = 99;

            return res;
        }
        [Authorize]
        [HttpGet("customers")]
        public ResponseGetCustomers GetAllCustomer()
        {
            var idUsuario = _httpContextAccessor.GetUserIdFromClaims();
            var idRole = _httpContextAccessor.GetContainerRoleFromClaims();
            ResponseGetCustomers res = new ResponseGetCustomers();

            if (idRole == "1" || idRole == "2")
            {
                res = _cliente.GetCustomers(Int32.Parse(idUsuario));
            }

            res.message = "No esta autorizado ha hacer eso";
            res.idError = 99;

            return res;
        }
        [Authorize]
        [HttpPost("add-customer")]
        public ResponseCustomer AddCustomer( RequestCustomer customer )
        {
            var idUsuario = _httpContextAccessor.GetUserIdFromClaims();
            var idRole = _httpContextAccessor.GetContainerRoleFromClaims();


            ResponseCustomer res = new ResponseCustomer();

            if (idRole == "2")
            {
                if (ModelState.IsValid)
                {
                    res = _cliente.AddCustomer(customer, Int32.Parse(idUsuario));
                    return res;
                }
            }

            res.message = "No esta autorizado ha hacer eso";
            res.idError = 99;

            return res;
        }
        [Authorize]
        [HttpPut("update-customer")]
        public ResponseCustomer UpdateCustomer( RequestCustomer customer )
        {
            var idUsuario = _httpContextAccessor.GetUserIdFromClaims();
            var idRole = _httpContextAccessor.GetContainerRoleFromClaims();


            ResponseCustomer res = new ResponseCustomer();

            if (idRole == "2")
            {
                if (!customer.numero_identificacion.IsNullOrEmpty())
                {
                    res = _cliente.UpdateCustomer(customer, Int32.Parse(idUsuario));
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
            res.message = "No esta autorizado ha hacer eso";
            res.idError = 99;

            return res;


        }
        [Authorize]
        [HttpDelete("delete-customer")]
        public ResponseCustomer DeleteCustomer( int id )
        {
            var idUsuario = _httpContextAccessor.GetUserIdFromClaims();
            var idRole = _httpContextAccessor.GetContainerRoleFromClaims();
            ResponseCustomer res = new ResponseCustomer();

            if (idRole == "1")
            {
                res = _cliente.DeleteCustomer(id, Int32.Parse(idUsuario));
                return res;
            }
            res.message = "No esta autorizado ha hacer eso";
            res.idError = 99;

            return res;
        }
    }
}
