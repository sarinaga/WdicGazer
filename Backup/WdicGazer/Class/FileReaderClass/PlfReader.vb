Public Class PlfReader
    Inherits FileReader


#Region "定数"


#End Region

#Region "メンバー変数"

    ''' <summary>
    ''' データを格納するWdicDataクラス
    ''' </summary>
    Protected _EnvironmentData As EnvironmentData

    ''' <summary>
    ''' wlf_filesテーブルに格納するID
    ''' </summary>
    ''' <remarks></remarks>
    Protected PlfFileId As Integer

#End Region

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="id">plf_filesテーブル主キー</param>
    ''' <param name="filename">読み取るPLFファイル</param>
    ''' <param name="env">読み取り結果を格納するためのWdicDataクラス</param>
    Public Sub New( _
        ByVal id As Integer, _
        ByVal filename As String, _
        ByRef env As EnvironmentData)

        MyBase.New(filename)

        If env Is Nothing Then Throw New System.ArgumentNullException()
        Me._EnvironmentData = env
        Me.PlfFileId = id

    End Sub


    ''' <summary>
    ''' WLFファイルの読み込みとDataSetへの格納
    ''' </summary>
    Public Sub Parse()
        If ReadText Is Nothing Then Read()
    End Sub

End Class


