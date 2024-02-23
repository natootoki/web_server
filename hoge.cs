using System;
using System.IO;
using System.Net;

class Program
{
    static void Main(string[] args)
    {
        // ポート番号を指定
        const int PORT = 8000;

        // HttpListenerのインスタンス化
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add("http://+:" + PORT + "/"); // ポートの指定

        try
        {
            // Webサーバーの開始
            listener.Start();
            Console.WriteLine("サーバーがポート" + PORT + "で実行中...");

            // リクエストの待機と処理
            while (true)
            {
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;

                // index.htmlの読み込み
                string filePath = Path.Combine(Environment.CurrentDirectory, "index.html");
                string htmlContent = File.ReadAllText(filePath);

                // レスポンスの設定
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(htmlContent);
                response.ContentLength64 = buffer.Length;
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("エラーが発生しました: " + e.Message);
        }
        finally
        {
            // Webサーバーの停止
            listener.Stop();
        }
    }
}