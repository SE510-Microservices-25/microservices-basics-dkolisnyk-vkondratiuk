using Notiffly.Api.Common.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Construct(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
