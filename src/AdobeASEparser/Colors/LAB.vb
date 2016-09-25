

Public Class LAB
    Inherits ColorBlock

    Public Sub New()
        ColorModel = ColorModel.LAB
    End Sub
    Public L As Single
    Public A As Single
    Public B As Single

    Public Overrides Function GetColor() As sRGB
        Const Epsilon As Double = 0.008856
        Const Kappa As Double = 903.3

        Dim y = (L + 16.0) / 116.0
        Dim x = A / 500.0 + y
        Dim z = y - B / 200.0

        Dim white As XYZ = XYZ.WhiteReference()
        Dim x3 = x * x * x
        Dim z3 = z * z * z
        Dim xyzColor = New XYZ

        xyzColor.X = white.X * (IIf(x3 > Epsilon, x3, (x - 16.0 / 116.0) / 7.787))
        xyzColor.Y = white.Y * (IIf(L > (Kappa * Epsilon), Math.Pow(((L + 16.0) / 116.0), 3), L / Kappa))
        xyzColor.Z = white.Z * (IIf(z3 > Epsilon, z3, (z - 16.0 / 116.0) / 7.787))

        Return xyzColor.ToRgb()
    End Function


End Class
