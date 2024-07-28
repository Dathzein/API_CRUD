
using DataAcces;
using Microsoft.Extensions.Options;
using Models.Contracts;
using Models.Data;
using Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussines
{

    public class AccessToken : IAccessToken
    {
        public readonly BDContext _context;
        private readonly ILog _log;

        public AccessToken(BDContext context, ILog log)
        {
            _context = context;
            _log = log;
        }

        public RegisterResponse Registrar(RegisterRequest newUser)
        {

            RegisterResponse response = new RegisterResponse();

            try
            {
                Usuarios? usuario = _context.Usuarios!.Where(x => x.usuario!.Equals(newUser.usuario) && x.active == true).FirstOrDefault();
                if (usuario == null)
                {
                    Roles rol = _context.Roles.Where(x => x.id == newUser.id_role && x.active == true).FirstOrDefault()!;
                    if(rol != null)
                    {
                        Usuarios usuarios = new Usuarios
                        {
                            usuario = newUser.usuario,
                            contrasena = newUser.clave,
                            codigo_identificacion = newUser.codigo_identificacion,
                            numero_identificacion = newUser.numero_identificacion,
                            id_role = newUser.id_role,
                            active = true,
                            fecha_control = DateTime.Now,
                        };
                        _context.Usuarios.Add(usuarios);
                        _context.SaveChanges();
                        response.idError = 0;
                        response.message = "Usuario creado";
                    }
                    else
                    {
                        response.idError = 2;
                        response.message = "Rol no existe o esta inactivo";
                    }
                }
                else
                {
                    response.idError = 1;
                    response.message = "Usuario ya existe o esta inactivo";
                }
                return response;
            }
            catch (Exception ex)
            {
                _log!.RegistrarError(0, "Registar", ex.ToString());
                return new RegisterResponse
                { 
                    idError = -1,
                    message = "Ha ocurrido un error revisar log de error"
                };
            }
        }

        public UserResponse ValidarCredenciales(LoginRequest request)
        {
            UserResponse response = new UserResponse();

            try
            {
                Usuarios? usuario = _context.Usuarios!.Where(x => x.usuario.Equals(request.usuario) && x.contrasena.Equals(request.clave) && x.active == true).FirstOrDefault();
                if (usuario != null)
                {
                    if (usuario.active)
                    {
                        response.idUsuario = usuario.id;
                        response.idRole = usuario.id_role;

                    }
                    else
                    {
                        response.idError = 2;
                        response.message = "Usuario Inactivo.";
                    }
                }
                else
                {
                    response.idError = 1;
                    response.message = "Credenciales Incorrectas";
                }
                return response;
            }
            catch (Exception ex)
            {
                _log!.RegistrarError(0, "ValidarCredenciales", ex.ToString());
                return new UserResponse {
                    idError = -1,
                    message = "Ha ocurrido un error revisar log de error"
                };
            }
        }
    }
}
