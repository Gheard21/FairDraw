using FairDraw.App.Core.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-z9o0q5dh.eu.auth0.com/";
    options.Audience = "https://fairdraw.uk";
});

builder.Services.AddControllers();

builder.Services.AddAutoMapper(x => x.AddMaps([nameof(Program)]));

builder.Services.AddValidatorsFromAssemblyContaining<NewCompetitionRequestValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
