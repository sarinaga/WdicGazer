Imports System.Text.RegularExpressions

''' <summary>
''' WdicCutterで切り出した文字列を構文解釈し、WordDataに格納する
''' </summary>
Public Class WdicParser

#Region "定義"

    ''' <summary>
    ''' 解析する文字列
    ''' </summary>
    Protected Text As String

    ''' <summary>
    ''' 解析結果
    ''' </summary>
    Protected Data As WordData

    ''' <summary>
    ''' エラー一覧
    ''' </summary>
    ''' <remarks></remarks>
    Protected ErrorList As New List(Of String)

    ''' <summary>
    ''' 例外用メッセージ(処理中断)
    ''' </summary>
    Private Const ExceptionBreakMessage As String = "構文解析にエラーがあり、処理を中断します。"

    ''' <summary>
    ''' 例外用メッセージ(処理続行)
    ''' </summary>
    Private Const ExceptionErrorMessage As String = "構文解析にエラーがあります。"


#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="text">解析する文字列</param>
    Public Sub New(ByVal text As String)
        Me.Text = text
        Data = New WordData
        ErrorList.Clear()
    End Sub

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 解析結果
    ''' </summary>
    Public ReadOnly Property ParsedData() As WordData
        Get
            Return Data
        End Get
    End Property

    ''' <summary>
    ''' 解析時のエラー一覧
    ''' </summary>
    ''' <returns>エラーの一覧</returns>
    Public ReadOnly Property GetErrorList() As String()
        Get
            Return ErrorList.ToArray()
        End Get
    End Property



#End Region

#Region "解析"

    ''' <summary>
    ''' 切り取った部分を構文解析する
    ''' </summary>
    Public Sub Parse()

        ' テキストが与えられていない場合はエラー
        If String.IsNullOrEmpty(Text) Then
            Throw New WordParseFaultException("解析する文字列が与えられていない")
        End If

        ' テキストファイルはStringReaderで読み込む
        Dim reader As New System.IO.StringReader(Text)

        ' ヘッダ部分を読み出す
        Dim line As String = ""
        ParseHeader(reader, line)

        ' 本文部分を読み出す
        ParseBody(reader, line)

        ' ファイルクローズ
        reader.Close()

        ' 有効期限を計算する(ヘッダ部分)
        Try
            ParseValidDataHeader()
        Catch ex As Exception
            ErrorList.Add(ex.Message)
            If False Then Throw New WordParseFaultException(ExceptionErrorMessage)
        End Try

        ' 有効期限を計算する(本文部分)
        Try
            ParseValidDataBody()
        Catch ex As Exception
            ErrorList.Add(ex.Message)
            If False Then Throw New WordParseFaultException(ExceptionErrorMessage)
        End Try

    End Sub

    ''' <summary>
    ''' 切り出した部分のヘッダ部分を解析する
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">本文部分の最初の１行を返す</param>
    ''' <remarks>lineはデータを受け取らない</remarks>
    Private Sub ParseHeader(ByVal reader As System.IO.StringReader, ByRef line As String)

        ' 単語名を読み出す（１行目）
        ' 正しくない場合は例外
        line = reader.ReadLine()
        If line Is Nothing Then
            ErrorList.Add("見出し語が正しく取得されなかった。")
            Throw New WordParseFaultException(ExceptionBreakMessage)
        End If
        Data.Word = Decode(line.Substring(1))

        ' 2行目からのヘッダ部分を読み込む
        ' 本文部分にたどり着く前に読み終わってしまったら例外
        ' 構文解析で例外が発生することがある
        Do
            line = reader.ReadLine()
            If line Is Nothing Then
                ErrorList.Add("構文解析の完了より先にデータが終了した。")
                If False Then Throw New WordParseFaultException(ExceptionErrorMessage)
            End If
            Dim item As WordItem.WordItemBase = ParseAttributeItem(line)
            If Not item Is Nothing Then
                If Not item.Level = 1 Then
                    ErrorList.Add("適切な項目レベルが与えられなかった。")
                    If False Then Throw New WordParseFaultException(ExceptionErrorMessage)
                End If
                Data.HeaderItems.Add(item)
            Else
                Exit Do
            End If
        Loop

    End Sub

    ''' <summary>
    ''' 切り出した部分のヘッダ部分を解析する
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">本文部分の最初の１行を受け取る</param>
    ''' <remarks>lineは参照化返ししているが意味はない(ParseHeaserと同じ構造にしたため)</remarks>
    Private Sub Parsebody(ByVal reader As System.IO.StringReader, ByRef line As String)

        ' 本文部分を読み込む
        Dim ch_temp As New ArrayList
        While (Not line Is Nothing)

            ' 属性読み取り
            Dim item As WordItem.WordItemBase = ParseAttributeItem(line)
            If Not item Is Nothing Then
                If ch_temp.Count > 1 Then
                    Dim i As Integer = ch_temp.Count - 1
                    If Not CType(ch_temp(i), WordItem.WordItemBase).Level = item.Level Then
                        ErrorList.Add("適切な項目レベルが与えられなかった。")
                        If False Then Throw New WordParseFaultException(ExceptionErrorMessage)
                    End If
                End If
                ch_temp.Add(item)
                line = reader.ReadLine
                Continue While
            End If

            ' 属性格納
            If ch_temp.Count > 0 Then
                Dim ch As New WordItem.ChapterHeader
                ch.Data = Nothing
                ch.Text = Nothing
                ch.Level = CType(ch_temp(0), WordItem.WordItemBase).Level
                ch.Items = ch_temp
                Data.BodyItems.Add(ch)
                ch_temp.Clear()
            End If

            ' 本文読み取り・格納
            item = ParseBodyItem(reader, line)
            Data.BodyItems.Add(item)

        End While

        ' 属性が残ってしまっている場合はエラー
        If ch_temp.Count > 0 Then
            ErrorList.Add("不必要な属性が残ってしまっている。")
            If False Then Throw New WordParseFaultException(ExceptionErrorMessage)
        End If

    End Sub

#Region "属性の構文解析"

    ''' <summary>
    ''' 属性の部分構文解析を行う
    ''' </summary>
    ''' <param name="line">読み取る行</param>
    ''' <returns>読み取った属性の情報</returns>
    ''' <remarks>属性の情報でなかった場合、構文解析に失敗したときはNothingが返る</remarks>
    Private Function ParseAttributeItem(ByVal line As String) As WordItem.WordItemBase

        ' ヘッダ部分の場合と本文部分とで利用できる属性が違うためそのチェック
        Dim level As Integer = GetLevel(line)
        Dim isHeader As Boolean = (level = 1)

        ' 属性名を取得
        Dim prefix As String = ChopAttributeName(line)

        ' ヘッダ-本文の属性対応チェック
        Select Case prefix

            Case "yomi", "qyomi", "spell", "pron", "pos", "dir"
                ' ヘッダ部分のみに出現する属性のチェック、本文中に出てきたら例外
                If Not isHeader Then
                    ErrorList.Add("本文部分で" & prefix & "が利用された。")
                    If False Then Throw New WordParseFaultException(ExceptionErrorMessage)
                End If

            Case "flag", "author", "valid", "expire"
                ' ヘッダ、本文双方に出現する属性
                ' そのまま処理

            Case "#$%&"
                ' 本文部分にのみに出現する属性
                ' そのようなものはない(上記はダミー)

            Case Else
                ' 本文の要素の場合は処理を行わない
                ' それ以外の場合は未知の要素であると判断する
                If Not String.IsNullOrEmpty(ChopBodyPrefix(line)) Then Return Nothing

        End Select

        ' ヘッダのデータ部を取得
        Dim data As String = Decode(line.TrimStart.Substring(prefix.Length + 1))
        data = Transrate.DecodeEntityReference(data)
        data = Transrate.DecodeNumberReference(data)
        data = Transrate.DecodeEscapeLetter(data)

        ' 属性のWordItemを作成する
        Dim exception_message As String = ""
        Dim wi As WordItem.WordItemBase = Nothing
        Try

            Select Case prefix

                Case "yomi"
                    ' 日本語読み
                    wi = New WordItem.Yomi
                    wi.Data = data

                Case "qyomi"
                    ' 歴史的読み
                    wi = New WordItem.OldYomi
                    wi.Data = data

                Case "spell"
                    ' 外語綴り
                    wi = ParseSpellData(data)

                Case "pron"
                    ' 発音
                    wi = ParsePronData(data)

                Case "pos"
                    ' 品詞
                    wi = ParsePosData(data)

                Case "dir"
                    ' ディレクトリ
                    wi = New WordItem.Dir
                    wi.Data = data

                Case "flag"
                    ' 特殊フラグ
                    wi = ParseFlagData(data)

                Case "author"
                    ' 筆者、更新履歴
                    wi = ParseAuthorData(data)

                Case "valid"
                    ' 有効期限
                    ' (注：ヘッダをすべて読み終わってから正確な日付を計算しなくてはならない)
                    wi = New WordItem.Valid
                    wi.Data = data

                Case "expire"
                    ' 最終日付
                    wi = New WordItem.Expire
                    If Not DateTime.TryParse(data, CType(wi, WordItem.Expire).ExpireDate) Then
                        ErrorList.Add("Expireに記述された日付が不正。")
                        If False Then Throw New WordParseFaultException(ExceptionErrorMessage)
                    End If

                Case Else
                    ' 未定義処理
                    wi = New WordItem.UndefinedItem
                    wi.Data = data

            End Select

        Catch ex As Exception
            exception_message = ex.Message
        End Try

        ' 解析に失敗した場合は例外
        If Not String.IsNullOrEmpty(exception_message) Then
            ErrorList.Add(exception_message)
            If False Then Throw New WordParseFaultException(ExceptionErrorMessage)

        ElseIf wi Is Nothing Then
            ErrorList.Add("処理において正しい値を返せなかった。")
            If False Then Throw New WordParseFaultException(ExceptionErrorMessage)

        End If

        ' 解析前文字列、レベルを設定してから返却
        wi.Text = line
        wi.Level = level
        Return wi

    End Function

    ''' <summary>
    ''' 行の頭を切り取って属性名を返す
    ''' </summary>
    ''' <param name="text">解析する文字列</param>
    ''' <returns>切り取られた属性名</returns>
    ''' <remarks>属性名が切り出せなかった場合は空文字列を返す</remarks>
    Private Function ChopAttributeName(ByVal text As String) As String

        ' : があればとりあえず属性と推定
        text = text.TrimStart
        Dim pos As Integer = text.IndexOf(":"c)
        If pos < 0 Then Return ""

        ' 属性名を取得して返す ( : だけの場合は定義付きリストの見出し)
        Dim prefix As String = text.Substring(0, pos).Trim()
        If prefix.Length = 0 Then Return ""
        Return prefix.Trim

    End Function

    ''' <summary>
    ''' 属性(綴り)を解析
    ''' </summary>
    ''' <param name="data">解析する文字列(データ部)</param>
    ''' <returns>解析して得られた綴りの情報</returns>
    ''' <remarks>不正データは例外を送出(Nothingは返らない)</remarks>
    Private Function ParseSpellData(ByVal data As String) As WordItem.Spell

        ' 格納クラス
        Dim s As New WordItem.Spell

        ' 言語コード・国コードを読み取る
        Dim pos_coron As Integer = data.IndexOf(":"c)
        Dim lang_code As String = data.Substring(0, pos_coron)

        Dim pos_hyphen As Integer = lang_code.IndexOf("-"c)
        If pos_hyphen < 0 Then
            If pos_coron = 2 Then
                s.LangCode2 = lang_code
            ElseIf pos_coron = 3 Then
                s.LangCode3 = lang_code
            End If
        Else
            If pos_hyphen = 2 Then
                s.LangCode2 = lang_code.Substring(0, pos_hyphen)
            ElseIf pos_hyphen = 3 Then
                s.LangCode3 = lang_code.Substring(0, pos_hyphen)
            End If
            s.CountryCode = lang_code.Substring(pos_hyphen + 1)
        End If

        ' 言語コード:綴りの時の処理
        Dim spell As String = data.Substring(pos_coron + 1)
        pos_coron = spell.IndexOf(":"c)
        If pos_coron < 0 Then
            s.Spell = spell
            s.Abbr = Nothing
            Return s
        End If

        ' 言語コード:略語:綴りの時の処理
        ' (空白文字がない場合は言語コード:綴り)
        Dim temp1 As String = spell.Substring(0, pos_coron)
        Dim temp2 As String = spell.Substring(pos_coron + 1)
        If temp2(0) = " "c Then
            s.Spell = temp2.Substring(1)
            s.Abbr = temp1
        Else
            s.Spell = spell
            s.Abbr = Nothing
        End If
        Return s

    End Function

    ''' <summary>
    ''' 属性(発音)を解析
    ''' </summary>
    ''' <param name="data">解析する文字列(データ部)</param>
    ''' <returns>解析して得られた発音の情報</returns>
    ''' <remarks>不正データは例外を送出(Nothingは返らない)</remarks>
    Private Function ParsePronData(ByVal data As String) As WordItem.Pron
        Dim p As New WordItem.Pron
        Dim pos_coron As Integer = data.IndexOf(":"c)
        Dim lang_code As String = data.Substring(0, pos_coron)

        Dim pos_hyphen As Integer = lang_code.IndexOf("-"c)
        If pos_hyphen < 0 Then
            If pos_coron = 2 Then
                p.LangCode2 = lang_code
            ElseIf pos_coron = 3 Then
                p.LangCode3 = lang_code
            End If
        Else
            If pos_hyphen = 2 Then
                p.LangCode2 = lang_code.Substring(0, pos_hyphen)
            ElseIf pos_hyphen = 3 Then
                p.LangCode3 = lang_code.Substring(0, pos_hyphen)
            End If
            p.CountryCode = lang_code.Substring(pos_hyphen + 1)
        End If

        p.Spell = data.Substring(pos_coron + 1)
        Return p
    End Function

    ''' <summary>
    ''' 属性(品詞)を解析
    ''' </summary>
    ''' <param name="data">解析する文字列(データ部)</param>
    ''' <returns>解析して得られた品詞の情報</returns>
    ''' <remarks>不正データは例外を送出(Nothingは返らない)</remarks>
    Private Function ParsePosData(ByVal data As String) As WordItem.Pos
        Dim p As New WordItem.Pos
        p.Items = data.Split(","c)
        For i As Integer = 0 To p.Items.Length - 1
            p.Items(i).Trim()
        Next
        Return p
    End Function

    ''' <summary>
    ''' 属性(フラグ)を解析
    ''' </summary>
    ''' <param name="data">解析する文字列(データ部)</param>
    ''' <returns>解析して得られたフラグの情報</returns>
    ''' <remarks>不正データは例外を送出(Nothingは返らない)</remarks>
    Private Function ParseFlagData(ByVal data As String) As WordItem.Flag
        Dim f As New WordItem.Flag
        f.Items = data.Split(","c)
        For i As Integer = 0 To f.Items.Length - 1
            f.Items(i).Trim()
        Next
        Return f
    End Function

    ''' <summary>
    ''' 属性(筆者、更新履歴)を解析
    ''' </summary>
    ''' <param name="data">解析する文字列(データ部)</param>
    ''' <returns>解析して得られた筆者、更新履歴の情報</returns>
    ''' <remarks>不正データは例外を送出(Nothingは返らない)</remarks>
    Private Function ParseAuthorData(ByVal data As String) As WordItem.Author
        Dim d() As String = data.Split(","c)
        If d.Length < 3 Or d.Length > 4 Then
            Throw New WordParseFaultException("筆者情報の書式が正しくない")
        End If
        For i As Integer = 0 To d.Length - 1
            d(i).Trim()
        Next
        Dim a As New WordItem.Author
        a.Type = d(0)(0)
        If Not Date.TryParse(d(1), a.WrittenDate) Then
            Throw New WordParseFaultException("筆者情報の日付の書式が正しくない")
        End If
        a.AuthorName = d(2)
        If d.Length = 4 Then
            a.Source = d(3)
        Else
            a.Source = Nothing
        End If
        Return a
    End Function

    ''' <summary>
    ''' 属性(原稿内容の有効期限)を解析[ヘッダ部分]
    ''' </summary>
    ''' <remarks>ヘッダ部分すべてを読み込んでから呼び出すこと</remarks>
    Private Sub ParseValidDataHeader()

        ' 基準となる有効期限と更新履歴を取得する
        Dim valid As WordItem.Valid = Nothing
        Dim author As WordItem.Author = Nothing
        For Each item As WordItem.WordItemBase In Data.HeaderItems
            If TypeOf item Is WordItem.Author Then
                If Not author Is Nothing Then
                    If author.WrittenDate > CType(item, WordItem.Author).WrittenDate Then
                        Throw New WordParseFaultException("authorの日付が古い順に並んでいない")
                    End If
                End If
                author = CType(item, WordItem.Author)
            End If
            If TypeOf item Is WordItem.Valid Then
                If Not valid Is Nothing Then
                    Throw New WordParseFaultException("validが2つある")
                Else
                    valid = CType(item, WordItem.Valid)
                End If
            End If
        Next

        ' validがない場合はそのまま終了
        ' validがあるにもかかわらずauthorがないものはエラー
        If valid Is Nothing Then Exit Sub
        If author Is Nothing Then GoTo err_notfoundauthor

        ' validを日付に変換
        valid.ValidDate = ParseValidDataTrans(valid.Data, author.WrittenDate)
no_err:
        Exit Sub

err_notfoundauthor:
        Throw New WordParseFaultException("更新履歴がないのにValidがある")

    End Sub

    ''' <summary>
    ''' 有効期限の構文を解析する
    ''' </summary>
    ''' <param name="data">有効期限文字列</param>
    ''' <param name="lastModified">最終更新日</param>
    ''' <returns>計算された有効期限</returns>
    Private Function ParseValidDataTrans(ByVal data As String, ByVal lastModified As Date) As Date

        ' 通常の日付変換
        Dim valid As Date
        If Date.TryParse(data, valid) Then
            Return valid
        End If

        ' 構文解析
        Dim r As Regex = New Regex("(\d+)\s(day|week|month|year)")
        Dim m As Match = r.Match(data)
        If Not m.Success Then
            Throw New WordParseFaultException("有効期限の書式が正しくない")
        End If

        ' 日付計算
        Dim f As Integer = CInt(m.Groups(1).Value)
        Dim t As String = m.Groups(2).Value
        Select Case t
            Case "day"
                valid = lastModified.AddDays(f)
            Case "week"
                valid = lastModified.AddDays(f * 7)
            Case "month"
                valid = lastModified.AddMonths(f)
            Case "year"
                valid = lastModified.AddYears(f)
            Case Else
                Throw New UnjustProcessingException
        End Select
        Return valid

    End Function

#End Region

#Region "本文部分の構文解析"

    ''' <summary>
    ''' 本文の部分構造解析を行う
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">先に読み込まれている行を受け取り、次の１行を返す</param>
    ''' <returns>解析した要素１つ</returns>
    ''' <remarks>
    ''' 最後まで読んだとき、lineはnothingを返す
    ''' 再起呼び出しあり
    ''' </remarks>
    Private Function ParseBodyItem(ByVal reader As System.IO.StringReader, ByRef line As String) As WordItem.WordItemBase

        Dim item As WordItem.WordItemBase

        ' 深さを探知
        Dim deep As Integer = GetLevel(line)
        If deep < 0 Then
            Throw New WordParseFaultException("タブが不正で正しく読み取れない")
        End If

        ' 頭の部分を取得
        Dim prefix As String = ChopBodyPrefix(line)
        If String.IsNullOrEmpty(prefix) Then
            Throw New WordParseFaultException("書式が不正で正しく読み取れない")
        End If

        ' データ部分を取得
        ' (一部のサブメソッドは次の行を読んでlineの値を書き換える)
        Dim data As String = line.TrimStart.Substring(prefix.Length + 1)
        Select Case prefix.Trim()

            Case "*"
                ' 通常段落
                item = New WordItem.Paragraph()
                item.Data = data
                item.Text = line
                line = reader.ReadLine

            Case "="
                ' 見出し
                item = New WordItem.Heading()
                item.Data = data
                item.Text = line
                line = reader.ReadLine

            Case "=>"
                ' 転送
                item = New WordItem.Trancefer()
                item.Data = data.Trim.TrimStart("["c).TrimEnd("]"c)
                item.Text = line
                line = reader.ReadLine

            Case "//"
                ' リンク集
                item = New WordItem.Linker()
                item.Data = Nothing
                item.Text = line
                line = reader.ReadLine

            Case "-", "-!", "+"
                ' 箇条書き
                item = ReadListItem(reader, line)

            Case "))"
                ' 整形済テキスト
                item = ReadPreItem(reader, line)

            Case ">>"
                ' 引用
                item = ReadQuoteItem(reader, line)

            Case "%%"
                ' コメント
                item = New WordItem.Comment()
                item.Data = data
                item.Text = line
                line = reader.ReadLine

            Case "::"
                ' 定義リスト簡略表記
                item = ReadDefListLittle(reader, line)

            Case ":", ":>", ":<"
                ' 定義リスト正式表記
                item = ReadDefList(reader, line)

            Case "||", "|="
                ' テーブル簡易表記
                item = ReadTableItem(reader, line)

            Case "|"
                ' テーブル正式表記
                item = ReadTableItemFull(reader, line)

            Case Else
                ' 未確定物
                item = New WordItem.WordItemBase()
                item.Data = Nothing
                item.Text = line
                line = reader.ReadLine

        End Select

        ' レベルを設定してから返却
        item.Level = deep
        Return item

    End Function

    ''' <summary>
    ''' 行の頭を切り取って本文要素記号を返す
    ''' </summary>
    ''' <param name="text">解析する文字列</param>
    ''' <returns>切り取られた記号</returns>
    ''' <remarks>本文部分の要素でなかったときは空文字列を返す</remarks>
    Private Function ChopBodyPrefix(ByVal text As String) As String

        ' 記号部分(頭１文字か２文字)を取得する
        Dim prefix As String = text.Trim()
        If prefix.Length < 3 Then GoTo err_common
        prefix = prefix.Substring(0, 3)
        If prefix(2) = " " Then
            prefix = prefix.TrimEnd()
        ElseIf prefix(1) = " " Then
            prefix = prefix.Substring(0, 1)
        Else
            prefix = prefix.Substring(0, 2)
        End If

        ' 正規の記号かどうかをチェックする
        Select Case prefix
            Case "=", "*", "-", "-!", "+", ":", "::", ":>", ":<", "||", "|=", "))", ">>", "%%", "=>", "//"
                ' 正規の記号
                Return prefix

            Case Else
                ' 正規の記号でない
                Return ""

        End Select
        Return ""

err_common:
        Throw New WordParseFaultException("行頭記号が取得できないか正しくない")

    End Function

    ''' <summary>
    ''' 属性(原稿内容の有効期限)を解析[本文部分]
    ''' </summary>
    ''' <remarks>すべてを読み込んでから呼び出すこと</remarks>
    Private Sub ParseValidDataBody()

        ' ヘッダに書かれた最新更新日付を取得
        Dim header_author As WordItem.Author = Nothing
        For Each item As WordItem.WordItemBase In Data.HeaderItems
            If Not TypeOf item Is WordItem.Author Then Continue For
            header_author = CType(item, WordItem.Author)
        Next

        ' 本文部分に散らばっているChapterHeaderクラスを読み取っていく
        For Each item1 As WordItem.WordItemBase In Data.BodyItems
            If Not TypeOf item1 Is WordItem.ChapterHeader Then Continue For

            ' 基準となる有効期限と更新履歴を取得する
            Dim valid As WordItem.Valid = Nothing
            Dim author As WordItem.Author = Nothing
            For Each item2 As WordItem.WordItemBase In CType(item1, WordItem.ChapterHeader).Items
                If TypeOf item2 Is WordItem.Author Then
                    If Not author Is Nothing Then
                        If author.WrittenDate > CType(item2, WordItem.Author).WrittenDate Then
                            Throw New WordParseFaultException("authorの日付が古い順に並んでいない")
                        End If
                    End If
                    author = CType(item2, WordItem.Author)
                End If
                If TypeOf item2 Is WordItem.Valid Then
                    If Not valid Is Nothing Then
                        Throw New WordParseFaultException("validが2つある")
                    Else
                        valid = CType(item2, WordItem.Valid)
                    End If
                End If
            Next

            ' validがない場合は次に進む
            ' validがある場合は日付を計算する
            ' validがあるにもかかわらずauthorがないものは
            ' ヘッダ部のauthorのもっとも新しいものを利用
            ' それもない場合はエラー
            Dim lastModified As Date
            If valid Is Nothing Then Continue For
            If author Is Nothing Then
                If header_author Is Nothing Then
                    Throw New WordParseFaultException("更新履歴がないのにvalidがある")
                End If
                lastModified = header_author.WrittenDate
            Else
                lastModified = author.WrittenDate
            End If
            valid.ValidDate = ParseValidDataTrans(valid.Data, lastModified)

        Next

    End Sub


#Region "箇条書き"

    ''' <summary>
    ''' リストアイテムを読み込む
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">最初の１行を受け取り、リストアイテムの次の１行を返す</param>
    ''' <returns>箇条書き基底クラス</returns>
    ''' <remarks>lineはnothingを返す、</remarks>
    Private Function ReadListItem(ByRef reader As System.IO.StringReader, ByRef line As String) As WordItem.ListBase
        Dim text As String = ""
        Dim prefix As String = ChopBodyPrefix(line)
        Dim item As WordItem.ListBase
        If prefix = "-" Or prefix = "-!" Then
            item = New WordItem.UnorderedList
        ElseIf prefix = "+" Then
            item = New WordItem.OrderedList
        Else
            Throw New UnjustProcessingException("箇条書きのマーカーが正しくない")
        End If
        item.Level = GetLevel(line)
        item.Items = ReadListItemDeep(reader, line, text)
        item.Data = Nothing
        item.Text = text
        Return item
    End Function

    ''' <summary>
    ''' リストアイテムの詳細な読み込み
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">最初の１行を受け取り、リストアイテムの次の１行を返す</param>
    ''' <param name="text">解析した文字列</param>
    ''' <returns>箇条書き内容クラス</returns>
    ''' <remarks></remarks>
    Private Function ReadListItemDeep(ByRef reader As System.IO.StringReader, ByRef line As String, ByRef text As String) As WordItem.ListItem()

        Dim items As ArrayList = New ArrayList()
        Dim deep As Integer
        Dim last_deep As Integer = GetLevel(line)
        Dim prefix As String
        Dim last_prefix As String = ""

        Do

            ' データ保存
            text &= line & vbCrLf

            ' 再起の終了かどうかを確認する(リストの終わりの検知)
            prefix = ChopBodyPrefix(line)
            If (Not prefix = "-" And Not prefix = "-!" And Not prefix = "+") Then
                Exit Do
            End If

            ' 再起の終了かどうかを確認する(レベルの浅化の検知)
            deep = GetLevel(line)
            If last_deep > deep Then
                Exit Do
            End If

            ' 再起の終了かどうかを確認する(レベルの深化の検知)
            If last_deep < deep Then
                CType(items(items.Count - 1), WordItem.ListItem).ChildItem = ReadListItem(reader, line)
                Continue Do
            End If

            ' 再起の終了かどうかを確認する(同レベルのリストの切り替えの検知)
            If _
             (last_prefix = "+" And (prefix = "-" Or prefix = "-!")) Or _
             (last_prefix = "-" Or last_prefix = "-!") And prefix = "+" _
            Then
                Exit Do
            End If

            ' アイテム格納
            Dim item As New WordItem.ListItem
            Dim data As String = line.TrimStart.Substring(prefix.Length + 1)
            item.ChildItem = Nothing
            item.Text = data
            If prefix = "-!" Then item.Type = "対義語"
            items.Add(item)
            last_deep = deep

            ' 次の行の読み込み
            line = reader.ReadLine()
            If line Is Nothing Then Exit Do

        Loop
        Return CType(items.ToArray(GetType(WordItem.ListItem)), WordItem.ListItem())


    End Function

#End Region

#Region "整形済・引用"

    ''' <summary>
    ''' 整形済テキストを読み込む
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">最初の１行を受け取り、整形済テキストの次の１行を返す</param>
    ''' <returns>整形済テキストクラス</returns>
    ''' <remarks></remarks>
    Private Function ReadPreItem(ByRef reader As System.IO.StringReader, ByRef line As String) As WordItem.Pre
        Return CType(ReadPreQuoteCommon(reader, line, "))"), WordItem.Pre)
    End Function

    ''' <summary>
    ''' 引用テキストを読み込む
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">最初の１行を受け取り、引用テキストの次の１行を返す</param>
    ''' <returns>引用テキストクラス</returns>
    ''' <remarks></remarks>
    Private Function ReadQuoteItem(ByRef reader As System.IO.StringReader, ByRef line As String) As WordItem.Quote
        Return CType(ReadPreQuoteCommon(reader, line, ">>"), WordItem.Quote)
    End Function

    ''' <summary>
    ''' 整形済/引用 共通処理
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">最初の１行を受け取り、引用テキストの次の１行を返す</param>
    ''' <param name="usePrefix">利用するプレフィックス</param>
    ''' <returns>整形済/引用テキストクラス</returns>
    ''' <remarks></remarks>
    Private Function ReadPreQuoteCommon(ByRef reader As System.IO.StringReader, ByRef line As String, ByVal usePrefix As String) As WordItem.WordItemBase
        Dim wi As WordItem.WordItemBase
        If usePrefix = "))" Then
            wi = New WordItem.Pre()
        ElseIf usePrefix = ">>" Then
            wi = New WordItem.Quote()
        Else
            Throw New UnjustProcessingException
        End If
        wi.Data = Nothing
        wi.Text = String.Join(vbCrLf, ReadTextCommon(reader, line, usePrefix))
        Return wi
    End Function

    ''' <summary>
    ''' 連続した項目のデータ部分を取得する
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">最初の１行を受け取り、引用テキストの次の１行を返す</param>
    ''' <param name="usePrefix">利用するプレフィックス</param>
    ''' <returns>データ部分の配列</returns>
    ''' <remarks></remarks>
    Private Function ReadTextCommon(ByRef reader As System.IO.StringReader, ByRef line As String, ByVal usePrefix As String) As String()
        Dim w As ArrayList = New ArrayList
        Do
            Dim trimed As String = line.TrimStart()
            Dim prefix As String = trimed.Substring(0, usePrefix.Length)
            If Not prefix = usePrefix Then Exit Do
            If trimed.Length > 2 Then
                w.Add(trimed.Substring(usePrefix.Length + 1))
            Else
                w.Add("")
            End If
            line = reader.ReadLine()
            If line Is Nothing Then Exit Do
        Loop
        Return CType(w.ToArray(GetType(String)), String())
    End Function

#End Region

#Region "見出しつき箇条書き"

    ''' <summary>
    ''' 定義リストを読み込む(簡易書式)
    ''' </summary>
    ''' <returns>解析した定義済リストのデータ</returns>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">最初の１行を受け取り、引用テキストの次の１行を返す</param>
    ''' <remarks></remarks>
    Private Function ReadDefListLittle(ByRef reader As System.IO.StringReader, ByRef line As String) As WordItem.DefList

        ' 簡易表記部分をすべて読み込む
        Dim lines() As String = ReadTextCommon(reader, line, "::")

        ' 読み取った行数から必要とするリストの内容の数を計算して用意(1行で2item)
        Dim items((lines.Length - 1) * 2 + 1) As WordItem.DefItem

        ' １行ずつ読み込む(1行で2itemなので注意)
        For i As Integer = 0 To lines.Length - 1

            ' 区切り記号 | を見つける
            Dim l As String = lines(i)
            Dim pos As Integer = 0
            Do
                pos = l.IndexOf("|"c, pos)
                If pos < 0 Then Exit Do
                Dim count As Integer = 0
                For j As Integer = pos - 1 To 0 Step -1
                    If Not l(j) = "\"c Then Exit For
                    count += 1
                Next
                If count Mod 2 = 0 Then Exit Do
                pos += 1
            Loop

            ' 見つからなかった場合は例外
            If pos < 0 Then
                Throw New WordParseFaultException("正しい区切り記号が見つからなかった")
            End If

            ' DefItem構造体に格納
            Dim head As String = l.Substring(0, pos).Trim()
            Dim text As String = l.Substring(pos + 1).Trim()
            items(i * 2).IsHeading = True
            items(i * 2).Text = head
            items(i * 2 + 1).IsHeading = False
            items(i * 2 + 1).Text = text

        Next

        ' データを格納して返却
        Dim def As WordItem.DefList = New WordItem.DefList
        def.Data = Nothing
        def.Text = String.Join(vbCrLf, lines)
        def.Caption = ""
        def.Items = items
        Return def

    End Function

    ''' <summary>
    ''' 定義リストを読み込む(正式表記)
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">最初の１行を受け取り、引用テキストの次の１行を返す</param>
    ''' <returns>解析した定義済リストのデータ</returns>
    ''' <remarks>バグに対する耐性が低いのであとで見直し</remarks>
    Private Function ReadDefList(ByRef reader As System.IO.StringReader, ByRef line As String) As WordItem.DefList

        Dim def As New WordItem.DefList
        Dim items As ArrayList = New ArrayList
        Dim isFirstRow As Boolean = True
        Dim caption As String = ""
        Dim lines As String = ""
        Dim level As Integer = GetLevel(line)

        Do
            Dim trimed As String = line.TrimStart()
            Dim deep As Integer = line.Length - trimed.Length
            Dim prefix As String = trimed.Substring(0, 2).Trim()
            Dim item As WordItem.DefItem = New WordItem.DefItem

            If prefix = ":" Then
                ' キャプション部
                If Not isFirstRow Then Exit Do
                def.Caption = trimed.Substring(2).Trim()

            ElseIf prefix = ":>" Then
                ' 見出し部
                item.IsHeading = True
                item.Text = trimed.Substring(2).Trim()
                items.Add(item)

            ElseIf prefix = ":<" Then
                ' データ部
                item.IsHeading = False
                Dim text As String = trimed.Substring(2).Trim()
                If Not String.IsNullOrEmpty(text) Then
                    ' データのみの場合
                    item.Text = trimed.Substring(3).Trim()
                    item.ChildItems = Nothing
                    items.Add(item)
                Else
                    ' 子要素が存在する場合
                    line = reader.ReadLine()
                    If line Is Nothing Then
                        Throw New WordParseFaultException("定義リストが不完全に終わっている")
                    End If
                    item.ChildItems = ReadDefListChild(reader, line)
                    item.Text = Nothing
                    items.Add(item)
                    Continue Do
                End If
            Else
                Exit Do
            End If

            ' 次の行を読み込む(最後まで読み終わったときは終了)
            line = reader.ReadLine()
            If line Is Nothing Then Exit Do
            lines &= line
            line = line.TrimEnd()
            isFirstRow = False

        Loop

        ' データを格納して返却
        def.Level = level
        def.Caption = caption
        def.Text = lines
        def.Data = Nothing
        def.Items = CType(items.ToArray(GetType(WordItem.DefItem)), WordItem.DefItem())
        Return def

    End Function

    ''' <summary>
    ''' 定義リストの子要素を読み込む
    ''' </summary>
    ''' <param name="reader">データを読み込むためのストリームクラス</param>
    ''' <param name="line">最初の１行を受け取り、最後に読み取った１行を返す</param>
    ''' <returns>読み取った子要素</returns>
    ''' <remarks>他の多くのファンクションと違い、lineで値を受け取らない</remarks>
    Private Function ReadDefListChild(ByRef reader As System.IO.StringReader, ByRef line As String) As ArrayList

        Dim items As New ArrayList
        Dim item As WordItem.WordItemBase
        Dim level As Integer
        Dim last_level As Integer

        last_level = GetLevel(line)
        Do
            level = GetLevel(line)
            If Not last_level = level Then Exit Do
            item = ParseBodyItem(reader, line)
            If item Is Nothing Then
                Throw New WordParseFaultException()
            End If
            items.Add(item)
        Loop
        Return items

    End Function

#End Region

#Region "テーブル"


    ''' <summary>
    ''' テーブルを読み込む(簡易書式)
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="work"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadTableItem(ByRef reader As System.IO.StringReader, ByRef work As String) As WordItem.Table
        Dim w As ArrayList = New ArrayList

        Const t1 As String = "||"
        Const t2 As String = "|="

        Do
            Dim prefix As String = work.Trim().Substring(0, 2).Trim()
            If Not prefix = t1 And Not prefix = t2 Then Exit Do
            w.Add(work)
            work = reader.ReadLine()
            If work Is Nothing Then Exit Do
            work = work.TrimEnd()
        Loop

        Dim rows As Integer = w.Count
        Dim cols As Integer = RowParts(w(0).ToString).Length

        Dim t As WordItem.Table = New WordItem.Table
        t.Data = ""
        Dim cells(cols - 1, rows - 1) As WordItem.TableCell
        Dim text(cols - 1, rows - 1) As String

        ' セルの値を取得する
        For i As Integer = 0 To rows - 1
            Dim prefix As String = w(i).ToString.Trim().Substring(0, 2).Trim()
            Dim parts() As String = RowParts(w(i).ToString)
            For j As Integer = 0 To cols - 1
                cells(j, i).Text = parts(j)
                cells(j, i).RowSpan = 1
                cells(j, i).ColSpan = 1
                cells(j, i).IsEmpty = False
                If prefix = t2 Then
                    cells(j, i).IsHeader = True
                Else
                    cells(j, i).IsHeader = False
                End If

                ' テスト用
                text(j, i) = parts(j)

            Next
        Next

        ' セルの連結情報・ヘッダ情報・align情報などを取得する
        Dim span(cols - 1, rows - 1) As String
        For i As Integer = rows - 1 To 0 Step -1
            For j As Integer = cols - 1 To 0 Step -1

                If String.IsNullOrEmpty(cells(j, i).Text) Then
                    cells(j, i).Text = ""
                    Continue For
                End If

                If cells(j, i).Text(0) = "=" Then
                    cells(j, i).IsHeader = True
                    cells(j, i).Text = cells(j, i).Text.Substring(1)
                End If

                Dim prefix As String
                If cells(j, i).Text.Length > 1 Then
                    prefix = cells(j, i).Text.Substring(0, 2)
                ElseIf cells(j, i).Text.Length > 0 Then
                    prefix = cells(j, i).Text(0)
                Else
                    prefix = ""
                End If

                If cells(j, i).IsHeader Then
                    cells(j, i).Align = 0
                Else
                    cells(j, i).Align = -1
                End If

                If prefix = "><" Then
                    cells(j, i).Align = 0
                    cells(j, i).Text = cells(j, i).Text.Substring(2).Trim()

                ElseIf prefix = "<<" Then
                    cells(j, i).Align = -1
                    cells(j, i).Text = cells(j, i).Text.Substring(2).Trim()

                ElseIf prefix = ">>" Then
                    cells(j, i).Align = 1
                    cells(j, i).Text = cells(j, i).Text.Substring(2).Trim()

                ElseIf prefix = "<" And Not cells(j, i).IsEmpty Then
                    For k As Integer = j To 0 Step -1
                        If String.IsNullOrEmpty(cells(k, i).Text) OrElse cells(k, i).Text(0) <> "<" Then
                            cells(k, i).ColSpan = j - k + 1
                            Exit For
                        End If
                        cells(k, i).IsEmpty = True
                    Next

                ElseIf prefix = "~" And Not cells(j, i).IsEmpty Then
                    For k As Integer = i To 0 Step -1
                        If String.IsNullOrEmpty(cells(j, k).Text) OrElse cells(j, k).Text(0) <> "~" Then
                            cells(j, k).RowSpan = i - k + 1
                            Exit For
                        End If
                        cells(j, k).IsEmpty = True
                    Next
                End If
            Next
        Next

        ' セルの連結情報を設定する
        For i As Integer = 0 To rows - 1
            For j As Integer = 0 To cols - 1
                span(j, i) = String.Format("{0},{1}", cells(j, i).ColSpan, cells(j, i).RowSpan)
            Next
        Next

        ' 空のセルの情報を設定する
        Dim empty(cols - 1, rows - 1) As Boolean
        For i As Integer = 0 To rows - 1
            For j As Integer = 0 To cols - 1
                If Not cells(j, i).IsEmpty And (cells(j, i).RowSpan > 1 Or cells(j, i).ColSpan > 1) Then
                    For y As Integer = 0 To cells(j, i).RowSpan - 1
                        For x As Integer = 0 To cells(j, i).ColSpan - 1
                            cells(j + x, i + y).IsEmpty = True
                            empty(j + x, i + y) = True
                        Next
                    Next
                    cells(j, i).IsEmpty = False
                    empty(j, i) = False
                End If
            Next
        Next

        t.Cells = cells
        Return t

    End Function

    ''' <summary>
    ''' テーブルの行を分割する
    ''' </summary>
    ''' <param name="line"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RowParts(ByVal line As String) As String()

        Dim reg As Regex = New Regex("(\\*)\|")

        Dim w As ArrayList = New ArrayList

        line = line.Trim.Substring(3)
        Dim mc As MatchCollection = reg.Matches(line)
        Dim pos As Integer = 0

        For i As Integer = 0 To mc.Count - 1

            ' エスケープされた区切り記号の場合は除外する
            Dim gv As String = mc(i).Groups(1).Value
            If gv.Length Mod 2 = 1 Then
                ' 一番最後でないかを確認する。一番最後の場合は格納する
                If mc(i).Index + mc(i).Length = line.Length Then
                    w.Add(mc(i).Value)
                End If
                Continue For
            End If

            ' 列を格納する
            Dim index As Integer = mc(i).Index
            Dim length As Integer = mc(i).Value.Length
            w.Add(line.Substring(pos, index - pos + length - 1).Trim())
            pos = index + length

            ' 残りが最後の列の場合は格納する
            If i + 1 = mc.Count Then
                w.Add(line.Substring(index + length).Trim())
            End If

        Next
        Return CType(w.ToArray(GetType(String)), String())

    End Function

    ''' <summary>
    ''' テーブルを読み込む(正式書式：ただし実装されていない)
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="work"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadTableItemFull(ByRef reader As System.IO.StringReader, ByRef work As String) As WordItem.Table
        Dim lines As ArrayList = New ArrayList
        Dim rows As Integer = 0
        Dim cols As Integer = 0
        Dim t As WordItem.Table = New WordItem.Table()
        Dim isFirstRow As Boolean = True
        Do
            Dim trimed As String = work.TrimStart()
            Dim prefix As String
            If trimed.Length < 2 Then
                prefix = trimed
            Else
                prefix = trimed.Substring(0, 2)
            End If

            If prefix = "|" Then
                If Not isFirstRow Then Exit Do
                t.Data = trimed.Substring(1).Trim()
                GoTo skip
            End If

            If prefix.Length < 2 Then Exit Do

            If prefix = "|>" Then
                rows += 1
            ElseIf prefix(0) = "|" And (Char.IsDigit(prefix(1)) Or prefix(1) = "|") Then
                ' 特になし
            Else
                Exit Do
            End If

skip:
            lines.Add(trimed)
            work = reader.ReadLine()
            If work Is Nothing Then Exit Do
            work = work.TrimEnd()
            isFirstRow = False

        Loop


        Dim cells(cols, rows) As WordItem.TableCell

        t.Cells = cells
        Return t


    End Function

#End Region

#End Region

#End Region

#Region "共通関数"

    ''' <summary>
    ''' デコード
    ''' </summary>
    ''' <param name="text">処理される前の文字列</param>
    ''' <returns>処理されたあとの文字列</returns>
    Private Function Decode(ByVal text As String) As String

        ' 数値参照、文字参照
        text = Transrate.DecodeEntityReference(text)
        text = Transrate.DecodeNumberReference(text)

        ' エスケープ文字除去
        text = Transrate.DecodeEscapeLetter(text)

        Return text

    End Function

    ''' <summary>
    ''' 字下げのレベルを取得
    ''' </summary>
    ''' <param name="line">解析する文字列</param>
    ''' <returns>字下げされているレベル</returns>
    ''' <remarks>Tab、空白文字ともに1文字と計算</remarks>
    Private Function GetLevel(ByVal line As String) As Integer
        Return line.Length - line.TrimStart.Length
    End Function

#End Region

End Class


