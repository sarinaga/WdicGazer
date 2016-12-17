''' <summary>
''' 読み込んだDV6ファイルから必要な単語の部分だけを切り取るクラス
''' </summary>
Public Class WdicCutter

#Region "定義"

    ''' <summary>
    ''' 読み取った辞書ファイル全体のテキスト
    ''' </summary>
    Private ReadText As String

#Region "定数"

    ''' <summary>
    ''' 例外送出用文字列１
    ''' </summary>
    Private Const InvalidStartPos As String = "startPosの位置が正しくない"

    ''' <summary>
    ''' 例外送出用文字列２
    ''' </summary>
    Private Const NotFoundElementsTerminal As String = "項目の最後が見つからない(一般には辞書が不正)"

    ''' <summary>
    ''' 単語につけられているマーカー
    ''' </summary>
    Private Const WordMarker As Char = "#"c


    ''' <summary>
    ''' 区切り線用の文字
    ''' </summary>
    Private Const SeparateLetter As Char = "-"c

#End Region

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' 解析するWDIC文字列を指定-テキスト
    ''' </summary>
    ''' <param name="readText">読み取ったファイル</param>
    Public Sub New(ByVal readText As String)
        Me.ReadText = readText
    End Sub

    ''' <summary>
    ''' 解析するWDIC文字列を指定-WdicReaderBase
    ''' </summary>
    ''' <param name="reader">Read()を実行した後のWdicReaderBase</param>
    Public Sub New(ByRef reader As FileReader)
        Me.ReadText = reader.AllText
    End Sub

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 解析中のWDICファイル内容
    ''' </summary>
    ''' <returns>WDICファイル内容</returns>
    Public ReadOnly Property Text() As String
        Get
            Return ReadText
        End Get
    End Property

#End Region

#Region "メソッド"

    ''' <summary>
    ''' # のある位置を指定してそこから記事の最後までの文字列を取得
    ''' </summary>
    ''' <param name="startPos">その単語の # のある位置</param>
    ''' <returns>切り出された文字列</returns>
    Public Function Cut(ByVal startPos As Integer) As String

        ' 値の妥当性チェック
        If startPos < 0 Then Throw New ArgumentException(InvalidStartPos)
        If Not ReadText(startPos) = WordMarker Then Throw New ArgumentException(InvalidStartPos)
        If startPos > 0 Then
            If Not ReadText(startPos - 1) = vbLf Then
                Throw New ArgumentException(InvalidStartPos)
            End If
        End If

        ' 項目の最後を探す
        Dim endPos, endPos1, endPos2, endPos3 As Integer
        endPos1 = ReadText.IndexOf(vbCrLf & WordMarker, startPos)
        endPos2 = ReadText.IndexOf(vbCrLf & vbCrLf, startPos)
        endPos3 = ReadText.IndexOf(vbCrLf & SeparateLetter, startPos)
        If endPos1 < 0 And endPos2 < 0 And endPos3 < 0 Then
            Throw New FormatException(NotFoundElementsTerminal)
        End If
        If endPos3 < endPos1 Then endPos1 = endPos3
        If endPos1 < 0 Then
            endPos = endPos2
        ElseIf endPos2 < 0 Then
            endPos = endPos1
        ElseIf endPos1 >= endPos2 Then
            endPos = endPos2
        Else
            endPos = endPos1
        End If
        Return ReadText.Substring(startPos, endPos - startPos)

    End Function



#End Region

End Class