using InfoFretamento.Application.Services;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);




builder.Services.AddDbContext<AppDbContext>(opts => opts.UseNpgsql("Host=ep-withered-bar-a5ctvc81.us-east-2.aws.neon.tech;Database=neondb;Username=neondb_owner;Password=O3ZVkCTfQRq1;SslMode=Require;"));

builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IPessoaRepository<>), typeof(PessoaRepository<>));
builder.Services.AddScoped<ReceitaService>();
builder.Services.AddScoped<DespesaService>();
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<MotoristaService>();
builder.Services.AddScoped<FornecedorService>();
builder.Services.AddScoped<VeiculoService>();
builder.Services.AddScoped<DespesaService>();
builder.Services.AddScoped<DocumentoService>();
builder.Services.AddScoped<ManutencaoService>();
builder.Services.AddScoped<ViagemProgramadaService>();
builder.Services.AddScoped<ServicoService>();
builder.Services.AddScoped<PassageiroService>();
builder.Services.AddScoped<ViagemService>();
builder.Services.AddScoped<PassagemService>();

builder.Services.AddControllers().AddJsonOptions( options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opts => opts.AddPolicy("AppCors", policy =>
{
    policy.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod();

}));

builder.Services.AddMemoryCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AppCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
