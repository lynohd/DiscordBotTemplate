using DiscordBot.DataAccess.DbContexts;
using DiscordBot.WebAPI;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "ApiKey" }
            },
            new string[] { }
        }
    });
    x.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme() { 
        Description = "API Key",
        Name = "x-api-key",
        In  = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
});
builder.Services.AddDbContext<DiscordContext>(optionsAction: options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("Discord"));
});

builder.Services.AddScoped<ApiKeyMiddleware>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
