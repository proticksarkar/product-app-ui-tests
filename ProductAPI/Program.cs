using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProductAPI;
using ProductAPI.Data;
using ProductAPI.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services
    .AddSwaggerGen(c =>
    {
        c
            .SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "ProductAPI",
                Version = "v1"
            });

        c.SchemaFilter<EnumSchemaFilter>();
    });

builder.Services
    .AddDbContext<ProductDbContext>(options =>
        options
            .UseSqlServer(builder.Configuration
                .GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IProductRepository, ProductRepository>();

// Build
var app = builder.Build();

// Seed data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var productDbContext = services.GetRequiredService<ProductDbContext>();
    productDbContext.Database.Migrate();
    productDbContext.Database.EnsureCreated();
    productDbContext.Seed();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Run
app.Run();
