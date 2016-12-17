Imports System.Windows.Forms

Public Class MainTest

    ''' <summary>
    ''' 初回起動時設定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TestMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    End Sub

    ''' <summary>
    ''' OK(実際には起動しない)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' 閉じる(実際にはキャンセル)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' 環境未定義
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TestEnvironmentView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestEnvironmentView.Click
        WdicDataDispTest.Show()
    End Sub

    ''' <summary>
    ''' 年号一覧を表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TestEraCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestEraCheck.Click
        TestEraCheck.Show()
    End Sub

    ''' <summary>
    ''' 単語チェック(未定義)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub TestWordCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TestWordCheck.Click

    End Sub

    ''' <summary>
    ''' アプリケーション設定の削除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ClearSetting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearSetting.Click
        My.Settings.Reset()
        MsgBox("アプリケーション設定を消去しました")
    End Sub

End Class
