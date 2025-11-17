    using FluentValidation;
    using FluentValidation.AspNetCore;
    using LW4_task_3.Interface.Interfaces;
    using LW4_task_3.Interface.InterfacesRepository;
    using LW4_task_3.Mapping;
    using LW4_task_3.Models;
    using LW4_task_3.Repositories;
    using LW4_task_3.Services;
    using LW4_task_3.Validators;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;


    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers().AddFluentValidation();


    builder.Services.AddScoped<IPeopleRepository,PeopleRepository>();
    builder.Services.AddScoped<ISubRepository,SubRepository>();
    builder.Services.AddScoped<IMessageRepository,MessageRepository>();
    builder.Services.AddScoped<IUserRepository,UserRepository>();   

    builder.Services.AddScoped<IPeopleService, PeopleService>();
    builder.Services.AddScoped<ISubService, SubService>();
    builder.Services.AddScoped<IMessageService, MessageService>();
    builder.Services.AddScoped<IUserService,UserService>();

    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
    builder.Services.AddSingleton<IPasswordHasher, PaswordHasher>();
    builder.Services.AddSingleton<JwtTokenGenerator>();

    var _jwtSetting = builder.Configuration.GetSection("JwtSettings");
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSetting["Issuer"],
            ValidAudience = _jwtSetting["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting["SecretKey"]))
        };

    });

    builder.Services.AddAuthorization();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new() { Title = "API", Version = "v1" });

        c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "¬вед≥ть токен у формат≥: Bearer {токен}"
        });

        c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
        {
            {
                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference
                    {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

    });

    builder.Services.AddValidatorsFromAssemblyContaining<PeopleValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<SubValidator>();
    builder.Services.AddValidatorsFromAssemblyContaining<MessageValidator>();

    builder.Services.AddAutoMapper(typeof(PeopleProfile));
    builder.Services.AddAutoMapper(typeof(SubProfile));
    builder.Services.AddAutoMapper(typeof(MessageProfile));
    builder.Services.AddAutoMapper(typeof(UserProfile));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
