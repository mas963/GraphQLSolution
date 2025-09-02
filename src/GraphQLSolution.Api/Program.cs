using GraphQLSolution.Api.GraphQL.Mutations;
using GraphQLSolution.Api.GraphQL.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GraphQLSolution.Application.Exceptions;
using GraphQLSolution.Application.Services;
using GraphQLSolution.Application.Services.Impl;
using GraphQLSolution.Domain.Interfaces;
using GraphQLSolution.Infrastructure.Data;
using GraphQLSolution.Infrastructure.Repositories;
using GraphQLSolution.Infrastructure.Services;
using GraphQLSolution.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

builder.Services
    .AddGraphQLServer()
    .AddAuthorization()
    .AddQueryType<UserQueries>()
    .AddMutationType<UserMutations>()
    .AddFiltering()
    .AddSorting()
    .AddProjections()
    .AddErrorFilter<GraphQLErrorFilter>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL();

app.UseHttpsRedirection();

app.Run();