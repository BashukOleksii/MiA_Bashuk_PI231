    using CRUDAPI.Models;
    using CRUDAPI.Data;

    namespace CRUDAPI.EndPoints
    {
        public static class PeopleEndpoints
        {


        ///<summary>
        ///Реєструє всі маршрути до ресурсу People
        ///</summary>
        ///<param name="app">Екземпляр<see cref="WebApplication"/>.</param>
        public static void mapPeople(this WebApplication app)
            {


            app.MapGet("/peoples/{id:int}", (int id) =>
                {
                    var peopleItem = Elements.peopleItems.FirstOrDefault(A => A.Id == id);

                    return peopleItem is null ? Results.NotFound() : Results.Ok(peopleItem);

                })
                .WithSummary("Отримати корисувача за Id")
                .Produces<PeopleItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GETPeople")
                .WithDescription("Отримати користувача за Id")
                .WithOpenApi();



            app.MapGet("/peoples", (string? name, string? email) =>
                {
                    IEnumerable<PeopleItems> items = Elements.peopleItems;

                    if (name is not null)
                        items = items.Where(A => A.Name == name);
                    if (email is not null)
                        items = items.Where(A => A.Email == email);

                    return items.Count() != 0 ? Results.Ok(items) : Results.NotFound();
                })
                .WithSummary("Отримати всіх корисувачів або конкретизувати за допомогою фільтрів")
                .Produces<PeopleItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("GETPeoples")
                .WithDescription("Отримання всіх користувачів або конкреного за фільром")
                .WithOpenApi();

           
            app.MapPost("/peoples", (PeopleItems peopleItem) =>
            {
                if (!Valid.StringField(peopleItem.Name, 5, "Name", out var error1))
                    return Results.BadRequest(error1);

                if(!Valid.Email(peopleItem.Email,out var error2))
                    return Results.BadRequest(error2);

                peopleItem.Id = Elements.peopleItems.Max(A => A.Id) + 1;

                Elements.peopleItems.Add(peopleItem);

                return Results.Created($"/peoples/{peopleItem.Id}", peopleItem);

            })
                .WithSummary("Надіслати конкретного користувача в список")
                .Produces<PeopleItems>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("POSTPeople")
                .WithDescription("Відправити конкретного користувача")
                .WithOpenApi();


            app.MapPut("/peoples/{id:int}", (int id, PeopleItems updatePeople) =>
            {
                var people = Elements.peopleItems.Find(A => A.Id == id);

                if (people is null)
                    return Results.NotFound();

                if (!Valid.StringField(updatePeople.Name, 5, "Name", out var error1))
                    return Results.BadRequest(error1);

                if (!Valid.Email(updatePeople.Email, out var error2))
                    return Results.BadRequest(error2);

                people.Name = updatePeople.Name;
                people.Email = updatePeople.Email;

                return Results.Ok(people);

            })
                .WithSummary("Оновити повнсітю конкретного користувача")
                .Produces<PeopleItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("PUTPeople")
                .WithDescription("Оновити повністю конкреного кристувача")
                .WithOpenApi();

            app.MapPatch("/peoples/{id:int}", (int id, PeopleItems updatePeople) =>
            {
                var people = Elements.peopleItems.Find(A => A.Id == id);

                if (people is null)
                    return Results.NotFound();

                if (updatePeople.Name is not null)
                {
                    if (!Valid.StringField(updatePeople.Name, 5, "Name", out var error1))
                        return Results.BadRequest(error1);

                    people.Name = updatePeople.Name;
                }

                if (updatePeople.Email is not null)
                {
                    if (!Valid.Email(updatePeople.Email, out var error2))
                        return Results.BadRequest(error2);

                    people.Email = updatePeople.Email;
                }

                return Results.Ok(people);

            })
                .WithSummary("Оновити частково конкретного користувача")
                .Produces<PeopleItems>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status404NotFound)
                .Produces(StatusCodes.Status400BadRequest)
                .WithName("PATCHPeople")
                .WithDescription("Оновити частково конкреного кристувача")
                .WithOpenApi();

            app.MapDelete("/peoples/{id:int}", (int id) =>
            {
                var people = Elements.peopleItems.FirstOrDefault(A => A.Id == id);

                if (people is null)
                    return Results.NotFound();

                Elements.peopleItems.Remove(people);

                return Results.NoContent();
            })
                .WithSummary("Видалити конкретного користувача")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status404NotFound)
                .WithName("DELETEPeople")
                .WithDescription("Видалити конкретного користувача")
                .WithOpenApi();

        }

        }
    }
