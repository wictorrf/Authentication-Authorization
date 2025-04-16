using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Application.Dtos
{
    public class LoginUsuarioDto
    {
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}