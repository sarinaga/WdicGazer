''' <summary>
''' TSVファイルを読み込む
''' </summary>
''' <remarks></remarks>
Public Class TsvReader
    Inherits FileReader

#Region "定義"

    ''' <summary>
    ''' 読み取ったTSVを格納
    ''' </summary>
    Protected Matrix()() As String

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 読み取ったTSVファイルの内容を返す
    ''' </summary>
    ''' <returns>読み取り済のTSVファイル</returns>
    Public ReadOnly Property TsvMatrix() As String()()
        Get
            Return Matrix
        End Get
    End Property

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="filename">読み取るTSV形式のファイル</param>
    Public Sub New(ByVal filename As String)
        MyBase.New(filename)
    End Sub

#End Region

#Region "処理"

    ''' <summary>
    ''' TSV読み取り処理
    ''' </summary>
    Public Sub Parse()
        If ReadText Is Nothing Then Read()
        Dim lines As String() = Split(ReadText, vbCrLf)
        ReadText = Nothing

        ' 実際に存在する行を数える
        Dim i As Integer = 0
        For Each line As String In lines
            If line.Length > 0 Then i += 1
        Next

        ' 多次元配列に格納する
        Matrix = New String(i - 1)() {}
        i = 0
        For Each line As String In lines
            If line.Length = 0 Then Continue For
            Dim cells() As String = Split(line, vbTab)
            Matrix(i) = New String(cells.Length - 1) {}
            Dim j As Integer = 0
            For Each cell As String In cells
                Matrix(i)(j) = cell.Trim
                j += 1
            Next
            i += 1
        Next
    End Sub

#End Region


End Class
