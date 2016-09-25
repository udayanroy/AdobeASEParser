

Imports System.IO
Imports System.Text

Public Class BigEndianReader
    Inherits IO.BinaryReader

    Public Sub New(stream As Stream)
        MyBase.New(stream, Encoding.BigEndianUnicode)
    End Sub

    Public Overrides Function ReadInt32() As Integer
        Dim byts = MyBase.ReadBytes(4)
        Array.Reverse(byts)
        Return BitConverter.ToInt32(byts, 0)
    End Function

    Public Overrides Function ReadInt16() As Short
        Dim byts = MyBase.ReadBytes(2)
        Array.Reverse(byts)
        Return BitConverter.ToInt16(byts, 0)
    End Function

    Public Overrides Function ReadSingle() As Single
        Dim byts = MyBase.ReadBytes(4)
        Array.Reverse(byts)
        Return BitConverter.ToSingle(byts, 0)
    End Function

    Public Overrides Function ReadString() As String
        Dim length = ReadInt16()
        Return ReadBigEndianString(length * 2)
    End Function

    Public Function ReadBigEndianString(length As Integer) As String
        Dim byts = MyBase.ReadBytes(length)
        Dim Encoding As Encoding = Encoding.BigEndianUnicode
        Dim chrs = Encoding.GetChars(byts)
        Return New String(chrs)
    End Function

    Public Function ReadASCIIString(length As Integer) As String
        Dim byts = MyBase.ReadBytes(length)
        Dim encoader = New Text.ASCIIEncoding
        Dim chrs = encoader.GetChars(byts)
        Return New String(chrs)
    End Function
End Class
