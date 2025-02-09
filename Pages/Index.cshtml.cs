using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TodoApp.Pages
{
    public class IndexModel : PageModel
    {
        // フォームからバインドするプロパティ
        [BindProperty]
        public string InputValue { get; set; } = "こんにちわ";

        // 結果メッセージを表示するためのプロパティ
        public string ResultMessage { get; set; } = string.Empty;

        // GET リクエスト時に実行される
        public IActionResult OnGet()
        {
            ResultMessage = "OnGet が実行されました。";
            return Page();
        }

        // デフォルトの POST ハンドラー
        public IActionResult OnPostAsync()
        {
            InputValue = 1000.ToString();
            Console.WriteLine("Default OnPost");
            Console.WriteLine($"入力値 = {InputValue}");
            ResultMessage = $"Default OnPost: 入力値 = {InputValue}";
            return Page();
        }

        // asp-page-handler="Upload" を指定した場合に実行されるハンドラー
        public IActionResult OnPostUploadAsync()
        {
            Console.WriteLine("OnPostUpload");
            Console.WriteLine($"入力値 = {InputValue}");
            ResultMessage = $"OnPostUpload が実行されました。入力値 = {InputValue}";
            return Page();
        }

        // asp-page-handler="Cancel" を指定した場合に実行されるハンドラー
        public IActionResult OnPostCancelAsync()
        {
            Console.WriteLine("OnPostCancel");
            Console.WriteLine($"入力値 = {InputValue}");
            ResultMessage = $"OnPostCancel が実行されました.";
            return Page();
        }
    }
}
