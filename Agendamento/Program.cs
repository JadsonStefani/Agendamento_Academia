using Agendamento.Repositorio;
using Agendamento.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configuração do SQLite
builder.Services.AddDbContext<AcademiaContext>(options =>
    options.UseSqlite("Data Source=academia.db"));

// Services
builder.Services.AddScoped<IServAgendamento, ServAgendamento>();
builder.Services.AddScoped<IServAluno, ServAluno>();
builder.Services.AddScoped<IServAula, ServAula>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Inicializar e popular o banco de dados
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AcademiaContext>();
    context.Database.EnsureCreated();

    // Log do caminho do banco
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Banco de dados SQLite criado em: {DbPath}", context.DbPath);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
