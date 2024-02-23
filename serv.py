import http.server
import socketserver

# ポート番号を指定
PORT = 8000

# 簡易HTTPサーバーのハンドラクラス
class MyHTTPRequestHandler(http.server.SimpleHTTPRequestHandler):
    # ファイルの場所を指定
    def __init__(self, *args, **kwargs):
        super().__init__(*args, directory=".", **kwargs)

# サーバーの設定
with socketserver.TCPServer(("", PORT), MyHTTPRequestHandler) as httpd:
    print("サーバーがポート", PORT, "で実行中...")
    # クライアントからのリクエストを待ち続ける
    httpd.serve_forever()