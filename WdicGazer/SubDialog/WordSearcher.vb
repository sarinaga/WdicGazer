Option Strict On
Imports System.Windows.Forms
Imports System.Text.RegularExpressions

''' <summary>
''' 単語の検索処理とその進行状況を行う
''' </summary>
''' <remarks></remarks>
Public Class WordSearcher

#Region "定義"

    ''' <summary>
    ''' キャンセルボタンが押下されたときのフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Protected Canceled As Boolean = False

    ''' <summary>
    ''' 検索結果を格納
    ''' </summary>
    ''' <remarks></remarks>
    Protected List As ResultListData

    ''' <summary>
    ''' Mainで指定された検索条件
    ''' </summary>
    ''' <remarks></remarks>
    Protected Condition As SearchConditionData

    ''' <summary>
    ''' 検索条件を正規表現にしたもの
    ''' </summary>
    ''' <remarks></remarks>
    Protected RegexPattern As Regex

#Region "定数"

    ''' <summary>
    ''' 未定義のデータを表示するときの文字列
    ''' </summary>
    ''' <remarks></remarks>
    Protected Const UNDEFINED As String = "(未定義)"

    ''' <summary>
    ''' 通信用語の基礎知識を意味する辞書略称
    ''' </summary>
    Protected Const WDIC As String = "WDIC"

    ''' <summary>
    ''' 通信用語の基礎知識を意味する辞書略称
    ''' </summary>
    Protected Const TECH As String = "TECH"

    ''' <summary>
    ''' 通信用語の基礎知識を意味する辞書略称
    ''' </summary>
    Protected Const SCI As String = "SCI"

    ''' <summary>
    ''' 通信用語の基礎知識を意味する辞書略称
    ''' </summary>
    Protected Const MOE As String = "MOE"

    ''' <summary>
    ''' 通信用語の基礎知識を意味する辞書略称
    ''' </summary>
    Protected Const CUL As String = "CUL"

    ''' <summary>
    ''' 通信用語の基礎知識を意味する辞書略称
    ''' </summary>
    Protected Const MILI As String = "MILI"

    ''' <summary>
    ''' 通信用語の基礎知識を意味する辞書略称
    ''' </summary>
    Protected Const GEO As String = "GEO"

    ''' <summary>
    ''' 通信用語の基礎知識を意味する辞書略称
    ''' </summary>
    Protected Const RAIL As String = "RAIL"

    ''' <summary>
    ''' 古語であること笑わすプレフィックス
    ''' </summary>
    Protected Const OLD_WORD As String = "(古)"

#End Region

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 検索結果を返す
    ''' </summary>
    ''' <returns>検索結果</returns>
    ''' <remarks>SearchedWordDataクラス</remarks>
    Public ReadOnly Property SearchResult() As ResultListData
        Get
            Return List
        End Get
    End Property

    ''' <summary>
    ''' 検索条件を指定する
    ''' </summary>
    ''' <value>指定する検索条件</value>
    ''' <returns>指定されている検索条件</returns>
    ''' <remarks>SearchConditionDataクラス</remarks>
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
    ''' 画面を開くと同時に検索処理を開始する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub WordSearcher_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        List = New ResultListData()
        Canceled = False
        Try
            Search()
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Catch ex As CancelException
            ' キャンセルボタンが押下された
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel

        Catch ex As RersultOverflowException
            ' 検索結果が所定の件数を超えた場合
            Dim mes As String = Main.Environment.ErrorMessage("RESULT0004")
            MsgBox(String.Format(mes, My.Settings.MaxSearch), MsgBoxStyle.Information)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK

        Catch ex As Exception
            ' 不明なエラー
            MsgBox(Main.Environment.ErrorMessage("BUG0000"), MsgBoxStyle.Exclamation)
            MsgBox(ex.Message & ex.StackTrace, MsgBoxStyle.Exclamation)
        End Try
        Me.Close()
    End Sub

    ''' <summary>
    ''' OKボタンが押下されたときの処理(実際には処理は行われない)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
    End Sub

    ''' <summary>
    ''' キャンセルボタンが押下されたときの処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>キャンセルフラグを立てるだけで、実際の画面クローズ処理は？？？？で行う</remarks>
    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Canceled = True
    End Sub

#End Region

#Region "検索エンジン"

    ''' <summary>
    ''' 検索
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Search()

        ' 読み込むファイルを決定する
        Dim dr1() As DataRow = GetSearchFileList()

        ' 検索結果格納テーブル
        List = New ResultListData

        ' 辞書ファイルを順番に読み取る
        Dim count As Integer = dr1.Length
        Dim i As Integer = 0
        For Each dr2 As DataRow In dr1

            ' 辞書ファイル解析
            ParseDicFile(dr2)

            ' バー進捗
            HSearchProgressBar.Value = CInt(HSearchProgressBar.Maximum / count * (i + 1))
            Application.DoEvents()
            i += 1

            ' キャンセルボタンが押されたかどうかを判断し、押されたときは処理中断
            If Canceled Then Throw New CancelException()

        Next

    End Sub

    ''' <summary>
    ''' オプションで指定された利用する辞書にあわせて検索するファイル一覧を取得する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSearchFileList() As DataRow()

        Dim list As New ArrayList
        For Each dr As DataRow In Main.Environment.DicFileTable.Rows

            ' 全辞書が指定されている場合
            If Condition.SearchDictonary.Length = 0 Then
                list.Add(dr)
                Continue For
            End If

            ' 辞書種類を取得
            Dim wlf_file_id As Integer = CInt(dr.Item("wlf_file_id"))

            ' 検索する辞書ファイルを格納
            For Each selected As Integer In Condition.SearchDictonary
                If selected = wlf_file_id Then list.Add(dr)
            Next
        Next

        Return CType(list.ToArray(GetType(DataRow)), DataRow())

    End Function

    ''' <summary>
    ''' 辞書ファイルを読み取る
    ''' </summary>
    ''' <param name="dr1">読み取る辞書ファイルの情報</param>
    ''' <remarks>引数はdic_filesテーブルの1行</remarks>
    Private Sub ParseDicFile(ByVal dr1 As DataRow)

        ' 各種情報を取得
        Dim dic_id As Integer = CInt(dr1.Item("id"))
        Dim wlf_file_id As Integer = CInt(dr1.Item("wlf_file_id"))
        Dim dr3 As DataRow = Main.Environment.WlfFileTable.Select(String.Format("{0}='{1}'", "id", wlf_file_id))(0)
        Dim group_id As Integer = CInt(dr3.Item("id"))
        Dim typename As String = Main.Environment.GetDicTypeFromGroupId(group_id)
        Dim filename As String = dr1.Item("filename").ToString
        Dim fullpath As String = Main.Environment.GetDicFilenameFromId(dic_id)
        Dim largeType As String = dr3.Item("name").ToString
        Dim smallType As String = dr1.Item("kind").ToString

        ' 辞書ファイルを開く
        Dim reader As New DicReader(fullpath, Condition)
        reader.Read()
        Dim cutter As New WdicCutter(reader.AllText)
        Dim last_pos As Integer = -1
        Dim pos As Integer
        pos = reader.Match()
        While pos >= 0

            ' 次の単語を検索する。同じ単語を引っ掛けた場合は次の単語を検索する
            If last_pos = pos Then
                pos = reader.NextMatch
                Continue While
            End If
            last_pos = pos

            ' 解析
            Dim text As String = cutter.Cut(pos)
            Dim parser As New WdicParser(text)
            parser.Parse()

            ' 本文検索の場合は正当性をチェックする
            ' (未実装)
            If Main.SeaTextSearch.Checked Then
                ' もし正当でない場合はContinue While
            End If

            ' 列作成およびカテゴリ/ジャンルのチェックと格納
            Dim dr2 As DataRow = List.ResultList.NewRow
            If Not CategoryCheckAndSet(parser, dr2) Then Continue While

            ' 辞書ファイル名
            dr2.Item(ResultListData.Col_Filename) = filename

            ' 辞書ファイル名(フルパス)
            dr2.Item(ResultListData.Col_FullPath) = fullpath

            ' 行
            dr2.Item(ResultListData.Col_LineNumber) = StringFunction.CountString(reader.AllText, vbCrLf, pos) + 1

            ' 位置
            dr2.Item(ResultListData.Col_Position) = pos

            ' 辞書略称
            dr2.Item(ResultListData.Col_TypeName) = typename

            ' 辞書大項目
            dr2.Item(ResultListData.Col_TypeLarge) = largeType

            ' 辞書小項目
            dr2.Item(ResultListData.Col_TypeSmall) = smallType

            ' 単語名
            dr2.Item(ResultListData.Col_Word) = parser.ParsedData.Word

            ' 概要
            Dim body_item As WordItem.WordItemBase = CType(parser.ParsedData.BodyItems(0), WordItem.WordItemBase)
            Dim desc As String = body_item.Data
            If String.IsNullOrEmpty(desc) Then
                desc = ""
            Else
                If TypeOf body_item Is WordItem.Trancefer Then
                    desc = "→ " & desc
                End If
            End If
            desc = StringFunction.DecodeBodyText(desc)
            dr2.Item(ResultListData.Col_Description) = StringFunction.MakeShortDescription(desc)

            ' 品詞
            Dim speech As String = ""
            For Each item As WordItem.WordItemBase In parser.ParsedData.HeaderItems
                If Not TypeOf item Is WordItem.Pos Then Continue For
                speech = String.Join(",", CType(item, WordItem.Pos).Items)
            Next
            dr2.Item(ResultListData.Col_Pos) = speech

            ' 読み方
            Dim yomi As String = ""
            For Each item As WordItem.WordItemBase In parser.ParsedData.HeaderItems
                If TypeOf item Is WordItem.Yomi Then
                    yomi &= item.Data & ","
                ElseIf TypeOf item Is WordItem.OldYomi Then
                    yomi &= OLD_WORD & item.Data
                End If
            Next
            dr2.Item(ResultListData.Col_Yomi) = yomi.TrimEnd(","c)

            ' 英語綴り
            Dim spell As String = ""
            Dim abbr As String = ""
            For Each item As WordItem.WordItemBase In parser.ParsedData.HeaderItems
                If Not TypeOf item Is WordItem.Spell Then Continue For
                Dim lang As String
                Dim s As WordItem.Spell = CType(item, WordItem.Spell)
                If Not String.IsNullOrEmpty(s.LangCode2) Then
                    lang = Main.Environment.GetLangNameFromCode(s.LangCode2)
                ElseIf Not String.IsNullOrEmpty(s.LangCode3) Then
                    lang = Main.Environment.GetLangNameFromCode(s.LangCode3)
                Else
                    ' エラー
                    Throw New UnjustProcessingException
                End If
                spell = lang & ":" & s.Spell & ","
                If Not String.IsNullOrEmpty(s.Abbr) Then
                    abbr &= lang & ":" & s.Abbr & ","
                End If
            Next
            dr2.Item(ResultListData.Col_Spell) = spell.TrimEnd(","c)
            dr2.Item(ResultListData.Col_Abbr) = abbr.TrimEnd(","c)

            ' 発音・略語
            Dim pron As String = ""
            For Each item As WordItem.WordItemBase In parser.ParsedData.HeaderItems
                If Not TypeOf item Is WordItem.Pron Then Continue For
                Dim lang As String
                Dim p As WordItem.Pron = CType(item, WordItem.Pron)
                If Not String.IsNullOrEmpty(p.LangCode2) Then
                    lang = Main.Environment.GetLangNameFromCode(p.LangCode2)
                ElseIf Not String.IsNullOrEmpty(p.LangCode3) Then
                    lang = Main.Environment.GetLangNameFromCode(p.LangCode3)
                Else
                    ' エラー
                    Throw New UnjustProcessingException
                End If
                pron &= lang & ":" & p.Spell & ","
            Next
            dr2.Item(ResultListData.Col_Pron) = pron.TrimEnd(","c)

            ' 格納
            List.ResultList.Rows.Add(dr2)

            ' 検索結果超過時は例外
            If List.ResultList.Rows.Count >= My.Settings.MaxSearch Then Throw New RersultOverflowException()

            ' 次を検索する
            pos = reader.NextMatch

        End While

    End Sub

    ''' <summary>
    ''' WdicParserクラスからSearchResultDataRowにデータ転送
    ''' </summary>
    ''' <param name="parser">WdicParserによる単語解析結果</param>
    ''' <param name="dr">データを格納するResultDataの行</param>
    ''' <remarks>前もってWdicparserを実行しておくこと、ResultDataの行には必要なデータを入れておくこと</remarks>
    Public Function CategoryCheckAndSet(ByRef parser As WdicParser, ByRef dr As DataRow) As Boolean


        ' カテゴリ/ジャンルを取得 兼 検索カテゴリ一致確認
        Dim categotyHit As Boolean = False
        Dim dirNames As ArrayList = New ArrayList
        Dim dirLarges As ArrayList = New ArrayList
        Dim dirSmalls As ArrayList = New ArrayList
        For Each item As WordItem.WordItemBase In parser.ParsedData.HeaderItems

            If Not TypeOf item Is WordItem.Dir Then Continue For

            Dim dir As WordItem.Dir = CType(item, WordItem.Dir)

            ' カテゴリとジャンルを分離
            Dim dir_first As String
            Dim pos As Integer = dir.Data.IndexOf("/", 1)
            If pos < 0 Then
                dir_first = dir.Data
            Else
                dir_first = dir.Data.Substring(0, pos)
            End If

            ' カテゴリ略称を格納
            If Not dirNames.Contains(dir_first) Then dirNames.Add(dir_first.TrimStart("/"c))

            ' カテゴリを格納
            Dim dr1 As DataRow() = Main.Environment.CategoryTable.Select(String.Format("{0}='{1}'", "directory", dir_first))
            If dr1.Length = 0 Then
                dirLarges.Add(UNDEFINED)
            ElseIf dr1.Length = 1 Then
                dirLarges.Add(dr1(0).Item("name").ToString)
            Else
                Throw New UnjustProcessingException()
            End If

            ' ジャンルを格納
            Dim dr2 As DataRow() = Main.Environment.CategoryTable.Select(String.Format("{0}='{1}'", "directory", dir.Data))
            If dr2.Length = 0 Then
                dirSmalls.Add(UNDEFINED)
            ElseIf dr2.Length = 1 Then
                dirSmalls.Add(dr2(0).Item("name").ToString)
            Else
                Throw New UnjustProcessingException()
            End If

            ' 検索条件で指定されたカテゴリに該当するかを確認する
            For Each selected As String In Condition.SearchCategory
                If dir_first.TrimStart("/"c) = selected Then categotyHit = True : Exit For
            Next

        Next

        ' 大分類の重複消しこみ
        For i As Integer = dirLarges.Count - 1 To 1 Step -1
            If dirLarges(i - 1).ToString = dirLarges(i).ToString Then dirLarges.RemoveAt(i)
        Next

        ' 格納
        dr.Item(ResultListData.Col_DirName) = String.Join("　", CType(dirNames.ToArray(GetType(String)), String()))
        dr.Item(ResultListData.Col_DirLarge) = String.Join("　", CType(dirLarges.ToArray(GetType(String)), String()))
        dr.Item(ResultListData.Col_DirSmall) = String.Join("　", CType(dirSmalls.ToArray(GetType(String)), String()))

        ' 全カテゴリ検索の場合はTrueを返す
        If Condition.SearchCategory.Length = 0 Then Return True

        ' カテゴリが不一致だった場合はFalseを返す
        Return categotyHit

    End Function

#End Region


End Class


