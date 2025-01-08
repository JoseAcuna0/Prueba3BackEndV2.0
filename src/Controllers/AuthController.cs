using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ApiPrueba3V2._00.src.Interface;
using ApiPrueba3V2._00.src.DTOs;
using ApiPrueba3V2._00.src.Services;
using Microsoft.AspNetCore.Identity;
using ApiPrueba3V2._00.src.Model;


namespace ApiPrueba3V2._00.src.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<AppUser> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerDTO)
        {
            try
            {

                var user = new AppUser
                {
                    Email = registerDTO.email,
                    UserName = registerDTO.email,
                    PasswordHash = registerDTO.password
                };

                var result = await _userManager.CreateAsync(user, registerDTO.password);

                if(result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok("Usuario creado con exito!");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, result.Errors);
                }

                
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginDTO)
        {
            try
            {
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                var user = await _userManager.FindByEmailAsync(loginDTO.email);

                if (user == null)
                {
                    return Unauthorized("Usuario no encontrado");
                }

                var result = await _userManager.CheckPasswordAsync(user, loginDTO.password);

                if (result)
                {
                    var token = _jwtService.CreateToken(user);
                    return Ok(token);
                }
                else
                {
                    return Unauthorized("Credenciales incorrectas");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}