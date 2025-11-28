using APICOMMON.UtilObjects;
using ASISYA.Models.Autenticacion;
using Azure;
using FACADE;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ASISYA.Controllers.Autenticacion
{

    [Route("api/[controller]")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        private FACADE.Facade _BusClass;
        public FACADE.Facade BusClass
        {
            get
            {
                if (_BusClass != null)
                {
                    return _BusClass;
                }
                else
                {
                    return _BusClass = new FACADE.Facade(_configuration);
                }

            }
            set { _BusClass = value; }
        }

        MessageResponseOBJ MsgRes = new MessageResponseOBJ();

        [HttpPost, Route("GenerarToken")]

        public IActionResult Authenticate([Required, MaxLength(50)] string user, [Required, MaxLength(100)] string pass)
        {
            try
            {
                bool isAuthenticatedPruebas = user.ToUpper() == "ASISYATOKEN" && pass == "Sup3rU53r*_1";

                bool isAuthenticated = user.ToUpper() == "ASISYATOKEN" && pass == "Sup3rU53r*_1";


                //Datos por si se van a insertar en alguna tabla de logueo
                string ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
                string hostName = HttpContext.Request.Host.Value;

                if (isAuthenticated)
                {
                    return Ok(new
                    {
                        token = GenerateJwtToken()
                    });
                }
                else
                {
                    return Ok(new
                    {
                        token = "Usuario o contraseña incorrectos"
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    //success = false,
                    description = "Error: " + ex.Message,
                    result = (object)null
                });
            }
        }

        private string GenerateJwtToken()
        {
            try
            {
                var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, "ASISYATOKEN")
                };

                var tokeOptions = new JwtSecurityToken
                 (
                    issuer: jwtSettings.Issuer,
                    audience: jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return tokenString;
            }
            catch (Exception ex)
            {
                var error = ex.Message;
                return error;
            }
        }
    }
}
