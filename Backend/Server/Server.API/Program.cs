using Server.API.Routes;
using Server.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("SqLiteConnectionString");

builder.Services.AddBusinessLogic(connectionString!);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string allowCorsPolicy = "allowCorsPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowCorsPolicy, policyBuilder =>
    {
        policyBuilder.WithOrigins("*");
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
    } );
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors(allowCorsPolicy);

app.AddUserRoutes();

app.Run();