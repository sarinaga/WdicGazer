Imports System.Xml

''' <summary>
''' ブックマークアイテム
''' </summary>
Public Class BookmarkItem

    ''' <summary>
    ''' アイテムの名前・兼単語名
    ''' </summary>
    ''' <remarks>ルートの場合はヌルストリング</remarks>
    Public Name As String

    ''' <summary>
    ''' 親アイテム
    ''' </summary>
    ''' <remarks>
    ''' 1. アイテムがディレクトリのとき、親ディレクトリを指定
    ''' 2. 自分自身がルートディレクトリの場合はNothing
    ''' 3. アイテムが単語のとき、Nothing
    ''' </remarks>
    Public Parents As BookmarkItem

    ''' <summary>
    ''' ディレクトリの中にあるアイテム一覧
    ''' </summary>
    ''' <remarks>
    ''' 1. アイテムがディレクトリの時のみ設定、単語のときはNothing
    ''' </remarks>
    Public Children As List(Of BookmarkItem)

    ''' <summary>
    ''' XML形式に変換する
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Serialize() As String

        Try

            ' XMLライタ準備
            Dim sw As New System.IO.StringWriter
            Dim xw As New XmlTextWriter(sw)
            xw.WriteStartDocument()
            xw.WriteWhitespace(vbCrLf)
            xw.WriteStartElement("bookmark")
            xw.WriteString(vbCrLf)

            ' 基点アイテム設定
            Dim now As BookmarkItem = Me
            Dim pos As Integer = 0

            ' ダミースタック設定
            Dim stack_dir As New Stack
            Dim stack_pos As New Stack
            stack_dir.Push(now)
            stack_pos.Push(pos)

            ' データ解析
            Do

                ' 最初から子要素のみの場合
                If now.Children Is Nothing Then
                    xw.WriteStartElement("item")
                    xw.WriteString(now.Name)
                    xw.WriteEndElement()
                    xw.WriteWhitespace(vbCrLf)
                    Exit Do
                End If

                ' 親要素の場合
                If pos = 0 Then
                    xw.WriteStartElement("directory")
                    xw.WriteStartAttribute("name")
                    xw.WriteString(now.Name)
                    xw.WriteEndAttribute()
                    xw.WriteString(vbCrLf)
                End If
                Dim complete As Boolean = True
                For i As Integer = pos To now.Children.Count - 1
                    Dim child As BookmarkItem = now.Children(i)
                    If child.Children Is Nothing Then
                        xw.WriteStartElement("item")
                        xw.WriteString(child.Name)
                        xw.WriteEndElement()
                        xw.WriteWhitespace(vbCrLf)
                    Else
                        pos = 0
                        stack_dir.Push(now)
                        stack_pos.Push(i)
                        now = child
                        complete = False
                        Exit For
                    End If
                Next

                ' ディレクトリの中身をすべて吐き出した場合
                If complete Then
                    xw.WriteEndElement()
                    xw.WriteWhitespace(vbCrLf)
                    now = CType(stack_dir.Pop, BookmarkItem)
                    pos = CInt(stack_pos.Pop()) + 1
                End If
                If stack_dir.Count = 0 Then Exit Do
            Loop

            xw.WriteEndElement()
            xw.WriteWhitespace(vbCrLf)
            xw.WriteEndDocument()
            xw.Close()
            Dim xml As String = sw.ToString
            Return xml

        Catch
            Return ""

        End Try

    End Function


    ''' <summary>
    ''' 与えられたXMLをBookmarkItemsに変換
    ''' </summary>
    ''' <param name="xml">解析されたXML</param>
    ''' <returns>解析されて得られたBookmarkItems</returns>
    ''' <remarks></remarks>
    Shared Function DeSerialize(ByVal xml As String) As BookmarkItem

        Try

            ' XML読み込み
            Dim document As New XmlDocument()
            document.LoadXml(xml)

            ' ルート要素取得
            Dim root_node As XmlElement = document.DocumentElement
            Dim now_node As XmlNode = root_node.ChildNodes(0)

            ' ルートアイテム作成
            Dim root As New BookmarkItem
            root.Children = New List(Of BookmarkItem)()
            root.Name = ""
            root.Parents = Nothing
            Dim now_item As BookmarkItem = root

            ' ダミースタック格納
            Dim stack_node As New Stack
            Dim stack_item As New Stack
            Dim stack_pos As New Stack
            Dim pos As Integer = 0
            stack_node.Push(now_node)
            stack_item.Push(now_item)
            stack_pos.Push(pos)

            Do

                Dim complete As Boolean = True
                For i As Integer = pos To now_node.ChildNodes.Count - 1

                    Dim child_node As XmlNode = now_node.ChildNodes(i)
                    If child_node.Name = "item" And child_node.NodeType = XmlNodeType.Element Then
                        Dim add_item As New BookmarkItem
                        add_item.Children = Nothing
                        add_item.Name = child_node.InnerText
                        add_item.Parents = now_item
                        now_item.Children.Add(add_item)

                    ElseIf child_node.Name = "directory" And child_node.NodeType = XmlNodeType.Element Then
                        Dim add_item As New BookmarkItem
                        add_item.Children = New List(Of BookmarkItem)
                        For Each attr As XmlAttribute In child_node.Attributes
                            If attr.Name = "name" Then add_item.Name = attr.Value
                        Next
                        add_item.Parents = now_item
                        stack_node.Push(now_node)
                        stack_item.Push(now_item)
                        stack_pos.Push(i)
                        now_node = CType(child_node, XmlElement)
                        now_item.Children.Add(add_item)
                        now_item = add_item
                        pos = 0
                        complete = False
                        Exit For
                    End If

                Next

                ' ディレクトリの中身をすべて吐き出した場合
                If complete Then
                    now_node = CType(stack_node.Pop, XmlElement)
                    now_item = CType(stack_item.Pop, BookmarkItem)
                    pos = CInt(stack_pos.Pop()) + 1
                End If
                If stack_node.Count = 0 Then Exit Do

            Loop
            Return root

        Catch
            Return Nothing

        End Try

    End Function

End Class

