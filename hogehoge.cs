// webserver.cs

using System;
using System.IO;
using System.Net;

class SimpleWebServer
{
  static void Main()
  {
    string root = @"c:\wwwroot\"; // ドキュメント・ルート
    string prefix = "http://*/"; // 受け付けるURL

    HttpListener listener = new HttpListener();
    listener.Prefixes.Add(prefix); // プレフィックスの登録
    listener.Start();

    while (true) {
      HttpListenerContext context = listener.GetContext();
      HttpListenerRequest req = context.Request;
      HttpListenerResponse res = context.Response;

      Console.WriteLine(req.RawUrl);

      // リクエストされたURLからファイルのパスを求める
      string path = root + req.RawUrl.Replace("/", "\\");

      // ファイルが存在すればレスポンス・ストリームに書き出す
      if (File.Exists(path)) {
        byte[] content = File.ReadAllBytes(path);
        res.OutputStream.Write(content, 0, content.Length);
      }
      res.Close();
    }
  }
}

// コンパイル方法：csc webserver.cs