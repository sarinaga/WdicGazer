''' <summary>
''' 辞書ファイル読み込みの基本クラス
''' </summary>
''' <remarks></remarks>
Public Class FileReader

#Region "定義"

    ''' <summary>
    ''' 読み込むファイル名
    ''' </summary>
    ''' <remarks></remarks>
    Protected ReadFilename As String = Nothing

    ''' <summary>
    ''' 読み込んだファイル内容
    ''' </summary>
    ''' <remarks></remarks>
    Protected ReadText As String = Nothing

#End Region

#Region "コンストラクタ"
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="filename">読み込むファイル名</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal filename As String)
        If String.IsNullOrEmpty(filename) Then Throw New System.ArgumentNullException()
        ReadFilename = filename
    End Sub

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 読み込みファイル名
    ''' </summary>
    ''' <returns>読み込みファイル名</returns>
    Public ReadOnly Property Filename() As String
        Get
            Return ReadFilename
        End Get
    End Property

    ''' <summary>
    ''' 読み込み済ファイル全文
    ''' </summary>
    ''' <returns>ファイル全内容(Readメソッド実行後に有効)</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property AllText() As String
        Get
            Return ReadText
        End Get
    End Property

#End Region

#Region "メソッド"

    ''' <summary>
    ''' テキストファイル全内容読み込み
    ''' </summary>
    Public Sub Read()
        Dim sr As New System.IO.StreamReader(ReadFilename, System.Text.Encoding.UTF8)
        ReadText = sr.ReadToEnd
        sr.Close()
    End Sub

    ''' <summary>
    ''' 読み取ったテキストの廃棄
    ''' </summary>
    Public Sub Clear()
        ReadText = Nothing
    End Sub

#End Region

End Class
