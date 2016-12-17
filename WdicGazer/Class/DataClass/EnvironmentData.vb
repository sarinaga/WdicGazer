Imports System.Data

''' <summary>
''' 環境情報・辞書情報を保持・検索するクラス
''' </summary>
''' <remarks>
''' 1. エラーメッセージ一覧
''' 2. 言語一覧
''' 3. 国名一覧
''' 4. 年号一覧
''' 5. 文字参照一覧
''' *. 通信用語の基礎知識の情報
''' 7. グループ一覧
''' 8. 辞書・グループ一覧(WLFファイル一覧)
''' 9. 辞書・ファイル一覧
''' 10. プラグイン・グループ一覧(PLFファイル一覧)
''' 11. プラグイン・ファイル一覧
''' 12. カテゴリ一覧
'''  
''' 以上、11データをDataTable形式で保存
''' データを検索して取得するメソッドもここに用意する
''' </remarks>
Public Class EnvironmentData

#Region "定義"

    ''' <summary>
    ''' データが完全に読み取れているかどうかのフラグ
    ''' </summary>
    Protected _IsDataComplete As Boolean = False

    ''' <summary>
    ''' データセット本体
    ''' </summary>
    Protected EnvironmentDataSet As New DataSet

    ''' <summary>
    ''' データセット名
    ''' </summary>
    Public Const DataSetName As String = "Wdic"

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' error_messages
        Dim dt1 As DataTable = EnvironmentDataSet.Tables.Add("error_messages")
        dt1.CaseSensitive = True
        dt1.Columns.Add("id", Type.GetType("System.Int32"))
        dt1.Columns.Add("code", Type.GetType("System.String"))
        dt1.Columns.Add("text", Type.GetType("System.String"))
        dt1.PrimaryKey = New DataColumn() {dt1.Columns("id")}

        ' language_codes
        Dim dt2 As DataTable = EnvironmentDataSet.Tables.Add("language_codes")
        dt2.CaseSensitive = True
        dt2.Columns.Add("id", Type.GetType("System.Int32"))
        dt2.Columns.Add("code2", Type.GetType("System.String"))
        dt2.Columns.Add("code3", Type.GetType("System.String"))
        dt2.Columns.Add("english", Type.GetType("System.String"))
        dt2.Columns.Add("japanese", Type.GetType("System.String"))
        dt2.PrimaryKey = New DataColumn() {dt2.Columns("id")}

        ' country_codes
        Dim dt3 As DataTable = EnvironmentDataSet.Tables.Add("country_codes")
        dt3.CaseSensitive = True
        dt3.Columns.Add("id", Type.GetType("System.Int32"))
        dt3.Columns.Add("code2", Type.GetType("System.String"))
        dt3.Columns.Add("code3", Type.GetType("System.String"))
        dt3.Columns.Add("english", Type.GetType("System.String"))
        dt3.Columns.Add("japanese", Type.GetType("System.String"))
        dt3.PrimaryKey = New DataColumn() {dt3.Columns("id")}

        ' eras
        Dim dt4 As DataTable = EnvironmentDataSet.Tables.Add("eras")
        dt4.CaseSensitive = False
        dt4.Columns.Add("id", Type.GetType("System.Int32"))
        dt4.Columns.Add("start_date", Type.GetType("System.DateTime"))
        dt4.Columns.Add("name", Type.GetType("System.String"))
        dt4.PrimaryKey = New DataColumn() {dt4.Columns("id")}

        ' entities
        Dim dtd As DataTable = EnvironmentDataSet.Tables.Add("entities")
        dtd.CaseSensitive = True
        dtd.Columns.Add("id", Type.GetType("System.Int32"))
        dtd.Columns.Add("name", Type.GetType("System.String"))
        dtd.Columns.Add("letter", Type.GetType("System.String"))
        dtd.PrimaryKey = New DataColumn() {dtd.Columns("id")}

        ' wdics
        Dim dtb As DataTable = EnvironmentDataSet.Tables.Add("wdics")
        dtb.CaseSensitive = True
        dtb.Columns.Add("id", Type.GetType("System.Int32"))
        dtb.Columns.Add("name", Type.GetType("System.String"))
        dtb.Columns.Add("edition", Type.GetType("System.String"))
        dtb.Columns.Add("contacter", Type.GetType("System.String"))
        dtb.Columns.Add("email", Type.GetType("System.String"))
        dtb.PrimaryKey = New DataColumn() {dtb.Columns("id")}

        ' groups
        Dim dt5 As DataTable = EnvironmentDataSet.Tables.Add("groups")
        dt5.CaseSensitive = False
        dt5.Columns.Add("id", Type.GetType("System.Int32"))
        dt5.Columns.Add("type", Type.GetType("System.String"))
        dt5.Columns.Add("wlf_file", Type.GetType("System.String"))
        dt5.Columns.Add("dic_name", Type.GetType("System.String"))
        dt5.PrimaryKey = New DataColumn() {dt5.Columns("id")}

        ' wlf_files
        Dim dt7 As DataTable = EnvironmentDataSet.Tables.Add("wlf_files")
        dt7.CaseSensitive = False
        dt7.Columns.Add("id", Type.GetType("System.Int32"))
        dt7.Columns.Add("group_id", Type.GetType("System.Int32"))
        dt7.Columns.Add("name", Type.GetType("System.String"))
        dt7.Columns.Add("filename", Type.GetType("System.String"))
        dt7.Columns.Add("path", Type.GetType("System.String"))
        dt7.Columns.Add("content", Type.GetType("System.String"))
        dt7.Columns.Add("contacter", Type.GetType("System.String"))
        dt7.Columns.Add("email", Type.GetType("System.String"))
        dt7.PrimaryKey = New DataColumn() {dt7.Columns("id")}

        ' dic_files
        Dim dt8 As DataTable = EnvironmentDataSet.Tables.Add("dic_files")
        dt8.CaseSensitive = False
        dt8.Columns.Add("id", Type.GetType("System.Int32"))
        dt8.Columns.Add("wlf_file_id", Type.GetType("System.Int32"))
        dt8.Columns.Add("filename", Type.GetType("System.String"))
        dt8.Columns.Add("path", Type.GetType("System.String"))
        dt8.Columns.Add("kind", Type.GetType("System.String"))
        dt8.Columns.Add("description", Type.GetType("System.String"))
        dt8.PrimaryKey = New DataColumn() {dt8.Columns("id")}

        ' plf_files
        Dim dtc As DataTable = EnvironmentDataSet.Tables.Add("plf_files")
        dtc.CaseSensitive = False
        dtc.Columns.Add("id", Type.GetType("System.Int32"))
        dtc.Columns.Add("group_id", Type.GetType("System.Int32"))
        dtc.Columns.Add("filename", Type.GetType("System.String"))
        dtc.Columns.Add("path", Type.GetType("System.String"))
        dtc.Columns.Add("name", Type.GetType("System.String"))
        dtc.Columns.Add("content", Type.GetType("System.String"))
        dtc.Columns.Add("contacter", Type.GetType("System.String"))
        dtc.Columns.Add("email", Type.GetType("System.String"))
        dtc.PrimaryKey = New DataColumn() {dtc.Columns("id")}

        ' plugin_files
        Dim dta As DataTable = EnvironmentDataSet.Tables.Add("plugin_files")
        dta.CaseSensitive = False
        dta.Columns.Add("id", Type.GetType("System.Int32"))
        dta.Columns.Add("plf_file_id", Type.GetType("System.Int32"))
        dta.Columns.Add("filename", Type.GetType("System.String"))
        dta.Columns.Add("path", Type.GetType("System.String"))
        dta.Columns.Add("name", Type.GetType("System.String"))
        dta.Columns.Add("author", Type.GetType("System.String"))
        dta.Columns.Add("description", Type.GetType("System.String"))
        dta.PrimaryKey = New DataColumn() {dta.Columns("id")}

        ' categories
        Dim dt9 As DataTable = EnvironmentDataSet.Tables.Add("categories")
        dt9.CaseSensitive = True
        dt9.Columns.Add("id", Type.GetType("System.Int32"))
        dt9.Columns.Add("directory", Type.GetType("System.String"))
        dt9.Columns.Add("redirect", Type.GetType("System.String"))
        dt9.Columns.Add("name", Type.GetType("System.String"))
        dt9.PrimaryKey = New DataColumn() {dt9.Columns("id")}

    End Sub

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' データが完全であるかどうかを取得
    ''' </summary>
    ''' <returns>WdicDataが完全に読み取れた状態であるかどうか</returns>
    Public Property IsDataComplete() As Boolean
        Get
            Return _IsDataComplete
        End Get
        Set(ByVal value As Boolean)
            _IsDataComplete = value
        End Set
    End Property

    ''' <summary>
    ''' エラーメッセージ一覧テーブル
    ''' </summary>
    Public ReadOnly Property ErrorMessageTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("error_messages")
        End Get
    End Property

    ''' <summary>
    ''' 言語コードテーブル
    ''' </summary>
    Public ReadOnly Property LangCodeTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("language_codes")
        End Get
    End Property

    ''' <summary>
    ''' 国コードテーブル
    ''' </summary>
    Public ReadOnly Property ConturyCodeTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("country_codes")
        End Get
    End Property

    ''' <summary>
    ''' 年号テーブル
    ''' </summary>
    Public ReadOnly Property EraTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("eras")
        End Get
    End Property

    ''' <summary>
    ''' 実体参照テーブル
    ''' </summary>
    Public ReadOnly Property EntityTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("entities")
        End Get
    End Property

    ''' <summary>
    ''' 通信用語の基礎知識情報テーブル
    ''' </summary>
    Public ReadOnly Property WdicTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("wdics")
        End Get
    End Property

    ''' <summary>
    ''' グループテーブル
    ''' </summary>
    Public ReadOnly Property GroupTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("groups")
        End Get
    End Property

    ''' <summary>
    ''' 辞書グループテーブル
    ''' </summary>
    Public ReadOnly Property WlfFileTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("wlf_files")
        End Get
    End Property

    ''' <summary>
    ''' 辞書ファイルテーブル
    ''' </summary>
    Public ReadOnly Property DicFileTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("dic_files")
        End Get
    End Property

    ''' <summary>
    ''' プラグイングループテーブル
    ''' </summary>
    Public ReadOnly Property PlfFileTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("plf_files")
        End Get
    End Property

    ''' <summary>
    ''' プラグインファイルテーブル
    ''' </summary>
    Public ReadOnly Property PluginFileTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("plugin_files")
        End Get
    End Property

    ''' <summary>
    ''' カテゴリリストテーブル
    ''' </summary>
    Public ReadOnly Property CategoryTable() As DataTable
        Get
            Return EnvironmentDataSet.Tables("categories")
        End Get
    End Property

#End Region

#Region "情報取得(ファイル名系)"

    ''' <summary>
    ''' 辞書グループIDからWLFファイル名のフルパスを返す
    ''' </summary>
    ''' <param name="id">wlfファイルのID</param>
    ''' <returns>WLFファイル名</returns>
    ''' <remarks>取得できなかったときはNothing</remarks>
    Public Function GetWlfFilenameFromId(ByVal id As Integer) As String
        Dim drs() As DataRow = WlfFileTable.Select(String.Format("{0}='{1}'", "id", id))
        If Not drs.Length = 1 Then Return Nothing
        Return System.IO.Path.Combine(drs(0).Item("path").ToString, drs(0).Item("filename").ToString)
    End Function

    ''' <summary>
    ''' 辞書ファイルIDから辞書ファイルのフルパスを入手
    ''' </summary>
    ''' <param name="id">辞書ファイルのID</param>
    ''' <returns>取得したファイル名</returns>
    ''' <remarks>不正な場合はNothing</remarks>
    Public Function GetDicFilenameFromId(ByVal id As Integer) As String
        Dim drs As DataRow() = DicFileTable.Select(String.Format("{0}={1}", "id", id))
        If Not drs.Length = 1 Then Return Nothing
        Return System.IO.Path.Combine(drs(0).Item("path").ToString, drs(0).Item("filename").ToString)
    End Function

    ''' <summary>
    ''' プラグイングループファイルIDからPLFファイルのフルパスを返す
    ''' </summary>
    ''' <param name="id">plfファイルのID</param>
    ''' <returns>PLFファイルを返す</returns>
    ''' <remarks>取得できなかったときはNothing</remarks>
    Public Function GetPlfFilenameFromId(ByVal id As Integer) As String
        Dim drs As DataRow() = PlfFileTable.Select(String.Format("{0}={1}", "id", id))
        If Not drs.Length = 1 Then Return Nothing
        Return System.IO.Path.Combine(drs(0).Item("path").ToString, drs(0).Item("filename").ToString)
    End Function

    ''' <summary>
    ''' プラグインファイルIDからプラグインファイルのフルパスを返す
    ''' </summary>
    ''' <param name="id">プラグインファイルID</param>
    ''' <returns>プラグインファイル</returns>
    ''' <remarks>取得できなかったときはNothing</remarks>
    Public Function GetPluginFileFromId(ByVal id As Integer) As String
        Dim drs As DataRow() = PluginFileTable.Select(String.Format("{0}={1}", "id", id))
        If Not drs.Length = 1 Then Return Nothing
        Return System.IO.Path.Combine(drs(0).Item("path").ToString, drs(0).Item("filename").ToString)
    End Function

    ''' <summary>
    ''' プラグインファイルをフルパスにして返す
    ''' </summary>
    ''' <param name="filename">プラグインのファイル名</param>
    ''' <param name="group_id">そのファイルが所属しているgroup_id(実際には未利用)</param>
    ''' <returns>プラグインファイルのプルパス</returns>
    ''' <remarks>
    ''' 1. 取得できなかったときはNothing
    ''' 2. PLFファイルが正式に採用されたら変更されることに注意
    ''' </remarks>
    Public Function GetPluginFullpathFromFilename(ByVal filename As String, ByVal group_id As Integer) As String
        Dim drs() As DataRow = PluginFileTable.Select(String.Format("{0}='{1}'", "filename", filename))
        If Not drs.Length = 1 Then Return Nothing
        Return System.IO.Path.Combine(drs(0).Item("path").ToString, drs(0).Item("filename").ToString)
    End Function

#End Region

#Region "情報取得(グループ系)"

    ''' <summary>
    ''' グループIDから辞書種類略称を取得する
    ''' </summary>
    ''' <param name="group_id">グループID</param>
    ''' <returns>辞書種類略称</returns>
    ''' <remarks>取得できなかった場合はNothing</remarks>
    Public Function GetDicTypeFromGroupId(ByVal group_id As Integer) As String
        Dim drs() As DataRow = GroupTable.Select(String.Format("{0}={1}", "id", group_id))
        If Not drs.Length = 1 Then Return Nothing
        Return drs(0).Item("type").ToString
    End Function

    ''' <summary>
    ''' グループIDから辞書名を取得する
    ''' </summary>
    ''' <param name="group_id">グループID</param>
    ''' <returns>辞書名</returns>
    ''' <remarks>取得できなかった場合はNothing</remarks>
    Public Function GetDicNameFromGroupId(ByVal group_id As Integer) As String
        Dim drs() As DataRow = WlfFileTable.Select(String.Format("{0}={1}", "group_id", group_id))
        If Not drs.Length = 1 Then Return Nothing
        Return drs(0).Item("name").ToString
    End Function

    ''' <summary>
    ''' 辞書種類略称からグループIDを取得する
    ''' </summary>
    ''' <param name="type">辞書種類</param>
    ''' <returns>取得したグループID</returns>
    ''' <remarks>取得できなかった場合はマイナスの値</remarks>
    Public Function GetGroupNoFromTypeName(ByVal type As String) As Integer
        Dim drs() As DataRow = GroupTable.Select(String.Format("{0}='{1}'", "type", StringFunction.EscapeQuote(type).ToUpper))
        If Not drs.Length = 1 Then Return -1
        Return CInt(drs(0).Item("id"))
    End Function

#End Region

#Region "情報取得(コード系)"

    ''' <summary>
    ''' エラーコードからエラーメッセージを返す
    ''' </summary>
    ''' <param name="code">エラーコード</param>
    ''' <returns>エラーメッセージ</returns>
    Public Function ErrorMessage(ByVal code As String) As String
        Dim drs() As DataRow = ErrorMessageTable.Select(String.Format("{0}='{1}'", "code", code))
        If Not drs.Length = 1 Then Return "Undefined Error"
        Return drs(0).Item("text").ToString
    End Function

    ''' <summary>
    ''' 言語コードから言語名を取得する
    ''' </summary>
    ''' <param name="code">言語コード</param>
    ''' <returns>取得した言語名</returns>
    ''' <remarks>取得できない場合は何らかのエラーメッセージ</remarks>
    Public Function GetLangNameFromCode(ByVal code As String) As String
        Dim drs As DataRow()
        If code.Length = 2 Then
            drs = LangCodeTable.Select(String.Format("{0}='{1}'", "code2", code))
        ElseIf code.Length = 3 Then
            drs = LangCodeTable.Select(String.Format("{0}='{1}'", "code3", code))
        Else
            Return "(Error)"
        End If
        If Not drs.Length = 1 Then Return "(Unknown)"
        Return drs(0).Item("japanese").ToString
    End Function

    ''' <summary>
    ''' 国コードから国名を取得する
    ''' </summary>
    ''' <param name="code">国名コード</param>
    ''' <returns>取得した国名</returns>
    ''' <remarks>取得できない場合は何らかのエラーメッセージ</remarks>
    Public Function GetCountryNameFromCode(ByVal code As String) As String
        Dim drs() As DataRow
        If code.Length = 2 Then
            drs = ConturyCodeTable.Select(String.Format("{0}='{1}'", "code2", code))
        ElseIf code.Length = 3 Then
            drs = ConturyCodeTable.Select(String.Format("{0}='{1}'", "code3", code))
        Else
            Return "(error)"
        End If
        If Not drs.Length = 1 Then Return "(unknown)"
        Return drs(0).Item("japanese").ToString
    End Function

    ''' <summary>
    ''' 日付がどの年号に属しているか取得する(未実装)
    ''' </summary>
    ''' <param name="datetime">西暦における年月</param>
    ''' <returns>常にNothing</returns>
    ''' <remarks>変換ロジック</remarks>
    Public Function GetEraFromDate(ByVal datetime As Date) As String
        Return Nothing
    End Function

#End Region





End Class
