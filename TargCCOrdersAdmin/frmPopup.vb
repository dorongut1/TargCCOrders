Public Class frmPopup

  Private _ParentForm As Form

  Private _Resize As Boolean

  Private Sub frmPopup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    If Me.DesignMode = True Then Exit Sub
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)
    Me.Font = MyFont
    Me.PerformAutoScale()

    'Size and locate it
    Dim pOffsetPxl As Integer = 10

    If _ParentForm Is Nothing Then
      If _Resize = True Then
        Dim pOffset As Integer = ccHelper.ToInteger(Application.OpenForms(0).Width / pOffsetPxl)
        Me.Height = Application.OpenForms(0).Height - pOffset
        Me.Width = Application.OpenForms(0).Width - pOffset
        Me.Left = Application.OpenForms(0).Left + ccHelper.ToInteger(pOffset / 2)
        Me.Top = Application.OpenForms(0).Top + ccHelper.ToInteger(pOffset / 2)
      Else
        Me.Left = Application.OpenForms(0).Left + ccHelper.ToInteger((Application.OpenForms(0).Width - Me.Width) / 2)
        Me.Top = Application.OpenForms(0).Top + ccHelper.ToInteger((Application.OpenForms(0).Height - Me.Height) / 2)
      End If
    Else
      If _Resize = True Then
        Dim pOffset As Integer = ccHelper.ToInteger(_ParentForm.Width / pOffsetPxl)
        Me.Height = _ParentForm.Height - pOffset
        Me.Width = _ParentForm.Width - pOffset
        Me.Left = _ParentForm.Left + ccHelper.ToInteger(pOffset / 2)
        Me.Top = _ParentForm.Top + ccHelper.ToInteger(pOffset / 2)
      Else
        Me.Left = _ParentForm.Left + ccHelper.ToInteger((_ParentForm.Width - Me.Width) / 2)
        Me.Top = _ParentForm.Top + ccHelper.ToInteger((_ParentForm.Height - Me.Height) / 2)
      End If
    End If

  End Sub

  'Load control 
  Public Function LoadControl(ByVal vControlName As String, ByVal vEntity As Object, ByVal vRequester As clsRequester, Optional ByVal vResize As Boolean = True) As clsFault
    Dim pFunction As String = "LoadControl"
    Dim pFault As New clsFault
    Dim pControlType As String = ""

    _Resize = vResize

    Dim pExecutingAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
    'Get the assembly for WinFControls  
    Dim pAssemblyName As String = ""
    If pExecutingAssembly.GetName.Name.IndexOf("WinFControls") >= 0 Then
      'pAssemblyName = pExecutingAssembly.GetName.Name
      pAssemblyName = (New StackFrame(0)).GetMethod().DeclaringType.Namespace
    End If

    Dim pControlName As String = ""
    Dim pClassType As Type = Nothing
    If pAssemblyName <> "" Then
      Dim pAssembly As System.Reflection.Assembly = System.Reflection.Assembly.Load(pAssemblyName)
      pControlName = pAssemblyName & "." & vControlName
      pClassType = pAssembly.GetType(pControlName)
    End If
    If pClassType Is Nothing Then
      'Dim pMyApplicationInfoAssemblyName As String = My.Application.Info.AssemblyName
      'If pMyApplicationInfoAssemblyName.EndsWith("Dev") Then pMyApplicationInfoAssemblyName = pMyApplicationInfoAssemblyName.Replace("Dev", "")
      Dim pMyApplicationInfoAssemblyName As String = My.Application.Info.AssemblyName
      pControlName = (New StackFrame(0)).GetMethod().DeclaringType.Namespace & "." & vControlName
      pClassType = pExecutingAssembly.GetType(pControlName)
    End If
    Dim pControl As Control = CType(Activator.CreateInstance(pClassType), Control)

    'Find LoadControl 
    Dim pLoad As Reflection.MethodInfo = pClassType.GetMethod("LoadControlForPopup")

    If _Resize = True Then
      Me.Font = MyFont
      Me.PerformAutoScale()
      pControl.Dock = DockStyle.Fill
      Me.Controls.Add(pControl)
    End If

    'Get Parameter to pass   
    Dim pParam() As Object

    ReDim pParam(1)
    pParam(0) = vEntity
    pParam(1) = vRequester

    'Load the control 
    Try
      pFault = CType(pLoad.Invoke(pControl, pParam), clsFault) : If Not pFault.isOK Then Return pFault
    Catch ex As Exception
      Return pFault.LogException(ex, vControlName, "TRGT-141213-1738", vRequester)
    End Try

    If _Resize = False Then
      Me.Width = pControl.Width + 20
      Me.Height = pControl.Height + 40

      Me.Font = MyFont
      Me.PerformAutoScale()

      pControl.Dock = DockStyle.Fill
      Me.Controls.Add(pControl)
    End If

    pControl.BringToFront()
    pControl.Focus()

    Return pFault
  End Function

  Public Sub LoadReadme(ByVal vParentForm As Form)

    _ParentForm = vParentForm
    _Resize = True

    Dim pControl As TextBox = New TextBox

    With pControl
      .BorderStyle = BorderStyle.None
      .BackColor = System.Drawing.Color.FloralWhite
      .Dock = System.Windows.Forms.DockStyle.Fill
      .Location = New System.Drawing.Point(0, 0)
      .Multiline = True
      .Name = "txtReadme"
      .ReadOnly = True
      .ScrollBars = System.Windows.Forms.ScrollBars.Both
      .Size = New System.Drawing.Size(488, 362)
      .TabIndex = 0
    End With

    Me.Controls.Add(pControl)

    If IO.File.Exists(My.Computer.FileSystem.CurrentDirectory & "\" & "Readme.txt") Then
      pControl.Text = My.Computer.FileSystem.ReadAllText(My.Computer.FileSystem.CurrentDirectory & "\" & "Readme.txt")
    Else
      pControl.Text = "No ReadMe file found!"
    End If

    Me.Text = "Read Me"

    pControl.Select(0, 0)

    pControl.BringToFront()


  End Sub
  
  Public Sub LoadText(vTitle As String, vText As String, vParentForm As Form)

    _ParentForm = vParentForm
    _Resize = True

    Dim pControl As TextBox = New TextBox

    With pControl
      .BorderStyle = BorderStyle.None
      .BackColor = System.Drawing.Color.FloralWhite
      .Dock = System.Windows.Forms.DockStyle.Fill
      .Location = New System.Drawing.Point(0, 0)
      .Multiline = True
      .Name = "txtReadme"
      .ReadOnly = True
      .ScrollBars = System.Windows.Forms.ScrollBars.Both
      .Size = New System.Drawing.Size(488, 362)
      .TabIndex = 0
    End With

    Me.Controls.Add(pControl)

    pControl.Text = vText

    Me.Text = vTitle

    pControl.Select(0, 0)

    pControl.BringToFront()

  End Sub

End Class