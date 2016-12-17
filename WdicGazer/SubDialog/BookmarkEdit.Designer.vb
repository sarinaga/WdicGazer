<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class BookmarkEdit
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
        Me.OK_Button = New System.Windows.Forms.Button
        Me.BookmarkTree = New System.Windows.Forms.TreeView
        Me.CreateFolder = New System.Windows.Forms.Button
        Me.RenameFolder = New System.Windows.Forms.Button
        Me.DeleteItem = New System.Windows.Forms.Button
        Me.MoveItemUp = New System.Windows.Forms.Button
        Me.MoveItemDown = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'OK_Button
        '
        Me.OK_Button.Location = New System.Drawing.Point(254, 261)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(115, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "閉じる"
        '
        'BookmarkTree
        '
        Me.BookmarkTree.AllowDrop = True
        Me.BookmarkTree.Location = New System.Drawing.Point(12, 12)
        Me.BookmarkTree.Name = "BookmarkTree"
        Me.BookmarkTree.Size = New System.Drawing.Size(357, 214)
        Me.BookmarkTree.TabIndex = 1
        '
        'CreateFolder
        '
        Me.CreateFolder.Location = New System.Drawing.Point(133, 232)
        Me.CreateFolder.Name = "CreateFolder"
        Me.CreateFolder.Size = New System.Drawing.Size(115, 23)
        Me.CreateFolder.TabIndex = 2
        Me.CreateFolder.Text = "新しいフォルダ(&N)"
        Me.CreateFolder.UseVisualStyleBackColor = True
        '
        'RenameFolder
        '
        Me.RenameFolder.Location = New System.Drawing.Point(254, 232)
        Me.RenameFolder.Name = "RenameFolder"
        Me.RenameFolder.Size = New System.Drawing.Size(115, 23)
        Me.RenameFolder.TabIndex = 3
        Me.RenameFolder.Text = "名前の変更(&R)"
        Me.RenameFolder.UseVisualStyleBackColor = True
        '
        'DeleteItem
        '
        Me.DeleteItem.Location = New System.Drawing.Point(133, 261)
        Me.DeleteItem.Name = "DeleteItem"
        Me.DeleteItem.Size = New System.Drawing.Size(115, 23)
        Me.DeleteItem.TabIndex = 4
        Me.DeleteItem.Text = "削除(&D)"
        Me.DeleteItem.UseVisualStyleBackColor = True
        '
        'MoveItemUp
        '
        Me.MoveItemUp.Location = New System.Drawing.Point(12, 232)
        Me.MoveItemUp.Name = "MoveItemUp"
        Me.MoveItemUp.Size = New System.Drawing.Size(115, 23)
        Me.MoveItemUp.TabIndex = 5
        Me.MoveItemUp.Text = "上に移動(&U)"
        Me.MoveItemUp.UseVisualStyleBackColor = True
        '
        'MoveItemDown
        '
        Me.MoveItemDown.Location = New System.Drawing.Point(12, 261)
        Me.MoveItemDown.Name = "MoveItemDown"
        Me.MoveItemDown.Size = New System.Drawing.Size(115, 23)
        Me.MoveItemDown.TabIndex = 6
        Me.MoveItemDown.Text = "下に移動(&D)"
        Me.MoveItemDown.UseVisualStyleBackColor = True
        '
        'BookmarkEdit
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 296)
        Me.Controls.Add(Me.MoveItemDown)
        Me.Controls.Add(Me.MoveItemUp)
        Me.Controls.Add(Me.DeleteItem)
        Me.Controls.Add(Me.RenameFolder)
        Me.Controls.Add(Me.CreateFolder)
        Me.Controls.Add(Me.BookmarkTree)
        Me.Controls.Add(Me.OK_Button)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "BookmarkEdit"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "ブックマークの編集"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents BookmarkTree As System.Windows.Forms.TreeView
    Friend WithEvents CreateFolder As System.Windows.Forms.Button
    Friend WithEvents RenameFolder As System.Windows.Forms.Button
    Friend WithEvents DeleteItem As System.Windows.Forms.Button
    Friend WithEvents MoveItemUp As System.Windows.Forms.Button
    Friend WithEvents MoveItemDown As System.Windows.Forms.Button

End Class
