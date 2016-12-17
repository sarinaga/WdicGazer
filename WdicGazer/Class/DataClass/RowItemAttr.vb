''' <summary>
''' 検索結果の一覧表示で出力される項目を格納するためのクラス
''' </summary>
Class RowItemAttr

    ''' <summary>
    ''' 列コード名
    ''' </summary>
    ''' <remarks></remarks>
    Public Code As String

    ''' <summary>
    ''' 表示される列名
    ''' </summary>
    Public Name As String

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="code">列コード名</param>
    ''' <param name="name">表示される列名</param>
    Public Sub New(ByVal code As String, ByVal name As String)
        Me.Code = code
        Me.Name = name
    End Sub

    ''' <summary>
    ''' 文字列化
    ''' </summary>
    ''' <returns>文字列化されたRowItemArrt</returns>
    ''' <remarks>実際には表示される列名だけが返る</remarks>
    Public Overrides Function ToString() As String
        Return Name
    End Function

End Class
