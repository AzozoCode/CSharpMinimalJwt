using MinimalJwt.Models;

namespace MinimalJwt.Repositories
{
    public class UserRepository
    {
        public static List<User> Users = new()
        {
            new() { Username = "Luke_admin",EmailAddress="luke.admin@gmail.com",Password="MyPass_w0rd",GivenName="Luke",Surname="Roger",Role="Administrator"},

            new() { Username = "randy_standard", EmailAddress = "randy.standard@gmail.com", Password = "MyPass_w0rd", GivenName = "Randy", Surname = "Joseph", Role = "Standard" }
        };
    }
}
