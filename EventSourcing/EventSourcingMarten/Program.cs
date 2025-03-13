using EventSourcingMarten.Controllers;
using Marten;
using Scalar.AspNetCore;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddMarten(o =>
{
    o.Connection(builder.Configuration.GetConnectionString("DefaultConnection"));
    o.UseSystemTextJsonForSerialization();
    o.Projections.Add<OrderProjection>(Marten.Events.Projections.ProjectionLifecycle.Inline);

    if (builder.Environment.IsDevelopment())
    {
        o.AutoCreateSchemaObjects = AutoCreate.All;
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
