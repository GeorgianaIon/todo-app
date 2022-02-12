using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class TodoJSONData : ITodoData
    {
        // public int? filterById { get; set; }
        // public bool? filterByIsCompleted { get; set; }
        // public IList<Todo> todosToShow { get; set; }

        private string todoFile = "todos.json";
        private IList<Todo> todos;

        public TodoJSONData()
        {
            if (!File.Exists(todoFile))
            {
                Seed();
                WriteTodosToFile(); 
            }
            else
            {
                string content = File.ReadAllText(todoFile);
                todos = JsonSerializer.Deserialize<List<Todo>>(content);
            }
        }
        
        public async Task<IList<Todo>> GetTodos()
        {
            List<Todo> tmp = new List<Todo>(todos);
            return tmp;
        }
        
        // public void ExecuteFilter()
        // {
        //     todosToShow = todos.Where(t => (filterById != null && t.UserId == filterById || filterById == null) &&
        //                                    (filterByIsCompleted != null && t.IsCompleted == filterByIsCompleted ||
        //                                     filterByIsCompleted == null)).ToList();
        // }

        public void Seed()
        {
            Todo[] ts =
            {
                new Todo()
                {
                    UserId = 1,
                    TodoId = 1,
                    Title = "Do dishes",
                    IsCompleted = false
                },
                new Todo()
                {
                    UserId = 1,
                    TodoId = 2,
                    Title = "Walk the dog",
                    IsCompleted = false
                },
                new Todo()
                {
                    UserId = 2,
                    TodoId = 3,
                    Title = "Do DNP homework",
                    IsCompleted = true
                },
                new Todo()
                {
                    UserId = 3,
                    TodoId = 4,
                    Title = "Eat Breakfast",
                    IsCompleted = false
                },
                new Todo()
                {
                    UserId = 4,
                    TodoId = 5,
                    Title = "Mow lawnk",
                    IsCompleted = true
                },
            };
            todos = ts.ToList();
        }

        public async Task RemoveTodo(int todoId)
        {
            Todo toRemove = todos.First(t => t.TodoId == todoId);
            todos.Remove(toRemove);
            WriteTodosToFile();
        }

        private void WriteTodosToFile()
        {
            string todosAsJson = JsonSerializer.Serialize(todos);
            File.WriteAllText(todoFile, todosAsJson);
        }

        public async Task<Todo> Update(Todo todo)
        {
            Todo toUpdate = todos.FirstOrDefault(t => t.TodoId == todo.TodoId);
            if (toUpdate == null) throw new Exception($"No Todo with this id: {todo.TodoId}");
            toUpdate.IsCompleted = todo.IsCompleted;
            toUpdate.Title = todo.Title;
            WriteTodosToFile();
            return toUpdate;
        }

        public async Task<Todo> Get(int id)
        {
            return todos.FirstOrDefault(t => t.TodoId == id);
        }

        public async Task<IList<Todo>> GetTodosFilteredByBothAsync(bool? isCompleted, int? userId)
        {
            List<Todo> tmp = new List<Todo>();
            foreach (var todo in todos)
            {
                if (todo.IsCompleted == isCompleted && todo.UserId == userId) ;
                tmp.Add(todo);
            }

            return tmp;
        }

        public async Task<IList<Todo>> GetTodosFilteredAsync(bool? isCompleted)
        {
            List<Todo> tmp = new List<Todo>();
            foreach (var todo in todos)
            {
                if(todo.IsCompleted==isCompleted)
                    tmp.Add(todo);
            }

            return tmp;
        }

        public async Task<Todo> AddTodoAsync(Todo todo)
        {
            int max = todos.Max(todo => todo.TodoId);
            todo.TodoId = (++max);
            todos.Add(todo);
            WriteTodosToFile();
            return todo;
        }
    }
}