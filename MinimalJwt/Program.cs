using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using MinimalJwt.Models;
using MinimalJwt.Services;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateActor = true,
            ValidateAudience= true, 
            ValidateLifetime = true,
            ValidateIssuerSigningKey= true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key"))

        };
    });

builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IUserService, UserService>();



var app = builder.Build();

app.UseSwagger();
app.UseAuthorization();
app.UseAuthentication();



app.MapGet("/", () => "Hello World!");

app.MapPost("/login", (UserLogin user,IUserService service) =>

    Login(user,service)
);


app.MapPost("/create", 
    (Movie movie, IMovieService service) => Create(movie,service));

app.MapGet("/get",(int id,IMovieService service)=> Get(id,service));

app.MapGet("/list", (IMovieService service) => List(service));

app.MapPut("/put", (Movie newMovie, IMovieService service) => Update(newMovie, service));

app.MapDelete("/delete",(int id,IMovieService service)=>  Delete(id,service));


IResult Login(UserLogin user,IUserService service)
{
    if(!string.IsNullOrEmpty(user.Username)&& !string.IsNullOrEmpty(user.Password))
    {
        var loggedInUser = service.Get(user);
        if(loggedInUser is null) return Results.NotFound("User not found");

        var claims = new[] { 
            new Claim(ClaimTypes.NameIdentifier, loggedInUser.Username),
            new Claim(ClaimTypes.Email,loggedInUser.EmailAddress),
            new Claim(ClaimTypes.GivenName,loggedInUser.GivenName),
            new Claim(ClaimTypes.Surname,loggedInUser.Surname), 
            new Claim(ClaimTypes.Role,loggedInUser.Role)
        };

        var token = new JwtSecurityToken(
            issuer: builder.Configuration["Jwt:Issuer"],
            audience: builder.Configuration["Jwt:Audience"],
            claims:claims,
            expires:DateTime.UtcNow.AddDays(3),
            notBefore:DateTime.UtcNow,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Jwt:Key")),SecurityAlgorithms.HmacSha256)
            );


        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
    }
}

IResult Create(Movie movie, IMovieService service)
{
    var result = service.Create(movie);
    return Results.Ok(result);
}

IResult Get(int id,IMovieService service)
{
    var result = service.GetMovie(id);
    if(result is null) return Results.NotFound("Movie not Found");

    return Results.Ok(result);
}

IResult List(IMovieService service)
{
    var movies = service.GetAllMovies();
    if (movies is null) return Results.BadRequest("Movies not available");
    return Results.Ok(movies);
}


IResult Update(Movie newMovie,IMovieService service)
{
    var result = service.Put(newMovie);
    if (result is null) return Results.NotFound("Movie not found");

    return Results.Ok(result);

}

IResult Delete(int id, IMovieService service)
{
    var result = service.DeleteMovie(id);
    if (!result) return Results.BadRequest("Something went wrong");

    return Results.Ok(result);

}

app.UseSwaggerUI();


app.Run();



