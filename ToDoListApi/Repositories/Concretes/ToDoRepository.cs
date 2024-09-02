using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoListApi.Data;
using ToDoListApi.Entities;
using ToDoListApi.Repositories.Abstracts;

namespace ToDoListApi.Repositories.Concretes
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoListDbContext _dbContext;

        public ToDoRepository(ToDoListDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(ToDo entity)
        {
            await _dbContext.AddAsync(entity);
          await _dbContext.SaveChangesAsync();  
        }

        public async Task DeleteAsync(ToDo entity)
        {
           await Task.Run(() =>
            {
                _dbContext.ToDos.Remove(entity);
            });
            await _dbContext.SaveChangesAsync();    
        }

        public async Task<IEnumerable<ToDo>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                return _dbContext.ToDos;
            });
        }

        public async Task<ToDo> GetAsync(Expression<Func<ToDo, bool>> expression)
        {
          return await _dbContext.ToDos.FirstOrDefaultAsync(expression);
        }

        public async Task UpdateAsync(ToDo entity)
        {
            await Task.Run(() =>
            {
                _dbContext.ToDos.Update(entity);
            });
            await _dbContext.SaveChangesAsync();
        }
    }
}
