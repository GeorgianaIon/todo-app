using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data
{
    public class CloudTodoService : ITodoData
    {
        private string uri = "https://localhost:5001";
        private readonly HttpClient client;
        private HttpClientHandler clientHandler;

        public CloudTodoService()
        {
            clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
            client = new HttpClient(clientHandler);
        }

        public async Task<IList<Todo>> GetTodosAsync()
        {
            HttpResponseMessage response = await client.GetAsync($"{uri}/todos");
            if (!response.IsSuccessStatusCode)
                throw new Exception("Not good");
            string message = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new Exception(message);
            List<Todo> result = JsonSerializer.Deserialize<List<Todo>>(message, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result;
        }

        // public async Task<Todo> GetTodosAsyncc(int id)
        //     {
        //     HttpResponseMessage response = await client.GetAsync($"{uri}/todos/{id}");
        //     if (!response.IsSuccessStatusCode)
        //     {
        //         throw new Exception("Not good");
        //     }
        //
        //     string message = await response.Content.ReadAsStringAsync();
        //     if (!response.IsSuccessStatusCode)
        //         throw new Exception(message);
        //     Todo result = JsonSerializer.Deserialize<Todo>(message, new JsonSerializerOptions
        //         {
        //         PropertyNameCaseInsensitive = true
        //         });
        //     return result;
        //     }

        public async Task AddTodoAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri + "/todos", content);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task RemoveTodoAsync(int todoId)
        {
            HttpResponseMessage response=await client.DeleteAsync($"{uri}/todos/{todoId}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Error, {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<Todo> UpdateAsync(Todo todo)
        {
            string todoAsJson = JsonSerializer.Serialize(todo);
            HttpContent content = new StringContent(todoAsJson, Encoding.UTF8, "application/Json");
            HttpResponseMessage response = await client.PatchAsync($"{uri}/todos/{todo.Id}", content);
            string result = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
                throw new Exception(result);
            Todo newTodo = JsonSerializer.Deserialize<Todo>(result, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
            return newTodo;
        }

        public async Task<Todo> GetAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"{uri}/todos/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Not good");
            }
        
            string message = await response.Content.ReadAsStringAsync();
            Todo result = JsonSerializer.Deserialize<Todo>(message);
            return result;
        }
    }
}