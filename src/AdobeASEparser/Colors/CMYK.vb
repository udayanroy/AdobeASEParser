



Public Class CMYK
    Inherits ColorBlock

    Public Sub New()
        ColorModel = ColorModel.CMYK
    End Sub

    Public Property C As Single
    Public Property M As Single
    Public Property Y As Single
    Public Property K As Single

    Public Overrides Function GetColor() As sRGB
        Dim cl As New AdobeRGB
        cl.B = (1 - Y) * (1 - K)
        cl.G = (1 - M) * (1 - K)
        cl.R = (1 - C) * (1 - K)
        Return cl.GetColor()
    End Function
End Class
