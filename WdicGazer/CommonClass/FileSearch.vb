''' <summary>
''' ファイルを再帰的に取得するクラス
''' </summary>
Public Class FileSearch

    ''' <summary>
    ''' 検索基準となるディレクトリ
    ''' </summary>
    Protected Base As String

    ''' <summary>
    ''' 検索されるファイル書式
    ''' </summary>
    Protected Pattern As String

    ''' <summary>
    ''' コンストラクタ(デスクトップを検索基準とし、すべての種類のファイルを取得)
    ''' </summary>
    Public Sub New()
        Base = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory)
        Pattern = "*.*"
    End Sub

    ''' <summary>
    ''' コンストラクタ(引数を検索基準とし、すべての種類のファイルを取得)
    ''' </summary>
    ''' <param name="directory">基準ディレクトリ</param>
    Public Sub New(ByVal directory As String)
        Me.Base = directory
        Me.Pattern = "*.*"
    End Sub

    ''' <summary>
    ''' コンストラクタ(検索基準、検索パターン両方指定)
    ''' </summary>
    ''' <param name="directory">基準ディレクトリ</param>
    ''' <param name="pattern">検索パターン</param>
    Public Sub New(ByVal directory As String, ByVal pattern As String)
        Me.Base = directory
        Me.Pattern = pattern
    End Sub


    ''' <summary>
    ''' ディレクトリを再起的に検索し、ファイル一覧を作成する
    ''' </summary>
    ''' <returns>検索されたファイルの一覧</returns>
    Public Function GetFile() As String()
        Return GetFilesMostDeep(Base, Pattern)
    End Function

    ''' <summary>
    ''' FileSearchの実際の処理を行う部分(再起呼び出し部分)
    ''' (参照) http://dobon.net/vb/dotnet/file/getfolderpath.html 
    ''' </summary>
    ''' <param name="stRootPath">基準ディレクトリ</param>
    ''' <param name="stPattern">検索パターン</param>
    ''' <returns>検索されたファイルの一覧</returns>
    Private Function GetFilesMostDeep(ByVal stRootPath As String, ByVal stPattern As String) As String()
        Dim hStringCollection As New System.Collections.Specialized.StringCollection()

        ' このディレクトリ内のすべてのファイルを検索する
        For Each stFilePath As String In System.IO.Directory.GetFiles(stRootPath, stPattern)
            hStringCollection.Add(stFilePath)
        Next

        ' このディレクトリ内のすべてのサブディレクトリを検索する (再帰)
        For Each stDirPath As String In System.IO.Directory.GetDirectories(stRootPath)
            Dim stFilePathes As String() = GetFilesMostDeep(stDirPath, stPattern)

            ' 条件に合致したファイルがあった場合は、ArrayList に加える
            If Not stFilePathes Is Nothing Then
                hStringCollection.AddRange(stFilePathes)
            End If
        Next

        ' StringCollection を 1 次元の String 配列にして返す
        Dim stReturns As String() = New String(hStringCollection.Count - 1) {}
        hStringCollection.CopyTo(stReturns, 0)

        Return stReturns
    End Function

End Class
