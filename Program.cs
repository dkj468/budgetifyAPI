using budgetifyAPI.Data;
using budgetifyAPI.Middleware;
using budgetifyAPI.Repository.Accounts;
using budgetifyAPI.Repository.Expenses;
using budgetifyAPI.Repository.Incomes;
using budgetifyAPI.Repository.Transactions;
using budgetifyAPI.Repository.Users;
using budgetifyAPI.Services;
using budgetifyAPI.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
})
.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORS", policy =>
    {
        policy
            .AllowAnyHeader()
            .AllowAnyOrigin()
            .AllowAnyMethod();
    });
});

// Datacontext
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("budgetify"));
});

// Fluent validator
builder.Services.AddValidatorsFromAssemblyContaining<IncomeValidator>();

builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IIncomeRepository, IncomeRepostory>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISignInHelper, SignInHelper>();


// JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtTokenKey"])),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
        options.Events = new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (string.IsNullOrEmpty(userId))
                {
                    context.Fail("no user found in given token");
                }
                // access the user service and get the user from databse
                var _userRepo = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                var user = await _userRepo.GetUserById(int.Parse(userId));
                if (user == null)
                {
                    context.Fail("no user found for given token in database");
                }
                _userRepo.User = user;
            },
            OnAuthenticationFailed = async context =>
            {
                Console.WriteLine(context.Exception.Message);
            }
        };
    });

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
app.MapOpenApi();
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/openapi/v1.json", "v1");
//    });

//}
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<DataContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
try
{
    await context.Database.MigrateAsync();
    await InitializeDatabase.Initialize(context);
}
catch(Exception ex)
{
    logger.LogError(ex, "An error occured while applying migrations");
}


//app.UseMiddleware<JwtMiddleware>();
app.UseCors("CORS");
app.UseAuthorization();

app.MapControllers();

app.Run();
