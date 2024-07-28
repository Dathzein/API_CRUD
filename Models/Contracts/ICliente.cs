using Models.Data;
using Models.Data.Views;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Contracts
{
    public interface ICliente
    {
        ResponseCustomer AddCustomer(RequestCustomer request, int id);
        ResponseCustomer UpdateCustomer(RequestCustomer request, int id);
        ResponseCustomer DeleteCustomer(int id, int idUser);
        ResponseGetCustomers GetCustomers(int id);
        ResponseGetCustomers GetCustomersById(string id, int idUser);
    }
}
