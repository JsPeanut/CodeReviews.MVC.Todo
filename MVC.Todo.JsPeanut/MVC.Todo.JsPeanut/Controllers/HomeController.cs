using Microsoft.AspNetCore.Mvc;
using MVC.Todo.JsPeanut.Models;
using MVC.Todo.JsPeanut.Services;

namespace MVC.Todo.JsPeanut.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly HomeService _homeService;

        public HomeController(ILogger<HomeController> logger, HomeService homeService)
        {
            _logger = logger;
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            var todoViewModel = _homeService.GetAllTodos();
            return View(todoViewModel);
        }

        public IActionResult Insert(TodoModel todo)
        {
            var todoViewModel = _homeService.GetAllTodos();

            if (!ModelState.IsValid)
            {
                todoViewModel.Todo = todo;

                return View("Index", todoViewModel);
            }

            _homeService.Create(todo);

            return Redirect("https://localhost:7234");
        }

        public IActionResult Delete(int id)
        {
            _homeService.Delete(id);

            return Redirect("https://localhost:7234");
        }

        public IActionResult Update(TodoModel todo)
        {
            var todoViewModel = _homeService.GetAllTodos();

            if (!ModelState.IsValid)
            {
                todoViewModel.Todo = todo;

                return View("Index", todoViewModel);
            }

            _homeService.Update(todo);

            return Redirect("https://localhost:7234");
        }

        public IActionResult MarkAsComplete(int id)
        {
            var todo = _homeService.GetTodoById(id);

            _homeService.Update(new TodoModel
            {
                Id = id,
                Name = todo.Name,
                IsComplete = todo.IsComplete ? false : true
            });

            return Redirect("https://localhost:7234");
        }
    }
}