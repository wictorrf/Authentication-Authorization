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
        private readonly EditarUsuarioService _editarService;
        private readonly CriarUsuarioService _criarService;
        private readonly DeletarUsuarioService _deletarService;
        private readonly ListarUsuariosPaginadoService _listarPaginadoService;

        public UsuarioController(
            AtualizarUsuarioService atualizarService,
            ListarUsuariosService listarService,
            CriarUsuarioService criarService,
            EditarUsuarioService editarService,
            DeletarUsuarioService deletarService,
            ListarUsuariosPaginadoService listarPaginadoService
        )
        {
            _atualizarService = atualizarService;
            _listarService = listarService;
            _criarService = criarService;
            _editarService = editarService;
            _deletarService = deletarService;
            _listarPaginadoService = listarPaginadoService;
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

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Criar([FromBody] UsuarioCriacaoDto dto)
        {
            var resultado = await _criarService.ExecutarAsync(dto);
            return Ok(new { mensagem = resultado });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Editar(int id, [FromBody] UsuarioEdicaoDto dto)
        {
            var resultado = await _editarService.ExecutarAsync(id, dto);
            return Ok(new { mensagem = resultado });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Deletar(int id)
        {
            var resultado = await _deletarService.ExecutarAsync(id);
            return Ok(new { mensagem = resultado });
        }

        [HttpGet("paginado")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ListarComPaginacao([FromQuery] UsuarioFiltroDto filtro)
        {
            var resultado = await _listarPaginadoService.ExecutarAsync(filtro);
            return Ok(resultado);
        }

    }
}
