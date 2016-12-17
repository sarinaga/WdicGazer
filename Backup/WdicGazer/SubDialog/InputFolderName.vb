Imports System.Windows.Forms

Public Class InputFolderName

    ''' <summary>
    ''' フォルダの入力モード
    ''' </summary>
    ''' <remarks></remarks>
    Public Mode As InputMode

    ''' <summary>
    ''' 入力モード種類
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum InputMode
        Create
        Rename
    End Enum

    ''' <summary>
    ''' オープン初期化
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub InputFolderName_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If Mode = InputMode.Create Then
            Me.Text = "新しく作るフォルダ名"
        Else
            Me.Text = "変更後のフォルダ名"
        End If
        FolderName.Text = ""
        FolderName.Focus()
    End Sub

    ''' <summary>
    ''' OKボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If FolderName.Text.Length = 0 Then
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Else
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        End If
        Me.Close()
    End Sub

    ''' <summary>
    ''' キャンセルボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub



End Class
