using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ITRental.Data.AppDbContext>(options =>
    options.UseSqlite(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(System.IO.Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddScoped<ITRental.Services.IEquipmentService, ITRental.Services.EquipmentService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ITRental.Data.AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Equipments.Any())
    {
        context.Equipments.AddRange(
            new ITRental.Models.Equipment { Category = "Laptop", Model = "ThinkPad T14", DailyRate = 150, IsAvailable = true, ClientId = 1 },
            new ITRental.Models.Equipment { Category = "Projektor", Model = "Epson EB-X41", DailyRate = 80, IsAvailable = true, ClientId = 1 },
            new ITRental.Models.Equipment { Category = "Monitor", Model = "Dell UltraSharp", DailyRate = 50, IsAvailable = true, ClientId = 1 },
            new ITRental.Models.Equipment { Category = "Myszka", Model = "Logitech MX Master 3", DailyRate = 15, IsAvailable = true, ClientId = 1 },
            new ITRental.Models.Equipment { Category = "Klawiatura", Model = "Keychron K2", DailyRate = 25, IsAvailable = true, ClientId = 1 },
            new ITRental.Models.Equipment { Category = "Kamera", Model = "Logitech Brio", DailyRate = 40, IsAvailable = true, ClientId = 1 }
        );
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ITRental.Middleware.ExceptionHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();



