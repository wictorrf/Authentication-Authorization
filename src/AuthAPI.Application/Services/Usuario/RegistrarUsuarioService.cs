using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Application.Dtos;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Repositories;


namespace AuthAPI.Application.Services
{
    public class RegistrarUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public RegistrarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> ExecutarAsync(RegistrarUsuarioDto dto)
        {
            // Verificar se já existe usuário com o mesmo email
            var existente = await _usuarioRepository.BuscarPorEmailAsync(dto.Email);
            if (existente != null)
            {
                return "Já existe um usuário com esse e-mail.";
            }

            // Criar hash da senha
            var senhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha);

            // Criar usuário
            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                SenhaHash = senhaHash,
                Role = dto.Role
            };

            await _usuarioRepository.CadastrarAsync(usuario);

            return "Usuário cadastrado com sucesso!";
        }
    }
}