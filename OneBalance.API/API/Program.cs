using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
using Autorizacion.Middleware;
using DA;
using DA.Repositorios;
using Flujo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Reglas;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var tokenConfiguration = builder.Configuration.GetSection("Token").Get<TokenConfig>();

var jwtIssuer = tokenConfiguration.Issuer;
var jwtAudience = tokenConfiguration.Audience;
var jwtKey = tokenConfiguration.key;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    }
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();
builder.Services.AddScoped<IConfiguracion, Configuracion>();

builder.Services.AddScoped<ICuentaDA, CuentaDA>();
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();
builder.Services.AddScoped<ICategoriaDA, CategoriaDA>();
builder.Services.AddScoped<ITipoPagoDA, TipoPagoDA>();
builder.Services.AddScoped<IMovimientoDA, MovimientoDA>();

builder.Services.AddScoped<ICuentaFlujo, CuentaFlujo>();
builder.Services.AddScoped<IUsuarioFlujo, UsuarioFlujo>();
builder.Services.AddScoped<ICategoriaFlujo, CategoriaFlujo>();
builder.Services.AddScoped<ITIpoPagoFlujo, TipoPagoFlujo>();
builder.Services.AddScoped<IMovimientoFlujo, MovimientoFlujo>();

builder.Services.AddTransient<Autorizacion.Abstracciones.BW.IAutorizacionBW, Autorizacion.BW.AutorizacionBW>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.ISeguridadDA, Autorizacion.DA.SeguridadDA>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.IRepositorioDapper, Autorizacion.DA.Repositorios.RepositorioDapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AutorizacionClaims();

app.UseAuthorization();

app.MapControllers();

app.Run();
