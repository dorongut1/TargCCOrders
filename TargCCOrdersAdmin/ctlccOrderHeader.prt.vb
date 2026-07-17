Partial Public Class ctlccOrderHeader

  ''' <summary>
  ''' מאפשר גישה ל-Requester מבחוץ
  ''' </summary>
  Public Property Requester() As clsRequester
    Get
      Return _Requester
    End Get
    Set(value As clsRequester)
      _Requester = value
    End Set
  End Property

End Class