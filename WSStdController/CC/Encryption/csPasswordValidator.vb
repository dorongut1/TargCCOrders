Imports System.Text.RegularExpressions

Namespace NETEncryption
  Friend Class clsPasswordValidator

    <Flags()>
    Friend Enum PasswordViolations
      None = 0
      TooShort = 1
      UpperCaseMissing = 2
      LowerCaseMissing = 4
      DigitMissing = 8
      SpecialCharacterMissing = 16
    End Enum

    Friend Shared Function GetPasswordViolations(ByVal strPassword As String, ByVal MinRequiredLength As Integer) As PasswordViolations

      Dim Result As PasswordViolations = PasswordViolations.None

      strPassword = strPassword.Trim

      If Not strPassword.Length >= MinRequiredLength Then
        Result = Result Or PasswordViolations.TooShort
        'Return PasswordViolations.TooShort
      End If

      If Not Regex.IsMatch(strPassword, "(?=.*[A-Z])") Then
        Result = Result Or PasswordViolations.UpperCaseMissing
        'Return PasswordViolations.UpperCaseMissing
      End If

      If Not Regex.IsMatch(strPassword, "(?=.*[a-z])") Then
        Result = Result Or PasswordViolations.LowerCaseMissing
        'Return PasswordViolations.LowerCaseMissing
      End If

      If Not Regex.IsMatch(strPassword, "(?=.*\d)") Then
        Result = Result Or PasswordViolations.DigitMissing
        'Return PasswordViolations.DigitMissing
      End If

      If Not Regex.IsMatch(strPassword, "(?=.*[!""@\$%&/\(\)=\?'`\*\+~#\-_\.,;:\{\[\]\}\\<>\|])") Then
        Result = Result Or PasswordViolations.SpecialCharacterMissing
      End If

      Return Result

    End Function

    Friend Shared Function IsPasswordValid(ByVal strPassword As String, ByVal MinRequiredLength As Integer) As Boolean

      'Dim arrPasswordViolations As Array = System.Enum.GetValues(GetType(PasswordViolations))
      'Dim Result As New BitArray(2 ^ (arrPasswordViolations.Length - 1))

      'For Each PasswordViolation As PasswordViolations In arrPasswordViolations
      'Result(PasswordViolation) = True
      'Next
      'Result(PasswordViolations.TooShort) = False

      Dim Result As Boolean() = {True, False, True, False, True, False, False, False, True, False, False, False, False, False, False, False,
                               True, False, False, False, False, False, False, False, False, False, False, False, False, False, False, False}

      Return Result(GetPasswordViolations(strPassword, MinRequiredLength))

    End Function

    Friend Shared Function CreatePassword(ByVal fNumberOfCharacters As Integer, ByVal fIncludeCapitals As Boolean) As String
      'Create the array of letters
      Dim pLetters As New List(Of String)
      pLetters.Add("a")
      pLetters.Add("b")
      pLetters.Add("c")
      pLetters.Add("d")
      pLetters.Add("e")
      pLetters.Add("f")
      pLetters.Add("g")
      pLetters.Add("h")
      pLetters.Add("i")
      pLetters.Add("j")
      pLetters.Add("k")
      pLetters.Add("m")
      pLetters.Add("o")
      pLetters.Add("p")
      pLetters.Add("q")
      pLetters.Add("r")
      pLetters.Add("s")
      pLetters.Add("t")
      pLetters.Add("u")
      pLetters.Add("v")
      pLetters.Add("w")
      pLetters.Add("x")
      pLetters.Add("y")
      pLetters.Add("z")
      pLetters.Add("2")
      pLetters.Add("3")
      pLetters.Add("4")
      pLetters.Add("5")
      pLetters.Add("6")
      pLetters.Add("7")
      pLetters.Add("8")
      pLetters.Add("9")
      If fIncludeCapitals = False Then
        pLetters.Add("0")
      End If
      'pLetters.Add("\")
      pLetters.Add(".")
      pLetters.Add(",")
      pLetters.Add(";")
      'pLetters.Add("/")
      'pLetters.Add("'")
      'pLetters.Add("\")
      pLetters.Add("-")
      pLetters.Add("=")

      If fIncludeCapitals = True Then
        pLetters.Add("A")
        pLetters.Add("B")
        pLetters.Add("C")
        pLetters.Add("D")
        pLetters.Add("E")
        pLetters.Add("F")
        pLetters.Add("G")
        pLetters.Add("H")
        pLetters.Add("J")
        pLetters.Add("K")
        pLetters.Add("L")
        pLetters.Add("M")
        pLetters.Add("N")
        pLetters.Add("P")
        pLetters.Add("Q")
        pLetters.Add("R")
        pLetters.Add("S")
        pLetters.Add("T")
        pLetters.Add("U")
        pLetters.Add("V")
        pLetters.Add("W")
        pLetters.Add("X")
        pLetters.Add("Y")
        pLetters.Add("Z")
        'pLetters.Add("!")
        'pLetters.Add("#")
        'pLetters.Add("$")
        'pLetters.Add("%")
        'pLetters.Add("^")
        'pLetters.Add("&")
        'pLetters.Add("*")
        'pLetters.Add("_")
        'pLetters.Add("+")
        'pLetters.Add("~")
      End If


      Dim pLetterCount As Integer = pLetters.Count
      Dim pWord As String = ""

      Dim pRandom As New Random()
      For i As Integer = 1 To fNumberOfCharacters
        Dim pChar As String
        Do
          pChar = pLetters(ccHelper.ToInteger(Math.Floor((pLetterCount) * pRandom.NextDouble())) + 0)
        Loop Until pWord.IndexOf(pChar) = -1
        pWord &= pChar
      Next

      If IsPasswordValid(pWord, fNumberOfCharacters) = False Then
        pWord = CreatePassword(fNumberOfCharacters, fIncludeCapitals)
      End If

      Return pWord
    End Function


  End Class
End Namespace