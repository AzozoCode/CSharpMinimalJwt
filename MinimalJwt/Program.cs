using MinimalJwt.Models;
using MinimalJwt.Services;
using System.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IUserService, UserService>();


var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app.MapPost("/create", 
    (Movie movie, IMovieService service) => Create(movie,service));

app.MapGet("/get",(int id,IMovieService service)=> Get(id,service));

app.MapGet("/list", (IMovieService service) => List(service));

app.MapPut("/put", (Movie newMovie, IMovieService service) => Update(newMovie, service));


app.Run();



