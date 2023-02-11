using MinimalJwt.Models;

namespace MinimalJwt.Repositories
{
    public class MovieRepository
    {
        public static List<Movie> Movies = new() {
            new(){ Id= 1,Title="Ghost",Description="Feature adaptation of Frank",Rating=6.8},

            new(){Id= 2,Title="SpiderMan",Description="Feature Peter Parker",Rating=8.9},

            new(){Id= 3,Title="AquaMan",Description="Feature Bruce Monn",Rating=8.9},

            new(){Id= 4,Title="BatMan",Description="Feature Johnny Walker",Rating=8.9},

            new(){Id= 5,Title="SuperMan",Description="Feature Batemio",Rating=8.9},

            new(){Id= 6,Title="AntMan",Description="Feature Michael John",Rating=8.9},



        };
    }
}
