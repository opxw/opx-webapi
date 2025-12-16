using LinqToDB.Repository;
using Opx.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var cs = new MySqlConnector.MySqlConnectionStringBuilder()
{
	Database = "mydb",
	Server = "127.0.0.1",
	UserID = "root",
	Password = "",
	Port = 3306
};

builder.Services.UseWebApi()
	.UseWebApiLinq2DbRepository(LinqToDB.ProviderName.MariaDB10MySqlConnector, cs.ConnectionString);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseWebApiHandler();

app.Run();
