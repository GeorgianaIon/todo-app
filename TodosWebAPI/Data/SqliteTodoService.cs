using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApplication1.Models;
using WebApplication1.Persistence;

namespace WebApplication1.Data
{
    public class SqliteTodoService : ITodoData
    {
        private TodoContext todoContext;
        
        public SqliteTodoService(TodoContext todoContext)
        {
            this.todoContext = todoContext;
        }
        
        public async Task<IList<Todo>> GetTodos()
        {
            return await todoContext.Todos.ToListAsync();
        }

        public async Task<IList<Todo>> GetTodosFilteredByBothAsync(bool? isCompleted, int? userId)
        {
            IList<Todo> todos = await todoContext.Todos.ToListAsync();
            if (isCompleted != null)
            {
                todos = todos.Where(t => t.IsCompleted == isCompleted).ToList();
            }

            if (userId != null)
                todos = todos.Where(t => t.UserId == userId).ToList();
            return todos;
        }

        public async Task<IList<Todo>> GetTodosFilteredAsync(bool? isCompleted)
        {
            IList<Todo> todos = await todoContext.Todos.ToListAsync();
            if(isCompleted != null);
            todos = todos.Where(t => t.IsCompleted == isCompleted).ToList();
            return todos;
        }

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            EntityEntry<Todo> newTodo = await todoContext.Todos.AddAsync(todo);
            await todoContext.SaveChangesAsync();
            return newTodo.Entity;

        }  

        public async Task RemoveTodo(int tdoId)
        {
            Todo toDelete = await todoContext.Todos.FirstOrDefaultAsync(t => t.TodoId == tdoId);
            if (toDelete != null)
            {
                todoContext.Todos.Remove(toDelete);
                await todoContext.SaveChangesAsync();
            }
        }

        public async Task<Todo> Update(Todo todo)
        {
            try
            {
                Todo toUpdate = await todoContext.Todos.FirstAsync(t => t.TodoId == todo.TodoId);
                toUpdate.IsCompleted = todo.IsCompleted;
                todoContext.Update(toUpdate);
                await todoContext.SaveChangesAsync();
                return toUpdate;
            }
            catch (Exception e)
            {
                throw new ($"No Todo found with id {todo.TodoId}");
            }
        }
        
        public async Task<Todo> Get(int id)
        {
            return await todoContext.Todos.FirstAsync(t => t.TodoId == id);
        }
        //
        // public Task ExecuteFilter()
        // {
        //     throw new System.NotImplementedException();
        // }

        // public int? filterById { get; set; }
        // public bool? filterByIsCompleted { get; set; }
        // public Task<IList<Todo>> todosToShow { get; set; }

    }
}