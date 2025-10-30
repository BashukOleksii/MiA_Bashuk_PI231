using FluentValidation;
using FluentValidation.AspNetCore;
using LW4_task_3.Models;
using LW4_task_3.Validators;
using LW4_task_3.Services;
using LW4_task_3.Interfaces;
using LW4_task_3.InterfacesRepository;
using LW4_task_3.Repositories;
using LW4_task_3.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation();


builder.Services.AddScoped<IPeopleRepository,PeopleRepository>();
builder.Services.AddScoped<ISubRepository,SubRepository>();
builder.Services.AddScoped<IMessageRepository,MessageRepository>();

builder.Services.AddScoped<IPeopleService, PeopleService>();
builder.Services.AddScoped<ISubService, SubService>();
builder.Services.AddScoped<IMessageService, MessageService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(oprions =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    oprions.IncludeXmlComments(xmlPath);
});

builder.Services.AddValidatorsFromAssemblyContaining<PeopleValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SubValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MessageValidator>();

builder.Services.AddAutoMapper(typeof(PeopleProfile));
builder.Services.AddAutoMapper(typeof(SubProfile));
builder.Services.AddAutoMapper(typeof(MessageProfile));

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
