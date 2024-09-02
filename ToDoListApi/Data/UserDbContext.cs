using Microsoft.EntityFrameworkCore;
using WebApiFormatter.Entities;

namespace WebApiFormatter.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }


    }

}
