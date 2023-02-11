using MinimalJwt.Models;
using MinimalJwt.Repositories;

namespace MinimalJwt.Services
{
    public class MovieService : IMovieService
    {
        public bool DeleteMovie(int id)
        {
            var oldMovie = MovieRepository.Movies.Find(x => x.Id == id);

            if (oldMovie != null)
            {
                MovieRepository.Movies.Remove(oldMovie);
                return true;
            }
             
            return false;
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

        public Movie PutMovie(Movie newMovie)
        {
            var oldMovie = MovieRepository.Movies.FirstOrDefault(x=>x.Id == newMovie.Id);
            if(oldMovie is null) return null;

                oldMovie.Title = newMovie.Title;
                oldMovie.Description = newMovie.Description;
                oldMovie.Rating = newMovie.Rating;

            return newMovie;

           
        }
    }
}
