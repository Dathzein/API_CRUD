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
    public interface IAccessToken
    {
        RegisterResponse Registrar(RegisterRequest newUser);
        UserResponse ValidarCredenciales(LoginRequest request);
    }
}
