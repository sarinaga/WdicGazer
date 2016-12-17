''' <summary>
''' Webbrowserのショートカットキーの調整を行ったもの
''' </summary>
''' <remarks>参考:http://homepage1.nifty.com/yasunari/VB/VB2005/WebBrowserShortcuts.htm</remarks>
Public Class ExWebBrowser
    Inherits WebBrowser

    Sub New()
        MyBase.new()
    End Sub

    ''' <summary>
    ''' キーボードショートカットの処理を変更するPreProcessMessage
    ''' </summary>
    Public Overrides Function PreProcessMessage(ByRef msg As System.Windows.Forms.Message) As Boolean
        Const WM_KEYDOWN As Integer = &H100
        If msg.Msg = WM_KEYDOWN Then
            Dim keyCode As Keys = CType(msg.WParam, Keys) And Keys.KeyCode
            If My.Computer.Keyboard.CtrlKeyDown Then
                Select Case keyCode
                    Case Keys.F, Keys.Q
                        Return MyBase.PreProcessMessage(msg)
                End Select
            End If
        End If
        Return True
    End Function
End Class