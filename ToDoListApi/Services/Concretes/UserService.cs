using System.Linq.Expressions;
using WebApiFormatter.Entities;
using WebApiFormatter.Repositories.Abstracts;
using WebApiFormatter.Services.Abstracts;

namespace WebApiFormatter.Services.Concretes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(User entity)
        {
            await repository.AddAsync(entity);
        }

        public async Task DeleteAsync(User entity)
        {
            await repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            return list;
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            var item = await repository.GetAsync(expression);
            return item;
        }

        public async Task UpdateAsync(User entity)
        {
            await repository.UpdateAsync(entity);
        }
    }
}
