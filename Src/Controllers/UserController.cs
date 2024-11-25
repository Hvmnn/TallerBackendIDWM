using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TallerBackendIDWM.Src.DTOs.User;
using TallerBackendIDWM.Src.Models;
using TallerBackendIDWM.Src.Repositories.Interfaces;
using TallerBackendIDWM.Src.Services.Interface;

namespace TallerBackendIDWM.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly ITokenService _serviceToken;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _service = userService;
            _serviceToken = tokenService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDto>> GetUsers(){
            var result = _service.GetUsers().Result;
            return Ok(result);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDto>> SearchUser([FromQuery] string query){
            var result = _service.SearchUsers(query).Result;
            return Ok(result);
        }

        [HttpGet("genders")]
        public ActionResult<IEnumerable<Gender>> GetGenders(){
            var result = _service.GetGenders().Result;
            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> EditUser(int id, [FromBody] EditUserDto editUserDto){
            try{
                var idClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
                if(idClaim != null && int.Parse(idClaim.Value) != id){
                    return Unauthorized("Las IDs no coinciden.");
                }

                var result = await _service.EditUser(id, editUserDto);
                if(!result){
                    return NotFound("Usuario no encontrado.");
                }

                return Ok("Datos editados con exito.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/password")]
        [Authorize]
        public async Task<ActionResult<string>> ChangeUserPassword(int id, [FromBody] ChangePasswordDto changePasswordDto){
            try{
                var idClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
                if(idClaim != null && int.Parse(idClaim.Value) != id){
                    return Unauthorized("Las IDs no coinciden");
                }

                var result = await _service.ChangeUserPassword(id, changePasswordDto);
                if(!result){
                    return NotFound("Usuario no encontrado.");
                }

                return Ok("Contraseña cambiada con exito");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/state")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> ChangeUserState(int id, [FromBody] string newUserState){
            try{
                bool newState = bool.Parse(newUserState);
                var result = await _service.ChangeUserState(id, newState);
                if(!result){
                    return NotFound("Usuario no encontrado.");
                }

                return Ok("Estado cambiado con éxito.");
            }
            catch(FormatException){
                return BadRequest("Estado invalido.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeleteUser(){
            try{
                var userId = _serviceToken.GetUserIdFromToken();
                var result = await _service.DeleteUser(userId);
                if(!result){
                    return BadRequest("No se pudo eliminar la cuenta.");
                }

                return Ok("Cuenta eliminada exitosamente.");
            }
            catch (UnauthorizedAccessException ex){
                return Unauthorized(ex.Message);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}