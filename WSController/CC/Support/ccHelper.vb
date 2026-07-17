Imports System.Runtime.CompilerServices 
Imports System.Xml 
Imports System.Reflection 
Imports System.Text 
Imports System.Globalization 
Imports System.Net 
Imports System.Security.Cryptography 
Imports System.Net.Security 

Public Class ccHelper 
  
  Public Enum enmEncryptionMethod 
    UD 
    AES 
    TripleDES 
  End Enum 
 
  Public Enum enmHashType 
    UD 
    SHA1 
    MD5 
    SHA256 
    SHA384 
    SHA512 
  End Enum 
 
  Public Shared ReadOnly Property NewLine() As String 
    Get 
      Return " ‡ " 
    End Get 
  End Property 
 
  Public Shared Function IsNumeric(ByVal vString As String) As Boolean 
    Dim pResult As Double = 0 
    Return Double.TryParse(vString, pResult) 
  End Function 
 
  Public Shared Function IsHebrew(ByVal vString As String) As Boolean 
 
    Dim pIsHebrew As Boolean = False 
 
    For Each l In vString.ToCharArray 
      Dim pHebrew As String = "קראטוןםפשדגכעיחלךףזסבהנמצתץ" 
      If pHebrew.IndexOf(l) >= 0 Then 
        pIsHebrew = True 
        Exit For 
      End If 
    Next 
 
    Return pIsHebrew 
  End Function 
 
  Public Shared Function IsLatin(ByVal vString As String) As Boolean 
 
    Dim pIsLatin As Boolean = False 
 
    For Each l In vString.ToCharArray 
      Dim pLatin As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" 
      If pLatin.IndexOf(l) >= 0 Then 
        pIsLatin = True 
        Exit For 
      End If 
    Next 
 
    Return pIsLatin 
  End Function 
 
  Public Shared Function StringForCSV(vString As String) As String 
 
    Return vString.Replace(ChrW(34), ChrW(34) & ChrW(34)).Replace(Environment.NewLine, ccHelper.NewLine) 
 
  End Function 
 
  Public Shared Function GetSecureString(ByVal vPassword As String) As System.Security.SecureString 
 
    If String.IsNullOrEmpty(vPassword) Then Return Nothing 
 
    Dim pSecStrg As New System.Security.SecureString() 
 
    For Each l As Char In vPassword 
      pSecStrg.AppendChar(l) 
    Next 
 
    pSecStrg.MakeReadOnly() 
 
    Return pSecStrg 
  End Function 
 
  ''' <summary> 
  ''' AES is fastest (after 1st run). TripleDES is also fast, but is smallest. Blowfish is very fast, but not NIST approved, so has been removed.  
  ''' </summary>  
  ''' <param name="vEncryptionMethod"></param>  
  ''' <param name="vUnencrypted"></param>  
  ''' <returns></returns>  
  ''' <remarks></remarks>  
  Public Shared Function Cipher(ByVal vEncryptionMethod As enmEncryptionMethod, ByVal vUnencrypted As String) As String 
    Return Encrypt(vEncryptionMethod, vUnencrypted, vIsExternal:=True) 
  End Function 
 
 
  ''' <summary>  
  ''' Internal version of Cipher.   
  ''' PropertyName is used in the returned message if there is an error. If MaxLength = 0 then there is no limit.  
  ''' AES is fastest (after 1st run). TripleDES is also fast, but is smallest. Blowfish is very fast, but not NIST approved, so has been removed. 
  ''' </summary> 
  ''' <param name="vEncryptionMethod"></param> 
  ''' <param name="vUnencrypted"></param> 
  ''' <param name="vPropertyName"></param> 
  ''' <param name="vMaxLength"></param> 
  ''' <param name="vIsExternal"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Friend Shared Function Encrypt(ByVal vEncryptionMethod As enmEncryptionMethod, ByVal vUnencrypted As String, Optional ByVal vPropertyName As String = "", Optional ByVal vMaxLength As Integer = 0, Optional vIsExternal As Boolean = False) As String 
    If String.IsNullOrEmpty(MyController.DecipherKey) = True Then Throw New Exception("Missing DecipherKey") 
 
    If vUnencrypted = "" Then Return vUnencrypted 
    If vEncryptionMethod = enmEncryptionMethod.UD Then Return "" 
 
    Dim pDecipherKey As String = MyController.DecipherKey 
    If vIsExternal Then pDecipherKey &= pDecipherKey 
 
    Dim pStrg As String 
    If vEncryptionMethod = enmEncryptionMethod.AES Then 
      If Not NETEncryption.clsAES.KeyExists Then 
        'set the key for faster access next time  
        NETEncryption.clsAES.CreateKey(pDecipherKey) 
      End If 
      Try 
        pStrg = NETEncryption.clsAES.Encrypt(vUnencrypted) 
      Catch ex As Exception 
        pStrg = "" 
      End Try 
    ElseIf vEncryptionMethod = enmEncryptionMethod.TripleDES Then 
      pStrg = NETEncryption.clsTripleDES.EncryptData(vUnencrypted, pDecipherKey) 
    Else 
      pStrg = "" 
    End If 
 
    'Check size if required   
    If vMaxLength > 0 Then 
      If pStrg.Length > vMaxLength Then Throw New Exception(vPropertyName & " encrypted is too long") 
    End If 
    Return pStrg 
  End Function 
 
  ''' <summary> 
  ''' This function is used to create a Hash, which is a 1-way encryption.  
  ''' </summary> 
  ''' <param name="vHashType"></param> 
  ''' <param name="vUnencrypted"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Shared Function Encrypt(ByVal vHashType As enmHashType, ByVal vUnencrypted As String) As String 
    If vUnencrypted = "" Then Return vUnencrypted 
    If vHashType = enmHashType.UD Then Return "" 
 
    Dim pStrg As String 
 
    Dim pHashToSend As NETEncryption.clsHash.HashName = NETEncryption.clsHash.HashName.UD 
 
    If vHashType = enmHashType.MD5 Then 
      pHashToSend = NETEncryption.clsHash.HashName.MD5 
    ElseIf vHashType = enmHashType.SHA1 Then 
      pHashToSend = NETEncryption.clsHash.HashName.SHA1 
    ElseIf vHashType = enmHashType.SHA256 Then 
      pHashToSend = NETEncryption.clsHash.HashName.SHA256 
    ElseIf vHashType = enmHashType.SHA384 Then 
      pHashToSend = NETEncryption.clsHash.HashName.SHA384 
    ElseIf vHashType = enmHashType.SHA512 Then 
      pHashToSend = NETEncryption.clsHash.HashName.SHA512 
    ElseIf vHashType = enmHashType.UD Then 
      pHashToSend = NETEncryption.clsHash.HashName.UD 
    End If 
 
    pStrg = NETEncryption.clsHash.Hash(vUnencrypted, pHashToSend) 
 
    Return pStrg 
  End Function 
 
  ''' <summary> 
  ''' AES is fastest (after 1st run). TripleDES is also fast, but is smallest. Blowfish is very fast, but not NIST approved, so has been removed 
  ''' </summary> 
  ''' <param name="vEncryptionMethod"></param> 
  ''' <param name="vEncrypted"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Public Shared Function Decipher(ByVal vEncryptionMethod As enmEncryptionMethod, ByVal vEncrypted As String) As String 
    Return Decrypt(vEncryptionMethod, vEncrypted, vIsExternal:=True) 
  End Function 
 
  ''' <summary>  
  ''' Internal Version of Decipher 
  ''' AES is fastest (after 1st run). TripleDES is also fast, but is smallest. Blowfish is very fast, but not NIST approved, so has been removed  
  ''' </summary>  
  ''' <param name="vEncryptionMethod"></param>  
  ''' <param name="vEncrypted"></param>  
  ''' <param name="vIsExternal"></param>  
  ''' <returns></returns>  
  ''' <remarks></remarks>  
  Friend Shared Function Decrypt(ByVal vEncryptionMethod As enmEncryptionMethod, ByVal vEncrypted As String, Optional vIsExternal As Boolean = False) As String 
    If String.IsNullOrEmpty(MyController.DecipherKey) = True Then Throw New Exception("Missing DecipherKey") 
 
    If vEncrypted = "" Then Return vEncrypted 
    If vEncryptionMethod = enmEncryptionMethod.UD Then Return "" 
 
    Dim pDecipherKey As String = MyController.DecipherKey 
    If vIsExternal Then pDecipherKey &= pDecipherKey 
 
    Dim pStrg As String 
    If vEncryptionMethod = enmEncryptionMethod.AES Then 
      If Not NETEncryption.clsAES.KeyExists Then 
        'set the key for faster access next time  
        NETEncryption.clsAES.CreateKey(pDecipherKey) 
      End If 
      Try 
        pStrg = NETEncryption.clsAES.Decrypt(vEncrypted) 
      Catch ex As Exception 
        pStrg = "" 
      End Try 
    ElseIf vEncryptionMethod = enmEncryptionMethod.TripleDES Then 
      pStrg = NETEncryption.clsTripleDES.DecryptData(vEncrypted, pDecipherKey) 
    Else 
      pStrg = "" 
    End If 
 
    If pStrg Is Nothing Then pStrg = "!! Decryption Error !!" 
    Return pStrg 
  End Function 
 
  ''' <summary>  
  ''' Checks if password is at least 8 characters, and has 3 of lower case letters, uppercase letters, numbers or symbols 
  ''' </summary>  
  ''' <param name="vPassword"></param>  
  ''' <returns></returns>  
  Public Shared Function IsPasswordComplex(ByVal vPassword As String) As Boolean 
    If String.IsNullOrEmpty(vPassword) Then Return False 
 
    Return NETEncryption.clsPasswordValidator.IsPasswordValid(vPassword, 8) 
  End Function 
 
  ''' <summary> 
  ''' This creates a random password of 8 characters 
  ''' </summary> 
  ''' <returns></returns> 
  Public Shared Function CreatePassword() As String 
    Return NETEncryption.clsPasswordValidator.CreatePassword(8, True) 
  End Function 
 
  ''' <summary> 
  ''' Checks that the number is LUHN compliant 
  ''' </summary> 
  ''' <param name="vNumberToCheck"></param> 
  ''' <returns></returns> 
  Public Shared Function IsLUHNCompliant(ByVal vNumberToCheck As String) As Boolean 
    If vNumberToCheck = "" Then Return False 
 
    Return NETEncryption.clsLUHN.CheckNumber(vNumberToCheck) 
  End Function 
 
  Public Shared Function ValidatePasswordByBestPractice(vPassword As String, vUsername As String) As String 
 
    If String.IsNullOrEmpty(vPassword) OrElse vPassword.Length < 8 OrElse vPassword.Length > 64 Then 
      Return "Password length must be 8–64 characters." 
    End If 
 
    If Not String.IsNullOrEmpty(vUsername) Then 
      Dim u = vUsername.Trim() 
      ' Case insensitive check 
      If u.Length > 0 AndAlso vPassword.IndexOf(u, StringComparison.OrdinalIgnoreCase) >= 0 Then 
        Return "Password must not contain the username." 
      End If 
    End If 
 
    If vPassword IsNot Nothing AndAlso vPassword.Length <= 12 Then 
      Dim hasLetter = vPassword.Any(Function(ch) Char.IsLetter(ch)) 
      Dim hasNumber = vPassword.Any(Function(ch) Char.IsDigit(ch)) 
      Dim hasSpecial = vPassword.Any(Function(ch) Not Char.IsLetterOrDigit(ch)) 
      Dim hasMixedCase = vPassword.Any(AddressOf Char.IsLower) AndAlso vPassword.Any(AddressOf Char.IsUpper) 
 
      If Not (hasLetter AndAlso hasNumber AndAlso (hasSpecial OrElse hasMixedCase)) Then 
        Return "For passwords up to 12 characters, use letters, numbers, and a special character or mixed case." 
      End If 
    End If 
 
    ' Only hit HIBP if everything else passed 
    Try 
      Dim count As Integer = HIBP.BreachCount(vPassword) 
      If count > 0 Then 
        Return "This password appears in public breach data (HIBP)." 
      End If 
    Catch ex As Exception 
      Return "HIBP check failed: " & ex.Message 
    End Try 
 
    Return "OK" 
  End Function 
 
  ''' <summary> 
  ''' Generates a cryptographically secure random secret key. 
  ''' Default: 32 bytes = 256-bit key (strong for authentication tokens). 
  ''' Returns a URL-safe Base64 string. 
  ''' </summary> 
  Public Shared Function CreateSecretKey(Optional byteLength As Integer = 32) As String 
 
    Dim bytes(byteLength - 1) As Byte 
 
    Using rng As RandomNumberGenerator = RandomNumberGenerator.Create() 
      rng.GetBytes(bytes) 
    End Using 
 
    ' Convert to URL-safe Base64 (no + / or trailing =) 
    Dim base64 = Convert.ToBase64String(bytes) 
    base64 = base64.Replace("+", "-").Replace("/", "_").TrimEnd("="c) 
 
    Return base64 
  End Function 
 
  ' Nested helper class 
  Private Class HIBP 
    Public Shared Function BreachCount(vPassword As String) As Integer 
      Dim sha1Hash = Sha1Hex(vPassword) 
      Dim prefix = sha1Hash.Substring(0, 5) 
      Dim suffix = sha1Hash.Substring(5) 
 
      ' 2. In .NET 4.8, HttpClient is Async-only.  
      ' We use HttpWebRequest for true synchronous behavior. 
      Dim url As String = String.Format("https://api.pwnedpasswords.com/range/{0}", prefix) 
      Dim request As HttpWebRequest = DirectCast(WebRequest.Create(url), HttpWebRequest) 
 
      request.Method = "GET" 
      request.UserAgent = "PasswordPolicy/1.0" 
      request.Headers.Add("Add-Padding", "true") 
      request.Timeout = 10000 ' 10 seconds 
 
      Using response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse) 
        Using stream As IO.Stream = response.GetResponseStream() 
          Using reader As New IO.StreamReader(stream) 
            Dim body As String = reader.ReadToEnd() 
 
            ' Split on LineFeed (\n) 
            Dim lines = body.Split(New Char() {ChrW(10)}, StringSplitOptions.RemoveEmptyEntries) 
 
            For Each line In lines 
              Dim parts = line.Trim().Split(":"c) 
              If parts.Length <> 2 Then Continue For 
 
              If parts(0).Equals(suffix, StringComparison.OrdinalIgnoreCase) Then 
                Dim n As Integer 
                If Integer.TryParse(parts(1), n) Then 
                  Return n 
                Else 
                  Return 1 
                End If 
              End If 
            Next 
          End Using 
        End Using 
      End Using 
 
      Return 0 
    End Function 
 
    Private Shared Function Sha1Hex(s As String) As String 
      Using sha = SHA1.Create() 
        Dim bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(s)) 
        Dim sb As New StringBuilder(bytes.Length * 2) 
        For Each b In bytes 
          sb.Append(b.ToString("X2")) 
        Next 
        Return sb.ToString() 
      End Using 
    End Function 
  End Class 
 
  ''' <summary> 
  ''' This take a plain string and changes it to base64, so it be used in a querystring 
  ''' </summary> 
  ''' <param name="vPlainString"></param> 
  ''' <returns></returns> 
  Public Shared Function ToBase64String(ByVal vPlainString As String) As String 
 
    Dim plainTextBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(vPlainString) 
    Dim pBase64String As String = System.Convert.ToBase64String(plainTextBytes) 
 
    Return pBase64String 
  End Function 
 
  ''' <summary> 
  ''' This take a base64 string and changes it to a plain string 
  ''' </summary> 
  ''' <param name="vBase64String"></param> 
  ''' <returns></returns> 
  Public Shared Function ToPlainString(ByVal vBase64String As String) As String 
 
    Dim base64Bytes As Byte() = System.Convert.FromBase64String(vBase64String) 
    Dim pPlainString As String = System.Text.Encoding.UTF8.GetString(base64Bytes) 
 
    Return pPlainString 
  End Function 
 
  ''' <summary>  
  ''' This take a byte array and changes it to base64 string, so it be used in a querystring or anything else.... 
  ''' </summary>  
  ''' <param name="vBytes"></param>  
  ''' <returns></returns>  
  Public Shared Function ToBase64String(ByVal vBytes As Byte()) As String 
 
    Dim pBase64String As String = System.Convert.ToBase64String(vBytes) 
 
    Return pBase64String 
  End Function 
 
  ''' <summary>  
  ''' This takes a base64 string and changes it to a byte array 
  ''' </summary>  
  ''' <param name="vBase64String"></param>  
  ''' <returns></returns>  
  Public Shared Function ToByteArrayFromBase64String(ByVal vBase64String As String) As Byte() 
 
    Dim base64Bytes As Byte() = System.Convert.FromBase64String(vBase64String) 
 
    Return base64Bytes 
  End Function 
 
  ''' <summary>  
  ''' This take a plain string and changes it to a byte array 
  ''' </summary>  
  ''' <param name="vPlainString"></param>  
  ''' <returns></returns>  
  Public Shared Function ToByteArrayFromPlainString(ByVal vPlainString As String) As Byte() 
 
    Dim plainTextBytes As Byte() = System.Text.Encoding.UTF8.GetBytes(vPlainString) 
 
    Return plainTextBytes 
  End Function 
 
  Public Shared Function GetPropertyValue(vObjectInstance As Object, vPropertyName As String) As Object 
    Dim objType As Type = vObjectInstance.GetType() 
    Dim pInfo As Reflection.PropertyInfo 
    Try 
      pInfo = objType.GetProperty(vPropertyName) 
    Catch ex As Exception 
      Return Nothing 
    End Try 
    Dim PropValue As Object = pInfo.GetValue(vObjectInstance, Reflection.BindingFlags.GetProperty, Nothing, Nothing, Nothing) 
    Return PropValue 
  End Function 
 
  Public Shared Function GetPropertyTypeName(vObjectInstance As Object, vPropertyName As String) As String 
    Dim objType As Type = vObjectInstance.GetType() 
    Dim pInfo As Reflection.PropertyInfo 
    Try 
      pInfo = objType.GetProperty(vPropertyName) 
    Catch ex As Exception 
      Return "" 
    End Try 
    If pInfo Is Nothing Then Return "" 
    Return pInfo.PropertyType.Name 
  End Function 
 
  Public Shared Function MakeDBWild(ByVal vIn As String) As String 
    Dim pOut As String 
    If String.IsNullOrEmpty(vIn) Then 
      pOut = "%" 
    Else 
      pOut = vIn 
    End If 
    Return pOut 
  End Function 
 
  Public Shared Function ObjectNullable(ByVal vObj As Object) As Object 
    Dim pObj As Object 
    If vObj Is Nothing Then 
      pObj = System.DBNull.Value 
    Else 
      If TypeOf (vObj) Is String Then 
        pObj = DirectCast(vObj, String).Trim().CheckIfTruncated() 
      Else 
        pObj = vObj 
      End If 
    End If 
    Return pObj 
  End Function 
  Public Shared Function DateNullable(ByVal vDate As Date) As Object 
    Dim pDate As Object 
    If vDate = DateTime.MinValue Then 
      pDate = System.DBNull.Value 
    Else 
      pDate = vDate 
    End If 
    Return pDate 
  End Function 
  Public Shared Function DateTimeOffsetNullable(ByVal vDateTimeOffset As DateTimeOffset) As Object 
    Dim pDateTimeOffset As Object 
    If vDateTimeOffset = DateTimeOffset.MinValue Then 
      pDateTimeOffset = System.DBNull.Value 
    Else 
      pDateTimeOffset = vDateTimeOffset 
    End If 
    Return pDateTimeOffset 
  End Function 
  Public Shared Function ForeignKeyLongNullable(ByVal vFKID As Long, ByVal vPrimaryTableCanHave0AsPrimaryKey As Boolean) As Object 
    Dim pFKID As Object 
    If vPrimaryTableCanHave0AsPrimaryKey Then 
      If vFKID = -1 Then pFKID = System.DBNull.Value Else pFKID = vFKID 
    Else 
      If vFKID = 0 Then pFKID = System.DBNull.Value Else pFKID = vFKID 
    End If 
    Return pFKID 
  End Function 
  Public Shared Function ForeignKeyIntegerNullable(ByVal vFKID As Integer, ByVal vPrimaryTableCanHave0AsPrimaryKey As Boolean) As Object 
    Dim pFKID As Object 
    If vPrimaryTableCanHave0AsPrimaryKey Then 
      If vFKID = -1 Then pFKID = System.DBNull.Value Else pFKID = vFKID 
    Else 
      If vFKID = 0 Then pFKID = System.DBNull.Value Else pFKID = vFKID 
    End If 
    Return pFKID 
  End Function 
  Public Shared Function ForeignKeyStringNullable(ByVal vFKID As String) As Object 
    Dim pFKID As Object 
    If vFKID Is Nothing OrElse vFKID = "" Then 
      pFKID = System.DBNull.Value 
    Else 
      pFKID = vFKID.Trim().CheckIfTruncated() 
    End If 
    Return pFKID 
  End Function 
  Public Shared Function LookupNullable(ByVal pLookupCode As String) As Object 
    Dim pObj As Object 
    If String.IsNullOrEmpty(pLookupCode) Then 
      pObj = System.DBNull.Value 
    Else 
      pObj = pLookupCode 
    End If 
    Return pObj 
  End Function 
  Public Shared Function LookupNullable(ByVal pLookupCode As Integer) As Object 
    Dim pObj As Object 
    If pLookupCode = -1 Then 
      pObj = System.DBNull.Value 
    Else 
      pObj = pLookupCode 
    End If 
    Return pObj 
  End Function 
  Public Shared Function LookupNullable(ByVal pLookupCode As Long) As Object 
    Dim pObj As Object 
    If pLookupCode = -1 Then 
      pObj = System.DBNull.Value 
    Else 
      pObj = pLookupCode 
    End If 
    Return pObj 
  End Function 
 
  ''' <summary> 
  ''' Use these functions to ensure compatibility with .Net Core 
  ''' </summary> 
  ''' <param name="vStrg"></param> 
  ''' <returns></returns> 
  Public Shared Function ToLong(vStrg As String) As Long 
    If String.IsNullOrEmpty(vStrg) Then 
      Return 0 
    Else 
      Dim CharCheck As Integer = Convert.ToInt32(vStrg.Chars(0)) 
      If CharCheck = 8206 OrElse CharCheck = 1564 Then 
        Tools.LogToTextFile.WriteMessage($"ToLong: vStrg [{vStrg}], Offending Code [{CharCheck}]{Environment.NewLine}{ccHelper.GetStack()}", "CharCheck") 
        vStrg = vStrg.Substring(1) 
      End If 
    End If 
 
    Dim pLong As Long 
    Dim pSucceeded As Boolean 
    pSucceeded = Long.TryParse(vStrg, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, pLong) 
    If Not pSucceeded Then 
      pSucceeded = Long.TryParse(vStrg, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pLong) 
    End If 
 
    If Not pSucceeded Then 
      Throw New Exception($"The string received [{vStrg}] cannot be converted to a long. The initial character is [{vStrg.Substring(0, 1)}], CharCode [{Convert.ToInt32(vStrg.Chars(0))}]") 
    End If 
 
    Return pLong 
  End Function 
  Public Shared Function ToLong(vInt As Integer) As Long 
    Dim pLong As Long = Convert.ToInt64(vInt) 
    Return pLong 
  End Function 
  Public Shared Function ToLong(vLng As Long) As Long 
    Return vLng 
  End Function 
  Public Shared Function ToLong(vDble As Double) As Long 
    Dim pLong As Long = Convert.ToInt64(vDble) 
    'If vDble - pLong <> 0 Then Throw New Exception($"I can't let you convert the double [{vDble}] to a long") 
    Return pLong 
  End Function 
  Public Shared Function ToLong(vDec As Decimal) As Long 
    Dim pLong As Long = Convert.ToInt64(vDec) 
    If vDec - pLong <> 0 Then Throw New Exception($"I can't let you convert the decimal [{vDec}] to a long") 
    Return pLong 
  End Function 
  Public Shared Function ToLong(vObj As Object) As Long 
    Dim pLong As Long 
 
    Try 
      pLong = Convert.ToInt64(vObj) 
    Catch ex As Exception 
      Dim pStrg As String = Convert.ToString(vObj) 
      If String.IsNullOrEmpty(pStrg) Then Return 0 
      Throw New Exception($"I cannot convert the object received [{pStrg}] to a Long") 
    End Try 
 
    Return pLong 
  End Function 
 
  ''' <summary> 
  ''' Use these functions to ensure compatibility with .Net Core 
  ''' </summary> 
  ''' <param name="vStrg"></param> 
  ''' <returns></returns> 
  Public Shared Function ToInteger(vStrg As String) As Integer 
    If String.IsNullOrEmpty(vStrg) Then 
      Return 0 
    Else 
      Dim CharCheck As Integer = Convert.ToInt32(vStrg.Chars(0)) 
      If CharCheck = 8206 OrElse CharCheck = 1564 Then 
        Tools.LogToTextFile.WriteMessage($"ToLong: vStrg [{vStrg}], Offending Code [{CharCheck}]{Environment.NewLine}{ccHelper.GetStack()}", "CharCheck") 
        vStrg = vStrg.Substring(1) 
      End If 
    End If 
 
    Dim pInteger As Integer 
    Dim pSucceeded As Boolean 
    pSucceeded = Integer.TryParse(vStrg, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, pInteger) 
    If Not pSucceeded Then 
      pSucceeded = Integer.TryParse(vStrg, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pInteger) 
    End If 
 
    If Not pSucceeded Then 
      Throw New Exception($"The string received [{vStrg}] cannot be converted to an integer. The initial character is [{vStrg.Substring(0, 1)}], CharCode [{Convert.ToInt32(vStrg.Chars(0))}]") 
    End If 
 
    Return pInteger 
  End Function 
  Public Shared Function ToInteger(vLng As Long) As Integer 
    Dim pInteger As Integer = Convert.ToInt32(vLng) 
    Return pInteger 
  End Function 
  Public Shared Function ToInteger(vInt As Integer) As Integer 
    Return vInt 
  End Function 
  Public Shared Function ToInteger(vDble As Double) As Integer 
    Dim pInteger As Integer = Convert.ToInt32(vDble) 
    'If vDble - pInteger <> 0 Then Throw New Exception($"I can't let you convert the double [{vDble}] to a Integer") 
    Return pInteger 
  End Function 
  Public Shared Function ToInteger(vDec As Decimal) As Integer 
    Dim pInteger As Integer = Convert.ToInt32(vDec) 
    If vDec - pInteger <> 0 Then Throw New Exception($"I can't let you convert the decimal [{vDec}] to a Integer") 
    Return pInteger 
  End Function 
  Public Shared Function ToInteger(vObj As Object) As Integer 
    Dim pInteger As Integer 
 
    Try 
      pInteger = Convert.ToInt32(vObj) 
    Catch ex As Exception 
      Dim pStrg As String = Convert.ToString(vObj) 
      If String.IsNullOrEmpty(pStrg) Then Return 0 
      Throw New Exception($"I cannot convert the object received [{pStrg}] to an integer") 
    End Try 
 
    Return pInteger 
  End Function 
  ''' <summary>  
  ''' Use these functions to ensure compatibility with .Net Core  <br/><br/> 
  ''' This converts a string to a decimal value of the specified precision. <br/> 
  '''  -1 returns what we got (i.e. doesn't do anything) <br/> 
  '''  -2 rounds to the number of significant digits in the decimal part (e.g 2.35620000 returns 2.3562) <br/> 
  '''  if vDecimals >0 it rounds it to the number of decimals (e.g if vDecimals = 2 then for 2.35620000 returns 2.36) <br/> 
  ''' </summary>  
  ''' <param name="vStrg"></param>  
  ''' <returns></returns>  
  Public Shared Function ToDecimal(vStrg As String, Optional vDecimals As Integer = -1) As Decimal 
    If String.IsNullOrEmpty(vStrg) Then 
      Return 0 
    Else 
      Dim CharCheck As Integer = Convert.ToInt32(vStrg.Chars(0)) 
      If CharCheck = 8206 OrElse CharCheck = 1564 Then 
        Tools.LogToTextFile.WriteMessage($"ToLong: vStrg [{vStrg}], Offending Code [{CharCheck}]{Environment.NewLine}{ccHelper.GetStack()}", "CharCheck") 
        vStrg = vStrg.Substring(1) 
      End If 
    End If 
 
    Dim pDecimal As Decimal 
    Dim pSucceeded As Boolean 
    pSucceeded = Decimal.TryParse(vStrg, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, pDecimal) 
    If Not pSucceeded Then 
      pSucceeded = Decimal.TryParse(vStrg, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pDecimal) 
    End If 
 
    If Not pSucceeded Then 
      Throw New Exception($"The string received [{vStrg}] cannot be converted to a decimal. The initial character is [{vStrg.Substring(0, 1)}], CharCode [{Convert.ToInt32(vStrg.Chars(0))}]") 
    End If 
 
    Return ToDecimal(pDecimal, vDecimals) 
  End Function 
  ''' <summary> 
  ''' This converts a long to a decimal value of the specified precision. <br/> 
  '''  -1 returns what we got (i.e. doesn't do anything) <br/> 
  '''  -2 rounds to the number of significant digits in the decimal part (e.g 2.35620000 returns 2.3562) <br/> 
  '''  if vDecimals >0 it rounds it to the number of decimals (e.g if vDecimals = 2 then for 2.35620000 returns 2.36) <br/> 
  ''' </summary> 
  ''' <param name="vLng"></param> 
  ''' <param name="vDecimals"></param> 
  ''' <returns></returns> 
  Public Shared Function ToDecimal(vLng As Long, Optional vDecimals As Integer = -1) As Decimal 
    Dim pDecimal As Decimal = Convert.ToDecimal(vLng) 
    Return ToDecimal(pDecimal, vDecimals) 
  End Function 
  ''' <summary> 
  ''' This converts an integer to a decimal value of the specified precision. <br/> 
  '''  -1 returns what we got (i.e. doesn't do anything) <br/> 
  '''  -2 rounds to the number of significant digits in the decimal part (e.g 2.35620000 returns 2.3562) <br/> 
  '''  if vDecimals >0 it rounds it to the number of decimals (e.g if vDecimals = 2 then for 2.35620000 returns 2.36) <br/> 
  ''' </summary> 
  ''' <param name="vInt"></param> 
  ''' <param name="vDecimals"></param> 
  ''' <returns></returns> 
  Public Shared Function ToDecimal(vInt As Integer, Optional vDecimals As Integer = -1) As Decimal 
    Dim pDecimal As Decimal = Convert.ToDecimal(vInt) 
    Return ToDecimal(pDecimal, vDecimals) 
  End Function 
  ''' <summary> 
  ''' This converts a double to a decimal value of the specified precision. <br/> 
  '''  -1 returns what we got (i.e. doesn't do anything) <br/> 
  '''  -2 rounds to the number of significant digits in the decimal part (e.g 2.35620000 returns 2.3562) <br/> 
  '''  if vDecimals >0 it rounds it to the number of decimals (e.g if vDecimals = 2 then for 2.35620000 returns 2.36) <br/> 
  ''' </summary> 
  ''' <param name="vDble"></param> 
  ''' <param name="vDecimals"></param> 
  ''' <returns></returns> 
  Public Shared Function ToDecimal(vDble As Double, Optional vDecimals As Integer = -1) As Decimal 
    Dim pDecimal As Decimal = Convert.ToDecimal(vDble) 
    Return ToDecimal(pDecimal, vDecimals) 
  End Function 
  ''' <summary> 
  ''' This rounds a decimal value to the specified precision. <br/> 
  '''  -1 returns what we got (i.e. doesn't do anything) <br/> 
  '''  -2 rounds to the number of significant digits in the decimal part (e.g 2.35620000 returns 2.3562) <br/> 
  '''  if vDecimals >0 it rounds it to the number of decimals (e.g if vDecimals = 2 then for 2.35620000 returns 2.36) <br/> 
  ''' </summary> 
  ''' <param name="vDec"></param> 
  ''' <param name="vDecimals"></param> 
  ''' <returns></returns> 
  Public Shared Function ToDecimal(vDec As Decimal, Optional vDecimals As Integer = -1) As Decimal 
    If vDecimals > 0 Then 
      vDec = Decimal.Round(vDec, vDecimals, MidpointRounding.AwayFromZero) 
    ElseIf vDecimals = -2 Then 
      For i = 0 To 10 
        Dim pDec = vDec * 10 ^ i 
        If pDec - Math.Floor(pDec) = 0 Then 
          vDec = Decimal.Round(vDec, i, MidpointRounding.AwayFromZero) 
          Exit For 
        End If 
      Next 
    Else 
      vDec = vDec 
    End If 
    Return vDec 
  End Function 
  ''' <summary> 
  ''' This converts an object (if possible) to a decimal value of the specified precision. <br/> 
  '''  -1 returns what we got (i.e. doesn't do anything) <br/> 
  '''  -2 rounds to the number of significant digits in the decimal part (e.g 2.35620000 returns 2.3562) <br/> 
  '''  if vDecimals >0 it rounds it to the number of decimals (e.g if vDecimals = 2 then for 2.35620000 returns 2.36) <br/> 
  ''' </summary> 
  ''' <param name="vObj"></param> 
  ''' <param name="vDecimals"></param> 
  ''' <returns></returns> 
  Public Shared Function ToDecimal(vObj As Object, Optional vDecimals As Integer = -1) As Decimal 
    Dim pDecimal As Decimal 
 
    Try 
      pDecimal = Convert.ToDecimal(vObj) 
    Catch ex As Exception 
      Dim pStrg As String = Convert.ToString(vObj) 
      If String.IsNullOrEmpty(pStrg) Then Return 0 
      Throw New Exception($"I cannot convert the object received [{pStrg}] to an Decimal") 
    End Try 
    Return ToDecimal(pDecimal, vDecimals) 
  End Function 
  ''' <summary>  
  ''' Use these functions to ensure compatibility with .Net Core  
  ''' </summary>  
  ''' <param name="vStrg"></param>  
  ''' <returns></returns>  
  Public Shared Function ToDouble(vStrg As String) As Double 
    If String.IsNullOrEmpty(vStrg) Then 
      Return 0 
    Else 
      Dim CharCheck As Integer = Convert.ToInt32(vStrg.Chars(0)) 
      If CharCheck = 8206 OrElse CharCheck = 1564 Then 
        Tools.LogToTextFile.WriteMessage($"ToLong: vStrg [{vStrg}], Offending Code [{CharCheck}]{Environment.NewLine}{ccHelper.GetStack()}", "CharCheck") 
        vStrg = vStrg.Substring(1) 
      End If 
    End If 
 
    Dim pDouble As Double 
    Dim pSucceeded As Boolean 
    pSucceeded = Double.TryParse(vStrg, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, pDouble) 
    If Not pSucceeded Then 
      pSucceeded = Double.TryParse(vStrg, Globalization.NumberStyles.Any, Globalization.CultureInfo.CurrentCulture, pDouble) 
    End If 
 
    If Not pSucceeded Then 
      Throw New Exception($"The string received [{vStrg}] cannot be converted to a double. The initial character is [{vStrg.Substring(0, 1)}], CharCode [{Convert.ToInt32(vStrg.Chars(0))}]") 
    End If 
 
    Return pDouble 
  End Function 
  Public Shared Function ToDouble(vLng As Long) As Double 
    Dim pDouble As Double = Convert.ToDouble(vLng) 
    Return pDouble 
  End Function 
  Public Shared Function ToDouble(vInt As Integer) As Double 
    Dim pDouble As Double = Convert.ToDouble(vInt) 
    Return pDouble 
  End Function 
  Public Shared Function ToDouble(vDble As Double) As Double 
    Return vDble 
  End Function 
  Public Shared Function ToDouble(vDec As Decimal) As Double 
    Dim pDouble As Double = Convert.ToDouble(vDec) 
    Return pDouble 
  End Function 
  Public Shared Function ToDouble(vObj As Object) As Double 
    Dim pDouble As Double 
 
    Try 
      pDouble = Convert.ToDouble(vObj) 
    Catch ex As Exception 
      Dim pStrg As String = Convert.ToString(vObj) 
      If String.IsNullOrEmpty(pStrg) Then Return 0 
      Throw New Exception($"I cannot convert the object received [{pStrg}] to an Double") 
    End Try 
 
    Return pDouble 
  End Function 
 
  Public Shared Function ToBoolean(vObj As Object) As Boolean 
    Dim pBoolean As Boolean 
    Dim pStrg As String = vObj.ToString() 
 
    Dim pMessage As String = "" 
 
    Try 
 
      If IsNumeric(pStrg) Then 
        Dim pNum As Integer = ToInteger(pStrg) 
        If pNum = 1 Then 
          Return True 
        ElseIf pNum = 0 Then 
          Return False 
        Else 
          pMessage = $"I cannot convert the number received [{pStrg}] to a Boolean" 
        End If 
      Else 
        If pStrg.Equals("true", StringComparison.OrdinalIgnoreCase) Then 
          Return True 
        ElseIf pStrg.Equals("false", StringComparison.OrdinalIgnoreCase) Then 
          Return False 
        Else 
          pMessage = $"I cannot convert the string received [{pStrg}] to a Boolean" 
        End If 
      End If 
 
    Catch ex As Exception 
      Throw New Exception($"I cannot convert the object received [{pStrg}] to an Boolean") 
    End Try 
 
    If Not String.IsNullOrEmpty(pMessage) Then 
      Throw New Exception(pMessage) 
    End If 
 
    Return False 
  End Function 
 
  Public Shared Function GetAge(vBirthDate As DateTime) As Integer 
    Dim pToday As Date = Date.Today 
    Dim pAge As Integer = pToday.Year - vBirthDate.Year 
 
    ' Handle cases where birthday hasn't happened yet this year 
    If pToday.Month < vBirthDate.Month OrElse (pToday.Month = vBirthDate.Month AndAlso pToday.Day < vBirthDate.Day) Then 
      pAge -= 1 
    End If 
 
    Return pAge 
  End Function 
 
  ''' <summary> 
  ''' Returns the position of the first letter in the string in the alphabet.  
  ''' Supports Latin and Hebrew alphabets. Latin letters should not be accented. 
  ''' If the letter is invalid, then 28 is returned 
  ''' </summary> 
  ''' <param name="vLetter"></param> 
  ''' <returns></returns> 
  Public Shared Function GetPositionInAlphabet(vLetter As String) As Integer 
 
    Dim pLetter As String = (vLetter).Trim.Substring(0, 1) 
    Dim pInt As Integer = AscW(pLetter) - AscW("A") 
    If pInt < 0 OrElse pInt > 30 Then pInt = AscW(pLetter) - AscW("a") 
    If pInt < 0 OrElse pInt > 30 Then pInt = AscW(pLetter) - AscW("א") 
    pInt += 1 
 
    If pInt < 0 OrElse pInt > 30 Then pInt = 28 
 
    Return pInt 
  End Function 
 
  ''' <summary> 
  ''' this function accepts either an xml file or an xml string. Do not submit both 
  ''' </summary> 
  ''' <param name="vXMLFileName"></param> 
  ''' <param name="vXMLString"></param> 
  ''' <returns></returns> 
  Public Shared Function XMLToString(vXMLFileName As String, vXMLString As String) As String 
 
    ' Load the XML file 
    Dim doc As New XmlDocument() 
 
    If Not String.IsNullOrEmpty(vXMLFileName) Then 
      doc.Load(vXMLFileName) 
    Else 
      'Play it safe 
      If Not vXMLString.StartsWith("<?") Then Return vXMLString 
      Try 
        doc.LoadXml(vXMLString) 
      Catch ex As Exception 
        Return vXMLString 
      End Try 
    End If 
 
    Dim pValues As New Text.StringBuilder() 
 
    Dim pLevel As Integer = 0 
 
    ' Loop through the XML nodes  
    For Each node As XmlNode In doc.DocumentElement.ChildNodes 
      Dim pLevelName As String = node.Name 
      If node.HasChildNodes Then 
        If node.InnerXml.IndexOf("<") >= 0 Then pValues.AppendLine($"{pLevelName}") 
        pValues.Append(GetXMLChildNodes(node, pLevelName, pLevel).ToString()) 
      Else 
        If Not String.IsNullOrEmpty(node.InnerText) Then 
          pValues.AppendLine($"{pLevelName}: {node.InnerText}") 
        End If 
      End If 
    Next 
 
    Return pValues.ToString 
  End Function 
 
  Private Shared Function GetXMLChildNodes(parentNode As XmlNode, vParentTitle As String, vLevel As Integer) As Text.StringBuilder 
    Dim pValues As New Text.StringBuilder() 
 
    Dim pLevel As Integer = vLevel + 1 
    Dim pSpaces As New String("."c, pLevel * 2) 
 
    For Each childNode As XmlNode In parentNode.ChildNodes 
      Dim pLevelName As String = childNode.Name 
      If childNode.NodeType = XmlNodeType.Text Then 
        pLevelName = vParentTitle 
      End If 
      If childNode.HasChildNodes Then 
        If childNode.InnerXml.IndexOf("<") >= 0 Then pValues.AppendLine($"{New String("."c, (pLevel + 1) * 2)}{pLevelName}") 
        pValues.Append(GetXMLChildNodes(childNode, pLevelName, pLevel).ToString()) 
      Else 
        Dim pAttributes As XmlAttributeCollection = childNode.ParentNode.Attributes 
        If pAttributes IsNot Nothing AndAlso pAttributes.Count > 0 Then 
          pValues.AppendLine($"{pSpaces}{pLevelName}: {pAttributes(0).Value} ({childNode.InnerText})") 
        Else 
          If Not String.IsNullOrEmpty(childNode.InnerText) Then 
            pValues.AppendLine($"{pSpaces}{pLevelName}: {childNode.InnerText}") 
          End If 
        End If 
      End If 
    Next 
 
    Return pValues 
  End Function 
 
  Public Shared Function GetExceptionText(ByVal vEx As Exception) As String 
    Dim pEx As Exception = vEx 
    Dim pString As String 
 
    pString = "Exception!!! Type:" & vEx.GetType.ToString() & " ‡ " 
    pString &= " Details:" & " ‡ " 
 
    Dim iCntr As Integer = 1 
    pString &= "  " & iCntr & ". " & pEx.Message & " ‡ " 
    'now do inner exceptions 
    Do Until pEx.InnerException Is Nothing 
      iCntr += 1 
      pEx = pEx.InnerException 
      pString &= "  " & iCntr & ". " & pEx.Message & " ‡ " 
    Loop 
 
    Return pString 
  End Function 
 
  ''' <summary> 
  ''' Use this if you want to expose the string in a web service or other XML function 
  ''' </summary> 
  ''' <param name="vTextIn"></param> 
  ''' <returns></returns> 
  Public Shared Function RemoveChrW0(ByVal vTextIn As String) As String 
    Dim pNothing As Char = ChrW(0) 'Removes bad data 'using char is faster..  
 
    If vTextIn.IndexOf(pNothing) >= 0 Then vTextIn = vTextIn.Replace(pNothing, "") 
 
    Return vTextIn 
  End Function 
 
  Public Shared Function RemoveIllegalXMLChars(ByVal vTextIn As String) As String  
    'https://www.codetable.net/asciikeycodes 
 
    Dim pPattern As String = "[\u0000-\u0008\u0009\u000B-\u000C\u000E-\u0019]" 
    vTextIn = System.Text.RegularExpressions.Regex.Replace(vTextIn, pPattern, "") 
 
    Dim pChar As Char = ChrW(65533) 
    If vTextIn.IndexOf(pChar) >= 0 Then vTextIn = vTextIn.Replace(pChar, "") 
 
    'Note: prefix &#x is hexadecimal, &# is decimal 
    
    pChar = ChrW(16) 
    If vTextIn.IndexOf(pChar) >= 0 Then vTextIn = vTextIn.Replace(pChar, "") 
 
    pChar = ChrW(29) 
    If vTextIn.IndexOf(pChar) >= 0 Then vTextIn = vTextIn.Replace(pChar, "") 
 
    vTextIn = vTextIn.Replace("&#x10;", "") 
    vTextIn = vTextIn.Replace("&#x1D;", "") 
    vTextIn = vTextIn.Replace("&#x1F;", "") 
 
    Return vTextIn 
  End Function  
  
  Public Shared Function ResizeBitmap(ByVal vImageBytes As Byte(), ByVal vNewSize As System.Drawing.Size, ByRef rWasShrunk As Boolean) As Byte() 
 
    If vImageBytes Is Nothing Then Return Nothing 
 
    'Convert ByteArray to image 
    Dim pStream As IO.Stream = New System.IO.MemoryStream(vImageBytes) 
    Dim pImage As New System.Drawing.Bitmap(pStream, False) 
 
    'Now resize it 
    Dim pNewImage As System.Drawing.Bitmap = ResizeBitmap(pImage, vNewSize, rWasShrunk, False) 
 
    'Now convert to Byte Array 
    Dim pNewStream As New IO.MemoryStream 
    pNewImage.Save(pNewStream, System.Drawing.Imaging.ImageFormat.Jpeg) 
    Dim pNewByte As Byte() = pNewStream.ToArray 
 
    Return pNewByte 
  End Function 
 
  Public Shared Function ResizeBitmap(ByVal vImage As System.Drawing.Bitmap, ByVal vNewSize As System.Drawing.Size, ByRef rWasShrunk As Boolean, ByVal vShrinkOnly As Boolean) As System.Drawing.Bitmap 
 
    If vImage Is Nothing Then Return Nothing 
 
    Dim pPreviousSize As System.Drawing.Size = vImage.Size 
 
    If vNewSize.Height <= 0 OrElse vNewSize.Width <= 0 Then 
      rWasShrunk = False 
      vImage.SetResolution(96, 96) 
      Return vImage 
    End If 
 
    Dim pHorizontalRatio As Decimal = ccHelper.ToDecimal(pPreviousSize.Width) / ccHelper.ToDecimal(vNewSize.Width) 
    Dim pVerticalRatio As Decimal = ccHelper.ToDecimal(pPreviousSize.Height) / ccHelper.ToDecimal(vNewSize.Height) 
 
    Dim pRatio As Decimal = pHorizontalRatio 
    If pVerticalRatio > pHorizontalRatio Then 
      pRatio = pVerticalRatio 
    End If 
 
    If pRatio > 1 Then rWasShrunk = True Else rWasShrunk = False 
 
    If vShrinkOnly = True Then 
      If rWasShrunk = False Then 
        vImage.SetResolution(96, 96) 
        Return vImage 
      End If 
    End If 
 
    Dim pNewImage As System.Drawing.Bitmap = SizeImage(vImage, ccHelper.ToInteger(Decimal.Round(pPreviousSize.Width / pRatio, 0)), ccHelper.ToInteger(Decimal.Round(pPreviousSize.Height / pRatio, 0))) 
    pNewImage.SetResolution(96, 96) 
 
    Return pNewImage 
  End Function 
 
  Private Shared Function SizeImage(ByVal img As System.Drawing.Bitmap, ByVal width As Integer, ByVal height As Integer) As System.Drawing.Bitmap 
    'http://www.vbforums.com/showthread.php?383136-High-Quality-Image-Resizing 
    Dim newBit As New System.Drawing.Bitmap(width, height) 'new blank bitmap 
    Dim g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(newBit) 
    'change interpolation for reduction quality 
    g.CompositingQuality = Drawing.Drawing2D.CompositingQuality.HighQuality 
    g.SmoothingMode = Drawing.Drawing2D.SmoothingMode.HighQuality 
    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic 
    g.DrawImage(img, 0, 0, width, height) 
    Return newBit 
  End Function 
  
  Public Shared Function BitmapIsEqual(ByVal vBitmap1 As System.Drawing.Bitmap, ByVal vBitmap2 As System.Drawing.Bitmap) As Boolean 
    If vBitmap1.Size <> vBitmap2.Size Then 
      Return False 
    End If 
    For x = 0 To vBitmap1.Width - 1 
      For y = 0 To vBitmap1.Height - 1 
        If vBitmap1.GetPixel(x, y) <> vBitmap1.GetPixel(x, y) Then 
          Return False 
        End If 
      Next 
    Next 
    Return True 
  End Function 
 
  Public Shared Function BitmapIsEqual(ByVal vBitmapBytes1 As Byte(), ByVal vBitmapBytes2 As Byte()) As Boolean 
    Dim pStream As IO.Stream = New System.IO.MemoryStream(vBitmapBytes1) 
    Dim vBitmap1 As New System.Drawing.Bitmap(pStream, False) 
    pStream = New System.IO.MemoryStream(vBitmapBytes2) 
    Dim vBitmap2 As New System.Drawing.Bitmap(pStream, False) 
 
    Return BitmapIsEqual(vBitmap1, vBitmap2) 
  End Function 
 
  Public Shared Function InvertColour(ByVal c As System.Drawing.Color) As System.Drawing.Color 
    Return InvertColour(c.R, c.G, c.B) 
  End Function 
  Public Shared Function InvertColour(ByVal R As Integer, ByVal G As Integer, ByVal B As Integer) As System.Drawing.Color 
    Dim inv As System.Drawing.Color = System.Drawing.Color.FromArgb(255 - R, 255 - G, 255 - B) 
    Dim diff As Integer = 0 
    diff += Math.Abs(R - inv.R) 
    diff += Math.Abs(G - inv.G) 
    diff += Math.Abs(B - inv.B) 
    Dim factor As Double = diff / (255 * 3) 
    inv = System.Drawing.Color.FromArgb(ccHelper.ToInteger(Math.Round(inv.R * factor, 0, MidpointRounding.AwayFromZero)), ccHelper.ToInteger(Math.Round(inv.G * factor, 0, MidpointRounding.AwayFromZero)), ccHelper.ToInteger(Math.Round(inv.B * factor, 0, MidpointRounding.AwayFromZero))) 
    Return inv 
  End Function 
 
  Public Shared Function Zip(ByVal vText As String) As String 
    'http://www.neolisk.com/techblog/vbnet-zipandunzipstringusinggzipstream  
    'These are not exactly the same as CompressGZip and DeCompress gzip below, and they are 3% slower. 
    '  However, I'm keeping them for backwards compatibility 
    Dim buffer As Byte() = System.Text.Encoding.Unicode.GetBytes(vText) 
    Dim ms As New System.IO.MemoryStream() 
    Using zipStream As New System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, True) 
      zipStream.Write(buffer, 0, buffer.Length) 
    End Using 
    ms.Position = 0 
    Dim outStream As New System.IO.MemoryStream() 
    Dim compressed As Byte() = New Byte(ccHelper.ToInteger(ms.Length) - 1) {} 
    ms.Read(compressed, 0, compressed.Length) 
    Dim gzBuffer As Byte() = New Byte(compressed.Length + 3) {} 
    System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length) 
    System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4) 
    Return Convert.ToBase64String(gzBuffer) 
  End Function 
  Public Shared Function Zip(ByVal vByte As Byte()) As Byte() 
    'http://www.neolisk.com/techblog/vbnet-zipandunzipstringusinggzipstream  
    Dim ms As New System.IO.MemoryStream() 
    Using zipStream As New System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress, True) 
      zipStream.Write(vByte, 0, vByte.Length) 
    End Using 
    ms.Position = 0 
    Dim outStream As New System.IO.MemoryStream() 
    Dim compressed As Byte() = New Byte(ccHelper.ToInteger(ms.Length) - 1) {} 
    ms.Read(compressed, 0, compressed.Length) 
    Dim gzBuffer As Byte() = New Byte(compressed.Length + 3) {} 
    System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length) 
    System.Buffer.BlockCopy(BitConverter.GetBytes(vByte.Length), 0, gzBuffer, 0, 4) 
    Return gzBuffer 
  End Function 
 
  Public Shared Function UnZip(ByVal vCompressedText As String) As String 
    'http://www.neolisk.com/techblog/vbnet-zipandunzipstringusinggzipstream  
    Dim gzBuffer As Byte() = Convert.FromBase64String(vCompressedText) 
    Using ms As New System.IO.MemoryStream() 
      Dim msgLength As Integer = BitConverter.ToInt32(gzBuffer, 0) 
      ms.Write(gzBuffer, 4, gzBuffer.Length - 4) 
      Dim buffer As Byte() = New Byte(msgLength - 1) {} 
      ms.Position = 0 
      Using zipStream As New System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress) 
        zipStream.Read(buffer, 0, buffer.Length) 
      End Using 
      Return System.Text.Encoding.Unicode.GetString(buffer, 0, buffer.Length) 
    End Using 
  End Function 
  Public Shared Function UnZip(ByVal vCompressedBytes As Byte()) As Byte() 
    'http://www.neolisk.com/techblog/vbnet-zipandunzipstringusinggzipstream  
    Using ms As New System.IO.MemoryStream() 
      Dim msgLength As Integer = BitConverter.ToInt32(vCompressedBytes, 0) 
      ms.Write(vCompressedBytes, 4, vCompressedBytes.Length - 4) 
      Dim buffer As Byte() = New Byte(msgLength - 1) {} 
      ms.Position = 0 
      Using zipStream As New System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Decompress) 
        zipStream.Read(buffer, 0, buffer.Length) 
      End Using 
      Return buffer 
    End Using 
  End Function 
 
  Public Shared Function CompressGZip(ByVal vBytes As Byte()) As Byte() 
    'Gzip is 15-20% slower than Deflate 
    Dim pBytesToReturn As Byte() 
    Using pCompressedStream As New IO.MemoryStream() 
      Using gzip As New IO.Compression.GZipStream(pCompressedStream, IO.Compression.CompressionMode.Compress, True) 
        gzip.Write(vBytes, 0, vBytes.Length) 
        gzip.Close() 
      End Using 
      pBytesToReturn = pCompressedStream.ToArray() 
      pCompressedStream.Close() 
    End Using 
    Return pBytesToReturn 
  End Function 
  Public Shared Function DeCompressGZip(ByVal vBytes As Byte()) As Byte() 
    'https://social.msdn.microsoft.com/Forums/en-US/559405ae-4d2b-41c9-9881-897c47ef989d/compressing-and-decompressing-memorystream 
 
    Dim pBytesToReturn As Byte() 
    Using pCompresssedStream As New System.IO.MemoryStream() 
      pCompresssedStream.Write(vBytes, 0, vBytes.Length) 
      pCompresssedStream.Position = 0 
      ' Use the newly created memory stream for the compressed data. 
      Using gzip As New IO.Compression.GZipStream(pCompresssedStream, IO.Compression.CompressionMode.Decompress) 
        Using pUncompressedStream As New IO.MemoryStream 
          Dim pBuffer As Byte() = New Byte(63) {} 
          Dim pRead As Integer = -1 
          pRead = gzip.Read(pBuffer, 0, pBuffer.Length) 
          While pRead > 0 
            pUncompressedStream.Write(pBuffer, 0, pRead) 
            pRead = gzip.Read(pBuffer, 0, pBuffer.Length) 
          End While 
          pBytesToReturn = pUncompressedStream.ToArray 
          pUncompressedStream.Close() 
        End Using 
        gzip.Close() 
      End Using 
      pCompresssedStream.Close() 
    End Using 
    Return pBytesToReturn 
  End Function 
 
  ''' <summary> 
  ''' This creates a Zip using the folder name as the name of the Zip file, and puts it in a folder called 'zipped', under it 
  ''' </summary> 
  ''' <param name="vFolderToZip"></param> 
  ''' <returns></returns> 
  Public Shared Function ZipFolder(ByVal vFolderToZip As String) As String 
    If vFolderToZip Is Nothing OrElse String.IsNullOrWhiteSpace(vFolderToZip) Then Return "No vFolderToZip name received" 
 
    Try 
      vFolderToZip = vFolderToZip.Trim 
      'Get the folder name 
      If vFolderToZip.EndsWith("\", StringComparison.OrdinalIgnoreCase) Then 
        vFolderToZip = vFolderToZip.Substring(0, vFolderToZip.Length - 1) 
      End If 
      Dim pLastLocation As Integer = vFolderToZip.LastIndexOf("\") 
      Dim pRootFolder As String = vFolderToZip.Substring(0, pLastLocation) 
      Dim pFileName As String = vFolderToZip.Substring(pLastLocation + 1, vFolderToZip.Length - pLastLocation - 1) 
 
      Dim pFullFinalFileName As String = vFolderToZip & "\zipped\" & pFileName & ".zip" 
      If IO.File.Exists(pFullFinalFileName) Then IO.File.Delete(pFullFinalFileName) 
      Dim pFullTempFileName As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\" & pFileName & ".zip" 
      If IO.File.Exists(pFullTempFileName) Then IO.File.Delete(pFullTempFileName) 
 
      System.IO.Compression.ZipFile.CreateFromDirectory(vFolderToZip, pFullTempFileName) 
      IO.File.Copy(pFullTempFileName, pFullFinalFileName, True) 
 
      If IO.File.Exists(pFullTempFileName) Then IO.File.Delete(pFullTempFileName) 
    Catch ex As Exception 
      Return ex.Message 
    End Try 
 
    Return "OK" 
  End Function 
 
  ''' <summary> 
  ''' This looks for a file called update.zip. If it find it, it deletes everything but itself, expands the Zip file, and then deletes itself 
  ''' </summary> 
  ''' <param name="vZipFolder"></param> 
  ''' <returns></returns> 
  Public Shared Function UnZipUpdateFolder(ByVal vZipFolder As String) As String 
    If vZipFolder Is Nothing OrElse String.IsNullOrWhiteSpace(vZipFolder) Then Return "No vZipFolder name received" 
 
    Dim pFullUpdateFileName As String 
 
    Try 
      vZipFolder = vZipFolder.Trim 
      'Get the folder name 
      If vZipFolder.EndsWith("\", StringComparison.OrdinalIgnoreCase) Then 
        vZipFolder = vZipFolder.Substring(0, vZipFolder.Length - 1) 
      End If 
 
      pFullUpdateFileName = vZipFolder & "\update.zip" 
 
      Dim pFoundUpdate As Boolean = False 
      'scan files to find update zip 
      For Each l In IO.Directory.GetFiles(vZipFolder) 
        If l.Equals(pFullUpdateFileName, StringComparison.OrdinalIgnoreCase) Then 
          pFoundUpdate = True 
        End If 
      Next 
 
      If pFoundUpdate = False Then Return "update.zip not found" 
    Catch ex As Exception 
      Return ex.Message 
    End Try 
 
    Try 
      For Each l In IO.Directory.GetFiles(vZipFolder) 
        If Not l.Equals(pFullUpdateFileName, StringComparison.OrdinalIgnoreCase) Then 
          IO.File.Delete(l) 
        End If 
      Next 
 
      'now expand it 
      System.IO.Compression.ZipFile.ExtractToDirectory(pFullUpdateFileName, vZipFolder) 
 
      'Now delete it 
      IO.File.Delete(pFullUpdateFileName) 
    Catch ex As Exception 
      Return ex.Message 
    End Try 
 
    Return "OK" 
  End Function 
 
  ''' <summary> 
  ''' This creates a Zip of the file, using the name of the original file 
  ''' </summary> 
  ''' <param name="vFullFileName"></param> 
  ''' <returns></returns> 
  Public Shared Function ZipFile(ByVal vFullFileName As String) As String 
    If vFullFileName Is Nothing OrElse String.IsNullOrWhiteSpace(vFullFileName) Then Return "No vFullFileName name received" 
 
    Try 
      vFullFileName = vFullFileName.Trim 
      'Get the file name 
      Dim pLastLocation As Integer = vFullFileName.LastIndexOf("\") 
      Dim pRootFolder As String = vFullFileName.Substring(0, pLastLocation) 
      Dim pFileName As String = vFullFileName.Substring(pLastLocation + 1, vFullFileName.Length - pLastLocation - 1) 
      pLastLocation = pFileName.LastIndexOf(".") 
      Dim pFileNameNoExt As String = pFileName.Substring(0, pLastLocation) 
 
      Dim pTempFolder As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\D" & DateTime.Now.ToString("yyyyMMddTHHmmssffff") 
      Dim pFullTempFileName As String = pTempFolder & "\" & pFileNameNoExt & ".zip" 
      'copy it to a temporary folder 
      IO.File.Copy(vFullFileName, pTempFolder & "\" & pFileName, True) 
 
 
      'Create a zip to the location of the original file 
      System.IO.Compression.ZipFile.CreateFromDirectory(pTempFolder, pRootFolder & "\" & pFileNameNoExt & ".zip") 
 
      IO.Directory.Delete(pTempFolder, True) 
    Catch ex As Exception 
      Return ex.Message 
    End Try 
 
    Return "OK" 
  End Function 
 
  ''' <summary> 
  ''' This unzips a zip file in the folder in which it's located. 
  ''' </summary> 
  ''' <param name="vZipFilename"></param> 
  ''' <returns></returns> 
  Public Shared Function UnZipFile(ByVal vZipFilename As String) As String 
    If vZipFilename Is Nothing OrElse String.IsNullOrWhiteSpace(vZipFilename) Then Return "No vZipFilename name received" 
 
    Try 
      vZipFilename = vZipFilename.Trim 
      'Get the file name 
      Dim pLastLocation As Integer = vZipFilename.LastIndexOf("\") 
      Dim pRootFolder As String = vZipFilename.Substring(0, pLastLocation) 
 
      System.IO.Compression.ZipFile.ExtractToDirectory(vZipFilename, pRootFolder) 
    Catch ex As Exception 
      Return ex.Message 
    End Try 
 
    Return "OK" 
  End Function 
 
  Public Shared Function CompressDeflate(ByVal vBytes As Byte()) As Byte() 
 
    Dim pBytesToReturn As Byte() 
    Using pCompressedStream As New IO.MemoryStream() 
      Using gzip As New IO.Compression.DeflateStream(pCompressedStream, IO.Compression.CompressionMode.Compress, True) 
        gzip.Write(vBytes, 0, vBytes.Length) 
        gzip.Close() 
      End Using 
      pBytesToReturn = pCompressedStream.ToArray() 
      pCompressedStream.Close() 
    End Using 
    Return pBytesToReturn 
  End Function 
  Public Shared Function DeCompressDeflate(ByVal vBytes As Byte()) As Byte() 
    'https://social.msdn.microsoft.com/Forums/en-US/559405ae-4d2b-41c9-9881-897c47ef989d/compressing-and-decompressing-memorystream 
 
    Dim pBytesToReturn As Byte() 
    Using pCompresssedStream As New System.IO.MemoryStream() 
      pCompresssedStream.Write(vBytes, 0, vBytes.Length) 
      pCompresssedStream.Position = 0 
      ' Use the newly created memory stream for the compressed data. 
      Using gzip As New IO.Compression.DeflateStream(pCompresssedStream, IO.Compression.CompressionMode.Decompress) 
        Using pUncompressedStream As New IO.MemoryStream 
          Dim pBuffer As Byte() = New Byte(63) {} 
          Dim pRead As Integer = -1 
          pRead = gzip.Read(pBuffer, 0, pBuffer.Length) 
          While pRead > 0 
            pUncompressedStream.Write(pBuffer, 0, pRead) 
            pRead = gzip.Read(pBuffer, 0, pBuffer.Length) 
          End While 
          pBytesToReturn = pUncompressedStream.ToArray 
          pUncompressedStream.Close() 
        End Using 
        gzip.Close() 
      End Using 
      pCompresssedStream.Close() 
    End Using 
    Return pBytesToReturn 
  End Function 
 
  Public Shared Function DecimalMinutesToTimespan(ByVal vDecimalValue As Decimal) As TimeSpan 
    Dim pDays As Integer 
    Dim pHours As Integer 
    Dim pMinutes As Integer 
    Dim pSeconds As Integer 
    Dim pMilliSeconds As Integer 
 
    Dim pDecimalMinutes As Decimal = vDecimalValue 
 
    pDays = ccHelper.ToInteger(Math.Truncate(pDecimalMinutes / 60 / 24)) 
    pDecimalMinutes = pDecimalMinutes - (pDays * 24 * 60) 
 
    pHours = ccHelper.ToInteger(Math.Truncate(pDecimalMinutes / 60)) 
    pDecimalMinutes = pDecimalMinutes - (pHours * 60) 
 
    pMinutes = ccHelper.ToInteger(Math.Truncate(pDecimalMinutes)) 
    pDecimalMinutes = pDecimalMinutes - (pMinutes) 
 
    pSeconds = ccHelper.ToInteger(Math.Truncate(pDecimalMinutes * 60)) 
    pDecimalMinutes = pDecimalMinutes - (ccHelper.ToDecimal(pSeconds) / 60) 
 
    pMilliSeconds = ccHelper.ToInteger(Math.Truncate(pDecimalMinutes * 60 * 1000)) 
 
    Dim pTimespanValue As TimeSpan = New TimeSpan(pDays, pHours, pMinutes, pSeconds, pMilliSeconds) 
    Return pTimespanValue 
  End Function 
 
  Public Shared Function SaveFileToLogLocation(ByVal vData As String, ByVal vFileName As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("FileName={0}", vFileName) 
    Dim pFault As New clsFault 
 
    Try 
      'Prepare the variables  
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it  
 
      'Create the request  
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vData) 
          pBinaryWriter.Write(vFileName) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request  
      Dim pFunction As String = "ccHelperSaveFileToLogLocation" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, $"FileName: {vFileName}", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-150411-1847", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Friend Shared Function DoesAssemblyExist(ByVal vAssemblyName As String) As Boolean 
    Dim pFound As Boolean = False 
    For Each a In AppDomain.CurrentDomain.GetAssemblies() 
      'If a.FullName.IndexOf("HTTP") > 0 Then Stop 
      'If a.FullName.IndexOf("IIS") > 0 Then Stop 
      'If a.FullName.IndexOf("Web") > 0 Then Stop 
      If a.FullName.StartsWith(vAssemblyName, True, New System.Globalization.CultureInfo("en-US")) = True Then 
        pFound = True 
        Exit For 
      End If 
    Next 
    Return pFound 
  End Function 
  
  Friend Shared Function DoesAssemblyEndWith(ByVal vAssemblySuffix As String) As Boolean 
    Dim pFound As Boolean = False 
    For Each a In AppDomain.CurrentDomain.GetAssemblies() 
      If a.GetName.Name.EndsWith(vAssemblySuffix, StringComparison.OrdinalIgnoreCase) Then 
        pFound = True 
        Exit For 
      End If 
    Next 
    Return pFound 
  End Function 
 
  Public Shared Function GetControllerName() As String 
    If DoesAssemblyExist("TargCCOrders.DBController") Then 
      Return "DBController" 
    ElseIf DoesAssemblyExist("TargCCOrders.WSController") Then 
      Return "WSController" 
    Else 
      Return "None" 
    End If 
  End Function 
 
  Public Shared Function GetComputerName() As String 
 
    Dim pComputerName As String = "" 
    Try 
      Dim pIPGlobalProperties As Net.NetworkInformation.IPGlobalProperties = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties() 
      If String.IsNullOrEmpty(pIPGlobalProperties.DomainName) Then 
        pComputerName = $"{pIPGlobalProperties.HostName}" 
      Else 
        pComputerName = $"{pIPGlobalProperties.HostName}.{pIPGlobalProperties.DomainName}" 
      End If 
    Catch ex As Exception 
      If Environment.MachineName.Equals(Environment.UserDomainName, StringComparison.OrdinalIgnoreCase) Then 
        pComputerName = $"{Environment.MachineName}" 
      Else 
        pComputerName = $"{Environment.MachineName}.{Environment.UserDomainName}" 
      End If 
    End Try 
    Dim pUserName As String = "" 
    Try 
      pUserName = Environment.UserName 
    Catch ex As Exception 
      pUserName = "NoUser" 
    End Try 
    pComputerName = pUserName & "@" & pComputerName 
 
    Return pComputerName 
  End Function 
 
  Public Shared Function GetComputerDetails() As Dictionary(Of String, String)  
    Dim pComputerDetails As New Dictionary(Of String, String) 
 
    Dim pTry As String = "" 
    Try 
      Dim pSearcher As System.Management.ManagementObjectSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem") 
      Dim pResults As System.Management.ManagementObjectCollection = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        pTry = "OSName" 
        pComputerDetails.Add("OSName", GetManagementObject("Caption", l).Replace("Microsoft", "").Replace("Windows", "Win").Replace(" ", "") & " (" & GetManagementObject("Version", l) & " " & GetManagementObject("OSArchitecture", l) & ")") 
        pTry = "FreePhysicalMemory" 
        pComputerDetails.Add("FreePhysicalMemory", GetManagementObject("FreePhysicalMemory", l)) 
        pTry = "TotalPhysicalMemory" 
        pComputerDetails.Add("TotalPhysicalMemory", GetManagementObject("TotalVisibleMemorySize", l)) 
        Dim pLocalStartupTime As DateTime = DateTime.MinValue 
        Dim pSuccess As Boolean = DateTime.TryParseExact(GetManagementObject("LastBootUpTime", l).Split("-"c)(0).Split("+"c)(0), "yyyyMMddHHmmss.ffffff", System.Globalization.CultureInfo.CurrentCulture, Globalization.DateTimeStyles.AssumeLocal, pLocalStartupTime) 
        pTry = "LastBootUpTime" 
        If pSuccess Then 
          pComputerDetails.Add("LastBootUpTime", pLocalStartupTime.ToString("yyyy-MM-dd @ HH:mm:ss")) 
        Else 
          pComputerDetails.Add("LastBootUpTime", GetManagementObject("LastBootUpTime", l)) 
        End If 
      Next 
      pSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BIOS") 
      pResults = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        pTry = "BIOSVersion" 
        pComputerDetails.Add("BIOSVersion", GetManagementObject("Caption", l)) 
        pTry = "BIOSSerialNumber" 
        pComputerDetails.Add("BIOSSerialNumber", GetManagementObject("SerialNumber", l)) 
      Next 
      pSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard") 
      pResults = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        Dim pSerialNumber As String = GetManagementObject("SerialNumber", l) 
        If pSerialNumber = "NA" Then pSerialNumber = "" 
        If pSerialNumber = "UD" Then pSerialNumber = "" 
        If Not String.IsNullOrEmpty(pSerialNumber) AndAlso Not pComputerDetails("BIOSSerialNumber").Equals(pSerialNumber, StringComparison.OrdinalIgnoreCase) Then 
          If pComputerDetails("BIOSSerialNumber").IndexOf("Number", StringComparison.OrdinalIgnoreCase) > 0 Then 
            pComputerDetails("BIOSSerialNumber") = GetManagementObject("SerialNumber", l) 
          Else 
            pComputerDetails("BIOSSerialNumber") &= "|" & GetManagementObject("SerialNumber", l) 
          End If 
        End If 
        pTry = "Manufacturer" 
        pComputerDetails.Add("Manufacturer", GetManagementObject("Manufacturer", l)) 
        pTry = "Model" 
        pComputerDetails.Add("Model", GetManagementObject("Product", l)) 
      Next 
      pSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem") 
      pResults = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        pTry = "DNSHostName" 
        pComputerDetails.Add("DNSHostName", GetManagementObject("DNSHostName", l)) 
        pTry = "Domain" 
        pComputerDetails.Add("Domain", GetManagementObject("Domain", l)) 
        pTry = "Name" 
        pComputerDetails.Add("Name", GetManagementObject("Name", l)) 
      Next 
      pSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_VideoController") 
      pResults = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        If l("CurrentHorizontalResolution") IsNot Nothing Then 
          pTry = "VideoMode" 
          pComputerDetails.Add("VideoMode", GetManagementObject("CurrentHorizontalResolution", l) & " x " & GetManagementObject("CurrentVerticalResolution", l)) 
          Exit For 
        End If 
      Next 
      If Not pComputerDetails.ContainsKey("VideoMode") Then 
        pComputerDetails.Add("VideoMode", "VideoMode Not Provided") 
      End If 
      pSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive") 
      pResults = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        Dim pDiskIndex As String 
        pTry = "DiskIndex" 
        pDiskIndex = GetManagementObject("Index", l) 
        If pDiskIndex = "0" Then 
          pTry = "VolumeSerialNumber" 
          pComputerDetails.Add("VolumeSerialNumber", GetManagementObject("Caption", l) & "," & GetManagementObject("SerialNumber", l)) 
          Exit For 'want only the 1st 1      
        End If 
      Next 
      pSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk") 
      pResults = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        Dim pDeviceID As String 
        pTry = "DeviceID" 
        pDeviceID = GetManagementObject("DeviceID", l) 
        If pDeviceID.Equals("C:", StringComparison.OrdinalIgnoreCase) Then 
          Dim pSerNo As String = GetManagementObject("VolumeSerialNumber", l) 
          If pSerNo.Length > 5 Then pSerNo = pSerNo.Substring(0, 4) & "-" & pSerNo.Substring(4) 
          pComputerDetails("VolumeSerialNumber") &= "," & GetManagementObject("Caption", l) & "" & pSerNo 
          Exit For 'want only the 1st 1      
        End If 
      Next 
      pSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_Processor") 
      pResults = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        pTry = "Processor" 
        pComputerDetails.Add("Processor", GetManagementObject("Name", l)) 
        pTry = "ProcessorId" 
        pComputerDetails.Add("ProcessorId", GetManagementObject("ProcessorId", l)) 
        Exit For 'want only the 1st 1   
      Next 
      pSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration") 
      pResults = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        Dim pEnabled As Boolean = CBool(l("IPEnabled")) 
        If pEnabled = True Then 
          Dim pIPAddressList As String() = CType(l("IPAddress"), String()) 
          If GetManagementObject("MACAddress", l) = "" Then Continue For 
          If l("DefaultIPGateway") Is Nothing Then Continue For 
          Dim pIPAddresses As String = "" 
          For Each p In pIPAddressList 
            If p.IndexOf(".") < 0 Then Continue For 
            pIPAddresses &= p & ", " 
          Next 
          If pIPAddresses.EndsWith(", ") Then pIPAddresses = pIPAddresses.Substring(0, pIPAddresses.Length - 2) 
          pTry = "IPAddresses" 
          pComputerDetails.Add("IPAddresses", pIPAddresses) 
          pTry = "MACAddress" 
          pComputerDetails.Add("MACAddress", GetManagementObject("MACAddress", l)) 
          Exit For 
        End If 
      Next 
      pSearcher = New System.Management.ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter") 
      pResults = pSearcher.Get() 
      For Each l As System.Management.ManagementObject In pResults 
        If Not pComputerDetails.ContainsKey("MACAddress") Then Exit For 
        Dim pMAC As String = l("MACAddress")?.ToString() 
        If pMAC Is Nothing Then Continue For 
        If pMAC = pComputerDetails("MACAddress") Then 
          Dim pAdapterType As String = GetManagementObject("Name", l) 'AdapterType always returns Ethernet :-(, so I'll use name 
          pTry = "NetworkAdapterName" 
          If Not pComputerDetails.ContainsKey("AdapterType") Then 
            pComputerDetails.Add("AdapterType", pAdapterType) 
          Else 
            pComputerDetails("AdapterType") &= "&" & pAdapterType 
          End If 
        End If 
      Next 
      If pComputerDetails.ContainsKey("AdapterType") Then 
        If pComputerDetails("AdapterType").Length > 75 Then pComputerDetails("AdapterType") = pComputerDetails("AdapterType").Substring(0, 75) & "..." 
      End If 
      pTry = "" 
    Catch ex As Exception 
      Throw New Exception($"Failed reading {pTry}: {ex.Message}{Environment.NewLine}{ex.StackTrace}") 
    End Try 
 
    Return pComputerDetails  
  End Function  
  
  Private Shared Function GetManagementObject(ByVal vName As String, ByVal vManagementObject As System.Management.ManagementObject) As String 
    Dim pString As String 
 
    Try 
      If vManagementObject(vName) Is Nothing Then Return "UD" 
      pString = vManagementObject(vName).ToString() 
    Catch ex As Exception 
      pString = "UD" 
    End Try 
 
    Return pString 
  End Function 
 
  Public Structure FileDetails 
    Public ProductName As String 
    Public CompanyName As String 
    Public Version As String 
    Public AssemblyName As String 
    Public BinaryLocation As String 
  End Structure 
 
  Public Shared Function GetEntryAssemblyDetails() As FileDetails 
    Dim pFileDetails As New FileDetails 
    Dim pAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetEntryAssembly() 
    If pAssembly Is Nothing Then 
      pAssembly = System.Reflection.Assembly.GetCallingAssembly() 
    End If 
    Dim pFvi As FileVersionInfo = FileVersionInfo.GetVersionInfo(pAssembly.Location) 
    pFileDetails.ProductName = pFvi.ProductName 
    pFileDetails.CompanyName = pFvi.CompanyName 
    pFileDetails.Version = pFvi.FileVersion 
    Dim pAssemblyName As String = pAssembly.GetName().Name 
    pFileDetails.AssemblyName = pAssemblyName 
    pFileDetails.BinaryLocation = pAssembly.Location.Replace(pAssemblyName & ".exe", "").Replace(pAssemblyName & ".dll", "") 
    Return pFileDetails 
  End Function 
 
  Public Shared Function GetExecutingAssemblyDetails() As FileDetails 
    Dim pFileDetails As New FileDetails 
    Dim pAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly() 
    Dim pFvi As FileVersionInfo = FileVersionInfo.GetVersionInfo(pAssembly.Location) 
    pFileDetails.ProductName = pFvi.ProductName 
    pFileDetails.CompanyName = pFvi.CompanyName 
    pFileDetails.Version = pFvi.FileVersion 
    Dim pAssemblyName As String = pAssembly.GetName().Name 
    pFileDetails.AssemblyName = pAssemblyName 
    pFileDetails.BinaryLocation = pAssembly.Location.Replace(pAssemblyName & ".dll", "").Replace(pAssemblyName & ".exe", "") 
    Return pFileDetails 
  End Function 
 
  Public Shared Function WriteToRegistry(ByVal vKey As String, ByVal vValue As String, ByVal vRequester As clsRequester) As clsFault  
    Dim pFunctionParameters As String = String.Format("Key={0}, Value={1}", vKey, vValue)  
    Dim pFault As New clsFault 
 
    Dim pFileDetails As FileDetails = GetEntryAssemblyDetails() 
 
    Dim pRoot As String = "Software\" & pFileDetails.CompanyName & "\" & pFileDetails.ProductName 
 
    Try  
      Microsoft.Win32.Registry.CurrentUser.CreateSubKey(pRoot) 
      Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\" & pRoot, "Path", pFileDetails.BinaryLocation & "\" & pFileDetails.AssemblyName & ".exe") 
      Microsoft.Win32.Registry.SetValue("HKEY_CURRENT_USER\" & pRoot, vKey, vValue)  
    Catch ex As Exception  
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-150111-1904", vRequester)  
    End Try  
  
    Return pFault.SetOK()  
  End Function  
  
  Public Shared Function ReadFromRegistry(ByVal vKey As String, ByRef rValue As String, ByVal vRequester As clsRequester) As clsFault  
    Dim pFunctionParameters As String = String.Format("Key={0}", vKey)  
    Dim pFault As New clsFault 
 
    Dim pFileDetails As FileDetails = GetEntryAssemblyDetails() 
 
    Dim pRoot As String = "Software\" & pFileDetails.CompanyName & "\" & pFileDetails.ProductName 
 
    Try  
      rValue = Microsoft.Win32.Registry.GetValue("HKEY_CURRENT_USER\" & pRoot, vKey, "").ToString()  
    Catch ex As Exception  
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-150111-1914", vRequester)  
    End Try  
  
    Return pFault.SetOK()  
  End Function  
  
  ''' <summary> 
  ''' This uploads a ByteArray to the web service, to be saved as the file vFileNameToSaveAs. You can also add an ID which is sent with the file.  
  ''' </summary> 
  ''' <param name="vFileBytes"></param> 
  ''' <param name="vTask"></param> 
  ''' <param name="vID"></param> 
  ''' <param name="vFileNameToSaveAs"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function UploadBytesAsFile(ByVal vFileBytes As Byte(), ByVal vTask As String, ByVal vID As Long, ByVal vFileNameToSaveAs As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("vFileNameToSaveAs={0}", vFileNameToSaveAs) 
    Dim pFault As New clsFault 
 
    Dim pDestinationURL As String = MyController.GetConfigValueFromAppSetting("TargCCOrders.UploadFileURL") 
  
    'get extension    
    Dim pExtension As String = System.IO.Path.GetExtension(vFileNameToSaveAs).Substring(1)  
    pExtension = pExtension.ToLower() 
    
    If Not (vFileBytes.Length = 0 AndAlso pExtension.Equals("del")) Then 
      If ",exe,dll,".IndexOf($",{pExtension},", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        Return pFault.LogFreeTextFault("Invalid FileType received. Can not accept exe or dll's", pFunctionParameters, "TRGT-190212-1545", vRequester) 
      End If 
      'now double check  
      Dim pCalculatedExtension As String = GetFileExtension(vFileBytes, vFileNameToSaveAs, vRequester)  
      If pCalculatedExtension.Equals("ExeDll", StringComparison.OrdinalIgnoreCase) Then  
        Return pFault.LogFreeTextFault($"A binary (ExeDll) file was trying to be passed off as a {pExtension}. We can not accept exe or dll's", pFunctionParameters, "TRGT-240420-115205", vRequester)  
      End If 
      'Replace the extension 
      If (Not pCalculatedExtension.IndexOf(pExtension) >= 0) Then 
        pExtension = pCalculatedExtension 
        vFileNameToSaveAs = IO.Path.ChangeExtension(vFileNameToSaveAs, $".{pExtension}") 
      End If 
    End If 
 
    ' Create a new WebClient instance.   
    Dim pWebClient As New System.Net.WebClient() 
    pWebClient.QueryString.Add("ID", vID.ToString()) 
    pWebClient.QueryString.Add("Task", vTask) 
    pWebClient.QueryString.Add("FileNameToSaveAs", vFileNameToSaveAs) 
    pWebClient.QueryString.Add("WhatSent", "bytes") 
    'Ticket   
    Dim pTicket As String = vRequester.CreateTicket() 
    Dim bytes As Byte() = Text.Encoding.UTF8.GetBytes(pTicket) 
    Dim pTicketBase64 As String = Convert.ToBase64String(bytes) 
    pWebClient.QueryString.Add("TKT", pTicketBase64) 
    pWebClient.Credentials = vRequester.Credential 
 
    System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12 
 
    Tools.LogToTextFile.WriteMessage($"User: {vRequester.UserName}, FileSize: {vFileBytes.Length / 1024:#,##0} KB, URL: {pDestinationURL} (UploadBytesAsFile)", "UploadDetails") 
 
    ' Upload the file to the URL using the HTTP 1.2 POST.   
    Dim responseArray As Byte() 
    Try 
      responseArray = pWebClient.UploadData(pDestinationURL, "POST", vFileBytes) 
    Catch ex As Net.WebException 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-18121-1354", vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-181209-1859", vRequester) 
    End Try 
 
    Dim pResponse As String 
    pResponse = System.Text.Encoding.BigEndianUnicode.GetString(responseArray) 
 
    If pResponse.EndsWith(vbCrLf, StringComparison.OrdinalIgnoreCase) Then pResponse = pResponse.Substring(0, pResponse.Length - 2) 
 
    If pResponse <> "OK" Then 
      If pResponse.StartsWith("LoggedAlertID=", StringComparison.OrdinalIgnoreCase) Then 
        Dim pLoggedAlertID As Long = 0 
        Try 
          pLoggedAlertID = ccHelper.ToLong(pResponse.Split("="c)(1)) 
          Dim pLoggedAlert As New csLoggedAlert 
          pFault = pLoggedAlert.GetByID(pLoggedAlertID, vRequester, True) : If Not pFault.isOK Then Return pFault 
          pFault = New clsFault(pLoggedAlert) 
        Catch ex As Exception 
          pFault.LogException(ex, pFunctionParameters, "TRGT-181209-1902", vRequester) 
        End Try 
      ElseIf String.IsNullOrEmpty(pResponse) Then 
        pFault.LogFreeTextFault("File Upload Failed - Check text logs of web service. No response returned.", pFunctionParameters, "TRGT-181211-1407", vRequester) 
      Else 
        pFault.LogFreeTextFault("File Upload Failed - check text logs of web service: " & pResponse, pFunctionParameters, "TRGT-181209-1901", vRequester) 
      End If 
    Else 
      pFault.SetOK() 
    End If 
 
    Return pFault 
  End Function 
 
  Public Shared Function UploadBitmapAsFile(ByVal vBitmap As System.Drawing.Bitmap, ByVal vTask As String, ByVal vID As Long, ByVal vFileNameToSaveAs As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("vFileNameToSaveAs={0}", vFileNameToSaveAs) 
    Dim pFault As New clsFault 
 
 
    'Now convert to Byte Array  
    Dim pNewStream As New IO.MemoryStream 
    vBitmap.Save(pNewStream, System.Drawing.Imaging.ImageFormat.Jpeg) 
    Dim pNewByte As Byte() = pNewStream.ToArray 
 
 
    Dim pDestinationURL As String = MyController.GetConfigValueFromAppSetting("TargCCOrders.UploadFileURL") 
 
    'get extension    
    Dim pExtension As String = System.IO.Path.GetExtension(vFileNameToSaveAs).Substring(1) 
    pExtension = pExtension.ToLower() 
 
    If ",exe,dll,".IndexOf("," & pExtension & ",", StringComparison.OrdinalIgnoreCase) >= 0 Then 
      Return pFault.LogFreeTextFault("Invalid FileType received. Can not accept exe or dll's", pFunctionParameters, "TRGT-190212-1545", vRequester) 
    End If 
 
    ' Create a new WebClient instance.    
    Dim pWebClient As New System.Net.WebClient() 
    pWebClient.QueryString.Add("ID", vID.ToString()) 
    pWebClient.QueryString.Add("Task", vTask) 
    pWebClient.QueryString.Add("FileNameToSaveAs", vFileNameToSaveAs) 
    pWebClient.QueryString.Add("WhatSent", "bytes") 
    'Ticket    
    Dim pTicket As String = vRequester.CreateTicket() 
    Dim bytes As Byte() = Text.Encoding.UTF8.GetBytes(pTicket) 
    Dim pTicketBase64 As String = Convert.ToBase64String(bytes) 
    pWebClient.QueryString.Add("TKT", pTicketBase64) 
    pWebClient.Credentials = vRequester.Credential 
 
    System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12 
 
    Tools.LogToTextFile.WriteMessage($"User: {vRequester.UserName}, FileSize: {pNewByte.Length / 1024:#,##0} KB, URL: {pDestinationURL} (UploadBitmapAsFile)", "UploadDetails") 
 
    ' Upload the file to the URL using the HTTP 1.2 POST.    
    Dim responseArray As Byte() 
    Try 
      responseArray = pWebClient.UploadData(pDestinationURL, "POST", pNewByte) 
    Catch ex As Net.WebException 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-18121-1354", vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-181209-1859", vRequester) 
    End Try 
 
    Dim pResponse As String 
    pResponse = System.Text.Encoding.BigEndianUnicode.GetString(responseArray) 
 
    If pResponse.EndsWith(vbCrLf, StringComparison.OrdinalIgnoreCase) Then pResponse = pResponse.Substring(0, pResponse.Length - 2) 
 
    If pResponse <> "OK" Then 
      If pResponse.StartsWith("LoggedAlertID=", StringComparison.OrdinalIgnoreCase) Then 
        Dim pLoggedAlertID As Long = 0 
        Try 
          pLoggedAlertID = ccHelper.ToLong(pResponse.Split("="c)(1)) 
          Dim pLoggedAlert As New csLoggedAlert 
          pFault = pLoggedAlert.GetByID(pLoggedAlertID, vRequester, True) : If Not pFault.isOK Then Return pFault 
          pFault = New clsFault(pLoggedAlert) 
        Catch ex As Exception 
          pFault.LogException(ex, pFunctionParameters, "TRGT-181209-1902", vRequester) 
        End Try 
      ElseIf String.IsNullOrEmpty(pResponse) Then 
        pFault.LogFreeTextFault("File Upload Failed - Check text logs of web service. No response returned.", pFunctionParameters, "TRGT-181211-1407", vRequester) 
      Else 
        pFault.LogFreeTextFault("File Upload Failed - check text logs of web service: " & pResponse, pFunctionParameters, "TRGT-181209-1901", vRequester) 
      End If 
    Else 
      pFault.SetOK() 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary>  
  ''' This uploads a file on the file system named vFileName to the web service, to be saved as the file vFileNameToSaveAs. vTask tells the web service what to do with the file. You can also add an ID which is sent with the file.   
  ''' You can write code in the web service for your own task.  
  ''' </summary>  
  ''' <param name="vFileName"></param>  
  ''' <param name="vTask"></param>  
  ''' <param name="vID"></param>  
  ''' <param name="vFileNameToSaveAs"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Shared Function UploadFile(ByVal vFileName As String, ByVal vTask As String, ByVal vID As Long, ByVal vFileNameToSaveAs As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFunctionParameters As String = String.Format("FileName={0}", vFileName) 
    Dim pFault As New clsFault 
 
    Dim pDestinationURL As String = MyController.GetConfigValueFromAppSetting("TargCCOrders.UploadFileURL") 
  
    'get extension    
    Dim pExtension As String = System.IO.Path.GetExtension(vFileNameToSaveAs).Substring(1) 
    pExtension = pExtension.ToLower() 
    If ",exe,dll,".IndexOf($",{pExtension},", StringComparison.OrdinalIgnoreCase) >= 0 Then 
      Return pFault.LogFreeTextFault("Invalid FileType received. Can not accept exe or dll's", pFunctionParameters, "TRGT-190212-1545", vRequester) 
    End If 
    'now double check  
    Dim pFileBytes As Byte() = IO.File.ReadAllBytes(vFileName) 
    Dim pCalculatedExtension As String = GetFileExtension(pFileBytes, vFileNameToSaveAs, vRequester) 
    If pCalculatedExtension.Equals("ExeDll", StringComparison.OrdinalIgnoreCase) Then 
      Return pFault.LogFreeTextFault($"A binary (ExeDll) file was trying to be passed off as a {pExtension}. We can not accept exe or dll's", pFunctionParameters, "TRGT-240420-115205", vRequester) 
    End If 
    'Replace the extension 
    If (Not pCalculatedExtension.IndexOf(pExtension) >= 0) Then 
      pExtension = pCalculatedExtension 
      vFileNameToSaveAs = IO.Path.ChangeExtension(vFileNameToSaveAs, $".{pExtension}") 
    End If 
 
    ' Create a new WebClient instance.   
    Dim pWebClient As New System.Net.WebClient() 
    pWebClient.QueryString.Add("ID", vID.ToString()) 
    pWebClient.QueryString.Add("Task", vTask) 
    pWebClient.QueryString.Add("FileNameToSaveAs", vFileNameToSaveAs) 
    pWebClient.QueryString.Add("WhatSent", "file") 
    'Ticket   
    Dim pTicket As String = vRequester.CreateTicket() 
    Dim bytes As Byte() = Text.Encoding.UTF8.GetBytes(pTicket) 
    Dim pTicketBase64 As String = Convert.ToBase64String(bytes) 
    pWebClient.QueryString.Add("TKT", pTicketBase64) 
    pWebClient.Credentials = vRequester.Credential 
 
    'GetFileSize 
    Dim pFile As IO.FileInfo 
    Try 
      pFile = New IO.FileInfo(vFileName) 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-221130-1548", vRequester) 
    End Try 
 
    System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12 
 
    Tools.LogToTextFile.WriteMessage($"User: {vRequester.UserName}, FileSize: {pFile.Length / 1024:#,##0} KB, URL: {pDestinationURL} (UploadFile)", "UploadDetails") 
 
    ' Upload the file to the URL using the HTTP 1.2 POST.    
    Dim responseArray As Byte() 
    Try 
      responseArray = pWebClient.UploadFile(pDestinationURL, "POST", vFileName) 
    Catch ex As Net.WebException 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-18121-1354", vRequester) 
    Catch ex As Exception 
      Return pFault.LogException(ex, pFunctionParameters, "TRGT-181209-1859", vRequester) 
    End Try 
 
    Dim pResponse As String 
    pResponse = System.Text.Encoding.BigEndianUnicode.GetString(responseArray) 
 
    If pResponse.EndsWith(vbCrLf, StringComparison.OrdinalIgnoreCase) Then pResponse = pResponse.Substring(0, pResponse.Length - 2) 
 
    If pResponse <> "OK" Then 
      If pResponse.StartsWith("LoggedAlertID=", StringComparison.OrdinalIgnoreCase) Then 
        Dim pLoggedAlertID As Long = 0 
        Try 
          pLoggedAlertID = ccHelper.ToLong(pResponse.Split("="c)(1)) 
          Dim pLoggedAlert As New csLoggedAlert 
          pFault = pLoggedAlert.GetByID(pLoggedAlertID, vRequester, True) : If Not pFault.isOK Then Return pFault 
          pFault = New clsFault(pLoggedAlert) 
        Catch ex As Exception 
          pFault.LogException(ex, pFunctionParameters, "TRGT-181209-1902", vRequester) 
        End Try 
      ElseIf String.IsNullOrEmpty(pResponse) Then 
        pFault.LogFreeTextFault("File Upload Failed - Check text logs of web service. No response returned.", pFunctionParameters, "TRGT-181211-1407", vRequester) 
      Else 
        pFault.LogFreeTextFault("File Upload Failed - check text logs of web service: " & pResponse, pFunctionParameters, "TRGT-181209-1901", vRequester) 
      End If 
    Else 
      pFault.SetOK() 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' This creates a URL to access the file saved via 'UploadFile'. 
  ''' </summary> 
  ''' <param name="vTask"></param> 
  ''' <param name="vFileName"></param> 
  ''' <returns></returns> 
  Public Shared Function CreateURL(ByVal vTask As String, ByVal vFileName As String) As String 
    Dim pFault As New clsFault 
 
    Dim pDestinationURL As String = MyController.GetConfigValueFromAppSetting("TargCCOrders.DownloadFileURL") 
    If Not pDestinationURL.EndsWith("/", StringComparison.OrdinalIgnoreCase) Then pDestinationURL &= "/" 
    If Not String.IsNullOrEmpty(vTask) Then vTask &= "/" 
    pDestinationURL &= vTask & vFileName 
 
    Return pDestinationURL 
  End Function 
 
  ''' <summary>  
  ''' This creates a URL to access the file saved via 'UploadFile'.  
  ''' </summary>  
  ''' <param name="vTask"></param>  
  ''' <param name="vFileName"></param>  
  ''' <returns></returns>  
  Public Shared Function DownloadToTemp(ByVal vTask As String, ByVal vFileName As String, ByRef rFullDownloadedFileName As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault() 
 
    Dim pDestinationURL As String = CreateURL(vTask, vFileName) 
 
    Dim pWebClient As New System.Net.WebClient() 
    pWebClient.Headers.Add("Browser", "TargCC") 
 
    Try 
      'Credentials 
      If Not String.IsNullOrEmpty(MyController.ApplicationName) Then 
        pWebClient.Credentials = vRequester.Credential 
      End If 
      rFullDownloadedFileName = System.IO.Path.GetTempPath & vFileName 
      pWebClient.DownloadFile(pDestinationURL, rFullDownloadedFileName) 
    Catch ex As Exception 
      If ex.Message.IndexOf("(404)", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        pFault.LogFreeTextFault(65, $"URL: {pDestinationURL}", "", "TRGT-220511-1537", vRequester) 
      ElseIf ex.InnerException IsNot Nothing AndAlso ex.InnerException.Message.IndexOf("process cannot access the file", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        pFault.LogFreeTextFault(159, $"Download from: {pDestinationURL} to {rFullDownloadedFileName}", ex.InnerException.Message, "TRGT-240425-140424", vRequester) 
      Else 
        pFault.LogException(ex, $"Download from: {pDestinationURL} to {rFullDownloadedFileName}", "TRGT-210318-1134", vRequester) 
      End If 
      Return pFault 
    End Try 
 
    Return pFault.SetOK() 
  End Function 
 
  Public Shared Function DownloadFileSecurely(ByVal vTask As String, ByVal vFileName As String, ByRef rFullDownloadedFileName As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As New clsFault() 
 
    Dim pDestinationURL As String 
 
    Dim pSSL As String = "s" 
    If Not MyController.ServerRequiresSSL Then pSSL = "" 
    pDestinationURL = $"http{pSSL}://{MyController.ServerApplicationRoot(MyController.APIServerNumber)}/CC/FileServe.aspx" 
 
    Dim pWebClient As New System.Net.WebClient() 
    pWebClient.QueryString.Add("FileName", vFileName) 
    pWebClient.QueryString.Add("Task", vTask) 
    'Ticket    
    Dim pTicket As String = vRequester.CreateTicket() 
    Dim bytes As Byte() = Text.Encoding.UTF8.GetBytes(pTicket) 
    Dim pTicketBase64 As String = Convert.ToBase64String(bytes) 
    pWebClient.QueryString.Add("TKT", pTicketBase64) 
    pWebClient.Credentials = vRequester.Credential 
 
    pWebClient.Headers.Add("Browser", "TargCC") 
 
    Try 
      'Credentials  
      If Not String.IsNullOrEmpty(MyController.ApplicationName) Then 
        pWebClient.Credentials = vRequester.Credential 
      End If 
 
      'encrypt the name 
      Dim pFileNameEnc As String = vFileName 
      Dim pNames As String() = vFileName.Split("."c) 
      If pNames.Length = 2 Then 
        pFileNameEnc = $"{ccHelper.Encrypt(enmHashType.SHA1, "TargCCOrders")}{ccHelper.Encrypt(enmHashType.SHA256, pNames(0))}.{pNames(1)}" 
      End If  
 
      'Download the data 
      rFullDownloadedFileName = System.IO.Path.GetTempPath & pFileNameEnc 
      pWebClient.DownloadFile(pDestinationURL, rFullDownloadedFileName) 
 
      'now check the received file 
      Dim pFileBytes As Byte() = IO.File.ReadAllBytes(rFullDownloadedFileName) 
 
      If pFileBytes.Length = 0 Then 
        pFault.LogFreeTextFault(65, $"Download from: {pDestinationURL} to {rFullDownloadedFileName} failed (file exists with size = 0). Check the text logs on the server", "", "TRGT-240527-094238-143410", vRequester) 
        Try 
          IO.File.Delete(rFullDownloadedFileName) 
        Catch ex As Exception 
        End Try 
        Return pFault 
      End If 
 
      Dim pCalculatedExtension As String = GetFileExtension(pFileBytes, vFileName, vRequester) 
      If pCalculatedExtension.Equals("ExeDll", StringComparison.OrdinalIgnoreCase) Then 
        Return pFault.LogFreeTextFault($"A binary (ExeDll) file was downloaded. We can not accept exe or dll's", "", "TRGT-240420-115918", vRequester) 
      End If 
 
      If Not pCalculatedExtension.Equals("DocXls", StringComparison.OrdinalIgnoreCase) Then 
        'get extension      
        Dim pExtension As String = System.IO.Path.GetExtension(rFullDownloadedFileName) 
        pExtension = pExtension.ToLower() 
        'Replace the extension   
        If pCalculatedExtension.Length > 0 Then 
          If (Not pCalculatedExtension.IndexOf(pExtension) >= 0) OrElse String.IsNullOrWhiteSpace(pExtension) Then 
            pExtension = pCalculatedExtension 
            Dim pNewFileName As String = IO.Path.ChangeExtension(rFullDownloadedFileName, $".{pExtension}") 
            IO.File.Move(rFullDownloadedFileName, pNewFileName) 
            rFullDownloadedFileName = pNewFileName 
          End If 
        End If 
      End If 
 
    Catch ex As Exception 
      If ex.Message.IndexOf("(404)", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        pFault.LogFreeTextFault(65, $"URL: {pDestinationURL}", "", "TRGT-220511-1537", vRequester) 
      ElseIf ex.InnerException IsNot Nothing AndAlso ex.InnerException.Message.IndexOf("process cannot access the file", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        pFault.LogFreeTextFault(159, $"Download from: {pDestinationURL} to {rFullDownloadedFileName}", ex.InnerException.Message, "TRGT-240425-140424", vRequester) 
      Else 
        pFault.LogException(ex, $"Download from: {pDestinationURL} to {rFullDownloadedFileName}", "TRGT-210318-1134", vRequester) 
      End If 
      Return pFault 
    End Try 
 
    If Not IO.File.Exists(rFullDownloadedFileName) Then 
      Return pFault.LogFreeTextFault(65, $"Download from: {pDestinationURL} failed. Check the text logs on the server", "", "TRGT-220220-142629", vRequester) 
    End If 
 
    Return pFault.SetOK 
  End Function 
 
  Public Shared Function DownloadFileAsByteArraySecurely(ByVal vTask As String, ByVal vFileName As String, ByRef rBytes As Byte(), ByRef rFileExtension As String, ByVal vRequester As clsRequester) As clsFault  
    Dim pFault As New clsFault()  
  
    rBytes = Nothing 
 
    Dim pDestinationURL As String 
 
    Dim pSSL As String = "s" 
    If Not MyController.ServerRequiresSSL Then pSSL = "" 
    pDestinationURL = $"http{pSSL}://{MyController.ServerApplicationRoot(MyController.APIServerNumber)}/CC/FileServe.aspx" 
 
    Dim pWebClient As New System.Net.WebClient() 
    pWebClient.QueryString.Add("FileName", vFileName) 
    pWebClient.QueryString.Add("Task", vTask) 
    'Ticket     
    Dim pTicket As String = vRequester.CreateTicket() 
    Dim bytes As Byte() = Text.Encoding.UTF8.GetBytes(pTicket) 
    Dim pTicketBase64 As String = Convert.ToBase64String(bytes) 
    pWebClient.QueryString.Add("TKT", pTicketBase64) 
    pWebClient.Credentials = vRequester.Credential 
 
    pWebClient.Headers.Add("Browser", "TargCC") 
 
    Try 
      'Credentials   
      If Not String.IsNullOrEmpty(MyController.ApplicationName) Then 
        pWebClient.Credentials = vRequester.Credential 
      End If 
 
      'encrypt the name  
      Dim pFileNameEnc As String = vFileName 
      Dim pNames As String() = vFileName.Split("."c) 
      If pNames.Length = 2 Then 
        pFileNameEnc = $"{ccHelper.Encrypt(enmHashType.SHA256, pNames(0))}.{pNames(1)}" 
      End If 
 
      'Download the data  
      'rFullDownloadedFileName = System.IO.Path.GetTempPath & pFileNameEnc 
      'pWebClient.DownloadFile(pDestinationURL, rFullDownloadedFileName) 
 
      'use for image  
      rBytes = pWebClient.DownloadData(pDestinationURL) 
 
      Dim pExtension As String = GetFileExtension(rBytes, vFileName, vRequester) 
      If pExtension.Equals("ExeDll", StringComparison.OrdinalIgnoreCase) Then 
        Return pFault.LogFreeTextFault(65, $"Download from: {pDestinationURL} failed, as it tried to return an exe or dll file. This is not allowed.", "", "TRGT-240420-112718", vRequester) 
      End If 
 
      rFileExtension = pExtension 
 
    Catch ex As Exception 
      If ex.Message.IndexOf("(404)", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        pFault.LogFreeTextFault(65, $"URL: {pDestinationURL}", "", "TRGT-220511-1537", vRequester) 
      ElseIf ex.InnerException IsNot Nothing AndAlso ex.InnerException.Message.IndexOf("process cannot access the file", StringComparison.OrdinalIgnoreCase) >= 0 Then 
        pFault.LogFreeTextFault(159, $"Download from: {pDestinationURL}", ex.InnerException.Message, "TRGT-240425-143546", vRequester) 
      Else 
        pFault.LogException(ex, $"Download from: {pDestinationURL}", "TRGT-240425-143610", vRequester) 
      End If 
      Return pFault  
    End Try  
 
    If rBytes Is Nothing OrElse rBytes.Length = 0 Then 
      Return pFault.LogFreeTextFault(65, $"Download from: {pDestinationURL} failed (no bytes returned). Check the text logs on the server", "", "TRGT-240420-102714", vRequester) 
    End If 
 
    Return pFault.SetOK 
  End Function 
 
  Public Shared Function GetFileExtension(vBytes As Byte(), vFileName As String, vRequester As clsRequester) As String
    ' Check if the input byte array is valid and contains at least 4 bytes 
    If vBytes Is Nothing OrElse vBytes.Length < 4 Then 
      Return String.Empty 
    End If 
 
    ' Define a dictionary to map file signatures to file extensions 
    Dim fileSignatures As New Dictionary(Of String, String) From { 
        {"424D", "bmp"}, 
        {"4C01", "cab"}, 
        {"FFD8FFE0", "jpg"}, 
        {"FFD8FFE1", "jpg"}, 
        {"FFD8FFE8", "jpg"}, 
        {"89504E47", "png"}, 
        {"47494638", "gif"}, 
        {"25504446", "pdf"}, 
        {"D0CF11E0", "DocXls"}, 
        {"504B0304", "zip"}, 
        {"504B0506", "zip"}, 
        {"504B0708", "zip"}, 
        {"52617221", "rar"}, 
        {"57415645", "wav"}, 
        {"4D546864", "mid"}, 
        {"49492A00", "tif"}, 
        {"4D4D002A", "tif"}, 
        {"4944332E", "mp3"}, 
        {"000001BA", "mpg"}, 
        {"000001B3", "mpg"}, 
        {"6D6F6F76", "mov"}, 
        {"2E7261FD", "ra"}, 
        {"2E524D46", "rm"}, 
        {"4D5A9000", "ExeDll"}, 
        {"E11AB1A1", "bat"}, 
        {"7B5C7274", "rtf"}, 
        {"4F676753", "ogg"}, 
        {"49536328", "iso"}, 
        {"3C3F786D", "xml"}, 
        {"4D4D002B", "tif"}, 
        {"FFD8FFDB", "jpg"}, 
        {"1A45DFA3", "webm"}, 
        {"0000001C", "mp4"}, 
        {"00000024", "mp4"}, 
        {"00000020", "mp4"} 
    } 
    ' The last two, regarding Scanovate, were added by Ramy 
 
    ' Convert the first 4 bytes of the file to a hexadecimal string  
    Dim fileSignature As String = BitConverter.ToString(vBytes, 0, 4).Replace("-", String.Empty).ToUpperInvariant() 
 
    ' Check if the file signature is in the dictionary  
    If fileSignatures.ContainsKey(fileSignature) Then 
      Dim pExtension = fileSignatures(fileSignature) 
 
      If pExtension.Equals("DocXls", StringComparison.OrdinalIgnoreCase) Then '(from https://claude.ai/)  
        If vBytes(28) = &H3E Then 
          pExtension = "doc" 
        Else 
          pExtension = "xls" 
        End If 
      End If 
      Return pExtension 
    Else 
      Dim pFault As New clsFault 
      pFault.LogFreeTextFault(3, $"Unknown File extension for file '{vFileName}' received with fileSignature of '{fileSignature}'", $"Name: {vFileName}, Size {vBytes.Length}", "TRGT-240605-1950", vRequester) 
      Return String.Empty 
    End If 
 
  End Function 
 
  ''' <summary> 
  ''' Creates a phone number in international format based on a local number 
  ''' </summary> 
  ''' <param name="vLocalPhoneNumber"></param> 
  ''' <param name="rInternationalNumber"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function CreateInternationalPhoneNumber(ByVal vLocalPhoneNumber As String, ByRef rInternationalNumber As String, ByVal vRequester As clsRequester) As clsFault 
    Dim pParameters As String = $"LocalPhoneNumber: {vLocalPhoneNumber}" 
    Dim pFault As clsFault = Nothing 
 
    Static sLocalNumberIdentifierForSMS As String = "" 
    Static sCountryCodeForSMS As String = "" 
 
    vLocalPhoneNumber = vLocalPhoneNumber.Trim() 
 
    If sLocalNumberIdentifierForSMS = "" Then 
      Dim pSystemDefaults As New csSystemDefaultCol 
      pFault = pSystemDefaults.FillByGroup("Defaults", vRequester) : If Not pFault.isOK() Then Return pFault 
      sLocalNumberIdentifierForSMS = pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Defaults_LocalNumberIdentifierForSMS).SettingValue 
      sCountryCodeForSMS = pSystemDefaults.FindByFullSettingName(csSystemDefault.enmFullSettingName.Defaults_CountryCodeForSMS).SettingValue 
    End If 
 
    If sLocalNumberIdentifierForSMS = "0" AndAlso vLocalPhoneNumber.StartsWith("0", StringComparison.OrdinalIgnoreCase) Then 
      rInternationalNumber = "+" & sCountryCodeForSMS & vLocalPhoneNumber.Substring(1) 
      If pFault Is Nothing Then pFault = New clsFault() 
      Return pFault.SetOK() 
    End If 
 
    If sLocalNumberIdentifierForSMS = "10" AndAlso vLocalPhoneNumber.Length = 10 Then 
      rInternationalNumber = "+" & sCountryCodeForSMS & vLocalPhoneNumber 
      If pFault Is Nothing Then pFault = New clsFault() 
      Return pFault.SetOK() 
    End If 
 
    rInternationalNumber = "+" & vLocalPhoneNumber 
 
    If pFault Is Nothing Then pFault = New clsFault() 
    Return pFault.SetOK() 
  End Function 
 
  ''' <summary> 
  ''' Cleans up a phone number and checks validity 
  ''' </summary> 
  ''' <param name="vPhoneNumber"></param> 
  ''' <returns></returns> 
  Public Shared Function CleanUpPhoneNumber(vPhoneNumber As String) As String 
    Dim pPhoneNumber As String = vPhoneNumber 
 
    If String.IsNullOrEmpty(pPhoneNumber) Then Return "" 
 
    pPhoneNumber = pPhoneNumber.Replace(" ", "").Replace("-", "").Replace("+", "").Replace("(", "").Replace(")", "") 
 
    If String.IsNullOrEmpty(pPhoneNumber) Then Return "" 
 
    If Not ccHelper.IsNumeric(pPhoneNumber) Then Return "Invalid-Phone number must be numeric" 
 
    If pPhoneNumber.Length < 8 Then Return "Invalid-Phone number must have at least 8 digits" 
 
    Return pPhoneNumber 
  End Function 
 
  ''' <summary>   
  ''' If the destination include a '@' then it sends an email. If it's an SMS, then if the number is from out of country, it should be in international format, with a '+' prefix  
  ''' If there are multiple emails, delimit them with a ';' or NewLine and do not send a FullName.    
  ''' </summary>   
  ''' <param name="vMessage"></param>   
  ''' <param name="vDestinationEmailOrNumber"></param>   
  ''' <param name="vRequester"></param>   
  ''' <param name="vFullName"></param>   
  ''' <param name="vLanguage"></param>   
  ''' <param name="vSubject"></param>   
  ''' <returns></returns>   
  Public Shared Function SendSMSorEmail(ByVal vMessage As String, ByVal vDestinationEmailOrNumber As String, ByVal vRequester As clsRequester, Optional ByVal vFullName As String = "", Optional ByVal vLanguage As clsEnums.enmLanguage = clsEnums.enmLanguage.en, Optional vSubject As String = "") As clsFault 
    Dim pFault As New clsFault 
    Dim pFunctionParameters As String = $"DestinationEmailOrNumber: {vDestinationEmailOrNumber}, Message.Length: {vMessage.Length}" 
 
    If String.IsNullOrEmpty(vMessage) Then Return (pFault.LogFreeTextFault(310, "Missing a message", pFunctionParameters, "TRGT-2307017-1557", vRequester)) 
    If String.IsNullOrEmpty(vDestinationEmailOrNumber) Then Return (pFault.LogFreeTextFault(310, "Missing a vestinationEmailOrNumber", pFunctionParameters, "TRGT-2307017-1558", vRequester)) 
 
    Try 
      'Prepare the variables   
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 'Send it, but don't need it   
 
      'Create the request   
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New System.IO.BinaryWriter(pMemoryStream) 
          pBinaryWriter.Write(vMessage) 
          pBinaryWriter.Write(vDestinationEmailOrNumber) 
          pBinaryWriter.Write(vFullName) 
          pBinaryWriter.Write(vLanguage.FastToString()) 
          pBinaryWriter.Write(vSubject) 
          pBinaryWriter.Close() 
        End Using 
        pRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
 
      'Run the request   
      Dim pFunction As String = "ccHelperSendSMSorEmail" 
      pFault = WebAPI.RunAPI(pFunction, pRequest, $"DestinationEmailOrNumber: {vDestinationEmailOrNumber}", pResponse, vRequester) : If Not pFault.isOK Then Return pFault 
 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-230717-1604", vRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  Public Shared Function CreateFriendlyTextFromHungarianNotation(ByVal vHungarianNotation As String) As String 
    Dim pLabel As String = "" 
    Dim pPrevChar As Char = "."c 
    Dim pPrevPrevChar As Char = "."c 
 
    If String.IsNullOrWhiteSpace(vHungarianNotation) Then Return "" 
 
    If (vHungarianNotation.IndexOf(" ") > 0) Then Return vHungarianNotation 'already not HungarianNotation 
    If (vHungarianNotation.IndexOf(".") > 0) Then Return vHungarianNotation 'it's some kind of code 
 
    If vHungarianNotation.StartsWith("c_", StringComparison.OrdinalIgnoreCase) Then vHungarianNotation = vHungarianNotation.Substring(2) 
 
    Dim pCharArray As Char() = vHungarianNotation.ToCharArray 
 
    'Don't do file names
    If pCharArray.Length > 6 AndAlso pCharArray(pCharArray.Length - 4) = "."c AndAlso pCharArray(pCharArray.Length - 3) <> "."c AndAlso pCharArray(pCharArray.Length - 5) <> "."c Then Return vHungarianNotation 
 
    For Each pChar As Char In vHungarianNotation.ToCharArray 
      If pChar = Char.ToUpperInvariant(pChar) Then 
        If pPrevChar <> Char.ToUpperInvariant(pPrevChar) Then 
          pLabel &= " " 
        End If 
      Else 
        If pPrevChar <> "."c AndAlso pPrevChar = Char.ToUpperInvariant(pPrevChar) Then 
          If pPrevPrevChar <> "."c AndAlso pPrevPrevChar = Char.ToUpperInvariant(pPrevPrevChar) Then 
            If ccHelper.IsLatin(pPrevChar) AndAlso ccHelper.IsLatin(pPrevPrevChar) Then 
              pLabel = pLabel.Substring(0, pLabel.Length - 1) & " " & pLabel.Substring(pLabel.Length - 1) 
            End If 
          End If 
        End If 
      End If 
      pPrevPrevChar = pPrevChar 
      pPrevChar = pChar 
      pLabel &= pChar 
    Next 
    pLabel = pLabel.Trim.Replace("  ", " ").Replace("  ", " ") 
    Return pLabel 
  End Function 
 
  '''' <summary>  
  '''' This reports on all the properties and their values in an object. It is a deep scan (it also checks objects that are in the object)  
  '''' </summary>  
  '''' <param name="vObjectToScan"></param>  
  '''' <returns></returns>  
  'Public Shared Function ScanObject(ByVal vObjectToScan As Object) As String 
  '  If vObjectToScan Is Nothing Then Return "" 
 
  '  Dim pValues As New Text.StringBuilder() 
  '  pValues.AppendLine($"Object: {vObjectToScan.GetType.Name}") 
  '  pValues.AppendLine($"{New String("="c, vObjectToScan.GetType.Name.Length + 8)}") 
  '  If vObjectToScan.GetType Is GetType(Dictionary(Of String, String)) Then 
  '    Dim pDic As Dictionary(Of String, String) = CType(vObjectToScan, Dictionary(Of String, String)) 
  '    For Each l In pDic 
  '      pValues.AppendLine($"{l.Key} = {l.Value}") 
  '    Next 
  '  ElseIf System.ComponentModel.TypeDescriptor.GetProperties(vObjectToScan).Count = 1 AndAlso System.ComponentModel.TypeDescriptor.GetProperties(vObjectToScan)(0).Name.Equals("length", StringComparison.OrdinalIgnoreCase) Then 
  '    pValues.Append($"{vObjectToScan.GetType.Name} = {vObjectToScan.ToString()}") 
  '  Else 
  '    For Each descriptor As System.ComponentModel.PropertyDescriptor In System.ComponentModel.TypeDescriptor.GetProperties(vObjectToScan) 
  '      Dim pName As String = descriptor.Name 
  '      Dim oValue As Object = descriptor.GetValue(vObjectToScan) 
  '      Dim pType As String = descriptor.PropertyType.Name 
  '      Dim pValue As String = "" 
  '      If oValue Is Nothing Then 
  '        pValue = "null" 
  '      Else 
  '        pValue = descriptor.GetValue(vObjectToScan).ToString().Replace(Environment.NewLine, ccHelper.NewLine) 
  '      End If 
  '      If descriptor.Attributes.Count = 0 Then 
  '        pValues.Append($"{pName} ({pType}) = {ScanObject(descriptor.GetValue(vObjectToScan))}") 
  '        pValues.AppendLine($"    ==== End {pName}") 
  '      Else 
  '        If Not (String.IsNullOrEmpty(pValue.Trim) OrElse pValue.Trim.Equals("null", StringComparison.OrdinalIgnoreCase)) Then 
  '          pValues.AppendLine($"{pName} ({pType}) = {pValue}") 
  '        End If 
  '      End If 
  '    Next 
  '  End If 
 
  '  Return pValues.ToString() 
  'End Function 
 
 
  ''hide empty values As a flag, replace scanobject And Get rid Of Object To text 
 
  Public Shared Function ToStringCC(ByVal vObject As Object, Optional vShowAllProperties As Boolean = False) As String 
    Return ToStringCC(vObject, 0, Nothing, vShowAllProperties) 
  End Function 
 
  Private Shared Function ToStringCC(vObject As Object, vIndentLevel As Integer, vName As String, vShowAllProperties As Boolean) As String 
    If vIndentLevel >= 100 Then Return "" 
 
    'Private Shared Function ScanObject(obj As Object, Optional indentLevel As Integer = 0, Optional name As String = Nothing) As String  
    Dim indent As String = New String(" "c, vIndentLevel * 2) 
 
    ' null  
    If vObject Is Nothing Then 
      Return indent & If(vName, "<object>") & ": <null>" 
    End If 
 
    Dim type As Type = vObject.GetType() 
    Dim typeName As String = GetFriendlyTypeName(type) 
 
    ' primitive / simple  
    If IsLeaf(type, vObject) Then 
      Return indent & If(vName, typeName) & ": " & FormatLeaf(vObject) 
    End If 
 
    ' enumerable (but not string)  
    If GetType(IEnumerable).IsAssignableFrom(type) AndAlso Not TypeOf vObject Is String Then 
      Dim items As New List(Of String)() 
      For Each item In CType(vObject, IEnumerable) 
        Dim pstrg As String = ToStringCC(item, vIndentLevel + 1, Nothing, vShowAllProperties) 
        If pstrg = "" Then Return "" 
        items.Add(pstrg) ' elements have no explicit name  
      Next 
 
      Dim sb As New StringBuilder() 
      Dim headerName As String = If(vName, typeName)  ' show "Phones" or "String[]" at top-level  
      sb.Append(indent & headerName & ": [") 
      If items.Count > 0 Then 
        sb.Append(Environment.NewLine) 
        sb.Append(String.Join("," & Environment.NewLine, items)) 
      End If 
      sb.Append("]") 
      Return sb.ToString() 
    End If 
 
    ' complex object  
    Dim props As PropertyInfo() = type.GetProperties(BindingFlags.Public Or BindingFlags.Instance) 
    Dim lines As New List(Of String)(props.Length) 
 
    For Each prop As PropertyInfo In props 
      If prop Is Nothing Then Continue For 
 
      Dim value As Object = Nothing 
      Try 
        value = prop.GetValue(vObject, Nothing) 
      Catch 
        ' ignore getters that throw/inaccessible  
      End Try 
 
      If Not vShowAllProperties Then 
        If prop.Name.Equals("IsCleanForXML", StringComparison.OrdinalIgnoreCase) Then Continue For 
        If prop.Name.Equals("ccStatus", StringComparison.OrdinalIgnoreCase) Then Continue For 
        If prop.Name.Equals("HasParents", StringComparison.OrdinalIgnoreCase) Then Continue For 
        If prop.Name.Equals("WithParents", StringComparison.OrdinalIgnoreCase) Then Continue For 
        If prop.Name.Equals("IsEmpty", StringComparison.OrdinalIgnoreCase) Then Continue For 
        If prop.Name.Equals("HasLocalizedFields", StringComparison.OrdinalIgnoreCase) Then Continue For 
        If prop.Name.Equals("CanHave0AsPrimaryKey", StringComparison.OrdinalIgnoreCase) Then Continue For 
        If value Is Nothing Then Continue For 
      End If 
 
      If value Is Nothing OrElse IsLeaf(prop.PropertyType, value) Then 
        If Not vShowAllProperties Then 
          If prop.PropertyType.Name = "String" And value.ToString() = "." Then Continue For 
          If prop.PropertyType.Name = "String" And value.ToString() = "" Then Continue For 
        End If 
        Dim pPropName As String = "" 
        If vShowAllProperties Then 
          pPropName = $" {prop.PropertyType.Name}" 
        End If 
        If prop.PropertyType.Name = "Byte[]" Then 
          lines.Add(New String(" "c, (vIndentLevel + 1) * 2) & prop.Name & "" & pPropName & ": Length=" & CType(value, System.Byte()).Length) 
        Else 
          lines.Add(New String(" "c, (vIndentLevel + 1) * 2) & prop.Name & "" & pPropName & ": " & FormatLeaf(value)) 
        End If 
      Else 
        ' Nested object: print with its property name as the header (no duplicate type name)  
        Dim pStrg = ToStringCC(value, vIndentLevel + 1, prop.Name, vShowAllProperties) 
        If pStrg = "" Then Return "" 
        lines.Add(pStrg) 
      End If 
    Next 
 
    Dim sbObj As New StringBuilder() 
    ' If no name (root or unnamed), show the type once; otherwise just the property name.  
    Dim header As String 
    If vName Is Nothing Then 
      header = indent & typeName & ": {" 
    Else 
      header = indent & vName & ": {" 
    End If 
 
    sbObj.Append(header) 
    If lines.Count > 0 Then 
      sbObj.Append(Environment.NewLine) 
      sbObj.Append(String.Join("," & Environment.NewLine, lines)) 
    End If 
    sbObj.Append("}") 
    Return sbObj.ToString() 
  End Function 
 
  ' Decide whether to treat a value as a simple leaf (no recursion). 
  Private Shared Function IsLeaf(t As Type, value As Object) As Boolean 
    If value Is Nothing Then Return True 
    Return t.IsPrimitive OrElse t.IsEnum _ 
            OrElse TypeOf value Is String _ 
            OrElse TypeOf value Is DateTime _ 
            OrElse TypeOf value Is DateTimeOffset _ 
            OrElse TypeOf value Is Decimal _ 
            OrElse TypeOf value Is Guid _ 
            OrElse TypeOf value Is TimeSpan _ 
            OrElse TypeOf value Is Byte() 
  End Function 
 
  ' Compact, human-friendly formatting for leaf values 
  Private Shared Function FormatLeaf(value As Object) As String 
    If value Is Nothing Then Return "<null>" 
 
    If TypeOf value Is DateTimeOffset Then 
      Dim dto = DirectCast(value, DateTimeOffset) 
      ' e.g. 11-Oct-25 15:52:12 +00:00 
      Return dto.ToString("dd-MMM-yy HH:mm:ss zzz", CultureInfo.InvariantCulture) 
    End If 
 
    If TypeOf value Is DateTime Then 
      Dim dt = DirectCast(value, DateTime) 
      ' e.g. 11-Oct-25 18:52:12 
      Return dt.ToString("dd-MMM-yy HH:mm:ss", CultureInfo.InvariantCulture) 
    End If 
 
    Return value.ToString() 
  End Function 
 
  ' Friendly generic type names (e.g., Dictionary<String, Int32>) 
  Private Shared Function GetFriendlyTypeName(type As Type) As String 
    If Not type.IsGenericType Then Return type.Name 
    Dim args As Type() = type.GetGenericArguments() 
    Dim names As New List(Of String)() 
    For Each a In args 
      names.Add(GetFriendlyTypeName(a)) 
    Next 
    Return type.Name.Split("`"c)(0) & "<" & String.Join(", ", names) & ">" 
  End Function 
 
 
 
  '''' <summary>  
  '''' This returns a string representation of the object. Use it sparingly as it uses reflection, which is not efficient, to say the least  
  '''' </summary>  
  '''' <param name="obj"></param>  
  '''' <returns></returns>  
  'Public Shared Function ObjectToString(obj As Object) As String  
  '  Dim type As Type = obj.GetType()  
  '  Dim properties As Reflection.PropertyInfo() = type.GetProperties()  
 
  '  Dim stringBuilder As New System.Text.StringBuilder()  
  '  stringBuilder.Append($"{type.Name}~ ")  
  '  For Each lProperty As Reflection.PropertyInfo In properties  
  '    Dim pName As String = lProperty.Name  
  '    If pName.Equals("Json", StringComparison.OrdinalIgnoreCase) Then Continue For  
  '    Dim pValue As String = lProperty.GetValue(obj)?.ToString() '--' Check here to see if it's an object. If so, call recursively  
  '    If Not String.IsNullOrEmpty(pValue) Then  
  '      stringBuilder.Append($"{pName}: {pValue}, ")  
  '    End If  
  '  Next  
 
  '  Dim stringToReturn As String = stringBuilder.ToString()  
  '  Return stringToReturn.Substring(0, stringToReturn.Length - 2)  
  'End Function  
 
  Public Shared Function PrefixToComment(ByVal vTextToPrefix As String, ByVal vPresentComment As String, ByVal vRequester As clsRequester) As String 
    Dim pNewComment As String 
    pNewComment = vTextToPrefix & Environment.NewLine & 
                  DateTime.Now.ToString("dd-MMM-yyyy HH:mm", New System.Globalization.CultureInfo("en-US")) & " - " & vRequester.UserFullName & " (" & vRequester.UserName & "):" & Environment.NewLine & 
                  "--" & Environment.NewLine & 
                  vPresentComment 
    Return pNewComment 
  End Function 
 
  Public Shared Function GetStack() As String 
 
    Dim pStackTrace As New Text.StringBuilder() 
 
    pStackTrace.AppendLine("CalledBy: ") 
    Dim iCntr As Integer = -1 
    Do 
      iCntr += 1 
      If iCntr = 0 Then Continue Do 
      Dim pStackFrame = (New StackTrace(True).GetFrame(iCntr)) 
      If pStackFrame Is Nothing Then Exit Do 
      Dim pMethodBase As Reflection.MethodBase = pStackFrame.GetMethod() 
      If Not (pMethodBase Is Nothing) Then 
        If pMethodBase.DeclaringType?.Namespace.StartsWith("System.", StringComparison.OrdinalIgnoreCase) Then Continue Do 
        If pMethodBase.DeclaringType?.Namespace.StartsWith("Microsoft.", StringComparison.OrdinalIgnoreCase) Then Continue Do 
        Dim pLine As String = $"    {iCntr.ToString().PadLeft(2, " "c)}:{pMethodBase.DeclaringType?.FullName()}.{pMethodBase.Name}, File: {pStackFrame.GetFileName()}, Line: {pStackFrame.GetFileLineNumber()}" 
        If pLine.Contains("CC\Support\clsFault.vb") Then Continue Do 
        pStackTrace.AppendLine(pLine) 
      Else 
        Exit Do 
      End If 
    Loop 
 
    Return pStackTrace.ToString 
  End Function 
 
  ''' <summary> 
  ''' this returns active computer values 
  ''' </summary> 
  ''' <param name="rCPULoadPct"></param> 
  ''' <param name="rMemoryUsedPct"></param> 
  ''' <param name="rTotalMemoryGB"></param> 
  ''' <returns></returns> 
  Public Shared Function GetComputerStatus(ByRef rCPULoadPct As Decimal, ByRef rMemoryUsedPct As Decimal, ByRef rTotalMemoryGB As Integer) As String 
 
    rCPULoadPct = 0 
    rMemoryUsedPct = 0 
    rTotalMemoryGB = 0 
 
    Dim pFailedFunction As String = "" 
 
    Try 
      ' Get the total and available memory in bytes 
      pFailedFunction = "Memory" 
      Dim pTtotalMemory As Decimal = 0 
      Dim pAvailableMemory As Decimal = 0 
      Dim memoryQuery As New System.Management.ManagementObjectSearcher("SELECT TotalVisibleMemorySize, FreePhysicalMemory FROM Win32_OperatingSystem") 
      For Each memoryObject As System.Management.ManagementObject In memoryQuery.Get() 
        pTtotalMemory = ccHelper.ToDecimal(memoryObject("TotalVisibleMemorySize")) 
        pAvailableMemory = ccHelper.ToDecimal(memoryObject("FreePhysicalMemory")) 
      Next 
 
      ' Get the percentage of memory used 
      Dim pMemoryUsed As Decimal = (pTtotalMemory - pAvailableMemory) / pTtotalMemory * 100 
      pMemoryUsed = Decimal.Round(pMemoryUsed, 2) 
 
      pFailedFunction = "CPU" 
      'Get the current CPU usage in percentage 
      Dim cpuCounter As New PerformanceCounter("Processor Information", "% Processor Utility", "_Total", True) 
      Dim cpuUsage As Double = cpuCounter.NextValue() 
      System.Threading.Thread.Sleep(1000) 
      cpuUsage = cpuCounter.NextValue() 
 
      pFailedFunction = "Got Both" 
      'Get the total memory 
      Dim pTotalMemoryToDisplay As Decimal = pTtotalMemory / 1024 / 1024 
      pTotalMemoryToDisplay = Decimal.Round(pTotalMemoryToDisplay) 
      rTotalMemoryGB = ccHelper.ToInteger(pTotalMemoryToDisplay) 
 
      rMemoryUsedPct = pMemoryUsed 
 
      rCPULoadPct = ccHelper.ToDecimal(cpuUsage, 2) 
    Catch ex As Exception 
      Return $"Failed getting '{pFailedFunction}': {ex.Message}" 
    End Try 
 
    Return "OK" 
  End Function 
 
 
  'Languages 
  Private Shared _ObjectToTranslateCache As csObjectToTranslateCol 
  Private Shared _ObjectTranslationCache As csObjectTranslationCol 
  Private Shared _EnumCache As csEnumerationCol 
  Private Shared _LookupCache As csLookupCol 
  
  Friend Shared ReadOnly Property LookupCache As csLookupCol 
    Get 
      Return _LookupCache 
    End Get 
  End Property 
 
  ''' <summary> 
  ''' Gets the translated text for the Enum. If there is no translation, it uses the text field from the c_Enumeration table. 
  ''' Define the language if not using the Requester's language 
  ''' </summary> 
  ''' <param name="vEnum"></param> 
  ''' <param name="vValue"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function GetLocalizedEnum(ByVal vEnum As clsEnums.enmEnum, ByVal vValue As String, ByVal vRequester As clsRequester, Optional ByVal vLang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD) As String 
    
    Dim pEnumeration As csEnumeration 
    'Get the row 
    If MyController.CacheOn = False Then 
      pEnumeration = New csEnumeration(vIsLocalized:=True) 
      If vLang <> clsEnums.enmLanguage.UD Then 
        pEnumeration.OverrideDefaultLanguage(vLang) 
      End If 
      Dim pFault As clsFault = pEnumeration.GetByEnumTypeAndEnumValue(vEnum.FastToString(), vValue, vRequester, False) : If pFault.isOK = False Then Return "Translation Error. check logs" 
      Dim pStrg As String = pEnumeration.TextLocalized 
      If pStrg = "" Then pStrg = pEnumeration.Text 
      If pStrg = "" Then pStrg = ccHelper.CreateFriendlyTextFromHungarianNotation(pEnumeration.EnumValue) 
      Return pStrg 
    Else 
      Dim pStrg As String = "" 
      If _EnumCache IsNot Nothing Then 'This should never hppen, but it happenned once and caused the loss of a sale!! 
        pEnumeration = _EnumCache.FindByEnumTypeAndEnumValue(vEnum.FastToString(), vValue) 
        pStrg = ccHelper.GetLocalizedTableData("c_Enumeration", "Text", pEnumeration.ID, vRequester, vLang) 
        If pStrg = "" Then pStrg = pEnumeration.Text 
        If pStrg = "" Then pStrg = ccHelper.CreateFriendlyTextFromHungarianNotation(pEnumeration.EnumValue) 
      Else 
        If pStrg = "" Then pStrg = vValue & "!NC!" 
      End If 
      Return pStrg 
    End If 
 
  End Function 
  ''' <summary> 
  ''' Gets the translated text for the Lookup. If there is no translation, it uses the text field from the c_Lookup table. If that's blank, it uses the code. 
  ''' If there's no parent, then send vParentLookupType=clsEnums.enmLookup.UD and vParentCode="". 
  ''' Define the language if not using the Requester's language. 
  ''' </summary> 
  ''' <param name="vParentLookupType"></param> 
  ''' <param name="vParentCode"></param> 
  ''' <param name="vLookup"></param> 
  ''' <param name="vCode"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vLang"></param> 
  ''' <returns></returns> 
  Public Shared Function GetLocalizedLookup(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCode As String, ByVal vLookup As clsEnums.enmLookup, ByVal vCode As String, ByVal vRequester As clsRequester, Optional ByVal vLang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD) As String 
 
    Dim pLookup As csLookup 
    'Get the row  
    If MyController.CacheOn = False Then 
      pLookup = New csLookup(vIsLocalized:=True) 
      If vLang <> clsEnums.enmLanguage.UD Then 
        pLookup.OverrideDefaultLanguage(vLang) 
      End If 
      Dim pFault As clsFault = pLookup.GetByParentLookupTypeAndParentCodeAndLookupTypeAndCode(vParentLookupType, vParentCode, vLookup, vCode, vRequester, False) : If pFault.isOK = False Then Return "Translation Error. check logs" 
      Dim pStrg As String = pLookup.TextLocalized 
      If pStrg = "" Then pStrg = pLookup.Text 
      If pStrg = "" Then pStrg = pLookup.Code 
      Return pStrg 
    Else 
      Dim pStrg As String = "" 
      If _LookupCache IsNot Nothing Then 'This should never hppen, but it happenned once and caused the loss of a sale!! 
        pLookup = _LookupCache.FindByParentLookupTypeAndParentCodeAndLookupTypeAndCode(vParentLookupType, vParentCode, vLookup, vCode) 
        pStrg = ccHelper.GetLocalizedTableData("c_Lookup", "Text", pLookup.ID, vRequester, vLang) 
        If pStrg = "" Then pStrg = pLookup.Text 
        If pLookup.IsEmpty Then 
          If pStrg = "" Then If Not (vCode.Equals("-1")) Then pStrg = "****" 
        Else 
          If pStrg = "" Then pStrg = pLookup.Code 
        End If 
      Else 
        If pStrg = "" Then pStrg = vCode & "!NC!" 
      End If 
      Return pStrg 
    End If 
 
  End Function 
  ''' <summary> 
  ''' Gets the translated text for the Lookup. If there is no translation, it uses the text field from the c_Lookup table. If that's 0, it uses the code. 
  ''' If there's no parent, then send vParentLookupType=clsEnums.enmLookup.UD and vParentCode="". 
  ''' Define the language if not using the Requester's language. 
  ''' </summary> 
  ''' <param name="vParentLookupType"></param> 
  ''' <param name="vParentCode"></param> 
  ''' <param name="vLookup"></param> 
  ''' <param name="vCode"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="vLang"></param> 
  ''' <returns></returns> 
  Public Shared Function GetLocalizedLookup(ByVal vParentLookupType As clsEnums.enmLookup, ByVal vParentCode As String, ByVal vLookup As clsEnums.enmLookup, ByVal vCode As Integer, ByVal vRequester As clsRequester, Optional ByVal vLang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD) As String 
    Return GetLocalizedLookup(vParentLookupType, vParentCode, vLookup, vCode.ToString(), vRequester, vLang) 
  End Function 
  ''' <summary> 
  ''' Gets the translated text for the System text. If there is no translation, it returns the vText sent 
  ''' Define the language if not using the Requester's language 
  ''' </summary> 
  ''' <param name="vText"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function GetLocalizedSystemText(ByVal vText As String, ByVal vRequester As clsRequester, Optional ByVal vLang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD) As String 
    Dim pStrg As String = GetLocalized(clsEnums.enmObjectType.System, "Text", vText, 0, vRequester, vLang) 
    If String.IsNullOrEmpty(pStrg) Then pStrg = vText 
    Return pStrg 
  End Function 
  ''' <summary> 
  ''' Gets the translated text for the table data. If there is no translation, it uses the text from the data table. 
  ''' Define the language if not using the Requester's language 
  ''' </summary> 
  ''' <param name="vTableName"></param> 
  ''' <param name="vFieldName"></param> 
  ''' <param name="vRowID"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function GetLocalizedTableData(ByVal vTableName As String, ByVal vFieldName As String, ByVal vRowID As Long, ByVal vRequester As clsRequester, Optional ByVal vLang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD) As String 
    Return GetLocalized(clsEnums.enmObjectType.TableData, vTableName, vFieldName, vRowID, vRequester, vLang) 
  End Function 
  ''' <summary> 
  ''' Gets the translated text for the table field names. If there is no translation, it uses the actual field name. 
  ''' Define the language if not using the Requester's language 
  ''' </summary> 
  ''' <param name="vTableName"></param> 
  ''' <param name="vFieldName"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function GetLocalizedFieldName(ByVal vTableName As String, ByVal vFieldName As String, ByVal vRequester As clsRequester, Optional ByVal vLang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD) As String 
    Return GetLocalized(clsEnums.enmObjectType.TableFieldName, vTableName, vFieldName, 0, vRequester, vLang) 
  End Function 
  ''' <summary>  
  ''' Gets the translated text for UI text. This version includes line numbers. You can add translations and assign them to groups (e.g. Summary Page, User Text, etc.). CCText is a protected group, for text created by TargCC.  If there is no translation, it uses the Item field from the Object To Translate table. 
  ''' Define the language if not using the Requester's language 
  ''' </summary>  
  ''' <param name="vTopic"></param>  
  ''' <param name="vText"></param>  
  ''' <param name="vLineNumber"></param>  
  ''' <param name="vRequester"></param>  
  ''' <returns></returns>  
  Public Shared Function GetLocalizedUIText(ByVal vTopic As String, ByVal vText As String, ByVal vLineNumber As Integer, ByVal vRequester As clsRequester, Optional ByVal vLang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD) As String 
    Return GetLocalized(clsEnums.enmObjectType.UI, vTopic, vText, vLineNumber, vRequester, vLang) 
  End Function 
  ''' <summary> 
  ''' Gets the translated text for UI text. You can add translations and assign them to groups (e.g. Summary Page, User Text, etc.). CCText is a protected group, for text created by TargCC.  If there is no translation, it uses the Item field from the Object To Translate table. 
  ''' Define the language if not using the Requester's language 
  ''' </summary> 
  ''' <param name="vTopic"></param> 
  ''' <param name="vText"></param> 
  ''' <param name="vRequester"></param> 
  ''' <returns></returns> 
  Public Shared Function GetLocalizedUIText(ByVal vTopic As String, ByVal vText As String, ByVal vRequester As clsRequester, Optional ByVal vLang As clsEnums.enmLanguage = clsEnums.enmLanguage.UD) As String 
    Dim pString As String = GetLocalized(clsEnums.enmObjectType.UI, vTopic, vText, 0, vRequester, vLang) 
    If pString = "" Then pString = vText 
    Return pString 
  End Function 
  
  Private Shared _AddEnglish As Integer = -1 
  Private Shared Function GetLocalized(ByVal vObjectType As clsEnums.enmObjectType, ByVal vObject As String, ByVal vItem As String, ByVal vInstance As Long, ByVal vRequester As clsRequester, ByVal vLang As clsEnums.enmLanguage) As String 
    Dim pFault As New clsFault 
 
    Dim pTranslation As String 
 
    pFault = ccHelper.LoadLanguageCache(vRequester) : If Not pFault.isOK Then Return pFault.ShortStringForMessageBox(False) 
 
    Dim pUILang As clsEnums.enmLanguage 
    If vLang <> clsEnums.enmLanguage.UD Then 
      pUILang = vLang 
    Else 
      pUILang = vRequester.UILang 
    End If 
 
    If _AddEnglish = -1 Then 
      If vRequester IsNot Nothing Then 
        If pUILang <> clsEnums.enmLanguage.en Then _AddEnglish = Convert.ToInt32((MyController.UsersToShowEnglishAlso.IndexOf(vRequester.UserName, StringComparison.OrdinalIgnoreCase) >= 0)) 
      End If 
    End If 
 
    If MyController.CacheOn = False OrElse vRequester Is Nothing OrElse (MyController.CacheOn = True AndAlso MyController.CacheSingleLanguageOnly = True AndAlso pUILang <> vRequester.UILang) Then 
      
      Dim pObjectToTranslate As New csObjectToTranslate 
      pFault = pObjectToTranslate.GetByObjectTypeAndObjectAndItem(vObjectType, vObject, vItem, vRequester, False) 
      If pFault.isOK = False Then Return "Translation Error. check logs" 
      If pObjectToTranslate.ID = 0 Then 
        'No translation row exists  
        If vObjectType = clsEnums.enmObjectType.UI And vObject.ToLowerInvariant() <> "cctext" Then 'I expected this to be found  
          If pUILang <> clsEnums.enmLanguage.en Then 
            Return String.Format("!!ItemNotDefinedFor'{0}';'{1}'!!", vObject, vItem) 
          Else 
            Return "" 
          End If 
        Else 
          Return "" 
        End If 
      End If 
 
      Dim pObjectTranslation As New csObjectTranslation() 
      pFault = pObjectTranslation.GetByObjectToTranslateIDAndInstanceAndLanguage(pObjectToTranslate.ID, vInstance, pUILang, vRequester, False) 
      If pFault.isOK = False Then Return "Translation Error. check logs" 
      If pObjectTranslation.ID = 0 Then 
        pTranslation = "" 
        If pUILang <> clsEnums.enmLanguage.en Then 
          'Get the English   
          If vObjectType <> clsEnums.enmObjectType.TableData Then 
            pFault = pObjectTranslation.GetByObjectToTranslateIDAndInstanceAndLanguage(pObjectToTranslate.ID, vInstance, clsEnums.enmLanguage.en, vRequester, False) 
            If pFault.isOK = False Then Return "Translation Error. check logs" 
            pTranslation = pObjectTranslation.Text 
          End If 
        End If 
        If pObjectTranslation.ID = 0 Then 
          pTranslation = "" 
        End If 
      Else 
        pTranslation = pObjectTranslation.Text 
        If _AddEnglish = 1 Then pTranslation &= $":{pObjectToTranslate.Item}" 
      End If 
 
      Return pTranslation 
    Else 
 
      'To avoid errors during load 
      If _ObjectToTranslateCache Is Nothing OrElse _ObjectTranslationCache Is Nothing Then 
        Return "" 
      End If 
 
      Dim pObjectToTranslate As csObjectToTranslate 
      pObjectToTranslate = _ObjectToTranslateCache.FindByObjectTypeAndObjectAndItem(vObjectType, vObject, vItem) 
      If pObjectToTranslate.ID = 0 Then 
        'No translation row exists 
        If vObjectType = clsEnums.enmObjectType.UI And vObject.ToLowerInvariant() <> "cctext" Then 'I expected this to be found 
          If pUILang <> clsEnums.enmLanguage.en Then 
            Return String.Format("!!ItemNotDefinedFor'{0}';'{1}'!!", vObject, vItem) 
          Else 
            Return "" 
          End If 
        Else 
          Return "" 
        End If 
      End If 
 
      Dim pObjectTranslation As csObjectTranslation 
      pObjectTranslation = _ObjectTranslationCache.FindByObjectToTranslateIDAndInstanceAndLanguage(pObjectToTranslate.ID, vInstance, pUILang) 
      If pObjectTranslation.ID = 0 Then 
        pObjectTranslation = _ObjectTranslationCache.FindByObjectToTranslateIDAndInstanceAndLanguage(pObjectToTranslate.ID, vInstance, clsEnums.enmLanguage.en) 
        If pObjectTranslation.ID = 0 Then 
          Return "" 
        Else 
          pTranslation = pObjectTranslation.Text 
        End If 
      Else 
        pTranslation = pObjectTranslation.Text 
        If vInstance = 0 Then If _AddEnglish = 1 Then If pObjectToTranslate.Item = "_TableTitle" Then pTranslation &= $":{pObjectToTranslate.Object}" Else pTranslation &= $":{pObjectToTranslate.Item}" 
      End If 
      Return pTranslation 
      
    End If 
 
  End Function 
 
  Private Shared _LoadLanguageCachePadlock As New Object 
  Private Shared _LanguageCacheFilledTime As DateTimeOffset = DateTimeOffset.MinValue 
 
  Private Shared _InCacheLoad As Boolean 
 
  Friend Shared Function LoadLanguageCache(ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
    If vRequester Is Nothing Then Return New clsFault().SetOK() 
    
    'Test that we got all the variables 
    Dim pCacheOn As Boolean = False 
    Dim pCacheKeepAliveMin As Integer = 0 
    Dim pCacheSingleLanguageOnly As Boolean = False 
    Dim pTest As String = "" 
    Try 
      pTest = "CacheOn" 
      pCacheOn = MyController.CacheOn 
      If pCacheOn = False Then 
        pFault = New clsFault 
        Return pFault.SetOK() 
      End If 
      pTest = "CacheKeepAliveMin" 
      pCacheKeepAliveMin = MyController.CacheKeepAliveMin 
      pTest = "CacheSingleLanguageOnly" 
      pCacheSingleLanguageOnly = MyController.CacheSingleLanguageOnly 
    Catch ex As Exception 
      pFault = New clsFault 
      Return pFault.LogException(140, ex, pTest, "TRGT-190218-0851", vRequester) 
    End Try 
    If _InCacheLoad = True Then 
      pFault = New clsFault 
      Return pFault.SetOK() 
    End If 
 
    Dim pDoit As Boolean = False 
 
    SyncLock _LoadLanguageCachePadlock 
      If _LanguageCacheFilledTime = DateTimeOffset.MinValue Then 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("LanguageCache About to do initial fill", "Caches") 
        _LanguageCacheFilledTime = DateTimeOffset.Now 
        pFault = LoadCaches(vRequester) : If Not pFault.isOK Then Return pFault 
      ElseIf DateTimeOffset.Now.Subtract(_LanguageCacheFilledTime).TotalMinutes > pCacheKeepAliveMin Then 
        If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("LanguageCache About to do it (" & pCacheKeepAliveMin & "m)", "Caches") 
        _LanguageCacheFilledTime = DateTimeOffset.Now 
        pDoit = True 
      Else 
        'If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("    LanguageCache No update required", "Caches") 
      End If 
    End SyncLock 
 
    If pDoit = True Then 
      pFault = LoadCaches(vRequester) : If Not pFault.isOK Then Return pFault 
    Else 
      pFault = New clsFault 
      pFault.SetOK() 
    End If 
 
    Return pFault 
  End Function 
 
  Private Shared Function LoadCaches(ByVal vRequester As clsRequester) As clsFault 
    If MyController.LogDetails = True Then Tools.LogToTextFile.WriteMessage("LanguageCache Doing It", "Caches") 
 
    _InCacheLoad = True 
 
    Dim pFault As clsFault = Nothing 
 
    Dim pTasks(1) As Task(Of clsFault) 
 
    pTasks(0) = Task.Run(Function() 
                           pFault = ObjectToTranslateCacheMaintainer(vRequester) 
                           Return pFault 
                         End Function) 
 
    pTasks(1) = Task.Run(Function() 
                           pFault = ObjectTranslationCacheMaintainer(vRequester) 
                           Return pFault 
                         End Function) 
 
    Task.WaitAll(pTasks) 
    For Each p As Task(Of clsFault) In pTasks 
      If Not p.Result.isOK Then Return p.Result 
    Next 
 
 
    pTasks(0) = Task.Run(Function() 
                           pFault = EnumCacheMaintainer(vRequester) 
                           Return pFault 
                         End Function) 
 
    pTasks(1) = Task.Run(Function() 
                           pFault = LookupCacheMaintainer(vRequester) 
                           Return pFault 
                         End Function) 
 
    Task.WaitAll(pTasks) 
    For Each p As Task(Of clsFault) In pTasks 
      If Not p.Result.isOK Then Return p.Result 
    Next 
 
    _InCacheLoad = False 
 
    'pFault = ObjectToTranslateCacheMaintainer(vRequester) : If Not pFault.isOK Then Return pFault 
    'pFault = ObjectTranslationCacheMaintainer(vRequester) : If Not pFault.isOK Then Return pFault 
    'pFault = EnumCacheMaintainer(vRequester) : If Not pFault.isOK Then Return pFault 
    'pFault = LookupCacheMaintainer(vRequester) : If Not pFault.isOK Then Return pFault 
 
    If pFault Is Nothing Then pFault = New clsFault 
    Return pFault.SetOK() 
  End Function 
 
  Private Shared Function ObjectToTranslateCacheMaintainer(ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
 
    Dim pNewCol As New csObjectToTranslateCol 
    pFault = pNewCol.Fill(vRequester) : If Not pFault.isOK Then Return pFault 
 
    If _ObjectToTranslateCache Is Nothing Then 
      _ObjectToTranslateCache = pNewCol 
      Return pFault 
    End If 
 
    For Each pOld As csObjectToTranslate In _ObjectToTranslateCache 
      pOld.Tag = "" 
    Next 
 
    For Each pNew As csObjectToTranslate In pNewCol 
      Dim pOld As csObjectToTranslate = _ObjectToTranslateCache.FindByID(pNew.ID) 
      If pOld.IsEmpty Then 
        pNew.Tag = "New" 
        _ObjectToTranslateCache.Add(pNew) 
      Else 
        If pOld.isEqual(pNew) Then 
          pOld.Tag = "Used" 
        Else 
          _ObjectToTranslateCache.Remove(pOld) 
          pNew.Tag = "New" 
          _ObjectToTranslateCache.Add(pNew) 
        End If 
      End If 
    Next 
 
    'Load ID's to delete 
    Dim pIDsToDelete As New List(Of Long) 
    For Each pOld As csObjectToTranslate In _ObjectToTranslateCache 
      If pOld.Tag = "" Then pIDsToDelete.Add(pOld.ID) 
    Next 
 
    For Each pID As Long In pIDsToDelete 
      _ObjectToTranslateCache.Remove(_ObjectToTranslateCache.FindByID(pID)) 
    Next 
 
    Return pFault 
  End Function 
 
  Private Shared Function ObjectTranslationCacheMaintainer(ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
 
    Dim pNewCol As New csObjectTranslationCol 
    'Load the New 
    If MyController.CacheSingleLanguageOnly = True Then 
      pFault = pNewCol.FillByLanguage(vRequester.UILang, vRequester) 
      If pFault.isOK = False Then Return pFault 
      If vRequester.UILang <> clsEnums.enmLanguage.en Then 
        Dim pEnglishTranslations As New csObjectTranslationCol() 
        pFault = pEnglishTranslations.FillByLanguage(clsEnums.enmLanguage.en, vRequester) 
        If pFault.isOK = False Then Return pFault 
        pNewCol.AddRange(pEnglishTranslations) 
      End If 
    Else 
      pFault = pNewCol.Fill(vRequester) 
      If pFault.isOK = False Then Return pFault 
    End If 
 
    If _ObjectTranslationCache Is Nothing Then 
      _ObjectTranslationCache = pNewCol 
      Return pFault 
    End If 
 
    For Each pOld As csObjectTranslation In _ObjectTranslationCache 
      pOld.Tag = "" 
    Next 
 
    For Each pNew As csObjectTranslation In pNewCol 
      Dim pOld As csObjectTranslation = _ObjectTranslationCache.FindByID(pNew.ID) 
      If pOld.IsEmpty Then 
        pNew.Tag = "New" 
        _ObjectTranslationCache.Add(pNew) 
      Else 
        If pOld.isEqual(pNew) Then 
          pOld.Tag = "Used" 
        Else 
          _ObjectTranslationCache.Remove(pOld) 
          pNew.Tag = "New" 
          _ObjectTranslationCache.Add(pNew) 
        End If 
      End If 
    Next 
 
    'Load ID's to delete 
    Dim pIDsToDelete As New List(Of Long) 
    For Each pOld As csObjectTranslation In _ObjectTranslationCache 
      If pOld.Tag = "" Then pIDsToDelete.Add(pOld.ID) 
    Next 
 
    For Each pID As Long In pIDsToDelete 
      _ObjectTranslationCache.Remove(_ObjectTranslationCache.FindByID(pID)) 
    Next 
 
    Return pFault 
  End Function 
 
  Private Shared Function EnumCacheMaintainer(ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
 
    If _EnumCache Is Nothing Then _EnumCache = New csEnumerationCol(vIsLocalized:=False) 
 
    Dim pNewCol As New csEnumerationCol(vIsLocalized:=False) 
    'Load the New  
    pFault = pNewCol.Fill(vRequester) : If pFault.isOK = False Then Return pFault 
 
    If _EnumCache.Count = 0 Then 
      _EnumCache = pNewCol 
      Return pFault 
    End If 
 
    For Each pOld As csEnumeration In _EnumCache 
      pOld.Tag = "" 
    Next 
 
    For Each pNew As csEnumeration In pNewCol 
      Dim pOld As csEnumeration = _EnumCache.FindByID(pNew.ID) 
      If pOld.IsEmpty Then 
        pNew.Tag = "New" 
        _EnumCache.Add(pNew) 
      Else 
        If pOld.isEqual(pNew) Then 
          pOld.Tag = "Used" 
        Else 
          _EnumCache.Remove(pOld) 
          pNew.Tag = "New" 
          _EnumCache.Add(pNew) 
        End If 
      End If 
    Next 
 
    'Load ID's to delete 
    Dim pIDsToDelete As New List(Of Integer) 
    For Each pOld As csEnumeration In _EnumCache 
      If pOld.Tag = "" Then pIDsToDelete.Add(pOld.ID) 
    Next 
 
    For Each pID As Integer In pIDsToDelete 
      _EnumCache.Remove(_EnumCache.FindByID(pID)) 
    Next 
 
    Return pFault 
  End Function 
 
  Private Shared Function LookupCacheMaintainer(ByVal vRequester As clsRequester) As clsFault 
    Dim pFault As clsFault 
 
    If _LookupCache Is Nothing Then _LookupCache = New csLookupCol(vIsLocalized:=False) 
 
    Dim pNewCol As New csLookupCol(vIsLocalized:=False) 
    'Load the New 
    pFault = pNewCol.FillForLookupCache(vRequester) : If pFault.isOK = False Then Return pFault 
 
    If _LookupCache.Count = 0 Then 
      _LookupCache = pNewCol 
      Return pFault 
    End If 
 
    For Each pOld As csLookup In _LookupCache 
      pOld.Tag = "" 
    Next 
 
    For Each pNew As csLookup In pNewCol 
      Dim pOld As csLookup = _LookupCache.FindByID(pNew.ID) 
      If pOld.IsEmpty Then 
        pNew.Tag = "New" 
        _LookupCache.Add(pNew) 
      Else 
        If pOld.isEqual(pNew) Then 
          pOld.Tag = "Used" 
        Else 
          _LookupCache.Remove(pOld) 
          pNew.Tag = "New" 
          _LookupCache.Add(pNew) 
        End If 
      End If 
    Next 
 
    'Load ID's to delete 
    Dim pIDsToDelete As New List(Of Long) 
    For Each pOld As csLookup In _LookupCache 
      If pOld.Tag = "" Then pIDsToDelete.Add(pOld.ID) 
    Next 
 
    For Each pID As Long In pIDsToDelete 
      _LookupCache.Remove(_LookupCache.FindByID(pID)) 
    Next 
 
    Return pFault 
  End Function 
 
  Public Shared Function GetChoose(ByVal vRequester As clsRequester) As String  
    Dim pChoose As String  
  
    If MyController.CacheOn = False AndAlso vRequester.UILang = clsEnums.enmLanguage.en Then pChoose = "Choose" : Return pChoose 
    pChoose = GetLocalizedSystemText("Choose", vRequester) 
    If pChoose = "" Then If vRequester.UILang = clsEnums.enmLanguage.en Then pChoose = "Choose" Else pChoose = "$Choose$ (Translate)" 
  
    Return pChoose  
  End Function  
  
  Public Shared Function GetNew(ByVal vRequester As clsRequester) As String  
    Dim pNew As String  
  
    If MyController.CacheOn = False AndAlso vRequester.UILang = clsEnums.enmLanguage.en Then pNew = "New" : Return pNew 
    pNew = GetLocalizedSystemText("New", vRequester) 
    If pNew = "" Then If vRequester.UILang = clsEnums.enmLanguage.en Then pNew = "New" Else pNew = "$New$ (Translate System-Text-New)" 
  
    Return pNew  
  End Function  
  
  Public Shared Function GetUndefined(ByVal vRequester As clsRequester) As String  
    Dim pUndefined As String  
  
    If MyController.CacheOn = False AndAlso vRequester.UILang = clsEnums.enmLanguage.en Then pUndefined = "Undefined" : Return pUndefined 
    pUndefined = GetLocalizedSystemText("Undefined", vRequester) 
    If pUndefined = "" Then If vRequester.UILang = clsEnums.enmLanguage.en Then pUndefined = "Undefined" Else pUndefined = "$Undefined$ (Translate System-Text-Undefined)" 
  
    Return pUndefined  
  End Function  
  
End Class 
 
Public Class WSLoader 
  'This class loads the web service with a dummy (simply gets the time) 
  Event evtLoaded() 
 
  Private _Loaded As Boolean 
  Public ReadOnly Property Loaded As Boolean 
    Get 
      Return _Loaded 
    End Get 
  End Property 
 
  Public Sub New() 
    _Loaded = False 
  End Sub 
 
  Public Sub Load() 
    'Dummy stub for web service version 
  End Sub 
End Class 
 
Public Class WebAPI 
  
  Private Shared _CertMessage As String 
 
  ''' <summary> 
  ''' Use this when you want to get the for user functions. It's easier to create, returning a csTargCCParameterCol. If don't send vTimeOutMs, it will use the default (100 sec) 
  ''' </summary> 
  ''' <param name="vClassName"></param> 
  ''' <param name="vFunctionName"></param> 
  ''' <param name="vParameters"></param> 
  ''' <param name="vRequester"></param> 
  ''' <param name="rResults"></param> 
  ''' <param name="vTimeOutMs"></param> 
  ''' <returns></returns> 
  Public Shared Function ExecuteFunction(ByVal vClassName As String, ByVal vFunctionName As String, ByVal vParameters As ccWSAL.csTargCCParameterCol, ByVal vRequester As clsRequester, ByRef rResults As ccWSAL.csTargCCParameterCol, Optional ByVal vTimeOutMs As Integer = 0) As clsFault 
    Dim pFunctionParameters = $"{vClassName}_{vFunctionName}" 
    Dim pFault As New clsFault 
    Dim pLoggedRowHeader As Text.StringBuilder = Nothing 
 
    If vTimeOutMs <> 0 Then 
      pFunctionParameters &= $"(TimeOutSec:{vTimeOutMs})" 
    End If 
 
    Dim pCreateHeader As Boolean = (pLoggedRowHeader Is Nothing) 
    Dim sw As Stopwatch = Nothing 
    Dim pLoggedRow As Text.StringBuilder = Nothing 
 
    Dim pLogWSAL As Boolean = MyController.LogDetails 
 
    If pLogWSAL = True Then 
      sw = New Stopwatch 
      pLoggedRow = New Text.StringBuilder 
      If pCreateHeader = True Then 
        pLoggedRowHeader = New Text.StringBuilder 
        pLoggedRowHeader.Append(", CallingApplication, UserName, LoggedLoginID, vClassName_vFunctionName, ") 
      End If 
      pLoggedRow.Append(String.Format(", {0}, {1}, {2}, {3}, ", vRequester.CallingApplication, vRequester.UserName, vRequester.LoggedLoginID, $"{vClassName}_{vFunctionName}")) 
      If pCreateHeader = True Then 
        pLoggedRowHeader.Append("ChangedBy, ") 
      End If 
      Try 
        If vParameters.Count > 2 AndAlso vParameters(vParameters.Count - 2).Value.ToString().Length < 25 Then 'vParameters(vParameters.Count - 2).Name = "ChangedBy" Then  
          pLoggedRow.Append(String.Format("{0}, ", vParameters(vParameters.Count - 2).Value.ToString() & "(" & vParameters(vParameters.Count - 2).Name & ")")) 
        Else 
          pLoggedRow.Append("None, ") 
        End If 
      Catch ex As Exception 
        pLoggedRow.Append("None (Not caught), ") 
      End Try 
      sw.Start() 
    End If 
 
    Try 
      Dim pRequest As Byte() = Nothing 
      Dim pResponse As Byte() = Nothing 
 
      'Create bytes 
      pRequest = ccWSAL.ParametersToBytes(vParameters) 
      If pLogWSAL = True Then 
        sw.Stop() 
        If pCreateHeader = True Then pLoggedRowHeader.Append("Created Bytes, ") 
        pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds)) 
        sw.Restart() 
      End If 
 
      'run query 
      Dim pParamaters As String = "Set LogDetails to 1 to see parameters" 
      If MyController.LogDetails = True Then 
        If vParameters.Count > 0 Then 
          'get 1st parameter 
          pParamaters = vParameters(0).Name & ":" & vParameters(0).Value.ToString() 
        Else 
          pParamaters = "Parameters: None" 
        End If 
      End If 
      pFault = WebAPI.RunAPI($"{vClassName}_{vFunctionName}", pRequest, pParamaters, pResponse, vRequester, vTimeOutMs) 
      If Not pFault.isOK AndAlso pResponse Is Nothing Then Return pFault 'I want whatever is in pResponse, so I won't return pFault at this point if it's 'Not OK'
      If pLogWSAL = True Then 
        sw.Stop() 
        If pCreateHeader = True Then pLoggedRowHeader.Append("Ran API, ") 
        pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds)) 
        sw.Restart() 
      End If 
 
      'create parameters 
      If pResponse IsNot Nothing AndAlso pResponse.Length > 0 Then 
        rResults = ccWSAL.BytesToParameters(pResponse) 
      End If 
      If pLogWSAL = True Then 
        sw.Stop() 
        If pCreateHeader = True Then pLoggedRowHeader.Append("Created Parameters, ") 
        pLoggedRow.Append(String.Format("{0}, ", sw.Elapsed.TotalMilliseconds)) 
        sw.Restart() 
      End If 
    Catch ex As Exception 
      Return pFault.LogException(73, ex, pFunctionParameters, "TRGT-ItemAtPartner-150407-2142", vRequester) 
    End Try 
 
    If pLogWSAL = True Then 
      If pCreateHeader = True Then Tools.LogToTextFile.WriteMessage(pLoggedRowHeader.ToString() & " times in ms", "WSALTimes") 
      Tools.LogToTextFile.WriteMessage(pLoggedRow.ToString(), "WSALTimes") 
    End If 
 
    Return pFault 
  End Function 
 
  ''' <summary> 
  ''' Timeout of 0 leaves the timeout at default (100 sec) 
  ''' </summary> 
  ''' <param name="vTask"></param> 
  ''' <param name="vRequest"></param> 
  ''' <param name="rResponse"></param> 
  ''' <param name="rRequester"></param> 
  ''' <param name="vTimeOutMs"></param> 
  ''' <returns></returns> 
  ''' <remarks></remarks> 
  Friend Shared Function RunAPI(vTask As String, 
                                vRequest As Byte(), 
                                vParametersToLog As String, 
                                ByRef rResponse As Byte(), 
                                ByRef rRequester As clsRequester, Optional ByVal vTimeOutMs As Integer = 0) As clsFault 
    Dim pFunctionParameters As String = "" 
 
    If Debugger.IsAttached Then 
      Dim pClass As String = "" 
      Dim pFunction As String = "" 
      Dim pTaskTest As String = pClass & pFunction 
 
      If vTask.IndexOf("_", StringComparison.OrdinalIgnoreCase) > 0 Then 
        pClass = (New StackFrame(2)).GetMethod().DeclaringType.FullName() 
        pFunction = (New StackFrame(2)).GetMethod().Name 
        pTaskTest = pClass & "_" & pFunction 
      Else 
        pClass = (New StackFrame(1)).GetMethod().DeclaringType.FullName() 
        pFunction = (New StackFrame(1)).GetMethod().Name 
        pTaskTest = pClass & pFunction 
      End If 
 
      If pTaskTest.StartsWith("TargCCOrders.") Then 
        pTaskTest = pTaskTest.Substring(("TargCCOrders.").Length) 
        If pTaskTest.StartsWith("DataController.") Then 
          pTaskTest = pTaskTest.Substring(("DataController.").Length) 
        End If 
      End If 
 
      If Not vTask.Equals(pTaskTest) Then 
        Dim pMessage As String = ($"Function RunAPI: Received: {vTask}, Calculated: {pTaskTest}") 
        Return New clsFault(67, pMessage, "", "TRGT-210204-2017", rRequester, vAdditionalMessageToUser:=pMessage) 
      End If 
    End If 
 
    Dim pFault As New clsFault 
 
    'get the ticket from the requester 
    Dim pTicket As String = "" 
    If rRequester IsNot Nothing Then 
      pTicket = rRequester.CreateTicket() 
    End If 
 
    'Get the version 
    pFunctionParameters = "Getting the version" 
    Dim pVersion As String = "" 
    Try 
      pVersion = System.Reflection.Assembly.GetExecutingAssembly.FullName.Split(","c)(1).Split("="c)(1).Trim 
    Catch ex As Exception 
      Return pFault.LogException(66, ex, pFunctionParameters, "TRGT-150316-1017", rRequester) 
    End Try 
 
    Dim pServiceRequest As Byte() 
 
    'Create the request 
 
    'Create an array of the whole lot 
    pFunctionParameters = "Creating the Binary Request" 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream() 
        Using pBinaryWriter As New IO.BinaryWriter(pMemoryStream, Text.Encoding.UTF8) 
          pBinaryWriter.Write(vTask) 
          pBinaryWriter.Write(vRequest.Length) 
          pBinaryWriter.Write(vRequest, 0, vRequest.Length) 
          pBinaryWriter.Write(pTicket) 
          pBinaryWriter.Write(pVersion) 
          pBinaryWriter.Write(MyController.WSPwd) 
          pBinaryWriter.Close() 
        End Using 
        pServiceRequest = pMemoryStream.ToArray() 
        pMemoryStream.Close() 
      End Using 
    Catch ex As Exception 
      Return pFault.LogException(71, ex, pFunctionParameters, "TRGT-150316-1018", rRequester) 
    End Try 
 
    'Let's Upload it 
    Dim pUri As String = "" 
    Dim pAppOffline As String = "" 
    If MyController.ServerRequiresSSL = True Then 
      pUri = "https://" & MyController.ServerApplicationRoot(MyController.APIServerNumber) & "/CC/ccAPI.aspx" 
      pAppOffline = "https://" & MyController.ServerApplicationRoot(MyController.APIServerNumber) & "/app_offline.htm" 
    Else 
      pUri = "http://" & MyController.ServerApplicationRoot(MyController.APIServerNumber) & "/CC/ccAPI.aspx" 
      pAppOffline = "http://" & MyController.ServerApplicationRoot(MyController.APIServerNumber) & "/app_offline.htm" 
    End If 
 
    Dim pWebClient As New WebClientExtended() 
    pWebClient.Timeout = vTimeOutMs 
    'set compression 
    If MyController.ccAPICompressionMode = clsEnums.enmccAPICompressionMode.IIS Then 
      pWebClient.Headers.Add(Net.HttpRequestHeader.AcceptEncoding, "gzip,deflate") 
    End If 
    pWebClient.Headers.Add("Browser", "TargCC") 
    pWebClient.Headers.Add("Content-Type", "application/octet-stream") 
    If rRequester IsNot Nothing Then 
      pWebClient.Credentials = rRequester.Credential 
    Else 
      If MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials Then 
        pWebClient.Credentials = New System.Net.NetworkCredential(MyController.ApplicationName, ccHelper.GetSecureString(MyController.ApplicationPwd)) 
      ElseIf MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ActiveUserCredentials Then 
        pWebClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials 
      End If 
    End If 
 
    System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12 
 
    Tools.LogToTextFile.WriteMessage("Task:  " & vTask & ", " & vParametersToLog & ", UserName:" & rRequester?.Credential?.UserName & ", password Length=" & rRequester?.Credential?.SecurePassword.Length, "ControllerAPI") 
 
    pFunctionParameters = "Uploading data" 
    Dim pServiceResponse As Byte() = Nothing 
    Dim pUploadDataException As String = "" 
    Try 
      pServiceResponse = pWebClient.UploadData(pUri, "POST", pServiceRequest) 
    Catch ex As Net.WebException 
      'ex.Status  
      Tools.LogToTextFile.WriteMessage("Net.WebException Status:" & ex.Status & ", Message:" & ex.Message, "ControllerAPI") 
      If MyController.ServerApplicationRoot.Length > 1 AndAlso (ex.Status = Net.WebExceptionStatus.ConnectFailure OrElse ex.Status = Net.WebExceptionStatus.NameResolutionFailure) Then 
        MyController.SetNextApiServer() ' In case of mutiple servers (failover)  
        Return RunAPI(vTask, vRequest, "Try 2: " & vParametersToLog, rResponse, rRequester, vTimeOutMs) 
      End If 
      
      'Try to get text from app_offline.htm 
      Dim pCheckWebClient As New WebClientExtended() 
      pCheckWebClient.Timeout = vTimeOutMs 
      'set compression  
      If MyController.ccAPICompressionMode = clsEnums.enmccAPICompressionMode.IIS Then 
        pCheckWebClient.Headers.Add(Net.HttpRequestHeader.AcceptEncoding, "gzip,deflate") 
      End If 
      pCheckWebClient.Headers.Add("Browser", "TargCC") 
      pCheckWebClient.Headers.Add("Content-Type", "application/octet-stream") 
      If rRequester IsNot Nothing Then 
        pCheckWebClient.Credentials = rRequester.Credential 
      Else 
        If MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ApplicationCredentials Then 
          pCheckWebClient.Credentials = New System.Net.NetworkCredential(MyController.ApplicationName, ccHelper.GetSecureString(MyController.ApplicationPwd)) 
        ElseIf MyController.ApplicationAuthenticationToWS = clsEnums.enmApplicationAuthenticationToWS.ActiveUserCredentials Then 
          pCheckWebClient.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials 
        End If 
      End If 
 
      System.Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Tls12 
 
      Static pText As String = "" 
      If String.IsNullOrEmpty(pText) Then 
        Threading.Thread.Sleep(3000) 
        Try 
          pText = pCheckWebClient.DownloadString(pAppOffline) 
        Catch exx As Exception 
        End Try 
      End If 
      'End Try to get text from app_offline.htm 
       
      If String.IsNullOrEmpty(pText) Then 
        pUploadDataException = $"UploadDataException: {ex.Message}{Environment.NewLine}URL: {pUri}" 
      Else 
        pUploadDataException = $"Message from Server: {pText}" 
      End If 
    Catch ex As Exception 
      If vTask = "clsFaultCreateLoggedAlert" Then Tools.LogToTextFile.WriteException("", ex, "ControllerAPI") 
      Return pFault.LogException(68, ex, pFunctionParameters, "TRGT-150316-1019", rRequester) 
    End Try  
  
    If pServiceResponse Is Nothing OrElse pServiceResponse.Length = 0 Then  
      If pUploadDataException <> "" Then 
        Return pFault.LogFreeTextFault(68, "UploadDataException: " & pUploadDataException, pFunctionParameters, "TRGT-160523-0903", rRequester) 
      Else 
        Return pFault.LogFreeTextFault(68, "Server Side error - Check LoggedAlerts", pFunctionParameters, "TRGT-150407-1137", rRequester) 
      End If 
    End If  
   
    pFunctionParameters = "Uncompressing Response"  
    Try  
      If MyController.ccAPICompressionMode = clsEnums.enmccAPICompressionMode.DeflateTargCC Then  
        pServiceResponse = ccHelper.DeCompressDeflate(pServiceResponse)  
      ElseIf MyController.ccAPICompressionMode = clsEnums.enmccAPICompressionMode.GzipTargCC Then  
        pServiceResponse = ccHelper.DeCompressGZip(pServiceResponse)  
      ElseIf MyController.ccAPICompressionMode = clsEnums.enmccAPICompressionMode.IIS Then  
        pServiceResponse = ccHelper.DeCompressGZip(pServiceResponse)  
      ElseIf MyController.ccAPICompressionMode = clsEnums.enmccAPICompressionMode.None Then  
        Static pTaskFaultCreateLoggedAlertTimesEntered As Integer = 0  
        If vTask = "clsFaultCreateLoggedAlert" Then  
          pTaskFaultCreateLoggedAlertTimesEntered += 1  
          If pTaskFaultCreateLoggedAlertTimesEntered > 10 Then  
            Tools.LogToTextFile.WriteMessage("clsFaultCreateLoggedAlert is cacsading", "ControllerAPI") 
            Throw New Exception("clsFaultCreateLoggedAlert is cacsading") 
          End If  
        Else 
          pTaskFaultCreateLoggedAlertTimesEntered = 0 
        End If 
        'Do nothing  
      ElseIf MyController.ccAPICompressionMode = clsEnums.enmccAPICompressionMode.UD Then 
        'Throw error 
      End If 
    Catch ex As Exception 
        Return pFault.LogException(66, ex, pFunctionParameters, "TRGT-150316-1020", rRequester) 
    End Try 
 
 
    pFunctionParameters = "Deciphering Response" 
    Try 
      Using pMemoryStream As New System.IO.MemoryStream(pServiceResponse) 
        Using pReader As New System.IO.BinaryReader(pMemoryStream) 
          Dim pLength As Integer = 0 
          'Fault 
          Dim pFaultGettingFault As New clsFault 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then 
            pFault = New clsFault(pReader.ReadBytes(pLength), pFaultGettingFault, rRequester) 
            If Not pFaultGettingFault.isOK Then Return pFaultGettingFault 
          Else 
            Return pFaultGettingFault.LogFreeTextFault(66, "No Fault returned!", pFunctionParameters, "TRGT-150310-1214", rRequester) 
          End If 
          'Requester  
          pTicket = pReader.ReadString() 
          If String.IsNullOrEmpty(pTicket) Then 
            rRequester = Nothing 
          Else 
            rRequester.LoadTicket(pTicket) 
          End If 
          'returning object 
          pLength = pReader.ReadInt32 
          If pLength > 0 Then rResponse = pReader.ReadBytes(pLength) Else rResponse = New Byte() {} 
          pReader.Close() 
        End Using 
        pMemoryStream.Close() 
      End Using 
    Catch ex As Exception 
      Return pFault.LogException(66, ex, pFunctionParameters, "TRGT-150310-1510", rRequester) 
    End Try 
 
    Return pFault 
  End Function 
 
  <System.ComponentModel.DesignerCategory("")> 
  Friend Class WebClientExtended 
    Inherits System.Net.WebClient 
    'http://stackoverflow.com/questions/1789627/how-to-change-the-timeout-on-a-net-webclient-object 
    ' Note: The default timeout is 100 seconds 
    Private _TimeoutMS As Integer = 0 
    ''' <summary> 
    ''' Set the web call timeout in Milliseconds 
    ''' </summary> 
    ''' <value></value> 
    Friend WriteOnly Property Timeout() As Integer 
      Set(ByVal value As Integer) 
        _TimeoutMS = value 
      End Set 
    End Property 
 
    Friend Sub New() 
      MyBase.New() 
    End Sub 
 
    Protected Overrides Function GetWebRequest(ByVal address As System.Uri) As System.Net.WebRequest 
      Dim w As System.Net.WebRequest = MyBase.GetWebRequest(address) 
      w.PreAuthenticate = True 
      If _TimeoutMS <> 0 Then 
        w.Timeout = _TimeoutMS 
      End If 
      Return w 
    End Function 
  End Class 
 
  Friend Shared Function customCertValidation(ByVal sender As Object, 
                                           ByVal cert As System.Security.Cryptography.X509Certificates.X509Certificate, 
                                           ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, 
                                         ByVal errors As System.Net.Security.SslPolicyErrors) As Boolean 
 
    'This enables us to use the certificate issued by your company server  
    'http://zerosandtheone.com/blogs/vb/archive/2009/07/13/ssl-testing-certificate-and-the-net-framework.aspx  
 
    Dim pExpectedIssuer As String = MyController.SSLCertificateIssuer(MyController.APIServerNumber).Split("#"c)(0) 
 
    Dim pAllowSelfSigned As Boolean = False 
    Try 
      Dim pValue As String = MyController.SSLCertificateIssuer(MyController.APIServerNumber).Split("#"c)(1) 
      pValue = pValue.ToLowerInvariant() 
      If pValue = "y" Then 
        pAllowSelfSigned = True 
      ElseIf pValue = "n" Then 
        pAllowSelfSigned = False 
      Else 
        Dim pMessage As String = "Invalid suffix received to define self-signed certificate permission" 
        Throw New Exception(pMessage) 
      End If 
    Catch ex As Exception 
      Tools.LogToTextFile.WriteException("Problem with TargCCOrders.Issuer in config file", ex, "ControllerAPI") 
      Throw ex 
    End Try 
 
    Dim pActualDomain As String = CType(sender, System.Net.HttpWebRequest).Host.ToLowerInvariant() 
 
    'Now get certificate details 
    Dim pCertValidFrom As Date = System.Convert.ToDateTime(cert.GetEffectiveDateString) 
    Dim pCertValidto As Date = System.Convert.ToDateTime(cert.GetExpirationDateString) 
 
    Dim pString As String = cert.Issuer 
    Dim pStart As Integer = pString.IndexOf("CN=", StringComparison.OrdinalIgnoreCase) + 3 
    Dim pLength As Integer = pString.IndexOf(",", pStart, StringComparison.OrdinalIgnoreCase) - pStart : If pLength < 0 Then pLength = pString.Length - (pString.IndexOf("CN=", StringComparison.OrdinalIgnoreCase) + 3) 
    Dim pCertIssuer As String = pString.Substring(pStart, pLength) 
 
    pString = cert.Subject 
    pStart = pString.IndexOf("CN=", StringComparison.OrdinalIgnoreCase) + 3 
    pLength = pString.IndexOf(",", pStart, StringComparison.OrdinalIgnoreCase) - pStart : If pLength < 0 Then pLength = pString.Length - (pString.IndexOf("CN=", StringComparison.OrdinalIgnoreCase) + 3) 
    Dim pDomainIssuedFor As String = pString.Substring(pStart, pLength) 
 
    'Check Date 
    If DateTime.Now < pCertValidFrom AndAlso DateTime.Now > pCertValidto Then 
      _CertMessage = "Invalid Certificate Date" 
      Return False 
    End If 
 
    'Check that the issuer is who we expected 
    If pExpectedIssuer <> pCertIssuer Then 
      _CertMessage = "The certicate issuer is not the expected issuer" 
      Return False 
    End If 
 
    'Check SelfSigned 
    If pAllowSelfSigned = True Then 
      If pCertIssuer = pDomainIssuedFor Then 
        Return True 
      End If 
    End If 
 
    'check that it's for this domain 
    If pDomainIssuedFor <> pActualDomain Then 
      _CertMessage = "The certicate was issued for a domain other than this one." 
      Return False 
    Else 
      Return True 
    End If 
  End Function 
 
End Class 
 
Public Class ccMonetaryValue 
 
  Private _Currency As String 
  Private _Amount As Decimal 
 
  Public Property [Currency]() As String 
    Get 
      Return Me._Currency 
    End Get 
    Set(ByVal value As String) 
      Me._Currency = value 
    End Set 
  End Property 
 
  Public Property [Amount]() As Decimal 
    Get 
      Return Me._Amount 
    End Get 
    Set(ByVal value As Decimal) 
      Me._Amount = value 
    End Set 
  End Property 
 
  Public Sub New(vCurrency As String, vAmount As Decimal) 
    Me._Currency = vCurrency 
    Me._Amount = vAmount 
  End Sub 
  Public Sub New() 
    Me._Currency = "" 
    Me._Amount = 0 
  End Sub 
 
End Class 
 
Public Module clsExtensions 
 
  'IDictionary - Sample use 
  'Dim pTest As New Dictionary(Of APIFunctions.enmFeesAndLimits, Decimal) 
  'pTest.Add(APIFunctions.enmFeesAndLimits.ClientCHBFee, 500) 
  'pTest.Add(APIFunctions.enmFeesAndLimits.ClientLoadFee, 27) 
  'pTest.Add(APIFunctions.enmFeesAndLimits.ClientNewCardFee, 10) 
 
  'Dim pPbytes As Byte() = pTest.ToBinary() 
 
  'pTest = New Dictionary(Of APIFunctions.enmFeesAndLimits, Decimal) 
  'pTest = CType(pTest.FromBinary(pPbytes), Dictionary(Of APIFunctions.enmFeesAndLimits, Decimal)) 
 
  'Dim plop As Decimal = pTest(APIFunctions.enmFeesAndLimits.ClientCHBFee) 
 
  <Extension> 
  Public Function ToByteArray(ByVal vDictionary As Dictionary(Of String, Decimal)) As Byte() 
    Dim pBinary As Byte() 
 
    'Failed: Search on System.Runtime.Serialization.Formatters.Binary.BinaryAssemblyInfo.GetAssembly Unable to find assembly different assembly names 
    'Dim pMemoryStream As New System.IO.MemoryStream() 
    'Dim pBinaryFormatter As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter() 
    'pBinaryFormatter.Binder = New CustomizedBinder() 
    'pBinaryFormatter.Serialize(pMemoryStream, vDictionary) 
    'pBinary = pMemoryStream.ToArray() 
    'pMemoryStream.Close() 
 
    Dim pComboList As New clsComboList 
 
    For Each l As KeyValuePair(Of String, Decimal) In vDictionary 
      Dim pDecimal As String = CStr(l.Value).Replace(",", ".") 'set the decimal symbol, since we don't know what region we're in 
      Dim pComboListMember As New clsComboListMember() 
      pComboListMember.KeyString = l.Key 
      pComboListMember.Text = pDecimal 
      pComboList.Add(pComboListMember) 
    Next 
 
    Dim pFault As New clsFault 
    pBinary = pComboList.CreateByteArray(pFault, Nothing) 
 
    Return pBinary 
  End Function 
 
  <Extension> 
  Public Function FromByteArray(ByVal vDictionary As Dictionary(Of String, Decimal), ByVal vBytes As Byte()) As Dictionary(Of String, Decimal) 
 
    Dim pDictionary As New Dictionary(Of String, Decimal) 
 
    'We're assuming the data is in invariant culture (i.e. with a dot as the decimal symbol), since that's what we created in ToByteArray. 
 
    Dim pFault As New clsFault 
    Dim pComboList As New clsComboList(vBytes, pFault, Nothing) 
    For Each l As clsComboListMember In pComboList 
      Dim pDecimal As String = l.Text.Replace(",", ".") 
      Dim pDecimalValue As Decimal = 0 
      Dim pSucceeded As Boolean = Decimal.TryParse(pDecimal, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, pDecimalValue) 
      If Not pSucceeded Then 
        Throw New Exception($"Cannot parse the decimal value for {pDecimal}") 
      End If 
      pDictionary.Add(l.KeyString, ccHelper.ToDecimal(pDecimalValue)) 
    Next 
 
    Return pDictionary 
  End Function 
  
  <Extension> 
  Public Function ToStringExt(ByVal vDictionary As Dictionary(Of String, String)) As String 
 
    Dim pStrg As New Text.StringBuilder 
 
    For Each l In vDictionary 
      pStrg.AppendLine(l.Key & ":" & l.Value) 
    Next 
 
    Return pStrg.ToString() 
  End Function 
 
  ''' <summary> 
  ''' The strings cannot have the character '^' 
  ''' </summary> 
  ''' <param name="vList"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function ToByteArray(ByVal vList As List(Of String)) As Byte() 
    Dim pBinary As Byte() 
 
    Dim pString As New Text.StringBuilder 
 
    For Each l As String In vList 
      pString.Append(l & "^") 
    Next 
 
    Dim pFault As New clsFault 
    pBinary = ccHelper.ToByteArrayFromPlainString(pString.ToString()) 
 
    Return pBinary 
  End Function 
 
  ''' <summary> 
  ''' The strings cannot have the character '^' 
  ''' </summary> 
  ''' <param name="vList"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function FromByteArray(ByVal vList As List(Of String), ByVal vBytes As Byte()) As List(Of String) 
 
    Dim pStringList As List(Of String) 
 
    Dim p64String As String = ccHelper.ToBase64String(vBytes) 
 
    Dim pString As String = ccHelper.ToPlainString(p64String) 
 
    Dim pStringArray As String() = pString.Split("^"c) 
 
    pStringList = pStringArray.ToList() 
 
    pStringList.Remove(pStringList(pStringList.Count - 1)) 
 
    Return pStringList 
  End Function 
 
  <Extension> 
  Public Function ToByteArray(ByVal vList As List(Of Integer)) As Byte() 
    Dim pBinary As Byte() 
 
    Dim pString As New Text.StringBuilder 
 
    For Each l As Integer In vList 
      pString.Append(l & "^") 
    Next 
 
    Dim pFault As New clsFault 
    pBinary = ccHelper.ToByteArrayFromPlainString(pString.ToString()) 
 
    Return pBinary 
  End Function 
 
  <Extension> 
  Public Function FromByteArray(ByVal vList As List(Of Integer), ByVal vBytes As Byte()) As List(Of Integer) 
 
    Dim pStringList As List(Of String) 
 
    Dim p64String As String = ccHelper.ToBase64String(vBytes) 
    Dim pString As String = ccHelper.ToPlainString(p64String) 
 
    Dim pStringArray As String() = pString.Split("^"c) 
 
    pStringList = pStringArray.ToList() 
 
    pStringList.Remove(pStringList(pStringList.Count - 1)) 
 
    Dim pIntegerList As New List(Of Integer) 
 
    For Each l In pStringList 
      pIntegerList.Add(ccHelper.ToInteger(l)) 
    Next 
 
    Return pIntegerList 
  End Function 
 
  ''' <summary>  
  ''' The strings cannot have the character '^'  
  ''' </summary>  
  ''' <param name="vList"></param>  
  ''' <returns></returns>  
  <Extension> 
  Public Function ToDelimitedString(ByVal vList As List(Of String)) As String 
 
    Dim pSB As New Text.StringBuilder 
 
    For Each l As String In vList 
      pSB.Append(l & "^") 
    Next 
 
    Return pSB.ToString() 
  End Function 
 
  ''' <summary>  
  ''' The strings cannot have the character '^'  
  ''' </summary>  
  ''' <param name="vString"></param>  
  ''' <returns></returns>  
  <Extension> 
  Public Function FromDelimitedString(ByVal vString As String) As List(Of String) 
 
    Dim pStringList As List(Of String) 
 
    Dim pStringArray As String() = vString.Split("^"c) 
 
    pStringList = pStringArray.ToList() 
 
    pStringList.Remove(pStringList(pStringList.Count - 1)) 
 
    Return pStringList 
  End Function 
 
  ''' <summary> 
  ''' Throws an exception of the string length is greater than TotalWidth 
  ''' </summary> 
  ''' <param name="vStrg"></param> 
  ''' <param name="vTotalWidth"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function PadLeftAndCheck(vStrg As String, vTotalWidth As Integer) As String 
 
    If vStrg.Length > vTotalWidth Then Throw New Exception($"The string {vStrg} is longer than the padded with {vTotalWidth} ") 
    Return vStrg.PadLeft(vTotalWidth) 
 
  End Function 
 
  ''' <summary> 
  ''' Throws an exception of the string length is greater than TotalWidth 
  ''' </summary> 
  ''' <param name="vStrg"></param> 
  ''' <param name="vTotalWidth"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function PadLeftAndCheck(vStrg As String, vTotalWidth As Integer, vPaddedChar As Char) As String 
 
    If vStrg.Length > vTotalWidth Then Throw New Exception($"The string {vStrg} is longer than the padded with {vTotalWidth} ") 
    Return vStrg.PadLeft(vTotalWidth, vPaddedChar) 
 
  End Function 
 
  ''' <summary> 
  ''' Throws an exception of the string length is greater than TotalWidth 
  ''' </summary> 
  ''' <param name="vStrg"></param> 
  ''' <param name="vTotalWidth"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function PadRightAndCheck(vStrg As String, vTotalWidth As Integer) As String 
 
    If vStrg.Length > vTotalWidth Then Throw New Exception($"The string {vStrg} is longer than the padded with {vTotalWidth} ") 
    Return vStrg.PadRight(vTotalWidth) 
 
  End Function 
 
  ''' <summary> 
  ''' Throws an exception of the string length is greater than TotalWidth 
  ''' </summary> 
  ''' <param name="vStrg"></param> 
  ''' <param name="vTotalWidth"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function PadRightAndCheck(vStrg As String, vTotalWidth As Integer, vPaddedChar As Char) As String 
 
    If vStrg.Length > vTotalWidth Then Throw New Exception($"The string {vStrg} is longer than the padded with {vTotalWidth} ") 
    Return vStrg.PadRight(vTotalWidth, vPaddedChar) 
 
  End Function 
 
  ''' <summary> 
  ''' If a string is null, it returns an empty string. If it's not, it trims and returns it 
  ''' </summary> 
  ''' <param name="vStrg"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function NullToEmptyOrTrimmed(vStrg As String) As String 
 
    If vStrg Is Nothing Then 
      Return "" 
    Else 
      Return vStrg.Trim() 
    End If 
 
    Return vStrg 
 
  End Function 
 
  ''' <summary> 
  ''' If a string is null, it returns an empty string. If it's not, it trims and returns it.  
  ''' If the string length is larger that vLength, it appends ' ~~~' to it, and sets rWasTruncated to true.  
  ''' </summary> 
  ''' <param name="vStrg"></param> 
  ''' <param name="vLength"></param> 
  ''' <param name="rWasTruncated"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function Truncate(vStrg As String, vLength As Integer, ByRef rWasTruncated As Boolean) As String 
 
    If vLength = 0 Then 
      Return vStrg 
    ElseIf vStrg Is Nothing Then 
      Return "" 
    ElseIf String.IsNullOrWhiteSpace(vStrg) Then 
      Return "" 
    ElseIf vStrg.Length > vLength Then 
      rWasTruncated = True 
      Return (vStrg.Substring(0, vLength - 4) & " ~~~").Trim() 
    Else 
      Return vStrg.Trim() 
    End If 
 
    Return vStrg 
 
  End Function 
 
  ''' <summary> 
  ''' If a string is null, it returns an empty string. If it's not, it trims and returns it.  
  ''' If the string length is larger that vLength, it truncates it.  
  ''' </summary> 
  ''' <param name="vStrg"></param> 
  ''' <param name="vLength"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function TruncateSafely(vStrg As String, vLength As Integer) As String 
 
    If vLength = 0 Then 
      Return vStrg 
    ElseIf vStrg Is Nothing Then 
      Return "" 
    ElseIf String.IsNullOrWhiteSpace(vStrg) Then 
      Return "" 
    ElseIf vStrg.Length > vLength Then 
      Return (vStrg.Substring(0, vLength)).Trim() 
    Else 
      Return vStrg.Trim() 
    End If 
 
    Return vStrg 
 
  End Function 
 
  ''' <summary>   
  ''' We want to avoid saving a truncated variable back to the database. 
  ''' This can happen if we get an item from a collection, make changes, and then try to update it. 
  ''' </summary>   
  ''' <param name="vStrg"></param>   
  ''' <returns></returns>   
  <Extension> 
  Public Function CheckIfTruncated(vStrg As String) As String 
 
    If vStrg.EndsWith(" ~~~", StringComparison.OrdinalIgnoreCase) Then 
      Tools.LogToTextFile.WriteMessage($"{vStrg}{Environment.NewLine}{ccHelper.GetStack()}", "TruncatedAndExcepted") 
      Throw New Exception("The value was previously truncated and cannot be saved") 
    End If 
 
    Return vStrg 
 
  End Function 
 
  ''' <summary> 
  ''' This "Pretty Prints" any object 
  ''' </summary> 
  ''' <param name="vObj"></param> 
  ''' <returns></returns> 
  <Extension> 
  Public Function ToStringCC(vObj As Object) As String 
    Return ccHelper.ToStringCC(vObj) 
  End Function 
 
End Module 
