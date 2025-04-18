using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Domain.Repositories;

namespace AuthAPI.Application.Services
{
    public class DeletarUsuarioService
    {
        private readonly IUsuarioRepository _repo;

        public DeletarUsuarioService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> ExecutarAsync(int id)
        {
            await _repo.DeletarAsync(id);
            return "Usuário deletado com sucesso!";
        }
    }
}