Public Class frmAbout
 
  Private _WasLoaded As Boolean = False

  Private Sub frmAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
    If Me.DesignMode = True Then Exit Sub

    If Not _WasLoaded Then
      MakeControlRTL(pnlMain) 'to avoid possibly running it twice 
      _WasLoaded = True
    End If

    If Debugger.IsAttached Then
      'Visual Studio doesn't break on unhandled exception with windows 64-bit. Note - only on form_load
      'http://social.msdn.microsoft.com/Forums/pl-PL/vsdebug/thread/69a0b831-7782-4bd9-b910-25c85f18bceb
      Try
        FormLoad()
      Catch ex As Exception
        frmMessageOrInputBox.ShowMsg(Me.GetType.Name & "_Load.UnhandledException: TRGT-121125-1334" & Environment.NewLine & Tools.LogToTextFile.GetExceptionString(ex).Replace("~", vbNewLine) & Environment.NewLine & Environment.NewLine & "This Fault could not be sent to the controller." & Environment.NewLine & "Please contact Customer Service" & vbNewLine & Environment.NewLine & "Further Details:" & Environment.NewLine & ex.ToString, frmMessageOrInputBox.enmIconType.CriticalError)
        Application.DoEvents() 
        Environment.Exit(0)
      End Try
    Else
      FormLoad()
    End If
  End Sub

  Private Sub FormLoad()
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)
    Me.Font = MyFont

    lblCompanyName.Text = My.Application.Info.CompanyName
    lblCopyright.Text = My.Application.Info.Copyright
    lblDescription.Text = My.Application.Info.Description
    lblProductName.Text = My.Application.Info.ProductName

    lblDescription.ForeColor = ccHelper.InvertColour(pnlMain.BackColor)
    lblImage.ForeColor = ccHelper.InvertColour(pnlMain.BackColor)
    lblProductName.ForeColor = ccHelper.InvertColour(pnlMain.BackColor)
    lblCopyright.ForeColor = ccHelper.InvertColour(pnlMain.BackColor)
    lblVersion.ForeColor = ccHelper.InvertColour(pnlMain.BackColor)
    lblCompanyName.ForeColor = ccHelper.InvertColour(pnlMain.BackColor)

    Try
      lblVersion.Text = "Version: " & My.Application.Info.Version.ToString & " (" & MyController.ServerName & "\" & MyController.ServerApplication & ")"
    Catch ex As Exception
      frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.CriticalError)
      Environment.Exit(0)
    End Try
  End Sub

  Public Sub ShowBorder()

    frmAbout_FontChanged(New Object, New EventArgs)

    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
    Me.Text = "About " & My.Application.Info.ProductName
    btnOK.Visible = True
    btnReadMe.Visible = True
  End Sub

  Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
    Me.Close()
  End Sub

  Private Sub btnReadMe_Click(sender As Object, e As EventArgs) Handles btnReadMe.Click
    'Show ReadMe in Notepad
    frmPopup.LoadReadme(Me)
    Cursor = Cursors.Default
    frmPopup.ShowDialog()
  End Sub

  Private Sub frmAbout_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged
    If MyFont Is Nothing Then Return
    'Me.PerformAutoScale()

    lblDescription.Font = New Font(MyFont.Name, CSng(16 * MyFont.Size / 9), lblDescription.Font.Style)
    lblImage.Font = New Font(MyFont.Name, CSng(21 * MyFont.Size / 9), lblImage.Font.Style)
    lblProductName.Font = New Font(MyFont.Name, CSng(9 * MyFont.Size / 9), lblProductName.Font.Style)
    lblCopyright.Font = New Font(MyFont.Name, CSng(9 * MyFont.Size / 9), lblCopyright.Font.Style)
    lblVersion.Font = New Font(MyFont.Name, CSng(9 * MyFont.Size / 9), lblVersion.Font.Style)
    lblCompanyName.Font = New Font(MyFont.Name, CSng(9 * MyFont.Size / 9), lblCompanyName.Font.Style)

    'Me.MaximumSize = Screen.GetWorkingArea(Me.DesktopLocation).Size

    'Me.Left = frmMain.Left + ccHelper.ToInteger((frmMain.Width - Me.Width) / 2)
    'Me.Top = frmMain.Top + ccHelper.ToInteger((frmMain.Height - Me.Height) / 2)

  End Sub

End Class