using InfoFretamento.Application.Services;
using InfoFretamento.Domain.Repositories;
using InfoFretamento.Infrastructure;
using InfoFretamento.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


var connectionString = "Server=srv1072.hstgr.io;Database=u111997024_InfoFretamento;User=u111997024_admin;Password=Inforservice0510@;";

builder.Services.AddDbContext<AppDbContext>(opts => opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped(typeof(IBaseRepository<>),typeof(BaseRepository<>));
builder.Services.AddScoped(typeof(IPessoaRepository<>), typeof(PessoaRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();
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
builder.Services.AddScoped<AdiantamentoService>();
builder.Services.AddScoped<AbastecimentoService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<DbSeed>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    var key = builder.Configuration["TokenConfiguration:SecurityKey"];

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key!)),
        ClockSkew = TimeSpan.Zero
    };
});


builder.Services.AddControllers().AddJsonOptions( options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});


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


    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("AppCors");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var dbInitializer = services.GetRequiredService<DbSeed>();
        await dbInitializer.SeedAsync(); // Executa o método de seed
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao inicializar o banco de dados: {ex.Message}");
    }
}

app.Run();
