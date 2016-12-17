''' <summary>
''' FILE.GLを読み込むためのクラス
''' </summary>
Public Class FileGlReader
    Inherits FileReader

#Region "定義"

#Region "定数"

    ''' <summary>
    ''' INFOセクションの文字列
    ''' </summary>
    Protected Const InfoSection As String = "[INFO]"

    ''' <summary>
    ''' GROUPセクションの文字列
    ''' </summary>
    Protected Const GroupSection As String = "[GROUP]"

    ''' <summary>
    ''' PLUGINセクションの文字列
    ''' </summary>
    Protected Const PluginSection As String = "[PLUGIN]"

    ''' <summary>
    ''' コメントで使われるマーカー
    ''' </summary>
    Protected Const CommentMarker As Char = "#"c

    ''' <summary>
    ''' コメントで使われるマーカー
    ''' </summary>
    Protected Const EqualMarker As Char = "="c

    ''' <summary>
    ''' セクション行の開始の文字
    ''' </summary>
    ''' <remarks></remarks>
    Protected Const SectionStartMarker As Char = "["c

    ''' <summary>
    ''' セクション行の終了の文字
    ''' </summary>
    ''' <remarks></remarks>
    Protected Const SectionEndMarker As Char = "]"c


    ''' <summary>
    ''' INFOセクション.連絡先のタグ名
    ''' </summary>
    Protected Const CONTACT As String = "CONTACT"

    ''' <summary>
    ''' INFOセクション.辞書名のタグ名
    ''' </summary>
    Protected Const NAME As String = "NAME"

    ''' <summary>
    ''' INFOセクション.エディションのタグ名
    ''' </summary>
    Protected Const EDITION As String = "EDITION"

    ''' <summary>
    ''' GROUP/PLUGINセクション.辞書種類略称の列番号
    ''' </summary>
    Protected Const POS_TYPENAME As Integer = 0

    ''' <summary>
    ''' GROUPセクション.WLFファイル名の列番号
    ''' </summary>
    Protected Const POS_WLFFILENAME As Integer = 1

    ''' <summary>
    ''' GROUPセクション.辞書名の列番号
    ''' </summary>
    Protected Const POS_DICTONARYTYPE As Integer = 2

    ''' <summary>
    ''' PLUGINセクション.PLFファイルの列番号
    ''' </summary>
    ''' <remarks></remarks>
    Protected Const POS_PLFFILENAME As Integer = 1

    ''' <summary>
    ''' PLUGINセクション.辞書名の列番号
    ''' </summary>
    Protected Const POS_PLUGINTYPE As Integer = 2

#End Region

    ''' <summary>
    ''' データを格納するWdicDataクラス
    ''' </summary>
    Protected _EnvironmentData As EnvironmentData

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="filename">FILE.GLフルパス名</param>
    ''' <param name="env_data">読み取り結果を格納するためのEnvironmentDataクラス</param>
    Public Sub New(ByVal filename As String, ByRef env_data As EnvironmentData)
        MyBase.New(filename)
        If env_data Is Nothing Then Throw New System.ArgumentNullException()
        Me._EnvironmentData = env_data
    End Sub

#End Region

#Region "処理"

    ''' <summary>
    ''' FILE.GL読み込みとデータの格納
    ''' </summary>
    Public Sub Parse()

        ' まだファイルが読み込まれていない場合は読み込む
        If ReadText Is Nothing Then Read()

        ' セクション切り出し
        Dim ipos, gpos, ppos As Integer
        ipos = ReadText.IndexOf(InfoSection) + InfoSection.Length
        gpos = ReadText.IndexOf(GroupSection)
        ppos = ReadText.IndexOf(PluginSection)

        ' インフォメーション部分を取得
        Dim info As String = ReadText.Substring(ipos + 2, gpos - ipos - 3) ' CRLFの分だけ文字数を加減しなくてはいけない
        gpos += GroupSection.Length

        ' 辞書ファイル一覧部分を取得
        Dim groups As String = ""
        If ppos >= 0 Then
            groups = ReadText.Substring(gpos + 2, ppos - gpos - 3)
        Else
            groups = ReadText.Substring(gpos + 2)
        End If

        ' プラグインリスト部分を取得
        Dim plugins As String = Nothing
        If ppos >= 0 Then
            ppos += PluginSection.Length
            plugins = ReadText.Substring(ppos + 2)
        End If

        ' [INFO]セクションから基礎情報取得
        ParseInfo(info)

        ' [GROUP]セクションからグループ一覧情報とWLFファイル一覧情報を作成
        ParseGroup(groups)

        ' [PLUGIN]セクションからPLFファイル一覧情報を作成
        ParsePlugin(plugins)


    End Sub

    ''' <summary>
    ''' INFOセクションから基礎情報を取得
    ''' </summary>
    ''' <param name="info">INFOセクション部分の文字列</param>
    Private Sub ParseInfo(ByVal info As String)

        ' データ格納レコード
        Dim dt As DataTable = _EnvironmentData.WdicTable
        Dim dr As DataRow = dt.NewRow
        dr.Item("id") = 0

        ' [INFO]セクション -> 基礎情報取得
        Dim lines() As String = Split(info, vbCrLf)
        For Each line As String In lines

            ' 空行、コメント行は対象外
            If String.IsNullOrEmpty(line) Then Continue For
            If line(0) = CommentMarker Then Continue For

            ' 行を分解して変数名/値を取得する
            line = line.Trim()
            Dim b() As String = line.Split(EqualMarker)    ' 将来的に変更の可能性あり
            Dim element, value As String
            element = StrConv(b(0), VbStrConv.Uppercase).Trim()
            If b.Length >= 2 Then
                value = b(1).Trim()
            Else
                value = ""
            End If

            ' 値格納
            If element = CONTACT Then
                Dim c() As String = Split(b(1), vbTab)
                If c.Length > 0 Then dr.Item("email") = c(0).Trim
                If c.Length > 1 Then dr.Item("contacter") = c(1).Trim
            ElseIf element = NAME Then
                dr.Item("name") = value
            ElseIf element = EDITION Then
                dr.Item("edition") = value
            End If
        Next

        ' テーブルに登録
        dt.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' GROUPセクションからグループ一覧情報とWLFファイル情報を取得
    ''' </summary>
    ''' <param name="groups">GROUPセクション部分の文字列</param>
    Private Sub ParseGroup(ByVal groups As String)

        Dim dt1 As DataTable = _EnvironmentData.GroupTable()
        Dim dt2 As DataTable = _EnvironmentData.WlfFileTable()

        Dim lines As String() = Split(groups, vbCrLf)
        Dim i As Integer = 0
        For Each line As String In lines
            line = line.Trim()
            If line.Length = 0 Then Continue For
            Dim items() As String = line.Split(CChar(vbTab))

            ' groups に格納
            Dim dr1 As DataRow = dt1.NewRow
            dr1.Item("id") = i
            dr1.Item("type") = items(POS_TYPENAME)
            dr1.Item("wlf_file") = items(POS_WLFFILENAME)
            dr1.Item("dic_name") = items(POS_DICTONARYTYPE)

            ' wlf_files に格納
            Dim dr2 As DataRow = dt2.NewRow
            dr2.Item("id") = i
            dr2.Item("group_id") = i
            dr2.Item("filename") = items(POS_WLFFILENAME)
            dr2.Item("name") = items(POS_DICTONARYTYPE)

            dt1.Rows.Add(dr1)
            dt2.Rows.Add(dr2)

            i += 1

        Next

    End Sub

    ''' <summary>
    ''' PLUGINセクションからPLFファイル情報を取得
    ''' </summary>
    ''' <param name="plugin"></param>
    ''' <remarks></remarks>
    Private Sub ParsePlugin(ByVal plugin As String)

        If String.IsNullOrEmpty(plugin) Then Exit Sub
        Dim dt As DataTable = _EnvironmentData.PlfFileTable()
        Dim lines As String() = Split(plugin, vbCrLf)

        Dim i As Integer = 0
        For Each line As String In lines

            ' 分解
            line = line.Trim()
            If line.Length = 0 Then Continue For
            Dim items() As String = line.Split(CChar(vbTab))

            ' 辞書略称からgroup_idを取得
            Dim type As String = items(POS_TYPENAME)
            Dim dr1s() As DataRow = _EnvironmentData.GroupTable.Select(String.Format("{0}='{1}'", "type", type))
            If Not dr1s.Length = 1 Then
                Throw New InvalidFileGroupException()
                Exit Sub
            End If
            Dim group_id As Integer = CInt(dr1s(0).Item("id"))

            ' groups に格納
            Dim dr2 As DataRow = dt.NewRow
            dr2.Item("id") = i
            dr2.Item("group_id") = group_id
            dr2.Item("filename") = items(POS_PLFFILENAME)
            dr2.Item("name") = items(POS_PLUGINTYPE)
            dt.Rows.Add(dr2)

            i += 1

        Next

    End Sub

#End Region

End Class
