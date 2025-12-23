using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Opx.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.UseOpxWebApi();

builder.Services.UseOpxJwtBearerTokenAuth(new Opx.WebApi.Jwt.JwtTokenValidationSetting()
{
	SecretKey = "thesecretkeyissceretceperetkey1234567890",
	Algorithm = SecurityAlgorithms.HmacSha256,
	Audience = "Application",
	ExpirationSeconds = 212121,
	Issuer = "https://xxxx.co.id"
});

var app = builder.Build();
app.UseOpxWebApiStatusCodePages();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.Run();
