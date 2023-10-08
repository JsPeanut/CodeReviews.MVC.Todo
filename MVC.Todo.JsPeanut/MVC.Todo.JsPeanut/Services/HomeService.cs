using MVC.Todo.JsPeanut.Models;
using MVC.Todo.JsPeanut.Models.ViewModels;
using RestSharp;
using System.Text.Json;
using System.Web;

namespace MVC.Todo.JsPeanut.Services
{
    public class HomeService
    {
        public TodoViewModel GetAllTodos()
        {
            List<TodoModel> todoList = new();

            var client = new RestClient("https://localhost:7280");
            var request = new RestRequest("todoitems");
            var response = client.ExecuteGet(request);
            var data = JsonSerializer.Deserialize<List<TodoModel>>(response.Content!)!;

            foreach (var todo in data)
            {
                todoList.Add(todo);
            }

            return new TodoViewModel
            {
                TodoList = todoList
            };
        }

        public TodoModel GetTodoById(int id)
        {
            var todoList = GetAllTodos().TodoList;
            var todo = todoList.Where(x => x.Id == id).First();

            return todo;
        }

        public void Create(TodoModel todo)
        {
            var client = new RestClient("https://localhost:7280");
            var request = new RestRequest("todoitems");
            request.AddBody(new
            {
                name = todo.Name,
                isComplete = todo.IsComplete
            });
            var response = client.ExecutePost(request);
        }

        public void Delete(int id)
        {
            var client = new RestClient("https://localhost:7280");
            var request = new RestRequest($"todoitems/{HttpUtility.UrlEncode(id.ToString())}", Method.Delete);
            var response = client.Execute(request);
        }

        public void Update(TodoModel todo)
        {
            var client = new RestClient("https://localhost:7280");
            var request = new RestRequest($"todoitems/{HttpUtility.UrlEncode(todo.Id.ToString())}");
            request.AddBody(new
            {
                name = todo.Name,
                isComplete = todo.IsComplete
            });
            var response = client.ExecutePut(request);
        }
    }
}
