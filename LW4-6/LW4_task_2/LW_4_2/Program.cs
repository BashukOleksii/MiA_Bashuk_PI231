    using FluentValidation;
    using FluentValidation.AspNetCore;
    using LW_4_2.Models;
    using LW_4_2.Validator;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers().AddFluentValidation();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(oprions =>
    {
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        oprions.IncludeXmlComments(xmlPath);
    });

    builder.Services.AddValidatorsFromAssemblyContaining<SubscriptionValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<MessageValidator>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
