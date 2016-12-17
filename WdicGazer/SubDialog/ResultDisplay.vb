Option Strict On
Imports System.Windows.Forms
Imports System.Data

''' <summary>
''' 検索結果が２つ以上になったときにを表示するダイアログ画面
''' </summary>
Public Class ResultDisplay

#Region "定義"

    ''' <summary>
    ''' 検索結果一覧
    ''' </summary>
    ''' <remarks></remarks>
    Private Result As ResultListData

    ''' <summary>
    ''' 検索条件
    ''' </summary>
    ''' <remarks></remarks>
    Protected Condition As SearchConditionData

#Region "定数"

    ''' <summary>
    ''' 表示される列とその位置を記録した設定環境変数のプレフィックス
    ''' </summary>
    ''' <remarks></remarks>
    Private Const RowPrefix As String = "Row"

    ''' <summary>
    ''' 簡易説明に表示する文字数
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DescriptionLength As Integer = 40

#End Region

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 表示する検索結果
    ''' </summary>
    ''' <value>表示する検索結果</value>
    ''' <returns>表示している検索結果</returns>
    ''' <remarks>WordSearcherを実行したあとの検索結果</remarks>
    Public Property WordList() As ResultListData
        Get
            Return Result
        End Get
        Set(ByVal value As ResultListData)
            Result = value
        End Set
    End Property

    ''' <summary>
    ''' 表示する検索条件
    ''' </summary>
    ''' <value>検索結果を得るのに利用した検索条件</value>
    ''' <returns>検索結果を得るのに利用した検索条件</returns>
    ''' <remarks>WordSearcherを実行した時に利用した物を格納</remarks>
    Public Property SearchCondition() As SearchConditionData
        Get
            Return Condition
        End Get
        Set(ByVal value As SearchConditionData)
            Condition = value
        End Set
    End Property

#End Region

#Region "初期化・終了"

    ''' <summary>
    ''' 検索結果を表に表示する
    ''' </summary>
    ''' <param name="value">検索結果</param>
    Protected Friend Sub DataSet(ByVal value As ResultListData)
        WordList = value
        DataSet()
    End Sub

    ''' <summary>
    ''' 検索結果を表に表示する
    ''' </summary>
    Protected Friend Sub DataSet()

        ' データバインド
        Dim dt As DataTable = Result.ResultList
        If dt Is Nothing Then Throw New System.ArgumentNullException
        ResultTable.DataSource = dt

        ' いったん列を全部非表示にする
        For Each col As DataGridViewColumn In ResultTable.Columns
            col.Visible = False
        Next

        ' 表示される項目の順番並べ替え/表示・非表示設定
        Dim settingsType As Type = My.Settings.GetType()
        Dim settingsTypeArray() As Reflection.PropertyInfo = settingsType.GetProperties
        For Each p As Reflection.PropertyInfo In settingsTypeArray
            Dim name As String = p.Name
            If Not name.Substring(0, RowPrefix.Length) = RowPrefix Then Continue For
            Dim index As Integer = CInt(p.GetValue(My.Settings, Nothing))
            Dim d As DataGridViewColumn = ResultTable.Columns(name.Substring(RowPrefix.Length))
            If d Is Nothing Then
                Continue For
            End If
            If index >= 0 Then
                d.Visible = True
                d.DisplayIndex = index
            Else
                d.Visible = d.Visible
            End If
        Next

        ' 列の幅を決定
        Dim g As Graphics = Graphics.FromHwnd(ResultTable.Handle)
        Dim sf As New StringFormat(StringFormat.GenericTypographic)
        Dim col_count As Integer = ResultTable.ColumnCount
        Dim col_str() As String = New String(col_count - 1) {}
        For i As Integer = 0 To col_count - 1
            col_str(i) = ""
        Next
        For Each row As DataGridViewRow In ResultTable.Rows
            For i As Integer = 0 To col_count - 1
                Dim cell_str As String = CStr(row.Cells(i).Value)
                If cell_str.Length > DescriptionLength Then cell_str = cell_str.Substring(0, DescriptionLength)
                If cell_str.Length > col_str(i).Length Then col_str(i) = cell_str
            Next
        Next
        For i As Integer = 0 To col_count - 1

            ' セルの横幅を取得
            Dim word_cell As String = col_str(i)
            If word_cell.Length < 5 Then word_cell = "ＭＭＭＭＭ"
            Dim width_cell As Single = g.MeasureString(word_cell & "ＭＭＭ", ResultTable.Font, ResultTable.Width, sf).Width

            ' ヘッダの横幅を取得
            Dim word_header As String = CStr(ResultTable.Columns(i).HeaderCell.Value)
            Dim width_header As Single = g.MeasureString(word_header & "ＭＭＭ", ResultTable.Font, ResultTable.Width, sf).Width
            ResultTable.Columns(i).Width = Math.Max(CInt(width_cell), CInt(width_header))
            ResultTable.Columns(i).MinimumWidth = CInt(width_header)

        Next
        g.Dispose()

        ' ソート等設定
        ResultTable.Sort(ResultTable.Columns(ResultListData.Col_Word), System.ComponentModel.ListSortDirection.Ascending)
        ResultTable.Focus()

        ' 検索結果キャッシュの設定
        For Each dr As DataRow In dt.Rows
        Next

    End Sub

    ''' <summary>
    ''' OKボタン(非動作)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' 閉じるボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    ''' <summary>
    ''' 閉じるメニュー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CloseByMenu_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CloseOnResult.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


#End Region

#Region "単語表示"

    ''' <summary>
    ''' 行がクリックされたときの動作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ResultTable_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ResultTable.CellContentClick

        ' 単語部分が選択された場合は遷移する
        Dim dgv As DataGridView = CType(sender, DataGridView)
        Dim row As Integer = e.RowIndex
        If dgv.Columns(e.ColumnIndex).Name = "Word" Then
            CallWordDisplay(e.RowIndex)
        End If

    End Sub

    ''' <summary>
    ''' 行がダブルクリックされたときの動作
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ResultTable_CellContentDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles ResultTable.CellContentDoubleClick
        CallWordDisplay(e.RowIndex)
    End Sub

    ''' <summary>
    ''' 単語の表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Display_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Display.Click
        For i As Integer = 0 To ResultTable.Rows.Count - 1
            If ResultTable.Rows(i).Selected Then
                If Not CallWordDisplay(i) Then Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' 単語選択後、単語表示画面に遷移(共通処理)
    ''' </summary>
    ''' <param name="line">データを表示する行数</param>
    ''' <remarks></remarks>
    Private Function CallWordDisplay(ByVal line As Integer) As Boolean
        If line < 0 Then Exit Function

        ' 結果をクリックしたとき
        ' 1. Group名
        ' 2. 検索する単語
        ' 3. ファイルフルパス
        ' 4. 場所
        Dim dr As DataGridViewRow = ResultTable.Rows(line)
        Dim typename As String = CStr(dr.Cells(ResultListData.Col_TypeName).Value)
        Dim word As String = CStr(dr.Cells(ResultListData.Col_Word).Value)
        Dim fullpath As String = CStr(dr.Cells(ResultListData.Col_FullPath).Value)
        Dim position As Integer = CInt(dr.Cells(ResultListData.Col_Position).Value)

        ' 新しいウィンドウを開いて、新しいタブを作成する
        WordDisplay.Show()
        If Not WordDisplay.AddTab(typename, word, fullpath, position) Then
            Return False
        End If
        WordDisplay.WindowState = FormWindowState.Normal
        WordDisplay.SwitchViewWordHeader()
        Return True

    End Function

#End Region

#Region "テキストエディタ表示"


    ''' <summary>
    ''' テキストエディタを呼び出す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DisplayOnTextEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisplayOnTextEditor.Click
        CallTextEditor()
    End Sub

    ''' <summary>
    ''' テキストエディタを呼び出す(共通処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CallTextEditor()

        Dim files As New Hashtable
        Dim exe As String = CStr(My.Settings(SettingOption.TextEditorExeFile.Name))
        Dim arg_orig As String = My.Settings.TextEditorOption

        For Each dr As DataGridViewRow In ResultTable.SelectedRows

            Dim line As Integer = CInt(dr.Cells("LineNumber").Value)
            Dim file As String = CStr(dr.Cells("FullPath").Value)

            If files.ContainsKey(file) Then Continue For
            files.Add(file, Nothing)

            Dim arg As String = arg_orig.Replace("%d", CStr(line)).Replace("%s", file)

            Try
                Process.Start(exe, arg)
            Catch ex As Exception
                MsgBox(Main.Environment.ErrorMessage("SYS0003"), MsgBoxStyle.Information)
                Exit For
            End Try

        Next

    End Sub

#End Region

#Region "クリップボード操作・データ保存"

    ''' <summary>
    ''' 全データ選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectAllOnResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectAllOnResult.Click
        ResultTable.SelectAll()
    End Sub

    ''' <summary>
    ''' データコピー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub CopyOnResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CopyOnResult.Click
        Dim tsv As String = CreateTsvData()
        Clipboard.SetText(tsv, TextDataFormat.Text)
    End Sub


    ''' <summary>
    ''' クリップボード、データ保存用のデータを作成(TSV)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateTsvData() As String

        Dim indexes As New Hashtable
        Dim tsv As New System.Text.StringBuilder

        ' ヘッダ部分を作成(指定されたときのみ)
        If My.Settings.IncludeHeader Then
            For i As Integer = 0 To ResultTable.Columns.Count - 1
                For Each dc As DataGridViewColumn In ResultTable.Columns
                    If Not dc.DisplayIndex = i Then Continue For
                    indexes.Add(dc.DisplayIndex, dc.Index)
                    If Not dc.Visible Then Exit For
                    tsv.Append(dc.HeaderText & vbTab)
                    Exit For
                Next
            Next
            tsv.Remove(tsv.Length - 1, 1)
            tsv.Append(vbCrLf)
        End If

        ' データ部分を作成
        For i As Integer = 0 To ResultTable.Rows.Count - 1

            Dim dr As DataGridViewRow = ResultTable.Rows(i)
            If Not dr.Selected Then Continue For

            For j As Integer = 0 To ResultTable.Columns.Count - 1
                Dim dc As DataGridViewColumn = ResultTable.Columns(CInt(indexes(j)))
                If Not dc.Visible Then Continue For
                Dim cell As DataGridViewCell = ResultTable.Rows(i).Cells(CInt(indexes(j)))
                tsv.Append(cell.Value.ToString & vbTab)
            Next

            tsv.Remove(tsv.Length - 1, 1)
            tsv.Append(vbCrLf)

        Next

        Return tsv.ToString

    End Function




#End Region

#Region "その他"

    ''' <summary>
    ''' 行番号表示
    ''' </summary>
    Private Sub ResultTable_RowPostPaint(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowPostPaintEventArgs) Handles ResultTable.RowPostPaint

        ' 行ヘッダのセル領域を、行番号を描画する長方形とする
        ' （ただし右端に4ドットのすき間を空ける）
        Dim rect As New Rectangle( _
          e.RowBounds.Location.X, _
          e.RowBounds.Location.Y, _
          ResultTable.RowHeadersWidth - 4, _
          ResultTable.Rows(e.RowIndex).Height)

        ' 上記の長方形内に行番号を縦方向中央＆右詰で描画する
        ' フォントや色は行ヘッダの既定値を使用する
        TextRenderer.DrawText( _
          e.Graphics, _
          (e.RowIndex + 1).ToString(), _
          ResultTable.RowHeadersDefaultCellStyle.Font, _
          rect, _
          ResultTable.RowHeadersDefaultCellStyle.ForeColor, _
          TextFormatFlags.VerticalCenter Or TextFormatFlags.Right)
    End Sub

#End Region

End Class
