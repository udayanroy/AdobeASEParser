
Public Class sRGB
    Inherits ColorBlock


    Public Sub New()
        ColorModel = ColorModel.RGB
    End Sub

    Public Property R As Single 'range 0 - 1
    Public Property G As Single 'range 0 - 1
    Public Property B As Single 'range 0 - 1

    Public Overrides Function GetColor() As sRGB
        Dim c As New sRGB
        c.B = B
        c.G = G
        c.R = R
        Return c
    End Function

End Class

