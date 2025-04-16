using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthAPI.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // Aqui você pode definir a connection string manualmente ou pegar de um arquivo se quiser
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=AuthDb;Username=postgres;Password=123456");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}