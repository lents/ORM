using DataAccess.Data;
using DataAccess.DataAccess;
using WebApi;
using DataAccess.LinqToDb.DataAccess;
using LinqToDB.AspNet;
using LinqToDB;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddLinqToDBContext<ApiDataContext>((provider, options)
           => options               
               .UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddSingleton<IUserData, UserData>();
builder.Services.AddScoped<DataAccess.LinqToDb.Data.IUserData, DataAccess.LinqToDb.Data.UserData>();
builder.Services.AddScoped<DataAccess.ADO.Data.IUserData, DataAccess.ADO.Data.UserData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureApi();

app.Run();

