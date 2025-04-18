using AuthAPI.Application.Dtos;
using AuthAPI.Application.Dtos.Usuario;
using AuthAPI.Domain.Repositories;

namespace AuthAPI.Application.Services
{
    public class ListarUsuariosPaginadoService
    {
        private readonly IUsuarioRepository _repo;

        public ListarUsuariosPaginadoService(IUsuarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<PaginacaoResultadoDto<UsuarioListagemDto>> ExecutarAsync(UsuarioFiltroDto filtro)
        {
            var skip = (filtro.Page - 1) * filtro.PageSize;

            var (usuarios, total) = await _repo.ListarComFiltroAsync(filtro.Nome, filtro.Email, skip, filtro.PageSize);

            return new PaginacaoResultadoDto<UsuarioListagemDto>
            {
                Itens = usuarios.Select(u => new UsuarioListagemDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Role = u.Role,
                    DataCriacao = u.DataCriacao
                }).ToList(),
                Total = total,
                Pagina = filtro.Page,
                TotalPaginas = (int)Math.Ceiling(total / (double)filtro.PageSize)
            };
        }
    }
}
