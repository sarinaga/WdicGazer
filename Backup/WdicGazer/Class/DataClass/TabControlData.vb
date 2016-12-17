''' <summary>
''' タブの中のコントロール一覧と表示するデータ
''' </summary>
Class TabControlData

#Region "パネルコントロール"

    ''' <summary>
    ''' ヘッダ部分パネル
    ''' </summary>
    Public Panel_Header As Panel

#End Region

#Region "ラベルコントロール"

    ''' <summary>
    ''' 単語ラベル
    ''' </summary>
    Public Label_Word As Label

    ''' <summary>
    ''' 日本語読みラベル
    ''' </summary>
    Public Label_ReadJapanese As Label

    ''' <summary>
    ''' 別名ラベル
    ''' </summary>
    Public Label_OtherName As Label

    ''' <summary>
    ''' 更新履歴ラベル
    ''' </summary>
    Public Label_History As Label

    ''' <summary>
    ''' 英語表記ラベル
    ''' </summary>
    Public Label_English As Label

    ''' <summary>
    ''' 略語ラベル
    ''' </summary>
    Public Label_Abbr As Label

    ''' <summary>
    ''' 発音ラベル
    ''' </summary>
    Public Label_Pron As Label

    ''' <summary>
    ''' 品詞ラベル
    ''' </summary>
    ''' <remarks></remarks>
    Public Label_Pos As Label

    ''' <summary>
    ''' 辞書種類ラベル
    ''' </summary>
    Public Label_Dictonary As Label

    ''' <summary>
    ''' カテゴリラベル
    ''' </summary>
    Public Label_Category As Label

#End Region

#Region "表示コントロール"

    ''' <summary>
    ''' 単語
    ''' </summary>
    Public TextBox_Word As TextBox

    ''' <summary>
    ''' 日本語表記
    ''' </summary>
    Public ListBox_ReadJapanese As ListBox

    ''' <summary>
    ''' 別名
    ''' </summary>
    Public ListBox_OtherName As ListBox

    ''' <summary>
    ''' 更新履歴
    ''' </summary>
    Public ListBox_History As ListBox

    ''' <summary>
    ''' 外国語
    ''' </summary>
    Public ListBox_English As ListBox

    ''' <summary>
    ''' 略語
    ''' </summary>
    ''' <remarks></remarks>
    Public ListBox_Abbr As ListBox

    ''' <summary>
    ''' 発音
    ''' </summary>
    Public ListBox_Pron As ListBox

    ''' <summary>
    ''' 品詞
    ''' </summary>
    ''' <remarks></remarks>
    Public TextBox_Pos As TextBox

    ''' <summary>
    ''' 辞書種類
    ''' </summary>
    Public TextBox_Dictonary As TextBox

    ''' <summary>
    ''' カテゴリ
    ''' </summary>
    Public ListBox_Category As ListBox

    ''' <summary>
    ''' 本文表示部
    ''' </summary>
    ''' <remarks></remarks>
    Public Discription_Display As ExWebBrowser

#End Region

#Region "展開前データ"

    ''' <summary>
    ''' タブ内に表示する単語のデータ
    ''' </summary>
    Public Word As WordData

#End Region

#Region "初期化等"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()

        ' パネルの初期化
        Panel_Header = New Panel()

        ' ラベルの初期化
        Label_Word = New Label()
        Label_ReadJapanese = New Label()
        Label_OtherName = New Label()
        Label_History = New Label()
        Label_English = New Label()
        Label_Abbr = New Label()
        Label_Pron = New Label()
        Label_Pos = New Label()
        Label_Dictonary = New Label()
        Label_Category = New Label()

        ' 表示コントロールの初期化
        TextBox_Word = New TextBox()
        ListBox_ReadJapanese = New ListBox()
        ListBox_OtherName = New ListBox()
        ListBox_History = New ListBox()
        ListBox_English = New ListBox()
        ListBox_Abbr = New ListBox()
        ListBox_Pron = New ListBox()
        TextBox_Pos = New TextBox()
        TextBox_Dictonary = New TextBox()
        ListBox_Category = New ListBox
        Discription_Display = New ExWebBrowser()

    End Sub

    ''' <summary>
    ''' アイテム破棄
    ''' </summary>
    Public Sub Dispose()

        ' パネルの破棄
        Panel_Header.Dispose()

        ' ラベルの破棄
        Label_Word.Dispose()
        Label_ReadJapanese.Dispose()
        Label_OtherName.Dispose()
        Label_History.Dispose()
        Label_English.Dispose()
        Label_Abbr.Dispose()
        Label_Pron.Dispose()
        Label_Pos.Dispose()
        Label_Dictonary.Dispose()
        Label_Category.Dispose()

        ' コントロールの破棄
        TextBox_Word.Dispose()
        ListBox_ReadJapanese.Dispose()
        ListBox_OtherName.Dispose()
        ListBox_History.Dispose()
        ListBox_English.Dispose()
        ListBox_Abbr.Dispose()
        ListBox_Pron.Dispose()
        TextBox_Pos.Dispose()
        TextBox_Dictonary.Dispose()
        ListBox_Category.Dispose()
        Discription_Display.Dispose()

    End Sub

    ''' <summary>
    ''' コントロールの内容の初期化
    ''' </summary>
    Public Sub Clear()

        TextBox_Word.Text = ""
        ListBox_ReadJapanese.Items.Clear()
        ListBox_OtherName.Items.Clear()
        ListBox_History.Items.Clear()
        ListBox_English.Items.Clear()
        ListBox_Abbr.Items.Clear()
        ListBox_Pron.Items.Clear()
        TextBox_Pos.Text = ""
        TextBox_Dictonary.Text = ""
        ListBox_Category.Items.Clear()
        Discription_Display.Navigate("about:blank")
        Discription_Display.DocumentText = ""

    End Sub

#End Region

End Class
