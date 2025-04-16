using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Application.Dtos;
using AuthAPI.Domain.Repositories;

namespace AuthAPI.Application.Services
{
    public class ListarUsuariosService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ListarUsuariosService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<List<UsuarioListagemDto>> ExecutarAsync()
        {
            var usuarios = await _usuarioRepository.ListarTodosAsync();

            return usuarios.Select(u => new UsuarioListagemDto
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Role = u.Role,
                DataCriacao = u.DataCriacao
            }).ToList();
        }
    }
}