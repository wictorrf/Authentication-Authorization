using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Domain.Entities;

namespace AuthAPI.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario> BuscarPorEmailAsync(string email);
        Task<Usuario> BuscarPorIdAsync(int id);
        Task<List<Usuario>> ListarTodosAsync();
        Task CadastrarAsync(Usuario usuario);
        Task AtualizarAsync(Usuario usuario);
        Task DeletarAsync(int id);
    }
}