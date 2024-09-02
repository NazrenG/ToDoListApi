using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiFormatter.Data;
using WebApiFormatter.Entities;
using WebApiFormatter.Repositories.Abstracts;

namespace WebApiFormatter.Repositories.Concretes
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;

        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User entity)
        {
            await _dbContext.AddAsync(entity);
          await _dbContext.SaveChangesAsync();  
        }

        public async Task DeleteAsync(User entity)
        {
           await Task.Run(() =>
            {
                _dbContext.Users.Remove(entity);
            });
            await _dbContext.SaveChangesAsync();    
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return _dbContext.Users;
            });
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
          return await _dbContext.Users.FirstOrDefaultAsync(expression);
        }

        public async Task UpdateAsync(User entity)
        {
            await Task.Run(() =>
            {
                _dbContext.Users.Update(entity);
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
