Public Class TreeMenu

    ''' <summary>
    ''' ブックマークメニューを設定する(共通)
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub SetBookmark()
        SetBookmarkToDropDown(Main.BookmarkMenuOnMain.DropDown)
        SetBookmarkToDropDown(WordDisplay.BookMarkMenuOnWord.DropDown)
    End Sub

    ''' <summary>
    ''' ブックマークメニューを設定する(ToolStripDropDown単位)
    ''' </summary>
    ''' <param name="menu"></param>
    ''' <remarks></remarks>
    Public Shared Sub SetBookmarkToDropDown(ByVal menu As ToolStripDropDown)

        ' 消去される基点を取得
        Dim index As Integer = -1
        For i As Integer = 0 To menu.Items.Count - 1
            If TypeOf menu.Items(i) Is ToolStripSeparator Then
                index = i + 1
                Exit For
            End If
        Next
        If index < 0 Then Exit Sub

        ' 基点以降のアイテムを消去
        For i As Integer = menu.Items.Count - 1 To index Step -1
            menu.Items.RemoveAt(i)
        Next

        ' アイテムを追加
        If Main.BookmarkRoot.Children.Count = 0 Then
            Dim tsi As ToolStripMenuItem = CType(menu.Items.Add(My.Resources.NO_ITEM), ToolStripMenuItem)
            tsi.Enabled = False
        Else
            CreateChildDropDown(Main.BookmarkRoot, menu)
        End If

    End Sub

    ''' <summary>
    ''' ブックマークメニューの子メニューを作成
    ''' </summary>
    ''' <param name="dir"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function CreateChildDropDown(ByVal dir As BookmarkItem, Optional ByVal ts As ToolStripDropDown = Nothing) As ToolStripDropDown
        For Each item As BookmarkItem In dir.Children
            Dim add_item As ToolStripMenuItem = CType(ts.Items.Add(item.Name), ToolStripMenuItem)
            If Not item.Children Is Nothing Then
                Dim ddm As New ToolStripDropDownMenu
                ddm.RenderMode = ToolStripRenderMode.System
                add_item.DropDown = CreateChildDropDown(item, ddm)
            Else
                AddHandler add_item.Click, AddressOf BookmarkItem_Click
            End If
        Next
        Return ts
    End Function

    ''' <summary>
    ''' 履歴メニューを設定する(共通)
    ''' </summary>
    Public Shared Sub SetHistory()
        SetHistoryToDropDown(Main.HistoryMenuOnMain.DropDown)
        SetHistoryToDropDown(WordDisplay.HistoryMenuOnWord.DropDown)
    End Sub

    ''' <summary>
    ''' 履歴メニューを設定する(ToolStripDropDown単位)
    ''' </summary>
    Public Shared Sub SetHistoryToDropDown(ByVal ts As ToolStripDropDown)
        ts.Items.Clear()
        If Main.History.Children.Count = 0 Then
            Dim item As ToolStripItem = ts.Items.Add(My.Resources.NO_ITEM)
            item.Enabled = False
            Exit Sub
        End If
        For Each h_item As BookmarkItem In Main.History.Children
            Dim t_item As ToolStripItem = ts.Items.Add(h_item.Name)
            AddHandler t_item.Click, AddressOf TreeMenu.BookmarkItem_Click
        Next
    End Sub


    ''' <summary>
    ''' ブックマークアイテムが選択されたときの処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Friend Shared Sub BookmarkItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim item As ToolStripItem = CType(sender, ToolStripItem)
        Dim type As String = WordDisplay.GetTypename(item.Text)
        Dim word As String = WordDisplay.RemoveTypename(item.Text)
        Dim result As ResultListData = WordDisplay.SearchWordForHyperLinkInner(type, word)
        If result Is Nothing Then Exit Sub
        If Not result.ResultList.Rows.Count = 1 Then Exit Sub
        Dim dr As DataRow = result.ResultList.Rows(0)
        Dim fullpath As String = CStr(dr.Item(ResultListData.Col_FullPath))
        Dim position As Integer = CInt(dr.Item(ResultListData.Col_Position))
        WordDisplay.Show()
        WordDisplay.AddTab(type, word, fullpath, position)
        WordDisplay.SwitchViewWordHeader()
    End Sub


End Class


