Option Strict On

''' <summary>
''' ファイル関連の処理を行う共通クラス
''' </summary>
''' <remarks></remarks>
Public Class FileFunction

    ''' <summary>
    ''' 実行ファイルがあるディレクトリを取得
    ''' </summary>
    ''' <returns>取得したディレクトリ</returns>
    Public Shared Function GetApplicationPath() As String
        Return System.IO.Path.GetDirectoryName( _
            System.Reflection.Assembly.GetExecutingAssembly().Location)
    End Function

    ''' <summary>
    ''' 実行ファイルと同じフォルダにあるテキストファイル(設定ファイル)の
    ''' 全内容を読み取って返す
    ''' </summary>
    ''' <param name="filename">読み取るファイル名</param>
    ''' <returns>読み取ったファイルの内容</returns>
    Public Shared Function GetCurrentTextFile(ByVal filename As String) As String
        Return _
            GetTextFile( _
                System.IO.Path.Combine( _
                    FileFunction.GetApplicationPath(), _
                    filename))
    End Function

    ''' <summary>
    ''' テキストファイルの全内容を読み取って返す
    ''' </summary>
    ''' <param name="filename">読み取るファイル名</param>
    ''' <returns>読み取ったファイルの内容</returns>
    Public Shared Function GetTextFile(ByVal filename As String) As String
        Dim sr As New System.IO.StreamReader( _
            filename, _
            System.Text.Encoding.GetEncoding("utf-8"))
        Dim text As String = sr.ReadToEnd()
        sr.Close()
        Return text
    End Function



End Class


