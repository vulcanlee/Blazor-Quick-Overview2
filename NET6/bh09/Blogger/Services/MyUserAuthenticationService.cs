using Blogger.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogger.Services
{
    public class MyUserAuthenticationService : IMyUserAuthenticationService
    {
        public MyUserAuthenticationService(BlogDbContext context)
        {
            Context = context;
        }

        public BlogDbContext Context { get; }

        public async Task<bool> CheckUserAsync(string account, string password)
        {
            var myuser = await Context.MyUser
                .FirstOrDefaultAsync(x => x.Account == account && x.Password == password);
            if (myuser == null)
                return false;
            else
                return true;
        }
    }
}
