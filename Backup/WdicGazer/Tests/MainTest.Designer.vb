<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainTest
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.TestEnvironmentView = New System.Windows.Forms.Button
        Me.TestEraCheck = New System.Windows.Forms.Button
        Me.TestWordCheck = New System.Windows.Forms.Button
        Me.ClearSetting = New System.Windows.Forms.Button
        Me.TableLayoutPanel1.SuspendLayout()
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
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(277, 253)
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
        'TestEnvironmentView
        '
        Me.TestEnvironmentView.Location = New System.Drawing.Point(12, 12)
        Me.TestEnvironmentView.Name = "TestEnvironmentView"
        Me.TestEnvironmentView.Size = New System.Drawing.Size(411, 23)
        Me.TestEnvironmentView.TabIndex = 5
        Me.TestEnvironmentView.Text = "デバッグ用データ表示"
        Me.TestEnvironmentView.UseVisualStyleBackColor = True
        '
        'TestEraCheck
        '
        Me.TestEraCheck.Location = New System.Drawing.Point(12, 41)
        Me.TestEraCheck.Name = "TestEraCheck"
        Me.TestEraCheck.Size = New System.Drawing.Size(411, 23)
        Me.TestEraCheck.TabIndex = 6
        Me.TestEraCheck.Text = "年号チェック"
        Me.TestEraCheck.UseVisualStyleBackColor = True
        '
        'TestWordCheck
        '
        Me.TestWordCheck.Location = New System.Drawing.Point(12, 70)
        Me.TestWordCheck.Name = "TestWordCheck"
        Me.TestWordCheck.Size = New System.Drawing.Size(411, 23)
        Me.TestWordCheck.TabIndex = 7
        Me.TestWordCheck.Text = "単語チェック"
        Me.TestWordCheck.UseVisualStyleBackColor = True
        '
        'ClearSetting
        '
        Me.ClearSetting.Location = New System.Drawing.Point(12, 99)
        Me.ClearSetting.Name = "ClearSetting"
        Me.ClearSetting.Size = New System.Drawing.Size(411, 23)
        Me.ClearSetting.TabIndex = 8
        Me.ClearSetting.Text = "アプリケーション設定消去"
        Me.ClearSetting.UseVisualStyleBackColor = True
        '
        'TestMain
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(435, 291)
        Me.Controls.Add(Me.ClearSetting)
        Me.Controls.Add(Me.TestWordCheck)
        Me.Controls.Add(Me.TestEraCheck)
        Me.Controls.Add(Me.TestEnvironmentView)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TestMain"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "TestMain"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents TestEnvironmentView As System.Windows.Forms.Button
    Friend WithEvents TestEraCheck As System.Windows.Forms.Button
    Friend WithEvents TestWordCheck As System.Windows.Forms.Button
    Friend WithEvents ClearSetting As System.Windows.Forms.Button

End Class
