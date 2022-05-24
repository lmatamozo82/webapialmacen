using Microsoft.EntityFrameworkCore;
using WebAPIAlmacen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Agregamos DBContext como servicio para poder usarlo como inyección de depenecias.
var connectionstring = builder.Configuration.GetConnectionString("defaultConnection");
builder.Services.AddDbContext<ApplicationDBContext>(opciones =>
{
    opciones.UseSqlServer(connectionstring); //Agregamos el servicio DBContext a la aplciacion.
    opciones.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);    //Puesto para desactivar el tracking de forma general para este DBContext. Asi no tenemos que hacerlo en cada metodo.
});

var app = builder.Build();

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
