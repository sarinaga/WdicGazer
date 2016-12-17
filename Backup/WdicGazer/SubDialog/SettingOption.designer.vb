<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingOption
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.Setting_Button = New System.Windows.Forms.Button
        Me.FolderBrowserDictionary = New System.Windows.Forms.FolderBrowserDialog
        Me.FolderBrowserPlugin = New System.Windows.Forms.FolderBrowserDialog
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.CheckBox2 = New System.Windows.Forms.CheckBox
        Me.CheckBox3 = New System.Windows.Forms.CheckBox
        Me.CheckBox4 = New System.Windows.Forms.CheckBox
        Me.CheckBox5 = New System.Windows.Forms.CheckBox
        Me.CheckBox6 = New System.Windows.Forms.CheckBox
        Me.CheckBox7 = New System.Windows.Forms.CheckBox
        Me.CheckBox8 = New System.Windows.Forms.CheckBox
        Me.CheckBox9 = New System.Windows.Forms.CheckBox
        Me.OpenFileTextEditor = New System.Windows.Forms.OpenFileDialog
        Me.FontDialogBase = New System.Windows.Forms.FontDialog
        Me.TpOther = New System.Windows.Forms.TabPage
        Me.GrpOther = New System.Windows.Forms.GroupBox
        Me.MaxHistory = New System.Windows.Forms.NumericUpDown
        Me.lblMaxHistoryBetween = New System.Windows.Forms.Label
        Me.lblMaxSearchBetween = New System.Windows.Forms.Label
        Me.MaxDisplay = New System.Windows.Forms.NumericUpDown
        Me.MaxSearch = New System.Windows.Forms.NumericUpDown
        Me.lblMaxDisplayBetween = New System.Windows.Forms.Label
        Me.lblMaxDisplay = New System.Windows.Forms.Label
        Me.AutoTransfer = New System.Windows.Forms.CheckBox
        Me.lblMaxHistory = New System.Windows.Forms.Label
        Me.AlreadyNoMakeTab = New System.Windows.Forms.CheckBox
        Me.lblMaxSearch = New System.Windows.Forms.Label
        Me.AlreadyMoveOnly = New System.Windows.Forms.CheckBox
        Me.GrpTextEditor = New System.Windows.Forms.GroupBox
        Me.TextEditorExeFile = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextEditorOption = New System.Windows.Forms.TextBox
        Me.SelectTextEditor = New System.Windows.Forms.Button
        Me.TpTransfer = New System.Windows.Forms.TabPage
        Me.GrpYMD = New System.Windows.Forms.GroupBox
        Me.YearBC = New System.Windows.Forms.RadioButton
        Me.YearImperial = New System.Windows.Forms.RadioButton
        Me.YearEra = New System.Windows.Forms.RadioButton
        Me.GrpRuby = New System.Windows.Forms.GroupBox
        Me.RubyUse = New System.Windows.Forms.RadioButton
        Me.RubyParent = New System.Windows.Forms.RadioButton
        Me.RubyNotUse = New System.Windows.Forms.RadioButton
        Me.GrpTime = New System.Windows.Forms.GroupBox
        Me.TimeBeat = New System.Windows.Forms.CheckBox
        Me.Time12 = New System.Windows.Forms.RadioButton
        Me.Time24 = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.NumberPrecision = New System.Windows.Forms.NumericUpDown
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.GrpGraphPlugIn = New System.Windows.Forms.GroupBox
        Me.DisplayGraphic = New System.Windows.Forms.CheckBox
        Me.DisplayGraphicInParagraph = New System.Windows.Forms.CheckBox
        Me.TpSearchResultItem = New System.Windows.Forms.TabPage
        Me.GrpDisplayList = New System.Windows.Forms.GroupBox
        Me.SelectColumn = New System.Windows.Forms.Button
        Me.RemoveColumn = New System.Windows.Forms.Button
        Me.lblSelectedColumns = New System.Windows.Forms.Label
        Me.Up = New System.Windows.Forms.Button
        Me.SelectedColumns = New System.Windows.Forms.ListBox
        Me.Down = New System.Windows.Forms.Button
        Me.Columns = New System.Windows.Forms.ListBox
        Me.lblColumns = New System.Windows.Forms.Label
        Me.TpDic = New System.Windows.Forms.TabPage
        Me.GrpDicFolder = New System.Windows.Forms.GroupBox
        Me.DirDictionary = New System.Windows.Forms.TextBox
        Me.DirPlugin = New System.Windows.Forms.TextBox
        Me.lblDirPlugin = New System.Windows.Forms.Label
        Me.l3 = New System.Windows.Forms.Label
        Me.SelectPlugin = New System.Windows.Forms.Button
        Me.SelectDir = New System.Windows.Forms.Button
        Me.lblDirDictionary = New System.Windows.Forms.Label
        Me.GrpUseDic = New System.Windows.Forms.GroupBox
        Me.DicAll = New System.Windows.Forms.CheckBox
        Me.DicWdic = New System.Windows.Forms.CheckBox
        Me.DicTech = New System.Windows.Forms.CheckBox
        Me.DicSci = New System.Windows.Forms.CheckBox
        Me.DicMoe = New System.Windows.Forms.CheckBox
        Me.DicCul = New System.Windows.Forms.CheckBox
        Me.DicMili = New System.Windows.Forms.CheckBox
        Me.DicGeo = New System.Windows.Forms.CheckBox
        Me.DicRail = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.CheckBox10 = New System.Windows.Forms.CheckBox
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.TabOption = New System.Windows.Forms.TabControl
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TpOther.SuspendLayout()
        Me.GrpOther.SuspendLayout()
        CType(Me.MaxHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaxDisplay, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.MaxSearch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpTextEditor.SuspendLayout()
        Me.TpTransfer.SuspendLayout()
        Me.GrpYMD.SuspendLayout()
        Me.GrpRuby.SuspendLayout()
        Me.GrpTime.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumberPrecision, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpGraphPlugIn.SuspendLayout()
        Me.TpSearchResultItem.SuspendLayout()
        Me.GrpDisplayList.SuspendLayout()
        Me.TpDic.SuspendLayout()
        Me.GrpDicFolder.SuspendLayout()
        Me.GrpUseDic.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabOption.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Setting_Button, 2, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(218, 391)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(246, 31)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 4)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(75, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(84, 4)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(75, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "キャンセル"
        '
        'Setting_Button
        '
        Me.Setting_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Setting_Button.Location = New System.Drawing.Point(167, 4)
        Me.Setting_Button.Name = "Setting_Button"
        Me.Setting_Button.Size = New System.Drawing.Size(75, 23)
        Me.Setting_Button.TabIndex = 2
        Me.Setting_Button.Text = "適用"
        Me.Setting_Button.UseVisualStyleBackColor = True
        '
        'FolderBrowserDictionary
        '
        Me.FolderBrowserDictionary.Description = "辞書ファイルが置かれているフォルダを選択してください"
        Me.FolderBrowserDictionary.ShowNewFolderButton = False
        '
        'FolderBrowserPlugin
        '
        Me.FolderBrowserPlugin.Description = "プラグインが置かれているフォルダを選択してください"
        Me.FolderBrowserPlugin.ShowNewFolderButton = False
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(384, 38)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(48, 16)
        Me.CheckBox1.TabIndex = 8
        Me.CheckBox1.Text = "鉄道"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'CheckBox2
        '
        Me.CheckBox2.AutoSize = True
        Me.CheckBox2.Location = New System.Drawing.Point(330, 38)
        Me.CheckBox2.Name = "CheckBox2"
        Me.CheckBox2.Size = New System.Drawing.Size(48, 16)
        Me.CheckBox2.TabIndex = 7
        Me.CheckBox2.Text = "国土"
        Me.CheckBox2.UseVisualStyleBackColor = True
        '
        'CheckBox3
        '
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(276, 38)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(48, 16)
        Me.CheckBox3.TabIndex = 6
        Me.CheckBox3.Text = "軍事"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'CheckBox4
        '
        Me.CheckBox4.AutoSize = True
        Me.CheckBox4.Location = New System.Drawing.Point(222, 38)
        Me.CheckBox4.Name = "CheckBox4"
        Me.CheckBox4.Size = New System.Drawing.Size(48, 16)
        Me.CheckBox4.TabIndex = 5
        Me.CheckBox4.Text = "文化"
        Me.CheckBox4.UseVisualStyleBackColor = True
        '
        'CheckBox5
        '
        Me.CheckBox5.AutoSize = True
        Me.CheckBox5.Location = New System.Drawing.Point(168, 38)
        Me.CheckBox5.Name = "CheckBox5"
        Me.CheckBox5.Size = New System.Drawing.Size(48, 16)
        Me.CheckBox5.TabIndex = 4
        Me.CheckBox5.Text = "萌色"
        Me.CheckBox5.UseVisualStyleBackColor = True
        '
        'CheckBox6
        '
        Me.CheckBox6.AutoSize = True
        Me.CheckBox6.Location = New System.Drawing.Point(114, 38)
        Me.CheckBox6.Name = "CheckBox6"
        Me.CheckBox6.Size = New System.Drawing.Size(48, 16)
        Me.CheckBox6.TabIndex = 3
        Me.CheckBox6.Text = "科学"
        Me.CheckBox6.UseVisualStyleBackColor = True
        '
        'CheckBox7
        '
        Me.CheckBox7.AutoSize = True
        Me.CheckBox7.Location = New System.Drawing.Point(60, 38)
        Me.CheckBox7.Name = "CheckBox7"
        Me.CheckBox7.Size = New System.Drawing.Size(48, 16)
        Me.CheckBox7.TabIndex = 2
        Me.CheckBox7.Text = "電算"
        Me.CheckBox7.UseVisualStyleBackColor = True
        '
        'CheckBox8
        '
        Me.CheckBox8.AutoSize = True
        Me.CheckBox8.Location = New System.Drawing.Point(6, 38)
        Me.CheckBox8.Name = "CheckBox8"
        Me.CheckBox8.Size = New System.Drawing.Size(48, 16)
        Me.CheckBox8.TabIndex = 1
        Me.CheckBox8.Text = "通信"
        Me.CheckBox8.UseVisualStyleBackColor = True
        '
        'CheckBox9
        '
        Me.CheckBox9.AutoSize = True
        Me.CheckBox9.Location = New System.Drawing.Point(6, 18)
        Me.CheckBox9.Name = "CheckBox9"
        Me.CheckBox9.Size = New System.Drawing.Size(60, 16)
        Me.CheckBox9.TabIndex = 0
        Me.CheckBox9.Text = "全辞書"
        Me.CheckBox9.UseVisualStyleBackColor = True
        '
        'OpenFileTextEditor
        '
        Me.OpenFileTextEditor.AddExtension = False
        Me.OpenFileTextEditor.DefaultExt = "exe"
        Me.OpenFileTextEditor.Filter = "実行ファイル|*.exe|すべてのファイル|*.*"
        Me.OpenFileTextEditor.Title = "テクストエディタの実行ファイルを指定してください。"
        '
        'FontDialogBase
        '
        Me.FontDialogBase.AllowVerticalFonts = False
        Me.FontDialogBase.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 16.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.FontDialogBase.FontMustExist = True
        Me.FontDialogBase.ShowEffects = False
        '
        'TpOther
        '
        Me.TpOther.Controls.Add(Me.GrpTextEditor)
        Me.TpOther.Controls.Add(Me.GrpOther)
        Me.TpOther.Location = New System.Drawing.Point(4, 21)
        Me.TpOther.Name = "TpOther"
        Me.TpOther.Padding = New System.Windows.Forms.Padding(3)
        Me.TpOther.Size = New System.Drawing.Size(444, 349)
        Me.TpOther.TabIndex = 6
        Me.TpOther.Text = "その他"
        Me.TpOther.UseVisualStyleBackColor = True
        '
        'GrpOther
        '
        Me.GrpOther.Controls.Add(Me.AlreadyMoveOnly)
        Me.GrpOther.Controls.Add(Me.lblMaxSearch)
        Me.GrpOther.Controls.Add(Me.AlreadyNoMakeTab)
        Me.GrpOther.Controls.Add(Me.lblMaxHistory)
        Me.GrpOther.Controls.Add(Me.AutoTransfer)
        Me.GrpOther.Controls.Add(Me.lblMaxDisplay)
        Me.GrpOther.Controls.Add(Me.lblMaxDisplayBetween)
        Me.GrpOther.Controls.Add(Me.MaxSearch)
        Me.GrpOther.Controls.Add(Me.MaxDisplay)
        Me.GrpOther.Controls.Add(Me.lblMaxSearchBetween)
        Me.GrpOther.Controls.Add(Me.lblMaxHistoryBetween)
        Me.GrpOther.Controls.Add(Me.MaxHistory)
        Me.GrpOther.Location = New System.Drawing.Point(6, 112)
        Me.GrpOther.Name = "GrpOther"
        Me.GrpOther.Size = New System.Drawing.Size(432, 231)
        Me.GrpOther.TabIndex = 21
        Me.GrpOther.TabStop = False
        Me.GrpOther.Text = "その他"
        '
        'MaxHistory
        '
        Me.MaxHistory.Location = New System.Drawing.Point(125, 49)
        Me.MaxHistory.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.MaxHistory.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.MaxHistory.Name = "MaxHistory"
        Me.MaxHistory.Size = New System.Drawing.Size(47, 19)
        Me.MaxHistory.TabIndex = 13
        Me.MaxHistory.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'lblMaxHistoryBetween
        '
        Me.lblMaxHistoryBetween.AutoSize = True
        Me.lblMaxHistoryBetween.Location = New System.Drawing.Point(178, 51)
        Me.lblMaxHistoryBetween.Name = "lblMaxHistoryBetween"
        Me.lblMaxHistoryBetween.Size = New System.Drawing.Size(43, 12)
        Me.lblMaxHistoryBetween.TabIndex = 14
        Me.lblMaxHistoryBetween.Text = "(5～30)"
        '
        'lblMaxSearchBetween
        '
        Me.lblMaxSearchBetween.AutoSize = True
        Me.lblMaxSearchBetween.Location = New System.Drawing.Point(178, 26)
        Me.lblMaxSearchBetween.Name = "lblMaxSearchBetween"
        Me.lblMaxSearchBetween.Size = New System.Drawing.Size(55, 12)
        Me.lblMaxSearchBetween.TabIndex = 11
        Me.lblMaxSearchBetween.Text = "(10～999)"
        '
        'MaxDisplay
        '
        Me.MaxDisplay.Location = New System.Drawing.Point(125, 74)
        Me.MaxDisplay.Maximum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.MaxDisplay.Minimum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.MaxDisplay.Name = "MaxDisplay"
        Me.MaxDisplay.Size = New System.Drawing.Size(47, 19)
        Me.MaxDisplay.TabIndex = 16
        Me.MaxDisplay.Value = New Decimal(New Integer() {5, 0, 0, 0})
        '
        'MaxSearch
        '
        Me.MaxSearch.Location = New System.Drawing.Point(125, 24)
        Me.MaxSearch.Maximum = New Decimal(New Integer() {999, 0, 0, 0})
        Me.MaxSearch.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.MaxSearch.Name = "MaxSearch"
        Me.MaxSearch.Size = New System.Drawing.Size(47, 19)
        Me.MaxSearch.TabIndex = 9
        Me.MaxSearch.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'lblMaxDisplayBetween
        '
        Me.lblMaxDisplayBetween.AutoSize = True
        Me.lblMaxDisplayBetween.Location = New System.Drawing.Point(178, 76)
        Me.lblMaxDisplayBetween.Name = "lblMaxDisplayBetween"
        Me.lblMaxDisplayBetween.Size = New System.Drawing.Size(43, 12)
        Me.lblMaxDisplayBetween.TabIndex = 17
        Me.lblMaxDisplayBetween.Text = "(5～30)"
        '
        'lblMaxDisplay
        '
        Me.lblMaxDisplay.AutoSize = True
        Me.lblMaxDisplay.Location = New System.Drawing.Point(6, 76)
        Me.lblMaxDisplay.Name = "lblMaxDisplay"
        Me.lblMaxDisplay.Size = New System.Drawing.Size(89, 12)
        Me.lblMaxDisplay.TabIndex = 15
        Me.lblMaxDisplay.Text = "最大表示履歴数"
        '
        'AutoTransfer
        '
        Me.AutoTransfer.AutoSize = True
        Me.AutoTransfer.Location = New System.Drawing.Point(6, 108)
        Me.AutoTransfer.Name = "AutoTransfer"
        Me.AutoTransfer.Size = New System.Drawing.Size(134, 16)
        Me.AutoTransfer.TabIndex = 18
        Me.AutoTransfer.Text = "単語の自動転送を行う"
        Me.AutoTransfer.UseVisualStyleBackColor = True
        '
        'lblMaxHistory
        '
        Me.lblMaxHistory.AutoSize = True
        Me.lblMaxHistory.Location = New System.Drawing.Point(6, 51)
        Me.lblMaxHistory.Name = "lblMaxHistory"
        Me.lblMaxHistory.Size = New System.Drawing.Size(89, 12)
        Me.lblMaxHistory.TabIndex = 12
        Me.lblMaxHistory.Text = "最大検索履歴数"
        '
        'AlreadyNoMakeTab
        '
        Me.AlreadyNoMakeTab.AutoSize = True
        Me.AlreadyNoMakeTab.Location = New System.Drawing.Point(6, 130)
        Me.AlreadyNoMakeTab.Name = "AlreadyNoMakeTab"
        Me.AlreadyNoMakeTab.Size = New System.Drawing.Size(294, 16)
        Me.AlreadyNoMakeTab.TabIndex = 19
        Me.AlreadyNoMakeTab.Text = "すでに同一の単語がタブにある場合、新たにタブを作らない"
        Me.AlreadyNoMakeTab.UseVisualStyleBackColor = True
        '
        'lblMaxSearch
        '
        Me.lblMaxSearch.AutoSize = True
        Me.lblMaxSearch.Location = New System.Drawing.Point(6, 26)
        Me.lblMaxSearch.Name = "lblMaxSearch"
        Me.lblMaxSearch.Size = New System.Drawing.Size(113, 12)
        Me.lblMaxSearch.TabIndex = 10
        Me.lblMaxSearch.Text = "最大検索結果表示数"
        '
        'AlreadyMoveOnly
        '
        Me.AlreadyMoveOnly.AutoSize = True
        Me.AlreadyMoveOnly.Location = New System.Drawing.Point(6, 152)
        Me.AlreadyMoveOnly.Name = "AlreadyMoveOnly"
        Me.AlreadyMoveOnly.Size = New System.Drawing.Size(354, 16)
        Me.AlreadyMoveOnly.TabIndex = 20
        Me.AlreadyMoveOnly.Text = "すでに同一の単語がタブにある場合、リンクはタブの選択変更だけにする"
        Me.AlreadyMoveOnly.UseVisualStyleBackColor = True
        '
        'GrpTextEditor
        '
        Me.GrpTextEditor.Controls.Add(Me.SelectTextEditor)
        Me.GrpTextEditor.Controls.Add(Me.TextEditorOption)
        Me.GrpTextEditor.Controls.Add(Me.Label1)
        Me.GrpTextEditor.Controls.Add(Me.TextEditorExeFile)
        Me.GrpTextEditor.Location = New System.Drawing.Point(6, 6)
        Me.GrpTextEditor.Name = "GrpTextEditor"
        Me.GrpTextEditor.Size = New System.Drawing.Size(432, 100)
        Me.GrpTextEditor.TabIndex = 0
        Me.GrpTextEditor.TabStop = False
        Me.GrpTextEditor.Text = "テキストエディタの指定"
        '
        'TextEditorExeFile
        '
        Me.TextEditorExeFile.Location = New System.Drawing.Point(6, 18)
        Me.TextEditorExeFile.Name = "TextEditorExeFile"
        Me.TextEditorExeFile.Size = New System.Drawing.Size(339, 19)
        Me.TextEditorExeFile.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 46)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(308, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "オプションの指定（%dが行番号に、%sがファイル名に変換されます）"
        '
        'TextEditorOption
        '
        Me.TextEditorOption.Location = New System.Drawing.Point(6, 61)
        Me.TextEditorOption.Name = "TextEditorOption"
        Me.TextEditorOption.Size = New System.Drawing.Size(88, 19)
        Me.TextEditorOption.TabIndex = 3
        '
        'SelectTextEditor
        '
        Me.SelectTextEditor.Location = New System.Drawing.Point(351, 16)
        Me.SelectTextEditor.Name = "SelectTextEditor"
        Me.SelectTextEditor.Size = New System.Drawing.Size(75, 23)
        Me.SelectTextEditor.TabIndex = 1
        Me.SelectTextEditor.Text = "選択"
        Me.SelectTextEditor.UseVisualStyleBackColor = True
        '
        'TpTransfer
        '
        Me.TpTransfer.Controls.Add(Me.GrpGraphPlugIn)
        Me.TpTransfer.Controls.Add(Me.GroupBox1)
        Me.TpTransfer.Controls.Add(Me.GrpTime)
        Me.TpTransfer.Controls.Add(Me.GrpRuby)
        Me.TpTransfer.Controls.Add(Me.GrpYMD)
        Me.TpTransfer.Location = New System.Drawing.Point(4, 21)
        Me.TpTransfer.Name = "TpTransfer"
        Me.TpTransfer.Padding = New System.Windows.Forms.Padding(3)
        Me.TpTransfer.Size = New System.Drawing.Size(444, 349)
        Me.TpTransfer.TabIndex = 5
        Me.TpTransfer.Text = "形式"
        Me.TpTransfer.UseVisualStyleBackColor = True
        '
        'GrpYMD
        '
        Me.GrpYMD.Controls.Add(Me.YearEra)
        Me.GrpYMD.Controls.Add(Me.YearImperial)
        Me.GrpYMD.Controls.Add(Me.YearBC)
        Me.GrpYMD.Location = New System.Drawing.Point(6, 6)
        Me.GrpYMD.Name = "GrpYMD"
        Me.GrpYMD.Size = New System.Drawing.Size(158, 100)
        Me.GrpYMD.TabIndex = 0
        Me.GrpYMD.TabStop = False
        Me.GrpYMD.Text = "年月日"
        '
        'YearBC
        '
        Me.YearBC.AutoSize = True
        Me.YearBC.Location = New System.Drawing.Point(6, 18)
        Me.YearBC.Name = "YearBC"
        Me.YearBC.Size = New System.Drawing.Size(120, 16)
        Me.YearBC.TabIndex = 0
        Me.YearBC.Text = "西暦のみを表示する"
        Me.YearBC.UseVisualStyleBackColor = True
        '
        'YearImperial
        '
        Me.YearImperial.AutoSize = True
        Me.YearImperial.Location = New System.Drawing.Point(6, 62)
        Me.YearImperial.Name = "YearImperial"
        Me.YearImperial.Size = New System.Drawing.Size(131, 16)
        Me.YearImperial.TabIndex = 1
        Me.YearImperial.Text = "西暦と皇紀を表示する"
        Me.YearImperial.UseVisualStyleBackColor = True
        '
        'YearEra
        '
        Me.YearEra.AutoSize = True
        Me.YearEra.Checked = True
        Me.YearEra.Location = New System.Drawing.Point(6, 40)
        Me.YearEra.Name = "YearEra"
        Me.YearEra.Size = New System.Drawing.Size(131, 16)
        Me.YearEra.TabIndex = 2
        Me.YearEra.TabStop = True
        Me.YearEra.Text = "西暦と元号を表示する"
        Me.YearEra.UseVisualStyleBackColor = True
        '
        'GrpRuby
        '
        Me.GrpRuby.Controls.Add(Me.RubyNotUse)
        Me.GrpRuby.Controls.Add(Me.RubyParent)
        Me.GrpRuby.Controls.Add(Me.RubyUse)
        Me.GrpRuby.Location = New System.Drawing.Point(6, 112)
        Me.GrpRuby.Name = "GrpRuby"
        Me.GrpRuby.Size = New System.Drawing.Size(200, 100)
        Me.GrpRuby.TabIndex = 1
        Me.GrpRuby.TabStop = False
        Me.GrpRuby.Text = "ふりがな"
        '
        'RubyUse
        '
        Me.RubyUse.AutoSize = True
        Me.RubyUse.Checked = True
        Me.RubyUse.Location = New System.Drawing.Point(6, 18)
        Me.RubyUse.Name = "RubyUse"
        Me.RubyUse.Size = New System.Drawing.Size(94, 16)
        Me.RubyUse.TabIndex = 0
        Me.RubyUse.TabStop = True
        Me.RubyUse.Text = "ルビで表示する"
        Me.RubyUse.UseVisualStyleBackColor = True
        '
        'RubyParent
        '
        Me.RubyParent.AutoSize = True
        Me.RubyParent.Location = New System.Drawing.Point(6, 40)
        Me.RubyParent.Name = "RubyParent"
        Me.RubyParent.Size = New System.Drawing.Size(161, 16)
        Me.RubyParent.TabIndex = 1
        Me.RubyParent.TabStop = True
        Me.RubyParent.Text = "単語のあとに括弧で表示する"
        Me.RubyParent.UseVisualStyleBackColor = True
        '
        'RubyNotUse
        '
        Me.RubyNotUse.AutoSize = True
        Me.RubyNotUse.Location = New System.Drawing.Point(6, 62)
        Me.RubyNotUse.Name = "RubyNotUse"
        Me.RubyNotUse.Size = New System.Drawing.Size(76, 16)
        Me.RubyNotUse.TabIndex = 2
        Me.RubyNotUse.TabStop = True
        Me.RubyNotUse.Text = "表示しない"
        Me.RubyNotUse.UseVisualStyleBackColor = True
        '
        'GrpTime
        '
        Me.GrpTime.Controls.Add(Me.Time24)
        Me.GrpTime.Controls.Add(Me.Time12)
        Me.GrpTime.Controls.Add(Me.TimeBeat)
        Me.GrpTime.Location = New System.Drawing.Point(170, 6)
        Me.GrpTime.Name = "GrpTime"
        Me.GrpTime.Size = New System.Drawing.Size(268, 100)
        Me.GrpTime.TabIndex = 2
        Me.GrpTime.TabStop = False
        Me.GrpTime.Text = "時間"
        '
        'TimeBeat
        '
        Me.TimeBeat.AutoSize = True
        Me.TimeBeat.Location = New System.Drawing.Point(6, 63)
        Me.TimeBeat.Name = "TimeBeat"
        Me.TimeBeat.Size = New System.Drawing.Size(201, 16)
        Me.TimeBeat.TabIndex = 0
        Me.TimeBeat.Text = "インターネット時間(@Beat)を表示する"
        Me.TimeBeat.UseVisualStyleBackColor = True
        '
        'Time12
        '
        Me.Time12.AutoSize = True
        Me.Time12.Checked = True
        Me.Time12.Location = New System.Drawing.Point(6, 18)
        Me.Time12.Name = "Time12"
        Me.Time12.Size = New System.Drawing.Size(102, 16)
        Me.Time12.TabIndex = 1
        Me.Time12.TabStop = True
        Me.Time12.Text = "12時間表示する"
        Me.Time12.UseVisualStyleBackColor = True
        '
        'Time24
        '
        Me.Time24.AutoSize = True
        Me.Time24.Location = New System.Drawing.Point(6, 40)
        Me.Time24.Name = "Time24"
        Me.Time24.Size = New System.Drawing.Size(102, 16)
        Me.Time24.TabIndex = 2
        Me.Time24.Text = "24時間表示する"
        Me.Time24.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.NumberPrecision)
        Me.GroupBox1.Location = New System.Drawing.Point(212, 112)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(226, 100)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "計算結果の精度"
        '
        'NumberPrecision
        '
        Me.NumberPrecision.Location = New System.Drawing.Point(65, 22)
        Me.NumberPrecision.Maximum = New Decimal(New Integer() {5, 0, 0, 0})
        Me.NumberPrecision.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NumberPrecision.Name = "NumberPrecision"
        Me.NumberPrecision.Size = New System.Drawing.Size(54, 19)
        Me.NumberPrecision.TabIndex = 0
        Me.NumberPrecision.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "有効桁数"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(125, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 12)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "桁で表示(2～10)"
        '
        'GrpGraphPlugIn
        '
        Me.GrpGraphPlugIn.Controls.Add(Me.DisplayGraphicInParagraph)
        Me.GrpGraphPlugIn.Controls.Add(Me.DisplayGraphic)
        Me.GrpGraphPlugIn.Location = New System.Drawing.Point(6, 218)
        Me.GrpGraphPlugIn.Name = "GrpGraphPlugIn"
        Me.GrpGraphPlugIn.Size = New System.Drawing.Size(432, 125)
        Me.GrpGraphPlugIn.TabIndex = 4
        Me.GrpGraphPlugIn.TabStop = False
        Me.GrpGraphPlugIn.Text = "画像・プラグイン"
        '
        'DisplayGraphic
        '
        Me.DisplayGraphic.AutoSize = True
        Me.DisplayGraphic.Checked = True
        Me.DisplayGraphic.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DisplayGraphic.Location = New System.Drawing.Point(6, 18)
        Me.DisplayGraphic.Name = "DisplayGraphic"
        Me.DisplayGraphic.Size = New System.Drawing.Size(100, 16)
        Me.DisplayGraphic.TabIndex = 0
        Me.DisplayGraphic.Text = "画像を表示する"
        Me.DisplayGraphic.UseVisualStyleBackColor = True
        '
        'DisplayGraphicInParagraph
        '
        Me.DisplayGraphicInParagraph.AutoSize = True
        Me.DisplayGraphicInParagraph.Checked = True
        Me.DisplayGraphicInParagraph.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DisplayGraphicInParagraph.Location = New System.Drawing.Point(6, 40)
        Me.DisplayGraphicInParagraph.Name = "DisplayGraphicInParagraph"
        Me.DisplayGraphicInParagraph.Size = New System.Drawing.Size(145, 16)
        Me.DisplayGraphicInParagraph.TabIndex = 1
        Me.DisplayGraphicInParagraph.Text = "本文中に画像を表示する"
        Me.DisplayGraphicInParagraph.UseVisualStyleBackColor = True
        '
        'TpSearchResultItem
        '
        Me.TpSearchResultItem.Controls.Add(Me.GrpDisplayList)
        Me.TpSearchResultItem.Location = New System.Drawing.Point(4, 21)
        Me.TpSearchResultItem.Name = "TpSearchResultItem"
        Me.TpSearchResultItem.Padding = New System.Windows.Forms.Padding(3)
        Me.TpSearchResultItem.Size = New System.Drawing.Size(444, 349)
        Me.TpSearchResultItem.TabIndex = 4
        Me.TpSearchResultItem.Text = "検索結果一覧"
        Me.TpSearchResultItem.UseVisualStyleBackColor = True
        '
        'GrpDisplayList
        '
        Me.GrpDisplayList.Controls.Add(Me.lblColumns)
        Me.GrpDisplayList.Controls.Add(Me.Columns)
        Me.GrpDisplayList.Controls.Add(Me.Down)
        Me.GrpDisplayList.Controls.Add(Me.SelectedColumns)
        Me.GrpDisplayList.Controls.Add(Me.Up)
        Me.GrpDisplayList.Controls.Add(Me.lblSelectedColumns)
        Me.GrpDisplayList.Controls.Add(Me.RemoveColumn)
        Me.GrpDisplayList.Controls.Add(Me.SelectColumn)
        Me.GrpDisplayList.Location = New System.Drawing.Point(6, 6)
        Me.GrpDisplayList.Name = "GrpDisplayList"
        Me.GrpDisplayList.Size = New System.Drawing.Size(423, 337)
        Me.GrpDisplayList.TabIndex = 7
        Me.GrpDisplayList.TabStop = False
        Me.GrpDisplayList.Text = "検索結果一覧表示で表示する項目"
        '
        'SelectColumn
        '
        Me.SelectColumn.Location = New System.Drawing.Point(168, 49)
        Me.SelectColumn.Name = "SelectColumn"
        Me.SelectColumn.Size = New System.Drawing.Size(75, 23)
        Me.SelectColumn.TabIndex = 3
        Me.SelectColumn.Text = "←"
        Me.SelectColumn.UseVisualStyleBackColor = True
        '
        'RemoveColumn
        '
        Me.RemoveColumn.Location = New System.Drawing.Point(168, 78)
        Me.RemoveColumn.Name = "RemoveColumn"
        Me.RemoveColumn.Size = New System.Drawing.Size(75, 23)
        Me.RemoveColumn.TabIndex = 4
        Me.RemoveColumn.Text = "→"
        Me.RemoveColumn.UseVisualStyleBackColor = True
        '
        'lblSelectedColumns
        '
        Me.lblSelectedColumns.AutoSize = True
        Me.lblSelectedColumns.Location = New System.Drawing.Point(4, 25)
        Me.lblSelectedColumns.Name = "lblSelectedColumns"
        Me.lblSelectedColumns.Size = New System.Drawing.Size(81, 12)
        Me.lblSelectedColumns.TabIndex = 2
        Me.lblSelectedColumns.Text = "選択された項目"
        '
        'Up
        '
        Me.Up.Location = New System.Drawing.Point(168, 110)
        Me.Up.Name = "Up"
        Me.Up.Size = New System.Drawing.Size(75, 23)
        Me.Up.TabIndex = 5
        Me.Up.Text = "↑"
        Me.Up.UseVisualStyleBackColor = True
        '
        'SelectedColumns
        '
        Me.SelectedColumns.FormattingEnabled = True
        Me.SelectedColumns.ItemHeight = 12
        Me.SelectedColumns.Location = New System.Drawing.Point(6, 40)
        Me.SelectedColumns.Name = "SelectedColumns"
        Me.SelectedColumns.Size = New System.Drawing.Size(156, 280)
        Me.SelectedColumns.TabIndex = 1
        '
        'Down
        '
        Me.Down.Location = New System.Drawing.Point(168, 139)
        Me.Down.Name = "Down"
        Me.Down.Size = New System.Drawing.Size(75, 23)
        Me.Down.TabIndex = 6
        Me.Down.Text = "↓"
        Me.Down.UseVisualStyleBackColor = True
        '
        'Columns
        '
        Me.Columns.FormattingEnabled = True
        Me.Columns.ItemHeight = 12
        Me.Columns.Location = New System.Drawing.Point(249, 40)
        Me.Columns.Name = "Columns"
        Me.Columns.Size = New System.Drawing.Size(161, 280)
        Me.Columns.TabIndex = 0
        '
        'lblColumns
        '
        Me.lblColumns.AutoSize = True
        Me.lblColumns.Location = New System.Drawing.Point(247, 25)
        Me.lblColumns.Name = "lblColumns"
        Me.lblColumns.Size = New System.Drawing.Size(111, 12)
        Me.lblColumns.TabIndex = 7
        Me.lblColumns.Text = "選択されていない項目"
        '
        'TpDic
        '
        Me.TpDic.Controls.Add(Me.GroupBox2)
        Me.TpDic.Controls.Add(Me.GrpUseDic)
        Me.TpDic.Controls.Add(Me.GrpDicFolder)
        Me.TpDic.Location = New System.Drawing.Point(4, 21)
        Me.TpDic.Name = "TpDic"
        Me.TpDic.Padding = New System.Windows.Forms.Padding(3)
        Me.TpDic.Size = New System.Drawing.Size(444, 349)
        Me.TpDic.TabIndex = 0
        Me.TpDic.Text = "辞書"
        Me.TpDic.UseVisualStyleBackColor = True
        '
        'GrpDicFolder
        '
        Me.GrpDicFolder.Controls.Add(Me.lblDirDictionary)
        Me.GrpDicFolder.Controls.Add(Me.SelectDir)
        Me.GrpDicFolder.Controls.Add(Me.SelectPlugin)
        Me.GrpDicFolder.Controls.Add(Me.l3)
        Me.GrpDicFolder.Controls.Add(Me.lblDirPlugin)
        Me.GrpDicFolder.Controls.Add(Me.DirPlugin)
        Me.GrpDicFolder.Controls.Add(Me.DirDictionary)
        Me.GrpDicFolder.Location = New System.Drawing.Point(8, 9)
        Me.GrpDicFolder.Name = "GrpDicFolder"
        Me.GrpDicFolder.Size = New System.Drawing.Size(424, 109)
        Me.GrpDicFolder.TabIndex = 12
        Me.GrpDicFolder.TabStop = False
        Me.GrpDicFolder.Text = "フォルダ"
        '
        'DirDictionary
        '
        Me.DirDictionary.Location = New System.Drawing.Point(110, 16)
        Me.DirDictionary.Name = "DirDictionary"
        Me.DirDictionary.Size = New System.Drawing.Size(233, 19)
        Me.DirDictionary.TabIndex = 2
        '
        'DirPlugin
        '
        Me.DirPlugin.Location = New System.Drawing.Point(110, 43)
        Me.DirPlugin.Name = "DirPlugin"
        Me.DirPlugin.Size = New System.Drawing.Size(233, 19)
        Me.DirPlugin.TabIndex = 3
        '
        'lblDirPlugin
        '
        Me.lblDirPlugin.AutoSize = True
        Me.lblDirPlugin.Location = New System.Drawing.Point(6, 46)
        Me.lblDirPlugin.Name = "lblDirPlugin"
        Me.lblDirPlugin.Size = New System.Drawing.Size(84, 12)
        Me.lblDirPlugin.TabIndex = 1
        Me.lblDirPlugin.Text = "プラグインフォルダ"
        '
        'l3
        '
        Me.l3.AutoSize = True
        Me.l3.Location = New System.Drawing.Point(6, 78)
        Me.l3.Name = "l3"
        Me.l3.Size = New System.Drawing.Size(274, 12)
        Me.l3.TabIndex = 6
        Me.l3.Text = "※ 指定されたフォルダからファイルを再帰的に検索します。"
        '
        'SelectPlugin
        '
        Me.SelectPlugin.Location = New System.Drawing.Point(349, 41)
        Me.SelectPlugin.Name = "SelectPlugin"
        Me.SelectPlugin.Size = New System.Drawing.Size(69, 23)
        Me.SelectPlugin.TabIndex = 5
        Me.SelectPlugin.Text = "選択"
        Me.SelectPlugin.UseVisualStyleBackColor = True
        '
        'SelectDir
        '
        Me.SelectDir.Location = New System.Drawing.Point(349, 14)
        Me.SelectDir.Name = "SelectDir"
        Me.SelectDir.Size = New System.Drawing.Size(69, 23)
        Me.SelectDir.TabIndex = 4
        Me.SelectDir.Text = "選択"
        Me.SelectDir.UseVisualStyleBackColor = True
        '
        'lblDirDictionary
        '
        Me.lblDirDictionary.AutoSize = True
        Me.lblDirDictionary.Location = New System.Drawing.Point(6, 19)
        Me.lblDirDictionary.Name = "lblDirDictionary"
        Me.lblDirDictionary.Size = New System.Drawing.Size(100, 12)
        Me.lblDirDictionary.TabIndex = 0
        Me.lblDirDictionary.Text = "辞書フォルダ（必須）"
        '
        'GrpUseDic
        '
        Me.GrpUseDic.Controls.Add(Me.DicRail)
        Me.GrpUseDic.Controls.Add(Me.DicGeo)
        Me.GrpUseDic.Controls.Add(Me.DicMili)
        Me.GrpUseDic.Controls.Add(Me.DicCul)
        Me.GrpUseDic.Controls.Add(Me.DicMoe)
        Me.GrpUseDic.Controls.Add(Me.DicSci)
        Me.GrpUseDic.Controls.Add(Me.DicTech)
        Me.GrpUseDic.Controls.Add(Me.DicWdic)
        Me.GrpUseDic.Controls.Add(Me.DicAll)
        Me.GrpUseDic.Location = New System.Drawing.Point(8, 124)
        Me.GrpUseDic.Name = "GrpUseDic"
        Me.GrpUseDic.Size = New System.Drawing.Size(424, 87)
        Me.GrpUseDic.TabIndex = 11
        Me.GrpUseDic.TabStop = False
        Me.GrpUseDic.Text = "利用する辞書"
        '
        'DicAll
        '
        Me.DicAll.AutoSize = True
        Me.DicAll.Location = New System.Drawing.Point(6, 18)
        Me.DicAll.Name = "DicAll"
        Me.DicAll.Size = New System.Drawing.Size(60, 16)
        Me.DicAll.TabIndex = 0
        Me.DicAll.Text = "全辞書"
        Me.DicAll.UseVisualStyleBackColor = True
        '
        'DicWdic
        '
        Me.DicWdic.AutoSize = True
        Me.DicWdic.Location = New System.Drawing.Point(6, 40)
        Me.DicWdic.Name = "DicWdic"
        Me.DicWdic.Size = New System.Drawing.Size(48, 16)
        Me.DicWdic.TabIndex = 1
        Me.DicWdic.Text = "通信"
        Me.DicWdic.UseVisualStyleBackColor = True
        '
        'DicTech
        '
        Me.DicTech.AutoSize = True
        Me.DicTech.Location = New System.Drawing.Point(60, 40)
        Me.DicTech.Name = "DicTech"
        Me.DicTech.Size = New System.Drawing.Size(48, 16)
        Me.DicTech.TabIndex = 2
        Me.DicTech.Text = "電算"
        Me.DicTech.UseVisualStyleBackColor = True
        '
        'DicSci
        '
        Me.DicSci.AutoSize = True
        Me.DicSci.Location = New System.Drawing.Point(114, 40)
        Me.DicSci.Name = "DicSci"
        Me.DicSci.Size = New System.Drawing.Size(48, 16)
        Me.DicSci.TabIndex = 3
        Me.DicSci.Text = "科学"
        Me.DicSci.UseVisualStyleBackColor = True
        '
        'DicMoe
        '
        Me.DicMoe.AutoSize = True
        Me.DicMoe.Location = New System.Drawing.Point(168, 40)
        Me.DicMoe.Name = "DicMoe"
        Me.DicMoe.Size = New System.Drawing.Size(48, 16)
        Me.DicMoe.TabIndex = 4
        Me.DicMoe.Text = "萌色"
        Me.DicMoe.UseVisualStyleBackColor = True
        '
        'DicCul
        '
        Me.DicCul.AutoSize = True
        Me.DicCul.Location = New System.Drawing.Point(222, 40)
        Me.DicCul.Name = "DicCul"
        Me.DicCul.Size = New System.Drawing.Size(48, 16)
        Me.DicCul.TabIndex = 5
        Me.DicCul.Text = "文化"
        Me.DicCul.UseVisualStyleBackColor = True
        '
        'DicMili
        '
        Me.DicMili.AutoSize = True
        Me.DicMili.Location = New System.Drawing.Point(276, 40)
        Me.DicMili.Name = "DicMili"
        Me.DicMili.Size = New System.Drawing.Size(48, 16)
        Me.DicMili.TabIndex = 6
        Me.DicMili.Text = "軍事"
        Me.DicMili.UseVisualStyleBackColor = True
        '
        'DicGeo
        '
        Me.DicGeo.AutoSize = True
        Me.DicGeo.Location = New System.Drawing.Point(6, 62)
        Me.DicGeo.Name = "DicGeo"
        Me.DicGeo.Size = New System.Drawing.Size(48, 16)
        Me.DicGeo.TabIndex = 7
        Me.DicGeo.Text = "国土"
        Me.DicGeo.UseVisualStyleBackColor = True
        '
        'DicRail
        '
        Me.DicRail.AutoSize = True
        Me.DicRail.Location = New System.Drawing.Point(60, 62)
        Me.DicRail.Name = "DicRail"
        Me.DicRail.Size = New System.Drawing.Size(48, 16)
        Me.DicRail.TabIndex = 8
        Me.DicRail.Text = "鉄道"
        Me.DicRail.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Controls.Add(Me.CheckBox10)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 217)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(423, 93)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "エラー処理"
        '
        'CheckBox10
        '
        Me.CheckBox10.AutoSize = True
        Me.CheckBox10.Location = New System.Drawing.Point(8, 18)
        Me.CheckBox10.Name = "CheckBox10"
        Me.CheckBox10.Size = New System.Drawing.Size(252, 16)
        Me.CheckBox10.TabIndex = 0
        Me.CheckBox10.Text = "辞書の読み取り時にエラーが発生したら通知する"
        Me.CheckBox10.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(8, 62)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(212, 16)
        Me.RadioButton1.TabIndex = 1
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "エラーが発生した単語は完全に無視する"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(8, 40)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(260, 16)
        Me.RadioButton2.TabIndex = 2
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "エラーが発生した単語は検索結果一覧に表示する"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'TabOption
        '
        Me.TabOption.Controls.Add(Me.TpDic)
        Me.TabOption.Controls.Add(Me.TpSearchResultItem)
        Me.TabOption.Controls.Add(Me.TpTransfer)
        Me.TabOption.Controls.Add(Me.TpOther)
        Me.TabOption.Location = New System.Drawing.Point(12, 12)
        Me.TabOption.Name = "TabOption"
        Me.TabOption.SelectedIndex = 0
        Me.TabOption.Size = New System.Drawing.Size(452, 374)
        Me.TabOption.TabIndex = 1
        '
        'SettingOption
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 433)
        Me.Controls.Add(Me.TabOption)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "SettingOption"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "オプション"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TpOther.ResumeLayout(False)
        Me.GrpOther.ResumeLayout(False)
        Me.GrpOther.PerformLayout()
        CType(Me.MaxHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaxDisplay, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.MaxSearch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpTextEditor.ResumeLayout(False)
        Me.GrpTextEditor.PerformLayout()
        Me.TpTransfer.ResumeLayout(False)
        Me.GrpYMD.ResumeLayout(False)
        Me.GrpYMD.PerformLayout()
        Me.GrpRuby.ResumeLayout(False)
        Me.GrpRuby.PerformLayout()
        Me.GrpTime.ResumeLayout(False)
        Me.GrpTime.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumberPrecision, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpGraphPlugIn.ResumeLayout(False)
        Me.GrpGraphPlugIn.PerformLayout()
        Me.TpSearchResultItem.ResumeLayout(False)
        Me.GrpDisplayList.ResumeLayout(False)
        Me.GrpDisplayList.PerformLayout()
        Me.TpDic.ResumeLayout(False)
        Me.GrpDicFolder.ResumeLayout(False)
        Me.GrpDicFolder.PerformLayout()
        Me.GrpUseDic.ResumeLayout(False)
        Me.GrpUseDic.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabOption.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDictionary As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents FolderBrowserPlugin As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Setting_Button As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox3 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents OpenFileTextEditor As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FontDialogBase As System.Windows.Forms.FontDialog
    Friend WithEvents TpOther As System.Windows.Forms.TabPage
    Friend WithEvents GrpTextEditor As System.Windows.Forms.GroupBox
    Friend WithEvents SelectTextEditor As System.Windows.Forms.Button
    Friend WithEvents TextEditorOption As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextEditorExeFile As System.Windows.Forms.TextBox
    Friend WithEvents GrpOther As System.Windows.Forms.GroupBox
    Friend WithEvents AlreadyMoveOnly As System.Windows.Forms.CheckBox
    Friend WithEvents lblMaxSearch As System.Windows.Forms.Label
    Friend WithEvents AlreadyNoMakeTab As System.Windows.Forms.CheckBox
    Friend WithEvents lblMaxHistory As System.Windows.Forms.Label
    Friend WithEvents AutoTransfer As System.Windows.Forms.CheckBox
    Friend WithEvents lblMaxDisplay As System.Windows.Forms.Label
    Friend WithEvents lblMaxDisplayBetween As System.Windows.Forms.Label
    Friend WithEvents MaxSearch As System.Windows.Forms.NumericUpDown
    Friend WithEvents MaxDisplay As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblMaxSearchBetween As System.Windows.Forms.Label
    Friend WithEvents lblMaxHistoryBetween As System.Windows.Forms.Label
    Friend WithEvents MaxHistory As System.Windows.Forms.NumericUpDown
    Friend WithEvents TpTransfer As System.Windows.Forms.TabPage
    Friend WithEvents GrpGraphPlugIn As System.Windows.Forms.GroupBox
    Friend WithEvents DisplayGraphicInParagraph As System.Windows.Forms.CheckBox
    Friend WithEvents DisplayGraphic As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents NumberPrecision As System.Windows.Forms.NumericUpDown
    Friend WithEvents GrpTime As System.Windows.Forms.GroupBox
    Friend WithEvents Time24 As System.Windows.Forms.RadioButton
    Friend WithEvents Time12 As System.Windows.Forms.RadioButton
    Friend WithEvents TimeBeat As System.Windows.Forms.CheckBox
    Friend WithEvents GrpRuby As System.Windows.Forms.GroupBox
    Friend WithEvents RubyNotUse As System.Windows.Forms.RadioButton
    Friend WithEvents RubyParent As System.Windows.Forms.RadioButton
    Friend WithEvents RubyUse As System.Windows.Forms.RadioButton
    Friend WithEvents GrpYMD As System.Windows.Forms.GroupBox
    Friend WithEvents YearEra As System.Windows.Forms.RadioButton
    Friend WithEvents YearImperial As System.Windows.Forms.RadioButton
    Friend WithEvents YearBC As System.Windows.Forms.RadioButton
    Friend WithEvents TpSearchResultItem As System.Windows.Forms.TabPage
    Friend WithEvents GrpDisplayList As System.Windows.Forms.GroupBox
    Friend WithEvents lblColumns As System.Windows.Forms.Label
    Friend WithEvents Columns As System.Windows.Forms.ListBox
    Friend WithEvents Down As System.Windows.Forms.Button
    Friend WithEvents SelectedColumns As System.Windows.Forms.ListBox
    Friend WithEvents Up As System.Windows.Forms.Button
    Friend WithEvents lblSelectedColumns As System.Windows.Forms.Label
    Friend WithEvents RemoveColumn As System.Windows.Forms.Button
    Friend WithEvents SelectColumn As System.Windows.Forms.Button
    Friend WithEvents TpDic As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
    Friend WithEvents GrpUseDic As System.Windows.Forms.GroupBox
    Friend WithEvents DicRail As System.Windows.Forms.CheckBox
    Friend WithEvents DicGeo As System.Windows.Forms.CheckBox
    Friend WithEvents DicMili As System.Windows.Forms.CheckBox
    Friend WithEvents DicCul As System.Windows.Forms.CheckBox
    Friend WithEvents DicMoe As System.Windows.Forms.CheckBox
    Friend WithEvents DicSci As System.Windows.Forms.CheckBox
    Friend WithEvents DicTech As System.Windows.Forms.CheckBox
    Friend WithEvents DicWdic As System.Windows.Forms.CheckBox
    Friend WithEvents DicAll As System.Windows.Forms.CheckBox
    Friend WithEvents GrpDicFolder As System.Windows.Forms.GroupBox
    Friend WithEvents lblDirDictionary As System.Windows.Forms.Label
    Friend WithEvents SelectDir As System.Windows.Forms.Button
    Friend WithEvents SelectPlugin As System.Windows.Forms.Button
    Friend WithEvents l3 As System.Windows.Forms.Label
    Friend WithEvents lblDirPlugin As System.Windows.Forms.Label
    Friend WithEvents DirPlugin As System.Windows.Forms.TextBox
    Friend WithEvents DirDictionary As System.Windows.Forms.TextBox
    Friend WithEvents TabOption As System.Windows.Forms.TabControl

End Class
