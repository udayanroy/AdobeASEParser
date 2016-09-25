


Public Class Gray
    Inherits ColorBlock

    Public Sub New()
        ColorModel = ColorModel.GRAY
    End Sub

    Public Property G As Single

    Public Overrides Function GetColor() As sRGB
        Dim cl As New AdobeRGB

        cl.B = G
        cl.G = G
        cl.R = G
        Return cl.GetColor
    End Function
End Class
