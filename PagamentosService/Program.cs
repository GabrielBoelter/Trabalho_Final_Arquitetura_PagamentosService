using Microsoft.EntityFrameworkCore;
using PagamentosService.Data;
using PagamentosService.External;
using PagamentosService.Repositories;
using PagamentosService.Services;

var builder = WebApplication.CreateBuilder(args);

// Adicionando os servi�os de controllers e Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Inje��o de depend�ncia
builder.Services.AddScoped<IPagamentoRepository, PagamentoRepository>();
builder.Services.AddScoped<IPagamentoService, PagamentoService>();

// Adicionando o HttpClient para fazer chamadas entre microsservi�os
builder.Services.AddHttpClient<ITreinosServiceClient, TreinosServiceClient>();

// Configura��o do SQLite 
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Aplicar migra��es automaticamente
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate(); // Substitui EnsureCreated()
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();