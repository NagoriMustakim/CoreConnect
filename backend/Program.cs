using Hangfire;
using LinkwayAPI.Configurations;
using LinkwayAPI.Constants.Program;
using LinkwayAPI.Data;
using LinkwayAPI.Models;
using LinkwayAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LinkwayDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(ProgramStrings.CONNECTION_STRING)), ServiceLifetime.Transient);



builder.Services.AddIdentity<UsrUser, IdentityRole>()
    .AddEntityFrameworkStores<LinkwayDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromHours(1));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration[ProgramStrings.JWT_VALID_AUDIENCE],
            ValidIssuer = builder.Configuration[ProgramStrings.JWT_VALID_ISSUEER],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[ProgramStrings.JWT_KEY]))
        };
    });

builder.Services.AddPolicies();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddDependencies();
builder.Services.AddHostedService<EmailBackgroundService>();

builder.Services.AddCors();


builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
builder.AllowAnyOrigin()
.AllowAnyHeader()
.AllowAnyMethod());

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), ProgramStrings.RESOURCE_PATH)),
    RequestPath = new PathString(ProgramStrings.REQUEST_PATH)
});
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
