using InfoFretamento.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InfoFretamento.Infrastructure.Repositories
{
    public class UserRepository(AppDbContext context) : IUserRepository
    {
        private readonly AppDbContext _context = context;
        public async Task<bool> Login(string username, string password)
        {
             var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(username) && u.Password.Equals(password));
             return user != null;   
        }
    }
}
