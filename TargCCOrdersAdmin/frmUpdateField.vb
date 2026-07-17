Public Class frmUpdateField

  Public Enum enmDataControl
    SingleLineTextBox
    MultiLineTextBox
    PasswordTextBox
    DateTimePicker
    CheckBox
    ComboBox
    ListBox
    UD
  End Enum

  Private _DialoguePrompt As String
  Private _ListOptions As clsComboList
  Private _Requester As clsRequester
  Private _DialogueInitialValue As Object
  Private _DialogueDataControl As enmDataControl

  Private _DialogueReturnValue As Object

  ''' <summary>
  ''' The prompt that is written in the dialogue box
  ''' </summary>
  ''' <value></value>
  ''' <remarks></remarks>
  Public WriteOnly Property DialoguePrompt() As String
    Set(ByVal value As String)
      lblPrompt.Text = value
    End Set
  End Property

  ''' <summary>
  ''' This is the list of items the user can choose from, sent as a clsComboList.
  ''' </summary>
  ''' <value></value>
  ''' <remarks></remarks>
  Public WriteOnly Property ListOptions As clsComboList
    Set(value As clsComboList)
      _ListOptions = value
    End Set
  End Property

  ''' <summary>
  ''' Requester is required if using a ComboBox
  ''' </summary>
  ''' <value></value>
  ''' <remarks></remarks>
  Public WriteOnly Property Requester As clsRequester
    Set(value As clsRequester)
      _Requester = value
    End Set
  End Property

  Public WriteOnly Property DateFormat As String
    Set(value As String)
      dtpField.CustomFormat = value
    End Set
  End Property

  ''' <summary>
  ''' This is the initial value. In the case of a ListBox and ComboBox, send a clsComboListMember 
  ''' </summary>
  ''' <value></value>
  ''' <remarks></remarks>
  Public WriteOnly Property DialogueInitialValue() As Object
    Set(ByVal value As Object)
      _DialogueInitialValue = value
      If _DialogueDataControl = enmDataControl.MultiLineTextBox Then
        txtField.Text = CStr(_DialogueInitialValue)
      ElseIf _DialogueDataControl = enmDataControl.PasswordTextBox Then
        txtField.Text = CStr(_DialogueInitialValue)
      ElseIf _DialogueDataControl = enmDataControl.SingleLineTextBox Then
        txtField.Text = CStr(_DialogueInitialValue)
      ElseIf _DialogueDataControl = enmDataControl.DateTimePicker Then
        If _DialogueInitialValue.GetType().Name.Equals("DateTimeOffset", StringComparison.OrdinalIgnoreCase) Then
          Dim pStartDate As DateTimeOffset = CType(_DialogueInitialValue, DateTimeOffset)
          If pStartDate.LocalDateTime < dtpField.MinDate Then
            pStartDate = Now
          End If
          dtpField.Value = pStartDate.LocalDateTime
        Else
          Dim pStartDate As Date = CDate(_DialogueInitialValue)
          If pStartDate < dtpField.MinDate Then
            pStartDate = Now
          End If
          dtpField.Value = pStartDate
        End If
      ElseIf _DialogueDataControl = enmDataControl.CheckBox Then
        chkField.Checked = CBool(_DialogueInitialValue)
      ElseIf _DialogueDataControl = enmDataControl.ComboBox Then
        Dim pCombolist As clsComboList = CType(cboField.DataSource, clsComboList)
        Dim pSelectedItem As clsComboListMember = CType(_DialogueInitialValue, clsComboListMember)
        Dim pSelectedValue As Object = pSelectedItem.Key
        Select Case pCombolist.KeyType
          Case clsEnums.enmComboListKeyType.Integer
            cboField.SelectedValue = CType(pSelectedValue, Integer)
          Case clsEnums.enmComboListKeyType.Long
            cboField.SelectedValue = CType(pSelectedValue, Long)
          Case clsEnums.enmComboListKeyType.String
            cboField.SelectedValue = CType(pSelectedValue, String)
          Case clsEnums.enmComboListKeyType.Enum
            cboField.SelectedValue = CType(pSelectedValue, System.Enum)
          Case clsEnums.enmComboListKeyType.Object
            cboField.SelectedValue = CType(pSelectedValue, Object)
          Case Else
            cboField.SelectedValue = Nothing
        End Select
      ElseIf _DialogueDataControl = enmDataControl.ListBox Then
        Dim pComboList As clsComboList = CType(_DialogueInitialValue, clsComboList)
        If pComboList.Count > 0 Then
          lstField.SetSelected(0, False)
          For Each l In pComboList
            If l.Text = "" Then Continue For
            lstField.SetSelected(lstField.FindStringExact(l.Text), True)
          Next
        End If
      ElseIf _DialogueDataControl = enmDataControl.UD Then
        Throw New Exception("You must define the DataControl type before setting an initial value")
      End If
    End Set
  End Property

  ''' <summary>
  ''' ComboBox and ListBox use ListOptions to load the options the user can choose from
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public Property DialogueDataControl() As enmDataControl
    Get
      Return _DialogueDataControl
    End Get
    Set(ByVal value As enmDataControl)
      _DialogueDataControl = value

      cboField.Location = txtField.Location
      lstField.Location = txtField.Location
      dtpField.Location = txtField.Location
      chkField.Location = txtField.Location

      txtField.Visible = False
      cboField.Visible = False
      lstField.Visible = False
      dtpField.Visible = False
      chkField.Visible = False

      If _DialogueDataControl = enmDataControl.MultiLineTextBox Then
        Me.AcceptButton = Nothing
        txtField.UseSystemPasswordChar = False
        txtField.Multiline = True
        txtField.Size = New Size(297, 131)
        txtField.ScrollBars = ScrollBars.Vertical
        Me.Height = 250
        txtField.Visible = True
        txtField.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
      ElseIf _DialogueDataControl = enmDataControl.PasswordTextBox Then
        Me.AcceptButton = Me.btnOK
        txtField.UseSystemPasswordChar = True
        txtField.Multiline = False
        txtField.Size = New Size(297, 25)
        txtField.ScrollBars = ScrollBars.None
        Me.Height = 130
        txtField.Visible = True
      ElseIf _DialogueDataControl = enmDataControl.SingleLineTextBox Then
        Me.AcceptButton = Me.btnOK
        txtField.UseSystemPasswordChar = False
        txtField.Multiline = False
        txtField.Size = New Size(297, 25)
        txtField.ScrollBars = ScrollBars.None
        Me.Height = 130
        txtField.Visible = True
      ElseIf _DialogueDataControl = enmDataControl.DateTimePicker Then
        Me.AcceptButton = Me.btnOK
        dtpField.Size = New Size(297, 25)
        Me.Height = 130
        dtpField.Visible = True
      ElseIf _DialogueDataControl = enmDataControl.CheckBox Then
        Me.AcceptButton = Me.btnOK
        chkField.Size = New Size(297, 25)
        Me.Height = 130
        chkField.Visible = True
      ElseIf _DialogueDataControl = enmDataControl.ComboBox Then
        Me.AcceptButton = Me.btnOK
        If _Requester Is Nothing Then
          Throw New Exception("You must assign the requester before setting the DialogueDataControl when using a combobox.")
        End If
        'Load the initial values
        Dim pFault As clsFault = LoadCbo(cboField, _ListOptions, _Requester)
        cboField.Size = New Size(297, 25)
        Me.Height = 140
        cboField.Visible = True
        cboField.Refresh()
      ElseIf _DialogueDataControl = enmDataControl.ListBox Then
        Me.AcceptButton = Me.btnOK
        'Load the initial values
        _ListOptions.SortByText()
        Dim pFault As clsFault = LoadLst(lstField, _ListOptions)
        lstField.Size = New Size(297, 131)
        Me.Height = 235
        lstField.Visible = True
        lstField.Refresh()
      End If
      Me.Refresh()
    End Set
  End Property

  ''' <summary>
  ''' The Object type is self evident, except in the case of the Combobox, where the SelectedValue is returned, and the ListBox, where a clsCombolist object is returned
  ''' </summary>
  ''' <value></value>
  ''' <returns></returns>
  ''' <remarks></remarks>
  Public ReadOnly Property DialogueReturnValue() As Object
    Get
      Return _DialogueReturnValue
    End Get
  End Property

  Private Sub CreateEmpty()
    _DialoguePrompt = ""
    _DialogueInitialValue = Nothing
    _DialogueDataControl = enmDataControl.UD

    _DialogueReturnValue = Nothing
  End Sub

  Public Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.
    CreateEmpty()
    lblPrompt.Text = ""
    txtField.Text = ""
    dtpField.Value = dtpField.MinDate
    chkField.Checked = False
    'cboField.Items.Clear()
    'lstField.Items.Clear()
  End Sub

  Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK
    If _DialogueDataControl = enmDataControl.MultiLineTextBox Then
      _DialogueReturnValue = txtField.Text
    ElseIf _DialogueDataControl = enmDataControl.PasswordTextBox Then
      _DialogueReturnValue = txtField.Text
    ElseIf _DialogueDataControl = enmDataControl.SingleLineTextBox Then
      _DialogueReturnValue = txtField.Text
    ElseIf _DialogueDataControl = enmDataControl.DateTimePicker Then
      _DialogueReturnValue = dtpField.Value
    ElseIf _DialogueDataControl = enmDataControl.CheckBox Then
      _DialogueReturnValue = chkField.Checked
    ElseIf _DialogueDataControl = enmDataControl.ComboBox Then
      _DialogueReturnValue = cboField.SelectedValue
    ElseIf _DialogueDataControl = enmDataControl.ListBox Then
      Dim pCombList As New clsComboList
      For Each l In lstField.SelectedItems
        pCombList.AddToEnd(CType(l, clsComboListMember))
      Next
      _DialogueReturnValue = pCombList
    End If
    Me.Close()
  End Sub

  Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
    _DialogueReturnValue = Nothing
    Me.Close()
  End Sub

  Private Sub frmUpdateField_Load(sender As Object, e As EventArgs) Handles Me.Load
    If Me.DesignMode = True Then Exit Sub
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)
    Me.Font = MyFont
    Me.PerformAutoScale()

    Me.MaximumSize = Screen.GetWorkingArea(Me.DesktopLocation).Size

    Me.Left = frmMain.Left + ccHelper.ToInteger((frmMain.Width - Me.Width) / 2)
    Me.Top = frmMain.Top + ccHelper.ToInteger((frmMain.Height - Me.Height) / 2)
  End Sub

End Class
