using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string SenhaHash { get; set; }

        public string Role { get; set; } = "Cliente"; // ou "Admin"

        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    }
}