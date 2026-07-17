Public Class frmMessageOrInputBox

  Private _WasLoaded As Boolean = False

  Private Sub frmMessageOrInputBox_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    If Me.DesignMode = True Then Exit Sub
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)
    Me.Font = MyFont
    Me.PerformAutoScale()

    If Not _WasLoaded Then
      MakeControlRTL(pnlButtons) 'to avoid possibly running it twice 
      _WasLoaded = True
    End If

    If frmMain.Visible AndAlso (frmMain.Width > 50 AndAlso frmMain.Height > 50) Then
      Dim pOffset As Integer = 80
      Me.MaximumSize = New Size(ccHelper.ToInteger(frmMain.Width - pOffset / 2), ccHelper.ToInteger(frmMain.Height - pOffset / 2))
    Else
      Me.MaximumSize = Screen.GetWorkingArea(Me.DesktopLocation).Size
    End If

    Me.Left = frmMain.Left + ccHelper.ToInteger((frmMain.Width - Me.Width) / 2)
    Me.Top = frmMain.Top + ccHelper.ToInteger((frmMain.Height - Me.Height) / 2)
  End Sub


  Public Enum enmIconType
    Exclamation
    QuestionMark
    CriticalError
    Information
    Warning
  End Enum

  Public Enum enmButtons
    Yes
    YesCancel
    YesNo
    YesNoCancel
  End Enum
  Public Enum enmButtonReturned
    Yes
    No
    Cancel
  End Enum

  Private _BtnReturned As enmButtonReturned
  Private _TextLength As Integer


  Public Function ShowMsg(vMessage As String, vIcon As enmIconType, Optional vButtons As enmButtons = enmButtons.Yes, Optional vYesText As String = "OK", Optional vNoText As String = "No", Optional vCancelText As String = "Cancel") As enmButtonReturned

    If Not Me.Visible Then
      lblMessage.Text = vMessage
    Else
      lblMessage.Text = vMessage & Environment.NewLine & lblMessage.Text
    End If

    If vIcon = enmIconType.Information Then
      pbIcon.Image = SystemIcons.Information.ToBitmap()
      'Me.BackColor = Drawing.Color.LightGreen
    ElseIf vIcon = enmIconType.QuestionMark Then
      pbIcon.Image = SystemIcons.Question.ToBitmap()
      'Me.BackColor = Drawing.Color.LightBlue
    ElseIf vIcon = enmIconType.Exclamation Then
      pbIcon.Image = SystemIcons.Exclamation.ToBitmap()
      'Me.BackColor = Drawing.Color.Yellow
    ElseIf vIcon = enmIconType.Warning Then
      pbIcon.Image = SystemIcons.Warning.ToBitmap()
      'Me.BackColor = Drawing.Color.LightYellow
    ElseIf vIcon = enmIconType.CriticalError Then
      pbIcon.Image = SystemIcons.Error.ToBitmap()
      'Me.BackColor = Drawing.Color.Red
    End If

    pnlInput.Visible = False

    Me.Text = My.Application.Info.ProductName

    If vButtons = enmButtons.Yes Then
      btnNo.Visible = False
      btnCancel.Visible = False
    ElseIf vButtons = enmButtons.YesNo Then
      btnNo.Visible = True
      btnCancel.Visible = False
    ElseIf vButtons = enmButtons.YesCancel Then
      btnNo.Visible = False
      btnCancel.Visible = True
    ElseIf vButtons = enmButtons.YesNoCancel Then
      btnNo.Visible = True
      btnCancel.Visible = True
    End If

    btnYes.Text = vYesText
    btnNo.Text = vNoText
    btnCancel.Text = vCancelText
    btnCopy.Visible = True

    If Not Me.Visible Then
      Me.ShowDialog()
    End If

    Return _BtnReturned

  End Function

  Public Function GetInput(vPrompt As String, Optional vLength As Integer = 0) As String

    lblMessage.Text = vPrompt

    pbIcon.Image = SystemIcons.Question.ToBitmap()
    Me.BackColor = Drawing.Color.White

    pnlInput.Visible = True
    txtInput.Text = ""

    Me.Text = My.Application.Info.ProductName

    btnYes.Text = "OK"

    btnNo.Visible = False
    btnCancel.Visible = True
    btnCopy.Visible = False

    _TextLength = vLength

    Me.ShowDialog()

    If _BtnReturned = enmButtonReturned.Yes Then
      Return txtInput.Text
    Else
      Return ""
    End If

  End Function

  Private Sub btnYes_Click(sender As Object, e As EventArgs) Handles btnYes.Click
    _BtnReturned = enmButtonReturned.Yes
    Me.Close()
  End Sub
  Private Sub btnNo_Click(sender As Object, e As EventArgs) Handles btnNo.Click
    _BtnReturned = enmButtonReturned.No
    Me.Close()
  End Sub
  Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
    _BtnReturned = enmButtonReturned.Cancel
    Me.Close()
  End Sub

  Private Sub btnCopy_Click(sender As Object, e As EventArgs) Handles btnCopy.Click
    My.Computer.Clipboard.SetText(lblMessage.Text, TextDataFormat.UnicodeText)
    btnCopy.Visible = False
    Dim frm As New frmMessageOrInputBox
    frm.btnCopy.Parent.Controls.Remove(frm.btnCopy)
    frm.ShowMsg("The text is now in your clipboard", frmMessageOrInputBox.enmIconType.Information)
  End Sub

  Private Sub btnCancel_KeyPress(sender As Object, e As KeyPressEventArgs) Handles btnCancel.KeyPress, btnYes.KeyPress
    If pnlInput.Visible = True Then
      txtInput.Focus()
      txtInput.Text = e.KeyChar
      txtInput.SelectionStart = 2
    End If
  End Sub

  Private Sub txtInput_TextChanged(sender As Object, e As EventArgs) Handles txtInput.TextChanged
    If txtInput.Text.Length = _TextLength AndAlso txtInput.Text.Length > 0 AndAlso _TextLength > 0 Then
      btnYes_Click(sender, e)
    End If
  End Sub
End Class