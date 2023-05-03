using Autofac.Extensions.DependencyInjection;
using Autofac;
using MovieBL.DependencyResolves.Autofac;
using MovieDataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MovieDbContext>();
/*builder.Services.AddDbContext<MovieDbContext>(options =>
options.UseSqlServer(Configuration.GetConnectionString("connString")));*/

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new MovieAutofac()));

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "https://api.themoviedb.org/3/";
//        options.Audience = "9dcf2ba77f975bf9e7eac2255969b417";
//    });

builder.Services.AddAuthentication(options => //auth0 dan alýndý id:6452b96ab4c9d18c65475355
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.Authority = "https://dev-uto8dtnmt5bctz75.us.auth0.com/";
    options.Audience = "https://api.themoviedb.org/3/movie/popular?api_key=9dcf2ba77f975bf9e7eac2255969b417";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
