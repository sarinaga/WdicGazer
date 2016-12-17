#Region "単語クラス"

''' <summary>
''' 単語の解析情報を保持するクラス
''' </summary>
''' <remarks></remarks>
Public Class WordData

#Region "定義"

    ''' <summary>
    ''' 単語
    ''' </summary>
    Public Word As String

    ''' <summary>
    ''' ファイル番号
    ''' </summary>
    Public FileNo As Integer

    ''' <summary>
    ''' グループ番号
    ''' </summary>
    Public GroupNo As Integer

    ''' <summary>
    ''' 辞書種類
    ''' </summary>
    Public Type As String

    ''' <summary>
    ''' ファイル名
    ''' </summary>
    Public Filename As String

    ''' <summary>
    ''' フルパス
    ''' </summary>
    Public Fullpath As String

    ''' <summary>
    ''' 文字位置
    ''' </summary>
    Public Pos As Integer

    ''' <summary>
    ''' ヘッダ部分データ
    ''' </summary>
    Public HeaderItems As ArrayList

    ''' <summary>
    ''' 本文部分データ
    ''' </summary>
    Public BodyItems As ArrayList

#End Region

#Region "コンストラクタ"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        HeaderItems = New ArrayList
        BodyItems = New ArrayList
    End Sub

#End Region

End Class

#End Region

#Region "単語要素クラス"

Namespace WordItem

    ''' <summary>
    ''' 単語要素の基底クラス
    ''' </summary>
    Class WordItemBase

        ''' <summary>
        ''' 構文解析後テキスト
        ''' </summary>
        ''' <remarks>場合によってはこの項目はNothingのことがある</remarks>
        Public Data As String

        ''' <summary>
        ''' 構文解析前テキスト
        ''' </summary>
        Public Text As String

        ''' <summary>
        ''' 階層の深さ
        ''' </summary>
        Public Level As Integer

    End Class

#Region "ヘッダ部分の項目"

    ''' <summary>
    ''' 未対応の項目 (本文でも利用)
    ''' </summary>
    Class UndefinedItem
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' 読み方
    ''' </summary>
    Class Yomi
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' 歴史的読み方
    ''' </summary>
    Class OldYomi
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' 外語
    ''' </summary>
    Class Spell
        Inherits WordItemBase

        ''' <summary>
        ''' 言語コード(2文字)
        ''' </summary>
        ''' <remarks>存在しないときはNothing</remarks>
        Public LangCode2 As String

        ''' <summary>
        ''' 言語コード(3文字)
        ''' </summary>
        ''' <remarks>存在しないときはNothing</remarks>
        Public LangCode3 As String

        ''' <summary>
        ''' 国名コード(2文字または3文字)
        ''' </summary>
        ''' <remarks>存在しないときはNothing</remarks>
        Public CountryCode As String

        ''' <summary>
        ''' 表記
        ''' </summary>
        Public Spell As String

        ''' <summary>
        ''' 略語、頭字語
        ''' </summary>
        ''' <remarks>略語が存在しない場合はNothing</remarks>
        Public Abbr As String

    End Class

    ''' <summary>
    ''' 発音
    ''' </summary>
    Class Pron
        Inherits WordItemBase

        ''' <summary>
        ''' 言語コード(2文字)
        ''' </summary>
        ''' <remarks>存在しないときはNothing</remarks>
        Public LangCode2 As String

        ''' <summary>
        ''' 言語コード(3文字)
        ''' </summary>
        ''' <remarks>存在しないときはNothing</remarks>
        Public LangCode3 As String

        ''' <summary>
        ''' 国名コード(2文字または3文字)
        ''' </summary>
        ''' <remarks>存在しないときはNothing</remarks>
        Public CountryCode As String

        ''' <summary>
        ''' 発音表記
        ''' </summary>
        Public Spell As String

    End Class

    ''' <summary>
    ''' ディレクトリ(カテゴリ)
    ''' </summary>
    Class Dir
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' 品詞
    ''' </summary>
    Class Pos
        Inherits WordItemBase

        ''' <summary>
        ''' 複数記述されている品詞のリスト
        ''' </summary>
        Public Items As String()

    End Class

    ''' <summary>
    ''' 実装依存の処理フラグ(本文でも利用)
    ''' </summary>
    Class Flag
        Inherits WordItemBase

        ''' <summary>
        ''' 複数記述されているフラグのリスト
        ''' </summary>
        Public Items As String()

    End Class

    ''' <summary>
    ''' 更新履歴、筆者情報(本文でも利用)
    ''' </summary>
    Class Author
        Inherits WordItemBase

        ''' <summary>
        ''' 筆者名
        ''' </summary>
        Public AuthorName As String

        ''' <summary>
        ''' 記述日
        ''' </summary>
        Public WrittenDate As DateTime

        ''' <summary>
        ''' 更新区分
        ''' </summary>
        ''' <remarks>A=新規, R=更新, I=挿入</remarks>
        Public Type As Char

        ''' <summary>
        ''' 出典、引用元
        ''' </summary>
        ''' <remarks>存在しないときはNothing</remarks>
        Public Source As String

    End Class

    ''' <summary>
    ''' 原稿内容の有効期限(本文でも利用)
    ''' </summary>
    Class Valid
        Inherits WordItemBase

        ''' <summary>
        ''' 有効期限
        ''' </summary>
        Public ValidDate As DateTime

    End Class

    ''' <summary>
    ''' 有効となる最終日付(本文でも利用)
    ''' </summary>
    Class Expire
        Inherits WordItemBase

        ''' <summary>
        ''' 最終日付
        ''' </summary>
        Public ExpireDate As DateTime

    End Class

#End Region

#Region "本文部分の項目"

    ''' <summary>
    ''' 章要素
    ''' </summary>
    Class ChapterHeader
        Inherits WordItemBase

        ''' <summary>
        ''' ヘッダ
        ''' </summary>
        Public Items As ArrayList

    End Class


    ''' <summary>
    ''' 見出し
    ''' </summary>
    Class Heading
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' 段落
    ''' </summary>
    Class Paragraph
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' テーブル
    ''' </summary>
    Class Table
        Inherits WordItemBase

        ''' <summary>
        ''' テーブルのマトリックス
        ''' </summary>
        Public Cells(,) As TableCell

    End Class

    ''' <summary>
    ''' テーブルのセルの内容
    ''' </summary>
    Structure TableCell

        ''' <summary>
        ''' ヘッダセルかどうか
        ''' </summary>
        Public IsHeader As Boolean

        ''' <summary>
        ''' セルの文字列
        ''' </summary>
        Public Text As String

        ''' <summary>
        ''' 配置
        ''' </summary>
        ''' <remarks>-1 左寄せ、0 センタリング、1 右寄せ</remarks>
        Public Align As Integer

        ''' <summary>
        ''' 列連結
        ''' </summary>
        ''' <remarks></remarks>
        Public ColSpan As Integer

        ''' <summary>
        ''' 行連結
        ''' </summary>
        ''' <remarks></remarks>
        Public RowSpan As Integer

        ''' <summary>
        ''' RowSpan，ColSpan等で無効になっているセルであるかどうか
        ''' </summary>
        Public IsEmpty As Boolean

    End Structure

    ''' <summary>
    ''' 見出しつきリスト
    ''' </summary>
    Class DefList
        Inherits WordItemBase

        ''' <summary>
        ''' キャプション
        ''' </summary>
        Public Caption As String

        ''' <summary>
        ''' リストの内容
        ''' </summary>
        Public Items() As DefItem

    End Class

    ''' <summary>
    ''' 見出しつきリストの内容
    ''' </summary>
    Structure DefItem

        ''' <summary>
        ''' 見出しかどうか
        ''' </summary>
        Public IsHeading As Boolean

        ''' <summary>
        '''リストの内容
        ''' </summary>
        ''' <remarks>子要素が存在するときはNothing(排他利用)</remarks>
        Public Text As String

        ''' <summary>
        ''' 子要素
        ''' </summary>
        ''' <remarks>リストの内容が存在するときはNothing(排他利用)</remarks>
        Public ChildItems As ArrayList

    End Structure

    ''' <summary>
    ''' 箇条書き基底クラス
    ''' </summary>
    Class ListBase
        Inherits WordItemBase

        ''' <summary>
        ''' リストの内容
        ''' </summary>
        Public Items As ListItem()

    End Class

    ''' <summary>
    ''' 番号付き箇条書き
    ''' </summary>
    Class OrderedList
        Inherits ListBase
    End Class

    ''' <summary>
    ''' 番号なし箇条書き
    ''' </summary>
    Class UnorderedList
        Inherits ListBase
    End Class

    ''' <summary>
    ''' 箇条書きの内容
    ''' </summary>
    ''' <remarks></remarks>
    Class ListItem

        ''' <summary>
        ''' 箇条書きの内容
        ''' </summary>
        Public Text As String

        ''' <summary>
        ''' 子要素
        ''' </summary>
        ''' <remarks>子要素がない場合はNothing</remarks>
        Public ChildItem As WordItemBase

        ''' <summary>
        ''' 箇条書きの形式
        ''' </summary>
        ''' <remarks></remarks>
        Public Type As String

    End Class

    ''' <summary>
    ''' 転送
    ''' </summary>
    Class Trancefer
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' リンクリスト開始
    ''' </summary>
    Class Linker
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' コメント(表示されない)
    ''' </summary>
    Class Comment
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' 整形済テキスト
    ''' </summary>
    Class Pre
        Inherits WordItemBase
    End Class

    ''' <summary>
    ''' 引用部分
    ''' </summary>
    Class Quote
        Inherits WordItemBase
    End Class

    Class PluginBase
        Inherits WordItemBase
        Protected _FullFilename As String
        Public ReadOnly Property Ext() As String
            Get
                Return System.IO.Path.GetExtension(_FullFilename)
            End Get
        End Property
        Public Property FullFilename() As String
            Get
                Return _FullFilename
            End Get
            Set(ByVal value As String)
                _FullFilename = value
            End Set
        End Property
        Public ReadOnly Property Filename() As String
            Get
                Return System.IO.Path.GetFileName(_FullFilename)
            End Get
        End Property
        Public ReadOnly Property url() As String
            Get
                Return "file:///" & _FullFilename.Replace("\", "/")
            End Get
        End Property
    End Class
    Class Graphic
        Inherits PluginBase

        Private _Image As Image
        Private _Float As Integer
        Private _Thumb As Integer

        Public Sub New()
            _Image = Nothing
            _Float = 0
            _Thumb = -1
        End Sub
        Public Sub New(ByVal filename As String)
            Me.New()
            _FullFilename = filename
        End Sub
        Public Sub ReadGraphData(Optional ByVal force As Boolean = False)
            If Not _Image Is Nothing And Not force Then Exit Sub
            _Image = System.Drawing.Image.FromFile(_FullFilename)
            If _Thumb < 0 Then _Thumb = _Image.Width
        End Sub
        Public ReadOnly Property Size() As Size
            Get
                ReadGraphData()
                Return _Image.Size()
            End Get
        End Property
        Public Property Float() As Integer
            Get
                Return _Float
            End Get
            Set(ByVal value As Integer)
                If value = 0 Then
                    _Float = 0
                ElseIf value < 0 Then
                    _Float = -1
                Else
                    _Float = 1
                End If
            End Set
        End Property
        Public ReadOnly Property Image() As Image
            Get
                ' -1 左回り、0 回り込みはなく中央寄せ、1 右回り
                ReadGraphData()
                Return _Image
            End Get
        End Property
        Public Property Thumb() As Integer
            Get
                Return _Thumb
            End Get
            Set(ByVal value As Integer)
                _Thumb = value
            End Set
        End Property
        Public WriteOnly Property ThumbPercent() As Integer
            Set(ByVal value As Integer)
                ReadGraphData()
                _Thumb = CInt(_Image.Width * value / 100)
            End Set
        End Property
        Public ReadOnly Property Tall() As Integer
            Get
                ReadGraphData()
                Return CInt(_Thumb / _Image.Width * _Image.Height)
            End Get
        End Property
    End Class
    Class Plugin
        Inherits PluginBase
    End Class

#End Region

End Namespace

#End Region

