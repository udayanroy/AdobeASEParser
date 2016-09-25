


Public Class XYZ
    Public Property X As Single
    Public Property Y As Single
    Public Property Z As Single

    Public Function ToRgb() As sRGB

        Dim X1 = X / 100.0
        Dim Y1 = Y / 100.0
        Dim Z1 = Z / 100.0

        Dim r = X1 * 3.2406 + Y1 * -1.5372 + Z1 * -0.4986
        Dim g = X1 * -0.9689 + Y1 * 1.8758 + Z1 * 0.0415
        Dim b = X1 * 0.0557 + Y1 * -0.204 + Z1 * 1.057


        Dim r1 = IIf(r > 0.0031308, 1.055 * Math.Pow(r, 1 / 2.4) - 0.055, 12.92 * r)
        Dim g1 = IIf(g > 0.0031308, 1.055 * Math.Pow(g, 1 / 2.4) - 0.055, 12.92 * g)
        Dim b1 = IIf(b > 0.0031308, 1.055 * Math.Pow(b, 1 / 2.4) - 0.055, 12.92 * b)

        Dim cl As New sRGB

        cl.R = FixRGB(r1)
        cl.G = FixRGB(g1)
        cl.B = FixRGB(b1)
        Return cl
    End Function
    Private Shared Function FixRGB(n As Double) As Double

        If (n < 0) Then
            Return 0
        End If
        If (n > 1) Then
            Return 1
        End If

        Return n
    End Function

    Public Shared Function WhiteReference() As XYZ
        Dim white As New XYZ
        white.X = 95.047 '95.047
        white.Y = 100.0 '100.0
        white.Z = 108.883 '108.883
        Return white
    End Function
End Class
