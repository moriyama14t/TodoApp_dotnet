# IActionResult とは？

`IActionResult` は、ASP.NET Core におけるアクションメソッドや Razor Pages のハンドラーメソッドが返す結果（レスポンス）を表すインターフェース。  
これにより、以下のようなさまざまなレスポンスを柔軟に返すことができる

## 主な特徴

- **目的**  
  アクションメソッドの実行結果を表現し、その結果を HTTP レスポンスとしてクライアントに返すための契約（コントラクト）を定義。

- **具体的な実装例**  
  `IActionResult` を実装するクラスには、以下のようなものがあります：
  - **ViewResult**: ビュー（HTMLページ）をレンダリングして返す
  - **PageResult**: Razor Pages の結果を返す（Razor Pages でよく使用）
  - **RedirectResult / RedirectToActionResult**: 別の URL へリダイレクトする
  - **JsonResult**: JSON データを返す
  - **StatusCodeResult**: 特定の HTTP ステータスコード（例：404、500 など）を返す

## 使用例

以下は、Razor Pages のハンドラーメソッドで `IActionResult` を返す例

```csharp
public IActionResult OnPostUpload()
{
    // 何らかの処理を実施する
    return Page(); // 同じページを再表示する場合
    // 例: return RedirectToPage("Index"); でリダイレクトすることも可能
}
