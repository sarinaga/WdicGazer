Option Strict On
Imports System.Windows.Forms

Public Class SettingOption

#Region "定義"

    ''' <summary>
    ''' 適用ボタンが押されたかどうかを格納するフラグ
    ''' </summary>
    Private OptionSaved As Boolean


    ''' <summary>
    ''' 検索結果の一覧表示で出力される項目の一覧
    ''' </summary>
    Private RowItems() As RowItemAttr = _
        { _
        New RowItemAttr("TypeName", "辞書略称"), _
        New RowItemAttr("TypeLarge", "辞書(大分類)"), _
        New RowItemAttr("TypeSmall", "辞書(小分類)"), _
        New RowItemAttr("Word", "単語"), _
        New RowItemAttr("Description", "簡易説明"), _
        New RowItemAttr("DirName", "カテゴリ略称"), _
        New RowItemAttr("DirLarge", "カテゴリ"), _
        New RowItemAttr("DirSmall", "ジャンル"), _
        New RowItemAttr("Pos", "品詞"), _
        New RowItemAttr("Yomi", "読み"), _
        New RowItemAttr("Spell", "外語"), _
        New RowItemAttr("Abbr", "外語略語"), _
        New RowItemAttr("Pron", "発音"), _
        New RowItemAttr("Filename", "ファイル名"), _
        New RowItemAttr("FullPath", "ファイル名(フルパス)"), _
        New RowItemAttr("LineNumber", "行番号") _
        }

    ''' <summary>
    ''' アプリケーション設定に保存される検索結果一覧表示項目の変数名のプレフィックス
    ''' </summary>
    Private Const PrefixRow As String = "Row"

    ''' <summary>
    ''' 検索結果一覧表示の画面で利用するダミーデータ
    ''' </summary>
    Private Const DummyData As String = "dummy"

#End Region

#Region "初期化、終了"

    ''' <summary>
    ''' 初期化、ボタン処理
    ''' </summary>
    Private Sub SettingOption_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.RegistoryReader()
        OptionSaved = False
        Setting_Button.Enabled = False
        TabOption.SelectedIndex = 0
        SetControlEvent()
    End Sub

    ''' <summary>
    ''' コントロールイベント設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetControlEvent()

        Static Dim already As Boolean = False
        If already Then Exit Sub
        already = True

        ' 辞書タブ(ディレクトリ入力部分は直接設定済)
        For Each c As Control In GrpUseDic.Controls
            AddHandler c.Click, AddressOf ControlsChanged
        Next

        ' 検索結果一覧タブ
        For Each c As Control In GrpDisplayList.Controls
            If TypeOf c Is Button Then
                AddHandler c.Click, AddressOf ControlsChanged
            End If
        Next

        ' 形式タブ
        For Each c1 As Control In TpTransfer.Controls
            If Not TypeOf c1 Is GroupBox Then Continue For
            For Each c2 As Control In c1.Controls
                AddHandler c2.Click, AddressOf ControlsChanged
            Next
        Next

        ' その他タブ
        AddHandler TextEditorExeFile.TextChanged, AddressOf ControlsChanged
        AddHandler TextEditorOption.TextChanged, AddressOf ControlsChanged
        For Each c As Control In GrpOther.Controls
            If TypeOf c Is CheckBox Then
                AddHandler c.Click, AddressOf ControlsChanged
            ElseIf TypeOf c Is NumericUpDown Then
                AddHandler CType(c, NumericUpDown).ValueChanged, AddressOf ControlsChanged
            End If
        Next

    End Sub

#End Region

#Region "基本３ボタン"

    ''' <summary>
    ''' OKボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        If Not IsValidInput() Then Exit Sub
        RegistoryWriter()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    ''' <summary>
    ''' キャンセルボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click

        ' 変更前の値に戻す
        RegistoryReader()

        ' 辞書フォルダが入力されていない場合は強制終了か続行かを選択させる
        ' OKなら全体を終了
        If DirDictionary.Text.Length = 0 Then
            Dim result As MsgBoxResult = MsgBox("辞書フォルダが入力されていないと検索できません。", MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly)
        End If

        ' キャンセルボタンが押されたことを示す
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()

    End Sub

    ''' <summary>
    ''' 適用ボタン押下
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Setting_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Setting_Button.Click
        If Not IsValidInput() Then Exit Sub
        RegistoryWriter()
        Setting_Button.Enabled = False
        OK_Button.Focus()
    End Sub

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsValidInput() As Boolean
        If String.IsNullOrEmpty(DirDictionary.Text) Then
            MsgBox(Main.Environment.ErrorMessage("DIC0000"), MsgBoxStyle.Information)

            Return False
        End If

        If Not IsSelectedDictionary() Then
            MsgBox(Main.Environment.ErrorMessage("MAIN0005"), MsgBoxStyle.Information)
            Return False
        End If
        Return True
    End Function

    ''' <summary>
    ''' 利用辞書が少なくとも1つ選択されていることを確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSelectedDictionary() As Boolean
        Dim selected As Boolean = False
        For Each check As CheckBox In GrpUseDic.Controls
            selected = selected Or check.Checked
        Next
        Return selected
    End Function

#End Region

#Region "入力情報変更チェック"

    ''' <summary>
    ''' 辞書ディレクトリの文字列が変更された場合、そのことを記憶する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DirTextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DirDictionary.TextChanged, DirPlugin.TextChanged
        Setting_Button.Enabled = True
    End Sub

    ''' <summary>
    ''' 画面コントロールが操作された場合、そのことを記録する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ControlsChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Setting_Button.Enabled = True
    End Sub

    ''' <summary>
    ''' 画像表示ボタン変更の場合
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DisplayGraphic_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisplayGraphic.CheckedChanged
        DisplayGraphicInParagraph.Enabled = Not DisplayGraphic.Checked
    End Sub

#End Region

#Region "辞書タブ"

    ''' <summary>
    ''' フォルダ選択(辞書)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectDir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectDir.Click
        FolderBrowserDictionary.SelectedPath = DirDictionary.Text
        If FolderBrowserDictionary.ShowDialog() = DialogResult.OK Then
            DirDictionary.Text = FolderBrowserDictionary.SelectedPath
        End If
    End Sub

    ''' <summary>
    ''' フォルダ選択(プラグイン)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectPlugin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectPlugin.Click
        FolderBrowserPlugin.SelectedPath = DirPlugin.Text
        If FolderBrowserPlugin.ShowDialog() = DialogResult.OK Then
            DirPlugin.Text = FolderBrowserPlugin.SelectedPath
        End If
    End Sub

    ''' <summary>
    ''' 全辞書を検索する場合は辞書選択ボックスを無効化する
    ''' </summary>
    Private Sub DicAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DicAll.CheckedChanged
        For Each c As CheckBox In GrpUseDic.Controls
            If c Is DicAll Then
                c.Enabled = True
            Else
                If DicAll.Checked Then
                    c.Enabled = False
                Else
                    c.Enabled = True
                End If
            End If
        Next
    End Sub

#End Region

#Region "検索結果一覧タブ"

    ''' <summary>
    ''' 項目選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectColumn.Click
        If Columns.SelectedIndex < 0 Then Exit Sub
        SelectedColumns.Items.Add(Columns.SelectedItem)
        Columns.Items.Remove(Columns.SelectedItem)
        Setting_Button.Enabled = True
    End Sub

    ''' <summary>
    ''' 項目削除
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub RemoveColumn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RemoveColumn.Click
        If SelectedColumns.SelectedIndex < 0 Then Exit Sub
        If CType(SelectedColumns.SelectedItem, RowItemAttr).Code = "Word" Then
            MsgBox(String.Format(Main.Environment.ErrorMessage("DIC0000"), "単語"), MsgBoxStyle.Information)
            Exit Sub
        End If
        Columns.Items.Add(SelectedColumns.SelectedItem)
        SelectedColumns.Items.Remove(SelectedColumns.SelectedItem)
        Setting_Button.Enabled = True
    End Sub

    ''' <summary>
    ''' 項目順番変更(上)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Up_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Up.Click
        Dim index As Integer = SelectedColumns.SelectedIndex
        If index < 1 Then Exit Sub
        If SelectedColumns.Items.Count < 2 Then Exit Sub
        Dim temp As RowItemAttr = CType(SelectedColumns.Items(index - 1), RowItemAttr)
        SelectedColumns.Items(index - 1) = SelectedColumns.Items(index)
        SelectedColumns.Items(index) = temp
        SelectedColumns.SelectedIndex = index - 1
        Setting_Button.Enabled = True
    End Sub

    ''' <summary>
    ''' 項目順番変更(下)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub Down_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Down.Click
        Dim index As Integer = SelectedColumns.SelectedIndex
        If index < 0 Then Exit Sub
        If index = SelectedColumns.Items.Count - 1 Then Exit Sub
        If SelectedColumns.Items.Count < 2 Then Exit Sub
        Dim temp As RowItemAttr = CType(SelectedColumns.Items(index + 1), RowItemAttr)
        SelectedColumns.Items(index + 1) = SelectedColumns.Items(index)
        SelectedColumns.Items(index) = temp
        SelectedColumns.SelectedIndex = index + 1
        Setting_Button.Enabled = True
    End Sub

#End Region

#Region "その他タブ"

    ''' <summary>
    ''' テクストエディタ選択
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SelectTextEditor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SelectTextEditor.Click
        Try
            OpenFileTextEditor.InitialDirectory = System.IO.Path.GetDirectoryName(TextEditorExeFile.Text)
            OpenFileTextEditor.FileName = System.IO.Path.GetFileName(TextEditorExeFile.Text)
        Catch ex As Exception
        End Try
        If OpenFileTextEditor.ShowDialog() = DialogResult.OK Then
            TextEditorExeFile.Text = OpenFileTextEditor.FileName
        End If
    End Sub

#End Region

#Region "レジストリ(書き込み)"

    ''' <summary>
    ''' レジストリに値を書き込む
    ''' </summary>
    Public Sub RegistoryWriter()
        RegistoryWriterTpDic()
        RegistoryWriterTpSearchResultItem()
        RegistoryWriterTpTransfer()
        RegistoryWriterTpOther()
        My.Settings.Save()
        OptionSaved = True
    End Sub

    ''' <summary>
    ''' 辞書タブの設定を保存
    ''' </summary>
    Public Sub RegistoryWriterTpDic()
        DirDictionary.Text = DirDictionary.Text.TrimEnd("\"c)
        DirPlugin.Text = DirPlugin.Text.TrimEnd("\"c)
        My.Settings.DirDictionary = DirDictionary.Text
        My.Settings.DirPlugin = DirPlugin.Text
        For Each c As CheckBox In GrpUseDic.Controls
            My.Settings(c.Name) = c.Checked
        Next
    End Sub

    ''' <summary>
    ''' 検索結果一覧の設定を保存
    ''' </summary>
    Public Sub RegistoryWriterTpSearchResultItem()

        ' リフレクション取得
        Dim settingsType As Type = My.Settings.GetType()
        Dim settingsTypeArray() As Reflection.PropertyInfo = settingsType.GetProperties

        ' いったん初期化
        For Each d As RowItemAttr In RowItems
            For Each p As Reflection.PropertyInfo In settingsTypeArray
                If Not p.Name.Substring(0, PrefixRow.Length) = PrefixRow Then Continue For
                If Not p.Name.Substring(PrefixRow.Length) = d.Code Then Continue For
                p.SetValue(My.Settings, -1, Nothing)
            Next
        Next

        ' インデックスを保存
        For i As Integer = 0 To SelectedColumns.Items.Count - 1

            ' 選択された項目を探す
            Dim d As RowItemAttr = Nothing
            For Each j As RowItemAttr In RowItems
                If j.Name = SelectedColumns.Items.Item(i).ToString Then
                    d = j
                    Exit For
                End If
            Next
            If d Is Nothing Then Continue For

            ' 選択された項目の順番を記録する
            For Each p As Reflection.PropertyInfo In settingsTypeArray
                If Not p.Name.Substring(0, PrefixRow.Length) = PrefixRow Then Continue For
                If Not p.Name.Substring(PrefixRow.Length) = d.Code Then Continue For
                p.SetValue(My.Settings, i, Nothing)
            Next
        Next
    End Sub

    ''' <summary>
    ''' 表示形式の設定を保存
    ''' </summary>
    Public Sub RegistoryWriterTpTransfer()
        For Each r As RadioButton In GrpYMD.Controls
            My.Settings.Item(r.Name) = r.Checked
        Next
        For Each r As ButtonBase In GrpTime.Controls
            If TypeOf r Is RadioButton Then
                My.Settings.Item(r.Name) = CType(r, RadioButton).Checked
            ElseIf TypeOf r Is CheckBox Then
                My.Settings.Item(r.Name) = CType(r, CheckBox).Checked
            End If
        Next
        For Each r As RadioButton In GrpRuby.Controls
            My.Settings.Item(r.Name) = r.Checked
        Next
        My.Settings.NumberPrecision = CInt(NumberPrecision.Value)
        For Each r As CheckBox In GrpGraphPlugIn.Controls
            My.Settings.Item(r.Name) = r.Checked
        Next
    End Sub

    ''' <summary>
    ''' その他の設定を保存
    ''' </summary>
    Public Sub RegistoryWriterTpOther()
        My.Settings.TextEditorExeFile = TextEditorExeFile.Text
        My.Settings.TextEditorOption = TextEditorOption.Text
        My.Settings.MaxSearch = CInt(MaxSearch.Value)
        My.Settings.MaxHistory = CInt(MaxHistory.Value)
        My.Settings.MaxDisplay = CInt(MaxDisplay.Value)
        My.Settings.AutoTransfer = AutoTransfer.Checked
        My.Settings.AlreadyNoMakeTab = AlreadyNoMakeTab.Checked
        My.Settings.AlreadyMoveOnly = AlreadyMoveOnly.Checked

    End Sub

#End Region

#Region "アプリケーション設定(読み込み)"

    ''' <summary>
    ''' アプリケーション設定から値を読み込み、コントロールにセットする
    ''' </summary>
    Public Sub RegistoryReader()
        RegistoryReaderTpDic()
        RegistoryReaderTpSearchResultItem()
        RegistoryReaderTpTransfer()
        RegistoryReaderTpOther()
    End Sub

    ''' <summary>
    ''' 辞書タブの値をセットする
    ''' </summary>
    Private Sub RegistoryReaderTpDic()
        DirDictionary.Text = My.Settings.DirDictionary
        DirPlugin.Text = My.Settings.DirPlugin
        For Each c As CheckBox In GrpUseDic.Controls
            c.Checked = CBool(My.Settings(c.Name))
        Next
    End Sub

    ''' <summary>
    ''' 検索結果一覧タブの値をセットする
    ''' </summary>
    Private Sub RegistoryReaderTpSearchResultItem()

        ' アプリケーション設定のリフレクションを取得
        Dim settingsType As Type = My.Settings.GetType()
        Dim settingsTypeArray() As Reflection.PropertyInfo = settingsType.GetProperties

        ' 再描写禁止および初期化
        Columns.BeginUpdate()
        SelectedColumns.BeginUpdate()
        Columns.Items.Clear()
        SelectedColumns.Items.Clear()

        ' 非選択リストに値投入(選択リストは下準備する)
        For Each d As RowItemAttr In RowItems
            For Each p As Reflection.PropertyInfo In settingsTypeArray
                If Not p.Name.Substring(0, PrefixRow.Length) = PrefixRow Then Continue For
                If Not p.Name.Substring(PrefixRow.Length) = d.Code Then Continue For
                Dim index As Integer = CInt(p.GetValue(My.Settings, Nothing))
                If index < 0 Then
                    Columns.Items.Add(d)
                Else
                    SelectedColumns.Items.Add(DummyData)
                End If
            Next
        Next

        ' 選択リストに値投入
        For Each p As Reflection.PropertyInfo In settingsTypeArray
            If Not p.Name.Substring(0, PrefixRow.Length) = PrefixRow Then Continue For
            For Each d As RowItemAttr In RowItems
                If Not p.Name.Substring(PrefixRow.Length) = d.Code Then Continue For
                Dim index As Integer = CInt(p.GetValue(My.Settings, Nothing))
                If index < 0 Then Exit For
                SelectedColumns.Items.Item(index) = d '.ItemName
            Next
        Next

        ' 再描写再開
        Columns.EndUpdate()
        SelectedColumns.EndUpdate()

    End Sub

    ''' <summary>
    ''' 形式タブの値をセットする
    ''' </summary>
    Private Sub RegistoryReaderTpTransfer()
        For Each r As RadioButton In GrpYMD.Controls
            r.Checked = CBool(My.Settings.Item(r.Name))
        Next
        For Each r As ButtonBase In GrpTime.Controls
            If TypeOf r Is RadioButton Then
                CType(r, RadioButton).Checked = CBool(My.Settings.Item(r.Name))
            ElseIf TypeOf r Is CheckBox Then
                CType(r, CheckBox).Checked = CBool(My.Settings.Item(r.Name))
            End If
        Next
        For Each r As RadioButton In GrpRuby.Controls
            r.Checked = CBool(My.Settings.Item(r.Name))
        Next
        NumberPrecision.Value = My.Settings.NumberPrecision
        For Each r As CheckBox In GrpGraphPlugIn.Controls
            r.Checked = CBool(My.Settings.Item(r.Name))
        Next
        DisplayGraphicInParagraph.Enabled = Not DisplayGraphic.Checked

    End Sub

    ''' <summary>
    ''' その他タブの値をセットする
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RegistoryReaderTpOther()
        TextEditorExeFile.Text = My.Settings.TextEditorExeFile
        TextEditorOption.Text = My.Settings.TextEditorOption
        MaxSearch.Value = My.Settings.MaxSearch
        MaxHistory.Value = My.Settings.MaxHistory
        MaxDisplay.Value = My.Settings.MaxDisplay
        AutoTransfer.Checked = My.Settings.AutoTransfer
        AlreadyNoMakeTab.Checked = My.Settings.AlreadyNoMakeTab
        AlreadyMoveOnly.Checked = My.Settings.AlreadyMoveOnly
    End Sub

#End Region

End Class
