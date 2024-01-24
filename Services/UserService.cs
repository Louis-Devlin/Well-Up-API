using Well_Up_API.Models;
namespace Well_Up_API.Services
{
    public class UserService
    {
        private readonly PostgresDbContext _context;

        public UserService(PostgresDbContext context)
        {
            _context = context;
        }
        public int Register(User user)
        {
            var existingUser = _context.Users.Where(u => u.Email == user.Email).FirstOrDefault();
            if (existingUser != null)
            {
                return -1;
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }
        public User Login(User user)
        {
            var userResponse = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (userResponse == null)
            {
                return null;
            }
            return userResponse;
        }

        public bool Update(int userId, UserRequest user)
        {
            var existingUser = _context.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (existingUser == null || existingUser.Password != user.Password)
            {
                return false;
            }
            if (existingUser.Name != user.Name)
            {
                existingUser.Name = user.Name;
            }
            if (user.NewPassword != null)
            {
                existingUser.Password = user.NewPassword;
            }
            _context.SaveChanges();
            return true;
        }
        public bool Delete (int userId){
            var existingUser = _context.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (existingUser == null)
            {
                return false;
            }
            _context.Users.Remove(existingUser);
            _context.SaveChanges();
            return true;
        }
    }
}