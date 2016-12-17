''' <summary>
''' 文字列関連の処理を行う共通クラス
''' </summary>
Public Class StringFunction

    ''' <summary>
    ''' Select構文用引用符エスケープ処理
    ''' </summary>
    ''' <param name="str">エスケープする前の文字列</param>
    ''' <returns>エスケープされたあとの文字列</returns>
    Public Shared Function EscapeQuote(ByVal str As String) As String
        Return str.Replace("'", "''")
    End Function

    ''' <summary>
    ''' 正規表現の特殊文字をエスケープする
    ''' </summary>
    ''' <param name="pattern">エスケープされる前の文字列</param>
    ''' <returns>エスケープ後の文字列</returns>
    Public Shared Function ExcapeRegex(ByVal pattern As String) As String
        Dim p As String = pattern
        p = p.Replace("\", "\\")
        p = p.Replace(".", "\.")
        p = p.Replace("$", "\$")
        p = p.Replace("^", "\^")
        p = p.Replace("[", "\[")
        p = p.Replace("(", "\(")
        p = p.Replace("|", "\|")
        p = p.Replace(")", "\)")
        p = p.Replace("]", "\]")
        p = p.Replace("*", "\*")
        p = p.Replace("+", "\+")
        p = p.Replace("?", "\?")
        Return p
    End Function

    ''' <summary>
    ''' WDICのエスケープ文字を解析する
    ''' </summary>
    ''' <param name="text">デコードされる前の文</param>
    ''' <returns>デコードされた後の分</returns>
    Public Shared Function DecodeBodyText(ByVal text As String) As String

        text = Transrate.DecodeDate(text)              ' 年月日変換
        text = Transrate.TimeTrans(text)                 ' 時間変換
        text = Transrate.DecodeUnits(text)                 ' 単位変換
        text = Transrate.SetRuby(text)                 ' ルビ挿入
        text = Transrate.LinkDelete(text)                ' リンク消去
        text = Transrate.DecodeNumberReference(text)               ' 数値参照
        text = Transrate.DecodeEntityReference(text)                 ' 文字参照
        text = Transrate.EraseUndefined(text)            ' 未定義コマンド除去
        text = Transrate.DecodeEscapeLetter(text)              ' 1文字エスケープ除去
        Return text

    End Function

    ''' <summary>
    ''' 指定された文字列内にある文字列が幾つあるか数える
    ''' </summary>
    ''' <param name="strInput">検索対象文字列</param>
    ''' <param name="strFind">数える文字列</param>
    ''' <param name="limit">どこまでの文字を数えるか</param>
    ''' <returns>strInput内にstrFindが幾つあったか</returns>
    Public Shared Function CountString( _
        ByVal strInput As String, _
        ByVal strFind As String, _
        Optional ByVal limit As Integer = -1) As Integer

        Dim foundCount As Integer = 0
        If limit < 0 Then limit = strInput.Length - strFind.Length
        Dim sPos As Integer = strInput.IndexOf(strFind)
        If limit < sPos Then Return 0
        While sPos > -1
            foundCount += 1
            sPos = strInput.IndexOf(strFind, sPos + 1)
            If limit < sPos Then Exit While
        End While
        Return foundCount
    End Function

    ''' <summary>
    ''' 簡易説明用文字列切り出し
    ''' </summary>
    ''' <param name="text">切り出す前の文字列</param>
    ''' <returns>切り出したあとの文字列</returns>
    Public Shared Function MakeShortDescription(ByVal text As String) As String
        If String.IsNullOrEmpty(text) Then Return ""
        Dim pos As Integer = text.IndexOf("。")
        If pos < 0 Then Return text
        Return text.Substring(0, pos + 1)
    End Function

End Class

