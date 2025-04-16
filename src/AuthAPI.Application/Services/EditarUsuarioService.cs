using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Application.Dtos;
using AuthAPI.Domain.Repositories;

namespace AuthAPI.Application.Services
{
    public class EditarUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public EditarUsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> ExecutarAsync(int id, UsuarioEdicaoDto dto)
        {
            var usuario = await _repo.BuscarPorIdAsync(id);
            if (usuario == null)
                return "Usuário não encontrado.";

            usuario.Nome = dto.Nome;
            usuario.Role = dto.Role;

            await _repo.AtualizarAsync(usuario);
            return "Usuário atualizado com sucesso!";
        }
    }
}