using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<Infrastructure.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<Core.Interfaces.IUsuarioRepository, Infrastructure.Repositories.UsuarioRepository>();
builder.Services.AddScoped<Core.Interfaces.IPostagemRepository, Infrastructure.Repositories.PostagemRepository>();
builder.Services.AddScoped<Core.Interfaces.IEventoRepository, Infrastructure.Repositories.EventoRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();