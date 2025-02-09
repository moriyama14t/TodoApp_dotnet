# Razor Pages における OnGet / OnPost の挙動

Razor Pages（ASP.NET Core）では、ページモデルに定義されたハンドラーメソッドが自動的にHTTPリクエストに対応して実行される。

- **OnGet メソッド**
  - HTTP GET リクエスト時に呼び出され、ページ表示のための初期化処理を行う。
- **OnPost メソッド**
  - HTTP POST リクエスト時に呼び出され、フォーム送信などの処理を実行。

## 基本例

```csharp
public class IndexModel : PageModel
{
    // GET リクエスト時に実行される
    public void OnGet()
    {
        // 例：ページの初期データの設定
    }

    // POST リクエスト時に実行される
    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            // バリデーションエラーの場合は同じページを再表示
            return Page();
        }
        
        // 例：データベース更新、ファイル処理などの処理
        
        // 処理完了後、別ページにリダイレクト
        return RedirectToPage("Index");
    }
}
```

## 重要なポイント
### フォームの設定
フォームタグの method="post" を指定することで、POST送信時に自動的に OnPost メソッドが呼ばれる。

### 名前付きハンドラー
複数のPOST処理を行いたい場合、例えば OnPostUpload や OnPostCancel のようにメソッド名を拡張し、フォーム送信ボタンに asp-page-handler="Upload" などを指定することで、特定のハンドラーを呼び出すことができる。

### 自動的なハンドラー選択
HTTPリクエストの種類（GETかPOST）に応じて、対応するハンドラー（OnGetまたはOnPost）が自動的に実行されるため、開発者は命名規則に従うだけで簡単にリクエスト処理を実装できます。

## Note
Razor Pages アプリケーションでは、Pages ディレクトリがデフォルトの場所として用意され、ページファイル（.cshtml）とその対応する PageModel ファイル（.cshtml.cs）がセットで管理される
一方、Views ディレクトリは主に MVC アーキテクチャでコントローラーと共に利用されるため、もし Razor Pages で開発する場合は Pages ディレクトリにファイルを作成するのが推奨されます。
