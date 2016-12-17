Public Class EnvironmentReader

#Region "メンバー変数"

    ''' <summary>
    ''' 基礎知識ファイルから読み取った基礎知識情報
    ''' </summary>
    Protected _EnvironmentData As New EnvironmentData

    ''' <summary>
    ''' 辞書ファイルが格納されているディレクトリ
    ''' </summary>
    Private DicFileDir As String

    ''' <summary>
    ''' 辞書ファイル一覧
    ''' </summary>
    Private DicFileList As String()

    ''' <summary>
    ''' プラグインファイルが格納されているディレクトリ
    ''' </summary>
    Private PluginFileDir As String

    ''' <summary>
    ''' プラグインファイル一覧
    ''' </summary>
    Private PluginFileList As String()

    ''' <summary>
    ''' データの読み取りが完全に終了しているかどうかを表す
    ''' </summary>
    ''' <remarks></remarks>
    Private _IsReadingComplete As Boolean

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 読み取ったWdicDataを返却する
    ''' </summary>
    ''' <returns>読み取られた環境情報を格納しているWdicDataクラス</returns>
    Public ReadOnly Property GetWdicData() As EnvironmentData
        Get
            Return _EnvironmentData
        End Get
    End Property

    ''' <summary>
    ''' WdicDataが完全に読み取られているかどうかを取得する
    ''' </summary>
    ''' <returns>読み取りに成功したときはTrue、そうでない場合はFalse</returns>
    Public ReadOnly Property IsReadingComplete() As Boolean
        Get
            Return _IsReadingComplete
        End Get
    End Property


#End Region

#Region "データ読み取り"

    ''' <summary>
    ''' 環境データ、辞書データ等の一括読み取り
    ''' </summary>
    ''' <returns>
    ''' 読み取りが完全に終了したときはTrue
    ''' 不完全のときはFalse
    ''' </returns>
    ''' <remarks>EnvironmentDataクラスのIsDataCompleteの値と連動</remarks>
    Public Function ReadEnvironment() As Boolean

        ' 終了フラグ初期化(エラーの時はFalse)
        _IsReadingComplete = True

        ' 基礎情報を読み取る
        If _IsReadingComplete Then ReadBaseData()

        ' 辞書データを読み取る
        If _IsReadingComplete Then
            DicFileDir = My.Settings("DirDictionary").ToString
            PluginFileDir = My.Settings("DirPlugin").ToString
            ReadDicData()
        End If

        ' 読み取りが成功かどうかを返す
        _EnvironmentData.IsDataComplete = _IsReadingComplete
        Return _IsReadingComplete

    End Function

#End Region

#Region "読み取り処理(基礎情報)"

    ''' <summary>
    ''' 基礎情報を読み取る
    ''' </summary>
    Private Sub ReadBaseData()

        ' エラーメッセージ
        Try
            ReadErrorMessage()
        Catch ex As System.IO.IOException
            MsgBox("エラーメッセージファイル(ErrorMessage.txt)が正しく読み取れませんでした。", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        Catch
            MsgBox("エラーメッセージファイル(ErrorMessage.txt)が壊れています。", MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        End Try

        ' 言語コード
        Try
            ReadLangCode()
        Catch ex As System.IO.IOException
            MsgBox(_EnvironmentData.ErrorMessage("ENV0003"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        Catch
            MsgBox(_EnvironmentData.ErrorMessage("ENV0004"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        End Try

        ' 国コード
        Try
            ReadCountryCode()
        Catch ex As System.IO.IOException
            MsgBox(_EnvironmentData.ErrorMessage("ENV0010"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        Catch
            MsgBox(_EnvironmentData.ErrorMessage("ENV0011"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        End Try

        ' 年号コード
        Try
            ReadEra()
        Catch ex As System.IO.IOException
            MsgBox(_EnvironmentData.ErrorMessage("ENV0005"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        Catch
            MsgBox(_EnvironmentData.ErrorMessage("ENV0006"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        End Try

        ' 実体参照
        Try
            ReadEntity()
        Catch ex As System.IO.IOException
            MsgBox(_EnvironmentData.ErrorMessage("ENV0007"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        Catch
            MsgBox(_EnvironmentData.ErrorMessage("ENV0008"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            GoTo err_common
        End Try

no_err:
        Exit Sub

err_common:
        _IsReadingComplete = False
        Exit Sub

    End Sub

    ''' <summary>
    ''' エラーメッセージを読み取る
    ''' </summary>
    Private Sub ReadErrorMessage()

        ' エラーメッセージファイルの読み込み
        Dim filename As String = System.IO.Path.Combine(FileFunction.GetApplicationPath(), My.Resources.ErrorMessageFile)
        Dim tr As New TsvReader(filename)
        tr.Parse()
        Dim datas()() As String = tr.TsvMatrix

        ' DataSetに格納する
        For i As Integer = 0 To datas.Length - 1

            ' データ整合性チェック
            If Not datas(i).Length = 2 Then
                Throw New ArgumentException("エラーメッセージファイルの形式が正しくない：" & i & "行目")
            End If

            ' データ格納
            Dim dr As DataRow = _EnvironmentData.ErrorMessageTable.NewRow
            dr.Item("id") = i
            dr.Item("code") = datas(i)(0).Trim()  ' 0列目 = エラーコード
            dr.Item("text") = datas(i)(1).Trim() ' 1列目 = エラーメッセージ
            _EnvironmentData.ErrorMessageTable.Rows.Add(dr)

        Next

    End Sub

    ''' <summary>
    ''' 言語コードを読み取る
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadLangCode()

        ' 言語コードファイルを展開
        Dim filename As String = System.IO.Path.Combine(FileFunction.GetApplicationPath(), My.Resources.LangCodeFile)
        Dim tr As New TsvReader(filename)
        tr.Parse()
        Dim datas()() As String = tr.TsvMatrix

        ' DataSetに格納する
        Dim dt As DataTable = _EnvironmentData.LangCodeTable
        For i As Integer = 0 To datas.Length - 1

            ' データ整合性チェック
            If Not datas(i).Length = 4 Then
                Throw New ArgumentException("言語コードファイルの形式が正しくない：" & i + 1 & "行目")
            End If

            ' データ格納
            Dim dr As DataRow = dt.NewRow
            dr.Item("id") = i
            dr.Item("code3") = datas(i)(0).Trim()
            dr.Item("code2") = datas(i)(1).Trim()
            dr.Item("english") = datas(i)(2).Trim()  ' 実際にはデータなし
            dr.Item("japanese") = datas(i)(3).Trim()
            dt.Rows.Add(dr)

        Next

    End Sub

    ''' <summary>
    ''' 国名コードファイルを読み取る
    ''' </summary>
    Private Sub ReadCountryCode()

        ' 国名コードファイルを展開
        Dim filename As String = System.IO.Path.Combine(FileFunction.GetApplicationPath(), My.Resources.CountryCodeFile)
        Dim tr As New TsvReader(filename)
        tr.Parse()
        Dim datas()() As String = tr.TsvMatrix

        ' DataSetに格納する
        Dim dt As DataTable = _EnvironmentData.ConturyCodeTable
        For i As Integer = 0 To datas.Length - 1

            ' データ整合性チェック
            If Not datas(i).Length = 4 Then
                Throw New ArgumentException("国名コードファイルの形式が正しくない：" & i + 1 & "行目")
            End If

            ' データ格納
            Dim dr As DataRow = dt.NewRow
            dr.Item("id") = i
            dr.Item("code3") = datas(i)(0).Trim()
            dr.Item("code2") = datas(i)(1).Trim()
            dr.Item("english") = datas(i)(2).Trim()
            dr.Item("japanese") = datas(i)(3).Trim()
            dt.Rows.Add(dr)

        Next

    End Sub

    ''' <summary>
    ''' 年号一覧を読み取る
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadEra()

        ' 年号一覧ファイルの読み込み
        Dim filename As String = System.IO.Path.Combine(FileFunction.GetApplicationPath(), My.Resources.EraListFile)
        Dim tr As New TsvReader(filename)
        tr.Parse()
        Dim datas()() As String = tr.TsvMatrix

        ' DataSetに格納する
        Dim i As Integer
        For i = 0 To datas.Length - 1

            ' データ整合性チェック
            If Not datas(i).Length = 2 Then GoTo err_common
            Dim parsedDate As Date
            If Not Date.TryParse(datas(i)(0), parsedDate) Then
                If Not Date.TryParse(datas(i)(0) & "/1/1", parsedDate) Then
                    GoTo err_common
                End If
            End If

            ' データ格納
            Dim dr As DataRow = _EnvironmentData.EraTable.NewRow
            dr.Item("id") = i
            dr.Item("start_date") = parsedDate
            dr.Item("name") = datas(i)(1).Trim()
            _EnvironmentData.EraTable.Rows.Add(dr)

        Next

no_err:
        Exit Sub

err_common:
        Throw New ArgumentException("年号一覧ファイルの形式が正しくない：" & i + 1 & "行目")
        Exit Sub

    End Sub

    ''' <summary>
    ''' 文字参照を読み取る
    ''' </summary>
    Private Sub ReadEntity()

        ' 文字参照一覧ファイルの読み込み
        Dim filename As String = System.IO.Path.Combine(FileFunction.GetApplicationPath(), My.Resources.EntityReferncesFile)
        Dim tr As New TsvReader(filename)
        tr.Parse()
        Dim datas()() As String = tr.TsvMatrix

        ' DataSetに格納する
        For i As Integer = 0 To datas.Length - 1

            ' データ整合性チェック
            If Not datas(i).Length = 2 Then
                Throw New ArgumentException(" 文字参照一覧ファイルの形式が正しくない：" & i + 1 & "行目")
            End If

            ' データ格納
            Dim dr As DataRow = _EnvironmentData.EntityTable.NewRow
            dr.Item("id") = i
            dr.Item("name") = datas(i)(0).Trim()
            dr.Item("letter") = datas(i)(1).Trim()
            _EnvironmentData.EntityTable.Rows.Add(dr)

        Next

    End Sub

#End Region

#Region "読み取り処理(辞書情報)"

    ''' <summary>
    ''' 辞書情報を読み取る
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadDicData()

        ' 辞書ファイル一覧のデータを作成する
        If _IsReadingComplete Then ReadDicFile()

        ' プラグインファイル一覧のデータを作成する
        If _IsReadingComplete Then ReadPluginFile()

    End Sub

#Region "辞書ファイル"

    ''' <summary>
    ''' 辞書ファイル一覧を取得
    ''' </summary>
    Private Sub ReadDicFile()

        ' 辞書ファイル格納先フォルダが設定されていない場合は処理をしない
        If String.IsNullOrEmpty(DicFileDir) Then
            _IsReadingComplete = False
            Exit Sub
        End If

        ' 辞書ファイル格納先フォルダ配下にあるファイル名の一覧を再帰的に読み込む
        ' 読み込めなかったらエラーの後、終了
        Dim fs As New FileSearch(DicFileDir)
        Try
            DicFileList = fs.GetFile()
            If DicFileList.Length = 0 Then
                GoTo err_common
            End If
        Catch ex As System.IO.IOException
            GoTo err_common
        End Try

        ' FILE.GLを読み込む
        If _IsReadingComplete Then ReadFileGl()

        ' 辞書グループファイル一覧にWLFファイルのフルパスを追加する
        If _IsReadingComplete Then ReadWlfPath()

        ' WLFファイルを読み込む(辞書グループ一覧、辞書ファイル一覧を作成)
        If _IsReadingComplete Then ReadWlfFile()

        ' ディレクトリファイルを読み込む(単語ディレクトリを作成)
        If _IsReadingComplete Then ReadDirFile()

        ' 終了
        Exit Sub

        ' エラー表示
err_common:
        MsgBox(_EnvironmentData.ErrorMessage("DIC0001"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

    End Sub

    ''' <summary>
    ''' FILE.GLを読み込む
    ''' 
    ''' 辞書基礎情報
    ''' グループ一覧
    ''' WLFファイル(辞書グループ)一覧、
    ''' PLFファイル(プラグイングループ)一覧
    ''' </summary>
    Private Sub ReadFileGl()

        ' FILE.GLファイルが正しく存在することを確認する
        ' 1. 見つからないときはエラー
        ' 2. 2つ以上見つかったときはエラー
        Dim filename As String = Nothing
        For Each f As String In DicFileList
            If System.IO.Path.GetFileName(f).ToUpper = My.Resources.GroupFile.ToUpper Then
                If String.IsNullOrEmpty(filename) Then
                    filename = f
                Else
                    GoTo err_double
                End If
            End If
        Next
        If String.IsNullOrEmpty(filename) Then GoTo err_no_file

        ' FILE.GLを読み取る
        ' 読み取りに失敗したときはエラー
        Try
            Dim fgr As New FileGlReader(filename, _EnvironmentData)
            fgr.Parse()
        Catch
            GoTo err_readfail
        End Try
        Exit Sub

err_double:
        ' FILE.GLが複数個見つかったときはエラーを返す
        MsgBox(String.Format(_EnvironmentData.ErrorMessage("DIC0200"), My.Resources.GroupFile), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

err_no_file:
        ' FILE.GLが見つからなかった場合はエラーを返す
        MsgBox(_EnvironmentData.ErrorMessage("DIC0006"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

err_readfail:
        ' FILE.GLの読み込みに失敗した場合はエラーを返す
        MsgBox(_EnvironmentData.ErrorMessage("DIC0007"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

    End Sub

    ''' <summary>
    ''' 辞書グループファイル一覧にWLFファイルのフルパスを追加する
    ''' </summary>
    Private Sub ReadWlfPath()

        Dim dt As DataTable = _EnvironmentData.GroupTable
        Dim filename As String
        For Each filename In DicFileList

            ' 拡張子がWLFのもののみ処理する
            If Not System.IO.Path.GetExtension(filename) = "." & My.Resources.DicGroupFileExt Then Continue For

            ' ファイルグループリストに含まれるWLFファイルであるかを確認
            ' 1. 含まれない場合は無視
            ' 2. FILE.GLから2つ以上のデータが取得できる場合はエラー
            ' 3. WLFファイルが2つ以上存在する場合はエラー
            Dim drs() As DataRow = _EnvironmentData.WlfFileTable.Select(String.Format("{0}='{1}'", "filename", StringFunction.EscapeQuote(System.IO.Path.GetFileName(filename))))
            If drs.Length = 0 Then Continue For
            If drs.Length > 1 Then GoTo filegl_err
            If Not drs(0).Item("path") Is DBNull.Value Then GoTo wlf_double

            ' フルパス格納
            drs(0).Item("path") = System.IO.Path.GetDirectoryName(filename)

        Next
        Exit Sub

filegl_err:
        ' FILE.GLに異常がある場合はエラーを返す
        MsgBox(_EnvironmentData.ErrorMessage("DIC0007"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False

wlf_double:
        ' 検索したファイル一覧にWLFファイルが2つ以上存在する場合はエラーを返す
        Dim errmes As String = _EnvironmentData.ErrorMessage("DIC0200")
        MsgBox(String.Format(errmes, System.IO.Path.GetFileName(filename)), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False

    End Sub

    ''' <summary>
    ''' 辞書グループ一覧、辞書ファイル一覧を作成(WLFファイルを読み込む)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadWlfFile()

        ' WLFファイルを読み込んで辞書グループ・辞書ファイル一覧を作成
        If _IsReadingComplete Then ReadWlfFileParse()

        ' 辞書ファイル一覧と実際の辞書ファイル(フルパス)の対応を作成
        If _IsReadingComplete Then ReadDicFileFullPath()

    End Sub

    ''' <summary>
    ''' WLFファイルを読み込んで辞書グループ・辞書ファイル一覧を作成
    ''' </summary>
    Private Sub ReadWlfFileParse()
        Dim dt As DataTable = _EnvironmentData.WlfFileTable
        For Each dr As DataRow In dt.Rows
            Dim id As Integer = CInt(dr.Item("id"))
            Dim filename As String = _EnvironmentData.GetWlfFilenameFromId(id)
            Dim wr As WlfReader = New WlfReader(id, filename, _EnvironmentData)
            Try
                wr.Parse()
            Catch
                Dim errmes As String = _EnvironmentData.ErrorMessage("DIC0005")
                MsgBox(String.Format(errmes, filename), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                _IsReadingComplete = False
                Exit Sub
            End Try
        Next
    End Sub

    ''' <summary>
    ''' 辞書ファイル一覧と実際の辞書ファイル(フルパス)の対応を作成
    ''' </summary>
    Private Sub ReadDicFileFullPath()

        Dim dt As DataTable = Me._EnvironmentData.DicFileTable
        Dim drs() As DataRow
        Dim filename As String

        For Each fullpath As String In DicFileList

            ' 拡張子が辞書ファイルでない場合(.DV6以外)は処理しない
            If Not System.IO.Path.GetExtension(fullpath) = "." & My.Resources.DirectryFileExt Then Continue For

            ' dic_filesの中で一致するファイルのものを取得する
            filename = System.IO.Path.GetFileName(fullpath)
            drs = dt.Select(String.Format("{0}='{1}'", "filename", StringFunction.EscapeQuote(filename)))

            ' 一致するファイルがない場合は無視
            If drs.Length = 0 Then Continue For

            ' 2つ以上一致するものがあった場合はファイルが不正
            If drs.Length > 1 Then GoTo bad_wlffile

            ' すでにディレクトリパスが設定されている場合は不正
            If Not drs(0).Item("path") Is DBNull.Value Then GoTo file_double

            ' 上記すべてクリアしたものは格納
            drs(0).Item("path") = System.IO.Path.GetDirectoryName(fullpath)

        Next
        Exit Sub

bad_wlffile:
        ' WLFファイルの内容が不正の時
        Dim wlf_file_id As Integer = CInt(drs(0).Item("wlf_file_id"))
        Dim wlf_file As String = _EnvironmentData.GetWlfFilenameFromId(wlf_file_id)
        Dim err_mes1 As String = _EnvironmentData.ErrorMessage("DIC0005")
        MsgBox(String.Format(err_mes1, wlf_file), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

file_double:
        ' 辞書ファイルが2つ以上見つかったとき
        Dim err_mes2 As String = _EnvironmentData.ErrorMessage("DIC0200")
        MsgBox(String.Format(err_mes2, filename), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

    End Sub

    ''' <summary>
    ''' 単語ディレクトリを作成(ディレクトリファイルを読み込む)
    ''' </summary>
    Private Sub ReadDirFile()

        ' DIR.LSTファイルが正しく存在することを確認する
        Dim filename As String = Nothing
        For Each f As String In DicFileList
            If System.IO.Path.GetFileName(f) = My.Resources.DirectryListFile Then
                If String.IsNullOrEmpty(filename) Then
                    filename = f
                Else
                    GoTo err_double
                End If
            End If
        Next
        If String.IsNullOrEmpty(filename) Then GoTo err_no_file

        ' DIR.LSTを読み込んでDataSetに格納
        Try
            Dim dir As CategoryReader = New CategoryReader(filename, _EnvironmentData)
            dir.Parse()
        Catch
            GoTo err_readfail
        End Try
        Exit Sub

err_double:
        ' DIR.LSTが複数個見つかったときはエラーを返す
        MsgBox(String.Format(_EnvironmentData.ErrorMessage("DIC0200"), My.Resources.DirectryListFile), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

err_no_file:
        ' DIR.LSTが見つからなかった場合はエラーを返す
        MsgBox(_EnvironmentData.ErrorMessage("DIC0008"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

err_readfail:
        ' DIR.LSTの読み込みに失敗した場合はエラーを返す
        MsgBox(_EnvironmentData.ErrorMessage("DIC0009"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

    End Sub

#End Region

#Region "プラグインファイル"

    ''' <summary>
    ''' プラグインファイル一覧を取得
    ''' </summary>
    Private Sub ReadPluginFile()

        ' プラグインファイル格納先フォルダが設定されていない場合は処理をしない
        If String.IsNullOrEmpty(PluginFileDir) Then Exit Sub
        If Not _IsReadingComplete Then Exit Sub

        ' プラグインファイル一覧を読み込む
        ' 読み込めなかったら何もしない
        Dim fs As New FileSearch(PluginFileDir)
        Dim files As String() = fs.GetFile()
        If files.Length = 0 Then Exit Sub
        PluginFileList = files

        ' PLFファイル一覧を作成
        If _IsReadingComplete Then ReadPlfPath()

        ' プラグインファイル一覧を作成(未実装)
        If _IsReadingComplete Then ReadPlfFile()

    End Sub

    ''' <summary>
    ''' プラグイングループファイル一覧にPLFファイルのフルパスを追加する
    ''' </summary>
    Private Sub ReadPlfPath()

        Dim filename As String
        For Each filename In PluginFileList

            ' 拡張子がPLFのもののみ処理する
            If Not System.IO.Path.GetExtension(filename) = "." & My.Resources.PluginGroupFileExt Then Continue For

            ' ファイルグループリストに含まれるPLFファイルであるかを確認
            ' 1. 含まれない場合は無視
            ' 2. FILE.GLから2つ以上のデータが取得できる場合はエラー
            ' 3. PLFファイルが2つ以上存在する場合はエラー
            Dim drs() As DataRow = _EnvironmentData.PlfFileTable.Select(String.Format("{0}='{1}'", "filename", StringFunction.EscapeQuote(System.IO.Path.GetFileName(filename))))
            If drs.Length = 0 Then Continue For
            If drs.Length > 1 Then GoTo filegl_err
            If Not drs(0).Item("path") Is DBNull.Value Then GoTo plf_double

            ' フルパス格納
            drs(0).Item("path") = System.IO.Path.GetDirectoryName(filename)

        Next
        Exit Sub

filegl_err:
        ' FILE.GLに異常がある場合はエラーを返す
        MsgBox(_EnvironmentData.ErrorMessage("DIC0007"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

plf_double:
        ' 検索したファイル一覧にPLFファイルが2つ以上存在する場合はエラーを返す
        Dim errmes As String = _EnvironmentData.ErrorMessage("DIC0200")
        MsgBox(String.Format(errmes, System.IO.Path.GetFileName(filename)), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        _IsReadingComplete = False
        Exit Sub

    End Sub

    ''' <summary>
    ''' プラグインファイル一覧の読み込み
    ''' </summary>
    ''' <remarks>PLFファイルが正式に採用されたら処理は変更される</remarks>
    Private Sub ReadPlfFile()

        ' PLFファイルを読み込んで辞書グループ・辞書ファイル一覧を作成
        ' (PLFファイルが正式採用されていないのでまだ利用しません)
        'If _IsReadingComplete Then ReadPlfFileParse()

        ' 辞書ファイル一覧と実際の辞書ファイル(フルパス)の対応を作成
        ' (PLFファイルが正式採用されていないのでいんちき実装です)
        If _IsReadingComplete Then ReadPluginFileFullPath()


    End Sub

    ''' <summary>
    ''' PLFファイルを読み込んでプラグインファイル一覧を設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadPlfFileParse()
        For Each dr As DataRow In _EnvironmentData.PlfFileTable.Rows
            Dim id As Integer = CInt(dr.Item("id"))
            Dim filename As String = _EnvironmentData.GetPlfFilenameFromId(id)
            Dim pr As PlfReader = New PlfReader(id, filename, _EnvironmentData)
            Try
                pr.Parse()
            Catch
                Dim errmes As String = _EnvironmentData.ErrorMessage("DIC0005")
                MsgBox(String.Format(errmes, filename), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                _IsReadingComplete = False
                Exit Sub
            End Try
        Next
    End Sub

    ''' <summary>
    ''' プラグインファイル一覧のフルパスを設定する
    ''' </summary>
    ''' <remarks>まだ完全な実装ではありません</remarks>
    Private Sub ReadPluginFileFullPath()

        ' ファイル一覧をDataSetに格納する
        Dim i As Integer = 0
        Dim dt As DataTable = _EnvironmentData.PluginFileTable
        For Each file As String In PluginFileList

            Dim dr As DataRow = dt.NewRow
            dr.Item("id") = i
            dr.Item("plf_file_id") = 0   ' ダミー
            dr.Item("filename") = System.IO.Path.GetFileName(file)
            dr.Item("path") = System.IO.Path.GetDirectoryName(file)
            dt.Rows.Add(dr)
            i += 1
        Next

    End Sub

#End Region

#End Region

End Class
