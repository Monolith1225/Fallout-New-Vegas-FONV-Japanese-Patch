Imports System.Net
Imports System.IO
Imports System.Text
Imports System.Diagnostics
Public Class config_fonv
    Private mousePoint As Point
    Dim Root As String
    Private Sub config_fonv_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        'フォーム非表示にする
        Me.Visible = False
        Error_1.Enabled = False
        Error_2.Enabled = False
        Warning_TEX.Visible = False
    End Sub
    Private Sub CloseingButton_Click(sender As Object, e As EventArgs) Handles CloseingButton.Click
        'フォーム非表示にする
        Me.Visible = False
        Error_1.Enabled = False
        Error_2.Enabled = False
        Warning_TEX.Visible = False
    End Sub
    Private Sub config_fonv_MouseDown(sender As Object, e As MouseEventArgs) Handles MyBase.MouseDown
        If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
            mousePoint = New Point(e.X, e.Y)
        End If
    End Sub

    Private Sub config_fonv_MouseMove(sender As Object, e As MouseEventArgs) Handles MyBase.MouseMove
        If (e.Button And MouseButtons.Left) = MouseButtons.Left Then
            Me.Location = New Point(
                Me.Location.X + e.X - mousePoint.X,
                Me.Location.Y + e.Y - mousePoint.Y)
        End If
    End Sub
    Private Sub Error_1_Tick(sender As Object, e As EventArgs) Handles Error_1.Tick
        '演出
        Error_1.Enabled = False
        FalloutNV_VER_CLIENT.ForeColor = Color.White
        Warning_TEX.ForeColor = Color.Red
        Error_2.Enabled = True
    End Sub
    Private Sub Error_2_Tick(sender As Object, e As EventArgs) Handles Error_2.Tick
        '演出
        Error_2.Enabled = False
        FalloutNV_VER_CLIENT.ForeColor = Color.Red
        Warning_TEX.ForeColor = Color.Yellow
        Error_1.Enabled = True
    End Sub
    Private Sub Pre_release_button_Click(sender As Object, e As EventArgs) Handles Pre_release_button.Click
        'WEBREQUESTでバージョン取得する
        Try
            Dim ver_req As HttpWebRequest = DirectCast(
            WebRequest.Create("http://fallout.allplay.jp/download/fonv_jp/pre_ver.txt"), HttpWebRequest)
            Dim ver_res As HttpWebResponse = DirectCast(ver_req.GetResponse(), HttpWebResponse)
            Dim st As Stream = ver_res.GetResponseStream()
            Dim sr As New StreamReader(st, Encoding.UTF8)
            Dim ver_source As String = sr.ReadToEnd()
            ver_res.Close()

            'ソフトウェアバージョン [ Application.ProductVersion ]
            '現在のバージョンチェック
            If Application.ProductVersion = ver_source Then
                MessageBox.Show("現在: " & Application.ProductVersion & vbCr &
                            "WEB: " & ver_source & vbCr & vbCr &
                            "現在のバージョンは最新版です", "Version Checker",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                '新しいバージョンが見つかった場合(GITHUB)
                MessageBox.Show("新しいベータ版がリリースされています。詳しくはブログを確認して下さい", "New Version",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As System.Net.WebException
            'サーバエラー
            If ex.Status = System.Net.WebExceptionStatus.ProtocolError Then
                Dim err As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                MessageBox.Show("エラーコード: " & err.StatusCode & vbCr &
                                "説明: " & err.StatusDescription & vbCr & vbCr &
                                "サーバエラーが発生しました。正常に取得出来ません…")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub FinallyVersion_Click(sender As Object, e As EventArgs) Handles FinallyVersion.Click
        'WEBREQUESTでバージョン取得する
        Try
            Dim ver_req As HttpWebRequest = DirectCast(
            WebRequest.Create("http://fallout.allplay.jp/download/fonv_jp/ver.txt"), HttpWebRequest)
            Dim ver_res As HttpWebResponse = DirectCast(ver_req.GetResponse(), HttpWebResponse)
            Dim st As Stream = ver_res.GetResponseStream()
            Dim sr As New StreamReader(st, Encoding.UTF8)
            Dim ver_source As String = sr.ReadToEnd()
            ver_res.Close()

            'ソフトウェアバージョン [ Application.ProductVersion ]
            '現在のバージョンチェック
            If Application.ProductVersion = ver_source Then
                MessageBox.Show("現在: " & Application.ProductVersion & vbCr &
                            "WEB: " & ver_source & vbCr & vbCr &
                            "現在のバージョンは最新版です", "Version Checker",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                '新しいバージョンが見つかった場合(GITHUB)
                MessageBox.Show("新しいベータ版がリリースされています。詳しくはブログを確認して下さい", "New Version",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As System.Net.WebException
            'サーバエラー
            If ex.Status = System.Net.WebExceptionStatus.ProtocolError Then
                Dim err As HttpWebResponse = CType(ex.Response, HttpWebResponse)
                MessageBox.Show("エラーコード: " & err.StatusCode & vbCr &
                                "説明: " & err.StatusDescription & vbCr & vbCr &
                                "サーバエラーが発生しました。正常に取得出来ません…")
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub config_fonv_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'FalloutNV Version
        '但し設定
        Dim FalloutNV_EXE As String = mains.Path_pass.Text
        If FalloutNV_EXE = "" Then
            '点滅させる
            Error_1.Enabled = True
            Warning_TEX.Visible = True
        Else
            Try
                'バージョンを取得する
                Dim vi As FileVersionInfo = FileVersionInfo.GetVersionInfo(FalloutNV_EXE & "FalloutNV.EXE")
                FalloutNV_VER_CLIENT.ForeColor = Color.White
                FalloutNV_VER_CLIENT.Text = "FalloutNV Version:" & vbCr & vi.FileVersion
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
End Class