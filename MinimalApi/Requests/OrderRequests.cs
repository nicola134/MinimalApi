using FluentValidation;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MinimalApi.Model;
using MinimalApi.Services;
using System.ComponentModel.DataAnnotations;

namespace MinimalApi.Requests
{
    public static class OrderRequests
    {
        public static WebApplication RegisterEndPoints(this WebApplication app)
        {
            app.MapGet("/orders", OrderRequests.GetAll)
                .Produces<List<Order>>()
                .WithTags("Orders");

            app.MapGet("/orders/{id}", OrderRequests.GetById)
                .Produces<Order>()
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Orders");

            app.MapPost("/orders", OrderRequests.Create)
                .Produces<Order>(StatusCodes.Status201Created)
                .Accepts<Order>("appplication/json")
                .WithTags("Orders");

            app.MapPut("/orders/{id}", OrderRequests.Update)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .Accepts<Order>("appplication/json")
                .WithTags("Orders");

            app.MapDelete("/orders/{id}", OrderRequests.Delete)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithTags("Orders")
                .ExcludeFromDescription();

            return app;
        }
        public static IResult GetAll(IOrderService service)
        {
            var orders = service.GetAll();
            return Results.Ok(orders);
        }
        public static IResult GetById(IOrderService service, Guid id)
        {
            var order = service.GetById(id);
            if(order == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(order);
        }

        public static IResult Create(IOrderService service, Order order, IValidator<Order> validator) 
        {
            var validationResult = validator.Validate(order);
            if(!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }
            service.Add(order);

            return Results.Created($"/orders/{order.Id}", order);
        }

        public static IResult Update(IOrderService service, Guid id, Order orderUpdate, IValidator<Order> validator)
        {
            var validationResult = validator.Validate(orderUpdate);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest(validationResult.Errors);
            }

            var order = service.GetById(id);
            if (order == null)
            {
                return Results.NotFound();
            }

            service.Update(order);

            return Results.NoContent();
        }

        public static IResult Delete(IOrderService service, Guid id)
        {
            var order = service.GetById(id);
            if (order == null)
            {
                return Results.NotFound();
            }

            service.Remove(id); 
            
            return Results.NoContent();

        }
    }
}
