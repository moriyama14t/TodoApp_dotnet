// Microsoft.EntityFrameworkCore と TodoApp.Data 名前空間をインポート
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;

// Webアプリケーションのビルダーを作成し、設定やサービスの登録を開始
// args は、プログラムの起動時に渡されるコマンドライン引数を格納した文字列の配列
var builder = WebApplication.CreateBuilder(args);

// MVCパターンの「コントローラ」と「ビュー」を使用するためのサービスを登録
builder.Services.AddControllersWithViews();

// Razor Pages のサービスを登録
builder.Services.AddRazorPages();

// Entity Framework Coreのデータベースコンテキスト（TodoContext）を登録
// SQLiteをデータベースとして使用し、接続文字列に「TodoApp.db」を指定
builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite("Data Source=TodoApp.db"));

// 登録したサービスをもとにWebアプリケーションを構築
var app = builder.Build();

// 開発環境以外の場合のエラーハンドリングとセキュリティ設定
if (!app.Environment.IsDevelopment())
{
    // エラー発生時に /Home/Error へリダイレクトするエラーハンドラを設定
    app.UseExceptionHandler("/Home/Error");
    
    // HTTP Strict Transport Security (HSTS) を有効にし、セキュリティを強化
    app.UseHsts();
}

// HTTPリクエストを HTTPS にリダイレクトするミドルウェアを追加
app.UseHttpsRedirection();

// URL に基づいてリクエストを適切なエンドポイントに振り分けるためのルーティングミドルウェアを追加
app.UseRouting();

// 認可（アクセス権のチェック）を行うミドルウェアを追加
app.UseAuthorization();

// 静的ファイル（CSS、JavaScript、画像など）の配信設定（カスタムまたは拡張メソッド）
app.MapStaticAssets();

// MVCのルート設定を定義
// デフォルトルート：controller が指定されない場合は "todo"、action が指定されない場合は "Index"、
// オプションパラメータとして "id" を受け取る
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=todo}/{action=Index}/{id?}")
//    .WithStaticAssets();  // ルートに対して静的アセットの設定を追加（カスタム拡張メソッドの可能性あり）

// Razor Pages のエンドポイントをマッピングする
app.MapRazorPages();

// アプリケーションのリクエスト受信を開始し、実行状態に移行
app.Run();
