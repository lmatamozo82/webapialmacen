using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using WebAPIAlmacen;
using WebAPIAlmacen.Filtros;
using WebAPIAlmacen.Helpers;
using WebAPIAlmacen.Middlewares;
using WebAPIAlmacen.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(
    opciones=> {
        opciones.Filters.Add(typeof(FiltroDeExcepcion));
        opciones.Filters.Add(typeof(LogFilter));   // También se puede poner a nivel de controlador/método poniendo // [TypeFilter(typeof(LogFilter))]
        //Filtro global para manejar las excepciones no manejadas en cualquier controlador.
    }).AddJsonOptions(opciones=>opciones.JsonSerializerOptions.ReferenceHandler=System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);  //hecho para evitar ciclos cruzados.



builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://www.apirequest.io").AllowAnyMethod().AllowAnyHeader();
        //builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




//Agregamos DBContext como servicio para poder usarlo como inyección de depenecias.
var connectionstring = builder.Configuration.GetConnectionString("defaultConnection");

//Opción 1 para agregar DBContext.

//builder.Services.AddDbContext<ApplicationDBContext>(opciones =>
//{
//    opciones.UseSqlServer(connectionstring); //Agregamos el servicio DBContext a la aplciacion.
//    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);    //Puesto para desactivar el tracking de forma general para este DBContext. Asi no tenemos que hacerlo en cada metodo.
//});


// AddDbContextPool mantiene varios DbContext vivos y te da uno sin usar en lugar de crear uno nuevo cada vez
// AddDbContextPool "recicla" el AddDbContextPool. Si se pone un punto de interrupción en el constructor del AddDbContextPool
// con AddDbContextPool no se para más que la primera vez. Con el bloque de arriba (AddDbContext) se detiene cada vez que se hace
//una petición
//builder.Services.AddDbContextPool<ApplicationDBContext>(opciones =>
//{
//    opciones.UseSqlServer(connectionstring);
//    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
//}
//);

//AddDbContextFactory permite instanciar un servicio para que cada controller lo utilice creando un using
//Puede ser útil para varios dbcontext
//Ha tardado mucho menos en ejecutar la primera consulta.
builder.Services.AddDbContextFactory<ApplicationDBContext>(opciones =>
{
    opciones.UseSqlServer(connectionstring);
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}
);



builder.Services.AddHttpContextAccessor(); //Inyecta un servicio neceario para su uso en el interfaz IGestorArvhivos


builder.Services.AddHttpClient("webapi", x => { x.BaseAddress = new Uri("https://localhost:7115"); });   //Llamada de ejemplo para ver com se usa httpclientfactory


//Configuración de servicios propios. Se define el alcance de cada uno de ellos.
builder.Services.AddTransient<ServicioTransient>();
builder.Services.AddScoped<ServicioScoped>();
builder.Services.AddSingleton<ServicioSingleton>();

builder.Services.AddTransient<IGestorArchivos,GestorArchivosLocal>();
builder.Services.AddScoped<ProductoExiste>();
builder.Services.AddDataProtection();
builder.Services.AddTransient<HashService>(); //Servicio propio para crear un HASH

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  //Configuramos servico de autenticación
               .AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(builder.Configuration["ClaveJWT"])),
                   ClockSkew = TimeSpan.Zero
               });

//Configuración de Swagger para que utilize autenticación.
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

});


//Nuestra tarea ya es de tipo IhostedService por lo tanto no hay que añadirlo como singleton
//builder.Services.AddHostedService<TareaProgramadaService>();

//Serilog
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();


var app = builder.Build();

//Middlewares

app.UseMiddleware<LogFilePathIPMiddleware>();
app.UseMiddleware<LogFileBodyHttpResponseMiddleware>();
app.UseCors(); //Para activar la configuración de Cors que hemos puesto arriba.


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();  //Necesario para que pueda devolver ficheros staticos.

app.UseAuthorization();

app.MapControllers();

app.Run();
