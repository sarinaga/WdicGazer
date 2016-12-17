''' <summary>
''' WLFファイルを読み込むクラス
''' </summary>
Public Class WlfReader
    Inherits FileReader

#Region "定義"

#Region "定数"

    ''' <summary>
    ''' グループセクションの文字
    ''' </summary>
    Public Const GroupSection As String = "[GROUP]"

    ''' <summary>
    ''' ファイルセクションの文字
    ''' </summary>
    Public Const FilesSection As String = "[FILES]"

    ''' <summary>
    ''' 編集者
    ''' </summary>
    Public Const CONTACT As String = "CONTACT"

    ''' <summary>
    ''' 辞書名
    ''' </summary>
    Public Const NAME As String = "NAME"

    ''' <summary>
    ''' 内容
    ''' </summary>
    Public Const CONTENT As String = "CONTENT"

    ''' <summary>
    ''' コメントのマーカー
    ''' </summary>
    Public Const CommentMarker As Char = "#"c

    ''' <summary>
    ''' 統合のマーカー
    ''' </summary>
    Public Const EqualMarker As Char = "="c

    ''' <summary>
    ''' ファイル名の列番号
    ''' </summary>
    Protected Const POS_FILENAME As Integer = 0

    ''' <summary>
    ''' 辞書種類の列番号
    ''' </summary>
    Protected Const POS_KIND As Integer = 1

    ''' <summary>
    ''' 辞書説明の列番号
    ''' </summary>
    Protected Const POS_DESC As Integer = 2

#End Region

    ''' <summary>
    ''' データを格納するWdicDataクラス
    ''' </summary>
    Protected _EnvironmentData As EnvironmentData

    ''' <summary>
    ''' wlf_filesテーブルに格納するID
    ''' </summary>
    ''' <remarks></remarks>
    Protected WlfFileId As Integer


#End Region

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="id">wlf_filesテーブル主キー</param>
    ''' <param name="filename">読み取るWLFファイル</param>
    ''' <param name="env">読み取り結果を格納するためのWdicDataクラス</param>
    Public Sub New( _
        ByVal id As Integer, _
        ByVal filename As String, _
        ByRef env As EnvironmentData)

        MyBase.New(filename)

        If env Is Nothing Then Throw New System.ArgumentNullException()
        Me.WlfFileId = id
        Me._EnvironmentData = env

    End Sub

    ''' <summary>
    ''' WLFファイルの読み込みとDataSetへの格納
    ''' </summary>
    Public Sub Parse()
        If ReadText Is Nothing Then Read()

        ' セクション切り出し
        Dim gpos, fpos As Integer
        gpos = ReadText.IndexOf(GroupSection)
        fpos = ReadText.IndexOf(FilesSection)
        gpos += GroupSection.Length
        Dim group As String = ReadText.Substring(gpos + 2, fpos - gpos - 3) ' CRLFの分だけ文字数を加減しなくてはいけない
        fpos += FilesSection.Length
        Dim files As String = ReadText.Substring(fpos + 2)

        ' [GROUP]セクション から GroupListTableに格納
        ParseGroups(group)

        ' [Files]セクション から FileListTableに格納
        ParseFiles(files)

    End Sub

    ''' <summary>
    ''' [GROUP]セクション部分を解析する
    ''' </summary>
    ''' <param name="group">[GROUP]セクション文字列</param>
    Private Sub ParseGroups(ByVal group As String)

        Dim dt As DataTable = _EnvironmentData.WlfFileTable()
        Dim drs() As DataRow = dt.Select(String.Format("{0}='{1}'", "id", WlfFileId))
        If Not drs.Length = 1 Then
            Throw New InvalidWlfFileException()
        End If

        For Each line As String In Split(group, vbCrLf)

            If String.IsNullOrEmpty(line) Then Continue For
            If line(1) = CommentMarker Then Continue For

            ' 行を分解して変数名/値を取得する
            line = line.Trim()
            Dim b() As String = Split(line, EqualMarker)
            If b.Length < 1 Then Throw New InvalidWlfFileException
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
                If c.Length > 0 Then drs(0).Item("email") = c(0).Trim
                If c.Length > 1 Then drs(0).Item("contacter") = c(1).Trim
            ElseIf element = NAME Then
                drs(0).Item("name") = value
            ElseIf element = CONTENT Then
                drs(0).Item("content") = value
            End If
        Next

    End Sub

    ''' <summary>
    ''' [FILES]セクション部分を解析する
    ''' </summary>
    ''' <param name="files">[FILES]セクション文字列</param>
    Private Sub ParseFiles(ByVal files As String)

        Dim dt As DataTable = _EnvironmentData.DicFileTable

        ' dicfilesテーブルの最新のidを取得する
        Dim id As Integer = 0
        If dt.Rows.Count > 0 Then
            id = CInt(dt.Compute("Max(id)", Nothing)) + 1
        End If

        ' 値格納
        For Each line As String In Split(files, vbCrLf)
            line = line.Trim()
            If line.Length = 0 Then Continue For
            Dim items() As String = Split(line, vbTab)
            Dim dr As DataRow = dt.NewRow
            dr.Item("id") = id : id += 1
            dr.Item("wlf_file_id") = WlfFileId
            dr.Item("filename") = items(POS_FILENAME)
            dr.Item("kind") = items(POS_KIND)
            dr.Item("description") = items(POS_DESC)
            dt.Rows.Add(dr)
        Next

    End Sub

End Class
