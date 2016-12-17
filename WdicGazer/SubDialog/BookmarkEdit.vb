Imports System.Windows.Forms

Public Class BookmarkEdit

#Region "開始・終了イベント"

    ''' <summary>
    ''' 読み込み時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkEdit_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SetBookmarkTree()
    End Sub

    ''' <summary>
    ''' ツリーを作成する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetBookmarkTree()
        BookmarkTree.Nodes.Clear()
        BookmarkTree.Nodes.Add(New TreeNode("Root"))
        Dim root As TreeNode = BookmarkTree.Nodes(0)
        SetBookmarkTreeNode(Main.BookmarkRoot, root)
        RenameFolder.Enabled = False
        DeleteItem.Enabled = False
        CreateFolder.Enabled = False
        If Not root.IsExpanded Then root.Toggle()
    End Sub

    ''' <summary>
    ''' 再帰的にツリーノードを作成する
    ''' </summary>
    ''' <param name="item_node"></param>
    ''' <param name="tree_node"></param>
    ''' <remarks></remarks>
    Private Sub SetBookmarkTreeNode(ByVal item_node As BookmarkItem, ByVal tree_node As TreeNode)
        For Each item As BookmarkItem In item_node.Children
            Dim pos As Integer = tree_node.Nodes.Add(New TreeNode(item.Name))
            If Not item.Children Is Nothing Then
                SetBookmarkTreeNode(item, tree_node.Nodes(pos))
            End If
        Next
    End Sub

    ''' <summary>
    ''' 閉じる
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim newroot As BookmarkItem = New BookmarkItem
        newroot.Name = ""
        newroot.Parents = Nothing
        newroot.Children = SetBookmarkFromTreeNode(BookmarkTree.Nodes(0), newroot)
        Main.BookmarkRoot = newroot
        TreeMenu.SetBookmark()
        My.Settings.BookmarkData = Main.BookmarkRoot.Serialize()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' 編集されたブックマークを保存する
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetBookmarkFromTreeNode(ByVal node As TreeNode, ByVal parents As BookmarkItem) As List(Of BookmarkItem)
        Dim nodes As New List(Of BookmarkItem)
        For Each n As TreeNode In node.Nodes
            Dim item As New BookmarkItem
            item.Name = n.Text
            item.Parents = parents
            If Not n.Text(0) = "/"c Then
                item.Children = SetBookmarkFromTreeNode(n, item)
            Else
                item.Children = Nothing
            End If
            nodes.Add(item)
        Next
        Return nodes

    End Function

    ''' <summary>
    ''' キャンセル
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

#End Region

#Region "ボタンイベント"

    ''' <summary>
    ''' 削除ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DeleteFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteItem.Click
        DeleteBookmarkItem()
    End Sub

    ''' <summary>
    ''' フォルダ名変更ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RenameFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RenameFolder.Click
        RenameBookmarkFolder()
    End Sub

    ''' <summary>
    ''' フォルダ作成ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CreateFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CreateFolder.Click
        CreateBookmarkFolder()
    End Sub

    ''' <summary>
    ''' アイテムの上移動ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MoveItemUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveItemUp.Click
        If Not IsOKItemMove() Then Exit Sub
        Dim selected_node As TreeNode = BookmarkTree.SelectedNode
        Dim parents As TreeNode = selected_node.Parent
        Dim pos As Integer = parents.Nodes.IndexOf(selected_node)
        If pos = 0 Then Exit Sub
        parents.Nodes.RemoveAt(pos)
        parents.Nodes.Insert(pos - 1, selected_node)
        BookmarkTree.SelectedNode = selected_node
        BookmarkTree.Focus()
    End Sub

    ''' <summary>
    ''' アイテムの下移動ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub MoveItemDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MoveItemDown.Click
        If Not IsOKItemMove() Then Exit Sub
        Dim selected_node As TreeNode = BookmarkTree.SelectedNode
        Dim parents As TreeNode = selected_node.Parent
        Dim pos As Integer = parents.Nodes.IndexOf(selected_node)
        If pos = parents.Nodes.Count - 1 Then Exit Sub
        parents.Nodes.RemoveAt(pos)
        parents.Nodes.Insert(pos + 1, selected_node)
        BookmarkTree.SelectedNode = selected_node
        BookmarkTree.Focus()
    End Sub

    ''' <summary>
    ''' アイテム移動時の共通チェック処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsOKItemMove() As Boolean
        If BookmarkTree.SelectedNode Is Nothing Then Return False
        If BookmarkTree.SelectedNode.Parent Is Nothing Then Return False
        Return True
    End Function


#End Region

#Region "ツリービューイベント"

    ''' <summary>
    ''' アイテムが選択されたとき、利用できる機能を変更する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkTree_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles BookmarkTree.AfterSelect
        Dim node As TreeNode = BookmarkTree.SelectedNode
        If node Is Nothing Then Exit Sub

        ' ルートが選択された場合
        If node.Parent Is Nothing Then
            RenameFolder.Enabled = False
            CreateFolder.Enabled = True
            DeleteItem.Enabled = False
            MoveItemUp.Enabled = False
            MoveItemDown.Enabled = False
            Exit Sub
        End If

        ' それ以外が選択された場合
        Dim text As String = node.Text
        Dim enabled As Boolean = Not (text(0) = "/"c)
        RenameFolder.Enabled = enabled
        CreateFolder.Enabled = enabled
        DeleteItem.Enabled = True

        Dim parents As TreeNode = node.Parent
        Dim pos As Integer = parents.Nodes.IndexOf(node)
        MoveItemUp.Enabled = Not (pos = 0)
        MoveItemDown.Enabled = Not (pos = parents.Nodes.Count - 1)

    End Sub

    ''' <summary>
    ''' ドロップ開始処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkTree_ItemDrag(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles BookmarkTree.ItemDrag
        DoDragDrop(e.Item, DragDropEffects.Move)
    End Sub

    ''' <summary>
    ''' ドロップアイテムがEditTreeコントロールに入ったとき、ドロップ可能かどうかを判断してエフェクトを変更する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkTree_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles BookmarkTree.DragEnter
        EffectTrack(sender, e)
    End Sub

    ''' <summary>
    ''' EditTreeコントロール内でドロップアイテムが移動したときの処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkTree_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles BookmarkTree.DragOver
        EffectTrack(sender, e)
    End Sub

    ''' <summary>
    ''' EditTreeドロップ移動処理の共通処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub EffectTrack(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
        Dim node As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)
        If Not node.TreeView Is BookmarkTree Then Exit Sub
        Dim position As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
        Dim destinationNode As TreeNode = CType(sender, TreeView).GetNodeAt(position)
        If destinationNode Is Nothing Then Exit Sub
        If destinationNode Is node.Parent Then
            e.Effect = DragDropEffects.None
        ElseIf Not destinationNode.Text(0) = "/"c Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ''' <summary>
    ''' ドロップ処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BookmarkTree_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles BookmarkTree.DragDrop

        ' TreeNode以外がドロップされたときは終了
        If Not e.Data.GetDataPresent("System.Windows.Forms.TreeNode", False) Then Exit Sub

        ' TreeNodeの移動処理
        Dim position As Point = CType(sender, TreeView).PointToClient(New Point(e.X, e.Y))
        Dim destinationNode As TreeNode = CType(sender, TreeView).GetNodeAt(position)
        If destinationNode Is Nothing Then Exit Sub
        Dim newNode As TreeNode = CType(e.Data.GetData("System.Windows.Forms.TreeNode"), TreeNode)

        ' 移動先が正当でない場合は終了
        If destinationNode Is newNode.TreeView Then Exit Sub

        ' 自分自身に移動している場合は終了(実際には動作しない)
        If destinationNode Is newNode.Parent Then Exit Sub

        ' 移動先に同名のフォルダがないかどうかをチェック
        For Each node As TreeNode In destinationNode.Nodes
            If node.Text = newNode.Text Then
                MsgBox(Main.Environment.ErrorMessage("WORD0003"), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
                Exit Sub
            End If
        Next

        ' ノードを移動する
        destinationNode.Nodes.Add(CType(newNode.Clone, TreeNode))
        destinationNode.Expand()
        newNode.Remove()

    End Sub


#End Region

#Region "実処理"

    ''' <summary>
    ''' ブックマーク削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteBookmarkItem()
        Dim node As TreeNode = BookmarkTree.SelectedNode
        If node Is Nothing Then Exit Sub
        If node.Parent Is Nothing Then Exit Sub
        Dim parents As TreeNode = BookmarkTree.SelectedNode.Parent
        parents.Nodes.Remove(node)
    End Sub

    ''' <summary>
    ''' ブックマーク内のフォルダの名称を変更
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RenameBookmarkFolder()

        Do

            ' 新しいフォルダ名の入力
            InputFolderName.Mode = InputFolderName.InputMode.Rename
            InputFolderName.ShowDialog()

            ' キャンセルのときは処理なし
            If InputFolderName.DialogResult = DialogResult.Cancel Then Exit Sub

            ' 頭に / が入っている場合はエラー、やり直し
            Dim new_folder As String = InputFolderName.FolderName.Text
            If new_folder = "/" Then
                MsgBox(Main.Environment.ErrorMessage("WORD0001"), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
                Continue Do
            End If

            ' 名称変更がない場合は処理なし
            Dim selected_node As TreeNode = BookmarkTree.SelectedNode
            If selected_node.Text = new_folder Then Exit Sub

            ' 同じ名称のフォルダがある場合はエラー、やり直し
            Dim i As Integer = selected_node.Nodes.IndexOfKey(new_folder)
            If selected_node.Nodes.IndexOfKey(new_folder) < 0 Then
                MsgBox(Main.Environment.ErrorMessage("WORD0002"), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
                Continue Do
            End If

            ' 名称変更
            selected_node.Text = new_folder

        Loop While False

    End Sub


    ''' <summary>
    ''' ブックマーク内にフォルダを作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateBookmarkFolder()

        Do

            ' 新しいフォルダ名の入力
            InputFolderName.Mode = InputFolderName.InputMode.Create
            InputFolderName.ShowDialog()

            ' キャンセルのときは処理なし
            If InputFolderName.DialogResult = DialogResult.Cancel Then Exit Sub

            ' 頭に / が入っている場合はエラー
            Dim new_folder As String = InputFolderName.FolderName.Text
            If new_folder = "/" Then
                MsgBox(Main.Environment.ErrorMessage("WORD0001"), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
                Continue Do
            End If

            ' 新しいフォルダを作る場所を決定
            Dim new_node As New TreeNode(new_folder)
            Dim selected_node As TreeNode = BookmarkTree.SelectedNode
            Dim spot_node As TreeNode = Nothing
            If selected_node.Parent Is Nothing Then
                ' ルートノードが選択されていた場合
                spot_node = selected_node

            ElseIf selected_node.Text(0) = "/" Then
                ' 単語ノードが選択されていた場合
                spot_node = selected_node.Parent

            Else
                ' ディレクトリノードが選択されていた場合
                spot_node = selected_node
            End If

            ' 同じ階層に同名のフォルダがないかを調べる
            For Each node As TreeNode In spot_node.Nodes
                If node.Text = new_node.Text Then
                    Dim error_code As String = Nothing
                    If new_node.Text(0) = "/" Then
                        error_code = "WORD0004"
                    Else
                        error_code = "WORD0003"
                    End If
                    MsgBox(Main.Environment.ErrorMessage(error_code), MsgBoxStyle.Information Or MsgBoxStyle.OkOnly)
                    Continue Do
                End If
            Next

            ' フォルダの追加
            spot_node.Nodes.Add(new_node)
            spot_node.Expand()
            BookmarkTree.SelectedNode = new_node
            BookmarkTree.Focus()

        Loop While False

    End Sub


#End Region



End Class
