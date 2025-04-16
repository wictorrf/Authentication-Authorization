using AuthAPI.Application.Dtos;
using AuthAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AtualizarUsuarioService _atualizarService;
        private readonly ListarUsuariosService _listarService;

        public UsuarioController(
            AtualizarUsuarioService atualizarService,
            ListarUsuariosService listarService
        )
        {
            _atualizarService = atualizarService;
            _listarService = listarService;
        }

        [HttpGet("perfil")]
        [Authorize]
        public IActionResult Perfil()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                mensagem = "Rota protegida acessada com sucesso!",
                usuario = new { id, email, role }
            });
        }

        [HttpPut("atualizar")]
        [Authorize]
        public async Task<IActionResult> Atualizar([FromBody] AtualizarUsuarioDto dto)
        {
            var resultado = await _atualizarService.ExecutarAsync(dto);

            if (resultado.Contains("sucesso"))
                return Ok(new { mensagem = resultado });

            return BadRequest(new { erro = resultado });
        }

        [HttpGet("todos")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListarTodos()
        {
            var lista = await _listarService.ExecutarAsync();
            return Ok(lista);
        }

    }
}
