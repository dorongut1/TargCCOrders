Imports System.Security.Cryptography

Namespace NETEncryption
  Friend Class clsTripleDES
    'https://msdn.microsoft.com/en-us/library/ms172831.aspx


    Private Shared Function TruncateHash(ByVal key As String, ByVal length As Integer) As Byte()

      Dim sha1 As New SHA1CryptoServiceProvider

      ' Hash the key. 
      Dim keyBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(key)
      Dim hash() As Byte = sha1.ComputeHash(keyBytes)

      ' Truncate or pad the hash. 
      ReDim Preserve hash(length - 1)
      Return hash
    End Function

    Friend Shared Function EncryptData(ByVal plaintext As String, ByVal pKey As String) As String
      Try
        Dim TripleDes As New TripleDESCryptoServiceProvider
        TripleDes.Key = TruncateHash(pKey, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)

        ' Convert the plain-text string to a byte array. 
        Dim plaintextBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(plaintext)

        ' Create the stream. 
        Dim ms As New System.IO.MemoryStream
        ' Create the encoder to write to the stream. 
        Dim encStream As New CryptoStream(ms, TripleDes.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        encStream.Write(plaintextBytes, 0, plaintextBytes.Length)
        encStream.FlushFinalBlock()

        ' Convert the encrypted stream to a printable string. 
        Return Convert.ToBase64String(ms.ToArray)
      Catch ex As Exception
        Return Nothing
      End Try
    End Function

    Friend Shared Function DecryptData(ByVal encryptedtext As String, ByVal pKey As String) As String
      Try
        Dim TripleDes As New TripleDESCryptoServiceProvider
        TripleDes.Key = TruncateHash(pKey, TripleDes.KeySize \ 8)
        TripleDes.IV = TruncateHash("", TripleDes.BlockSize \ 8)

        ' Convert the encrypted text string to a byte array. 
        Dim encryptedBytes() As Byte = Convert.FromBase64String(encryptedtext)

        ' Create the stream. 
        Dim ms As New System.IO.MemoryStream
        ' Create the decoder to write to the stream. 
        Dim decStream As New CryptoStream(ms, TripleDes.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write)

        ' Use the crypto stream to write the byte array to the stream.
        decStream.Write(encryptedBytes, 0, encryptedBytes.Length)
        decStream.FlushFinalBlock()

        ' Convert the plain-text stream to a string. 
        Return System.Text.Encoding.UTF8.GetString(ms.ToArray)
      Catch ex As Exception
        Return Nothing
      End Try
    End Function

  End Class
End Namespace