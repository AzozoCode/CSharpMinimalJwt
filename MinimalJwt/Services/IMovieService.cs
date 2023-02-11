using MinimalJwt.Models;

namespace MinimalJwt.Services
{
    public interface IMovieService
    {
        public Movie GetMovie(int id);

        public List<Movie> GetAllMovies();

        public Movie Put(Movie newMovie);

        public bool DeleteMovie(int id);  
        
        public Movie Create(Movie movie);
    }
}
