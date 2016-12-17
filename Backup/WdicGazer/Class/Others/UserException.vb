#Region "例外"

''' <summary>
''' 処理が途中で中断されたときに発生する例外
''' </summary>
''' <remarks>WordSearcherClass専用</remarks>
Class CancelException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
    Public Sub New(ByVal msg As String, ByRef ex As Exception)
        MyBase.New(msg, ex)
    End Sub
End Class

''' <summary>
''' プログラムが正しく実装されているときには理論上発生しない処理の流れになったときに発生する例外
''' </summary>
Class UnjustProcessingException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
    Public Sub New(ByVal msg As String, ByRef ex As Exception)
        MyBase.New(msg, ex)
    End Sub
End Class

''' <summary>
''' WLFファイルの内容が不正な場合に発生する例外
''' </summary>
''' 
Class InvalidWlfFileException
        Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
    Public Sub New(ByVal msg As String, ByRef ex As Exception)
        MyBase.New(msg, ex)
    End Sub
End Class

''' <summary>
''' 辞書ファイル(*.DV6)が見つからない場合に発生する例外
''' </summary>
    Class NotFountDV6FileException
        Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
    Public Sub New(ByVal msg As String, ByRef ex As Exception)
        MyBase.New(msg, ex)
    End Sub
End Class

''' <summary>
''' 検索結果が所定の数を超えた場合に発生する例外
''' </summary>
Class RersultOverflowException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
    Public Sub New(ByVal msg As String, ByRef ex As Exception)
        MyBase.New(msg, ex)
    End Sub
End Class

''' <summary>
''' 単語の構文解析に失敗したときに発生する例外
''' </summary>
Public Class WordParseFaultException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
    Public Sub New(ByVal msg As String, ByRef ex As Exception)
        MyBase.New(msg, ex)
    End Sub
End Class

''' <summary>
''' 不正なディレクトリファイルのときに発生する例外
''' </summary>
Class InvalidDirFileException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
    Public Sub New(ByVal msg As String, ByRef ex As Exception)
        MyBase.New(msg, ex)
    End Sub
End Class

''' <summary>
''' FILE.GLが見つからなかったときに発生する例外
''' </summary>
Class NotFoundFileGroupException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
    Public Sub New(ByVal msg As String, ByRef ex As Exception)
        MyBase.New(msg, ex)
    End Sub
End Class

''' <summary>
''' FILE.GLが不正であった場合に発生する例外
''' </summary>
Class InvalidFileGroupException
    Inherits ApplicationException
    Public Sub New()
        MyBase.New()
    End Sub
    Public Sub New(ByVal msg As String)
        MyBase.New(msg)
    End Sub
    Public Sub New(ByVal msg As String, ByRef ex As Exception)
        MyBase.New(msg, ex)
    End Sub
End Class


#End Region
