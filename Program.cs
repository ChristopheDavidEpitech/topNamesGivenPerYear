using System.Data;
using Dapper;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();




// var connString = "Host=localhost;Username=christophe;Password=test;Database=postgres";
// IDbConnection conn = new NpgsqlConnection(connString);

// // string createTableCmd = "CREATE TABLE IF NOT EXISTS name (id serial PRIMARY KEY, sexe varchar(1) NOT NULL, annee integer NOT NULL, prenoms varchar(100) NOT NULL);";
// string checkSqlCmd = "SELECT COUNT(*) FROM name;";
// marche pas pck  postrges  pas les droits de read
// string importSqlCmd = "COPY name(sexe, annee, prenoms) FROM './liste_des_prenoms.csv' DELIMITER ';' CSV HEADER;";
//Console.WriteLine("\\");
// conn.Query(checkSqlCmd);


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
