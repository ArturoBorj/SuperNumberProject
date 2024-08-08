using SuperNumberProject.Models;

namespace SuperNumberProject.Services
{
    public class UserServices
    {
        private readonly SuperNumberdbContext _context;

        public UserServices(SuperNumberdbContext context)
        {
            _context = context;
        }

        public User? AuthenticateUser(string name, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Name and password cannot be empty.");
            }

            var user = _context.Users.SingleOrDefault(u => u.Name == name);
            return (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PassHash)?  null: user);
        }
    }
}
