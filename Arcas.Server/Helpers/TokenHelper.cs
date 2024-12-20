using System.Text;
using Arcas.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Arcas.Server.Helpers
{
    public class TokenHelper
    {
        public static string Get(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                sb.Append(chars[index]);
            }

            return sb.ToString();
        }

        public static bool Write(ArcasDbContext _context, int userID, string token)
        {
            var user = _context.User.FirstOrDefault(u => u.userID == userID);
            if (user == null)
            {
                return false;
            }

            user.token = token;
            _context.SaveChanges();
            return true;
        }

        public static bool Check(ArcasDbContext _context, int userID, string token)
        {
            var user = _context.User.FirstOrDefault(u => u.userID == userID && u.token == token);
            return user != null;
        }

    }
}
