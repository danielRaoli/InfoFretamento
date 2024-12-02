using InfoFretamento.Application.Services;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("sqlserver");

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql(connectionString));

builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<ColaboradorService>();
builder.Services.AddScoped<MotoristaService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<VeiculoService>();
builder.Services.AddScoped<DespesaService>();
builder.Services.AddScoped<DocumentoService>();
builder.Services.AddScoped<GrupoDeCustoService>();
builder.Services.AddScoped<ViagemService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
