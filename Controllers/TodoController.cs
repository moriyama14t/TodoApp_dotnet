using Microsoft.AspNetCore.Mvc;  // ASP.NET Core MVC の機能を利用するための名前空間
using TodoApp.Data;              // TodoApp のデータアクセス関連（TodoContext など）の名前空間
using TodoApp.Models;            // TodoApp のモデル（Todo クラスなど）の名前空間

namespace TodoApp.Controllers  // TodoApp のコントローラをまとめる名前空間
{
    // TodoController クラスは Controller を継承し、Todo に関するアクションを定義する
    public class TodoController : Controller
    {
        // データベースアクセス用の TodoContext を DI（依存性注入）により受け取るフィールド
        private readonly TodoContext _context;

        // ログ出力用の ILogger を DI により受け取るフィールド（TodoController 用）
        private readonly ILogger<TodoController> _logger;

        // コンストラクタ
        // TodoContext と ILogger<TodoController> が依存性注入によって提供される
        public TodoController(TodoContext context, ILogger<TodoController> logger)
        {
            _context = context;   // DI された TodoContext をフィールドに保持
            _logger = logger;     // DI された Logger をフィールドに保持
        }

        // GET: Todo/Index もしくは /Todo で呼び出されるアクション
        public IActionResult Index()
        {
            // データベースから全ての Todo をリストとして取得
            var todos = _context.Todos.ToList();
            // 取得した Todo リストをビューに渡して表示する
            return View(todos);
        }

        // POST: Todo/Create アクション
        // フォームから送信された title を受け取り、新しい Todo を作成する
        [HttpPost]
        public IActionResult Create(string title)
        {
            // デバッグ用のコンソール出力
            Console.WriteLine("_logger");
            Console.WriteLine(_logger);
            Console.WriteLine("title");
            Console.WriteLine(title);

            // 入力された title が空文字または null でないかチェック
            if (!string.IsNullOrEmpty(title))
            {
                // 新しい Todo オブジェクトを作成（初期状態は未完了）
                var todo = new Todo { Title = title, IsComplete = false };
                // 新しい Todo をデータベースに追加
                _context.Todos.Add(todo);
                // 変更内容をデータベースに保存
                _context.SaveChanges();
            }
            // 処理が完了したら、Index アクションにリダイレクトし、Todo リストを再表示する
            return RedirectToAction("Index");
        }

        // Todo の完了状態を切り替えるアクション
        // id を受け取り、該当する Todo の IsComplete を反転する
        public IActionResult ToggleComplete(int id)
        {
            // 指定された id に対応する Todo をデータベースから検索
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                // Todo が存在する場合、完了状態を反転（true ⇔ false）する
                todo.IsComplete = !todo.IsComplete;
                // 変更をデータベースに保存
                _context.SaveChanges();
            }
            // 処理完了後、Index アクションにリダイレクトし、最新の Todo リストを表示する
            return RedirectToAction("Index");
        }

        // Todo を削除するアクション
        // id を受け取り、該当する Todo をデータベースから削除する
        public IActionResult Delete(int id)
        {
            // 指定された id に対応する Todo をデータベースから検索
            var todo = _context.Todos.Find(id);
            if (todo != null)
            {
                // Todo が存在する場合、データベースから削除する
                _context.Todos.Remove(todo);
                // 変更をデータベースに保存
                _context.SaveChanges();
            }
            // 処理完了後、Index アクションにリダイレクトし、最新の Todo リストを表示する
            return RedirectToAction("Index");
        }
    }
}
