<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ResultDisplay
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResultDisplay))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.ResultTable = New System.Windows.Forms.DataGridView
        Me.Word = New System.Windows.Forms.DataGridViewLinkColumn
        Me.Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TypeName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TypeLarge = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.TypeSmall = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DirName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DirLarge = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DirSmall = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Pos = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Yomi = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Spell = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Abbr = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Pron = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Filename = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.FullPath = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.LineNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Position = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.MenuStrip = New System.Windows.Forms.MenuStrip
        Me.FileMenuOnResult = New System.Windows.Forms.ToolStripMenuItem
        Me.Display = New System.Windows.Forms.ToolStripMenuItem
        Me.DisplayOnTextEditor = New System.Windows.Forms.ToolStripMenuItem
        Me.Save = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.PrintOnResult = New System.Windows.Forms.ToolStripMenuItem
        Me.PrintPreviewOnResult = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.CloseOnResult = New System.Windows.Forms.ToolStripMenuItem
        Me.EditMenuOnResult = New System.Windows.Forms.ToolStripMenuItem
        Me.CopyOnResult = New System.Windows.Forms.ToolStripMenuItem
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.SelectAllOnResult = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip = New System.Windows.Forms.StatusStrip
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ResultTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(645, 450)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 27)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 21)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        Me.OK_Button.Visible = False
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 21)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "閉じる"
        '
        'ResultTable
        '
        Me.ResultTable.AllowUserToAddRows = False
        Me.ResultTable.AllowUserToDeleteRows = False
        Me.ResultTable.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ResultTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ResultTable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Word, Me.Description, Me.TypeName, Me.TypeLarge, Me.TypeSmall, Me.DirName, Me.DirLarge, Me.DirSmall, Me.Pos, Me.Yomi, Me.Spell, Me.Abbr, Me.Pron, Me.Filename, Me.FullPath, Me.LineNumber, Me.Position})
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Font = New System.Drawing.Font("MS UI Gothic", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer))
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ResultTable.DefaultCellStyle = DataGridViewCellStyle1
        Me.ResultTable.Location = New System.Drawing.Point(12, 39)
        Me.ResultTable.Name = "ResultTable"
        Me.ResultTable.ReadOnly = True
        Me.ResultTable.RowTemplate.Height = 21
        Me.ResultTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ResultTable.Size = New System.Drawing.Size(779, 405)
        Me.ResultTable.TabIndex = 1
        '
        'Word
        '
        Me.Word.DataPropertyName = "Word"
        Me.Word.HeaderText = "単語"
        Me.Word.LinkColor = System.Drawing.Color.Blue
        Me.Word.Name = "Word"
        Me.Word.ReadOnly = True
        Me.Word.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.Word.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'Description
        '
        Me.Description.DataPropertyName = "Description"
        Me.Description.HeaderText = "簡易説明"
        Me.Description.Name = "Description"
        Me.Description.ReadOnly = True
        Me.Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'TypeName
        '
        Me.TypeName.DataPropertyName = "TypeName"
        Me.TypeName.HeaderText = "辞書略称"
        Me.TypeName.Name = "TypeName"
        Me.TypeName.ReadOnly = True
        '
        'TypeLarge
        '
        Me.TypeLarge.DataPropertyName = "TypeLarge"
        Me.TypeLarge.HeaderText = "辞書大分類"
        Me.TypeLarge.Name = "TypeLarge"
        Me.TypeLarge.ReadOnly = True
        '
        'TypeSmall
        '
        Me.TypeSmall.DataPropertyName = "TypeSmall"
        Me.TypeSmall.HeaderText = "辞書小分類"
        Me.TypeSmall.Name = "TypeSmall"
        Me.TypeSmall.ReadOnly = True
        '
        'DirName
        '
        Me.DirName.DataPropertyName = "DirName"
        Me.DirName.HeaderText = "カテゴリ略称"
        Me.DirName.Name = "DirName"
        Me.DirName.ReadOnly = True
        '
        'DirLarge
        '
        Me.DirLarge.DataPropertyName = "DirLarge"
        Me.DirLarge.HeaderText = "カテゴリ"
        Me.DirLarge.Name = "DirLarge"
        Me.DirLarge.ReadOnly = True
        '
        'DirSmall
        '
        Me.DirSmall.DataPropertyName = "DirSmall"
        Me.DirSmall.HeaderText = "ジャンル"
        Me.DirSmall.Name = "DirSmall"
        Me.DirSmall.ReadOnly = True
        '
        'Pos
        '
        Me.Pos.DataPropertyName = "Pos"
        Me.Pos.HeaderText = "品詞"
        Me.Pos.Name = "Pos"
        Me.Pos.ReadOnly = True
        Me.Pos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Yomi
        '
        Me.Yomi.DataPropertyName = "Yomi"
        Me.Yomi.HeaderText = "読み"
        Me.Yomi.Name = "Yomi"
        Me.Yomi.ReadOnly = True
        '
        'Spell
        '
        Me.Spell.DataPropertyName = "Spell"
        Me.Spell.HeaderText = "外語"
        Me.Spell.Name = "Spell"
        Me.Spell.ReadOnly = True
        Me.Spell.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Abbr
        '
        Me.Abbr.DataPropertyName = "Abbr"
        Me.Abbr.HeaderText = "略語"
        Me.Abbr.Name = "Abbr"
        Me.Abbr.ReadOnly = True
        Me.Abbr.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Pron
        '
        Me.Pron.DataPropertyName = "Pron"
        Me.Pron.HeaderText = "発音"
        Me.Pron.Name = "Pron"
        Me.Pron.ReadOnly = True
        Me.Pron.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Filename
        '
        Me.Filename.DataPropertyName = "Filename"
        Me.Filename.HeaderText = "ファイル名"
        Me.Filename.Name = "Filename"
        Me.Filename.ReadOnly = True
        '
        'FullPath
        '
        Me.FullPath.DataPropertyName = "FullPath"
        Me.FullPath.HeaderText = "フルパス名"
        Me.FullPath.Name = "FullPath"
        Me.FullPath.ReadOnly = True
        '
        'LineNumber
        '
        Me.LineNumber.DataPropertyName = "LineNumber"
        Me.LineNumber.HeaderText = "行番号"
        Me.LineNumber.Name = "LineNumber"
        Me.LineNumber.ReadOnly = True
        Me.LineNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'Position
        '
        Me.Position.DataPropertyName = "Position"
        Me.Position.HeaderText = "位置"
        Me.Position.Name = "Position"
        Me.Position.ReadOnly = True
        Me.Position.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileMenuOnResult, Me.EditMenuOnResult})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.MenuStrip.Size = New System.Drawing.Size(803, 24)
        Me.MenuStrip.TabIndex = 2
        Me.MenuStrip.Text = "MenuStrip1"
        '
        'FileMenuOnResult
        '
        Me.FileMenuOnResult.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Display, Me.DisplayOnTextEditor, Me.Save, Me.toolStripSeparator1, Me.PrintOnResult, Me.PrintPreviewOnResult, Me.toolStripSeparator2, Me.CloseOnResult})
        Me.FileMenuOnResult.Name = "FileMenuOnResult"
        Me.FileMenuOnResult.Size = New System.Drawing.Size(66, 20)
        Me.FileMenuOnResult.Text = "ファイル(&F)"
        '
        'Display
        '
        Me.Display.Name = "Display"
        Me.Display.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.Display.Size = New System.Drawing.Size(291, 22)
        Me.Display.Text = "選択された単語を閲覧する"
        '
        'DisplayOnTextEditor
        '
        Me.DisplayOnTextEditor.Name = "DisplayOnTextEditor"
        Me.DisplayOnTextEditor.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Q), System.Windows.Forms.Keys)
        Me.DisplayOnTextEditor.Size = New System.Drawing.Size(291, 22)
        Me.DisplayOnTextEditor.Text = "選択された単語をテキストエディタで開く"
        '
        'Save
        '
        Me.Save.Name = "Save"
        Me.Save.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.Save.Size = New System.Drawing.Size(291, 22)
        Me.Save.Text = "名前を付けて保存(&A)"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(288, 6)
        '
        'PrintOnResult
        '
        Me.PrintOnResult.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintOnResult.Name = "PrintOnResult"
        Me.PrintOnResult.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.P), System.Windows.Forms.Keys)
        Me.PrintOnResult.Size = New System.Drawing.Size(291, 22)
        Me.PrintOnResult.Text = "印刷(&P)"
        '
        'PrintPreviewOnResult
        '
        Me.PrintPreviewOnResult.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintPreviewOnResult.Name = "PrintPreviewOnResult"
        Me.PrintPreviewOnResult.Size = New System.Drawing.Size(291, 22)
        Me.PrintPreviewOnResult.Text = "印刷プレビュー(&V)"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(288, 6)
        '
        'CloseOnResult
        '
        Me.CloseOnResult.Name = "CloseOnResult"
        Me.CloseOnResult.Size = New System.Drawing.Size(291, 22)
        Me.CloseOnResult.Text = "閉じる(&W)"
        '
        'EditMenuOnResult
        '
        Me.EditMenuOnResult.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CopyOnResult, Me.toolStripSeparator4, Me.SelectAllOnResult})
        Me.EditMenuOnResult.Name = "EditMenuOnResult"
        Me.EditMenuOnResult.Size = New System.Drawing.Size(56, 20)
        Me.EditMenuOnResult.Text = "編集(&E)"
        '
        'CopyOnResult
        '
        Me.CopyOnResult.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CopyOnResult.Name = "CopyOnResult"
        Me.CopyOnResult.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys)
        Me.CopyOnResult.Size = New System.Drawing.Size(177, 22)
        Me.CopyOnResult.Text = "コピー(&C)"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(174, 6)
        '
        'SelectAllOnResult
        '
        Me.SelectAllOnResult.Name = "SelectAllOnResult"
        Me.SelectAllOnResult.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys)
        Me.SelectAllOnResult.Size = New System.Drawing.Size(177, 22)
        Me.SelectAllOnResult.Text = "すべて選択(&A)"
        '
        'StatusStrip
        '
        Me.StatusStrip.Location = New System.Drawing.Point(0, 480)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(803, 22)
        Me.StatusStrip.TabIndex = 3
        Me.StatusStrip.Text = "StatusStrip1"
        '
        'ResultDisplay
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(803, 502)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ResultTable)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.MenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip
        Me.MinimumSize = New System.Drawing.Size(393, 330)
        Me.Name = "ResultDisplay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "検索結果"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.ResultTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents ResultTable As System.Windows.Forms.DataGridView
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents FileMenuOnResult As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Save As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintOnResult As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintPreviewOnResult As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CloseOnResult As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditMenuOnResult As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyOnResult As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SelectAllOnResult As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents Word As System.Windows.Forms.DataGridViewLinkColumn
    Friend WithEvents Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TypeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TypeLarge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TypeSmall As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DirName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DirLarge As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DirSmall As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pos As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Yomi As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Spell As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Abbr As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Pron As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Filename As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents FullPath As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LineNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Position As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Display As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DisplayOnTextEditor As System.Windows.Forms.ToolStripMenuItem

End Class
