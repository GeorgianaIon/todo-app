using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data
{
    public interface ITodoData
    {
        // Task<Todo> GetTodosAsyncc(int id);
       Task<IList<Todo>> GetTodosAsync();
       Task AddTodoAsync(Todo todo);
       Task RemoveTodoAsync(int todoId);
       Task<Todo> UpdateAsync(Todo todo);
       Task<Todo> GetAsync(int id);
    }
}