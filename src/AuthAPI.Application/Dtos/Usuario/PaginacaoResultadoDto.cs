using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Application.Dtos.Usuario
{
    public class PaginacaoResultadoDto<T>
    {
        public required List<T> Itens { get; set; }
        public int Total { get; set; }
        public int Pagina { get; set; }
        public int TotalPaginas { get; set; }
    }
}