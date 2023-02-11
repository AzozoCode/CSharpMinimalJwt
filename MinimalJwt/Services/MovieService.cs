using MinimalJwt.Models;
using MinimalJwt.Repositories;

namespace MinimalJwt.Services
{
    public class MovieService : IMovieService
    {
        public bool DeleteMovie(int id)
        {
            var isDeleted = MovieRepository.Movies.Find(x => x.Id == id);
            MovieRepository.Movies.Remove(isDeleted);   
            return true;
        }

        public List<Movie> GetAllMovies()
        {
            
            var movies = MovieRepository.Movies.ToList();
            return movies;
        }

        public Movie GetMovie(int id)
        {
            var movie =  MovieRepository.Movies.FirstOrDefault(x => x.Id == id);
            if (movie is null) return null;

            return movie;
        }

        public Movie PostMovie(Movie movie)
        {
            movie.Id = MovieRepository.Movies.Count + 1;
            MovieRepository.Movies.Add(movie);
            return movie;
        }

        public Movie PutMovie(Movie movie)
        {
            throw new NotImplementedException();
        }
    }
}
