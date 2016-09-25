
Public Class AdobeRGB
    Inherits ColorBlock


    Public Sub New()
        ColorModel = ColorModel.RGB
    End Sub

    Public Property R As Single
    Public Property G As Single
    Public Property B As Single

    Public Overrides Function GetColor() As sRGB
        Dim xyz = ToXYZ()
        Return xyz.ToRgb
    End Function

    Public Function ToXYZ() As XYZ
        Dim R1 = (R) ^ 2.19921875
        Dim G1 = (G) ^ 2.19921875
        Dim B1 = (B) ^ 2.19921875

        Dim xyz As New XYZ

        ' Observer. = 2°, Illuminant = D65
        xyz.X = (0.57667 * R1 + 0.18556 * G1 + 0.18823 * B1) * 100
        xyz.Y = (0.29734 * R1 + 0.62736 * G1 + 0.07529 * B1) * 100
        xyz.Z = (0.02703 * R1 + 0.07069 * G1 + 0.99134 * B1) * 100
        Return xyz
    End Function

    Public Shared Function FromSRGB(srgb As sRGB) As AdobeRGB

        Dim R = srgb.R '/ 255
        Dim G = srgb.G '/ 255
        Dim B = srgb.B '/ 255

        ' Apply gamma correction (i.e. conversion to linear-space)
        If (R > 0.04045) Then
            R = ((R + 0.055) / 1.055) ^ 2.4
        Else
            R = R / 12.92
        End If

        If (G > 0.04045) Then
            G = ((G + 0.055) / 1.055) ^ 2.4
        Else
            G = G / 12.92
        End If

        If (B > 0.04045) Then
            B = ((B + 0.055) / 1.055) ^ 2.4
        Else
            B = B / 12.92
        End If


        ' Observer. = 2°, Illuminant = D65
        Dim X = R * 0.4124 + G * 0.3576 + B * 0.1805
        Dim Y = R * 0.2126 + G * 0.7152 + B * 0.0722
        Dim Z = R * 0.0193 + G * 0.1192 + B * 0.9505


        ' 
        'xyz to Adobe RGB
        Dim AdobeR = 2.04159 * X - 0.56501 * Y - 0.34473 * Z
        Dim AdobeG = -0.96924 * X + 1.87597 * Y + 0.04156 * Z
        Dim AdobeB = 0.01344 * X - 0.11836 * Y + 1.01517 * Z

        ' Gamma correction
        AdobeR = AdobeR ^ (1.0 / 2.19921875)
        AdobeG = AdobeG ^ (1.0 / 2.19921875)
        AdobeB = AdobeB ^ (1.0 / 2.19921875)

        Dim adobeRgb As New AdobeRGB
        adobeRgb.R = fixParam(AdobeR)
        adobeRgb.G = fixParam(AdobeG)
        adobeRgb.B = fixParam(AdobeB)

        Return adobeRgb
    End Function

    Private Shared Function fixParam(pm As Double) As Double
        If Double.IsNaN(pm) Then
            Return 0
        End If
        If pm < 0 Then
            Return 0
        End If
        If pm > 1 Then
            Return 1
        End If
        Return pm
    End Function

End Class
