using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dtos
{
    public class Encriptacion
    {
        public string? clave { get; set; }
    }
    public class RegisterRequest
    {
        public string? usuario { get; set; }
        public string? clave { get; set; }
        public int codigo_identificacion { get; set; }
        public string? numero_identificacion { get; set; }
        public bool active { get; set; }
        public int id_role { get; set; }
    }
    public class RegisterResponse
    {
        public int idError { get; set; }
        public string? message { get; set; }
    }
    public class LoginRequest
    {
        public string? usuario { get; set; }
        public string? clave { get; set; }
    }
   
    public class LoginResponse
    {
        public int idError { get; set; }
        public string? message { get; set; }
        public string? token { get; set; }
        public DateTime expires { get; set; }
    }
    public class UserResponse
    {
        public int idError { get; set; }
        public string? message { get; set; }
        public int idUsuario { get; set; }
        public int idRole { get; set; }
    }
}
