using InfoFretamento.Domain.Entities;

namespace InfoFretamento.Infrastructure
{
    public class DbSeed(AppDbContext context)
    {
        private readonly AppDbContext _context = context;
        public async Task SeedAsync()
        {
            if (!_context.Users.Any())
            {
                var user = new User { UserName = "Marcelo", Password = "senha123" };
                _context.Users.Add(user);

                await _context.SaveChangesAsync();
            }

        }
    }
}
