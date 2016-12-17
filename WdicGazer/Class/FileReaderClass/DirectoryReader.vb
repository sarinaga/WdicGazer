''' <summary>
''' ディレクトリファイルを読み取るクラス
''' </summary>
Public Class DirectoryReader
    Inherits FileReader

#Region "定義"

    ''' <summary>
    ''' データを格納するWdicDataクラス
    ''' </summary>
    Protected Wdic As EnvironmentData

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="filename">DIR.LSTのフルパス</param>
    ''' <param name="wdic">読み取り結果を格納するためのWdicDataクラス</param>
    Public Sub New(ByVal filename As String, ByRef wdic As EnvironmentData)
        MyBase.New(filename)
        If wdic Is Nothing Then Throw New System.ArgumentNullException()
        Me.Wdic = wdic
    End Sub

#End Region

#Region "処理"

    ''' <summary>
    ''' DIR.LSTの読み込みとデータの格納
    ''' </summary>
    Public Sub Parse()
        If ReadText Is Nothing Then Read()

        Dim dt As DataTable = Wdic.DirectoryListTable
        Dim dr As DataRow

        Dim lines() As String = ReadText.Split(CChar(vbCr))
        For i As Integer = 0 To lines.Length - 1
            lines(i) = lines(i).Trim()
        Next

        Dim j As Integer = 0
        For Each line As String In lines
            If line = "" Then Continue For

            Dim b() As String = line.Split(CChar(vbTab))
            If b.Length < 2 Then Throw New InvalidDirFile()

            Dim category As String = b(b.Length - 1).Trim()
            dr = dt.NewRow()
            dr.Item(EnvironmentData.DirectoryListCol_DirNo) = j : j += 1
            dr.Item(EnvironmentData.DirectoryListCol_Directory) = b(0).Trim()
            dr.Item(EnvironmentData.DirectoryListCol_Category) = category
            If b.Length = 3 Then
                dr.Item(EnvironmentData.DirectoryListCol_Redirect) = b(1).Trim()
            Else
                dr.Item(EnvironmentData.DirectoryListCol_Redirect) = DBNull.Value
            End If
            dt.Rows.Add(dr)
        Next
    End Sub

#End Region

End Class
