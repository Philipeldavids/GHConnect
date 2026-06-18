using FaithConnect.Application.Interfaces;
using FaithConnect.Application.Services;
using FaithConnect.Application.Utilities;
using FaithConnect.Domain.Models;
using FaithConnect.Infrastructure.DATA;
using FaithConnect.Infrastructure.Interfaces;
using FaithConnect.Infrastructure.Persistence;
using FaithConnect.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/faithconnect-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();
// Add services to the container.
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings")
); 

var conn = builder.Configuration.GetConnectionString("DefaultConnection");


//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(conn));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));


builder.Services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 7;
});
// Authentication (JWT)
var jwt = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwt["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // change to true in production
    options.SaveToken = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwt["Issuer"],
        ValidAudience = jwt["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),

        //NameClaimType = "sub", // ?? critical
        ClockSkew = TimeSpan.Zero
    };

});

builder.Services.AddScoped<IMemberPortalService, MemberPortalService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IMemberRepository, MemberRepository>();
builder.Services.AddScoped<IAttendanceRepository, AttendanceRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<
    IDashboardService,
    DashboardService>();
builder.Services.AddScoped<ITenantService,
    TenantService>();

builder.Services.AddScoped<
    IUserService,
    UserService>();
builder.Services.AddScoped<
    IPermissionService,
    PermissionService>();

builder.Services.AddScoped<
    IRoleService,
    RoleService>();

builder.Services.AddScoped<ITemplateService, TemplateService>();
builder.Services.AddScoped<ISettingsService, SettingsService>();
builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IServiceService , ServiceService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<TenantContext>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", p => p
    //WithOrigins("https://www.zucohr.com")
    .WithOrigins("http://localhost:5173")
    //.WithOrigins("https://faithconnect10.vercel.app")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
    );

});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FaithConnect", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below. \r\n\r\nBearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                new string[] { }
            }

        });
});



var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager =
        scope.ServiceProvider
            .GetRequiredService<
                RoleManager<Role>
            >();

    await SeedData.SeedRolesAsync(
        roleManager);
    var ctx = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();
    await PermissionSeeder.SeedAsync( ctx );
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseMiddleware<TenantMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
