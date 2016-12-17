''' <summary>
''' 検索条件を格納するクラス
''' </summary>
''' <remarks></remarks>
Public Class SearchConditionData

#Region "定義"

    ''' <summary>
    ''' 検索方法
    ''' </summary>
    Protected Mode As SearchModeType

    ''' <summary>
    ''' 検索方法の種類
    ''' </summary>
    Public Enum SearchModeType
        Prefix
        Suffix
        Include
        Perfect
        Regex
    End Enum

    ''' <summary>
    ''' テキスト部分の検索を行うかどうかのフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Protected TextSearch As Boolean

    ''' <summary>
    ''' 大文字小文字の区別を行うかどうかのフラグ
    ''' </summary>
    Protected CapitalCheck As Boolean

    ''' <summary>
    ''' 検索文字
    ''' </summary>
    Protected Word As String

    ''' <summary>
    ''' 検索する辞書の種類
    ''' </summary>
    ''' <remarks>空配列の時は全辞書を指定したのと同じ扱い</remarks>
    Protected Dic As Integer()

    ''' <summary>
    ''' 検索するカテゴリの種類
    ''' </summary>
    ''' <remarks></remarks>
    Protected Category As String()

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        Mode = SearchModeType.Perfect
        TextSearch = False
        CapitalCheck = True
        Word = ""
        Dic = New Integer() {}
        Category = New String() {}
    End Sub

#End Region

#Region "プロパティ"

    ''' <summary>
    ''' 検索モードの指定
    ''' </summary>
    ''' <value>設定する検索モード</value>
    ''' <returns>設定されている検索モード</returns>
    ''' <remarks>SearchModeTypeを指定</remarks>
    Public Property SearchMode() As SearchModeType
        Get
            Return Mode
        End Get
        Set(ByVal value As SearchModeType)
            Mode = value
        End Set
    End Property

    ''' <summary>
    ''' 本文部分も検索するかどうかを指定
    ''' </summary>
    ''' <value>検索範囲の指定</value>
    ''' <returns>指定されている検索範囲</returns>
    ''' <remarks>Trueの場合本文も検索する、Falseの場合は単語のみ</remarks>
    Public Property IsTextSearch() As Boolean
        Get
            Return TextSearch
        End Get
        Set(ByVal value As Boolean)
            TextSearch = value
        End Set
    End Property

    ''' <summary>
    ''' 大文字小文字の区別をするかどうかを指定
    ''' </summary>
    ''' <value>大文字小文字区別の指定</value>
    ''' <returns>指定されている大文字小文字の区別</returns>
    ''' <remarks>Trueの時大文字小文字の区別を行う、Falseの場合は行わない</remarks>
    Public Property IsCapitalCheck() As Boolean
        Get
            Return CapitalCheck
        End Get
        Set(ByVal value As Boolean)
            CapitalCheck = value
        End Set
    End Property

    ''' <summary>
    ''' 検索を行う単語の指定
    ''' </summary>
    ''' <value>検索を行う単語の指定</value>
    ''' <returns>現在指定されている単語</returns>
    Public Property SearchWord() As String
        Get
            Return Word
        End Get
        Set(ByVal value As String)
            Word = value
        End Set
    End Property

    ''' <summary>
    ''' 検索する辞書の種類の指定
    ''' </summary>
    ''' <value>検索する辞書の種類のWLF no</value>
    ''' <returns>現在指定されている辞書の種類のWLF no</returns>
    ''' <remarks>配列で指定、指令がない場合は全辞書選択</remarks>
    Public Property SearchDictonary() As Integer()
        Get
            Return Dic
        End Get
        Set(ByVal value As Integer())
            Dic = value
        End Set
    End Property

    ''' <summary>
    ''' 検索するカテゴリの種類の指定
    ''' </summary>
    ''' <value>検索するカテゴリの種類</value>
    ''' <returns>現在指定されている辞書の種類の一覧</returns>
    ''' <remarks>配列で指定、指定がない場合は全カテゴリ選択</remarks>
    Public Property SearchCategory() As String()
        Get
            Return Category
        End Get
        Set(ByVal value As String())
            Category = value
        End Set
    End Property

#End Region

End Class
