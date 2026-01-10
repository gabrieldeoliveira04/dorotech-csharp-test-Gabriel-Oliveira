using Microsoft.AspNetCore.Authentication.JwtBearer; //Inclusão da funcionalidade do token JWT ao projeto
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();

