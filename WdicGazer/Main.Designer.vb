<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.StatusStripOnMain = New System.Windows.Forms.StatusStrip
        Me.MenuStrip = New System.Windows.Forms.MenuStrip
        Me.FileMenuOnMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.ReloadWdic = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenTestDialog = New System.Windows.Forms.ToolStripMenuItem
        Me.CloseWdicGazer = New System.Windows.Forms.ToolStripMenuItem
        Me.EditMenuOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.CutOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.UndoOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAllOnMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.HistoryMenuOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.BookmarkMenuOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.EditBookmarkOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolMenuOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenOption = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpMenuOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenHelpOnMain = New System.Windows.Forms.ToolStripMenuItem
        Me.DisplayVersion = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenAboutWdic = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenAboutWdicGazer = New System.Windows.Forms.ToolStripMenuItem
        Me.ブックマークBToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ブックマークの管理ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.ClearHistoryList = New System.Windows.Forms.Button
        Me.Category = New System.Windows.Forms.GroupBox
        Me.CatMoe = New System.Windows.Forms.CheckBox
        Me.CatGame = New System.Windows.Forms.CheckBox
        Me.CatMili = New System.Windows.Forms.CheckBox
        Me.CatRail = New System.Windows.Forms.CheckBox
        Me.CatTran = New System.Windows.Forms.CheckBox
        Me.CatGeo = New System.Windows.Forms.CheckBox
        Me.CatEdu = New System.Windows.Forms.CheckBox
        Me.CatSoc = New System.Windows.Forms.CheckBox
        Me.CatTel = New System.Windows.Forms.CheckBox
        Me.CatCul = New System.Windows.Forms.CheckBox
        Me.CatComm = New System.Windows.Forms.CheckBox
        Me.CatNat = New System.Windows.Forms.CheckBox
        Me.CatAll = New System.Windows.Forms.CheckBox
        Me.CatTech = New System.Windows.Forms.CheckBox
        Me.CatComp = New System.Windows.Forms.CheckBox
        Me.SearchMode = New System.Windows.Forms.GroupBox
        Me.SeaCapitalCheck = New System.Windows.Forms.CheckBox
        Me.SeaPerfect = New System.Windows.Forms.RadioButton
        Me.SeaRegex = New System.Windows.Forms.RadioButton
        Me.SeaTextSearch = New System.Windows.Forms.CheckBox
        Me.SeaInclude = New System.Windows.Forms.RadioButton
        Me.SeaSuffix = New System.Windows.Forms.RadioButton
        Me.SeaPrefix = New System.Windows.Forms.RadioButton
        Me.SearchWord = New System.Windows.Forms.TextBox
        Me.Search = New System.Windows.Forms.Button
        Me.HistoryList = New System.Windows.Forms.ComboBox
        Me.MenuStrip.SuspendLayout()
        Me.Category.SuspendLayout()
        Me.SearchMode.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStripOnMain
        '
        Me.StatusStripOnMain.Location = New System.Drawing.Point(0, 298)
        Me.StatusStripOnMain.Name = "StatusStripOnMain"
        Me.StatusStripOnMain.Size = New System.Drawing.Size(520, 22)
        Me.StatusStripOnMain.SizingGrip = False
        Me.StatusStripOnMain.TabIndex = 8
        Me.StatusStripOnMain.Text = "MainStatusStrip"
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenuOnMenu, Me.EditMenuOnMain, Me.HistoryMenuOnMain, Me.BookmarkMenuOnMain, Me.ToolMenuOnMain, Me.HelpMenuOnMain})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip.Size = New System.Drawing.Size(520, 24)
        Me.MenuStrip.TabIndex = 0
        Me.MenuStrip.Text = "MenuStrip"
        '
        'FileMenuOnMenu
        '
        Me.FileMenuOnMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ReloadWdic, Me.OpenTestDialog, Me.CloseWdicGazer})
        Me.FileMenuOnMenu.Name = "FileMenuOnMenu"
        Me.FileMenuOnMenu.Size = New System.Drawing.Size(66, 20)
        Me.FileMenuOnMenu.Text = "ファイル(&F)"
        '
        'ReloadWdic
        '
        Me.ReloadWdic.Name = "ReloadWdic"
        Me.ReloadWdic.Size = New System.Drawing.Size(242, 22)
        Me.ReloadWdic.Text = "環境・辞書ファイルの再読み込み(&R)"
        '
        'OpenTestDialog
        '
        Me.OpenTestDialog.Name = "OpenTestDialog"
        Me.OpenTestDialog.Size = New System.Drawing.Size(242, 22)
        Me.OpenTestDialog.Text = "テスト用ダイアログ表示"
        Me.OpenTestDialog.Visible = False
        '
        'CloseWdicGazer
        '
        Me.CloseWdicGazer.Name = "CloseWdicGazer"
        Me.CloseWdicGazer.Size = New System.Drawing.Size(242, 22)
        Me.CloseWdicGazer.Text = "終了(&X)"
        '
        'EditMenuOnMain
        '
        Me.EditMenuOnMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutOnMain, Me.CopyOnMain, Me.PasteOnMain, Me.ToolStripSeparator1, Me.UndoOnMain, Me.ToolStripSeparator2, Me.SelectAllOnMenu})
        Me.EditMenuOnMain.Name = "EditMenuOnMain"
        Me.EditMenuOnMain.Size = New System.Drawing.Size(56, 20)
        Me.EditMenuOnMain.Text = "編集(&E)"
        '
        'CutOnMain
        '
        Me.CutOnMain.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutOnMain.Name = "CutOnMain"
        Me.CutOnMain.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CutOnMain.Size = New System.Drawing.Size(177, 22)
        Me.CutOnMain.Text = "切り取り(&T)"
        '
        'CopyOnMain
        '
        Me.CopyOnMain.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyOnMain.Name = "CopyOnMain"
        Me.CopyOnMain.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyOnMain.Size = New System.Drawing.Size(177, 22)
        Me.CopyOnMain.Text = "コピー(&C)"
        '
        'PasteOnMain
        '
        Me.PasteOnMain.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteOnMain.Name = "PasteOnMain"
        Me.PasteOnMain.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteOnMain.Size = New System.Drawing.Size(177, 22)
        Me.PasteOnMain.Text = "貼り付け(&P)"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(174, 6)
        '
        'UndoOnMain
        '
        Me.UndoOnMain.Name = "UndoOnMain"
        Me.UndoOnMain.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys)
        Me.UndoOnMain.Size = New System.Drawing.Size(177, 22)
        Me.UndoOnMain.Text = "元に戻す(&U)"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(174, 6)
        '
        'SelectAllOnMenu
        '
        Me.SelectAllOnMenu.Name = "SelectAllOnMenu"
        Me.SelectAllOnMenu.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllOnMenu.Size = New System.Drawing.Size(177, 22)
        Me.SelectAllOnMenu.Text = "すべて選択(&A)"
        '
        'HistoryMenuOnMain
        '
        Me.HistoryMenuOnMain.Name = "HistoryMenuOnMain"
        Me.HistoryMenuOnMain.Size = New System.Drawing.Size(56, 20)
        Me.HistoryMenuOnMain.Text = "履歴(&S)"
        '
        'BookmarkMenuOnMain
        '
        Me.BookmarkMenuOnMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditBookmarkOnMain, Me.ToolStripSeparator3})
        Me.BookmarkMenuOnMain.Name = "BookmarkMenuOnMain"
        Me.BookmarkMenuOnMain.Size = New System.Drawing.Size(84, 20)
        Me.BookmarkMenuOnMain.Text = "ブックマーク(&B)"
        '
        'EditBookmarkOnMain
        '
        Me.EditBookmarkOnMain.Name = "EditBookmarkOnMain"
        Me.EditBookmarkOnMain.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.EditBookmarkOnMain.Size = New System.Drawing.Size(223, 22)
        Me.EditBookmarkOnMain.Text = "ブックマークの管理"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(220, 6)
        '
        'ToolMenuOnMain
        '
        Me.ToolMenuOnMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenOption})
        Me.ToolMenuOnMain.Name = "ToolMenuOnMain"
        Me.ToolMenuOnMain.Size = New System.Drawing.Size(61, 20)
        Me.ToolMenuOnMain.Text = "ツール(&T)"
        '
        'OpenOption
        '
        Me.OpenOption.Name = "OpenOption"
        Me.OpenOption.Size = New System.Drawing.Size(129, 22)
        Me.OpenOption.Text = "オプション(&O)"
        '
        'HelpMenuOnMain
        '
        Me.HelpMenuOnMain.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenHelpOnMain, Me.DisplayVersion})
        Me.HelpMenuOnMain.Name = "HelpMenuOnMain"
        Me.HelpMenuOnMain.Size = New System.Drawing.Size(62, 20)
        Me.HelpMenuOnMain.Text = "ヘルプ(&H)"
        '
        'OpenHelpOnMain
        '
        Me.OpenHelpOnMain.ImageTransparentColor = System.Drawing.Color.Fuchsia
        Me.OpenHelpOnMain.Name = "OpenHelpOnMain"
        Me.OpenHelpOnMain.Size = New System.Drawing.Size(161, 22)
        Me.OpenHelpOnMain.Text = "ヘルプ(&H)"
        '
        'DisplayVersion
        '
        Me.DisplayVersion.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenAboutWdic, Me.OpenAboutWdicGazer})
        Me.DisplayVersion.Name = "DisplayVersion"
        Me.DisplayVersion.Size = New System.Drawing.Size(161, 22)
        Me.DisplayVersion.Text = "バージョン情報(&A)..."
        '
        'OpenAboutWdic
        '
        Me.OpenAboutWdic.Name = "OpenAboutWdic"
        Me.OpenAboutWdic.Size = New System.Drawing.Size(176, 22)
        Me.OpenAboutWdic.Text = "通信用語の基礎知識"
        '
        'OpenAboutWdicGazer
        '
        Me.OpenAboutWdicGazer.Name = "OpenAboutWdicGazer"
        Me.OpenAboutWdicGazer.Size = New System.Drawing.Size(176, 22)
        Me.OpenAboutWdicGazer.Text = "WDIC Gazer"
        '
        'ブックマークBToolStripMenuItem
        '
        Me.ブックマークBToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ブックマークの管理ToolStripMenuItem, Me.ToolStripSeparator8})
        Me.ブックマークBToolStripMenuItem.Name = "ブックマークBToolStripMenuItem"
        Me.ブックマークBToolStripMenuItem.Size = New System.Drawing.Size(84, 20)
        Me.ブックマークBToolStripMenuItem.Text = "ブックマーク(&B)"
        '
        'ブックマークの管理ToolStripMenuItem
        '
        Me.ブックマークの管理ToolStripMenuItem.Name = "ブックマークの管理ToolStripMenuItem"
        Me.ブックマークの管理ToolStripMenuItem.Size = New System.Drawing.Size(155, 22)
        Me.ブックマークの管理ToolStripMenuItem.Text = "ブックマークの管理"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(152, 6)
        '
        'ClearHistoryList
        '
        Me.ClearHistoryList.Location = New System.Drawing.Point(12, 64)
        Me.ClearHistoryList.Name = "ClearHistoryList"
        Me.ClearHistoryList.Size = New System.Drawing.Size(121, 23)
        Me.ClearHistoryList.TabIndex = 2
        Me.ClearHistoryList.Text = "検索履歴を消去する"
        Me.ClearHistoryList.UseVisualStyleBackColor = True
        '
        'Category
        '
        Me.Category.Controls.Add(Me.CatMoe)
        Me.Category.Controls.Add(Me.CatGame)
        Me.Category.Controls.Add(Me.CatMili)
        Me.Category.Controls.Add(Me.CatRail)
        Me.Category.Controls.Add(Me.CatTran)
        Me.Category.Controls.Add(Me.CatGeo)
        Me.Category.Controls.Add(Me.CatEdu)
        Me.Category.Controls.Add(Me.CatSoc)
        Me.Category.Controls.Add(Me.CatTel)
        Me.Category.Controls.Add(Me.CatCul)
        Me.Category.Controls.Add(Me.CatComm)
        Me.Category.Controls.Add(Me.CatNat)
        Me.Category.Controls.Add(Me.CatAll)
        Me.Category.Controls.Add(Me.CatTech)
        Me.Category.Controls.Add(Me.CatComp)
        Me.Category.Location = New System.Drawing.Point(12, 173)
        Me.Category.MaximumSize = New System.Drawing.Size(500, 115)
        Me.Category.MinimumSize = New System.Drawing.Size(500, 115)
        Me.Category.Name = "Category"
        Me.Category.Size = New System.Drawing.Size(500, 115)
        Me.Category.TabIndex = 7
        Me.Category.TabStop = False
        Me.Category.Text = "検索するカテゴリ"
        '
        'CatMoe
        '
        Me.CatMoe.AutoSize = True
        Me.CatMoe.Location = New System.Drawing.Point(84, 85)
        Me.CatMoe.Name = "CatMoe"
        Me.CatMoe.Size = New System.Drawing.Size(45, 16)
        Me.CatMoe.TabIndex = 12
        Me.CatMoe.Text = "萌え"
        Me.CatMoe.UseVisualStyleBackColor = True
        '
        'CatGame
        '
        Me.CatGame.AutoSize = True
        Me.CatGame.Location = New System.Drawing.Point(162, 85)
        Me.CatGame.Name = "CatGame"
        Me.CatGame.Size = New System.Drawing.Size(54, 16)
        Me.CatGame.TabIndex = 13
        Me.CatGame.Text = "ゲーム"
        Me.CatGame.UseVisualStyleBackColor = True
        '
        'CatMili
        '
        Me.CatMili.AutoSize = True
        Me.CatMili.Location = New System.Drawing.Point(244, 85)
        Me.CatMili.Name = "CatMili"
        Me.CatMili.Size = New System.Drawing.Size(48, 16)
        Me.CatMili.TabIndex = 14
        Me.CatMili.Text = "軍事"
        Me.CatMili.UseVisualStyleBackColor = True
        '
        'CatRail
        '
        Me.CatRail.AutoSize = True
        Me.CatRail.Location = New System.Drawing.Point(6, 85)
        Me.CatRail.Name = "CatRail"
        Me.CatRail.Size = New System.Drawing.Size(48, 16)
        Me.CatRail.TabIndex = 11
        Me.CatRail.Text = "鉄道"
        Me.CatRail.UseVisualStyleBackColor = True
        '
        'CatTran
        '
        Me.CatTran.AutoSize = True
        Me.CatTran.Location = New System.Drawing.Point(328, 63)
        Me.CatTran.Name = "CatTran"
        Me.CatTran.Size = New System.Drawing.Size(48, 16)
        Me.CatTran.TabIndex = 10
        Me.CatTran.Text = "運輸"
        Me.CatTran.UseVisualStyleBackColor = True
        '
        'CatGeo
        '
        Me.CatGeo.AutoSize = True
        Me.CatGeo.Location = New System.Drawing.Point(244, 62)
        Me.CatGeo.Name = "CatGeo"
        Me.CatGeo.Size = New System.Drawing.Size(48, 16)
        Me.CatGeo.TabIndex = 9
        Me.CatGeo.Text = "地理"
        Me.CatGeo.UseVisualStyleBackColor = True
        '
        'CatEdu
        '
        Me.CatEdu.AutoSize = True
        Me.CatEdu.Location = New System.Drawing.Point(162, 62)
        Me.CatEdu.Name = "CatEdu"
        Me.CatEdu.Size = New System.Drawing.Size(48, 16)
        Me.CatEdu.TabIndex = 8
        Me.CatEdu.Text = "教育"
        Me.CatEdu.UseVisualStyleBackColor = True
        '
        'CatSoc
        '
        Me.CatSoc.AutoSize = True
        Me.CatSoc.Location = New System.Drawing.Point(84, 63)
        Me.CatSoc.Name = "CatSoc"
        Me.CatSoc.Size = New System.Drawing.Size(72, 16)
        Me.CatSoc.TabIndex = 7
        Me.CatSoc.Text = "社会科学"
        Me.CatSoc.UseVisualStyleBackColor = True
        '
        'CatTel
        '
        Me.CatTel.AutoSize = True
        Me.CatTel.Location = New System.Drawing.Point(84, 40)
        Me.CatTel.Name = "CatTel"
        Me.CatTel.Size = New System.Drawing.Size(48, 16)
        Me.CatTel.TabIndex = 2
        Me.CatTel.Text = "電話"
        Me.CatTel.UseVisualStyleBackColor = True
        '
        'CatCul
        '
        Me.CatCul.AutoSize = True
        Me.CatCul.Location = New System.Drawing.Point(6, 63)
        Me.CatCul.Name = "CatCul"
        Me.CatCul.Size = New System.Drawing.Size(72, 16)
        Me.CatCul.TabIndex = 6
        Me.CatCul.Text = "人文科学"
        Me.CatCul.UseVisualStyleBackColor = True
        '
        'CatComm
        '
        Me.CatComm.AutoSize = True
        Me.CatComm.Location = New System.Drawing.Point(6, 40)
        Me.CatComm.Name = "CatComm"
        Me.CatComm.Size = New System.Drawing.Size(48, 16)
        Me.CatComm.TabIndex = 1
        Me.CatComm.Text = "通信"
        Me.CatComm.UseVisualStyleBackColor = True
        '
        'CatNat
        '
        Me.CatNat.AutoSize = True
        Me.CatNat.Location = New System.Drawing.Point(328, 40)
        Me.CatNat.Name = "CatNat"
        Me.CatNat.Size = New System.Drawing.Size(72, 16)
        Me.CatNat.TabIndex = 5
        Me.CatNat.Text = "自然科学"
        Me.CatNat.UseVisualStyleBackColor = True
        '
        'CatAll
        '
        Me.CatAll.AutoSize = True
        Me.CatAll.Location = New System.Drawing.Point(6, 18)
        Me.CatAll.Name = "CatAll"
        Me.CatAll.Size = New System.Drawing.Size(70, 16)
        Me.CatAll.TabIndex = 0
        Me.CatAll.Text = "全カテゴリ"
        Me.CatAll.UseVisualStyleBackColor = True
        '
        'CatTech
        '
        Me.CatTech.AutoSize = True
        Me.CatTech.Location = New System.Drawing.Point(244, 40)
        Me.CatTech.Name = "CatTech"
        Me.CatTech.Size = New System.Drawing.Size(78, 16)
        Me.CatTech.TabIndex = 4
        Me.CatTech.Text = "技術・工学"
        Me.CatTech.UseVisualStyleBackColor = True
        '
        'CatComp
        '
        Me.CatComp.AutoSize = True
        Me.CatComp.Location = New System.Drawing.Point(162, 40)
        Me.CatComp.Name = "CatComp"
        Me.CatComp.Size = New System.Drawing.Size(76, 16)
        Me.CatComp.TabIndex = 3
        Me.CatComp.Text = "コンピュータ"
        Me.CatComp.UseVisualStyleBackColor = True
        '
        'SearchMode
        '
        Me.SearchMode.Controls.Add(Me.SeaCapitalCheck)
        Me.SearchMode.Controls.Add(Me.SeaPerfect)
        Me.SearchMode.Controls.Add(Me.SeaRegex)
        Me.SearchMode.Controls.Add(Me.SeaTextSearch)
        Me.SearchMode.Controls.Add(Me.SeaInclude)
        Me.SearchMode.Controls.Add(Me.SeaSuffix)
        Me.SearchMode.Controls.Add(Me.SeaPrefix)
        Me.SearchMode.Location = New System.Drawing.Point(12, 100)
        Me.SearchMode.Name = "SearchMode"
        Me.SearchMode.Size = New System.Drawing.Size(502, 67)
        Me.SearchMode.TabIndex = 6
        Me.SearchMode.TabStop = False
        Me.SearchMode.Text = "検索方法"
        '
        'SeaCapitalCheck
        '
        Me.SeaCapitalCheck.AutoSize = True
        Me.SeaCapitalCheck.Location = New System.Drawing.Point(6, 41)
        Me.SeaCapitalCheck.Name = "SeaCapitalCheck"
        Me.SeaCapitalCheck.Size = New System.Drawing.Size(158, 16)
        Me.SeaCapitalCheck.TabIndex = 9
        Me.SeaCapitalCheck.Text = "大文字小文字の区別を行う"
        Me.SeaCapitalCheck.UseVisualStyleBackColor = True
        '
        'SeaPerfect
        '
        Me.SeaPerfect.AutoSize = True
        Me.SeaPerfect.Location = New System.Drawing.Point(230, 19)
        Me.SeaPerfect.Name = "SeaPerfect"
        Me.SeaPerfect.Size = New System.Drawing.Size(71, 16)
        Me.SeaPerfect.TabIndex = 8
        Me.SeaPerfect.TabStop = True
        Me.SeaPerfect.Text = "完全一致"
        Me.SeaPerfect.UseVisualStyleBackColor = True
        '
        'SeaRegex
        '
        Me.SeaRegex.AutoSize = True
        Me.SeaRegex.Location = New System.Drawing.Point(305, 19)
        Me.SeaRegex.Name = "SeaRegex"
        Me.SeaRegex.Size = New System.Drawing.Size(71, 16)
        Me.SeaRegex.TabIndex = 5
        Me.SeaRegex.TabStop = True
        Me.SeaRegex.Text = "正規表現"
        Me.SeaRegex.UseVisualStyleBackColor = True
        '
        'SeaTextSearch
        '
        Me.SeaTextSearch.AutoSize = True
        Me.SeaTextSearch.Location = New System.Drawing.Point(382, 20)
        Me.SeaTextSearch.Name = "SeaTextSearch"
        Me.SeaTextSearch.Size = New System.Drawing.Size(72, 16)
        Me.SeaTextSearch.TabIndex = 4
        Me.SeaTextSearch.Text = "本文検索"
        Me.SeaTextSearch.UseVisualStyleBackColor = True
        '
        'SeaInclude
        '
        Me.SeaInclude.AutoSize = True
        Me.SeaInclude.Location = New System.Drawing.Point(160, 19)
        Me.SeaInclude.Name = "SeaInclude"
        Me.SeaInclude.Size = New System.Drawing.Size(64, 16)
        Me.SeaInclude.TabIndex = 2
        Me.SeaInclude.Text = "含まれる"
        Me.SeaInclude.UseVisualStyleBackColor = True
        '
        'SeaSuffix
        '
        Me.SeaSuffix.AutoSize = True
        Me.SeaSuffix.Location = New System.Drawing.Point(83, 19)
        Me.SeaSuffix.Name = "SeaSuffix"
        Me.SeaSuffix.Size = New System.Drawing.Size(71, 16)
        Me.SeaSuffix.TabIndex = 1
        Me.SeaSuffix.Text = "後方一致"
        Me.SeaSuffix.UseVisualStyleBackColor = True
        '
        'SeaPrefix
        '
        Me.SeaPrefix.AutoSize = True
        Me.SeaPrefix.Checked = True
        Me.SeaPrefix.Location = New System.Drawing.Point(6, 19)
        Me.SeaPrefix.Name = "SeaPrefix"
        Me.SeaPrefix.Size = New System.Drawing.Size(71, 16)
        Me.SeaPrefix.TabIndex = 0
        Me.SeaPrefix.TabStop = True
        Me.SeaPrefix.Text = "前方一致"
        Me.SeaPrefix.UseVisualStyleBackColor = True
        '
        'SearchWord
        '
        Me.SearchWord.Location = New System.Drawing.Point(201, 39)
        Me.SearchWord.Name = "SearchWord"
        Me.SearchWord.Size = New System.Drawing.Size(232, 19)
        Me.SearchWord.TabIndex = 3
        '
        'Search
        '
        Me.Search.Location = New System.Drawing.Point(439, 37)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(75, 23)
        Me.Search.TabIndex = 24
        Me.Search.Text = "検索"
        Me.Search.UseVisualStyleBackColor = True
        '
        'HistoryList
        '
        Me.HistoryList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.HistoryList.Location = New System.Drawing.Point(12, 38)
        Me.HistoryList.Name = "HistoryList"
        Me.HistoryList.Size = New System.Drawing.Size(183, 20)
        Me.HistoryList.TabIndex = 1
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(520, 320)
        Me.Controls.Add(Me.StatusStripOnMain)
        Me.Controls.Add(Me.MenuStrip)
        Me.Controls.Add(Me.ClearHistoryList)
        Me.Controls.Add(Me.Category)
        Me.Controls.Add(Me.SearchMode)
        Me.Controls.Add(Me.SearchWord)
        Me.Controls.Add(Me.Search)
        Me.Controls.Add(Me.HistoryList)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Main"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "WDIC Gazer"
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.Category.ResumeLayout(False)
        Me.Category.PerformLayout()
        Me.SearchMode.ResumeLayout(False)
        Me.SearchMode.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStripOnMain As System.Windows.Forms.StatusStrip
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents HistoryList As System.Windows.Forms.ComboBox
    Friend WithEvents ClearHistoryList As System.Windows.Forms.Button
    Friend WithEvents Search As System.Windows.Forms.Button
    Friend WithEvents CatAll As System.Windows.Forms.CheckBox
    Friend WithEvents CatComp As System.Windows.Forms.CheckBox
    Friend WithEvents CatTech As System.Windows.Forms.CheckBox
    Friend WithEvents CatNat As System.Windows.Forms.CheckBox
    Friend WithEvents CatComm As System.Windows.Forms.CheckBox
    Friend WithEvents CatTel As System.Windows.Forms.CheckBox
    Friend WithEvents CatCul As System.Windows.Forms.CheckBox
    Friend WithEvents Category As System.Windows.Forms.GroupBox
    Friend WithEvents CatMili As System.Windows.Forms.CheckBox
    Friend WithEvents CatRail As System.Windows.Forms.CheckBox
    Friend WithEvents CatTran As System.Windows.Forms.CheckBox
    Friend WithEvents CatGeo As System.Windows.Forms.CheckBox
    Friend WithEvents CatEdu As System.Windows.Forms.CheckBox
    Friend WithEvents CatSoc As System.Windows.Forms.CheckBox
    Friend WithEvents SearchWord As System.Windows.Forms.TextBox
    Friend WithEvents CatMoe As System.Windows.Forms.CheckBox
    Friend WithEvents CatGame As System.Windows.Forms.CheckBox
    Friend WithEvents SearchMode As System.Windows.Forms.GroupBox
    Friend WithEvents SeaInclude As System.Windows.Forms.RadioButton
    Friend WithEvents SeaSuffix As System.Windows.Forms.RadioButton
    Friend WithEvents SeaPrefix As System.Windows.Forms.RadioButton
    Friend WithEvents SeaTextSearch As System.Windows.Forms.CheckBox
    Friend WithEvents FileMenuOnMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseWdicGazer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditMenuOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolMenuOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenOption As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpMenuOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisplayVersion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenAboutWdic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenAboutWdicGazer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenHelpOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SeaRegex As System.Windows.Forms.RadioButton
    Friend WithEvents SeaPerfect As System.Windows.Forms.RadioButton
    Friend WithEvents SeaCapitalCheck As System.Windows.Forms.CheckBox
    Friend WithEvents ReloadWdic As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UndoOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllOnMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ブックマークBToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ブックマークの管理ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenTestDialog As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BookmarkMenuOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditBookmarkOnMain As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HistoryMenuOnMain As System.Windows.Forms.ToolStripMenuItem

End Class
