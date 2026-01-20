using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RegistroEstudiante.Adapters.MySql.Data;
using RegistroEstudiante.Adapters.MySql.Repositories;
using RegistroEstudiante.Application.Ports;
using RegistroEstudiante.Application.UseCases;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options => {
options.AddPolicy("CorsPolicy", policy => {
policy.AllowAnyOrigin() // o .WithOrigins("https://tudominio.com")
      .AllowAnyHeader() 
      .AllowAnyMethod(); }); });

var jwt = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Permiso", policy =>
        policy.RequireClaim("permiso"));
});


builder.Services.AddScoped<IPermisoRepository, PermisoRepositoryMySql>();
builder.Services.AddScoped<ObtenerPermisosUsuarioUseCase>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<LoginUsuarioUseCase>();

var connectionString = builder.Configuration.GetConnectionString("MySql");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepositoryMySql>();
builder.Services.AddScoped<IMateriaRepository, MateriaRepositoryMySql>();
builder.Services.AddScoped<IMateriaProfesorRepository, MateriaProfesorRepositoryMySql>();
builder.Services.AddScoped<IMateriaEstudianteRepository, MateriaEstudianteRepositoryMySql>();
builder.Services.AddScoped<CrearUsuarioUseCase>();
builder.Services.AddScoped<MateriaUseCase>();
builder.Services.AddScoped<MateriaProfesorUseCase>();
builder.Services.AddScoped<MateriaEstudianteUseCase>();

var app = builder.Build();

app.UseCors("CorsPolicy"); 
app.UseAuthentication(); 
app.UseAuthorization();


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
