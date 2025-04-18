using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Application.Dtos
{
    public class UsuarioListagemDto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Role { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}