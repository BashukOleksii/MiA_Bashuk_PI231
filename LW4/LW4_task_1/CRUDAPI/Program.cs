using CRUDAPI.EndPoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(oprions =>
{
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    oprions.IncludeXmlComments(xmlPath);
});
var app = builder.Build();
    
if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
    
    app.UseSwaggerUI();

}

app.mapPeople();
app.mapSubscriptions();
app.mapMessages();

app.Run();
