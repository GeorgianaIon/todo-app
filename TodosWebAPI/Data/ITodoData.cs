using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public interface ITodoData
    {
         Task<IList<Todo>> GetTodos();
        Task RemoveTodo(int tdoId);
        Task<Todo> Update(Todo todo);
        Task<Todo> Get(int id);

        Task<IList<Todo>> GetTodosFilteredByBothAsync(bool? isCompleted, int? userId);

        Task<IList<Todo>> GetTodosFilteredAsync(bool? isCompleted);
        // Task ExecuteFilter();
        // int? filterById { get; set; }
        // bool? filterByIsCompleted { get; set; }
        // Task<IList<Todo>> todosToShow { get; set; }
        Task<Todo> AddTodoAsync(Todo todo);
    }
}