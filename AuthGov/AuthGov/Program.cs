using AuthGov.LocalAuth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

KeyReader kr = new KeyReader();
if (kr.Init(1234))
{
    Console.WriteLine("KeyReader init was a succes !");
}

Console.WriteLine(kr.ReadKey("public"));
Console.WriteLine(kr.ReadKey("private", 1234));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.Run();

