using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Repositories;
using AuthAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AuthAPI.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> BuscarPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> BuscarPorIdAsync(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<List<Usuario>> ListarTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task CadastrarAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeletarAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<(List<Usuario> usuarios, int total)> ListarComFiltroAsync(string nome, string email, int skip, int take)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(u => u.Nome.ToLower().Contains(nome.ToLower()));

            if (!string.IsNullOrWhiteSpace(email))
                query = query.Where(u => u.Email.ToLower().Contains(email.ToLower()));

            var total = await query.CountAsync();
            var usuarios = await query.Skip(skip).Take(take).ToListAsync();

            return (usuarios, total);
        }
    }
}