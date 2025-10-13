using CRUDAPI.Data;
using CRUDAPI.Models;

namespace CRUDAPI.EndPoints
{
    public static class MessageEndPoints
    {

        ///<summary>
        ///Реєструє всі маршрути до ресурсу Subscriptions
        ///</summary>
        ///<param name="app">Екземпляр<see cref="WebApplication"/>.</param>
        public static void mapMessages(this WebApplication app)
        {
            app.MapGet("/messages/{id:int}", (int id) =>
            {
                var mItem = Elements.messageItems.FirstOrDefault(A => A.Id == id);

                return mItem is null ? Results.NotFound() : Results.Ok(mItem);

            })
                .WithSummary("Отримати повідомлення")
                .Produces<MessageItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GETmessage")
                .WithDescription("Отримати конкретне повідомлення за Id")
                .WithOpenApi();

            app.MapGet("/messages", (int? ownerId, int? subId,string? title) =>
            {
                IEnumerable<MessageItems> items = Elements.messageItems;

                if (ownerId is not null)
                    items = items.Where(A => A.OwnerId == ownerId);
                if (subId is not null)
                    items = items.Where(A => A.SubId == subId);
                if(title is not null)
                    items = items.Where(A => A.Title == title);

                return items.Count() != 0 ? Results.Ok(items) : Results.NotFound();
            })

                .WithSummary("Отримати всі повідомлення або конкретні")
                .Produces<MessageItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GETmessages")
                .WithDescription("Отримати всі повідомлення або використати фільри для конкретизації")
                .WithOpenApi();

            app.MapPost("/messages", (MessageItems message) =>
            {
                if (!Valid.StringField(message.Title, 10, "Title", out var error))
                    return Results.BadRequest(error);
                
                if(!Valid.Check_Subscribe_Owner(message.SubId,message.OwnerId,out string error1))
                   return Results.BadRequest(error1);
                

                message.Id = Elements.messageItems.Max(A => A.Id) + 1;

                Elements.messageItems.Add(message);

                return Results.Created($"/messages/{message.Id}", message);

            })
                .WithSummary("Зафіксувати повідомлення в колекції")
                .Produces<MessageItems>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("POSTmessage")
                .WithDescription("Відправити повідомлення в колекцію")
                .WithOpenApi();

            app.MapPut("/messages/{id:int}", (int id, MessageItems updateM) =>
            {
                var m = Elements.messageItems.Find(A => A.Id == id);

                if (m is null)
                    return Results.NotFound();

                if (!Valid.Check_Subscribe_Owner(updateM.SubId, updateM.OwnerId, out string error1))
                    return Results.BadRequest(error1);

                if (!Valid.StringField(updateM.Title, 10, "Title", out var error))
                    return Results.BadRequest(error);

                m.Title = updateM.Title;
                m.OwnerId = updateM.OwnerId;
                m.SubId = updateM.SubId;

                return Results.Ok(m);

            })
                .WithSummary("Оновити повністю повідомлення")
                .Produces<MessageItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("PUTmessage")
                .WithDescription("Оновити повністю повідомлення в колекції за Id")
                .WithOpenApi();

            app.MapPatch("/messages/{id:int}", (int id, MessageItems updateM) =>
            {
                var m = Elements.messageItems.Find(A => A.Id == id);

                if (m is null)
                    return Results.NotFound();

                int OId = updateM.OwnerId != 0? updateM.OwnerId : m.OwnerId;
                int SId = updateM.SubId != 0? updateM.SubId : m.SubId;

                if (!Valid.Check_Subscribe_Owner(SId, OId, out string error1))
                    return Results.BadRequest(error1);

                if (updateM.OwnerId != 0)
                    m.OwnerId = updateM.OwnerId;
                if(updateM.SubId != 0)
                    m.SubId = updateM.SubId;
                if (updateM.Title is not null)
                {
                    if (!Valid.StringField(updateM.Title, 10, "Title", out var error))
                        return Results.BadRequest(error);
                    m.Title = updateM.Title;

                }
                return Results.Ok(m);

            })
                .WithSummary("Оновити частково повідомлення")
                .Produces<MessageItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("PATCHmessage")
                .WithDescription("Оновити частково повідомлення в колекції за Id")
                .WithOpenApi();

            app.MapDelete("/messages/{id:int}", (int id) =>
            {
                var m = Elements.messageItems.FirstOrDefault(A => A.Id == id);

                if (m is null)
                    return Results.NotFound();

                Elements.messageItems.Remove(m);

                return Results.NoContent();
            })
                .WithSummary("Видалити повідомлення")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("DELETEmessage")
                .WithDescription("Видалити повідомлення з колекції за Id")
                .WithOpenApi();
        }

    }
}
