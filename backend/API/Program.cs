using FluentValidation;
using API.Data;
using API.Services.Commands.CreateEmpleadoCommand;
using API.Services.Commands.DeleteEmpleadoCommand;
using API.Services.Commands.UpdateEmpleadoCommand;
using API.Services.Queries.GetEmpleadoById;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<CodeContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(Options =>
{
    Options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader();
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(opt =>
{
    opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddScoped<IValidator<CreateEmpleadoCommand>, CreateEmpleadoCommandValidator>();
builder.Services.AddScoped<IValidator<UpdateEmpleadoCommand>, UpdateEmpleadoCommandValidator>();
builder.Services.AddScoped<IValidator<DeleteEmpleadoCommand>, DeleteEmpleadoCommandValidator>();

builder.Services.AddScoped<IValidator<GetEmpleadoByIdQuery>, GetEmpleadoByIdQueryValidator>();

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

app.UseCors();

app.MapControllers();

app.Run();
