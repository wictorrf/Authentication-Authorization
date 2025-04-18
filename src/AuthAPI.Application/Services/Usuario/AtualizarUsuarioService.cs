using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AuthAPI.Application.Dtos;
using AuthAPI.Domain.Repositories;
using Microsoft.AspNetCore.Http;


namespace AuthAPI.Application.Services
{
    public class AtualizarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IHttpContextAccessor _httpContext;

        public AtualizarUsuarioService(IUsuarioRepository usuarioRepository, IHttpContextAccessor httpContext)
        {
            _usuarioRepository = usuarioRepository;
            _httpContext = httpContext;
        }

        public async Task<string> ExecutarAsync(AtualizarUsuarioDto dto)
        {
            var userId = _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out var id))
                return "Usuário não autenticado.";

            var usuario = await _usuarioRepository.BuscarPorIdAsync(id);

            if (usuario == null)
                return "Usuário não encontrado.";

            if (!string.IsNullOrEmpty(dto.Nome))
                usuario.Nome = dto.Nome;

            if (!string.IsNullOrEmpty(dto.SenhaNova))
                usuario.SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.SenhaNova);

            await _usuarioRepository.AtualizarAsync(usuario);

            return "Dados atualizados com sucesso!";
        }
    }
}