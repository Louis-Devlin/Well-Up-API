using Well_Up_API.Models;
using System.Security.Cryptography;
using System.Text;
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
            user.Password = EncrpytString(user.Password);

            _context.Users.Add(user);
            _context.SaveChanges();
            return user.UserId;
        }
        public User Login(User user)
        {
            user.Password = EncrpytString(user.Password);
            var userResponse = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            if (userResponse == null)
            {
                return null;
            }
            return userResponse;
        }

        public bool Update(int userId, UserRequest user)
        {
            user.Password = EncrpytString(user.Password);
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
        public bool Delete(int userId)
        {
            var existingUser = _context.Users.Where(u => u.UserId == userId).FirstOrDefault();
            if (existingUser == null)
            {
                return false;
            }
            _context.Users.Remove(existingUser);
            _context.SaveChanges();
            return true;
        }
        private string EncrpytString(string plaintextPassword)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes("b14ca5898a4e4133bbce2ea2315a1916");
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plaintextPassword);
                        }
                        array = memoryStream.ToArray();
                    }
                }

            }
            return Convert.ToBase64String(array);
        }
    }
}