using CRUDAPI.Models;
using CRUDAPI.Data;

namespace CRUDAPI.EndPoints
{
    public static class SubscriptionsEndPoints
    {
        ///<summary>
        ///Реєструє всі маршрути до ресурсу Subscriptions
        ///</summary>
        ///<param name="app">Екземпляр<see cref="WebApplication"/>.</param>
        public static void mapSubscriptions(this WebApplication app)
        {
           

            app.MapGet("/subs/{id:int}", (int id) =>
            {
                var subItem = Elements.subsripptionItems.FirstOrDefault(A => A.Id == id);

                return subItem is null ? Results.NotFound() : Results.Ok(subItem);

            })
                .WithSummary("Отримати підписку")
                .Produces<SubscriptionItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GETSub")
                .WithDescription("Отримати конкретну підписку за Id")
                .WithOpenApi();

            app.MapGet("/subs", (int? ownerId, string? service) =>
            {
                IEnumerable<SubscriptionItems> items = Elements.subsripptionItems;

                if (ownerId is not null)
                    items = items.Where(A => A.OwnerId == ownerId);
                if (service is not null)
                    items = items.Where(A => A.Service == service);

                return items.Count() != 0 ? Results.Ok(items) : Results.NotFound();
            })
                .WithSummary("Отримати всі підписки або конкретні")
                .Produces<SubscriptionItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GETSubs")
                .WithDescription("Отримати всі підписки або конкретні за допомогою фільтрів")
                .WithOpenApi();

            app.MapPost("/subs", (SubscriptionItems subscription) =>
            {
                if (!Valid.StringField(subscription.Service, 3, "Service", out var error))
                    return Results.BadRequest(error);

                if (!Valid.CheckOwner(subscription.OwnerId))
                    return Results.BadRequest("Немає вказаного 'ownerId'");

                subscription.Id = Elements.subsripptionItems.Max(A => A.Id) + 1;

                Elements.subsripptionItems.Add(subscription);

                return Results.Created($"/subs/{subscription.Id}", subscription);

            })
                .WithSummary("Відправити підписку")
                .Produces<SubscriptionItems>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("POSTSub")
                .WithDescription("Відправити конкретну підписку")
                .WithOpenApi();

            app.MapPut("/subs/{id:int}", (int id, SubscriptionItems updateSub) =>
            {
                var subscription = Elements.subsripptionItems.Find(A => A.Id == id);

                if (subscription is null)
                    return Results.NotFound();

                if (!Valid.CheckOwner(updateSub.OwnerId))
                    return Results.BadRequest("Немає вказаного 'ownerId'");

                if (!Valid.StringField(updateSub.Service, 3, "Service", out var error))
                    return Results.BadRequest(error);

                subscription.OwnerId = updateSub.OwnerId;
                subscription.Service = updateSub.Service;

                return Results.Ok(subscription);

            })
                .WithSummary("Оновити повністю підписку")
                .Produces<SubscriptionItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("PUTSub")
                .WithDescription("Оновити повністю конкретну підписку за Id")
                .WithOpenApi();

            app.MapPatch("/subs/{id:int}", (int id, SubscriptionItems updateSub) =>
            {
                var subscription = Elements.subsripptionItems.Find(A => A.Id == id);

                if (subscription is null)
                    return Results.NotFound();

                if (!Valid.StringField(updateSub.Service, 3, "Service", out var error))
                    return Results.BadRequest(error);

                if (updateSub.OwnerId != 0)
                {

                    if (!Valid.CheckOwner(subscription.OwnerId))
                        return Results.BadRequest("Немає вказаного 'ownerId'");

                    subscription.OwnerId = updateSub.OwnerId;

                }
                if(updateSub.Service is not null)
                    subscription.Service = updateSub.Service;

                return Results.Ok(subscription);

                

            })
                .WithSummary("Оновити частково підписку")
                .Produces<SubscriptionItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("PATCHSub")
                .WithDescription("Оновити частково конкретну підписку за Id")
                .WithOpenApi();

            app.MapDelete("/subs/{id:int}", (int id) =>
            {
                var sub = Elements.subsripptionItems.FirstOrDefault(A => A.Id == id);

                if (sub is null)
                    return Results.NotFound();

                Elements.subsripptionItems.Remove(sub);

                return Results.NoContent();
            })
                .WithSummary("Видалити підписку")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("DELETESub")
                .WithDescription("Видалити конкретну підписку за Id")
                .WithOpenApi();
        }
    }
}
