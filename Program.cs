using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using YBCarRental3D_API.DataModels;
using YBCarRental3D_API.DataContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


Action<DbContextOptionsBuilder> option;
if (builder.Environment.IsDevelopment())
{
 option = new Action<DbContextOptionsBuilder>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("YBCarRentalLocalDb")));
}
else
{
 option = new Action<DbContextOptionsBuilder>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("YBCarRentalAzureDb")));
}


builder.Services
    .AddDbContext<YBUserContext>(option)
    .AddDbContext<YBCarContext>(option)
    .AddDbContext<YBRentContext>(option)
    .AddCors(options =>
    {
        options.AddPolicy("AllowSpecificOrigin", builder =>
        {
            //builder.WithOrigins("https://your-allowed-domain.com")
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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

    var userContext = services.GetRequiredService<YBUserContext>();
    userContext.Database.EnsureCreated();
    DbInitializer.InitializeUsers(userContext);

    var carContext = services.GetRequiredService<YBCarContext>();
    carContext.Database.EnsureCreated();
    DbInitializer.InitializeCars(carContext);

    var orderContext = services.GetRequiredService<YBRentContext>();
    orderContext.Database.EnsureCreated();
    DbInitializer.InitializeOrders(orderContext);
}
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.MapControllers();

app.Run();
