using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using VisentiaTwin_API.DataModels;
using VisentiaTwin_API.DataContexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddProblemDetails();


Action<DbContextOptionsBuilder> option;
if (builder.Environment.IsDevelopment())
{
    Console.WriteLine("DBContext: Using VisentiaTwinLocalDb");
    option = new Action<DbContextOptionsBuilder>(opt => opt.UseSqlServer(
       builder.Configuration.GetConnectionString("VisentiaTwinLocalDb"),
       sqlOptions =>
       {
           sqlOptions.EnableRetryOnFailure(
           maxRetryCount: 5,
           maxRetryDelay: TimeSpan.FromSeconds(30),
           errorNumbersToAdd: null);
       }));
}
else
{
    Console.WriteLine("DBContext: Using VisentiaTwinAzureDb");
    option = new Action<DbContextOptionsBuilder>(opt => opt.UseSqlServer(
       builder.Configuration.GetConnectionString("VisentiaTwinAzureDb"),
       sqlOptions =>
       {
           sqlOptions.EnableRetryOnFailure(
           maxRetryCount: 5,
           maxRetryDelay: TimeSpan.FromSeconds(30),
           errorNumbersToAdd: null);
       }));
}


builder.Services
    .AddDbContext<VTSystemContext>(option)
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

app.UseExceptionHandler();
app.UseStatusCodePages();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseMigrationsEndPoint();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var systemContext = services.GetRequiredService<VTSystemContext>();
    systemContext.Database.EnsureCreated();
    DbInitializer.InitializeSystem(systemContext);

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
//app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowSpecificOrigin");
app.MapControllers();

app.Run();
