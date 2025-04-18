using AuthAPI.Application.Dtos;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Repositories;
using BCrypt.Net;

namespace AuthAPI.Application.Services
{
    public class CriarUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public CriarUsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> ExecutarAsync(UsuarioCriacaoDto dto)
        {
            var existente = await _repo.BuscarPorEmailAsync(dto.Email);
            if (existente != null)
                return "E-mail já cadastrado.";

            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
                Role = dto.Role
            };

            await _repo.CadastrarAsync(usuario);
            return "Usuário criado com sucesso!";
        }
    }
}
