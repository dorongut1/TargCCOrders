Imports System.IO
Imports System.Text
Imports System.Security.Cryptography

Namespace NETEncryption
  Friend Class clsAES
    'based on
    'http://www.aspsnippets.com/Articles/AES-Encryption-Decryption-Cryptography-Tutorial-with-example-in-ASPNet-using-C-and-VBNet.aspx

    ''' <summary>
    ''' Use this for one-off.
    ''' </summary>
    ''' <param name="vClearText"></param>
    ''' <param name="vEncryptionKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Encrypt(ByVal vClearText As String, ByVal vEncryptionKey As String) As String
      Try
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(vClearText)
        Using encryptor As Aes = Aes.Create()
          Dim pdb As New Rfc2898DeriveBytes(vEncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
          encryptor.Key = pdb.GetBytes(32)
          encryptor.IV = pdb.GetBytes(16)
          Using ms As New MemoryStream()
            Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
              cs.Write(clearBytes, 0, clearBytes.Length)
              cs.Close()
            End Using
            vClearText = Convert.ToBase64String(ms.ToArray())
          End Using
        End Using
        Return vClearText
      Catch ex As Exception
        Return Nothing
      End Try
    End Function

    ''' <summary>
    ''' Use this for one-off.
    ''' </summary>
    ''' <param name="vCipherText"></param>
    ''' <param name="vEncryptionKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Decrypt(ByVal vCipherText As String, ByVal vEncryptionKey As String) As String
      Try
        Dim cipherBytes As Byte() = Convert.FromBase64String(vCipherText)
        Using encryptor As Aes = Aes.Create()
          Dim pdb As New Rfc2898DeriveBytes(vEncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D,
         &H65, &H64, &H76, &H65, &H64, &H65,
         &H76})
          encryptor.Key = pdb.GetBytes(32)
          encryptor.IV = pdb.GetBytes(16)
          Using ms As New MemoryStream()
            Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
              cs.Write(cipherBytes, 0, cipherBytes.Length)
              cs.Close()
            End Using
            vCipherText = Encoding.Unicode.GetString(ms.ToArray())
          End Using
        End Using
        Return vCipherText
      Catch ex As Exception
        Return Nothing
      End Try
    End Function

    Private Shared _Key As Byte()
    Private Shared _IV As Byte()

    Friend Shared Function KeyExists() As Boolean
      Return Not (_Key Is Nothing)
    End Function

    ''' <summary>
    ''' if using multiple times, 1st create the key, since it is an expensive operation
    ''' </summary>
    ''' <param name="vKey"></param>
    ''' <remarks></remarks>
    Friend Shared Sub CreateKey(ByVal vKey As String)
      Dim pEncryptionKey As String = vKey
      Dim pdb As New Rfc2898DeriveBytes(pEncryptionKey, New Byte() {&H49, &H76, &H61, &H6E, &H20, &H4D, &H65, &H64, &H76, &H65, &H64, &H65, &H76})
      _Key = pdb.GetBytes(32)
      _IV = pdb.GetBytes(16)
    End Sub

    ''' <summary>
    ''' Use this for multiple time use. Create key 1st
    ''' </summary>
    ''' <param name="vClearText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Encrypt(ByVal vClearText As String) As String
      Dim clearBytes As Byte() = Nothing
      Try
        clearBytes = Encoding.Unicode.GetBytes(vClearText)
      Catch ex As Exception
        Return Nothing
      End Try

      Try
        Using encryptor As Aes = Aes.Create()
          If _Key Is Nothing Then
            Throw New Exception("K")
          End If
          encryptor.Key = _Key
          encryptor.IV = _IV
          Using ms As New MemoryStream()
            Using cs As New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
              cs.Write(clearBytes, 0, clearBytes.Length)
              cs.Close()
            End Using
            vClearText = Convert.ToBase64String(ms.ToArray())
          End Using
        End Using
        Return vClearText
      Catch ex As Exception
        If ex.Message = "K" Then
          Throw New Exception("You must 1st create the key using CreateKey")
        End If
        Return Nothing
      End Try
    End Function

    ''' <summary>
    ''' Use this for multiple time use. Create key 1st
    ''' </summary>
    ''' <param name="vCipherText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Shared Function Decrypt(ByVal vCipherText As String) As String

      Dim cipherBytes As Byte() = Nothing
      Try
        cipherBytes = Convert.FromBase64String(vCipherText)
      Catch ex As Exception
        Return Nothing
      End Try

      Try
        Using encryptor As Aes = Aes.Create()
          If _Key Is Nothing Then
            Throw New Exception("K")
          End If
          encryptor.Key = _Key
          encryptor.IV = _IV
          Using ms As New MemoryStream()
            Using cs As New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
              cs.Write(cipherBytes, 0, cipherBytes.Length)
              cs.Close()
            End Using
            vCipherText = Encoding.Unicode.GetString(ms.ToArray())
          End Using
        End Using
        Return vCipherText
      Catch ex As Exception
        If ex.Message = "K" Then
          Throw New Exception("You must 1st create the key using CreateKey")
        End If
        Return Nothing
      End Try
    End Function

  End Class
End Namespace