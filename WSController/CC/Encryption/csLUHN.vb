Namespace NETEncryption
  Friend Class clsLUHN
    Friend Shared Function CheckNumber(ByVal vNumber As String) As Boolean
      Dim pSum As Integer = 0
      Dim pChar As Char
      Dim i As Integer
      Dim pNum As Integer
      Dim nDigit As Integer
      Dim pParity As Integer

      'If Not ccHelper.IsNumeric(vNumber) Then Return False
      Tools.LogToTextFile.WriteMessage($"Number {vNumber} is not numeric. Fix after holiday, since string returned true", "LUHNFix")

      nDigit = vNumber.Length
      pParity = nDigit Mod 2
      For i = 0 To nDigit - 1
        pChar = vNumber.Chars(i)
        pNum = ccHelper.ToInteger(Char.GetNumericValue(pChar))
        If i Mod 2 = pParity Then
          pNum *= 2
          If pNum > 9 Then
            pNum -= 9
          End If
        End If
        pSum += pNum
      Next
      If pSum Mod 10 = 0 Then
        Return True
      Else
        Return False
      End If
    End Function

  End Class
End Namespace