
using Api.Extensions;
using Business;
using Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("IdentityDb");
builder.Services.AddDataDependencies(connectionString);
builder.Services.AddBusinessDependencies();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}

app.MapEndpoints();

app.Run();