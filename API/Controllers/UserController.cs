using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Contracts;
using Models.Dtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAccessToken? _access;
        private readonly TokenFactory? _tokenFactory;
        private readonly ILog? _log;
        public UserController(IAccessToken? access, TokenFactory? tokenFactory, ILog? log)
        {
            _access = access;
            _tokenFactory = tokenFactory;
            _log = log;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public RegisterResponse Register(RegisterRequest request)
        {
            RegisterResponse register = new RegisterResponse();
            if (ModelState.IsValid)
            {
                register = _access.Registrar(request);
            }
            else
            {
                register.idError = 1;
                register.message = "Los campos no estan completos";
            }
                return register;

        }

        [AllowAnonymous]
        [HttpPost("login")]
        public LoginResponse Login(LoginRequest request)
        {
            UserResponse respuestaValidacion = new UserResponse();
            LoginResponse resp = new LoginResponse();
            try
            {
                respuestaValidacion = _access!.ValidarCredenciales(request);
                if (respuestaValidacion.idError == 0)
                {
                    resp = _tokenFactory!.CreateToken(respuestaValidacion);
                    if (resp.idError == 0)
                    {
                        resp.token = resp.token;
                        resp.expires = Math.Round(resp.expiresDate.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds, 0);
                        resp.idError = 0;

                    }

                }
                else
                {
                    resp.idError = respuestaValidacion.idError;
                    resp.message = respuestaValidacion.message;
                }
            }
            catch (Exception ex)
            {
                _log!.RegistrarError(0, "Login", ex.ToString());
                respuestaValidacion.idError = -1;
                respuestaValidacion.message = "Ha ocurrido un error revisar log de error";
            }
            return resp;
        }

       

    }
}
