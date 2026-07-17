Public Class clsMenu  
  Inherits Generic.List(Of clsMenuItem)  
  
  Public Sub New()  
    Me.Clear()  
  End Sub  
 
  Public ReadOnly Property CountLevel1 As Integer 
    Get 
      Dim pCount As Integer = 0 
      For Each pItem As clsMenuItem In Me 
        If pItem.Level = 1 Then pCount += 1 
      Next 
      Return pCount 
    End Get 
  End Property 
  Public ReadOnly Property CountLevel2 As Integer 
    Get 
      Dim pCount As Integer = 0 
      For Each pItem As clsMenuItem In Me 
        If pItem.Level = 2 Then pCount += 1 
      Next 
      Return pCount 
    End Get 
  End Property 
 
  Friend Shadows Sub Add(ByVal pMenuItem As clsMenuItem) 
    MyBase.Add(pMenuItem) 
  End Sub 
 
  Public Shadows Sub Add(ByVal vLevel As Integer, 
                         ByVal vParentCode As String, 
                         ByVal vOrdinalPosition As Integer, 
                         ByVal vCode As String, 
                         ByVal vControlName As String, 
                         ByVal vEnabled As Boolean, 
                         ByVal vText_L1 As String, 
                         Optional ByVal vText_L2 As String = "", 
                         Optional ByVal vText_L3 As String = "", 
                         Optional ByVal vText_L4 As String = "") 
    'check that the code doesn't already exist 
    For Each pItem As clsMenuItem In Me 
      If vCode = pItem.Code Then 
        Throw New Exception("Add Failed! Code already exists. Code=" & vCode) 
      End If 
    Next 
    'see if the ordinal position exists for this parent code 
    Dim pMenuItem As clsMenuItem = FindByLevelAndParentCodeAndOrdinalPosition(vLevel, vParentCode, vOrdinalPosition) 
    If pMenuItem IsNot Nothing Then 
      'move them one down 
      Dim pMenuItems As clsMenu = CloneByLevelAndParentCode(vLevel, vParentCode) 
      Dim pOrdinalPosition As Integer = vOrdinalPosition 
      For Each pMenuItem In pMenuItems 
        If pMenuItem.OrdinalPosition >= vOrdinalPosition Then 
          pOrdinalPosition += 1 
          pMenuItem.OrdinalPosition = pOrdinalPosition 
        End If 
      Next 
    End If 
    'now add it 
    pMenuItem = New clsMenuItem 
    With pMenuItem 
      .Level = vLevel 
      .OrdinalPosition = vOrdinalPosition 
      .Code = vCode 
      .ParentCode = vParentCode 
      .ControlName = vControlName 
      .Enabled = vEnabled 
      .Text_L1 = vText_L1 
      .Text_L2 = vText_L2 
      .Text_L3 = vText_L3 
      .Text_L4 = vText_L4 
    End With 
    Me.Add(pMenuItem) 
 
  End Sub 
 
  Public Shadows Sub Remove(ByVal vLevel As Integer, ByVal vParentCode As String, ByVal vOrdinalPosition As Integer) 
    Dim pMenuItemToRemove As clsMenuItem = Me.FindByLevelAndParentCodeAndOrdinalPosition(vLevel, vParentCode, vOrdinalPosition) 
    If pMenuItemToRemove Is Nothing Then Throw New Exception(String.Format("Remove Failed! MenuItem does not exist. Level={0}, ParentCode={1}, OrdinalPosition={2}", vLevel, vParentCode, vOrdinalPosition)) : Exit Sub 
 
    Me.Remove(pMenuItemToRemove) 
  End Sub 
 
  Public Shadows Sub Remove(ByVal vCode As String) 
    Dim pMenuItemToRemove As clsMenuItem = Me.FindByCode(vCode) 
    If pMenuItemToRemove Is Nothing Then Throw New Exception("Remove Failed! MenuItem does not exist: Code=" & vCode) : Exit Sub 
 
    Me.Remove(pMenuItemToRemove) 
  End Sub 
 
  Friend Shadows Sub Remove(ByVal pMenuItem As clsMenuItem) 
    Dim pMenuItems As clsMenu = CloneByLevelAndParentCode(pMenuItem.Level, pMenuItem.Code) 
    Dim pOrdinalPosition As Integer = pMenuItem.OrdinalPosition 
    For Each pMenuItemInCol In pMenuItems 
      If pMenuItemInCol Is pMenuItem Then Continue For 
      If pMenuItemInCol.OrdinalPosition >= pMenuItem.OrdinalPosition Then 
        pMenuItemInCol.OrdinalPosition = pOrdinalPosition 
        pOrdinalPosition += 1 
      End If 
    Next 
    MyBase.Remove(pMenuItem) 
  End Sub 
 
  Public Function FindByLevelAndParentCodeAndOrdinalPosition(ByVal vLevel As Integer, ByVal vParentCode As String, ByVal vOrdinalPosition As Integer) As clsMenuItem 
    For Each pMenuItem As clsMenuItem In Me 
      If pMenuItem.Level = vLevel AndAlso pMenuItem.ParentCode = vParentCode AndAlso pMenuItem.OrdinalPosition = vOrdinalPosition Then 
        Return pMenuItem 
      End If 
    Next 
    Return Nothing 
  End Function 
 
  Public Function FindByCode(ByVal vCode As String) As clsMenuItem 
    For Each pMenuItem As clsMenuItem In Me 
      If pMenuItem.Code = vCode Then 
        Return pMenuItem 
      End If 
    Next 
    Return Nothing 
  End Function 
 
  Public Function CloneByLevelAndParentCode(ByVal vLevel As Integer, ByVal vParentCode As String) As clsMenu 
    Dim pMenuItems As New clsMenu 
    For Each pMenuItem As clsMenuItem In Me 
      If pMenuItem.Level = vLevel AndAlso pMenuItem.ParentCode = vParentCode Then 
        pMenuItems.Add(pMenuItem) 
      End If 
    Next 
    SortByOrdinalPosition() 
    Return pMenuItems 
  End Function 
 
  Public Sub SortByOrdinalPosition() 
    Me.Sort(New clsMenu.CompareByOrdinalPosition) 
  End Sub 
  Private Class CompareByOrdinalPosition 
    Implements IComparer(Of clsMenuItem) 
    Private Function Compare(ByVal x As clsMenuItem, ByVal y As clsMenuItem) As Integer Implements System.Collections.Generic.IComparer(Of clsMenuItem).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
      If x.OrdinalPosition < y.OrdinalPosition Then 
        Return -1 
      ElseIf x.OrdinalPosition = y.OrdinalPosition Then 
        Return 0 
      Else 
        Return 1 
      End If 
    End Function 
  End Class 
  Public Sub SortByText_L1() 
    Me.Sort(New clsMenu.CompareByText_L1) 
  End Sub 
  Private Class CompareByText_L1 
    Implements IComparer(Of clsMenuItem) 
    Private Function Compare(ByVal x As clsMenuItem, ByVal y As clsMenuItem) As Integer Implements System.Collections.Generic.IComparer(Of clsMenuItem).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
      Return String.Compare(x.Text_L1, y.Text_L1, StringComparison.OrdinalIgnoreCase) 
    End Function 
  End Class 
 
  Public Sub SortByText_L2() 
    Me.Sort(New clsMenu.CompareByText_L2) 
  End Sub 
  Private Class CompareByText_L2 
    Implements IComparer(Of clsMenuItem) 
    Private Function Compare(ByVal x As clsMenuItem, ByVal y As clsMenuItem) As Integer Implements System.Collections.Generic.IComparer(Of clsMenuItem).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
      Return String.Compare(x.Text_L2, y.Text_L2, StringComparison.OrdinalIgnoreCase) 
    End Function 
  End Class 
 
  Public Sub SortByText_L3() 
    Me.Sort(New clsMenu.CompareByText_L3) 
  End Sub 
  Private Class CompareByText_L3 
    Implements IComparer(Of clsMenuItem) 
    Private Function Compare(ByVal x As clsMenuItem, ByVal y As clsMenuItem) As Integer Implements System.Collections.Generic.IComparer(Of clsMenuItem).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
      Return String.Compare(x.Text_L3, y.Text_L3, StringComparison.OrdinalIgnoreCase) 
    End Function 
  End Class 
 
  Public Sub SortByText_L4() 
    Me.Sort(New clsMenu.CompareByText_L4) 
  End Sub 
  Private Class CompareByText_L4 
    Implements IComparer(Of clsMenuItem) 
    Private Function Compare(ByVal x As clsMenuItem, ByVal y As clsMenuItem) As Integer Implements System.Collections.Generic.IComparer(Of clsMenuItem).Compare 
      If x Is Nothing AndAlso y Is Nothing Then Return 0 
      If x Is Nothing And Not y Is Nothing Then Return 1 
      If Not x Is Nothing And y Is Nothing Then Return -1 
      Return String.Compare(x.Text_L4, y.Text_L4, StringComparison.OrdinalIgnoreCase) 
    End Function 
  End Class 
 
  Public Class clsMenuItem 
    Private _Level As Integer 
    Private _ParentCode As String 
    Private _OrdinalPosition As Integer 
    Private _Code As String 
    Private _ControlName As String 
    Private _Enabled As Boolean 
    Private _Text_L1 As String 
    Private _Text_L2 As String 
    Private _Text_L3 As String 
    Private _Text_L4 As String 
 
    Public Property Level() As Integer 
      Get 
        Return _Level 
      End Get 
      Set(ByVal value As Integer) 
        _Level = value 
      End Set 
    End Property 
    Public Property ParentCode() As String 
      Get 
        Return _ParentCode 
      End Get 
      Set(ByVal value As String) 
        _ParentCode = value 
      End Set 
    End Property 
    Public Property OrdinalPosition() As Integer 
      Get 
        Return _OrdinalPosition 
      End Get 
      Set(ByVal value As Integer) 
        _OrdinalPosition = value 
      End Set 
    End Property 
    Public Property Code() As String 
      Get 
        Return _Code 
      End Get 
      Set(ByVal value As String) 
        _Code = value 
 
      End Set 
    End Property 
    Public Property ControlName() As String 
      Get 
        Return _ControlName 
      End Get 
      Set(ByVal value As String) 
        _ControlName = value 
      End Set 
    End Property 
    Public Property Enabled() As Boolean 
      Get 
        Return _Enabled 
      End Get 
      Set(ByVal value As Boolean) 
        _Enabled = value 
      End Set 
    End Property 
    Public Property Text_L1() As String 
      Get 
        If _Text_L1 = "" Then 
          Return _Code 
        Else 
          Return _Text_L1 
        End If 
      End Get 
      Set(ByVal value As String) 
        _Text_L1 = value 
      End Set 
    End Property 
    Public Property Text_L2() As String 
      Get 
        If _Text_L2 = "" Then 
          Return _Code 
        Else 
          Return _Text_L2 
        End If 
      End Get 
      Set(ByVal value As String) 
        _Text_L2 = value 
      End Set 
    End Property 
    Public Property Text_L3() As String 
      Get 
        If _Text_L3 = "" Then 
          Return _Code 
        Else 
          Return _Text_L3 
        End If 
      End Get 
      Set(ByVal value As String) 
        _Text_L3 = value 
      End Set 
    End Property 
    Public Property Text_L4() As String 
      Get 
        If _Text_L4 = "" Then 
          Return _Code 
        Else 
          Return _Text_L4 
        End If 
      End Get 
      Set(ByVal value As String) 
        _Text_L4 = value 
      End Set 
    End Property 
 
    Public Sub New() 
      CreateEmpty() 
    End Sub 
 
    Sub CreateEmpty() 
      _Level = 0 
      _ParentCode = "" 
      _OrdinalPosition = 0 
      _Code = "" 
      _ControlName = "" 
      _Enabled = False 
      _Text_L1 = "" 
      _Text_L2 = "" 
      _Text_L3 = "" 
      _Text_L4 = "" 
    End Sub 
  End Class 
End Class 
 
Public Class EntityEventArgs 
  Inherits EventArgs 
 
  Private _UniqueCode As Object 
  Private _Message As String 
  Private _Cancel As Boolean 
  Private _Object As Object 
  Private _SendBack As Object 
 
  Public Property UniqueCode() As Object 
    Get 
      Return _UniqueCode 
    End Get 
    Set(ByVal value As Object) 
      _UniqueCode = value 
    End Set 
  End Property 
  Public Property Message() As String 
    Get 
      Return _Message 
    End Get 
    Set(ByVal value As String) 
      _Message = value 
    End Set 
  End Property 
  Public Property Cancel() As Boolean 
    Get 
      Return _Cancel 
    End Get 
    Set(ByVal value As Boolean) 
      _Cancel = value 
    End Set 
  End Property 
  Public Property [Object]() As Object 
    Get 
      Return _Object 
    End Get 
    Set(ByVal value As Object) 
      _Object = value 
    End Set 
  End Property 
  Public Property SendBack() As Object 
    Get 
      Return _SendBack 
    End Get 
    Set(ByVal value As Object) 
      _SendBack = value 
    End Set 
  End Property 
 
 
  Public Sub New() 
    CreateEmpty() 
  End Sub 
 
  Private Sub CreateEmpty() 
    _UniqueCode = Nothing 
    _Message = "" 
    _Cancel = False 
    _Object = Nothing 
    _SendBack = Nothing 
  End Sub 
 
 
End Class 
 
Public Class CollectionEventArgs 
  Inherits EventArgs 
 
  Private _SelectedID As Long 
  Private _Message As String 
  Private _Cancel As Boolean 
  Private _SelectedObject As Object 
  Private _Collection As Object 
  Private _SendBack As Object 
 
  Public Property SelectedID() As Long 
    Get 
      Return _SelectedID 
    End Get 
    Set(ByVal value As Long) 
      _SelectedID = value 
    End Set 
  End Property 
  Public Property Message() As String 
    Get 
      Return _Message 
    End Get 
    Set(ByVal value As String) 
      _Message = value 
    End Set 
  End Property 
  Public Property Cancel() As Boolean 
    Get 
      Return _Cancel 
    End Get 
    Set(ByVal value As Boolean) 
      _Cancel = value 
    End Set 
  End Property 
  Public Property SelectedObject() As Object 
    Get 
      Return _SelectedObject 
    End Get 
    Set(ByVal value As Object) 
      _SelectedObject = value 
    End Set 
  End Property 
  Public Property Collection() As Object 
    Get 
      Return _Collection 
    End Get 
    Set(ByVal value As Object) 
      _Collection = value 
    End Set 
  End Property 
  Public Property SendBack() As Object 
    Get 
      Return _SendBack 
    End Get 
    Set(ByVal value As Object) 
      _SendBack = value 
    End Set 
  End Property 
 
  Public Sub New() 
    CreateEmpty() 
  End Sub 
 
  Private Sub CreateEmpty() 
    _SelectedID = 0 
    _Message = "" 
    _Cancel = False 
    _SelectedObject = Nothing 
    _Collection = Nothing 
    _SendBack = Nothing 
  End Sub 
 
 
End Class 
 
Public Class PanelEventArgs 
  Inherits EventArgs 
 
  Private _Message As String 
  Private _Cancel As Boolean 
  Private _Object As Object 
  Private _SendBack As Object 
 
  Public Property Message() As String 
    Get 
      Return _Message 
    End Get 
    Set(ByVal value As String) 
      _Message = value 
    End Set 
  End Property 
  Public Property Cancel() As Boolean 
    Get 
      Return _Cancel 
    End Get 
    Set(ByVal value As Boolean) 
      _Cancel = value 
    End Set 
  End Property 
  Public Property [Object]() As Object 
    Get 
      Return _Object 
    End Get 
    Set(ByVal value As Object) 
      _Object = value 
    End Set 
  End Property 
  Public Property SendBack() As Object 
    Get 
      Return _SendBack 
    End Get 
    Set(ByVal value As Object) 
      _SendBack = value 
    End Set 
  End Property 
 
  Public Sub New() 
    CreateEmpty() 
  End Sub 
 
  Private Sub CreateEmpty() 
    _Message = "" 
    _Cancel = False 
    _Object = Nothing 
    _SendBack = Nothing 
  End Sub 
 
End Class 
