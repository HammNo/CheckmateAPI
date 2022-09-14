using CheckmateAPI;
using CheckmateAPI.Services;
using Checkmate.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<TournamentService>();
builder.Services.AddScoped<MemberService>();
builder.Services.AddScoped<SmtpClient>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<LoginService>();
builder.Services.AddSingleton(builder.Configuration.GetSection("SMTP").Get<MailConfig>());
TokenConfig tkConfig = builder.Configuration.GetSection("Jwt").Get<TokenConfig>();
builder.Services.AddSingleton(tkConfig);
builder.Services.AddScoped<TokenService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
             new string[] {}
     }
 });
});

builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(o =>
    {
        o.AllowAnyOrigin();
        o.AllowAnyHeader();
        o.AllowAnyMethod();
    });
});

builder.Services.AddDbContext<CheckmateContext>(
    o => o.UseSqlServer(builder.Configuration.GetConnectionString("Default")
));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
     options.TokenValidationParameters = new TokenValidationParameters
     {
         ValidIssuer = tkConfig.Issuer,
         ValidateIssuer = true,
         ValidateAudience = false,
         ValidateLifetime = false,
         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tkConfig.Signature)),
         ValidateIssuerSigningKey = true
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
