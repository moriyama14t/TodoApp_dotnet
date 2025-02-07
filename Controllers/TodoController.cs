using Microsoft.AspNetCore.Mvc;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        private readonly ILogger<TodoController> _logger;

        public TodoController(TodoContext context,ILogger<TodoController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var todos = _context.Todos.ToList();
            return View(todos);
        }

        [HttpPost]
        public IActionResult Create(string title)
        {
            if (!string.IsNullOrEmpty(title))
            {
                var todo = new Todo { Title = title, IsComplete = false };
                _context.Todos.Add(todo);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult ToggleComplete(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                todo.IsComplete = !todo.IsComplete;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
