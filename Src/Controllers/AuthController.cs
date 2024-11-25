using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TallerBackendIDWM.Src.DTOs.User;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Controllers
{
    /// <summary>
    /// Controlador para la autenticación de usuarios.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// Constructor del controlador de autenticación.
        /// </summary>
        /// <param name="authService">Servicio de autenticación.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="registerUserDto">Datos del usuario a registrar.</param>
        /// <returns>Usuario registrado con sus credenciales.</returns>
        [HttpPost("register")]
        public async Task<ActionResult<LoggedUserDto>> Register(RegisterUserDto registerUserDto){
            try{
                var response = await _authService.RegisterUser(registerUserDto);
                return Ok(response);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Inicia sesión para un usuario.
        /// </summary>
        /// <param name="loginUserDto">Credenciales del usuario.</param>
        /// <returns>Usuario logueado con el token generado.</returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoggedUserDto>> Login(LoginUserDto loginUserDto){
            try{
                var response = await _authService.Login(loginUserDto);
                return Ok(response);
            }
            catch (Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}