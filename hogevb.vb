' webserver.vb

Imports System
Imports System.IO
Imports System.Net

Class SimpleWebServer
  Shared Sub Main()
    Dim root As String = "c:\wwwroot\" ' ドキュメント・ルート
    Dim prefix As String = "http://*/" ' 受け付けるURL

    Dim listener As New HttpListener()
    listener.Prefixes.Add(prefix) ' プレフィックスの登録
    listener.Start()

    While (True)
      Dim context As HttpListenerContext = listener.GetContext()
      Dim req As HttpListenerRequest = context.Request
      Dim res As HttpListenerResponse = context.Response

      Console.WriteLine(req.RawUrl)

      ' リクエストされたURLからファイルのパスを求める
      Dim path As String = root & req.RawUrl.Replace("/", "\\")

      ' ファイルが存在すればレスポンス・ストリームに書き出す
      If File.Exists(path) Then
        Dim content() As Byte = File.ReadAllBytes(path)
        res.OutputStream.Write(content, 0, content.Length)
      End If

      res.Close()
    End While
  End Sub
End Class

' コンパイル方法：vbc webserver.vb