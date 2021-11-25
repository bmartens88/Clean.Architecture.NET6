using Clean.Architecture.Api.WebApi;
using Clean.Architecture.Api.WebApi.Extensions;
using Clean.Architecture.Core.Application;
using Clean.Architecture.Infrastructure.Persistence;
using Clean.Architecture.Infrastructure.Persistence.Data;
using Clean.Architecture.Infrastructure.Persistence.Data.Seeds;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApiServices(builder.Configuration)
    .AddApplicationCore()
    .AddPersistenceInfrastructure(builder.Configuration);
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

var app = builder.Build();

// Configure the HTTP request pipeline.

// Use custom exception middleware
app.UseExceptionMiddleware();

// As this is a public API, we always want to include Swagger doc
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions)
    {
        c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
            description.GroupName.ToUpperInvariant());
        c.RoutePrefix = string.Empty;
    }
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

// Seed Database
using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    await MessageSeed.SeedAsync(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB.");
}

app.Run();
