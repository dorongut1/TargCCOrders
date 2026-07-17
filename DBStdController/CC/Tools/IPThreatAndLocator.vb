Imports System.IO
Imports System.Net
Imports System.Runtime.Serialization
Imports System.Runtime.Serialization.Json
Imports System.Text
Imports Newtonsoft.Json.Linq

Namespace Tools

  Public Class IPThreatAndLocator

    Public ReadOnly Property IP As String
    Public ReadOnly Property IpType As String

    Public ReadOnly Property CurrencyCode As String
    Public ReadOnly Property CurrencyName As String

    Public ReadOnly Property CountryCode As String
    Public ReadOnly Property CountryName As String
    Public ReadOnly Property FlagEmojitwo As String
    Public ReadOnly Property FlagTwemoji As String

    Public ReadOnly Property IsAbuser As Boolean?
    Public ReadOnly Property IsAttacker As Boolean?
    Public ReadOnly Property IsBogon As Boolean?
    Public ReadOnly Property IsCloudProvider As Boolean?
    Public ReadOnly Property IsProxy As Boolean?
    Public ReadOnly Property IsRelay As Boolean?
    Public ReadOnly Property IsTor As Boolean?
    Public ReadOnly Property IsTorExit As Boolean?
    Public ReadOnly Property IsVpn As Boolean?
    Public ReadOnly Property IsAnonymous As Boolean?
    Public ReadOnly Property IsThreat As Boolean?
    Public ReadOnly Property RiskLevel As Integer?

    Public ReadOnly Property TimeZoneAbbreviation As String
    Public ReadOnly Property TimeZoneOffsetSeconds As Integer?
    Public ReadOnly Property TimeZoneCurrentTime As String
    Public ReadOnly Property TimeZoneInDaylightSaving As Boolean?
    Public ReadOnly Property Json As String

    Public Overrides Function ToString() As String
      Return ccHelper.ToStringCC(Me)
    End Function

    Private _Key As String
    Private _BaseUrl As String

    Public Sub New(vBaseUrl As String, vKey As String)
      _Key = vKey

      _BaseUrl = vBaseUrl
      If Not _BaseUrl.EndsWith("/") Then _BaseUrl &= "/"
      ClearAllProperties()

    End Sub

    Public Sub New(vUrl As String)
      _Key = ""

      _BaseUrl = vUrl
      If Not _BaseUrl.EndsWith("/") Then _BaseUrl &= "/"
      ClearAllProperties()

    End Sub

    Private Sub ClearAllProperties()
      _IP = ""
      _IpType = ""
      _CurrencyCode = ""
      _CurrencyName = ""
      _CountryCode = ""
      _CountryName = ""
      _FlagEmojitwo = ""
      _FlagTwemoji = ""
      _IsAbuser = Nothing
      _IsAttacker = Nothing
      _IsBogon = Nothing
      _IsCloudProvider = Nothing
      _IsProxy = Nothing
      _IsRelay = Nothing
      _IsTor = Nothing
      _IsTorExit = Nothing
      _IsVpn = Nothing
      _IsAnonymous = Nothing
      _IsThreat = Nothing
      _TimeZoneAbbreviation = ""
      _TimeZoneOffsetSeconds = Nothing
      _TimeZoneCurrentTime = ""
      _TimeZoneInDaylightSaving = Nothing
      _Json = ""
    End Sub

    ''' <summary>
    ''' Gets only the IP address of the caller, without any other information from api.ipify.org
    ''' </summary>
    ''' <returns></returns>
    Public Function GetMyIpOnly() As String

      'https://api.ipify.org/

      Dim pResponse As String = ""

      Dim pIP As String = ""

      ClearAllProperties()

      pResponse = FetchIP(pIP) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Return pResponse

      _IP = pIP

      Return "OK"
    End Function

    Public Function GetMyIpInfo() As String

      Dim pResponse As String = ""

      Dim pUrl = _BaseUrl

      Dim pJson As String = ""

      Tools.LogToTextFile.WriteMessage($"GetMyIpInfo URL: {pUrl}.", "IPReport")

      pResponse = FetchJson(pUrl, pJson)
      If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then
        Tools.LogToTextFile.WriteMessage($"    Failed with {pResponse}.", "IPReport")
        Return pResponse
      End If

      If _BaseUrl.IndexOf("IPRegistry", StringComparison.OrdinalIgnoreCase) >= 0 Then
        pResponse = ParseSummaryIPRegistry(pJson) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Return pResponse
      End If

      Return "OK"
    End Function

    ''' <summary>
    ''' Gets the IP information for the specified IP address
    ''' </summary>
    ''' <param name="vIP"></param>
    ''' <returns></returns>
    Public Function GetIpInfo(vIP As String) As String

      Dim pResponse As String = ""

      Dim pUrl = _BaseUrl & vIP

      Dim pJson As String = ""

      Tools.LogToTextFile.WriteMessage($"GetIpInfo URL: {pUrl}.", "IPReport")

      pResponse = FetchJson(pUrl, pJson)
      If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then
        Tools.LogToTextFile.WriteMessage($"    Failed with {pResponse}.", "IPReport")
        Return pResponse
      End If

      If _BaseUrl.IndexOf("IPRegistry", StringComparison.OrdinalIgnoreCase) >= 0 Then
        pResponse = ParseSummaryIPRegistry(pJson) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Return pResponse
      ElseIf _BaseUrl.IndexOf("ProxyCheck", StringComparison.OrdinalIgnoreCase) >= 0 Then
        pResponse = ParseSummaryProxyCheck(pJson) : If Not pResponse.Equals("OK", StringComparison.OrdinalIgnoreCase) Then Return pResponse
      End If


      Return "OK"
    End Function

    Private Function FetchJson(vURL As String, ByRef rJson As String) As String
      rJson = ""

      Dim pUrl = vURL
      If pUrl.IndexOf("proxycheck", StringComparison.OrdinalIgnoreCase) >= 0 Then
        pUrl &= $"?key={Uri.EscapeDataString(_Key)}&vpn=1&asn=1&risk=1&seen=1"
      End If


      Try
        Dim req = CType(WebRequest.Create(pUrl), HttpWebRequest)
        req.Method = "GET"
        req.Accept = "application/json"
        req.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
        If pUrl.IndexOf("IPRegistry", StringComparison.OrdinalIgnoreCase) >= 0 Then
          req.Headers(HttpRequestHeader.Authorization) = "ApiKey " & _Key
        End If
        ' .NET 4.8 defaults to TLS1.2+, but this line doesn’t hurt:
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 'Or SecurityProtocolType.Tls13

        Using resp = CType(req.GetResponse(), HttpWebResponse)
          If resp.StatusCode <> HttpStatusCode.OK Then
            Return $"FetchJson failed: Ipregistry HTTP {(CInt(resp.StatusCode))} {resp.StatusDescription}"
          End If
          Using s = resp.GetResponseStream()
            Using sr As New StreamReader(s, Encoding.UTF8)
              rJson = sr.ReadToEnd()
            End Using
          End Using
        End Using
      Catch ex As Exception
        Tools.LogToTextFile.WriteException($"FetchJson Failed!!", ex, "IPReport")
        Return $"FetchJson had an exception: {ex.Message}"
      End Try

      Return "OK"
    End Function

    Private Function FetchIP(ByRef rIP As String) As String
      rIP = ""

      Try
        Dim req = CType(WebRequest.Create(_BaseUrl), HttpWebRequest)
        req.Method = "GET"
        req.Accept = "application/json"
        req.AutomaticDecompression = DecompressionMethods.GZip Or DecompressionMethods.Deflate
        ' .NET 4.8 defaults to TLS1.2+, but this line doesn’t hurt:
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 'Or SecurityProtocolType.Tls13

        Using resp = CType(req.GetResponse(), HttpWebResponse)
          If resp.StatusCode <> HttpStatusCode.OK Then
            Return $"FetchJson failed: Ipregistry HTTP {(CInt(resp.StatusCode))} {resp.StatusDescription}"
          End If
          Using s = resp.GetResponseStream()
            Using sr As New StreamReader(s, Encoding.UTF8)
              rIP = sr.ReadToEnd()
            End Using
          End Using
        End Using
      Catch ex As Exception
        Return $"FetchIP had an exception: {ex.Message}"
      End Try

      Return "OK"
    End Function

    Private Function ParseSummaryIPRegistry(vJson As String) As String

      Dim dto As IPRegistry.ApiResponse = Nothing
      Try
        dto = Deserialize(Of IPRegistry.ApiResponse)(vJson)
      Catch ex As Exception
        Return "Error In Deserialize(Of ApiResponse)(vJson) In ParseSummary: " & ex.Message
      End Try

      _Json = vJson

      Dim pVariableAtFail As String = ""
      Try
        pVariableAtFail = "IP"
        _IP = dto.Ip
        pVariableAtFail = "IpType"
        _IpType = dto.IpType
        pVariableAtFail = "CurrencyCode"
        _CurrencyCode = dto.Currency?.Code
        pVariableAtFail = "CurrencyName"
        _CurrencyName = dto.Currency?.Name
        pVariableAtFail = "CountryCode"
        _CountryCode = dto.Location?.Country?.Code
        pVariableAtFail = "CountryName"
        _CountryName = dto.Location?.Country?.Name
        pVariableAtFail = "FlagEmojitwo"
        _FlagEmojitwo = dto.Location?.Country?.Flag?.Emojitwo
        pVariableAtFail = "FlagTwemoji"
        _FlagTwemoji = dto.Location?.Country?.Flag?.Twemoji
        pVariableAtFail = "IsAbuser"
        _IsAbuser = dto.Security?.IsAbuser
        pVariableAtFail = "IsAttacker"
        _IsAttacker = dto.Security?.IsAttacker
        pVariableAtFail = "IsBogon"
        _IsBogon = dto.Security?.IsBogon
        pVariableAtFail = "IsCloudProvider"
        _IsCloudProvider = dto.Security?.IsCloudProvider
        pVariableAtFail = "IsProxy"
        _IsProxy = dto.Security?.IsProxy
        pVariableAtFail = "IsRelay"
        _IsRelay = dto.Security?.IsRelay
        pVariableAtFail = "IsTor"
        _IsTor = dto.Security?.IsTor
        pVariableAtFail = "IsTorExit"
        _IsTorExit = dto.Security?.IsTorExit
        pVariableAtFail = "IsVpn"
        _IsVpn = dto.Security?.IsVpn
        pVariableAtFail = "IsAnonymous"
        _IsAnonymous = dto.Security?.IsAnonymous
        pVariableAtFail = "IsThreat"
        _IsThreat = dto.Security?.IsThreat
        pVariableAtFail = "TimeZoneAbbreviation"
        _TimeZoneAbbreviation = dto.TimeZone?.Abbreviation
        pVariableAtFail = "TimeZoneOffsetSeconds"
        _TimeZoneOffsetSeconds = If(dto.TimeZone IsNot Nothing, CType(dto.TimeZone.Offset, Integer?), Nothing)
        pVariableAtFail = "TimeZoneCurrentTime"
        _TimeZoneCurrentTime = dto.TimeZone?.CurrentTime
        pVariableAtFail = "TimeZoneInDaylightSaving"
        _TimeZoneInDaylightSaving = dto.TimeZone?.InDaylightSaving
      Catch ex As Exception
        Return $"Error in when trying to parse '{pVariableAtFail}': " & ex.Message
      End Try


      Return "OK"
    End Function

    Private Function ParseSummaryProxyCheck(vJson As String) As String

      If String.IsNullOrWhiteSpace(vJson) Then Throw New ArgumentException("json is empty")

      ' Parse once as a loose object to find the single IP node
      Dim root As JObject
      Try
        root = JObject.Parse(vJson)
      Catch ex As Exception
        Throw New InvalidDataException("Root JSON is not a valid JSON object.", ex)
      End Try

      ' Known non-IP keys we should ignore
      Dim skip = New HashSet(Of String)(StringComparer.OrdinalIgnoreCase) From {"status", "query_time", "query time", "node"}

      Dim ipKey As String = Nothing
      Dim ipToken As JObject = Nothing

      For Each prop As JProperty In root.Properties()
        If skip.Contains(prop.Name) Then Continue For

        Dim candidate = TryCast(prop.Value, JObject)
        If candidate IsNot Nothing Then
          ' First non-envelope object at root is our IP node
          ipKey = prop.Name
          ipToken = candidate
          Exit For
        End If
      Next

      If ipToken Is Nothing Then
        Throw New InvalidDataException("No IP node found in proxycheck response.")
      End If

      ' Re-serialize that node and feed it to your existing deserializer
      Dim nodeJson As String = ipToken.ToString(Newtonsoft.Json.Formatting.None)

      Dim rec As ProxyCheck.PcMinRecord = Deserialize(Of ProxyCheck.PcMinRecord)(nodeJson)
      ' Alternatively (if you want to bypass Deserialize(Of T)):
      ' Dim rec As ProxyCheck.PcMinRecord = JsonConvert.DeserializeObject(Of ProxyCheck.PcMinRecord)(nodeJson)

      _Json = vJson
      Dim pVariableAtFail As String = ""
      Try
        pVariableAtFail = "IP"
        _IP = ipKey
        pVariableAtFail = "IpType"
        _IpType = rec.Network.IPType
        pVariableAtFail = "CurrencyCode"
        _CurrencyCode = rec.Location.Currency?.Code
        pVariableAtFail = "CurrencyName"
        '_CurrencyName = dto.Currency?.Name
        pVariableAtFail = "CountryCode"
        _CountryCode = rec.Location.CountryCode
        pVariableAtFail = "CountryName"
        '_CountryName = dto.Location?.Country?.Name
        pVariableAtFail = "FlagEmojitwo"
        '_FlagEmojitwo = dto.Location?.Country?.Flag?.Emojitwo
        pVariableAtFail = "FlagTwemoji"
        '_FlagTwemoji = dto.Location?.Country?.Flag?.Twemoji

        pVariableAtFail = "IsAbuser"
        _IsAbuser = rec.Detections.Scraper
        pVariableAtFail = "IsAttacker"
        '_IsAttacker = dto.Security?.IsAttacker
        pVariableAtFail = "IsBogon"
        '_IsBogon = dto.Security?.IsBogon
        pVariableAtFail = "IsCloudProvider"
        _IsCloudProvider = rec.Detections.Hosting
        pVariableAtFail = "IsProxy"
        _IsProxy = rec.Detections.Proxy
        pVariableAtFail = "IsRelay"
        '_IsRelay = dto.Security?.IsRelay
        pVariableAtFail = "IsTor"
        _IsTor = rec.Detections.Tor
        pVariableAtFail = "IsTorExit"
        '_IsTorExit = dto.Security?.IsTorExit
        pVariableAtFail = "IsVpn"
        _IsVpn = rec.Detections.Vpn
        pVariableAtFail = "IsAnonymous"
        _IsAnonymous = rec.Detections.Anonymous
        pVariableAtFail = "RiskLevel"
        _RiskLevel = rec.Detections.Risk
        pVariableAtFail = "TimeZoneAbbreviation"
        '_TimeZoneAbbreviation = dto.TimeZone?.Abbreviation
        pVariableAtFail = "TimeZoneOffsetSeconds"
        '_TimeZoneOffsetSeconds = If(dto.TimeZone IsNot Nothing, CType(dto.TimeZone.Offset, Integer?), Nothing)
        pVariableAtFail = "TimeZoneCurrentTime"
        '_TimeZoneCurrentTime = dto.TimeZone?.CurrentTime
        pVariableAtFail = "TimeZoneInDaylightSaving"
        '_TimeZoneInDaylightSaving = dto.TimeZone?.InDaylightSaving
      Catch ex As Exception
        Return $"Error in when trying to parse '{pVariableAtFail}': " & ex.Message
      End Try


      Return "OK"
    End Function

    Friend Class IPRegistry
      <DataContract>
      Friend Class ApiResponse
        <DataMember(Name:="ip")> Public Property Ip As String
        <DataMember(Name:="type")> Public Property IpType As String
        <DataMember(Name:="currency")> Public Property Currency As CurrencyInfo
        <DataMember(Name:="location")> Public Property Location As LocationInfo
        <DataMember(Name:="security")> Public Property Security As SecurityInfo
        <DataMember(Name:="time_zone")> Public Property TimeZone As ApiTimeZone
      End Class

      <DataContract>
      Friend Class CurrencyInfo
        <DataMember(Name:="code")> Public Property Code As String
        <DataMember(Name:="name")> Public Property Name As String
      End Class

      <DataContract>
      Friend Class LocationInfo
        <DataMember(Name:="country")> Public Property Country As CountryInfo
      End Class

      <DataContract>
      Friend Class CountryInfo
        <DataMember(Name:="code")> Public Property Code As String
        <DataMember(Name:="name")> Public Property Name As String
        <DataMember(Name:="flag")> Public Property Flag As FlagInfo
      End Class

      <DataContract>
      Friend Class FlagInfo
        <DataMember(Name:="emojitwo")> Public Property Emojitwo As String
        <DataMember(Name:="twemoji")> Public Property Twemoji As String
      End Class

      <DataContract>
      Friend Class SecurityInfo
        <DataMember(Name:="is_abuser")> Public Property IsAbuser As Boolean?
        <DataMember(Name:="is_attacker")> Public Property IsAttacker As Boolean?
        <DataMember(Name:="is_bogon")> Public Property IsBogon As Boolean?
        <DataMember(Name:="is_cloud_provider")> Public Property IsCloudProvider As Boolean?
        <DataMember(Name:="is_proxy")> Public Property IsProxy As Boolean?
        <DataMember(Name:="is_relay")> Public Property IsRelay As Boolean?
        <DataMember(Name:="is_tor")> Public Property IsTor As Boolean?
        <DataMember(Name:="is_tor_exit")> Public Property IsTorExit As Boolean?
        <DataMember(Name:="is_vpn")> Public Property IsVpn As Boolean?
        <DataMember(Name:="is_anonymous")> Public Property IsAnonymous As Boolean?
        <DataMember(Name:="is_threat")> Public Property IsThreat As Boolean?
      End Class

      <DataContract>
      Friend Class ApiTimeZone
        <DataMember(Name:="abbreviation")> Public Property Abbreviation As String
        <DataMember(Name:="offset")> Public Property Offset As Integer   ' seconds
        <DataMember(Name:="current_time")> Public Property CurrentTime As String
        <DataMember(Name:="in_daylight_saving")> Public Property InDaylightSaving As Boolean?
      End Class
    End Class

    Friend Class ProxyCheck

      <DataContract>
      Public Class PcMinRecord
        <DataMember(Name:="network")>
        Public Property Network As PcMinNetwork
        <DataMember(Name:="location")>
        Public Property Location As PcMinLocation

        <DataMember(Name:="detections")>
        Public Property Detections As PcMinDetections
      End Class

      <DataContract>
      Public Class PcMinNetwork
        <DataMember(Name:="type")>
        Public Property IPType As String

        <DataMember(Name:="organisation")>
        Public Property Organisation As String
        <DataMember(Name:="hostname")>
        Public Property HostName As String
      End Class

      <DataContract>
      Public Class PcMinLocation
        <DataMember(Name:="country_code")>
        Public Property CountryCode As String

        <DataMember(Name:="currency")>
        Public Property Currency As PcMinCurrency
      End Class

      <DataContract>
      Public Class PcMinCurrency
        <DataMember(Name:="code")>
        Public Property Code As String
      End Class

      <DataContract>
      Public Class PcMinDetections
        <DataMember(Name:="proxy")>
        Public Property [Proxy] As Boolean
        <DataMember(Name:="vpn")>
        Public Property Vpn As Boolean
        <DataMember(Name:="compromised")>
        Public Property Compromised As Boolean
        <DataMember(Name:="scraper")>
        Public Property Scraper As Boolean
        <DataMember(Name:="tor")>
        Public Property Tor As Boolean
        <DataMember(Name:="hosting")>
        Public Property Hosting As Boolean
        <DataMember(Name:="anonymous")>
        Public Property Anonymous As Boolean
        <DataMember(Name:="risk")>
        Public Property Risk As Integer
      End Class
    End Class

    Private Function Deserialize(Of T)(json As String) As T
      Dim ser = New DataContractJsonSerializer(GetType(T))
      Using ms As New MemoryStream(Encoding.UTF8.GetBytes(json))
        Return CType(ser.ReadObject(ms), T)
      End Using
    End Function

  End Class

End Namespace
