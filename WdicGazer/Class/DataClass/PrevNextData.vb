''' <summary>
''' 戻る・進むボタンのアイテム
''' </summary>
Public Class PrevNextData

#Region "定義"

    ''' <summary>
    ''' 前のアイテム
    ''' </summary>
    Private PrevItem As New ArrayList

    ''' <summary>
    ''' 次のアイテム
    ''' </summary>
    ''' <remarks></remarks>
    Private NextItem As New ArrayList

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 前の単語一覧を取得
    ''' </summary>
    ''' <returns>前の単語の一覧</returns>
    Public ReadOnly Property PrevWord() As String()
        Get
            Return CType(PrevItem.ToArray(GetType(String)), String())
        End Get
    End Property

    ''' <summary>
    ''' 前の単語を取得
    ''' </summary>
    ''' <param name="index">指定するインデックス</param>
    ''' <returns>前の単語</returns>
    Public ReadOnly Property PrevWord(ByVal index As Integer) As String
        Get
            Return CStr(PrevItem(index))
        End Get
    End Property

    ''' <summary>
    ''' 次の単語一覧を取得
    ''' </summary>
    ''' <returns>次の単語の一覧</returns>
    Public ReadOnly Property NextWord() As String()
        Get
            Return CType(NextItem.ToArray(GetType(String)), String())
        End Get
    End Property

    ''' <summary>
    ''' 次の単語を取得
    ''' </summary>
    ''' <param name="index">指定するインデックス</param>
    ''' <returns>次の単語</returns>
    Public ReadOnly Property NextWord(ByVal index As Integer) As String
        Get
            Return CStr(NextItem(index))
        End Get
    End Property

#End Region

#Region "単語操作"

    ''' <summary>
    ''' 前の単語を追加する
    ''' </summary>
    ''' <param name="add">追加する単語</param>
    Public Sub AddPrevWord(ByVal add As String)
        Dim index As Integer = PrevItem.IndexOf(add)
        If index < 0 Then
            PrevItem.Add(add)
        Else
            PrevItem.RemoveAt(index)
            PrevItem.Insert(0, add)
        End If
    End Sub

    ''' <summary>
    ''' 前に単語が存在するかどうかを確認する
    ''' </summary>
    ''' <param name="word">検索する単語</param>
    ''' <returns>検索結果</returns>
    Public Function IsHavePrevWord(ByVal word As String) As Boolean
        Return PrevItem.Contains(word)
    End Function

    ''' <summary>
    ''' 次に単語を追加する
    ''' </summary>
    ''' <param name="add">追加する単語</param>
    Public Sub AddNextWord(ByVal add As String)
        Dim index As Integer = NextItem.IndexOf(add)
        If index < 0 Then
            NextItem.Add(add)
        Else
            NextItem.RemoveAt(index)
            NextItem.Insert(0, add)
        End If
    End Sub

    ''' <summary>
    ''' 次に単語が存在するかどうかを確認する
    ''' </summary>
    ''' <param name="word">検索する単語</param>
    ''' <returns>検索結果</returns>
    Public Function IsHaveNextWord(ByVal word As String) As Boolean
        Return NextItem.Contains(word)
    End Function

#End Region

End Class
