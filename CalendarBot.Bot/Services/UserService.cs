using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalendarBot.Data;
using Microsoft.EntityFrameworkCore;

namespace CalendarBot.Bot.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<object> GetUserFromDiscordId(string discordId)
        {
                var users = _applicationDbContext.Users.ToList();
                var userClaims = _applicationDbContext.UserClaims.ToList();
                var userLogins = _applicationDbContext.UserLogins.ToList();
                var userTokens = _applicationDbContext.UserTokens.ToList();

            var login = await _applicationDbContext.UserLogins.SingleOrDefaultAsync(l =>
                l.LoginProvider == "Discord" && l.ProviderKey == discordId);
            var user = await _applicationDbContext.Users.SingleOrDefaultAsync(u => u.Id == login.UserId);

            return user.Email;
        }
    }
}
