using System.Text;
using Arcas.Server.Data;
using Arcas.Server.Models;
using Microsoft.AspNetCore.Identity;

namespace Arcas.Server.Helpers
{
    public class QuickQueryHelper
    {
        public static int GetUserID(ArcasDbContext _context, string username)
        {
            var user = _context.User.FirstOrDefault(u => u.username == username);
            if (user == null) return 0;
            return user.userID;
        }
    }
}
