using ToDoAPI.Contracts;
using ToDoAPI.Data;
using ToDoAPI.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Services
{
    public class TodoService : ITodoService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<TodoService>? _logger;
        private readonly IMapper _mapper;
        

        public TodoService(AppDbContext context, ILogger<TodoService>? logger, IMapper mapper)
        {
            _context = context;
             _logger = logger;
            _mapper = mapper;
           
        }

        public async Task<IEnumerable<Todo>> GetAllAsync()
        {
          var allTodos = await _context.Todos.ToListAsync();

          if (allTodos == null){
            _logger?.LogError("No todos found.");
            throw new Exception("No todos found.");
          }
          
          return allTodos;
        }

        public async Task<Todo?> GetByIdAsync(Guid id)
        {
          var todo = await _context.Todos.FindAsync(id);
          if (todo == null){
            _logger?.LogError($"Todo with id {id} not found.");
            throw new Exception($"Todo with id {id} not found.");
          }

          return todo;
        }

        public async Task<Todo> CreateTodoAsync(CreateTodoRequest request)
        {
          try {
            var todo = _mapper.Map<Todo>(request);
            todo.Id = Guid.NewGuid();
            todo.CreatedAt = DateTime.UtcNow;

            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            
            return todo;
          }
          catch (Exception ex){
            _logger?.LogError($"An error occurred while creating a todo: {ex.Message}");
             throw new Exception("An error occurred while creating the Todo item.");
          }
        }

        public async Task<Todo?> UpdateTodoAsync(Guid id, UpdateTodoRequest request)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return null;
            }

            _mapper.Map(request, todo);
            todo.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return todo;
        }

        public async Task<bool> DeleteTodoAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo == null)
            {
                return false;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}