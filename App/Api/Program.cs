using FairDraw.App.Core.Validators;
using FairDraw.App.Infrastructure;
using FairDraw.App.Infrastructure.Interfaces;
using FairDraw.App.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICompetitionsRepository, CompetitionsRepository>();

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

builder.Services.AddAutoMapper(x => x.AddMaps(typeof(Program).Assembly));

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
