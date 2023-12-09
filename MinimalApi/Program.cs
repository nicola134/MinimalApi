using MinimalApi.Model;
using MinimalApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IOrderService,OrderService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/orders", (IOrderService service) => service.GetAll());
app.MapGet("/orders/{id}", (IOrderService service, Guid id) => service.GetById(id));
app.MapPost("/orders", (IOrderService service, Order order) => service.Add(order));
app.MapPut("/orders/{id}", (IOrderService service, Guid id, Order order) => service.Update(order));
app.MapDelete("/orders/{id}", (IOrderService service, Guid id) => service.Remove(id));


app.Run();


