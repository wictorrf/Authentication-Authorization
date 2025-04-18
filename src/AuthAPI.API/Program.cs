using System.Text;
using AuthAPI.Application.Services;
using AuthAPI.Domain.Repositories;
using AuthAPI.Infrastructure.Data;
using AuthAPI.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configuração de serviços
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<RegistrarUsuarioService>();
builder.Services.AddScoped<LoginUsuarioService>();
builder.Services.AddScoped<ListarUsuariosService>();
builder.Services.AddScoped<EditarUsuarioService>();
builder.Services.AddScoped<DeletarUsuarioService>();
builder.Services.AddScoped<CriarUsuarioService>();
builder.Services.AddScoped<ListarUsuariosPaginadoService>();


builder.Services.AddControllers(); // Habilita suporte a controllers
var chaveJwt = builder.Configuration["Jwt:ChaveSecreta"];
var chaveBytes = Encoding.ASCII.GetBytes(chaveJwt);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(opt =>
{
    opt.RequireHttpsMetadata = false;
    opt.SaveToken = true;
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(chaveBytes),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor(); // necessário para acessar o usuário logado
builder.Services.AddScoped<AtualizarUsuarioService>();

// ✅ Construção da aplicação
var app = builder.Build();

// ✅ Pipeline de middlewares
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Faltando no seu código
app.UseAuthorization();

app.MapControllers();

app.Run();
