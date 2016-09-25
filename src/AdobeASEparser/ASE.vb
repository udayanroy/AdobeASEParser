


Public Class ASE

    Private _blocks As List(Of ColorBlock)

    Private Sub New()

    End Sub

    Public ReadOnly Property Blocks As List(Of ColorBlock)
        Get
            Return _blocks
        End Get
    End Property
    Public Shared Function Decoade(filename As String) As ASE
        Dim ase As ASE
        Using stream = New IO.FileStream(filename, IO.FileMode.Open)
            ase = Decoade(stream)
        End Using
        Return ase
    End Function

    Public Shared Function Decoade(stream As IO.Stream) As ASE
        'Const BLOCK_TYPE_COLOR_ENTRY As Short = &HC001
        Const Color_Entry As Short = 1

        Dim ase As New ASE


        Using Reader As New BigEndianReader(stream)

            Dim header = Reader.ReadASCIIString(4)
            If header <> "ASEF" Then  ' &H41534546'1095976262
                Throw New Exception("no ASE file")
            End If

            Dim version_major = Reader.ReadInt16()
            Dim version_minor = Reader.ReadInt16()

            Dim numBlocks = Reader.ReadInt32()
            ase._blocks = New List(Of ColorBlock)(numBlocks)

            For blockIndex As Integer = 0 To numBlocks - 1
                Dim blockType = Reader.ReadInt16
                Dim blockLength = Reader.ReadInt32()

                If blockType = Color_Entry Then

                    Dim name = Reader.ReadString()
                    Dim colorModel = Reader.ReadASCIIString(4)

                    If colorModel = "RGB " Then
                        Dim rgb As New AdobeRGB
                        rgb.Name = name

                        rgb.R = Reader.ReadSingle()
                        rgb.G = Reader.ReadSingle()
                        rgb.B = Reader.ReadSingle()
                        'Add RGB block
                        ase.Blocks.Add(rgb)
                    ElseIf colorModel = "CMYK" Then
                        Dim cmyk As New CMYK
                        cmyk.Name = name

                        cmyk.C = Reader.ReadSingle()
                        cmyk.M = Reader.ReadSingle()
                        cmyk.Y = Reader.ReadSingle()
                        cmyk.K = Reader.ReadSingle()

                        'Add RGB block
                        ase.Blocks.Add(cmyk)
                    ElseIf colorModel = "LAB " Then
                        Dim lab As New LAB
                        lab.Name = name

                        lab.L = Reader.ReadSingle() * 100
                        lab.A = Reader.ReadSingle()
                        lab.B = Reader.ReadSingle()
                        'Add RGB block
                        ase.Blocks.Add(lab)
                    ElseIf colorModel = "Gray" Then
                        Dim gray As New Gray
                        gray.Name = name

                        gray.G = Reader.ReadSingle()
                        'Add RGB block
                        ase.Blocks.Add(gray)
                    Else
                        ' Debug.Assert(False)
                    End If
                    Dim colorType = Reader.ReadInt16()

                Else
                    Reader.ReadBytes(blockLength)
                    ' Console.WriteLine("color Entry: false")
                    ' Debug.Assert(True)
                End If

            Next



        End Using

        Return ase
    End Function
End Class
