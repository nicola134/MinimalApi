using FluentValidation;
using MinimalApi.Model;
using MinimalApi.Requests;
using MinimalApi.Services;
using MinimalApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOrderService,OrderService>();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(OrderValidator));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.RegisterEndPoints();


app.Run();


