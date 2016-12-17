Imports System.Windows.Forms
Imports System.Text.RegularExpressions

''' <summary>
''' 単語の詳細内容を表示する
''' </summary>
Public Class WordDisplay

#Region "定義"

    ''' <summary>
    ''' 戻る・進むボタンの中に格納されるアイテム一覧
    ''' </summary>
    Protected PrevNext As Hashtable

    ''' <summary>
    ''' タブの中に表示されるコントロールとそのデータを保持する
    ''' </summary>
    Protected TabContents As ArrayList

    ''' <summary>
    ''' DiscriptionBrowserが利用するスタイルシート
    ''' </summary>
    Protected Friend StyleSheet As String

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' タブのデータを返す
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property TabDatas() As ArrayList
        Get
            Return TabContents
        End Get
    End Property

    ''' <summary>
    ''' 戻る・進むのデータを返す
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property WordPrevNext() As Hashtable
        Get
            Return PrevNext
        End Get
    End Property

#End Region

#Region "基本イベント"

    ''' <summary>
    ''' 閉じるメニュー押下
    ''' </summary>
    Private Sub CloseByMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseOnWord.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' 画面が閉じるとき全般の操作
    ''' </summary>
    Private Sub WordDisplay_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ' ウィンドウサイズ保持
        My.Settings.DisplayWordWidth = Me.Width
        My.Settings.DisplayWordHeight = Me.Height
    End Sub

    ''' <summary>
    ''' 画面初期化
    ''' </summary>
    Private Sub WordDisplay_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Width = My.Settings.DisplayWordWidth
        Me.Height = My.Settings.DisplayWordHeight
        ViewWordHeader.Checked = My.Settings.ViewWordHeader
        LinkNavigation.Text = ""
        If DisplayTab.TabPages(0).Name = "Empty" Then DisplayTab.TabPages.RemoveAt(0)
        PrevNext = New Hashtable()
        TabContents = New ArrayList()
        TreeMenu.SetBookmarkToDropDown(BookMarkMenuOnWord.DropDown)
        SetHistory()
    End Sub

    ''' <summary>
    ''' ブラウザ内コンテキストメニュー表示
    ''' </summary>
    Private Sub Browser_ContextMenu(ByVal sender As System.Object, ByVal e As HtmlElementEventArgs)
        e.ReturnValue = False
        Dim d As HtmlDocument = CType(sender, HtmlDocument)
        Dim w As WebBrowser = CType(TabContents(DisplayTab.SelectedIndex), TabControlData).Discription_Display
        Dim l As HtmlElement = w.Document.GetElementFromPoint(e.MousePosition)

        If False Then
            ' 何らかの選択がされている場合(未実装)
            ContextMenuBrowserSelected.Show(w, e.MousePosition)
        ElseIf l.TagName = "A" Then
            ' リンクの上の場合
            Dim url As String = l.GetAttribute("href")
            If Transrate.IsHaveSceamer(url) Then
                OpenLinkNewTab.Enabled = False
            Else
                OpenLinkNewTab.Enabled = True
            End If
            ContextMenuBrowserOnLink.Show(w, e.MousePosition)
        Else
            ' リンクの上でない場合
            ContextMenuBrowserOffLink.Show(w, e.MousePosition)
        End If
    End Sub

    ''' <summary>
    ''' WebControls周りに枠線を引く
    ''' </summary>
    Private Sub TabPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs)

        Dim t As TabPage = CType(sender, TabPage)
        Dim b As WebBrowser = Nothing
        For Each c As Control In t.Controls
            If TypeOf c Is WebBrowser Then
                b = CType(c, WebBrowser)
                Exit For
            End If
        Next
        If b Is Nothing Then Exit Sub

        Dim p As Point = b.Location
        Dim br As Rectangle = New Rectangle(p.X - 1, p.Y - 1, b.Width + 2, b.Height + 2)
        ControlPaint.DrawBorder(e.Graphics, br, Color.LightSteelBlue, ButtonBorderStyle.Solid)

    End Sub

    ''' <summary>
    ''' 進む・戻るボタンのプルダウンメニューが選択された場合
    ''' </summary>
    Private Sub GoBackForward_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) _
        Handles GoBackOnToolStrip.DropDownItemClicked, GoForwardOnToolStrip.DropDownItemClicked

        Dim tab As TabControlData = CType(TabContents(DisplayTab.SelectedIndex), TabControlData)
        HyperLinkInner("x-wdic:" & e.ClickedItem.Text)
        SetPrevNextButton()
    End Sub


#End Region

#Region "辞書読み取り"

    ''' <summary>
    ''' 単語の構文解析を行う
    ''' </summary>
    ''' <returns>構文解析の結果</returns>
    ''' <remarks>構文解析に失敗したときはNothing</remarks>
    Private Function ParseWord(ByVal typename As String, ByVal fullpath As String, ByVal pos As Integer) As WordData

        Dim reader As FileReader
        Dim cutter As WdicCutter
        Dim parser As WdicParser
        Try
            ' 検索結果から値を取得する
            reader = New FileReader(fullpath)
            reader.Read()
            cutter = New WdicCutter(reader)
            parser = New WdicParser(cutter.Cut(pos))
            parser.Parse()
        Catch
            ' 構文解析エラー表示
            MsgBox(Main.Environment.ErrorMessage("RESULT0005"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            Return Nothing
        End Try

        ' 必要なデータを格納して解析結果を返す
        parser.ParsedData.Type = typename
        parser.ParsedData.GroupNo = Main.Environment.GetGroupNoFromTypeName(typename)
        parser.ParsedData.Fullpath = fullpath
        parser.ParsedData.Filename = System.IO.Path.GetFileName(fullpath)
        parser.ParsedData.Pos = pos
        Return parser.ParsedData

    End Function

    ''' <summary>
    ''' 単語の検索を行う
    ''' </summary>
    ''' <param name="typename">辞書略称</param>
    ''' <param name="word">単語名</param>
    ''' <returns>検索結果</returns>
    ''' <remarks>SearchResultの書式を参照</remarks>
    Public Function SearchWord(ByVal typename As String, ByVal word As String) As ResultListData

        ' WordSearcherを利用して単語を検索する
        Dim condition As New SearchConditionData
        condition.SearchDictonary = New Integer() {Main.Environment.GetGroupNoFromTypeName(typename)}
        condition.SearchMode = SearchConditionData.SearchModeType.Perfect
        condition.SearchWord = word
        condition.IsCapitalCheck = True
        WordSearcher.SearchCondition = condition
        WordSearcher.ShowDialog()

        ' 結果返却
        ' 途中で検索を中断した場合はNothingを返す
        If WordSearcher.DialogResult = DialogResult.Cancel Then Return Nothing

        ' エラーが発生したときはメッセージを表示した上でNothingを返す
        If WordSearcher.SearchResult.ResultList Is Nothing Then
            MsgBox(Main.Environment.ErrorMessage("RESULT0002"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            Return Nothing
        End If

        ' 正常終了
        Return WordSearcher.SearchResult

    End Function

    ''' <summary>
    ''' 単語の自動転送処理を行う
    ''' </summary>
    ''' <returns>転送後の単語データ</returns>
    ''' <remarks>失敗時はNothing</remarks>
    Private Function Autotransfer(ByVal parsed As WordData) As WordData

        ' 転送しない設定の場合はそのまま返す
        If Not My.Settings.AutoTransfer Then Return parsed

        Dim type As String = parsed.Type
        Dim word As String
        Do
            ' 転送処理が終わった場合は終了
            If parsed Is Nothing Then Return Nothing
            If Not TypeOf parsed.BodyItems(0) Is WordItem.Trancefer Then Return parsed
            Dim tf As WordItem.Trancefer = CType(parsed.BodyItems(0), WordItem.Trancefer)

            ' 転送先単語を取得する
            If tf.Data(0) = "/"c Then
                Dim s_pos As Integer = tf.Data.IndexOf("/", 1)
                type = tf.Data.Substring(1, s_pos - 1)
                word = tf.Data.Substring(s_pos + 1)
            Else
                word = tf.Data
            End If

            ' 転送先単語を検索する
            ' 検索失敗の時はエラーを表示
            Dim transferd As ResultListData = SearchWord(type, word)
            If transferd Is Nothing Then
                ' エラーまたはキャンセルが起きた場合(何もしないで終了)
                Return Nothing
            ElseIf transferd.ResultList.Rows.Count = 0 Then
                ' 検索結果が0になった場合(未実装)
                MsgBox(Main.Environment.ErrorMessage("RESULT0001"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
                Return Nothing
            ElseIf transferd.ResultList.Rows.Count > 1 Then
                ' 複数結果の場合は検索結果一覧を表示
                ResultDisplay.WordList = transferd
                ResultDisplay.SearchCondition = Nothing
                ResultDisplay.DataSet()
                ResultDisplay.Show()
                ResultDisplay.Activate()
                Return Nothing
            End If

            ' 転送先単語の構文解析を行う
            Dim dr As DataRow = transferd.ResultList.Rows(0)
            Dim fullpath As String = dr.Item(ResultListData.Col_FullPath).ToString
            Dim pos As Integer = CInt(dr.Item(ResultListData.Col_Position))
            parsed = ParseWord(type, fullpath, pos)

        Loop
        Return Nothing

    End Function

#End Region

#Region "タブの新規作成"

    ''' <summary>
    ''' 新しいタブを生成する
    ''' </summary>
    ''' <param name="typename">辞書略称</param>
    ''' <param name="word">単語名</param>
    ''' <param name="fullpath">単語が格納されているファイル</param>
    ''' <param name="position">単語が格納されている位置</param>
    Public Function AddTab(ByVal typename As String, ByVal word As String, ByVal fullpath As String, ByVal position As Integer) As Boolean

        ' すでに同じ単語がタブにある場合で
        ' 新規タブを作らない場合は、タブを変更して終了
        If My.Settings.AlreadyNoMakeTab Then
            If MoveSameWord(typename, word) Then Return True
        End If

        ' タブの限界数を超えている場合はタブを作らず、メッセージを表示する
        If TabContents.Count >= My.Settings.MaxTab Then
            MsgBox(Main.Environment.ErrorMessage("SYS0004"), MsgBoxStyle.Information)
            Return False
        End If

        ' 辞書ファイルを読み取って構文解析する
        Dim parsed As WordData = Nothing
        If Not String.IsNullOrEmpty(fullpath) Then parsed = ParseWord(typename, fullpath, position)

        ' データが取得できなかったときは通常検索を行い、その結果を構文解析する
        If parsed Is Nothing Then parsed = WordReload(typename, word)

        ' 再検索後もだめな場合は処理終了
        If parsed Is Nothing Then Exit Function

        ' 自動転送を行う(設定が自動転送でない場合は処理なし)
        parsed = Autotransfer(parsed)

        ' 自動転送に失敗した場合は処理終了
        If parsed Is Nothing Then Exit Function

        ' 新しいタブを作成
        Dim newtabdata As TabControlData = CreateNewTab(word)
        If newtabdata Is Nothing Then Exit Function
        DisplayData(parsed)

        ' コンテキストイベント設定
        AddHandler newtabdata.Discription_Display.Document.ContextMenuShowing, AddressOf Browser_ContextMenu

        ' 右クリックメニュー調整
        RightClickMenuSet()

        ' 前後リンクの生成
        Dim word_key As String = CreateWordKey(typename, word)
        If Not PrevNext.ContainsKey(word_key) Then
            PrevNext.Add(word_key, New PrevNextData())
        End If

        ' 履歴に追加
        AddHistory(CreateWordKey(typename, word))

        ' 正常終了
        Return True

    End Function

    ''' <summary>
    ''' 辞書種類と単語名で辞書全体を検索する時の共通処理
    ''' </summary>
    ''' <param name="typename">辞書種類</param>
    ''' <param name="word">単語</param>
    ''' <returns>解析結果</returns>
    Private Function WordReload(ByVal typename As String, ByVal word As String) As WordData

        ' 単語の再検索を行う
        Dim result As ResultListData = SearchWord(typename, word)

        ' エラーまたはキャンセルが起きた場合はNothingを返す
        ' (エラーのときはメッセージはすでに出ている)
        If result Is Nothing Then Return Nothing

        ' 検索結果が0になった場合はエラーを送出し、Nothingを返す
        If result.ResultList.Rows.Count = 0 Then
            MsgBox(Main.Environment.ErrorMessage("RESULT0001"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            Return Nothing
        End If

        ' 複数単語が見つかった場合は検索結果一覧を表示してNothingを返す
        If result.ResultList.Rows.Count > 1 Then
            ResultDisplay.WordList = result
            ResultDisplay.SearchCondition = Nothing
            ResultDisplay.DataSet()
            ResultDisplay.Show()
            ResultDisplay.Activate()
            Return Nothing
        End If

        ' 単語の位置を取得し、再度構文解析をかけてその結果を返す
        Dim position As Integer = CInt(result.ResultList.Rows(0).Item(ResultListData.Col_Position))
        Dim fullpath As String = CStr(result.ResultList.Rows(0).Item(ResultListData.Col_FullPath))
        Dim parsed As WordData = ParseWord(typename, fullpath, position)
        Return parsed

    End Function

    ''' <summary>
    ''' 新しいタブコントロールを作成
    ''' </summary>
    ''' <param name="word">タブ名</param>
    Private Function CreateNewTab(ByVal word As String) As TabControlData

        ' タブ作りすぎの時のエラーメッセージ
        If TabContents.Count >= 100 Then
            MsgBox(Main.Environment.ErrorMessage("WORD0000"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
            Return Nothing
        End If

        ' タブ作成
        Me.Activate()
        Dim newPage As TabPage = New TabPage()
        newPage.Text = word
        newPage.BackColor = SystemColors.Window
        newPage.ContextMenuStrip = ContextMenuTab
        DisplayTab.TabPages.Add(newPage)
        DisplayTab.SelectedIndex = DisplayTab.TabPages.Count - 1
        Dim newTabData As TabControlData = CreateControlsOnTab(newPage)
        TabContents.Add(newTabData)
        AddHandler newPage.Paint, AddressOf TabPaint
        Return newTabData

    End Function

    ''' <summary>
    ''' 新しいタブコントロール上にアイテムを配置する
    ''' </summary>
    ''' <param name="page">新しくタブコントロールを作るタブページ</param>
    ''' <returns>新しいタブのコントロール</returns>
    Private Function CreateControlsOnTab(ByVal page As TabPage) As TabControlData

        Dim newtab As New TabControlData
        With newtab

            ' 単語
            .TextBox_Word.Parent = page
            .TextBox_Word.Font = New Font("MS UI Gothic", 20)
            .TextBox_Word.Location = New Point(6, 6)
            .TextBox_Word.Size = New Size(page.Width - 12, 34)
            .TextBox_Word.BackColor = SystemColors.Window
            .TextBox_Word.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
            .TextBox_Word.ReadOnly = True

            ' ヘッダパネル
            .Panel_Header.Parent = page
            .Panel_Header.Font = New Font("MS UI Gothic", 9)
            .Panel_Header.Location = New Point(6, 46)
            .Panel_Header.Size = New Size(page.Width - 12, 171)
            .Panel_Header.BackColor = SystemColors.Window

            ' 日本語読み
            .Label_ReadJapanese.Parent = .Panel_Header
            .Label_ReadJapanese.Font = New Font("MS UI Gothic", 9)
            .Label_ReadJapanese.Location = New Point(2, 0)
            .Label_ReadJapanese.Size = New Size(30, 12)
            .Label_ReadJapanese.Text = "読み"
            .ListBox_ReadJapanese.Parent = .Panel_Header
            .ListBox_ReadJapanese.Font = New Font("MS UI Gothic", 9)
            .ListBox_ReadJapanese.Location = New Point(47, 0)
            .ListBox_ReadJapanese.Size = New Size(287, 16)
            .ListBox_ReadJapanese.BackColor = SystemColors.Window
            .ListBox_ReadJapanese.SelectionMode = SelectionMode.None
            .ListBox_ReadJapanese.ContextMenuStrip = ContextMenuItem

            ' 履歴
            .Label_History.Parent = .Panel_Header
            .Label_History.Font = New Font("MS UI Gothic", 9)
            .Label_History.Location = New Point(352, 0)
            .Label_History.Size = New Size(30, 12)
            .Label_History.Text = "履歴"
            .ListBox_History.Parent = .Panel_Header
            .ListBox_History.Font = New Font("MS UI Gothic", 9)
            .ListBox_History.Location = New Point(387, 0)
            .ListBox_History.Size = New Size(120, 16)
            .ListBox_History.BackColor = SystemColors.Window
            .ListBox_History.SelectionMode = SelectionMode.None
            .ListBox_History.ContextMenuStrip = ContextMenuItem

            ' 外語
            .Label_English.Parent = .Panel_Header
            .Label_English.Font = New Font("MS UI Gothic", 9)
            .Label_English.Location = New Point(2, 22)
            .Label_English.Size = New Size(30, 12)
            .Label_English.Text = "外語"
            .ListBox_English.Parent = .Panel_Header
            .ListBox_English.Font = New Font("MS UI Gothic", 9)
            .ListBox_English.Location = New Point(47, 22)
            .ListBox_English.Size = New Size(287, 16)
            .ListBox_English.BackColor = SystemColors.Window
            .ListBox_English.SelectionMode = SelectionMode.None
            .ListBox_English.ContextMenuStrip = ContextMenuItem

            ' 略語
            .Label_Abbr.Parent = .Panel_Header
            .Label_Abbr.Font = New Font("MS UI Gothic", 9)
            .Label_Abbr.Location = New Point(352, 22)
            .Label_Abbr.Size = New Size(30, 12)
            .Label_Abbr.Text = "略語"
            .ListBox_Abbr.Parent = .Panel_Header
            .ListBox_Abbr.Font = New Font("MS UI Gothic", 9)
            .ListBox_Abbr.Location = New Point(387, 22)
            .ListBox_Abbr.Size = New Size(120, 16)
            .ListBox_Abbr.BackColor = SystemColors.Window
            .ListBox_Abbr.SelectionMode = SelectionMode.None
            .ListBox_Abbr.ContextMenuStrip = ContextMenuItem

            ' 発音
            .Label_Pron.Parent = .Panel_Header
            .Label_Pron.Font = New Font("MS UI Gothic", 9)
            .Label_Pron.Location = New Point(2, 44)
            .Label_Pron.Size = New Size(30, 12)
            .Label_Pron.Text = "発音"
            .ListBox_Pron.Parent = .Panel_Header
            .ListBox_Pron.Font = New Font("MS UI Gothic", 9)
            .ListBox_Pron.Location = New Point(47, 44)
            .ListBox_Pron.Size = New Size(120, 16)
            .ListBox_Pron.BackColor = SystemColors.Window
            .ListBox_Pron.SelectionMode = SelectionMode.None
            .ListBox_Pron.ContextMenuStrip = ContextMenuItem

            ' 別名
            .Label_OtherName.Parent = .Panel_Header
            .Label_OtherName.Font = New Font("MS UI Gothic", 9)
            .Label_OtherName.Location = New Point(352, 44)
            .Label_OtherName.Size = New Size(30, 12)
            .Label_OtherName.Text = "別名"
            .Label_OtherName.Visible = False
            .ListBox_OtherName.Parent = .Panel_Header
            .ListBox_OtherName.Font = New Font("MS UI Gothic", 9)
            .ListBox_OtherName.Location = New Point(387, 44)
            .ListBox_OtherName.Size = New Size(120, 16)
            .ListBox_OtherName.BackColor = SystemColors.Window
            .ListBox_OtherName.SelectionMode = SelectionMode.None
            .ListBox_OtherName.ContextMenuStrip = ContextMenuItem
            .ListBox_OtherName.Visible = False

            ' 品詞
            .Label_Pos.Parent = .Panel_Header
            .Label_Pos.Font = New Font("MS UI Gothic", 9)
            .Label_Pos.Location = New Point(2, 69)
            .Label_Pos.Size = New Size(30, 12)
            .Label_Pos.Text = "品詞"
            .TextBox_Pos.Parent = .Panel_Header
            .TextBox_Pos.Font = New Font("MS UI Gothic", 9)
            .TextBox_Pos.Location = New Point(47, 69)
            .TextBox_Pos.ReadOnly = True
            .TextBox_Pos.Size = New Size(161, 19)
            .TextBox_Pos.BackColor = SystemColors.Window

            ' 辞書
            .Label_Dictonary.Parent = .Panel_Header
            .Label_Dictonary.Font = New Font("MS UI Gothic", 9)
            .Label_Dictonary.Location = New Point(2, 94)
            .Label_Dictonary.Size = New Size(30, 12)
            .Label_Dictonary.Text = "辞書"
            .TextBox_Dictonary.Parent = .Panel_Header
            .TextBox_Dictonary.Font = New Font("MS UI Gothic", 9)
            .TextBox_Dictonary.Location = New Point(47, 91)
            .TextBox_Dictonary.ReadOnly = True
            .TextBox_Dictonary.Size = New Size(314, 19)
            .TextBox_Dictonary.BackColor = SystemColors.Window

            ' カテゴリ
            .Label_Category.Parent = .Panel_Header
            .Label_Category.Font = New Font("MS UI Gothic", 9)
            .Label_Category.Location = New Point(2, 120)
            .Label_Category.Size = New Size(45, 12)
            .Label_Category.Text = "カテゴリ"
            .ListBox_Category.Parent = .Panel_Header
            .ListBox_Category.Font = New Font("MS UI Gothic", 9)
            .ListBox_Category.Location = New Point(47, 116)
            .ListBox_Category.Size = New Size(460, 40)
            .ListBox_Category.BackColor = SystemColors.Window
            .ListBox_Category.SelectionMode = SelectionMode.None
            .ListBox_Category.ContextMenuStrip = ContextMenuItem

            ' 記述
            .Discription_Display.Parent = page
            .Discription_Display.Location = New Point(7, 218)
            .Discription_Display.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom
            .Discription_Display.Size = New Size(page.Width - 14, page.Height - 224)
            .Discription_Display.BackColor = Color.White
            .Discription_Display.Visible = True
            .Discription_Display.WebBrowserShortcutsEnabled = True
            .Discription_Display.IsWebBrowserContextMenuEnabled = False
            .Discription_Display.AllowWebBrowserDrop = False

            ' イベント設定
            AddHandler .Discription_Display.Navigating, AddressOf NavigatingCancel
            AddHandler .Discription_Display.DocumentCompleted, AddressOf SetLinkEvent

        End With

        Return newtab

    End Function

    ''' <summary>
    ''' リンクにマウスイベントを設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SetLinkEvent(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        For Each l As HtmlElement In CType(sender, WebBrowser).Document.Links
            AddHandler l.MouseOver, AddressOf CursorOnLink
            AddHandler l.MouseLeave, AddressOf CursorOffLink
            AddHandler l.Click, AddressOf LinkClicked
        Next
    End Sub

    ''' <summary>
    ''' ナビゲートイベントの強制キャンセル
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub NavigatingCancel(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs)
        If e.Url.AbsoluteUri = "about:blank" Then Exit Sub
        e.Cancel = True
    End Sub

#End Region

#Region "タブコントロールへの値の設定"

    ''' <summary>
    ''' 項目に値を投入する
    ''' </summary>
    ''' <param name="parsed">解析された単語</param>
    Private Sub DisplayData(ByVal parsed As WordData)

        ' データ転写
        Dim index As Integer = DisplayTab.SelectedIndex
        Me.Text = parsed.Word
        DisplayTab.TabPages(index).Text = parsed.Word
        With CType(TabContents(index), TabControlData)

            ' 元データ格納
            .Word = parsed

            ' 単語
            .TextBox_Word.Text = parsed.Word
            ToolTipOnWordDisplay.SetToolTip(.TextBox_Word, parsed.Word)

            ' 品詞
            Dim pos As String = ""
            For Each hitem As WordItem.WordItemBase In parsed.HeaderItems
                If Not TypeOf hitem Is WordItem.Pos Then Continue For
                For Each pitem As String In CType(hitem, WordItem.Pos).Items
                    pos &= pitem & ","
                Next
            Next
            .TextBox_Pos.Text = pos.TrimEnd(","c)
            ToolTipOnWordDisplay.SetToolTip(.TextBox_Pos, .TextBox_Pos.Text)

            ' 日本語読み
            Dim yomi As String = ""
            For Each hitem As WordItem.WordItemBase In parsed.HeaderItems
                If Not TypeOf hitem Is WordItem.Yomi Then Continue For
                Dim yitem As WordItem.Yomi = CType(hitem, WordItem.Yomi)
                .ListBox_ReadJapanese.Items.Add(yitem.Data)
                yomi &= yitem.Data & vbCrLf
            Next
            For Each item As WordItem.WordItemBase In parsed.HeaderItems
                If Not TypeOf item Is WordItem.OldYomi Then Continue For
                .ListBox_ReadJapanese.Items.Add(CType(item, WordItem.OldYomi).Text)
            Next
            If .ListBox_ReadJapanese.Items.Count = 0 Then .ListBox_ReadJapanese.Items.Add("(なし)")
            Dim toolTipsStr As String = ""
            ToolTipOnWordDisplay.SetToolTip(.ListBox_ReadJapanese, yomi.TrimEnd)

            ' 更新履歴
            Dim author As String = ""
            For Each hitem As WordItem.WordItemBase In parsed.HeaderItems
                If Not TypeOf hitem Is WordItem.Author Then Continue For
                Dim aitem As WordItem.Author = CType(hitem, WordItem.Author)
                Dim type As String
                Select Case aitem.Type
                    Case "R"c
                        type = "更新"
                    Case "A"c
                        type = "新規"
                    Case "I"c
                        type = "追記"
                    Case Else
                        type = "不明"
                End Select
                Dim line As String = aitem.WrittenDate.ToString("yyyy/MM/dd ") & type
                .ListBox_History.Items.Add(line)
                author &= line & vbCrLf
            Next
            If .ListBox_History.Items.Count = 0 Then .ListBox_History.Items.Add(My.Resources.NO_ITEM)
            If author.Length > 0 Then ToolTipOnWordDisplay.SetToolTip(.ListBox_History, author.TrimEnd)

            ' 外語綴りおよび略語
            Dim spell As String = ""
            Dim abbr As String = ""
            For Each hitem As WordItem.WordItemBase In parsed.HeaderItems
                If Not TypeOf hitem Is WordItem.Spell Then Continue For
                Dim sitem As WordItem.Spell = CType(hitem, WordItem.Spell)

                ' 言語コード
                Dim lang As String
                If Not String.IsNullOrEmpty(sitem.LangCode3) Then
                    lang = Main.Environment.GetLangNameFromCode(sitem.LangCode3)
                ElseIf Not String.IsNullOrEmpty(sitem.LangCode2) Then
                    lang = Main.Environment.GetLangNameFromCode(sitem.LangCode2)
                Else
                    Throw New UnjustProcessingException
                End If

                ' 外国語
                Dim line1 As String = String.Format("({0}) {1}", lang, sitem.Spell)
                .ListBox_English.Items.Add(line1)
                spell &= line1 & vbCrLf

                ' 略語
                If Not String.IsNullOrEmpty(sitem.Abbr) Then
                    Dim line2 As String = String.Format("({0}) {1}", lang, sitem.Abbr)
                    .ListBox_Abbr.Items.Add(line2)
                    abbr &= line2 & vbCrLf
                End If
            Next
            If .ListBox_English.Items.Count = 0 Then .ListBox_English.Items.Add(My.Resources.NO_ITEM)
            If .ListBox_Abbr.Items.Count = 0 Then .ListBox_Abbr.Items.Add(My.Resources.NO_ITEM)
            If spell.Length > 0 Then ToolTipOnWordDisplay.SetToolTip(.ListBox_English, spell.TrimEnd())
            If abbr.Length > 1 Then ToolTipOnWordDisplay.SetToolTip(.ListBox_Abbr, abbr.TrimEnd())

            ' 発音
            Dim pron As String = ""
            For Each hitem As WordItem.WordItemBase In parsed.HeaderItems
                If Not TypeOf hitem Is WordItem.Pron Then Continue For
                Dim pitem As WordItem.Pron = CType(hitem, WordItem.Pron)

                ' 言語コード
                Dim lang As String
                If Not String.IsNullOrEmpty(pitem.LangCode3) Then
                    lang = Main.Environment.GetLangNameFromCode(pitem.LangCode3)
                ElseIf Not String.IsNullOrEmpty(pitem.LangCode2) Then
                    lang = Main.Environment.GetLangNameFromCode(pitem.LangCode2)
                Else
                    Throw New UnjustProcessingException
                End If

                ' 発音
                Dim line As String = String.Format("({0}) {1}", lang, pitem.Spell)
                .ListBox_Pron.Items.Add(line)
                pron &= line & vbCrLf

            Next
            If .ListBox_Pron.Items.Count = 0 Then .ListBox_Pron.Items.Add(My.Resources.NO_ITEM)
            ToolTipOnWordDisplay.SetToolTip(.ListBox_Pron, pron.Trim())

            ' 辞書種類
            Dim dictype As String = Main.Environment.GetDicTypeFromGroupId(parsed.GroupNo)
            .TextBox_Dictonary.Text = dictype
            ToolTipOnWordDisplay.SetToolTip(.TextBox_Dictonary, dictype)

            ' ディレクトリ
            Dim directory As String = ""
            For Each hitem As WordItem.WordItemBase In parsed.HeaderItems
                If Not TypeOf hitem Is WordItem.Dir Then Continue For

                ' メイン
                Dim dir As WordItem.Dir = CType(hitem, WordItem.Dir)
                Dim line As String = GetCategoryType(dir.Data)
                .ListBox_Category.Items.Add(line)
                directory &= line & vbCrLf

                ' リダイレクト
                Dim dr() As DataRow = Main.Environment.CategoryTable.Select(String.Format("{0} = '{1}'", "directory", dir.Data))
                If Not dr.Length = 1 Then
                    Throw New UnjustProcessingException
                End If
                Dim redirect As Object = dr(0).Item("redirect")
                If Not TypeOf redirect Is DBNull Then
                    line = GetCategoryType(CStr(redirect))
                    .ListBox_Category.Items.Add(line)
                    directory &= line & vbCrLf
                End If
            Next
            If .ListBox_Category.Items.Count = 0 Then .ListBox_Category.Items.Add(My.Resources.NO_ITEM)
            ToolTipOnWordDisplay.SetToolTip(.ListBox_Category, toolTipsStr.Trim())

            ' 本文
            Dim documentText As String = SetDiscriptionBrowser(parsed)
            .Discription_Display.DocumentText = documentText

            If directory.Length = 0 Then

            End If

        End With

    End Sub

    ''' <summary>
    ''' WordDataから本文表示部分のHTMLを作成
    ''' </summary>
    Public Function SetDiscriptionBrowser(ByRef parsed As WordData) As String
        ' スタイルシートのデータを読み込む
        'If String.IsNullOrEmpty(StyleSheet) Then
        Dim filename As String = System.IO.Path.Combine(FileFunction.GetApplicationPath(), My.Resources.StyleSheetFile)
        Dim fr As New FileReader(filename)
        Try
            fr.Read()
            StyleSheet = fr.AllText
        Catch
            Throw New UnjustProcessingException("CSSファイルの読み取りに失敗した")
        End Try
        'End If
        Return _
            "<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">" & vbCrLf & _
            "<title>" & parsed.Word & "</title>" & vbCrLf & _
            "<style>" & StyleSheet & "</style>" & vbCrLf & _
            CreateHtml(parsed.BodyItems)
    End Function

    ''' <summary>
    ''' BodyItemsから本文表示部分のHTMLを作成
    ''' </summary>
    ''' <param name="items">本文データ</param>
    ''' <returns>変換されたHTML</returns>
    ''' <remarks>本文データはArrayListであること</remarks>
    Private Function CreateHtml(ByVal items As ArrayList) As String

        Dim text As New System.Text.StringBuilder()
        For Each item As WordItem.WordItemBase In items

            If TypeOf item Is WordItem.Heading Then

                ' 見出し
                Dim deep As Integer = item.Level
                If deep > 6 Then deep = 6
                Dim tag As String = String.Format("h{0}", deep + 1)
                text.AppendLine(String.Format("<{0}>{1}</{0}>", tag, ProcessText(item.Data)))

            ElseIf TypeOf item Is WordItem.Paragraph Then
                ' 段落
                text.AppendLine("<p>" & ProcessText(item.Data) & "</p>")

            ElseIf TypeOf item Is WordItem.Table Then
                ' テーブル
                text.AppendLine(TableToHtml(CType(item, WordItem.Table)))

            ElseIf TypeOf item Is WordItem.Pre Then
                ' 整形済テキスト
                text.AppendLine("<pre>" & vbCrLf & ProcessText(item.Text) & vbCrLf & "</pre>")

            ElseIf TypeOf item Is WordItem.Quote Then
                ' 引用
                text.AppendLine(QuoteToHtml(CType(item, WordItem.Quote)))

            ElseIf TypeOf item Is WordItem.UnorderedList Or TypeOf item Is WordItem.OrderedList Then
                ' 箇条書き
                text.AppendLine("<div class='List'>")
                text.AppendLine(ListItemToHtml(item))
                text.AppendLine("</div>")

            ElseIf TypeOf item Is WordItem.DefList Then
                ' 定義済リスト
                text.AppendLine("<div class='DicList'>")
                text.AppendLine(DefListToHtml(CType(item, WordItem.DefList)))
                text.AppendLine("</div>")

            ElseIf TypeOf item Is WordItem.Trancefer Then
                ' 転送
                text.AppendLine("<p>項目" & Transrate.SetLink("[[" & item.Data & "]]") & "をご覧ください。</p>")

            ElseIf TypeOf item Is WordItem.Linker Then
                ' リンク
                text.AppendLine("<h2>リンク</h2>")

            ElseIf TypeOf item Is WordItem.Comment Then
                ' コメント
                text.AppendLine("<!--" & item.Data.Replace("--", "") & "-->")

            End If

        Next
        Return text.ToString

    End Function

    ''' <summary>
    ''' 箇条書きを作成
    ''' </summary>
    ''' <param name="list">箇条書きクラスか番号なし箇条書きクラス</param>
    ''' <returns>変換されたHTML</returns>
    Private Function ListItemToHtml(ByVal list As WordItem.WordItemBase) As String

        ' データを格納するためのStringbuilder
        Dim text As System.Text.StringBuilder = New System.Text.StringBuilder()

        ' 利用するデータを展開
        Dim items() As WordItem.ListItem
        Dim type As String
        If TypeOf list Is WordItem.OrderedList Then
            items = CType(list, WordItem.OrderedList).Items
            type = "ol"
        ElseIf TypeOf list Is WordItem.UnorderedList Then
            items = CType(list, WordItem.UnorderedList).Items
            type = "ul"
        Else
            Throw New UnjustProcessingException
        End If

        ' データをHTMLに変換
        text.AppendLine("<" & type & ">")
        Dim last As Integer = items.Length - 1
        For Each item As WordItem.ListItem In items
            text.Append("<li>" & ProcessText(item.Text))
            If Not item.ChildItem Is Nothing Then
                text.Append(vbCrLf & ListItemToHtml(item.ChildItem))
            End If
            text.AppendLine("</li>")
        Next
        text.AppendLine("</" & type & ">")

        ' 結果返却
        Return text.ToString

    End Function

    ''' <summary>
    ''' 引用テキストを生成
    ''' </summary>
    ''' <param name="quote">引用テキストクラス</param>
    ''' <returns>生成されたHTML</returns>
    Private Function QuoteToHtml(ByRef quote As WordItem.Quote) As String
        Dim s As System.Text.StringBuilder = New System.Text.StringBuilder
        s.AppendLine("<blockquote>")
        For Each line As String In Split(quote.Text, vbCrLf)
            s.AppendLine("<p>" & ProcessText(line) & "</p>")
        Next
        s.AppendLine("</blockquote>")
        Return s.ToString()
    End Function

    ''' <summary>
    ''' 定義リスト作成
    ''' </summary>
    ''' <param name="def">定義リストクラス</param>
    ''' <returns>生成されたHTML</returns>
    Private Function DefListToHtml(ByRef def As WordItem.DefList) As String
        Dim text As System.Text.StringBuilder = New System.Text.StringBuilder
        text.AppendLine("<dl>")
        For Each item As WordItem.DefItem In def.Items
            If item.IsHeading Then
                ' 見出し
                text.AppendLine("<dt>" & ProcessText(item.Text) & "</dt>")
            Else
                ' 内容
                If Not String.IsNullOrEmpty(item.Text) Then
                    ' テキストのみ
                    text.AppendLine("<dd>" & ProcessText(item.Text) & "</dd>")

                ElseIf Not item.ChildItems Is Nothing Then
                    ' 複数要素
                    text.AppendLine("<dd>")
                    text.AppendLine(CreateHtml(item.ChildItems))
                    text.AppendLine("</dd>")
                Else
                    Throw New UnjustProcessingException
                End If
            End If
        Next
        text.AppendLine("</dl>")
        Return text.ToString()
    End Function

    ''' <summary>
    ''' テーブル作成
    ''' </summary>
    ''' <param name="t">テーブルクラス</param>
    ''' <returns>生成されたHTML</returns>
    Private Function TableToHtml(ByRef t As WordItem.Table) As String
        Dim s As System.Text.StringBuilder = New System.Text.StringBuilder
        s.AppendLine("<table>")
        If Not String.IsNullOrEmpty(t.Data) Then s.AppendLine("<caption>" & ProcessText(t.Data) & "</caption>")
        s.AppendLine("<tbody>")
        For i As Integer = 0 To t.Cells.GetUpperBound(1)
            s.AppendLine("<tr>")
            For j As Integer = 0 To t.Cells.GetUpperBound(0)
                If t.Cells(j, i).IsEmpty Then Continue For
                Dim tag As String
                If t.Cells(j, i).IsHeader Then
                    tag = "th"
                Else
                    tag = "td"
                End If
                Dim align As String
                If t.Cells(j, i).Align < 0 Then
                    align = "left"
                ElseIf t.Cells(j, i).Align > 0 Then
                    align = "right"
                Else
                    align = "center"
                End If
                s.AppendFormat("<{0} align='{1}' colspan='{2}' rowspan='{3}'>{4}</{0}>", _
                    tag, align, t.Cells(j, i).ColSpan, t.Cells(j, i).RowSpan, ProcessText(t.Cells(j, i).Text))
                If t.Cells(j, i).ColSpan > 1 Or t.Cells(j, i).RowSpan > 1 Then
                    Dim col As Integer = t.Cells(j, i).ColSpan
                    Dim row As Integer = t.Cells(j, i).RowSpan
                End If
            Next
            s.AppendLine(vbCrLf & "</tr>")
        Next
        s.AppendLine("</tbody>")
        s.AppendLine("</table>")
        Dim l As String = s.ToString
        Return s.ToString
    End Function

    ''' <summary>
    ''' ディレクトリからカテゴリを取得(ルートからすべてのカテゴリを探索して連結したもの)
    ''' </summary>
    ''' <param name="dir">検索するカテゴリ</param>
    ''' <returns>検索されたカテゴリ</returns>
    Public Function GetCategoryType(ByVal dir As String) As String

        Dim dirs As ArrayList = New ArrayList
        Do
            If String.IsNullOrEmpty(dir) Then Exit Do
            Try
                Dim dr As DataRow = Main.Environment.CategoryTable.Select(String.Format("{0} = '{1}'", "directory", dir))(0)
                dirs.Add(dr.Item("name"))
            Catch ex As Exception
                Return "不明なカテゴリ"
            End Try
            dir = dir.Substring(0, dir.LastIndexOf("/"))
        Loop
        dirs.Reverse()
        Return Join(dirs.ToArray(), " ")

    End Function

    ''' <summary>
    ''' 本文テキスト整形
    ''' </summary>
    ''' <param name="text"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ProcessText(ByVal text As String) As String

        ' 開発注意点
        ' 送られてくるデータは
        ' 1. リンクや強調表示などは行われていない -> タグを設定する
        ' 2. エスケープは解除されていない -> エスケープを解除する
        ' 3. 実体参照されるべき文字がされていない -> 実体参照に変換する

        text = Transrate.DecodeDate(text)                ' 年月日変換
        text = Transrate.TimeTrans(text)                 ' 時間変換
        text = Transrate.DecodeUnits(text)               ' 単位変換
        text = Transrate.SetLink(text)                ' リンク設定(HTML)
        text = Transrate.SetRuby(text)                 ' ルビ挿入(HTML)
        text = Transrate.EmphasisTrans(text)             ' 強調(HTML)
        text = Transrate.SubSupTrans(text)               ' 上付き、下付き(HTML)
        text = Transrate.EraseUndefined(text)            ' 未定義コマンド除去
        text = Transrate.DecodeNumberReference(text)     ' 数値参照
        text = Transrate.DecodeEntityReference(text)     ' 文字参照
        text = text.Replace("&", "&amp;")                 ' & のみ普通に変換
        text = Transrate.DecodeEscapeLetter(text)        ' 1文字エスケープ除去

        Return text

    End Function

#End Region

#Region "タブ操作"

    ''' <summary>
    ''' タブ切り替え
    ''' </summary>
    Private Sub DisplayTab_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles DisplayTab.MouseDown
        Me.Text = DisplayTab.SelectedTab.Text
    End Sub

    ''' <summary>
    ''' 指定されたタブを閉じる
    ''' </summary>
    Private Sub CloseThisTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseThisTab.Click

        ' タブ数が1の時は処理しない
        If DisplayTab.TabPages.Count <= 1 Then Exit Sub

        ' 選択されたタブを取得
        Dim index As Integer = SelectedTab()
        If index < 0 Then Exit Sub

        ' タブ消去
        DisplayTab.SuspendLayout()
        If index = DisplayTab.SelectedIndex Then
            If DisplayTab.TabPages.Count - 1 <= index Then
                DisplayTab.SelectedIndex = index - 1
            Else
                DisplayTab.SelectedIndex = index + 1
            End If
            DisplayTab.SelectedTab.Focus()
        End If
        DisplayTab.TabPages.RemoveAt(index)

        ' タブ内部のデータを消す
        CType(TabContents(index), TabControlData).Dispose()

        ' タブを消す
        TabContents.RemoveAt(index)

        ' サブコントロール再描画
        DisplayTab.ResumeLayout()

        ' 右クリックメニュー再設定
        RightClickMenuSet()

    End Sub

    ''' <summary>
    ''' どのタブが選択されているかを返す
    ''' </summary>
    ''' <returns>指定されているタブ</returns>
    Private Function SelectedTab() As Integer
        Dim index As Integer
        If TypeOf ContextMenuTab.SourceControl Is TabPage Then
            index = DisplayTab.SelectedIndex
        Else
            index = -1
            For i As Integer = 0 To DisplayTab.TabCount - 1
                Dim p As Point = DisplayTab.PointToClient(ContextMenuTab.Location)
                If DisplayTab.GetTabRect(i).Contains(p.X, p.Y) Then
                    index = i
                    Exit For
                End If
            Next
        End If
        Return index
    End Function

    ''' <summary>
    ''' 指定されたタブ以外を閉じる
    ''' </summary>
    Private Sub CloseOtherTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseOtherTab.Click
        CloseRightTab_Click(sender, e)
        CloseLeftTab_Click(sender, e)
    End Sub

    ''' <summary>
    ''' 指定されたタブより左のタブを閉じる
    ''' </summary>
    Private Sub CloseLeftTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseLeftTab.Click

        ' タブ数が1の時は処理しない
        If DisplayTab.TabPages.Count <= 1 Then Exit Sub

        ' 選択されたタブを取得
        Dim index As Integer = SelectedTab()
        If index < 0 Then Exit Sub

        ' タブ消去
        DisplayTab.SuspendLayout()
        For i As Integer = index - 1 To 0 Step -1
            CType(TabContents(i), TabControlData).Dispose()
            DisplayTab.TabPages.RemoveAt(i)
            TabContents.RemoveAt(i)
        Next
        DisplayTab.ResumeLayout()
        RightClickMenuSet()

    End Sub

    ''' <summary>
    ''' 指定されたタブより右のタブを閉じる
    ''' </summary>
    Private Sub CloseRightTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseRightTab.Click

        ' タブ数が1の時は処理しない
        If DisplayTab.TabPages.Count <= 1 Then Exit Sub

        ' 選択されたタブを取得
        Dim index As Integer = SelectedTab()
        If index < 0 Then Exit Sub

        ' タブ消去
        DisplayTab.SuspendLayout()
        For i As Integer = DisplayTab.TabPages.Count - 1 To index + 1 Step -1
            CType(TabContents(i), TabControlData).Dispose()
            DisplayTab.TabPages.RemoveAt(i)
            TabContents.RemoveAt(i)
        Next
        DisplayTab.ResumeLayout()
        RightClickMenuSet()

    End Sub

    ''' <summary>
    ''' すべてのタブを閉じる(利用禁止)
    ''' </summary>
    Private Sub CloseAllTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For i As Integer = TabContents.Count - 1 To 0 Step -1
            CType(TabContents(i), TabControlData).Dispose()
            TabContents.RemoveAt(i)
            DisplayTab.TabPages.RemoveAt(i)
        Next
        RightClickMenuSet()
    End Sub

    ''' <summary>
    ''' タブの数が1の場合に一部メニューを利用不可能にする
    ''' </summary>
    Public Sub RightClickMenuSet()
        Dim flag As Boolean = True
        If DisplayTab.TabPages.Count <= 1 Then flag = False
        ContextMenuTab.Items("CloseThisTab").Enabled = flag
        ContextMenuTab.Items("CloseOtherTab").Enabled = flag
        ContextMenuTab.Items("CloseLeftTab").Enabled = flag
        ContextMenuTab.Items("CloseRightTab").Enabled = flag
    End Sub

    ''' <summary>
    ''' タブが切り替わった際の処理
    '''  1. Prev Nextボタンを変更させる
    '''  2. 単語ヘッダ部分の表示非表示を切り替える
    ''' </summary>
    Private Sub DisplayTab_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DisplayTab.SelectedIndexChanged
        SetPrevNextButton()
        SwitchViewWordHeader()
    End Sub


#End Region

#Region "リンク・単語操作"

    ''' <summary>
    ''' リンクの上にカーソルが乗った場合
    ''' </summary>
    Private Sub CursorOnLink(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs)
        Dim l As HtmlElement = CType(sender, HtmlElement)
        Dim href As String = l.GetAttribute("href")
        Dim scamer As String = href.Substring(0, href.IndexOf(":"c))
        If scamer = "x-wdic" Then
            LinkNavigation.Text = href
        ElseIf scamer = "x-geo" Then

        ElseIf scamer = "file" Then
            LinkNavigation.Text = "[プラグイン] " & href

        ElseIf Transrate.IsHaveSceamer(href) Then
            If l.InnerText = href Then
                LinkNavigation.Text = "[外部リンク] " & href
            Else
                LinkNavigation.Text = String.Format("[外部リンク] {1} {0}", href, l.InnerText)
            End If
        Else
        End If
    End Sub

    ''' <summary>
    ''' リンクからカーソルが離れた場合
    ''' </summary>
    Private Sub CursorOffLink(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs)
        LinkNavigation.Text = ""
    End Sub

    ''' <summary>
    ''' リンクがクリックされたときの処理
    ''' </summary>
    Private Sub LinkClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.HtmlElementEventArgs)

        ' 要素が A でない場合は終了
        Dim element As HtmlElement = CType(sender, HtmlElement)
        If Not element.TagName = "A" Then Exit Sub

        ' 外部リンク処理
        Dim url As String = element.GetAttribute("href")
        If HyperLinkExternal(url) Then Exit Sub

        ' 現在の単語を取得する
        Dim old_word As WordData = CType(TabContents(DisplayTab.SelectedIndex), TabControlData).Word
        Dim old_wordkey As String = CreateWordKey(old_word.Type, old_word.Word)

        ' 内部リンク処理
        If Not HyperLinkInner(url) Then Exit Sub

        ' 単語の前後を調節
        Dim new_word As WordData = CType(TabContents(DisplayTab.SelectedIndex), TabControlData).Word
        Dim new_wordkey As String = CreateWordKey(new_word.Type, new_word.Word)
        AddPrevNextData(old_wordkey, new_wordkey)
        SetPrevNextButton()

        ' 履歴を表示
        AddHistory(new_wordkey)

    End Sub

    ''' <summary>
    ''' リンクメニューから単語を新タブで開く
    ''' </summary>
    Private Sub OpenLinkNewTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenLinkNewTab.Click

        ' リンクが選択されたことを確認する
        ' そうでない場合は終了
        Dim tabdata As TabControlData = CType(TabContents(DisplayTab.SelectedIndex), TabControlData)
        Dim browser As WebBrowser = tabdata.Discription_Display
        Dim html As HtmlElement = browser.Document.GetElementFromPoint(browser.PointToClient(ContextMenuBrowserOnLink.Location))
        If Not html.TagName = "A" Then Exit Sub

        ' 内部スキーマ(x-wdic)であることを確認する
        ' そうでない場合は終了
        Dim url As String = html.GetAttribute("href")
        Dim pos As Integer = url.IndexOf(":"c)
        Dim scheme As String = url.Substring(0, pos)
        If Not scheme = "x-wdic" Then Exit Sub

        ' 現在の単語を取得する
        Dim old_word As WordData = CType(TabContents(DisplayTab.SelectedIndex), TabControlData).Word
        Dim old_wordkey As String = CreateWordKey(old_word.Type, old_word.Word)

        ' 新しく表示する単語を取得する
        Dim new_word As LinkWord = GetLinkWord(url)
        Dim new_wordkey As String = CreateWordKey(new_word.Type, new_word.Word)

        ' 単語を検索する
        ' エラー、検索結果0件、2件以上などの場合は終了
        Dim result As ResultListData = SearchWordForHyperLinkInner(new_word.Type, new_word.Word)
        If Not result.ResultList.Rows.Count = 1 Then Exit Sub

        ' 構文解析
        Dim dr As DataRow = result.ResultList.Rows(0)
        Dim fullpath As String = CStr(dr.Item(ResultListData.Col_FullPath))
        Dim position As Integer = CInt(dr.Item(ResultListData.Col_Position))
        AddTab(new_word.Type, new_word.Word, fullpath, position)

        ' ヘッダ表示・非表示切り替え
        SwitchViewWordHeader()

    End Sub

    ''' <summary>
    ''' メニューから単語を開く
    ''' </summary>
    Private Sub OpenLink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenLink.Click
        Dim w As WebBrowser = CType(TabContents(DisplayTab.SelectedIndex), TabControlData).Discription_Display
        Dim l As HtmlElement = w.Document.GetElementFromPoint(w.PointToClient(ContextMenuBrowserOnLink.Location))
        Dim old_word As WordData = CType(TabContents(DisplayTab.SelectedIndex), TabControlData).Word

        If l.TagName = "A" Then
            Dim href As String = l.GetAttribute("href")
            HyperLinkInner(href)
        End If

        ' 単語の前後を調節
        Dim new_word As WordData = CType(TabContents(DisplayTab.SelectedIndex), TabControlData).Word
        AddPrevNextData(CreateWordKey(old_word.Type, old_word.Word), CreateWordKey(new_word.Type, new_word.Word))
        SetPrevNextButton()

    End Sub

    ''' <summary>
    ''' 内部ハイパーリンク処理共通
    ''' </summary>
    ''' <param name="url">ハイパーリンク先</param>
    ''' <returns>処理が正常に終了したときはTrue</returns>
    Private Function HyperLinkInner(ByVal url As String) As Boolean

        ' リンク先の単語名情報を取得
        Dim link As LinkWord = GetLinkWord(url)
        Dim new_type As String = link.Type
        Dim new_word As String = link.Word

        ' 既存のタブに単語が存在しているかどうかを確認し、
        ' 設定で新規タブを作らないとしている時はタブの移動だけとする
        If My.Settings.AlreadyMoveOnly Then
            If MoveSameWord(new_type, new_word) Then GoTo NaviClear
        End If

        ' 新しい単語を辞書から検索する
        ' エラー、0件、2件以上の場合は終了(エラー処理などは行われている)
        Dim result As ResultListData = SearchWordForHyperLinkInner(new_type, new_word)
        If result Is Nothing Then Return False
        If Not result.ResultList.Rows.Count = 1 Then Return False

        ' 構文解析
        Dim dr As DataRow = result.ResultList.Rows(0)
        Dim fullpath As String = CStr(dr.Item(ResultListData.Col_FullPath))
        Dim position As Integer = CInt(dr.Item(ResultListData.Col_Position))
        Dim parsed As WordData = ParseWord(new_type, fullpath, position)

        ' 構文解析に失敗したときは処理終了
        If parsed Is Nothing Then Return False

        ' 自動転送を行う
        parsed = Autotransfer(parsed)

        ' 自動転送に失敗した場合は処理終了
        If parsed Is Nothing Then Exit Function

        ' 画面表示切り替え
        Dim tab_data As TabControlData = CType(TabContents(Me.DisplayTab.SelectedIndex), TabControlData)
        tab_data.Clear()
        tab_data.Word = parsed
        DisplayData(parsed)

        ' ナビゲーション文字消去(これがないといつまでも残ってしまう)
NaviClear:
        LinkNavigation.Text = ""
        Return True

    End Function

    ''' <summary>
    ''' リンク先の単語名情報を取得
    ''' </summary>
    ''' <returns>リンク先の情報を返す</returns>
    ''' <remarks></remarks>
    Private Function GetLinkWord(ByVal url As String) As LinkWord
        Dim tab_data As TabControlData = CType(TabContents(Me.DisplayTab.SelectedIndex), TabControlData)
        Dim c_point As Integer = url.IndexOf(":"c)
        Dim link_main As String = url.Substring(c_point + 1)
        Dim link_word As New LinkWord
        If link_main(0) = "/" Then
            Dim s_point As Integer = link_main.IndexOf("/"c, 1)
            link_word.Type = link_main.Substring(1, s_point - 1)
            link_word.Word = link_main.Substring(s_point + 1)
        Else
            link_word.Type = tab_data.Word.Type
            link_word.Word = link_main
        End If
        Return link_word
    End Function

    ''' <summary>
    ''' リンク処理時の検索処理
    ''' </summary>
    ''' <param name="new_type"></param>
    ''' <param name="new_word"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SearchWordForHyperLinkInner(ByVal new_type As String, ByVal new_word As String) As ResultListData

        ' リンク先単語検索
        Dim result As ResultListData = SearchWord(new_type, new_word)

        ' エラーまたはキャンセルが起きた場合(エラーは送出済みなので何もしないで終了)
        If result Is Nothing Then Return Nothing

        ' 検索結果が0になった場合
        If result.ResultList.Rows.Count = 0 Then
            MsgBox(Main.Environment.ErrorMessage("RESULT0001"), MsgBoxStyle.Critical Or MsgBoxStyle.OkOnly)
        End If

        ' 複数結果の場合は検索結果一覧を表示
        If result.ResultList.Rows.Count > 1 Then
            ResultDisplay.WordList = result
            ResultDisplay.SearchCondition = Nothing
            ResultDisplay.DataSet()
            ResultDisplay.Show()
            ResultDisplay.Activate()

        End If

        ' 結果返却
        Return result

    End Function

    ''' <summary>
    ''' スキーマにあわせて外部リンク処理を行う
    ''' </summary>
    ''' <param name="url">URL</param>
    ''' <returns>動作結果</returns>
    ''' <remarks>外部リンクを行ったらTrue</remarks>
    Private Function HyperLinkExternal(ByVal url As String) As Boolean

        ' スキーマの種類別に処理を分岐
        Dim scheme As String = url.Substring(0, url.IndexOf(":"c))
        If scheme = "x-wdic" Then
            ' 内部リンク
            Return False

        ElseIf scheme = "x-geo" Then
            ' 地図リンク
            url = ""

        ElseIf scheme = "urn" Then
            ' urn別
            Dim pos As Integer = url.LastIndexOf(":"c)
            If url.Substring(0, pos) = "urn:ietf:rfc" Then
                url = My.Resources.RFCEditor & My.Resources.RFCEditorDir & "rfc" & url.Substring(pos + 1) & ".txt"
            ElseIf url.Substring(0, pos) = "urn:ietf:std" Then
                url = My.Resources.RFCEditor & My.Resources.STDEditorDir & "std" & url.Substring(pos + 1) & ".txt"
            Else
                url = ""
            End If
        Else
            ' WdicGazerが対応しているURLかどうか調査する
            If Not Transrate.IsHaveSceamer(url) Then
                url = ""
            End If
        End If

        ' 外部リンクを展開
        If Not String.IsNullOrEmpty(url) Then
            Process.Start(url)
        Else
            ' 未対応のスキーマの場合は処理を行わない(無視)
        End If
        Return True

    End Function

#End Region

#Region "テキストエディタで該当項目を開く"

    ''' <summary>
    ''' テキストエディタで開く(メニュー)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OpenTextEditorOnMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenTextEditorOnWord.Click
        OpenTextEditor()
    End Sub

    ''' <summary>
    ''' テキストエディタで開く(DiscriptionBrowserのコンテキストメニュー)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OpenTextEditorOnContextMenuStrip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenTextEditorOnContextBrowserOffLink.Click
        OpenTextEditor()
    End Sub

    ''' <summary>
    ''' テキストエディタで開く(タブのコンテキストメニュー)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OpenTextEditorOnContextMenuTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenTextEditorOnContextMenuTab.Click
        OpenTextEditor()
    End Sub

    ''' <summary>
    ''' 該当単語をテキストエディタで開く(共通処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenTextEditor()

        ' 基本情報入手
        Dim tabIndex As Integer = DisplayTab.SelectedIndex
        Dim tabData As TabControlData = CType(TabDatas(tabIndex), TabControlData)
        Dim filename As String = tabData.Word.Fullpath
        Dim position As Integer = tabData.Word.Pos

        ' ファイルを読み込み、行数を数える
        Dim reader As FileReader = New FileReader(filename)
        reader.Read()
        Dim line As Integer = StringFunction.CountString(reader.AllText, vbCrLf, position) + 1

        ' テキストファイルオープン
        Dim exe As String = CStr(My.Settings(SettingOption.TextEditorExeFile.Name))
        Dim arg As String = My.Settings.TextEditorOption
        arg = arg.Replace("%d", CStr(line))
        arg = arg.Replace("%s", filename)
        Try
            Process.Start(exe, arg)
        Catch ex As Exception
            MsgBox(Main.Environment.ErrorMessage("SYS0003"), MsgBoxStyle.Information)

        End Try

    End Sub




#End Region

#Region "印刷"
    Private Sub Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PrintOnWord.Click

    End Sub

#End Region

#Region "履歴・ブックマーク"

    ''' <summary>
    ''' 単語がクリックされたとき
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim item As ToolStripItem = CType(sender, ToolStripItem)
        Dim word As String = item.Text
        MsgBox(word)
    End Sub





    Private Sub BookmarkThisTab_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookmarkThisTab.Click
        MsgBox("TEST1")

    End Sub

    Private Sub BookMarkThisWord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BookMarkThisWord.Click
        MsgBox("TEST2")

    End Sub



    ''' <summary>
    ''' 履歴設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHistory()
        TreeMenu.SetHistory()
    End Sub


    ''' <summary>
    ''' 履歴更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddHistory(ByVal word As String)

        ' 追加する単語と同じ単語を削除
        Dim children As List(Of BookmarkItem) = Main.History.Children
        For i As Integer = children.Count - 1 To 0 Step -1
            If children(i).Name = word Then
                Main.History.Children.RemoveAt(i)
            End If
        Next

        ' 単語を追加する
        Dim add_item As New BookmarkItem
        add_item.Children = Nothing
        add_item.Parents = Nothing
        add_item.Name = word
        Main.History.Children.Insert(0, add_item)

        ' 履歴保存数超過のときは超過した分を消去する
        Dim count As Integer = Main.History.Children.Count
        Dim max As Integer = My.Settings.MaxHistory
        If max < count Then
            Main.History.Children.RemoveRange(max, count - max)
        End If
        SetHistory()

        ' 履歴を記憶する
        My.Settings.HistoryData = Main.History.Serialize()

    End Sub


    ''' <summary>
    ''' 履歴消去(実際には利用しない?)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveHistory()
        Main.History.Children.Clear()
        SetHistory()
        My.Settings.HistoryData = Main.History.Serialize()
    End Sub


    ''' <summary>
    ''' 現在表示している単語をブックマークする
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkOnMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetBookmark.Click

        ' 選択されているタブを取得
        Dim index As Integer = DisplayTab.SelectedIndex
        If index < 0 Then Exit Sub
        Dim tab As TabControlData = CType(TabContents(index), TabControlData)

        ' ブックマークに追加
        Dim add_item As New BookmarkItem
        add_item.Parents = Main.BookmarkRoot
        add_item.Name = CreateWordKey(tab.Word.Type, tab.Word.Word)
        add_item.Children = Nothing
        AddBookmarkItemToBookmarkRoot(add_item)

        ' ブックマークのメニューに設定
        TreeMenu.SetBookmark()

        ' ブックマーク保存
        My.Settings.BookmarkData = Main.BookmarkRoot.Serialize()

    End Sub

    ''' <summary>
    ''' ブックマークのルートに単語を格納する
    ''' </summary>
    ''' <param name="add_item">追加する単語</param>
    ''' <remarks>重複した値があるときは格納しない</remarks>
    Private Sub AddBookmarkItemToBookmarkRoot(ByVal add_item As BookmarkItem)
        For Each item As BookmarkItem In Main.BookmarkRoot.Children
            If item.Name = add_item.Name Then Exit Sub
        Next
        Main.BookmarkRoot.Children.Add(add_item)
    End Sub

    ''' <summary>
    ''' 表示されているすべての単語をブックマークに格納
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SetBookmarkAllWord.Click

        ' すべてのタグを読み取る
        For Each tab As TabControlData In TabContents
            Dim add_item As New BookmarkItem
            add_item.Parents = Main.BookmarkRoot
            add_item.Name = CreateWordKey(tab.Word.Type, tab.Word.Word)
            add_item.Children = Nothing
            AddBookmarkItemToBookmarkRoot(add_item)
        Next

        ' ブックマークのメニューに設定
        TreeMenu.SetBookmark()

    End Sub

    ''' <summary>
    ''' ブックマークの編集
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditBookmarkOnWord.Click
        BookmarkEdit.ShowDialog()
    End Sub

#End Region

#Region "戻る・進む"

    ''' <summary>
    ''' 進む・戻るボタンで利用するハッシュデータを新しく作成する
    ''' </summary>
    ''' <param name="base_word">基点となる単語</param>
    ''' <param name="new_word">追加する単語</param>
    Private Sub AddPrevNextData(ByVal base_word As String, ByVal new_word As String)

        ' 操作する起点を取得
        Dim oldPrevNext As PrevNextData = CType(PrevNext(base_word), PrevNextData)
        Dim newPrevNext As PrevNextData = CType(PrevNext(new_word), PrevNextData)

        ' 次が存在しない場合は新しく用意する
        If newPrevNext Is Nothing Then
            newPrevNext = New PrevNextData()
        End If

        ' 登録
        oldPrevNext.AddNextWord(new_word)
        newPrevNext.AddPrevWord(base_word)
        PrevNext.Item(base_word) = oldPrevNext
        PrevNext.Item(new_word) = newPrevNext

    End Sub

    ''' <summary>
    ''' 進む・戻るボタンで利用するハッシュデータに単語を追加する
    ''' </summary>
    ''' <param name="btn">設定するボタン</param>
    ''' <param name="words">単語一覧</param>
    ''' <param name="new_word">追加する単語</param>
    Private Sub AddNewWordToPrevNextData( _
        ByRef btn As ToolStripSplitButton, ByRef words() As String, ByVal new_word As String)

        ' 設定する単語が存在しない場合はボタンを無効化して終了
        btn.DropDown.Items.Clear()
        If words.Length = 0 Then
            btn.Enabled = False
            btn.Text = ""
            Exit Sub
        End If

        ' プルダウンメニューの生成
        Dim baroonText As String = Nothing
        Dim new_type As String = GetTypename(new_word)
        For i As Integer = 0 To words.Length - 1

            Dim menutext As String
            If new_type = GetTypename(words(i)) Then
                ' 辞書種類が同じ場合
                menutext = RemoveTypename(words(i))
            Else
                ' 辞書種類が違う場合
                menutext = words(i)
            End If
            btn.DropDown.Items.Add(menutext)

            ' ついでにバルーンテキストのデータを取得する
            If i = 0 Then baroonText = menutext

        Next

        ' バルーンテキストの設定
        If words.Length > 1 Then
            baroonText &= String.Format(" 他{0}件", words.Length - 1)
        End If
        btn.Enabled = True
        btn.Text = baroonText

    End Sub

    ''' <summary>
    ''' ボタンに表示されるリストの内容を設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPrevNextButton()

        ' データが存在しないときはボタンを無効化する
        Dim index As Integer = DisplayTab.SelectedIndex
        If index < 0 Then GoTo NoUse
        If TabContents.Count - 1 < index Then GoTo NoUse

        Dim tab As TabControlData = CType(TabContents(index), TabControlData)
        Dim wordkey As String = CreateWordKey(tab.Word.Type, tab.Word.Word)
        Dim pn As PrevNextData = CType(PrevNext(wordkey), PrevNextData)
        If pn Is Nothing Then GoTo NoUse

        AddNewWordToPrevNextData(GoBackOnToolStrip, pn.PrevWord, wordkey)
        AddNewWordToPrevNextData(GoForwardOnToolStrip, pn.NextWord, wordkey)
        Exit Sub

NoUse:
        GoBackOnToolStrip.Enabled = False
        GoBackOnToolStrip.Text = ""
        GoForwardOnToolStrip.Enabled = False
        GoForwardOnToolStrip.Text = ""
        Exit Sub

    End Sub

    ''' <summary>
    ''' 進む(ボタン押下)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GoForwardOnToolStrip_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoForwardOnToolStrip.ButtonClick
        GoForwardCommon()
    End Sub

    ''' <summary>
    ''' 進む(DiscriptionBrowserメニュー操作)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GoForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoForward.Click
        GoForwardCommon()
    End Sub

    ''' <summary>
    ''' 進む(共通処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GoForwardCommon()
        Dim tab As TabControlData = CType(TabDatas(DisplayTab.SelectedIndex), TabControlData)
        Dim base_word As String = CreateWordKey(tab.Word.Type, tab.Word.Word)
        Dim prev_next As PrevNextData = CType(PrevNext(base_word), PrevNextData)
        If prev_next.NextWord.Length > 0 Then
            HyperLinkInner("x-wdic:" & prev_next.NextWord(0))
        End If
        SetPrevNextButton()
    End Sub

    ''' <summary>
    ''' 戻る(ボタン押下)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GoBackOnToolStrip_ButtonClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBackOnToolStrip.ButtonClick
        GoBackCommon()
    End Sub

    ''' <summary>
    ''' 戻る(DiscriptionBrowserメニュー操作)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub GoBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoBack.Click
        GoBackCommon()
    End Sub

    ''' <summary>
    ''' 戻る(共通)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GoBackCommon()
        Dim tab As TabControlData = CType(TabDatas(DisplayTab.SelectedIndex), TabControlData)
        Dim base_word As String = CreateWordKey(tab.Word.Type, tab.Word.Word)
        Dim prev_next As PrevNextData = CType(PrevNext(base_word), PrevNextData)
        If prev_next.PrevWord.Length > 0 Then
            HyperLinkInner("x-wdic:" & prev_next.PrevWord(0))
        End If
        SetPrevNextButton()
    End Sub

#End Region

#Region "コピー＆ペースト"

    ''' <summary>
    ''' ヘッダ項目をコピー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ItemCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ItemCopy.Click

        ' 選択されたコントロールを検索
        Dim tab As TabPage = DisplayTab.SelectedTab
        Dim p As Point = tab.PointToClient(ContextMenuItem.Location)
        Dim selected_ctrl As Control = Nothing
        For Each c As Control In DisplayTab.SelectedTab.Controls
            If Not TypeOf c Is ListBox Then Continue For
            Dim l As Point = c.Location
            Dim r As Rectangle = New Rectangle(l.X, l.Y, c.Width, c.Height)
            If r.Contains(p.X, p.Y) Then
                selected_ctrl = c
            End If
        Next
        If selected_ctrl Is Nothing Then Exit Sub

        ' コントロールから文字列を作成してクリップボードに転写
        Dim sc As ListBox
        If Not TypeOf selected_ctrl Is ListBox Then Exit Sub
        sc = CType(selected_ctrl, ListBox)
        If sc.Items.Count = 0 Then Exit Sub
        Dim clip As String = ""
        For Each item As Object In sc.Items
            clip &= item.ToString() & vbCrLf
        Next
        Clipboard.SetText(clip)

    End Sub

    ''' <summary>
    ''' リンクの単語をコピー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub WordCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WordCopy.Click

    End Sub

    ''' <summary>
    ''' リンク先をコピー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LinkCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LinkCopy.Click

    End Sub

    ''' <summary>
    ''' コピー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CopyOnBrowser_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyOnBrowser.Click

    End Sub

    ''' <summary>
    ''' 全選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AllSelectInSelected_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AllSelectInSelected.Click, AllSelect.Click
        Dim w As WebBrowser = Nothing  ' CType(TabContents(DisplayTab.SelectedIndex), TabControl).Browser_Display
        Dim d As HtmlDocument = w.Document
        w.Focus()
        SendKeys.SendWait("^a")
        SendKeys.Flush()

    End Sub

#End Region

#Region "単語ヘッダ表示"

    ''' <summary>
    ''' 単語ヘッダ部分の表示可否の選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ViewWordHeader_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewWordHeader.Click

        ' メニューのチェックの変更
        Dim checked As Boolean = Not ViewWordHeader.Checked
        ViewWordHeader.Checked = checked

        ' 表示の変更
        SwitchViewWordHeader()

        ' 設定の保存
        My.Settings.ViewWordHeader = checked
        My.Settings.Save()

    End Sub

    ''' <summary>
    ''' 単語ヘッダ部分の表示切り替え
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub SwitchViewWordHeader()

        ' 必要データ取得
        Dim checked As Boolean = ViewWordHeader.Checked
        Dim index As Integer = DisplayTab.SelectedIndex

        ' タブが正しく選択されていないときは処理しない
        If index < 0 Then Exit Sub

        ' TabContentsが作られる前にこのルーチンに飛んだときは処理しない
        If index >= TabContents.Count Then Exit Sub

        ' すでに表示が切り替えられているときは処理しない
        Dim tab As TabControlData = CType(TabContents(index), TabControlData)
        If tab.Panel_Header.Visible = checked Then Exit Sub

        ' 寸法を取得
        Dim point_x As Integer = tab.Discription_Display.Location.X
        Dim size_x As Integer = tab.Discription_Display.Size.Width
        Dim point_y As Integer
        Dim size_y As Integer
        If checked Then
            point_y = tab.Panel_Header.Location.Y + tab.Panel_Header.Size.Height + 1
            size_y = tab.Discription_Display.Size.Height - tab.Panel_Header.Size.Height
        Else
            point_y = tab.Panel_Header.Location.Y + 1
            size_y = tab.Discription_Display.Size.Height + tab.Panel_Header.Size.Height
        End If

        ' タブの寸法切り替え
        tab.Panel_Header.Visible = checked
        tab.Discription_Display.Location = New Point(point_x, point_y)
        tab.Discription_Display.Size = New Size(size_x, size_y)

    End Sub




#End Region

#Region "共通"

    ''' <summary>
    ''' 単語キーを作成
    ''' </summary>
    ''' <param name="typename">辞書種類</param>
    ''' <param name="word">単語名</param>
    ''' <returns>生成された単語キー</returns>
    Private Function CreateWordKey(ByVal typename As String, ByVal word As String) As String
        Return "/" & typename & "/" & word
    End Function

    ''' <summary>
    ''' 該当する単語にタブを移動させる
    ''' </summary>
    ''' <param name="type">辞書種類</param>
    ''' <param name="word">単語</param>
    ''' <returns>タブ移動結果</returns>
    ''' <remarks>単語の移動が行われたときはTrue</remarks>
    Private Function MoveSameWord(ByVal type As String, ByVal word As String) As Boolean
        Dim index As Integer = IndexSameWordTab(type, word)
        If index < 0 Then Return False
        DisplayTab.SelectTab(index)
        Return True
    End Function

    ''' <summary>
    ''' 既に該当する単語が表示されているときそのインデックスを返す
    ''' </summary>
    ''' <param name="type">辞書種類</param>
    ''' <param name="word">単語</param>
    ''' <returns>タブインデックス番号</returns>
    ''' <remarks>見つからなかった場合は0未満の数字</remarks>
    Private Function IndexSameWordTab(ByVal type As String, ByVal word As String) As Integer
        Dim index As Integer = -1
        For i As Integer = 0 To TabContents.Count - 1
            Dim tabdata As WordData = CType(TabContents(i), TabControlData).Word
            If tabdata.Type = type And tabdata.Word = word Then
                index = i
                Exit For
            End If
        Next
        Return index
    End Function

    ''' <summary>
    ''' 単語から頭の辞書種類略称を消去する
    ''' </summary>
    ''' <param name="word">単語</param>
    ''' <returns>辞書種類略称が除かれた単語</returns>
    ''' <remarks>辞書略称がない場合はそのまま返す</remarks>
    Public Function RemoveTypename(ByVal word As String) As String
        If Not word(0) = "/"c Then Return word
        Dim pos As Integer = word.IndexOf("/", 1)
        If pos < 0 Then Return word
        Dim dic As String = word.Substring(1, pos - 1)
        Dim where As String = String.Format("{0}='{1}'", "type", StringFunction.EscapeQuote(dic))
        If Main.Environment.GroupTable.Select(where).Length = 0 Then Return word
        Return word.Substring(pos + 1)
    End Function

    ''' <summary>
    ''' 単語から頭の辞書種類略称を取得する
    ''' </summary>
    ''' <param name="word">単語</param>
    ''' <returns>取得した辞書種類略称</returns>
    ''' <remarks>取得できなかった場合はNothing</remarks>
    Public Function GetTypename(ByVal word As String) As String
        If Not word(0) = "/"c Then Return Nothing
        Dim pos As Integer = word.IndexOf("/", 1)
        If pos < 0 Then Return Nothing
        Return word.Substring(1, pos - 1)
    End Function

#End Region

End Class




