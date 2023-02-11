using MinimalJwt.Models;
using MinimalJwt.Services;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IMovieService, MovieService>();
builder.Services.AddSingleton<IUserService, UserService>();



var app = builder.Build();

app.UseSwagger();

app.MapGet("/", () => "Hello World!");


app.MapPost("/create", 
    (Movie movie, IMovieService service) => Create(movie,service));

app.MapGet("/get",(int id,IMovieService service)=> Get(id,service));

app.MapGet("/list", (IMovieService service) => List(service));

app.MapPut("/put", (Movie newMovie, IMovieService service) => Update(newMovie, service));

app.MapDelete("/delete",(int id,IMovieService service)=>  Delete(id,service));


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



