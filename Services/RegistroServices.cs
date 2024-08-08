using SuperNumberProject.Models;

namespace SuperNumberProject.Services
{
    public class RegistroServices
    {
        private readonly SuperNumberdbContext _context;

        public RegistroServices(SuperNumberdbContext context)
        {
            _context = context;
        }

        public User CreateUser(string name, string password)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Name and password cannot be empty.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = name,
                PassHash = BCrypt.Net.BCrypt.HashPassword(password),
                RegiDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}
