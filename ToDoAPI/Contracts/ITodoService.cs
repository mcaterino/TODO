using ToDoAPI.Models;
using ToDoAPI.Contracts;

namespace ToDoAPI.Contracts
{
    public interface ITodoService
    {
        Task<IEnumerable<Todo>> GetTodosAsync();
        Task<Todo?> GetByIdAsync(Guid id);
        Task<Todo> CreateTodoAsync(CreateTodoRequest request);
        Task<Todo?> UpdateTodoAsync(Guid id, UpdateTodoRequest request);
        Task<bool> DeleteTodoAsync(Guid id);
    }
}