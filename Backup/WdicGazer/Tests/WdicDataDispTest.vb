Imports System.Windows.Forms

Public Class WdicDataDispTest

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub DataSelect_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataSelect.SelectedIndexChanged
        If Main.Environment Is Nothing Then Exit Sub
        Select Case DataSelect.Text
            Case "ErrorMessage"
                WdicDataView.DataSource = Main.Environment.ErrorMessageTable
            Case "LangCode"
                WdicDataView.DataSource = Main.Environment.LangCodeTable
            Case "CountryCode"
                WdicDataView.DataSource = Main.Environment.ConturyCodeTable
            Case "Era"
                WdicDataView.DataSource = Main.Environment.EraTable
            Case "Entity"
                WdicDataView.DataSource = Main.Environment.EntityTable
            Case "Wdic"
                WdicDataView.DataSource = Main.Environment.WdicTable
            Case "Group"
                WdicDataView.DataSource = Main.Environment.GroupTable
            Case "WlfFile"
                WdicDataView.DataSource = Main.Environment.WlfFileTable
            Case "DicFileList"
                WdicDataView.DataSource = Main.Environment.DicFileTable
            Case "PlfFile"
                WdicDataView.DataSource = Main.Environment.PlfFileTable
            Case "PluginFile"
                WdicDataView.DataSource = Main.Environment.PluginFileTable
            Case "Category"
                WdicDataView.DataSource = Main.Environment.CategoryTable

        End Select
    End Sub

    Private Sub TestWdicDataDisp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DataSelect.SelectedIndex = 0
        DataSelect.Enabled = Not (Main.Environment Is Nothing)
    End Sub
End Class
