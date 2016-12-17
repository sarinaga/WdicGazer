Option Strict On

Public Class Main

#Region "定義・宣言"

    ''' <summary>
    ''' 環境情報を保持する
    ''' </summary>
    ''' <remarks>Mainだけでなく他のすべての画面・クラスから参照される</remarks>
    Protected Friend Environment As EnvironmentData

    ''' <summary>
    ''' 辞書種類を選択するときに利用するアプリケーション設定の接頭語
    ''' </summary>
    Private Const DIC_PREFIX As String = "Dic"

    ''' <summary>
    ''' カテゴリを選択する時に利用するコントロールの接頭語
    ''' </summary>
    Private Const CAT_PREFIX As String = "Cat"

    ''' <summary>
    ''' ブックマークデータ
    ''' </summary>
    Protected Friend BookmarkRoot As BookmarkItem

    ''' <summary>
    ''' 履歴データ
    ''' </summary>
    Protected Friend History As BookmarkItem

#End Region

#Region "初期化・終了時処理"

    ''' <summary>
    ''' 初期化
    ''' </summary>
    Private Sub Main_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' 設定読み取り
        Me.RegistryReader()

        ' 環境ファイル読み取り
        ReadEnvironment()

        ' ブックマーク読み取り・設定
        ReadBookmark()

        ' 履歴読み取り・設定
        ReadHistory()

        ' 起動時テストプログラム(通常はコメントアウト)
        InitialTest()

    End Sub

    ''' <summary>
    ''' 1回目の表示
    ''' </summary>
    Private Sub Main_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        ' 辞書ファイルのディレクトリの位置が指定されていない場合はオプション画面を表示する
        If My.Settings("DirDictionary").ToString.Length = 0 Then
            MsgBox(Environment.ErrorMessage("DIC0000"), MsgBoxStyle.Information)
            SettingOption.ShowDialog()
            If SettingOption.DialogResult = Windows.Forms.DialogResult.OK Then
                ReadEnvironment()
            End If
            If Not Search.Enabled Then
                MsgBox(Environment.ErrorMessage("DIC0300"), MsgBoxStyle.Information)
            End If
        End If

    End Sub

    ''' <summary>
    ''' 各種環境データや辞書ファイル等のインデックスを読み込む
    ''' </summary>
    Private Sub ReadEnvironment()

        ' データの読み取り
        Dim er As EnvironmentReader = New EnvironmentReader
        er.ReadEnvironment()
        Environment = er.GetWdicData

        ' 最後まで読み取れたかどうかで検索ボタン等をスイッチする
        Search.Enabled = er.IsReadingComplete

    End Sub

    ''' <summary>
    ''' ブックマーク読み取り
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadBookmark()
        BookmarkRoot = CType(BookmarkItem.DeSerialize(My.Settings.BookmarkData), BookmarkItem)
        If BookmarkRoot Is Nothing Then
            BookmarkRoot = New BookmarkItem
            BookmarkRoot.Children = New List(Of BookmarkItem)
            BookmarkRoot.Name = ""
            My.Settings.BookmarkData = BookmarkRoot.Serialize()
        End If
        TreeMenu.SetBookmarkToDropDown(BookmarkMenuOnMain.DropDown)
    End Sub

    ''' <summary>
    ''' 履歴読み取り
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ReadHistory()
        History = CType(BookmarkItem.DeSerialize(My.Settings.HistoryData), BookmarkItem)
        If History Is Nothing Then
            History = New BookmarkItem
            History.Children = New List(Of BookmarkItem)
            History.Name = ""
            My.Settings.HistoryData = History.Serialize()
        End If
        TreeMenu.SetHistoryToDropDown(HistoryMenuOnMain.DropDown)
    End Sub

    ''' <summary>
    ''' 終了処理
    ''' </summary>
    Private Sub Main_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        RegistryWriter()
    End Sub

#End Region

#Region "検索"

    ''' <summary>
    ''' 検索ボタン押下
    ''' </summary>
    Private Sub Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Search.Click
        SearchWdic()
    End Sub

    ''' <summary>
    ''' 検索単語入力フォームでEnterが押された場合
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SearchWord_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles SearchWord.KeyDown
        If e.KeyCode = Keys.Enter Then SearchWdic()
    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SearchWdic()

        ' 検索文字列チェック
        Dim result As Boolean = IsValidSearchText()

        ' 検索
        If result Then SearchProcess()

    End Sub

    ''' <summary>
    ''' 検索文字列の入力内容が正当であるかどうかを返す
    ''' </summary>
    ''' <returns>正しければTrue</returns>
    ''' <remarks></remarks>
    Private Function IsValidSearchText() As Boolean

        ' 環境が完全に読み込まれていない場合は処理を行わない
        ' (実際にはボタンが無効化されているので処理されない)
        If Not Environment.IsDataComplete Then
            MsgBox(Environment.ErrorMessage("ENV0009"), MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
            Return False
        End If

        ' 検索文字列が未入力の場合は処理を行わない
        If SearchWord.Text.Length = 0 Then
            MsgBox(Environment.ErrorMessage("MAIN0002"), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            Return False
        End If

        ' カテゴリが選択されていない場合は処理を行わない
        If Not IsSetCategotyDictionary() Then
            MsgBox(Environment.ErrorMessage("MAIN0006"), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            Return False
        End If

        ' 正規表現が正しくない場合は処理を行わない
        If SeaRegex.Checked And Not IsValidRegexPattern() Then
            MsgBox(Environment.ErrorMessage("MAIN0000"), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            Return False
        End If

        ' 正規表現を利用しているとき、位置指定子が利用できるのは単語検索のときのみ
        If Not IsNotHavePositonOperator() Then
            MsgBox(Environment.ErrorMessage("MAIN0001"), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' 正規表現の内容が正当であるかどうかをチェックする
    ''' </summary>
    ''' <returns>正当である場合はTrue</returns>
    ''' <remarks></remarks>
    Private Function IsValidRegexPattern() As Boolean
        Try
            System.Text.RegularExpressions.Regex.IsMatch("", SearchWord.Text)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 位置指定子の指定が正当であるかどうかをチェックする
    ''' </summary>
    ''' <returns>正当ならばTrue</returns>
    ''' <remarks></remarks>
    Private Function IsNotHavePositonOperator() As Boolean
        If Not (SeaRegex.Checked And SeaTextSearch.Checked) Then Return True
        Dim t As String = SearchWord.Text
        If t(0) = "^" Then Return False
        If t.Length < 2 Then Return True
        If t(t.Length - 1) <> "$" Then Return True
        Dim count As Integer = 0
        For i As Integer = t.Length - 2 To 0
            If t(i) <> "\"c Then Exit For
            count += 1
        Next
        Return Not (count Mod 2 = 0)
    End Function

    ''' <summary>
    ''' カテゴリが選択されているかどうかをチェックする
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSetCategotyDictionary() As Boolean
        Dim selected As Boolean = False
        For Each check As CheckBox In Category.Controls
            selected = selected Or check.Checked
        Next
        Return selected
    End Function

    ''' <summary>
    ''' 単語名検索処理
    ''' </summary>
    Private Sub SearchProcess()

        ' 検索条件格納
        Dim c As New SearchConditionData
        c.SearchWord = SearchWord.Text
        If SeaSuffix.Checked Then
            c.SearchMode = SearchConditionData.SearchModeType.Suffix
        ElseIf SeaPrefix.Checked Then
            c.SearchMode = SearchConditionData.SearchModeType.Prefix
        ElseIf SeaInclude.Checked Then
            c.SearchMode = SearchConditionData.SearchModeType.Include
        ElseIf SeaPerfect.Checked Then
            c.SearchMode = SearchConditionData.SearchModeType.Perfect
        ElseIf SeaRegex.Checked Then
            c.SearchMode = SearchConditionData.SearchModeType.Regex
        Else
            Throw New ArgumentException()
        End If
        c.IsTextSearch = SeaTextSearch.Checked
        c.IsCapitalCheck = SeaCapitalCheck.Checked

        ' 検索辞書の種類を格納
        If Not My.Settings.DicAll Then
            c.SearchDictonary = GetDictonaryList
        End If

        ' 検索カテゴリを格納
        If Not CatAll.Checked Then
            c.SearchCategory = GetCategoryList
        End If

        ' インデックス作成
        WordSearcher.SearchCondition = c
        WordSearcher.ShowDialog()

        ' 途中中断は終了
        If WordSearcher.DialogResult = Windows.Forms.DialogResult.Cancel Then Exit Sub
        If WordSearcher.SearchResult Is Nothing Then Exit Sub

        ' 検索結果0件の時はエラーメッセージを表示
        Dim dt As DataTable = WordSearcher.SearchResult.ResultList
        If dt.Rows.Count = 0 Then GoTo not_found_word

        ' 検索文字履歴を追加
        AddHistory()

        ' 検索結果表示
        If dt.Rows.Count > 1 Then
            ' 2つ以上見つかった場合
            ResultDisplay.WordList = WordSearcher.SearchResult
            ResultDisplay.SearchCondition = c
            ResultDisplay.DataSet()
            ResultDisplay.Show()
            ResultDisplay.Activate()
        Else
            '1つの場合
            CallWordDisplay(dt.Rows(0))
        End If
no_err:
        Exit Sub

not_found_word:
        Dim err_code As String
        If My.Settings.DicAll Then
            err_code = "RESULT0000"
        Else
            err_code = "RESULT0001"
        End If
        MsgBox(Environment.ErrorMessage(err_code), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
        Exit Sub

    End Sub

    ''' <summary>
    ''' 検索する辞書種類一覧を取得
    ''' </summary>
    ''' <returns>辞書種類の一覧</returns>
    ''' <remarks>GroupNoで返す</remarks>
    Private Function GetDictonaryList() As Integer()
        Dim dics As New ArrayList
        Dim DicType As Type = My.Settings.GetType()
        Dim DicTypeArray() As Reflection.PropertyInfo = DicType.GetProperties
        For Each p As Reflection.PropertyInfo In DicTypeArray
            If Not p.Name.Substring(0, DIC_PREFIX.Length) = DIC_PREFIX Then Continue For
            Dim name As String = p.Name.Substring(DIC_PREFIX.Length).ToUpper
            Dim checked As Boolean = CBool(p.GetValue(My.Settings, Nothing))
            If Not checked Then Continue For
            If name = "ALL" Then Return New Integer() {}
            Dim dr() As DataRow = Environment.GroupTable.Select(String.Format("{0}='{1}'", "type", name))
            If Not dr.Length = 1 Then Throw New UnjustProcessingException
            dics.Add(CInt(dr(0).Item("id")))
        Next
        Return CType(dics.ToArray(GetType(Integer)), Integer())
    End Function

    ''' <summary>
    ''' 検索するカテゴリ一覧を取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetCategoryList() As String()
        Dim cats As New ArrayList
        For Each ctrl As Control In Category.Controls
            If Not ctrl.Name.Substring(0, CAT_PREFIX.Length) = CAT_PREFIX Then Continue For
            If Not TypeOf ctrl Is CheckBox Then Continue For
            Dim cb As CheckBox = CType(ctrl, CheckBox)
            If Not cb.Checked Then Continue For
            Dim cat As String = cb.Name.Substring(CAT_PREFIX.Length).ToUpper
            If cat = "ALL" Then Return New String() {}
            cats.Add(cat)
        Next
        Return CType(cats.ToArray(GetType(String)), String())
    End Function

    ''' <summary>
    ''' WordDisplayを表示する(すでに表示されている場合は項目追加)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Sub CallWordDisplay(ByRef dr As DataRow)

        ' 画面初期化
        WordDisplay.Show()

        ' 基本情報入手
        Dim typename As String = CStr(dr.Item(ResultListData.Col_TypeName))
        Dim word As String = CStr(dr.Item(ResultListData.Col_Word))
        Dim fullpath As String = CStr(dr.Item(ResultListData.Col_FullPath))
        Dim pos As Integer = CInt(dr.Item(ResultListData.Col_Position))

        ' 単語表示
        WordDisplay.AddTab(typename, word, fullpath, pos)

        ' 画面表示
        WordDisplay.WindowState = FormWindowState.Normal

        ' ヘッダの表示非表示の設定
        WordDisplay.SwitchViewWordHeader()


    End Sub

    ''' <summary>
    ''' 検索文字列追加
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddHistory()
        For Each h As Object In HistoryList.Items
            If h.ToString = SearchWord.Text Then Exit Sub
        Next
        If HistoryList.Items.Count >= My.Settings.MaxHistory Then
            For i As Integer = 0 To HistoryList.Items.Count - My.Settings.MaxHistory
                HistoryList.Items.RemoveAt(i)
            Next
        End If
        HistoryList.Items.Add(SearchWord.Text)
    End Sub

#End Region

#Region "コピー＆ペーストイベント"

    ''' <summary>
    ''' コピー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyOnMain.Click
        If SearchWord.SelectionLength > 0 Then
            SearchWord.Copy()
        End If
    End Sub

    ''' <summary>
    ''' カット
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextCut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CutOnMain.Click
        If SearchWord.SelectionLength > 0 Then
            SearchWord.Cut()
        End If
    End Sub

    ''' <summary>
    ''' ペースト
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextPaste_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PasteOnMain.Click
        If Not SearchWord.Focused() Then Exit Sub
        If Clipboard.GetDataObject().GetDataPresent(DataFormats.Text) Then
            SearchWord.Paste()
        End If
    End Sub

    ''' <summary>
    ''' やり直し
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextUndoMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UndoOnMain.Click
        If Not SearchWord.Focused() Then Exit Sub
        If Not SearchWord.CanUndo Then Exit Sub
        SearchWord.Undo()
        SearchWord.ClearUndo()
    End Sub


    ''' <summary>
    ''' 全選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextAllSelectMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllOnMenu.Click
        If Not SearchWord.Focused() Then Exit Sub
        SearchWord.SelectAll()
    End Sub

#End Region

#Region "その他イベント"

    ''' <summary>
    ''' 環境ファイルの再読み込み
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ReloadWdic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ReloadWdic.Click
        ReadEnvironment()
        If Search.Enabled Then
            MsgBox(Environment.ErrorMessage("DIC0301"), MsgBoxStyle.Information)
        Else
            MsgBox(Environment.ErrorMessage("DIC0302"), MsgBoxStyle.Information)
        End If
    End Sub

    ''' <summary>
    ''' 終了
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseWdicGazer.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' 検索履歴呼び出し
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub History_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HistoryList.SelectedIndexChanged
        SearchWord.Text = HistoryList.SelectedItem.ToString
    End Sub

    ''' <summary>
    ''' 検索履歴消去
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ClearHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearHistoryList.Click
        HistoryList.Items.Clear()
    End Sub

    ''' <summary>
    ''' オプションダイアログ開くときの処理
    ''' </summary>
    Private Sub OptionOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenOption.Click

        ' 変更前の辞書ディレクトリを保存
        Dim tempdicdir As String = My.Settings.DirDictionary
        Dim tempplugindir As String = My.Settings.DirPlugin

        ' オプションダイアログ表示
        SettingOption.ShowDialog()

        ' 変更後の辞書ディレクトリを取得
        Dim newdicdir As String = My.Settings.DirDictionary
        Dim newplugindir As String = My.Settings.DirPlugin


        ' 辞書ディレクトリが変更されたときはインデックス再生成
        If (Not tempdicdir = newdicdir) Or (Not tempplugindir = newplugindir) Then
            ReadEnvironment()
        End If

    End Sub

    ''' <summary>
    ''' WDICバージョン情報
    ''' </summary>
    Private Sub WdicExplorerAbout(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenAboutWdicGazer.Click
        Dim f As WdicAbout = New WdicAbout()
        f.ShowDialog()
    End Sub

    ''' <summary>
    ''' WDIC Gazerバージョン情報
    ''' </summary>
    Private Sub WdicAboutOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenAboutWdic.Click
        Dim f As WdicGazerAbout = New WdicGazerAbout()
        f.ShowDialog()
    End Sub

    ''' <summary>
    ''' 本文を検索するときは[含まれる]か[正規表現]のみ
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TextSearch_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SeaTextSearch.CheckedChanged
        SeaPrefix.Enabled = Not SeaTextSearch.Checked
        SeaSuffix.Enabled = Not SeaTextSearch.Checked
        SeaPerfect.Enabled = Not SeaTextSearch.Checked
        If SeaTextSearch.Checked Then
            If Not (SeaInclude.Checked Or SeaRegex.Checked) Then
                SeaInclude.Checked = True
            End If
        End If
    End Sub

    ''' <summary>
    ''' 全カテゴリを検索する場合はカテゴリ選択チェックボックスを無効化する
    ''' </summary>
    Private Sub All_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CatAll.CheckedChanged
        For Each c As CheckBox In Category.Controls
            If c Is CatAll Then
                c.Enabled = True
            Else
                If CatAll.Checked Then
                    c.Enabled = False
                Else
                    c.Enabled = True
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' ブックマーク整理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub EditBookmarkOnMain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditBookmarkOnMain.Click
        BookmarkEdit.ShowDialog()
    End Sub


#End Region

#Region "レジストリ類操作"

    ''' <summary>
    ''' レジストリ操作(書き込み)
    ''' </summary>
    Sub RegistryWriter()
        For Each c As ButtonBase In SearchMode.Controls
            If TypeOf c Is RadioButton Then
                My.Settings(c.Name) = CType(c, RadioButton).Checked
            ElseIf TypeOf c Is CheckBox Then
                My.Settings(c.Name) = CType(c, CheckBox).Checked
            End If
        Next
        For Each c As CheckBox In Category.Controls
            My.Settings(c.Name) = c.Checked
        Next
        Dim sc As System.Collections.Specialized.StringCollection = New System.Collections.Specialized.StringCollection()
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' レジストリ操作(読み込み)
    ''' </summary>
    Sub RegistryReader()
        My.Settings.Upgrade()
        For Each c As ButtonBase In SearchMode.Controls
            If TypeOf c Is RadioButton Then
                CType(c, RadioButton).Checked = CBool(My.Settings(c.Name))
            ElseIf TypeOf c Is CheckBox Then
                CType(c, CheckBox).Checked = CBool(My.Settings(c.Name))
            End If
        Next
        For Each c As CheckBox In Category.Controls
            c.Checked = CBool(My.Settings(c.Name))
        Next
        HistoryList.Items.Clear()
    End Sub
#End Region

#Region "テスト用関数"

    ''' <summary>
    ''' テスト用のコントロールを有効にする
    ''' </summary>
    Private Sub InitialTest()

#If DEBUG Then
        ' デバッグ用ボタンの表示
        OpenTestDialog.Visible = True
#Else
        OpenTestDialog.Visible = False
#End If

    End Sub

    ''' <summary>
    ''' テストダイアログを表示する
    ''' </summary>
    Private Sub OpenTestDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenTestDialog.Click
        MainTest.Show()

    End Sub


#End Region




End Class
