using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Application.Dtos;
using AuthAPI.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly RegistrarUsuarioService _registrarService;
        private readonly LoginUsuarioService _loginService;

        public AuthController(RegistrarUsuarioService registrarService, LoginUsuarioService loginService)
        {
            _registrarService = registrarService;
            _loginService = loginService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioDto dto)
        {
            var resultado = await _registrarService.ExecutarAsync(dto);

            if (resultado.Contains("sucesso"))
                return Ok(new { mensagem = resultado });

            return BadRequest(new { erro = resultado });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioDto dto)
        {
            var token = await _loginService.ExecutarAsync(dto);

            if (token == null)
                return Unauthorized(new { erro = "Credenciais inválidas." });

            return Ok(new { token });
        }

    }
}