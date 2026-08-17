using System.Text;
using AyaBeauty.API.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
    builder.WebHost.ConfigureKestrel(k => k.Limits.MaxRequestBodySize = 15_728_640);

    builder.Services.AddDbContext<AyaBeautyDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("AyaBeautyDB")));

    builder.Services.Configure<Microsoft.AspNetCore.Http.Features.FormOptions>(o =>
{
    o.MultipartBodyLengthLimit = 10_485_760; // 10 MB
});

   var jwtKey = builder.Configuration["Jwt:Key"]!;
   builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
       };
   });

    var allowedOrigins = builder.Configuration["AllowedOrigins"]?.Split(',') ?? new[] { "http://localhost:4200" };

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAngular", policy =>
            policy.WithOrigins(allowedOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod());
    });

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
            ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=604800")
    });
    app.UseCors("AllowAngular");
    app.UseAuthorization();
    app.UseAuthentication();
    app.MapControllers();

    app.Run();