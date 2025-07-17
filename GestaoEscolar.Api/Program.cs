using GestaoEscolar.Application.UseCases.AtualizarDadosAlunoEMatriculas;
using GestaoEscolar.Application.UseCases.CadastrarAlunoEMatriculas;
using GestaoEscolar.Application.UseCases.ListarAlunosCadastrados;
using GestaoEscolar.Application.UseCases.ListarMatriculasPorAluno;
using GestaoEscolar.Application.UseCases.RemoverAluno;
using GestaoEscolar.Domain.Interfaces;
using GestaoEscolar.Infrastructure.Data;
using GestaoEscolar.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("GestaoEscolar")
    .EnableSensitiveDataLogging() // Ajuda no debug
    .EnableDetailedErrors());

builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IMatriculaRepository, MatriculaRepository>();
builder.Services.AddScoped<CadastrarAlunoEMatriculasUseCase>();
builder.Services.AddScoped<ListarAlunosCadastradosUseCase>();
builder.Services.AddScoped<ListarMatriculasPorAlunoUseCase>();
builder.Services.AddScoped<AtualizaAlunoEMatriculasUseCase>();
builder.Services.AddScoped<RemoverAlunoEMatriculasUseCase>();

builder.Services.AddControllers();
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
