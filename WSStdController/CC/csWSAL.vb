Public Class ccWSAL


  Public Shared Function ParametersToBytes(ByVal vParameters As csTargCCParameterCol) As Byte()
    Dim pBytesToReturn As Byte() = Nothing

    Dim pLastReadVariableName As String = ""

    Using pMemoryStream As New System.IO.MemoryStream()
      Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8)

        Try
          For Each l In vParameters
            pLastReadVariableName = l.Name
            pBinaryWriter.Write(l.Name)
            pBinaryWriter.Write(l.DataType.ToString())
            If l.Value Is Nothing Then
              If l.DataType = enmDNVariableType.String Then
                l.Value = "" 'I can't handle null strings....
              Else
                Continue For
              End If
            End If
            Select Case l.DataType
              Case enmDNVariableType.Boolean
                pBinaryWriter.Write(DirectCast(l.Value, Boolean))
              Case enmDNVariableType.ByteArray
                Dim pBytes As Byte() = DirectCast(l.Value, Byte())
                pBinaryWriter.Write(pBytes.Length)
                pBinaryWriter.Write(pBytes, 0, pBytes.Length)
              Case enmDNVariableType.Date
                pBinaryWriter.Write(DirectCast(l.Value, DateTime).Ticks)
              Case enmDNVariableType.DateTimeOffset
                Dim pDate As DateTimeOffset = DirectCast(l.Value, DateTimeOffset)
                pBinaryWriter.Write(pDate.Ticks)
                pBinaryWriter.Write(pDate.Offset.Ticks)
              Case enmDNVariableType.Decimal
                pBinaryWriter.Write(DirectCast(l.Value, Decimal))
              Case enmDNVariableType.Double
                pBinaryWriter.Write(DirectCast(l.Value, Double))
              Case enmDNVariableType.Enum
                Dim pEnumType As String = l.Value.GetType().Name
                pBinaryWriter.Write(pEnumType)
                pBinaryWriter.Write(DirectCast(l.Value, [Enum]).ToString())
              Case enmDNVariableType.Integer
                pBinaryWriter.Write(DirectCast(l.Value, Integer))
              Case enmDNVariableType.Long
                pBinaryWriter.Write(DirectCast(l.Value, Long))
              Case enmDNVariableType.Single
                pBinaryWriter.Write(DirectCast(l.Value, Single))
              Case enmDNVariableType.String
                pBinaryWriter.Write(DirectCast(l.Value, String))
              Case Else
            End Select
          Next
        Catch ex As Exception
          Throw New Exception($"Failed while reading {pLastReadVariableName}.{vbCrLf}Message:{ex.Message}{vbCrLf}Stack:{Tools.LogToTextFile.GetExceptionString(ex)}")
        End Try

        pBinaryWriter.Close()
      End Using
      pBytesToReturn = pMemoryStream.ToArray()
      pMemoryStream.Close()
    End Using

    Return pBytesToReturn
  End Function

  Public Shared Function BytesToParameters(ByVal vBytes As Byte()) As csTargCCParameterCol
    Dim pParametersToReturn As New ccWSAL.csTargCCParameterCol
    If vBytes.Length = 0 Then Return pParametersToReturn

    Dim pLastReadVariableName As String = ""

    Try
      Using pMemoryStream As New System.IO.MemoryStream(vBytes)
        Using pReader As New System.IO.BinaryReader(pMemoryStream)
          Dim pHasValue As Boolean = False
          Do
            Dim pName As String = pReader.ReadString()
            pLastReadVariableName = pName
            Dim pDataType As ccWSAL.enmDNVariableType = DirectCast([Enum].Parse(GetType(ccWSAL.enmDNVariableType), pReader.ReadString()), ccWSAL.enmDNVariableType)
            Select Case pDataType
              Case ccWSAL.enmDNVariableType.Boolean
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.Boolean).Value = pReader.ReadBoolean()
              Case ccWSAL.enmDNVariableType.ByteArray
                Dim pLength As Integer = pReader.ReadInt32()
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.ByteArray).Value = pReader.ReadBytes(pLength)
              Case ccWSAL.enmDNVariableType.Date
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.Date).Value = New Date(pReader.ReadInt64())
              Case ccWSAL.enmDNVariableType.DateTimeOffset
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.DateTimeOffset).Value = New DateTimeOffset(pReader.ReadInt64, New TimeSpan(pReader.ReadInt64))
              Case ccWSAL.enmDNVariableType.Decimal
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.Decimal).Value = pReader.ReadDecimal()
              Case ccWSAL.enmDNVariableType.Double
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.Double).Value = pReader.ReadDouble()
              Case ccWSAL.enmDNVariableType.Enum
                Dim pEnumType As String = pReader.ReadString()
                Dim pEnumValue As String = pReader.ReadString()
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.Enum).Value = clsEnums.TranslateToEnum(pEnumType, pEnumValue)
              Case ccWSAL.enmDNVariableType.Integer
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.Integer).Value = pReader.ReadInt32()
              Case ccWSAL.enmDNVariableType.Long
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.Long).Value = pReader.ReadInt64()
              Case ccWSAL.enmDNVariableType.Single
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.Single).Value = pReader.ReadSingle()
              Case ccWSAL.enmDNVariableType.String
                pParametersToReturn.Add(pName, ccWSAL.enmDNVariableType.String).Value = pReader.ReadString()
              Case Else
            End Select
          Loop Until pMemoryStream.Position = pMemoryStream.Length

          pReader.Close()
        End Using
        pMemoryStream.Close()
      End Using
    Catch ex As Exception
      Throw New Exception($"Failed while Loading {pLastReadVariableName}.{vbCrLf}Message:{ex.Message}{vbCrLf}Stack:{Tools.LogToTextFile.GetExceptionString(ex)}")
    End Try

    Return pParametersToReturn
  End Function


  Public Enum enmDNVariableType
    UD
    [String]
    [Long]
    [Integer]
    [Boolean]
    [Enum]
    [Decimal]
    [Date]
    [DateTimeOffset]
    [Double]
    [Single]
    [ByteArray]
    Undefined
  End Enum

  Public Class csTargCCParameter

    Public Property Name As String
    Public Property DataType As enmDNVariableType
    Public Property Value As Object

    Public Sub New()
      CreateEmpty()
    End Sub
    Public Sub New(ByVal vName As String, ByVal vDataType As enmDNVariableType)
      CreateEmpty()
      _Name = vName
      _DataType = vDataType
    End Sub

    Private Sub CreateEmpty()
      _Name = ""
      _DataType = enmDNVariableType.UD
      _Value = Nothing
    End Sub

  End Class

  Public Class csTargCCParameterCol
    Inherits Generic.List(Of csTargCCParameter)

    Default Public Overloads ReadOnly Property Item(ByVal vName As String) As csTargCCParameter
      Get
        Dim pParameter As csTargCCParameter
        pParameter = Me.Find(Function(p) p.Name = vName)
        If pParameter Is Nothing Then
          Throw New Exception($"Parameter {vName} was not found")
        End If
        Return pParameter
      End Get
    End Property

    Public Overloads Function Add(ByVal vName As String, ByVal vDataType As enmDNVariableType) As csTargCCParameter
      Dim pParameter As New csTargCCParameter(vName, vDataType)
      Me.Add(pParameter)
      Return pParameter
    End Function

    Public Overrides Function ToString() As String
      Dim pOut As New Text.StringBuilder
      For Each l As csTargCCParameter In Me
        pOut.AppendLine(l.Name & "='" & l.Value.ToString & "'")
      Next
      Return pOut.ToString()
    End Function

  End Class


End Class

