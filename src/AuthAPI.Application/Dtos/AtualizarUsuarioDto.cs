using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Application.Dtos
{
    public class AtualizarUsuarioDto
    {
        public string Nome { get; set; }
        public string SenhaNova { get; set; }
    }
}