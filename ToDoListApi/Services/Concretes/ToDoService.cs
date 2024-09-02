using System.Linq.Expressions;
using ToDoListApi.Entities;
using ToDoListApi.Repositories.Abstracts;
using ToDoListApi.Services.Abstracts;

namespace ToDoListApi.Services.Concretes
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository repository;

        public ToDoService(IToDoRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(ToDo entity)
        {
            await repository.AddAsync(entity);
        }

        public async Task DeleteAsync(ToDo entity)
        {
            await repository.DeleteAsync(entity);
        }

        public async Task<IEnumerable<ToDo>> GetAllAsync()
        {
            var list = await repository.GetAllAsync();
            return list;
        }

        public async Task<ToDo> GetAsync(Expression<Func<ToDo, bool>> expression)
        {
            var item = await repository.GetAsync(expression);
            return item;
        }

        public async Task UpdateAsync(ToDo entity)
        {
            await repository.UpdateAsync(entity);
        }
    }
}
