using HajorPay.ThriftService.API.Middlewares;
using HajorPay.ThriftService.Application;
using HajorPay.ThriftService.Infrastructure;
using HajorPay.ThriftService.Infrastructure.Contexts;
using HajorPay.ThriftService.Infrastructure.Identity;
using HajorPay.ThriftService.Infrastructure.Identity.Validators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ✅ Configure Kestrel to listen on the correct port for Cloud Run
builder.WebHost.ConfigureKestrel(options =>
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
    options.ListenAnyIP(int.Parse(port));
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Hajor API",
        Version = "v1"
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter a token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services
    .AddIdentity<ApplicationUser,IdentityRole<Guid>>(options =>
    {
        options.Password.RequiredLength = 10;
        options.Password.RequiredUniqueChars = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddPasswordValidator<UsernameAsPasswordValidator>()
    .AddPasswordValidator<PhoneNumberAsPasswordValidator>()
    .AddPasswordValidator<AppNameResemblesPasswordValidator>()
    .AddPasswordValidator<Top1000PasswordValidator<ApplicationUser>>();

//builder.Services.AddIdentityApiEndpoints<ApplicationUser, IdentityRole<Guid>>(options =>
//    {
//        options.Password.RequiredLength = 10;
//        options.Password.RequiredUniqueChars = 6;
//        options.Password.RequireLowercase = false;
//        options.Password.RequireUppercase = false;
//        options.Password.RequireNonAlphanumeric = false;
//        options.Password.RequireDigit = false;

//    })
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddPasswordValidator<UsernameAsPasswordValidator>()
//    .AddPasswordValidator<PhoneNumberAsPasswordValidator>()
//    .AddPasswordValidator<AppNameResemblesPasswordValidator>()
//    .AddPasswordValidator<Top1000PasswordValidator<ApplicationUser>>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SigningKey"]))
    };
});

//TODO: Your password was rejected because it is in a list of common passwords. Since we don’t want other people to easily guess your password, please choose another one.
//TODO: check null checks in validators
//how to throow exception in createuser/registeruser

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<ApplicationUser>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
