using Application.UseCases.UseCaseProducto;
using Application.UseCases.UseCaseProveedor;
using Domain.Interfaces;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.Mapping;
using Application.UseCases.UseCaseEmpleado;
using Application.UseCases.UseCaseCliente;
using Application.UseCases.UseCaseIngreso;
using Application.UseCases.UseCaseVenta;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("Infrastructure")));
//AutoMaper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

//Repositorios
builder.Services.AddScoped<IProducto, ProductoRepository>();
builder.Services.AddScoped<IProveedor, ProveedorRepository>();
builder.Services.AddScoped<IEmpleado, EmpleadoRepository>();
builder.Services.AddScoped<ICliente, ClienteRepository>();
builder.Services.AddScoped<IIngreso, IngresoRepository>();
builder.Services.AddScoped<IVenta, VentaRepository>();


//Casos de uso - Producto
builder.Services.AddScoped<CrearProducto>();
builder.Services.AddScoped<ActualizarProducto>();
builder.Services.AddScoped<EliminarProducto>();
builder.Services.AddScoped<ObtenerProductoPorId>();
builder.Services.AddScoped<ListarProductos>();

//Casos de uso - Proveedor
builder.Services.AddScoped<CrearProveedor>();
builder.Services.AddScoped<ActualizarProveedor>();
builder.Services.AddScoped<EliminarProveedor>();
builder.Services.AddScoped<ObtenerProveedorPorId>();
builder.Services.AddScoped<ListarProveedores>();

// Casos de uso - Empleado
builder.Services.AddScoped<CrearEmpleado>();
builder.Services.AddScoped<ActualizarEmpleado>();
builder.Services.AddScoped<EliminarEmpleado>();
builder.Services.AddScoped<ObtenerEmpleadoPorId>();
builder.Services.AddScoped<ListarEmpleados>();

// Casos de uso - Cliente
builder.Services.AddScoped<CrearCliente>();
builder.Services.AddScoped<ActualizarCliente>();
builder.Services.AddScoped<EliminarCliente>();
builder.Services.AddScoped<ObtenerClientePorId>();
builder.Services.AddScoped<ListarClientes>();

// Casos de uso - Ingreso
builder.Services.AddScoped<RegistrarIngreso>();
builder.Services.AddScoped<ListarIngresos>();
builder.Services.AddScoped<ObtenerIngresoPorId>();
builder.Services.AddScoped<RegistrarPagoIngreso>();
builder.Services.AddScoped<AnularIngreso>();

// Casos de uso - Venta
builder.Services.AddScoped<RegistrarVenta>();
builder.Services.AddScoped<ListarVentas>();
builder.Services.AddScoped<ObtenerVentaPorId>();
builder.Services.AddScoped<AnularVenta>();

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

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
