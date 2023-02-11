using MinimalJwt.Models;

namespace MinimalJwt.Services
{
    public interface IMovieService
    {
        public Movie GetMovie(int id);

        public List<Movie> GetAllMovies();

        public Movie PostMovie(Movie movie);

        public bool DeleteMovie(int id);  
        
        public Movie PutMovie(Movie movie);
    }
}
