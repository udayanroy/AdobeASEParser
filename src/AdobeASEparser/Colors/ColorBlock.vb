

Public Enum ColorModel
    RGB
    CMYK
    LAB
    GRAY
End Enum

Public MustInherit Class ColorBlock
    Public Property Name As String
    Public Property ColorModel As ColorModel
    Public MustOverride Function GetColor() As sRGB
    Public ReadOnly Property Color() As sRGB
        Get
            Return GetColor()
        End Get
    End Property

End Class
