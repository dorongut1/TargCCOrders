Imports System.Security.Cryptography
Imports System.Text

Namespace NETEncryption
  Friend Class clsHash

    Friend Enum HashName
      UD
      SHA1
      MD5
      SHA256
      SHA384
      SHA512
    End Enum

    Friend Shared Function Hash(ByVal strPlain As String, ByVal HashName As HashName) As String
      Dim strHased As StringBuilder

      Dim pHashed As Byte() = Nothing
      'convert string to bytes
      Dim keyBytes() As Byte = System.Text.Encoding.UTF8.GetBytes(strPlain)


      'Dim sha1 As New SHA1CryptoServiceProvider
      If HashName = clsHash.HashName.SHA1 Then
        Using pSha As New SHA1CryptoServiceProvider
          pHashed = pSha.ComputeHash(keyBytes)
        End Using
      ElseIf HashName = clsHash.HashName.MD5 Then
        Using pSha As New MD5CryptoServiceProvider
          pHashed = pSha.ComputeHash(keyBytes)
        End Using
      ElseIf HashName = clsHash.HashName.SHA256 Then
#If IsNonFIPSForXP = True Then
      Using pSha As New SHA256Managed
        pHashed = pSha.ComputeHash(keyBytes)
      End Using
    ElseIf HashName = clsHash.HashName.SHA384 Then
      Using pSha As New SHA384Managed
        pHashed = pSha.ComputeHash(keyBytes)
      End Using
    ElseIf HashName = clsHash.HashName.SHA512 Then
      Using pSha As New SHA512Managed
        pHashed = pSha.ComputeHash(keyBytes)
      End Using
#Else
        Using pSha As New SHA256CryptoServiceProvider
          pHashed = pSha.ComputeHash(keyBytes)
        End Using
      ElseIf HashName = clsHash.HashName.SHA384 Then
        Using pSha As New SHA384CryptoServiceProvider
          pHashed = pSha.ComputeHash(keyBytes)
        End Using
      ElseIf HashName = clsHash.HashName.SHA512 Then
        Using pSha As New SHA512CryptoServiceProvider
          pHashed = pSha.ComputeHash(keyBytes)
        End Using
#End If
      End If

      strHased = New StringBuilder
      For Each b As Byte In pHashed
        strHased.Append(String.Format("{0:x2}", b))
      Next

      'Dim strHased As String = Nothing

      'Using objHashAlgorithm As HashAlgorithm = HashAlgorithm.Create(HashName.ToString())

      '  Dim bHashed As Byte() = objHashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(strPlain))

      '  strHased = ""
      '  For Each b As Byte In bHashed
      '    strHased += String.Format("{0:x2}", b)
      '  Next

      'End Using

      Return strHased.ToString

    End Function

    Friend Shared Function ValidateHash(ByVal strPlain As String, ByVal strHashed As String, ByVal HashName As HashName) As Boolean

      Return Hash(strPlain, HashName) = strHashed

    End Function

  End Class
End Namespace