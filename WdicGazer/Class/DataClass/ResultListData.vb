''' <summary>
''' 検索された結果を格納するクラス
''' </summary>
''' <remarks></remarks>
Public Class ResultListData

#Region "定義"

    ''' <summary>
    ''' 検索結果一覧を格納するDataTable
    ''' </summary>
    Protected List As New DataTable

    ''' <summary>
    ''' 検索結果一覧を格納するテーブル名
    ''' </summary>
    Public Const TableName As String = "ResultList"

    ''' <summary>
    ''' 列名：辞書種類名(辞書大分類に対応)
    ''' </summary>
    Public Const Col_TypeName As String = "TypeName"

    ''' <summary>
    ''' 列名：辞書大分類
    ''' </summary>
    Public Const Col_TypeLarge As String = "TypeLarge"

    ''' <summary>
    ''' 列名：辞書小分類
    ''' </summary>
    Public Const Col_TypeSmall As String = "TypeSmall"

    ''' <summary>
    ''' 列名：単語
    ''' </summary>
    Public Const Col_Word As String = "Word"

    ''' <summary>
    ''' 列名：簡易説明
    ''' </summary>
    Public Const Col_Description As String = "Description"

    ''' <summary>
    ''' 列名：カテゴリ種類名
    ''' </summary>
    Public Const Col_DirName As String = "DirName"

    ''' <summary>
    ''' 列名：カテゴリ
    ''' </summary>
    Public Const Col_DirLarge As String = "DirLarge"

    ''' <summary>
    ''' 列名：ジャンル
    ''' </summary>
    Public Const Col_DirSmall As String = "DirSmall"

    ''' <summary>
    ''' 列名：品詞
    ''' </summary>
    Public Const Col_Pos As String = "Pos"

    ''' <summary>
    ''' 列名：日本語読み
    ''' </summary>
    Public Const Col_Yomi As String = "Yomi"

    ''' <summary>
    ''' 列名：単語(外語)
    ''' </summary>
    Public Const Col_Spell As String = "Spell"

    ''' <summary>
    ''' 列名：略語
    ''' </summary>
    Public Const Col_Abbr As String = "Abbr"

    ''' <summary>
    ''' 列名：外語読み
    ''' </summary>
    Public Const Col_Pron As String = "Pron"

    ''' <summary>
    ''' 列名：ファイル名
    ''' </summary>
    Public Const Col_Filename As String = "Filename"

    ''' <summary>
    ''' 列名：ファイル名(フルパス)
    ''' </summary>
    Public Const Col_FullPath As String = "FullPath"

    ''' <summary>
    ''' 列名：行番号
    ''' </summary>
    ''' <remarks></remarks>
    Public Const Col_LineNumber As String = "LineNumber"

    ''' <summary>
    ''' 列名：場所
    ''' </summary>
    ''' <remarks></remarks>
    Public Const Col_Position As String = "Position"

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        Dim dt As DataTable = New DataTable(TableName)
        dt.CaseSensitive = True
        dt.Columns.Add(Col_TypeName)
        dt.Columns.Add(Col_TypeLarge)
        dt.Columns.Add(Col_TypeSmall)
        dt.Columns.Add(Col_Word)
        dt.Columns.Add(Col_Description)
        dt.Columns.Add(Col_DirName)
        dt.Columns.Add(Col_DirLarge)
        dt.Columns.Add(Col_DirSmall)
        dt.Columns.Add(Col_Pos)
        dt.Columns.Add(Col_Yomi)
        dt.Columns.Add(Col_Spell)
        dt.Columns.Add(Col_Abbr)
        dt.Columns.Add(Col_Pron)
        dt.Columns.Add(Col_Filename)
        dt.Columns.Add(Col_FullPath)
        dt.Columns.Add(Col_LineNumber)
        dt.Columns.Add(Col_Position)
        List = dt

    End Sub

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 検索結果が格納されたDataTable
    ''' </summary>
    ''' <returns>検索結果</returns>
    ''' <remarks></remarks>
    Public ReadOnly Property ResultList() As DataTable
        Get
            Return List
        End Get
    End Property

#End Region

End Class
