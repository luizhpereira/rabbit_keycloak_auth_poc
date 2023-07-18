using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

/* JWT Auth */ 
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

//}).AddJwtBearer(options =>
//{
//    options.RequireHttpsMetadata = false;
//    options.Authority = "http://192.168.2.88:8088/realms/usecase";

//    options.TokenValidationParameters = new TokenValidationParameters()
//    {
//        ValidateAudience = false,
//        ValidateIssuerSigningKey = true,
//        ValidateIssuer = true,
//        ValidIssuer = "http://192.168.2.88:8088/realms/usecase",
//        ValidateLifetime = true
//    };
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
