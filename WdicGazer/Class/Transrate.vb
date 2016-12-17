Imports System.Text.RegularExpressions

''' <summary>
''' 基礎知識本文の特殊記号等の処理を行う共通クラス
''' </summary>
''' <remarks></remarks>
Public Class Transrate

#Region "定義"

    ''' <summary>
    ''' 年と年号の変換対応
    ''' </summary>
    ''' <remarks>NoTrans:変換なし、Era:年号、Imperial:皇紀</remarks>
    Private Enum DateGenerateMode
        NoTrans
        Era
        Imperial
    End Enum

    ''' <summary>
    ''' 日付出力の単位
    ''' </summary>
    ''' <remarks>出力形式DateFormat?に対応</remarks>
    Private Enum DatePrecision
        Year
        Month
        Day
    End Enum

#Region "定数"

#Region "正規表現"

    ''' <summary>
    ''' 年月日表示を検索するための正規表現
    ''' </summary>
    Private Shared DateSearch() As String = New String() _
    {"\\date{((\d+)年(\d{1,2}月(\d{1,2}日)?)?)};", "(\\)+date:((\d+)年(\d{1,2}月(\d{1,2}日)?)?);"}

    ''' <summary>
    ''' 時間を検索するための正規表現
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TimeSearch As String = "(\\)+dt{(\d\d\d\d)/(\d\d)/(\d\d) (\d\d):(\d\d)(:(\d\d))? (\+|-)(\d\d)(\d\d)};"
    ''' <summary>
    ''' 1文字エスケープ記号を検索するための正規表現
    ''' </summary>
    Private Const OneLetterSearch As String = "\\(.)"

    ''' <summary>
    ''' 文字参照を検索するための正規表現
    ''' </summary>
    Private Const CodeSearch As String = "(\\)+(\w+?);"

    ''' <summary>
    ''' コード参照を検索するための正規表現
    ''' </summary>
    Private Shared NumberSearch() As String = New String() {"(\\)+x\{([0-9a-fA-F]+)\};", "(\\)+x:([0-9a-fA-F]+);"}

    ''' <summary>
    ''' 単位書式を検索するための正規表現
    ''' </summary>
    Private Shared UnitSearch() As String = New String() {"(\\)+unit:(\d+(\.\d+)?)(.+?);", "(\\)+unit\{(\d+(\.\d+)?)(.+?)\};"}

    ''' <summary>
    ''' 上付き、下付きを検索するための正規表現
    ''' </summary>
    Private Shared SupSubSearch() As String = New String() {"(\\)+(sup|sub)\{(.*?)\};", "(\\)+(sup|sub):(.*?);"}

    ''' <summary>
    ''' ルビを検索するための正規表現
    ''' </summary>
    Private Shared RubySearch() As String = New String() {"\\+ruby\{(.*?)\}\{(.*?)\};", "\\ruby:(.*?):(.*?);"}

    ''' <summary>
    ''' 未定義命令を検索するための正規表現
    ''' </summary>
    Private Shared UndefinedSearch() As String = New String() {"(\\)+(.*?)\{(.*?)\}(\{.*?\})*;", "(\\)+(.*?)(:.*?)(:.*?)*;"}

    ''' <summary>
    ''' リンクを検索するための正規表現
    ''' </summary>
    Private Const LinkSearch As String = "\[\[(<(.*?)>)?(.*?)\]\]"

    ''' <summary>
    ''' 強調を検索するための正規表現
    ''' </summary>
    Private Shared EmphasisSearch() As String = New String() {"'''(.*?)'''", "''(.*?)''"}

    ''' <summary>
    ''' プラグイン書式を検索するための正規表現
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PlugInFormat As String = "^//(.+?)/(.+?)\|(.*)$"





#End Region

#Region "出力形式"

    ''' <summary>
    ''' 年月日の出力形式
    ''' </summary>
    Private Const DateFormat1 As String = "yyyy年M月d日"

    ''' <summary>
    ''' 年月の出力形式
    ''' </summary>
    Private Const DateFormat2 As String = "yyyy年M月"

    ''' <summary>
    ''' 年の出力形式
    ''' </summary>
    Private Const DateFormat3 As String = "yyyy年"

    ''' <summary>
    ''' 変換に失敗した場合の代替表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FailedTrans As String = "(変換失敗)"

#End Region

#End Region

#End Region

#Region "メタ処理(一般)"

    ''' <summary>
    ''' 1文字エスケープ記号変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function DecodeEscapeLetter(ByVal str As String) As String

        ' 正規表現用意
        Dim search As String = OneLetterSearch
        Dim r As Regex = New Regex(search)
        Dim mc As MatchCollection = r.Matches(str)

        ' すべての文字を処理
        For i As Integer = mc.Count - 1 To 0 Step -1
            Dim m As Match = mc(i)

            ' 正当なエスケープかを確認(ここは古典的方法でやるしかない)
            If m.Groups(1).Value.Length Mod 2 = 0 Then Continue For
            Dim count As Integer = 0
            For j As Integer = m.Index To 0 Step -1
                If str(j) <> "\"c Then Exit For
                count += 1
            Next
            If count Mod 2 = 0 Then Continue For

            ' 文字入れ替え
            str = str.Remove(m.Index, m.Length)
            Dim hit As String = m.Groups(1).Value
            Select Case hit
                Case "'"
                    str = str.Insert(m.Index, "&#39;")
                Case """"
                    str = str.Insert(m.Index, "&quot;")
                Case "<"
                    str = str.Insert(m.Index, "&lt;")
                Case ">"
                    str = str.Insert(m.Index, "&gt;")
                Case Else
                    str = str.Insert(m.Index, m.Groups(1).Value)
            End Select

        Next
        Return str

    End Function

    ''' <summary>
    ''' 文字参照変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function DecodeEntityReference(ByVal str As String) As String

        ' 正規表現用意
        Dim search As String = CodeSearch
        Dim r As Regex = New Regex(search)
        Dim mc As MatchCollection = r.Matches(str)

        ' すべての文字を処理
        For i As Integer = mc.Count - 1 To 0 Step -1

            ' 正当なエスケープかを確認
            Dim m As Match = mc(i)
            If m.Groups(1).Value.Length Mod 2 = 0 Then Continue For

            ' 一致確認
            Dim dr() As DataRow = Main.Environment.EntityTable.Select(String.Format("{0}='{1}'", "name", m.Groups(2).Value))
            If dr.Length = 0 Then Continue For

            ' 入れ替え
            str = str.Remove(m.Index, m.Length).Insert(m.Index, m.Groups(1).Value.Substring(1) & CStr(dr(0).Item("letter")))

        Next
        Return str
    End Function

    ''' <summary>
    ''' コード参照変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function DecodeNumberReference(ByVal str As String) As String
        For Each search As String In NumberSearch
            Dim r As Regex = New Regex(search)
            Dim mc As MatchCollection = r.Matches(str)
            For i As Integer = mc.Count - 1 To 0 Step -1
                Dim m As Match = mc(i)
                If m.Groups(1).Value.Length Mod 2 = 0 Then Continue For

                Dim letter As String
                Try
                    letter = Chr(Convert.ToInt32(m.Groups(2).Value, 16))
                Catch ex As Exception
                    letter = FailedTrans
                End Try
                str = str.Remove(m.Index, m.Length).Insert(m.Index, m.Groups(1).Value.Substring(1) & letter)
            Next
        Next
        Return str
    End Function

    ''' <summary>
    ''' 未定義コマンド除去
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function EraseUndefined(ByVal str As String) As String
        For Each search As String In UndefinedSearch
            Dim r As Regex = New Regex(search)
            Dim mc As MatchCollection = r.Matches(str)
            For i As Integer = mc.Count - 1 To 0 Step -1
                Dim m As Match = mc(i)
                If m.Groups(1).Value.Length Mod 2 = 0 Then Continue For
                str = str.Remove(m.Index, m.Length).Insert(m.Index, m.Groups(1).Value.Substring(1) & m.Groups(3).Value)
                Dim a As String = m.Groups(0).Value
                Dim b As String = m.Groups(1).Value
                Dim c As String = m.Groups(2).Value
            Next
        Next
        Return str
    End Function

#End Region

#Region "メタ処理(単位変換)"

    ''' <summary>
    ''' 単位変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function DecodeUnits(ByVal str As String) As String
        For j As Integer = 0 To UnitSearch.Length - 1
            Dim search As String = UnitSearch(j)
            Dim r As Regex = New Regex(search)
            Dim mc As MatchCollection = r.Matches(str)
            For i As Integer = mc.Count - 1 To 0 Step -1
                Dim m As Match = mc(i)
                If m.Groups(1).Value.Length Mod 2 = 0 Then Continue For
                Dim trans As String = TransUnits(CDec(m.Groups(2).Value), m.Groups(4).Value, My.Settings.NumberPrecision)
                str = str.Remove(m.Index, m.Length).Insert(m.Index, m.Groups(1).Value.Substring(1) & trans)
            Next
        Next
        Return str
    End Function

    ''' <summary>
    ''' 慣習単位をSI単位に変換
    ''' </summary>
    ''' <param name="amount">量</param>
    ''' <param name="unit">単位</param>
    ''' <param name="precision">未利用</param>
    ''' <returns>変換された単位</returns>
    Private Shared Function TransUnits( _
        ByVal amount As Decimal, ByVal unit As String, Optional ByVal precision As Integer = 3) _
        As String

        Select Case unit
            Case "m2"   ' 平方m
                Return amount.ToString() & "m" & ChrW(&HB2)
            Case "m3"   ' 立方m
                Return amount.ToString() & "m" & ChrW(&HB3)
            Case "cm2"  ' 平方cm
                Return amount.ToString() & "cm" & ChrW(&HB2)
            Case "cm3"  ' 立方cm
                Return amount.ToString() & "cm" & ChrW(&HB3)
            Case "mm2"  ' 平方mm
                Return amount.ToString() & "mm" & ChrW(&HB2)
            Case "mm3"  ' 立方mm
                Return amount.ToString() & "mm" & ChrW(&HB3)
            Case "km2"  ' 平方km
                Return amount.ToString() & "km" & ChrW(&HB2)
            Case "km3"  ' 立方km
                Return amount.ToString() & "km" & ChrW(&HB3)
            Case "km/h/s"   ' km毎時毎秒 -> m毎秒毎秒
                Dim mss As Decimal = amount * 1000 / 3600
                Return String.Format("{0}km/h/s({1}m/s{2})", amount, IndexExpression(mss, precision), ChrW(&HB2))
            Case "km/s2"    ' km毎秒毎秒 -> そのまま
                Return amount.ToString() & "km/s" & ChrW(&HB3)
            Case "m/s2"     ' m毎秒毎秒  -> そのまま
                Return amount.ToString() & "m/s" & ChrW(&HB3)
            Case "cm/s2"    ' cm毎秒毎秒 -> そのまま
                Return amount.ToString() & "cm/s" & ChrW(&HB3)
            Case "mm/s2"    ' mm毎秒毎秒 -> そのまま
                Return amount.ToString() & "mm/s" & ChrW(&HB3)
            Case "A"             ' SI暫定単位

            Case "海里"
                Dim kairi As Decimal = amount * 1852 / 1000
                Return String.Format("{0}海里({1}km)", amount, IndexExpression(kairi, precision))
            Case "パーセク"
                Dim persec As Decimal = CDec(amount * 3.08568 * 10 ^ 16)
                Return String.Format("{0}パーセク({1}m)", amount, IndexExpression(persec, precision))
            Case "天文単位"

            Case "カラット"      ' 日本の計量法で認められているもの
            Case "匁"
            Case "トロイオンス"

            Case "アール"        ' 面積で広く使われているもの
            Case "ヘクタール"
            Case "坪"

            Case "ノット"
            Case "ガル", "Gal", "gal"


        End Select

        Return amount.ToString() & unit

    End Function

#End Region

#Region "メタ変換(年月日)"

    ''' <summary>
    ''' 西暦から年号に変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function DecodeDateEra(ByVal str As String) As String
        Return DecodeDateCommon(str, DateGenerateMode.Era)
    End Function

    ''' <summary>
    ''' 西暦から皇紀に変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function DecodeDateImperial(ByVal str As String) As String
        Return DecodeDateCommon(str, DateGenerateMode.Imperial)
    End Function

    ''' <summary>
    ''' 西暦だけに変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function DecodeDateClear(ByVal str As String) As String
        Return DecodeDateCommon(str, DateGenerateMode.NoTrans)
    End Function

    ''' <summary>
    ''' 年変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function DecodeDate(ByVal str As String) As String
        Return DecodeDateCommon(str, GetYearMode())
    End Function

    ''' <summary>
    ''' 年変換共通処理
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Private Shared Function DecodeDateCommon(ByVal str As String, ByVal mode As DateGenerateMode) As String

        For Each d As String In DateSearch

            Dim r As Regex = New Regex(d)
            Dim mc As MatchCollection = r.Matches(str)
            For i As Integer = mc.Count - 1 To 0 Step -1

                ' 文字列抽出
                Dim m As Match = mc(i)
                Dim count As Integer = 0
                For j As Integer = m.Index To 0 Step -1
                    If str(j) <> "\"c Then Exit For
                    count += 1
                Next
                If count Mod 2 = 0 Then Continue For
                Dim matchedstr As String = m.Groups(1).Value
                Dim addstr As String = ""
                For j As Integer = 1 To 4 - m.Groups(2).Value.Length
                    addstr &= "0"
                Next
                matchedstr = addstr & matchedstr

                ' 文字列->日付型変換
                Dim dt As DateTime
                If String.IsNullOrEmpty(m.Groups(3).Value) Then
                    dt = DateTime.ParseExact(matchedstr, DateFormat3, Nothing)
                ElseIf String.IsNullOrEmpty(m.Groups(4).Value) Then
                    dt = DateTime.ParseExact(matchedstr, DateFormat2, Nothing)
                Else
                    dt = DateTime.ParseExact(matchedstr, DateFormat1, Nothing)
                End If

                ' 西暦->年号変換
                Dim add As String
                If String.IsNullOrEmpty(m.Groups(3).Value) Then
                    add = ComputeEra(dt, mode, DatePrecision.Year)
                ElseIf String.IsNullOrEmpty(m.Groups(4).Value) Then
                    add = ComputeEra(dt, mode, DatePrecision.Month)
                Else
                    add = ComputeEra(dt, mode, DatePrecision.Day)
                End If

                ' 置換
                If Not String.IsNullOrEmpty(add) Then add = "(" & add & ")"
                If String.IsNullOrEmpty(m.Groups(3).Value) Then
                    str = str.Remove(m.Index, m.Length).Insert(m.Index, dt.ToString("yyyy年" & add).TrimStart("0"c))
                ElseIf String.IsNullOrEmpty(m.Groups(4).Value) Then
                    str = str.Remove(m.Index, m.Length).Insert(m.Index, dt.ToString("yyyy年" & add & "M月").TrimStart("0"c))
                Else
                    str = str.Remove(m.Index, m.Length).Insert(m.Index, dt.ToString("yyyy年" & add & "M月d日").TrimStart("0"c))
                End If
            Next
        Next

        Return str

    End Function

    ''' <summary>
    ''' 年号/皇紀計算
    ''' </summary>
    ''' <param name="dt">グレゴリウス暦またはユリウス暦</param>
    ''' <param name="mode">変換モード(なし/皇紀/年号)</param>
    ''' <param name="precision">出力モード(年/月/日)</param>
    ''' <returns>所定の書式の年月日表記</returns>
    ''' <remarks>本文の年月日はすべてグレゴリウス暦およびユリウス暦で記述されていると*仮定する*</remarks>
    Private Shared Function ComputeEra(ByVal dt As DateTime, ByVal mode As DateGenerateMode, ByVal precision As DatePrecision) As String

        ' 何もなし/皇紀(注：現在のところ旧暦への変換は行っていない)
        If mode = DateGenerateMode.NoTrans Then Return ""
        If mode = DateGenerateMode.Imperial Then Return "皇紀" & (dt.Year + 660) & "年"
        Dim year As Integer = dt.Year

        ' 元号検索
        Dim where As String = String.Format("{0}<='{1}'", "start_date", dt.ToString("yyyy/MM/dd"))
        Dim order As String = "start_date DESC"
        Dim dr() As DataRow = Main.Environment.EraTable.Select(where, order)
        If dr.Length = 0 Then Return ""

        ' 組み立て
        Dim era_year As Integer = dt.Year - CDate(dr(0).Item("start_date")).Year + 1
        If era_year = 1 Then
            Return CStr(dr(0).Item("name")) & "元年"
        Else
            Return CStr(dr(0).Item("name")) & era_year & "年"
        End If
        Return ""

    End Function

    ''' <summary>
    ''' 現在の年表示モードを取得
    ''' </summary>
    ''' <returns>年表示モード</returns>
    Private Shared Function GetYearMode() As DateGenerateMode
        If My.Settings.YearBC Then
            Return DateGenerateMode.NoTrans
        ElseIf My.Settings.YearEra Then
            Return DateGenerateMode.Era
        ElseIf My.Settings.YearImperial Then
            Return DateGenerateMode.Imperial
        End If
    End Function

#End Region

#Region "メタ変換(時間)"

    ''' <summary>
    ''' 年月日時間を変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function TimeTrans(ByVal str As String) As String

        Dim r As Regex = New Regex(TimeSearch)
        Dim mc As MatchCollection = r.Matches(str)
        For i As Integer = mc.Count - 1 To 0 Step -1
            Dim m As Match = mc(i)
            If m.Groups(1).Value.Length Mod 2 = 0 Then Continue For

            Dim trans As String = ""
            Try
                ' 時間に変換
                Dim isHaveSec As Boolean = False
                Dim sec As Integer = 0
                If Not String.IsNullOrEmpty(m.Groups(8).Value) Then
                    Dim val As String = m.Groups(8).Value
                    sec = CInt(m.Groups(8).Value)
                    isHaveSec = True
                End If
                Dim d As DateTime = New DateTime( _
                    CInt(m.Groups(2).Value), _
                    CInt(m.Groups(3).Value), _
                    CInt(m.Groups(4).Value), _
                    CInt(m.Groups(5).Value), _
                    CInt(m.Groups(6).Value), _
                    sec)
                Dim add_h As Integer = CInt(m.Groups(10).Value)
                Dim add_m As Integer = CInt(m.Groups(11).Value)
                If m.Groups(9).Value = "+" Then
                    add_h = -add_h
                    add_m = -add_m
                End If

                ' 時間表記に変換
                Dim era As String = ComputeEra(d, GetYearMode(), DatePrecision.Day)
                If Not String.IsNullOrEmpty(era) Then era = "(" & era & ")"
                Dim sec_str As String = ""
                If isHaveSec Then
                    sec_str = d.Second & "秒"
                End If
                If My.Settings.Time24 Then
                    trans = String.Format("{0}年{1}{2}月{3}日{4}時{5}分{6}", d.Year, era, d.Month, d.Day, d.Hour, d.Minute, sec_str)
                Else
                    Dim ampm As String = "午前"
                    Dim hour As Integer = d.Hour
                    If hour > 12 Then
                        hour -= 12
                        ampm = "午後"
                    End If
                    trans = String.Format("{0}年{1}{2}月{3}日{7}{4}時{5}分{6}", d.Year, era, d.Month, d.Day, d.Hour, d.Minute, sec_str, ampm)
                End If
                If Not (add_h = -9 And add_m = 0) Then
                    trans = "現地時間" & trans
                End If

                ' UTC時間に変換
                d = d.AddHours(add_h)
                d = d.AddMinutes(add_m)

                ' beatに変換
                Dim beat As Decimal = CDec(1000 * (3600 * (d.Hour + 1) + 60 * d.Minute + d.Second) / (24 * 60 * 60))
                If My.Settings.TimeBeat Then
                    trans = String.Format("{0}(@{1:####.##}beat)", trans, beat)
                End If

            Catch ex As Exception
                trans = FailedTrans
            End Try
            str = str.Remove(m.Index, m.Length).Insert(m.Index, m.Groups(1).Value.Substring(1) & trans)

        Next
        Return str

    End Function

#End Region

#Region "HTML変換"

    ''' <summary>
    ''' ルビ設定
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function SetRuby(ByVal str As String) As String

        For Each search As String In RubySearch
            Dim r As Regex = New Regex(search)
            Dim mc As MatchCollection = r.Matches(str)
            For i As Integer = mc.Count - 1 To 0 Step -1
                Dim m As Match = mc(i)
                Dim count As Integer = 0
                For j As Integer = m.Index To 0 Step -1
                    If str(j) <> "\"c Then Exit For
                    count += 1
                Next
                If count Mod 2 = 0 Then Continue For

                Dim insert As String = Nothing
                If My.Settings.RubyParent Then
                    insert = "{0}({1})"
                ElseIf My.Settings.RubyUse Then
                    insert = "<ruby><rb>{0}</rb><rp>(</rp><rt>{1}</rt><rp>)</rp></ruby>"
                End If
                If insert Is Nothing Then
                    insert = m.Groups(1).Value
                Else
                    insert = String.Format(insert, m.Groups(1).Value, m.Groups(2).Value)
                End If
                str = str.Remove(m.Index, m.Length).Insert(m.Index, insert)
            Next
        Next
        Return str

    End Function

    ''' <summary>
    ''' リンク生成
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function SetLink(ByVal str As String) As String

        Dim r As Regex = New Regex(LinkSearch)
        Dim mc As MatchCollection = r.Matches(str)
        For i As Integer = mc.Count - 1 To 0 Step -1

            ' 文字列抽出は "\[\[(<(.+?)>)?(.+?)\]\]"
            Dim m As Match = mc(i)
            Dim display As String = m.Groups(2).Value
            Dim link As String = m.Groups(3).Value
            If String.IsNullOrEmpty(display) Then
                If IsHaveSceamer(link) Or link(0) <> "/"c Then
                    display = link
                Else
                    display = link.Substring(link.IndexOf("/"c, 1) + 1)
                End If
            End If

            Dim change As String
            If IsHaveSceamer(link) Then
                ' 外部リンク
                change = String.Format("<a href=""{0}"">{1}</a>", link, display)

            ElseIf IsPlugIn(link) Then
                ' 画像・プラグインリンク
                change = SetPluginLink(display, link)

            Else
                ' 単語リンク
                link = HtmlEscape(link)
                change = String.Format("<a href=""x-wdic:{0}"">{1}</a>", link, display)

            End If
            str = str.Remove(m.Index, m.Length).Insert(m.Index, change)
        Next

        Return str

    End Function

    ''' <summary>
    ''' リンクがプラグインであるかどうかを調べる
    ''' </summary>
    ''' <param name="link"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function IsPlugIn(ByVal link As String) As Boolean
        If link.Length < 3 Then Return False
        If link(0) = "/" And link(1) = "/" Then Return True
        Return False
    End Function

    ''' <summary>
    ''' リンクが画像ファイルであるかどうかを調べる
    ''' </summary>
    ''' <param name="link"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function IsGraphicFile(ByVal link As String) As Boolean
        Dim r As Regex = New Regex(PlugInFormat)
        Dim m As Match = r.Match(link & "|")
        Dim file As String = m.Groups(2).Value
        Dim ext As String = System.IO.Path.GetExtension(file)
        ext = ext.ToUpper().TrimStart("."c)
        Select Case ext
            Case "JPG", "GIF", "PNG"
                Return True
            Case Else
                Return False
        End Select
        Return False
    End Function

    ''' <summary>
    ''' 画像・プラグインリンク
    ''' </summary>
    ''' <param name="display"></param>
    ''' <param name="link"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function SetPluginLink(ByVal display As String, ByVal link As String) As String

        Dim change As String = ""
        Dim d As WordItem.PluginBase
        Try
            d = GetPlugInData(link)
            d.Data = display

            Dim url As String = d.url()
            If TypeOf d Is WordItem.Graphic Then
                If My.Settings.DisplayGraphic And My.Settings.DisplayGraphicInParagraph Then
                    change = String.Format("<img src=""{0}"" alt=""{1}"" />", url, display)
                Else
                    change = "(画像:" & display & ")"
                End If
            Else
                change = String.Format("<a href=""{0}"">{1}</a>", url, display)
            End If

        Catch ex As Exception
            change = "(プラグインの処理に失敗しました)"
        End Try
        Return change

    End Function

    ''' <summary>
    ''' 画像・プラグインのファイル情報、位置情報を取得する
    ''' </summary>
    ''' <param name="link"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Shared Function GetPlugInData(ByVal link As String) As WordItem.PluginBase

        ' ファイル文字列部分分解～データ取得
        Dim r As Regex = New Regex(PlugInFormat)
        Dim m As Match = r.Match(link & "|")
        If Not m.Success Then Return Nothing

        Dim wlf As String
        Dim file As String
        Dim param As String
        Dim filefull As String

        Try
            wlf = m.Groups(1).Value
            file = m.Groups(2).Value
            param = m.Groups(3).Value.Trim("|"c)
            filefull = Main.Environment.GetPluginFullpathFromFilename(file, -1)
        Catch ex As Exception
            ' プラグイン検索失敗
            Return Nothing
        End Try

        ' プラグインファイルデータ解析
        Dim d As WordItem.PluginBase
        If IsGraphicFile(link) Then
            d = New WordItem.Graphic()
        Else
            d = New WordItem.Plugin()
        End If
        d.FullFilename = filefull
        If TypeOf d Is WordItem.Graphic Then
            Dim params() As String = param.Trim("|"c).Split("|"c)
            For Each p As String In params
                With CType(d, WordItem.Graphic)
                    p = p.Trim().ToLower()
                    If p = "right" Then
                        .Float = 1
                    ElseIf p = "left" Then
                        .Float = -1
                    ElseIf p = "center" Then
                        .Float = 0
                    Else
                        r = New Regex("^thumb:(\d+)(.+)$")
                        m = r.Match(p)
                        If m.Success Then
                            Dim tanni As String = m.Groups(2).Value
                            Dim point As Integer = CInt(m.Groups(1).Value)
                            If tanni = "px" Then
                                .Thumb = point
                            ElseIf tanni = "%" Then
                                .ThumbPercent = point
                            End If
                        End If
                    End If
                End With
            Next
        End If
        Return d

    End Function

    ''' <summary>
    ''' リンク消去
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function LinkDelete(ByVal str As String) As String
        ' 文字列抽出はの正規表現は LinkSearch 参照
        Dim r As Regex = New Regex(LinkSearch)
        Dim mc As MatchCollection = r.Matches(Str)
        For i As Integer = mc.Count - 1 To 0 Step -1
            Dim m As Match = mc(i)
            Dim display As String = m.Groups(2).Value
            If String.IsNullOrEmpty(display) Then
                display = m.Groups(3).Value
                If display(0) = "/"c Then
                    display = display.Substring(display.IndexOf("/", 1) + 1)
                End If
            End If
            str = str.Remove(m.Index, m.Length).Insert(m.Index, display)
        Next
        Return str
    End Function

    ''' <summary>
    ''' 強調
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function EmphasisTrans(ByVal str As String) As String
        For j As Integer = 0 To EmphasisSearch.Length - 1
            Dim r As Regex = New Regex(EmphasisSearch(j))
            Dim mc As MatchCollection = r.Matches(str)
            For i As Integer = mc.Count - 1 To 0 Step -1
                Dim m As Match = mc(i)
                Dim trans As String
                If j = 1 Then
                    trans = "<em>" & m.Groups(1).Value & "</em>"
                Else
                    trans = "<strong>" & m.Groups(1).Value & "</strong>"
                End If
                str = str.Remove(m.Index, m.Length).Insert(m.Index, trans)
            Next
        Next
        Return str
    End Function

    ''' <summary>
    ''' 上付き・下付き文字
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function SubSupTrans(ByVal str As String) As String
        For j As Integer = 0 To SupSubSearch.Length - 1
            Dim search As String = SupSubSearch(j)
            Dim r As Regex = New Regex(search)
            Dim mc As MatchCollection = r.Matches(str)
            For i As Integer = mc.Count - 1 To 0 Step -1
                Dim m As Match = mc(i)
                If m.Groups(1).Value.Length Mod 2 = 0 Then Continue For
                Dim trans As String = String.Format("<{0}>{1}</{0}>", m.Groups(2).Value, m.Groups(3).Value)
                str = str.Remove(m.Index, m.Length).Insert(m.Index, m.Groups(1).Value.Substring(1) & trans)
            Next
        Next
        Return str
    End Function

    ''' <summary>
    ''' 引用符を変換
    ''' </summary>
    ''' <param name="str">変換する前の文字列</param>
    ''' <returns>変換されたあとの文字列</returns>
    Public Shared Function HtmlEscape(ByVal str As String) As String
        str = str.Replace("&", "&amp;").Replace("""", "&quot;")
        Return str
    End Function

#End Region

#Region "外部提供汎用機能"

    ''' <summary>
    ''' 文字列の先頭がサポートされている外部スキーマであるかどうかを調べる
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsHaveSceamer(ByVal str As String) As Boolean
        Dim pos As Integer = str.IndexOf(":"c)
        If pos < 0 Then Return False
        Return IsSupportSceamer(str.Substring(0, pos))
    End Function

    ''' <summary>
    ''' サポートされているスキーマ文字列かどうか調べる
    ''' </summary>
    ''' <param name="scam"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IsSupportSceamer(ByVal scam As String) As Boolean
        Select Case scam
            Case "mailto", "http", "https", "ftp", _
                 "news", "gopher", "telnet", "wais", "prospero", _
                 "file", "urn", "phone"
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' 小数を指数表示する
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IndexExpression(ByVal number As Decimal) As String
        Return IndexExpression(number, My.Settings.NumberPrecision)
    End Function

    ''' <summary>
    ''' 小数を指数表示する
    ''' </summary>
    ''' <param name="number"></param>
    ''' <param name="precision"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function IndexExpression(ByVal number As Decimal, ByVal precision As Integer) As String
        If precision < 2 Then precision = 2
        Dim format As String = "0."
        For i As Integer = 1 To precision - 1
            format &= "0"
        Next
        format &= "E0"
        Dim kari As String = number.ToString(format)
        Dim pos As Integer = kari.IndexOf("E"c)
        Dim kasu As String = kari.Substring(0, pos)
        Dim sisu As String = kari.Substring(pos + 1)
        If sisu = "0" Then
            Return kasu
        Else
            Return kasu & "×10" & SupNumber(sisu)
        End If
    End Function

    ''' <summary>
    ''' 数字を上付き文字にする
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SupNumber(ByVal number As Integer) As String
        Return SupNumber(CStr(number))
    End Function

    ''' <summary>
    ''' 数字を上付き文字にする
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SupNumber(ByVal number As String) As String

        Static Dim supnumbers() As Char = _
        {ChrW(&H2070), ChrW(&HB9), ChrW(&HB2), ChrW(&HB3), _
        ChrW(&H2074), ChrW(&H2075), ChrW(&H2076), _
        ChrW(&H2077), ChrW(&H2078), ChrW(&H2079)}

        Dim supped As String = ""
        number = number.Trim()

        For i As Integer = 0 To number.Length - 1
            If number(i) = "+" Then
                supped &= ChrW(&H207A)
            ElseIf number(i) = "-" Then
                supped &= ChrW(&H207B)
            Else
                Dim letter As Integer
                Try
                    letter = Val(number(i))
                Catch ex As Exception
                    ' 数字以外の文字が混じっていたということ
                    Return ""
                End Try
                supped &= supnumbers(letter)
            End If
        Next
        Return supped
    End Function

    ''' <summary>
    ''' 数字を下付き文字にする
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SubNumber(ByVal number As Integer) As String
        Return SubNumber(CStr(number))
    End Function


    ''' <summary>
    '''  数字を下付き文字にする
    ''' </summary>
    ''' <param name="number"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SubNumber(ByVal number As String) As String

        Static Dim subnumbers() As Char = _
        {ChrW(&H2080), ChrW(&H2081), ChrW(&H2082), ChrW(&H2083), _
        ChrW(&H2084), ChrW(&H2085), ChrW(&H2086), _
        ChrW(&H2087), ChrW(&H2088), ChrW(&H2089)}

        Dim subed As String = ""
        number = number.Trim()

        For i As Integer = 0 To number.Length - 1
            If number(i) = "+" Then
                subed &= ChrW(&H208A)
            ElseIf number(i) = "-" Then
                subed &= ChrW(&H208B)
            Else
                Dim letter As Integer
                Try
                    letter = Val(number(i))
                Catch ex As Exception
                    ' 数字以外の文字が混じっていたということ
                    Return ""
                End Try
                subed &= subnumbers(letter)
            End If
        Next
        Return subed
    End Function

#End Region

End Class
