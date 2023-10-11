using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using YBCarRental3D_API.DataModels;
using YBCarRental3D_API.DataContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<YBUserContext>(opt =>
//opt.UseSqlServer(
//    builder.Configuration.GetConnectionString("YBCarRentalContext")));

//builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<YBUserContext>();
    context.Database.EnsureCreated();
    DbInitializer.Initialize(context);
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
