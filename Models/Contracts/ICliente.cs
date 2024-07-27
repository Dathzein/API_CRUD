﻿using Models.Data;
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
        ResponseCustomer AddCustomer(RequestCustomer request);
        ResponseCustomer UpdateCustomer(RequestCustomer request);
        ResponseCustomer DeleteCustomer(int id);
        List<Clientes> GetCustomers();
        Clientes GetCustomersById(string id);
    }
}
