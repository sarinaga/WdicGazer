Imports System.Text.RegularExpressions

''' <summary>
''' 辞書ファイルの読み込み、検索を行う
''' </summary>
''' <remarks>このクラスは検索機能まで。詳細はWdicCutterで切り出し、WdicPaserで解析すること</remarks>
Public Class DicReader
    Inherits FileReader

#Region "定義"

    ''' <summary>
    ''' 検索条件
    ''' </summary>
    Protected Condition As SearchConditionData

    ''' <summary>
    ''' 検索時に利用する正規表現
    ''' </summary>
    Protected RegexPattern As Regex

    ''' <summary>
    ''' 正規表現による検索結果
    ''' </summary>
    Protected PatternMatch As Match

    ''' <summary>
    ''' 見出し語につけられるマーカー
    ''' </summary>
    Protected Const WordMarker As String = "#"

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="filename">DICファイルのフルパス</param>
    Public Sub New(ByVal filename As String, ByVal condition As SearchConditionData)
        MyBase.New(filename)
        Me.Condition = condition
        CorrectSearchText()
    End Sub

    ''' <summary>
    ''' 検索語を検索に利用するための正規表現に転換
    ''' </summary>
    Private Sub CorrectSearchText()

        Dim pattern As String
        Dim c As SearchConditionData = Condition
        If c.SearchMode = SearchConditionData.SearchModeType.Include Then
            If c.IsTextSearch Then
                ' 本文検索
                pattern = StringFunction.ExcapeRegex(c.SearchWord)
            Else
                ' 見出し語検索(含まれる)
                pattern = "\r\n" & WordMarker & ".*?" & StringFunction.ExcapeRegex(c.SearchWord) & ".*?" & "\r\n"
            End If

        ElseIf c.SearchMode = SearchConditionData.SearchModeType.Regex Then
            If c.IsTextSearch Then
                ' 本文検索(正規表現)
                pattern = c.SearchWord
            Else
                ' 見出し語検索(正規表現)
                pattern = c.SearchWord
                If pattern(pattern.Length - 1) = "$"c Then
                    pattern = pattern.Remove(pattern.Length - 1, 1) & "\r\n"
                Else
                    pattern &= ".*?\r\n"
                End If
                If pattern(0) = "^" Then
                    pattern = "\r\n" & WordMarker & pattern.Substring(1)
                Else
                    pattern = "\r\n" & WordMarker & ".*?" & pattern
                End If
            End If

        ElseIf c.SearchMode = SearchConditionData.SearchModeType.Prefix Then
            ' 見出し語検索(前方一致)
            pattern = "\r\n" & WordMarker & StringFunction.ExcapeRegex(c.SearchWord) & ".*?" & "\r\n"

        ElseIf c.SearchMode = SearchConditionData.SearchModeType.Suffix Then
            ' 見出し語検索(後方一致)
            pattern = "\r\n" & WordMarker & ".*?" & StringFunction.ExcapeRegex(c.SearchWord) & "\r\n"

        ElseIf c.SearchMode = SearchConditionData.SearchModeType.Perfect Then
            ' 見出し語検索(完全一致)
            pattern = "\r\n" & WordMarker & StringFunction.ExcapeRegex(c.SearchWord) & "\r\n"
        Else
            ' 不正
            Throw New UnjustProcessingException("正しい検索条件が指定されていない")
        End If

        ' 大文字小文字の区別
        Dim regexOption As System.Text.RegularExpressions.RegexOptions
        If c.IsCapitalCheck Then
            regexOption = System.Text.RegularExpressions.RegexOptions.None
        Else
            regexOption = System.Text.RegularExpressions.RegexOptions.IgnoreCase
        End If

        ' 正規表現の設定
        RegexPattern = New Regex(pattern, regexOption)

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 文字列検索処理(最初の1つ目)
    ''' </summary>
    ''' <returns>検索語の # がある位置</returns>
    ''' <remarks>検索語が見つからない場合は-1を返す</remarks>
    Public Function Match() As Integer
        If String.IsNullOrEmpty(ReadText) Then Read()
        PatternMatch = RegexPattern.Match(ReadText)
        If Not PatternMatch.Success Then Return -1
        Dim index As Integer = PatternMatch.Index
        Return ReviseIndex(index)
    End Function

    ''' <summary>
    ''' 文字列検索処理(次を検索)
    ''' </summary>
    ''' <returns>検索後の#がある位置</returns>
    ''' <remarks>先にMatchが実行されなかったときの動作は未定義。検索語が見つからない場合は-1を返す</remarks>
    Public Function NextMatch() As Integer
        If String.IsNullOrEmpty(ReadText) Then Read()
        PatternMatch = PatternMatch.NextMatch()
        If Not PatternMatch.Success Then Return -1
        Dim index As Integer = PatternMatch.Index
        Return ReviseIndex(index)
    End Function

    ''' <summary>
    ''' 検索した位置を検索条件に合わせて補正する
    ''' </summary>
    ''' <param name="index">補正される前の場所</param>
    ''' <returns>補正された後の場所</returns>
    ''' <remarks>不正があった場合は-1を返す</remarks>
    Private Function ReviseIndex(ByVal index As Integer) As Integer
        If Not Condition.IsTextSearch Then Return index + vbCrLf.Length
        Dim pos As Integer = ReadText.LastIndexOf(vbCrLf & WordMarker, index)
        If pos < 0 Then Return -1
        Return pos + vbCrLf.Length
    End Function

#End Region


End Class
