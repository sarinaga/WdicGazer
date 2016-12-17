<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class WordDisplay
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Windows フォーム デザイナで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナで必要です。
    'Windows フォーム デザイナを使用して変更できます。  
    'コード エディタを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(WordDisplay))
        Me.DisplayTab = New System.Windows.Forms.TabControl
        Me.ContextMenuTab = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CloseThisTab = New System.Windows.Forms.ToolStripMenuItem
        Me.BookmarkThisTab = New System.Windows.Forms.ToolStripMenuItem
        Me.Separator0 = New System.Windows.Forms.ToolStripSeparator
        Me.CloseOtherTab = New System.Windows.Forms.ToolStripMenuItem
        Me.CloseLeftTab = New System.Windows.Forms.ToolStripMenuItem
        Me.CloseRightTab = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.OpenTextEditorOnContextMenuTab = New System.Windows.Forms.ToolStripMenuItem
        Me.Empty = New System.Windows.Forms.TabPage
        Me.DiscriptionBrowser = New System.Windows.Forms.RichTextBox
        Me.ContextMenuItem = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ItemCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.Pron = New System.Windows.Forms.ListBox
        Me.lblPron = New System.Windows.Forms.Label
        Me.OtherName = New System.Windows.Forms.ListBox
        Me.lblOtherName = New System.Windows.Forms.Label
        Me.Abbr = New System.Windows.Forms.ListBox
        Me.lblAbbr = New System.Windows.Forms.Label
        Me.English = New System.Windows.Forms.ListBox
        Me.lblEnglish = New System.Windows.Forms.Label
        Me.Word = New System.Windows.Forms.TextBox
        Me.History = New System.Windows.Forms.ListBox
        Me.lblHistory = New System.Windows.Forms.Label
        Me.lblReadJapanese = New System.Windows.Forms.Label
        Me.lblDictonary = New System.Windows.Forms.Label
        Me.Pos = New System.Windows.Forms.TextBox
        Me.lblPos = New System.Windows.Forms.Label
        Me.ReadJapanese = New System.Windows.Forms.ListBox
        Me.Dictonary = New System.Windows.Forms.TextBox
        Me.WordHeaderPanel = New System.Windows.Forms.Panel
        Me.Category = New System.Windows.Forms.ListBox
        Me.lblCategory = New System.Windows.Forms.Label
        Me.MenuStrip = New System.Windows.Forms.MenuStrip
        Me.FileMenuOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenTextEditorOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.PrintOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintPreviewOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.CloseOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.EditMenuOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.CutOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.PasteOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAllOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.DisplayMenu = New System.Windows.Forms.ToolStripMenuItem
        Me.ViewWordHeader = New System.Windows.Forms.ToolStripMenuItem
        Me.HistoryMenuOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.BookMarkMenuOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.SetBookmark = New System.Windows.Forms.ToolStripMenuItem
        Me.SetBookmarkAllWord = New System.Windows.Forms.ToolStripMenuItem
        Me.EditBookmarkOnWord = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.LinkNavigation = New System.Windows.Forms.ToolStripStatusLabel
        Me.foo = New System.Windows.Forms.ToolStripStatusLabel
        Me.ToolTipOnWordDisplay = New System.Windows.Forms.ToolTip(Me.components)
        Me.ContextMenuBrowserOnLink = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.OpenLink = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenLinkNewTab = New System.Windows.Forms.ToolStripMenuItem
        Me.Sep1 = New System.Windows.Forms.ToolStripSeparator
        Me.WordCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.LinkCopy = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuBrowserOffLink = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.GoBack = New System.Windows.Forms.ToolStripMenuItem
        Me.GoForward = New System.Windows.Forms.ToolStripMenuItem
        Me.Sep2 = New System.Windows.Forms.ToolStripSeparator
        Me.BookMarkThisWord = New System.Windows.Forms.ToolStripMenuItem
        Me.Sep3 = New System.Windows.Forms.ToolStripSeparator
        Me.AllSelect = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.OpenTextEditorOnContextBrowserOffLink = New System.Windows.Forms.ToolStripMenuItem
        Me.ContextMenuBrowserSelected = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AllSelectInSelected = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyOnBrowser = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolIconOnWordDisplay = New System.Windows.Forms.ToolStrip
        Me.GoBackOnToolStrip = New System.Windows.Forms.ToolStripSplitButton
        Me.GoForwardOnToolStrip = New System.Windows.Forms.ToolStripSplitButton
        Me.PrintOutOnToolStrip = New System.Windows.Forms.ToolStripButton
        Me.HelpOnToolStrip = New System.Windows.Forms.ToolStripButton
        Me.DisplayTab.SuspendLayout()
        Me.ContextMenuTab.SuspendLayout()
        Me.Empty.SuspendLayout()
        Me.ContextMenuItem.SuspendLayout()
        Me.WordHeaderPanel.SuspendLayout()
        Me.MenuStrip.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.ContextMenuBrowserOnLink.SuspendLayout()
        Me.ContextMenuBrowserOffLink.SuspendLayout()
        Me.ContextMenuBrowserSelected.SuspendLayout()
        Me.ToolIconOnWordDisplay.SuspendLayout()
        Me.SuspendLayout()
        '
        'DisplayTab
        '
        Me.DisplayTab.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DisplayTab.ContextMenuStrip = Me.ContextMenuTab
        Me.DisplayTab.Controls.Add(Me.Empty)
        Me.DisplayTab.Location = New System.Drawing.Point(12, 52)
        Me.DisplayTab.Name = "DisplayTab"
        Me.DisplayTab.SelectedIndex = 0
        Me.DisplayTab.Size = New System.Drawing.Size(768, 676)
        Me.DisplayTab.TabIndex = 16
        '
        'ContextMenuTab
        '
        Me.ContextMenuTab.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CloseThisTab, Me.BookmarkThisTab, Me.Separator0, Me.CloseOtherTab, Me.CloseLeftTab, Me.CloseRightTab, Me.ToolStripSeparator8, Me.OpenTextEditorOnContextMenuTab})
        Me.ContextMenuTab.Name = "RightClickMenu"
        Me.ContextMenuTab.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuTab.Size = New System.Drawing.Size(265, 148)
        '
        'CloseThisTab
        '
        Me.CloseThisTab.Name = "CloseThisTab"
        Me.CloseThisTab.Size = New System.Drawing.Size(264, 22)
        Me.CloseThisTab.Text = "このタブを閉じる(&C)"
        '
        'BookmarkThisTab
        '
        Me.BookmarkThisTab.Name = "BookmarkThisTab"
        Me.BookmarkThisTab.Size = New System.Drawing.Size(264, 22)
        Me.BookmarkThisTab.Text = "この項目をブックマークする(&B)"
        '
        'Separator0
        '
        Me.Separator0.Name = "Separator0"
        Me.Separator0.Size = New System.Drawing.Size(261, 6)
        '
        'CloseOtherTab
        '
        Me.CloseOtherTab.Name = "CloseOtherTab"
        Me.CloseOtherTab.Size = New System.Drawing.Size(264, 22)
        Me.CloseOtherTab.Text = "このタブ以外を閉じる(&O)"
        '
        'CloseLeftTab
        '
        Me.CloseLeftTab.Name = "CloseLeftTab"
        Me.CloseLeftTab.Size = New System.Drawing.Size(264, 22)
        Me.CloseLeftTab.Text = "これより左のタブを閉じる(&L)"
        '
        'CloseRightTab
        '
        Me.CloseRightTab.Name = "CloseRightTab"
        Me.CloseRightTab.Size = New System.Drawing.Size(264, 22)
        Me.CloseRightTab.Text = "これより右のタブを閉じる(&R)"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(261, 6)
        '
        'OpenTextEditorOnContextMenuTab
        '
        Me.OpenTextEditorOnContextMenuTab.Name = "OpenTextEditorOnContextMenuTab"
        Me.OpenTextEditorOnContextMenuTab.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.OpenTextEditorOnContextMenuTab.Size = New System.Drawing.Size(264, 22)
        Me.OpenTextEditorOnContextMenuTab.Text = "テキストエディタで項目を表示する"
        '
        'Empty
        '
        Me.Empty.ContextMenuStrip = Me.ContextMenuTab
        Me.Empty.Controls.Add(Me.DiscriptionBrowser)
        Me.Empty.Controls.Add(Me.Pron)
        Me.Empty.Controls.Add(Me.lblPron)
        Me.Empty.Controls.Add(Me.OtherName)
        Me.Empty.Controls.Add(Me.lblOtherName)
        Me.Empty.Controls.Add(Me.Abbr)
        Me.Empty.Controls.Add(Me.lblAbbr)
        Me.Empty.Controls.Add(Me.English)
        Me.Empty.Controls.Add(Me.lblEnglish)
        Me.Empty.Controls.Add(Me.Word)
        Me.Empty.Controls.Add(Me.History)
        Me.Empty.Controls.Add(Me.lblHistory)
        Me.Empty.Controls.Add(Me.lblReadJapanese)
        Me.Empty.Controls.Add(Me.lblDictonary)
        Me.Empty.Controls.Add(Me.Pos)
        Me.Empty.Controls.Add(Me.lblPos)
        Me.Empty.Controls.Add(Me.ReadJapanese)
        Me.Empty.Controls.Add(Me.Dictonary)
        Me.Empty.Controls.Add(Me.WordHeaderPanel)
        Me.Empty.Location = New System.Drawing.Point(4, 21)
        Me.Empty.Name = "Empty"
        Me.Empty.Padding = New System.Windows.Forms.Padding(3)
        Me.Empty.Size = New System.Drawing.Size(760, 651)
        Me.Empty.TabIndex = 0
        Me.Empty.UseVisualStyleBackColor = True
        '
        'DiscriptionBrowser
        '
        Me.DiscriptionBrowser.ContextMenuStrip = Me.ContextMenuItem
        Me.DiscriptionBrowser.DetectUrls = False
        Me.DiscriptionBrowser.Location = New System.Drawing.Point(10, 222)
        Me.DiscriptionBrowser.Name = "DiscriptionBrowser"
        Me.DiscriptionBrowser.Size = New System.Drawing.Size(744, 414)
        Me.DiscriptionBrowser.TabIndex = 24
        Me.DiscriptionBrowser.Text = Global.WdicGazer.My.Resources.Resources.RFCEditorDir
        '
        'ContextMenuItem
        '
        Me.ContextMenuItem.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ItemCopy})
        Me.ContextMenuItem.Name = "ContextMenuControl"
        Me.ContextMenuItem.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuItem.Size = New System.Drawing.Size(215, 26)
        '
        'ItemCopy
        '
        Me.ItemCopy.Name = "ItemCopy"
        Me.ItemCopy.Size = New System.Drawing.Size(214, 22)
        Me.ItemCopy.Text = "このアイテムの内容をコピーする"
        '
        'Pron
        '
        Me.Pron.ContextMenuStrip = Me.ContextMenuItem
        Me.Pron.FormattingEnabled = True
        Me.Pron.ItemHeight = 12
        Me.Pron.Location = New System.Drawing.Point(53, 90)
        Me.Pron.Name = "Pron"
        Me.Pron.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.Pron.Size = New System.Drawing.Size(120, 16)
        Me.Pron.TabIndex = 23
        '
        'lblPron
        '
        Me.lblPron.AutoSize = True
        Me.lblPron.Location = New System.Drawing.Point(8, 90)
        Me.lblPron.Name = "lblPron"
        Me.lblPron.Size = New System.Drawing.Size(29, 12)
        Me.lblPron.TabIndex = 22
        Me.lblPron.Text = "発音"
        '
        'OtherName
        '
        Me.OtherName.ContextMenuStrip = Me.ContextMenuItem
        Me.OtherName.FormattingEnabled = True
        Me.OtherName.ItemHeight = 12
        Me.OtherName.Location = New System.Drawing.Point(393, 90)
        Me.OtherName.Name = "OtherName"
        Me.OtherName.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.OtherName.Size = New System.Drawing.Size(120, 16)
        Me.OtherName.TabIndex = 21
        Me.OtherName.Visible = False
        '
        'lblOtherName
        '
        Me.lblOtherName.AutoSize = True
        Me.lblOtherName.Location = New System.Drawing.Point(358, 90)
        Me.lblOtherName.Name = "lblOtherName"
        Me.lblOtherName.Size = New System.Drawing.Size(29, 12)
        Me.lblOtherName.TabIndex = 20
        Me.lblOtherName.Text = "別名"
        Me.lblOtherName.Visible = False
        '
        'Abbr
        '
        Me.Abbr.ContextMenuStrip = Me.ContextMenuItem
        Me.Abbr.FormattingEnabled = True
        Me.Abbr.ItemHeight = 12
        Me.Abbr.Location = New System.Drawing.Point(393, 68)
        Me.Abbr.Name = "Abbr"
        Me.Abbr.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.Abbr.Size = New System.Drawing.Size(120, 16)
        Me.Abbr.TabIndex = 19
        '
        'lblAbbr
        '
        Me.lblAbbr.AutoSize = True
        Me.lblAbbr.Location = New System.Drawing.Point(358, 68)
        Me.lblAbbr.Name = "lblAbbr"
        Me.lblAbbr.Size = New System.Drawing.Size(29, 12)
        Me.lblAbbr.TabIndex = 18
        Me.lblAbbr.Text = "略語"
        '
        'English
        '
        Me.English.ContextMenuStrip = Me.ContextMenuItem
        Me.English.FormattingEnabled = True
        Me.English.ItemHeight = 12
        Me.English.Location = New System.Drawing.Point(53, 68)
        Me.English.Name = "English"
        Me.English.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.English.Size = New System.Drawing.Size(287, 16)
        Me.English.TabIndex = 17
        '
        'lblEnglish
        '
        Me.lblEnglish.AutoSize = True
        Me.lblEnglish.Location = New System.Drawing.Point(8, 68)
        Me.lblEnglish.Name = "lblEnglish"
        Me.lblEnglish.Size = New System.Drawing.Size(29, 12)
        Me.lblEnglish.TabIndex = 16
        Me.lblEnglish.Text = "英語"
        '
        'Word
        '
        Me.Word.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Word.BackColor = System.Drawing.SystemColors.Window
        Me.Word.Font = New System.Drawing.Font("MS UI Gothic", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Word.Location = New System.Drawing.Point(6, 6)
        Me.Word.Name = "Word"
        Me.Word.ReadOnly = True
        Me.Word.Size = New System.Drawing.Size(748, 34)
        Me.Word.TabIndex = 2
        Me.Word.Text = "単語"
        '
        'History
        '
        Me.History.ContextMenuStrip = Me.ContextMenuItem
        Me.History.FormattingEnabled = True
        Me.History.ItemHeight = 12
        Me.History.Location = New System.Drawing.Point(393, 46)
        Me.History.Name = "History"
        Me.History.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.History.Size = New System.Drawing.Size(120, 16)
        Me.History.TabIndex = 15
        '
        'lblHistory
        '
        Me.lblHistory.AutoSize = True
        Me.lblHistory.Location = New System.Drawing.Point(358, 46)
        Me.lblHistory.Name = "lblHistory"
        Me.lblHistory.Size = New System.Drawing.Size(29, 12)
        Me.lblHistory.TabIndex = 13
        Me.lblHistory.Text = "履歴"
        '
        'lblReadJapanese
        '
        Me.lblReadJapanese.AutoSize = True
        Me.lblReadJapanese.Location = New System.Drawing.Point(8, 46)
        Me.lblReadJapanese.Name = "lblReadJapanese"
        Me.lblReadJapanese.Size = New System.Drawing.Size(28, 12)
        Me.lblReadJapanese.TabIndex = 6
        Me.lblReadJapanese.Text = "読み"
        '
        'lblDictonary
        '
        Me.lblDictonary.AutoSize = True
        Me.lblDictonary.Location = New System.Drawing.Point(8, 140)
        Me.lblDictonary.Name = "lblDictonary"
        Me.lblDictonary.Size = New System.Drawing.Size(29, 12)
        Me.lblDictonary.TabIndex = 5
        Me.lblDictonary.Text = "辞書"
        '
        'Pos
        '
        Me.Pos.BackColor = System.Drawing.SystemColors.Window
        Me.Pos.ContextMenuStrip = Me.ContextMenuItem
        Me.Pos.Location = New System.Drawing.Point(53, 112)
        Me.Pos.Name = "Pos"
        Me.Pos.ReadOnly = True
        Me.Pos.Size = New System.Drawing.Size(161, 19)
        Me.Pos.TabIndex = 9
        Me.Pos.Text = "品詞"
        '
        'lblPos
        '
        Me.lblPos.AutoSize = True
        Me.lblPos.Location = New System.Drawing.Point(8, 115)
        Me.lblPos.Name = "lblPos"
        Me.lblPos.Size = New System.Drawing.Size(29, 12)
        Me.lblPos.TabIndex = 3
        Me.lblPos.Text = "品詞"
        '
        'ReadJapanese
        '
        Me.ReadJapanese.ContextMenuStrip = Me.ContextMenuItem
        Me.ReadJapanese.FormattingEnabled = True
        Me.ReadJapanese.ItemHeight = 12
        Me.ReadJapanese.Location = New System.Drawing.Point(53, 46)
        Me.ReadJapanese.Name = "ReadJapanese"
        Me.ReadJapanese.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.ReadJapanese.Size = New System.Drawing.Size(287, 16)
        Me.ReadJapanese.TabIndex = 12
        '
        'Dictonary
        '
        Me.Dictonary.BackColor = System.Drawing.SystemColors.Window
        Me.Dictonary.ContextMenuStrip = Me.ContextMenuItem
        Me.Dictonary.Location = New System.Drawing.Point(53, 137)
        Me.Dictonary.Name = "Dictonary"
        Me.Dictonary.ReadOnly = True
        Me.Dictonary.Size = New System.Drawing.Size(314, 19)
        Me.Dictonary.TabIndex = 8
        Me.Dictonary.Text = "辞書"
        '
        'WordHeaderPanel
        '
        Me.WordHeaderPanel.Controls.Add(Me.Category)
        Me.WordHeaderPanel.Controls.Add(Me.lblCategory)
        Me.WordHeaderPanel.Location = New System.Drawing.Point(6, 46)
        Me.WordHeaderPanel.Name = "WordHeaderPanel"
        Me.WordHeaderPanel.Size = New System.Drawing.Size(748, 178)
        Me.WordHeaderPanel.TabIndex = 25
        '
        'Category
        '
        Me.Category.ContextMenuStrip = Me.ContextMenuItem
        Me.Category.FormattingEnabled = True
        Me.Category.ItemHeight = 12
        Me.Category.Location = New System.Drawing.Point(43, 120)
        Me.Category.Name = "Category"
        Me.Category.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.Category.Size = New System.Drawing.Size(460, 40)
        Me.Category.TabIndex = 11
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Location = New System.Drawing.Point(-2, 120)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(39, 12)
        Me.lblCategory.TabIndex = 4
        Me.lblCategory.Text = "カテゴリ"
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenuOnWord, Me.EditMenuOnWord, Me.DisplayMenu, Me.HistoryMenuOnWord, Me.BookMarkMenuOnWord})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip.Size = New System.Drawing.Size(792, 24)
        Me.MenuStrip.TabIndex = 17
        Me.MenuStrip.Text = "MenuStrip"
        '
        'FileMenuOnWord
        '
        Me.FileMenuOnWord.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenTextEditorOnWord, Me.ToolStripSeparator7, Me.PrintOnWord, Me.PrintPreviewOnWord, Me.ToolStripSeparator2, Me.CloseOnWord})
        Me.FileMenuOnWord.Name = "FileMenuOnWord"
        Me.FileMenuOnWord.Size = New System.Drawing.Size(66, 20)
        Me.FileMenuOnWord.Text = "ファイル(&F)"
        '
        'OpenTextEditorOnWord
        '
        Me.OpenTextEditorOnWord.Name = "OpenTextEditorOnWord"
        Me.OpenTextEditorOnWord.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.OpenTextEditorOnWord.Size = New System.Drawing.Size(257, 22)
        Me.OpenTextEditorOnWord.Text = "テキストエディタでこの項目を開く"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(254, 6)
        '
        'PrintOnWord
        '
        Me.PrintOnWord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintOnWord.Name = "PrintOnWord"
        Me.PrintOnWord.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintOnWord.Size = New System.Drawing.Size(257, 22)
        Me.PrintOnWord.Text = "印刷(&P)"
        Me.PrintOnWord.Visible = False
        '
        'PrintPreviewOnWord
        '
        Me.PrintPreviewOnWord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewOnWord.Name = "PrintPreviewOnWord"
        Me.PrintPreviewOnWord.Size = New System.Drawing.Size(257, 22)
        Me.PrintPreviewOnWord.Text = "印刷プレビュー(&V)"
        Me.PrintPreviewOnWord.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(254, 6)
        Me.ToolStripSeparator2.Visible = False
        '
        'CloseOnWord
        '
        Me.CloseOnWord.Name = "CloseOnWord"
        Me.CloseOnWord.Size = New System.Drawing.Size(257, 22)
        Me.CloseOnWord.Text = "閉じる(&W)"
        '
        'EditMenuOnWord
        '
        Me.EditMenuOnWord.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutOnWord, Me.CopyOnWord, Me.PasteOnWord, Me.toolStripSeparator4, Me.SelectAllOnWord})
        Me.EditMenuOnWord.Name = "EditMenuOnWord"
        Me.EditMenuOnWord.Size = New System.Drawing.Size(56, 20)
        Me.EditMenuOnWord.Text = "編集(&E)"
        '
        'CutOnWord
        '
        Me.CutOnWord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutOnWord.Name = "CutOnWord"
        Me.CutOnWord.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys)
        Me.CutOnWord.Size = New System.Drawing.Size(177, 22)
        Me.CutOnWord.Text = "切り取り(&T)"
        '
        'CopyOnWord
        '
        Me.CopyOnWord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyOnWord.Name = "CopyOnWord"
        Me.CopyOnWord.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyOnWord.Size = New System.Drawing.Size(177, 22)
        Me.CopyOnWord.Text = "コピー(&C)"
        '
        'PasteOnWord
        '
        Me.PasteOnWord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PasteOnWord.Name = "PasteOnWord"
        Me.PasteOnWord.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys)
        Me.PasteOnWord.Size = New System.Drawing.Size(177, 22)
        Me.PasteOnWord.Text = "貼り付け(&P)"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(174, 6)
        '
        'SelectAllOnWord
        '
        Me.SelectAllOnWord.Name = "SelectAllOnWord"
        Me.SelectAllOnWord.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllOnWord.Size = New System.Drawing.Size(177, 22)
        Me.SelectAllOnWord.Text = "すべて選択(&A)"
        '
        'DisplayMenu
        '
        Me.DisplayMenu.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ViewWordHeader})
        Me.DisplayMenu.Name = "DisplayMenu"
        Me.DisplayMenu.Size = New System.Drawing.Size(57, 20)
        Me.DisplayMenu.Text = "表示(&V)"
        '
        'ViewWordHeader
        '
        Me.ViewWordHeader.Name = "ViewWordHeader"
        Me.ViewWordHeader.Size = New System.Drawing.Size(172, 22)
        Me.ViewWordHeader.Text = "ヘッダ部分を表示する"
        '
        'HistoryMenuOnWord
        '
        Me.HistoryMenuOnWord.Name = "HistoryMenuOnWord"
        Me.HistoryMenuOnWord.Size = New System.Drawing.Size(56, 20)
        Me.HistoryMenuOnWord.Text = "履歴(&S)"
        '
        'BookMarkMenuOnWord
        '
        Me.BookMarkMenuOnWord.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetBookmark, Me.SetBookmarkAllWord, Me.EditBookmarkOnWord, Me.ToolStripSeparator6})
        Me.BookMarkMenuOnWord.Name = "BookMarkMenuOnWord"
        Me.BookMarkMenuOnWord.Size = New System.Drawing.Size(84, 20)
        Me.BookMarkMenuOnWord.Text = "ブックマーク(&B)"
        '
        'SetBookmark
        '
        Me.SetBookmark.Name = "SetBookmark"
        Me.SetBookmark.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.SetBookmark.Size = New System.Drawing.Size(261, 22)
        Me.SetBookmark.Text = "この単語をブックマーク"
        '
        'SetBookmarkAllWord
        '
        Me.SetBookmarkAllWord.Name = "SetBookmarkAllWord"
        Me.SetBookmarkAllWord.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.SetBookmarkAllWord.Size = New System.Drawing.Size(261, 22)
        Me.SetBookmarkAllWord.Text = "すべての単語をブックマーク"
        '
        'EditBookmarkOnWord
        '
        Me.EditBookmarkOnWord.Name = "EditBookmarkOnWord"
        Me.EditBookmarkOnWord.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.B), System.Windows.Forms.Keys)
        Me.EditBookmarkOnWord.Size = New System.Drawing.Size(261, 22)
        Me.EditBookmarkOnWord.Text = "ブックマークの管理"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(258, 6)
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LinkNavigation, Me.foo})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 731)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(792, 22)
        Me.StatusStrip.TabIndex = 18
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'LinkNavigation
        '
        Me.LinkNavigation.Name = "LinkNavigation"
        Me.LinkNavigation.Size = New System.Drawing.Size(43, 17)
        Me.LinkNavigation.Text = "Dummy"
        '
        'foo
        '
        Me.foo.Name = "foo"
        Me.foo.Size = New System.Drawing.Size(0, 17)
        '
        'ToolTipOnWordDisplay
        '
        Me.ToolTipOnWordDisplay.AutoPopDelay = 5000
        Me.ToolTipOnWordDisplay.InitialDelay = 100
        Me.ToolTipOnWordDisplay.ReshowDelay = 50
        '
        'ContextMenuBrowserOnLink
        '
        Me.ContextMenuBrowserOnLink.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenLink, Me.OpenLinkNewTab, Me.Sep1, Me.WordCopy, Me.LinkCopy})
        Me.ContextMenuBrowserOnLink.Name = "ContextMenuBrowser"
        Me.ContextMenuBrowserOnLink.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuBrowserOnLink.Size = New System.Drawing.Size(180, 98)
        '
        'OpenLink
        '
        Me.OpenLink.Name = "OpenLink"
        Me.OpenLink.Size = New System.Drawing.Size(179, 22)
        Me.OpenLink.Text = "リンクを開く"
        '
        'OpenLinkNewTab
        '
        Me.OpenLinkNewTab.Name = "OpenLinkNewTab"
        Me.OpenLinkNewTab.Size = New System.Drawing.Size(179, 22)
        Me.OpenLinkNewTab.Text = "新しいタブでリンクを開く"
        '
        'Sep1
        '
        Me.Sep1.Name = "Sep1"
        Me.Sep1.Size = New System.Drawing.Size(176, 6)
        '
        'WordCopy
        '
        Me.WordCopy.Name = "WordCopy"
        Me.WordCopy.Size = New System.Drawing.Size(179, 22)
        Me.WordCopy.Text = "単語をコピー"
        '
        'LinkCopy
        '
        Me.LinkCopy.Name = "LinkCopy"
        Me.LinkCopy.Size = New System.Drawing.Size(179, 22)
        Me.LinkCopy.Text = "リンク先をコピー"
        '
        'ContextMenuBrowserOffLink
        '
        Me.ContextMenuBrowserOffLink.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GoBack, Me.GoForward, Me.Sep2, Me.BookMarkThisWord, Me.Sep3, Me.AllSelect, Me.ToolStripSeparator1, Me.OpenTextEditorOnContextBrowserOffLink})
        Me.ContextMenuBrowserOffLink.Name = "ContextMenuBrowserOffLink"
        Me.ContextMenuBrowserOffLink.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuBrowserOffLink.Size = New System.Drawing.Size(283, 132)
        '
        'GoBack
        '
        Me.GoBack.Name = "GoBack"
        Me.GoBack.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Left), System.Windows.Forms.Keys)
        Me.GoBack.Size = New System.Drawing.Size(282, 22)
        Me.GoBack.Text = "戻る"
        '
        'GoForward
        '
        Me.GoForward.Name = "GoForward"
        Me.GoForward.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.Right), System.Windows.Forms.Keys)
        Me.GoForward.Size = New System.Drawing.Size(282, 22)
        Me.GoForward.Text = "進む"
        '
        'Sep2
        '
        Me.Sep2.Name = "Sep2"
        Me.Sep2.Size = New System.Drawing.Size(279, 6)
        '
        'BookMarkThisWord
        '
        Me.BookMarkThisWord.Name = "BookMarkThisWord"
        Me.BookMarkThisWord.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.D), System.Windows.Forms.Keys)
        Me.BookMarkThisWord.Size = New System.Drawing.Size(282, 22)
        Me.BookMarkThisWord.Text = "この単語をブックマーク"
        '
        'Sep3
        '
        Me.Sep3.Name = "Sep3"
        Me.Sep3.Size = New System.Drawing.Size(279, 6)
        '
        'AllSelect
        '
        Me.AllSelect.Name = "AllSelect"
        Me.AllSelect.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AllSelect.Size = New System.Drawing.Size(282, 22)
        Me.AllSelect.Text = "すべて選択"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(279, 6)
        '
        'OpenTextEditorOnContextBrowserOffLink
        '
        Me.OpenTextEditorOnContextBrowserOffLink.Name = "OpenTextEditorOnContextBrowserOffLink"
        Me.OpenTextEditorOnContextBrowserOffLink.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.OpenTextEditorOnContextBrowserOffLink.Size = New System.Drawing.Size(282, 22)
        Me.OpenTextEditorOnContextBrowserOffLink.Text = "テキストエディタでこの項目を表示する"
        '
        'ContextMenuBrowserSelected
        '
        Me.ContextMenuBrowserSelected.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AllSelectInSelected, Me.CopyOnBrowser})
        Me.ContextMenuBrowserSelected.Name = "ContextMenuBrowserSelected"
        Me.ContextMenuBrowserSelected.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ContextMenuBrowserSelected.Size = New System.Drawing.Size(162, 48)
        '
        'AllSelectInSelected
        '
        Me.AllSelectInSelected.Name = "AllSelectInSelected"
        Me.AllSelectInSelected.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.AllSelectInSelected.Size = New System.Drawing.Size(161, 22)
        Me.AllSelectInSelected.Text = "すべて選択"
        '
        'CopyOnBrowser
        '
        Me.CopyOnBrowser.Name = "CopyOnBrowser"
        Me.CopyOnBrowser.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyOnBrowser.Size = New System.Drawing.Size(161, 22)
        Me.CopyOnBrowser.Text = "コピー"
        '
        'ToolIconOnWordDisplay
        '
        Me.ToolIconOnWordDisplay.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.GoBackOnToolStrip, Me.GoForwardOnToolStrip, Me.PrintOutOnToolStrip, Me.HelpOnToolStrip})
        Me.ToolIconOnWordDisplay.Location = New System.Drawing.Point(0, 24)
        Me.ToolIconOnWordDisplay.Name = "ToolIconOnWordDisplay"
        Me.ToolIconOnWordDisplay.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.ToolIconOnWordDisplay.Size = New System.Drawing.Size(792, 25)
        Me.ToolIconOnWordDisplay.TabIndex = 19
        '
        'GoBackOnToolStrip
        '
        Me.GoBackOnToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.GoBackOnToolStrip.Image = CType(resources.GetObject("GoBackOnToolStrip.Image"), System.Drawing.Image)
        Me.GoBackOnToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.GoBackOnToolStrip.Name = "GoBackOnToolStrip"
        Me.GoBackOnToolStrip.Size = New System.Drawing.Size(32, 22)
        Me.GoBackOnToolStrip.Text = "ToolStripSplitButton1"
        '
        'GoForwardOnToolStrip
        '
        Me.GoForwardOnToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.GoForwardOnToolStrip.Image = CType(resources.GetObject("GoForwardOnToolStrip.Image"), System.Drawing.Image)
        Me.GoForwardOnToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.GoForwardOnToolStrip.Name = "GoForwardOnToolStrip"
        Me.GoForwardOnToolStrip.Size = New System.Drawing.Size(32, 22)
        Me.GoForwardOnToolStrip.Text = "ToolStripSplitButton1"
        '
        'PrintOutOnToolStrip
        '
        Me.PrintOutOnToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.PrintOutOnToolStrip.ImageTransparentColor = System.Drawing.Color.DarkCyan
        Me.PrintOutOnToolStrip.Name = "PrintOutOnToolStrip"
        Me.PrintOutOnToolStrip.Size = New System.Drawing.Size(23, 22)
        Me.PrintOutOnToolStrip.Text = "PrintOnToolStrip"
        '
        'HelpOnToolStrip
        '
        Me.HelpOnToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpOnToolStrip.ImageTransparentColor = System.Drawing.Color.DarkCyan
        Me.HelpOnToolStrip.Name = "HelpOnToolStrip"
        Me.HelpOnToolStrip.Size = New System.Drawing.Size(23, 22)
        Me.HelpOnToolStrip.Text = "HelpOnToolStrip"
        '
        'WordDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(792, 753)
        Me.Controls.Add(Me.ToolIconOnWordDisplay)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.DisplayTab)
        Me.Controls.Add(Me.MenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(560, 560)
        Me.Name = "WordDisplay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "WordDisplay"
        Me.DisplayTab.ResumeLayout(False)
        Me.ContextMenuTab.ResumeLayout(False)
        Me.Empty.ResumeLayout(False)
        Me.Empty.PerformLayout()
        Me.ContextMenuItem.ResumeLayout(False)
        Me.WordHeaderPanel.ResumeLayout(False)
        Me.WordHeaderPanel.PerformLayout()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ContextMenuBrowserOnLink.ResumeLayout(False)
        Me.ContextMenuBrowserOffLink.ResumeLayout(False)
        Me.ContextMenuBrowserSelected.ResumeLayout(False)
        Me.ToolIconOnWordDisplay.ResumeLayout(False)
        Me.ToolIconOnWordDisplay.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DisplayTab As System.Windows.Forms.TabControl
    Friend WithEvents Empty As System.Windows.Forms.TabPage
    Friend WithEvents Word As System.Windows.Forms.TextBox
    Friend WithEvents History As System.Windows.Forms.ListBox
    Friend WithEvents lblHistory As System.Windows.Forms.Label
    Friend WithEvents Category As System.Windows.Forms.ListBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents lblReadJapanese As System.Windows.Forms.Label
    Friend WithEvents lblDictonary As System.Windows.Forms.Label
    Friend WithEvents Pos As System.Windows.Forms.TextBox
    Friend WithEvents lblPos As System.Windows.Forms.Label
    Friend WithEvents ReadJapanese As System.Windows.Forms.ListBox
    Friend WithEvents Dictonary As System.Windows.Forms.TextBox
    Friend WithEvents OtherName As System.Windows.Forms.ListBox
    Friend WithEvents lblOtherName As System.Windows.Forms.Label
    Friend WithEvents Abbr As System.Windows.Forms.ListBox
    Friend WithEvents lblAbbr As System.Windows.Forms.Label
    Friend WithEvents English As System.Windows.Forms.ListBox
    Friend WithEvents lblEnglish As System.Windows.Forms.Label
    Friend WithEvents Pron As System.Windows.Forms.ListBox
    Friend WithEvents lblPron As System.Windows.Forms.Label
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileMenuOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintPreviewOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloseOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditMenuOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CutOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ContextMenuTab As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CloseThisTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Separator0 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloseOtherTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseLeftTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CloseRightTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BookmarkThisTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BookMarkMenuOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetBookmark As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetBookmarkAllWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditBookmarkOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolTipOnWordDisplay As System.Windows.Forms.ToolTip
    Friend WithEvents DiscriptionBrowser As System.Windows.Forms.RichTextBox
    Friend WithEvents ContextMenuItem As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ContextMenuBrowserOnLink As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents OpenLink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenLinkNewTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Sep1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents WordCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinkCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ItemCopy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuBrowserOffLink As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents GoBack As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GoForward As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Sep2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BookMarkThisWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Sep3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents AllSelect As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ContextMenuBrowserSelected As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CopyOnBrowser As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllSelectInSelected As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LinkNavigation As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents foo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents OpenTextEditorOnWord As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenTextEditorOnContextBrowserOffLink As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents OpenTextEditorOnContextMenuTab As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolIconOnWordDisplay As System.Windows.Forms.ToolStrip
    Friend WithEvents PrintOutOnToolStrip As System.Windows.Forms.ToolStripButton
    Friend WithEvents HelpOnToolStrip As System.Windows.Forms.ToolStripButton
    Friend WithEvents GoForwardOnToolStrip As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents GoBackOnToolStrip As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents WordHeaderPanel As System.Windows.Forms.Panel
    Friend WithEvents DisplayMenu As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ViewWordHeader As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HistoryMenuOnWord As System.Windows.Forms.ToolStripMenuItem

End Class
