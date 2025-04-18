using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Application.Dtos
{
    public class UsuarioEdicaoDto
    {
        public required string Nome { get; set; }
        public required string Role { get; set; }
    }
}