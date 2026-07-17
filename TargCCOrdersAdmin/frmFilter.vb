Public Class frmFilter

  Private _Combo01SelectedIndex As Integer
  Private _Check01CheckState As CheckState
  Private _Date01FromValue As Date
  Private _Date01ToValue As Date
  Private _String01TextText As String
  Private _String01WCTypeSelectedIndex As Integer
  Private _Text01FromText As String
  Private _Text01ToText As String

  Private _Combo02SelectedIndex As Integer
  Private _Check02CheckState As CheckState
  Private _Date02FromValue As Date
  Private _Date02ToValue As Date
  Private _String02TextText As String
  Private _String02WCTypeSelectedIndex As Integer
  Private _Text02FromText As String
  Private _Text02ToText As String

  Private _Combo03SelectedIndex As Integer
  Private _Check03CheckState As CheckState
  Private _Date03FromValue As Date
  Private _Date03ToValue As Date
  Private _String03TextText As String
  Private _String03WCTypeSelectedIndex As Integer
  Private _Text03FromText As String
  Private _Text03ToText As String

  Private _Combo04SelectedIndex As Integer
  Private _Check04CheckState As CheckState
  Private _Date04FromValue As Date
  Private _Date04ToValue As Date
  Private _String04TextText As String
  Private _String04WCTypeSelectedIndex As Integer
  Private _Text04FromText As String
  Private _Text04ToText As String

  Private _Combo05SelectedIndex As Integer
  Private _Check05CheckState As CheckState
  Private _Date05FromValue As Date
  Private _Date05ToValue As Date
  Private _String05TextText As String
  Private _String05WCTypeSelectedIndex As Integer
  Private _Text05FromText As String
  Private _Text05ToText As String

  Private _Combo06SelectedIndex As Integer
  Private _Check06CheckState As CheckState
  Private _Date06FromValue As Date
  Private _Date06ToValue As Date
  Private _String06TextText As String
  Private _String06WCTypeSelectedIndex As Integer
  Private _Text06FromText As String
  Private _Text06ToText As String

  Private _Combo07SelectedIndex As Integer
  Private _Check07CheckState As CheckState
  Private _Date07FromValue As Date
  Private _Date07ToValue As Date
  Private _String07TextText As String
  Private _String07WCTypeSelectedIndex As Integer
  Private _Text07FromText As String
  Private _Text07ToText As String

  Private _Combo08SelectedIndex As Integer
  Private _Check08CheckState As CheckState
  Private _Date08FromValue As Date
  Private _Date08ToValue As Date
  Private _String08TextText As String
  Private _String08WCTypeSelectedIndex As Integer
  Private _Text08FromText As String
  Private _Text08ToText As String

  Private _Combo09SelectedIndex As Integer
  Private _Check09CheckState As CheckState
  Private _Date09FromValue As Date
  Private _Date09ToValue As Date
  Private _String09TextText As String
  Private _String09WCTypeSelectedIndex As Integer
  Private _Text09FromText As String
  Private _Text09ToText As String

  Private _Combo10SelectedIndex As Integer
  Private _Check10CheckState As CheckState
  Private _Date10FromValue As Date
  Private _Date10ToValue As Date
  Private _String10TextText As String
  Private _String10WCTypeSelectedIndex As Integer
  Private _Text10FromText As String
  Private _Text10ToText As String

  Private _Combo11SelectedIndex As Integer
  Private _Check11CheckState As CheckState
  Private _Date11FromValue As Date
  Private _Date11ToValue As Date
  Private _String11TextText As String
  Private _String11WCTypeSelectedIndex As Integer
  Private _Text11FromText As String
  Private _Text11ToText As String

  Private _Combo12SelectedIndex As Integer
  Private _Check12CheckState As CheckState
  Private _Date12FromValue As Date
  Private _Date12ToValue As Date
  Private _String12TextText As String
  Private _String12WCTypeSelectedIndex As Integer
  Private _Text12FromText As String
  Private _Text12ToText As String

  Private _chkGroupBy01CheckState As Boolean
  Private _chkGroupBy02CheckState As Boolean
  Private _chkGroupBy03CheckState As Boolean
  Private _chkGroupBy04CheckState As Boolean
  Private _chkGroupBy05CheckState As Boolean
  Private _chkGroupBy06CheckState As Boolean
  Private _chkGroupBy07CheckState As Boolean
  Private _chkGroupBy08CheckState As Boolean
  Private _chkGroupBy09CheckState As Boolean
  Private _chkGroupBy10CheckState As Boolean
  Private _chkGroupBy11CheckState As Boolean
  Private _chkGroupBy12CheckState As Boolean
  Private _chkGroupBy13CheckState As Boolean
  Private _chkGroupBy14CheckState As Boolean
  Private _chkGroupBy15CheckState As Boolean
  Private _chkGroupBy16CheckState As Boolean
  Private _chkGroupBy17CheckState As Boolean
  Private _chkGroupBy18CheckState As Boolean


  Private _chkSumField01CheckState As Boolean
  Private _chkSumField02CheckState As Boolean
  Private _chkSumField03CheckState As Boolean
  Private _chkSumField04CheckState As Boolean
  Private _chkSumField05CheckState As Boolean
  Private _chkSumField06CheckState As Boolean
  Private _chkSumField07CheckState As Boolean
  Private _chkSumField08CheckState As Boolean
  Private _chkSumField09CheckState As Boolean
  Private _chkSumField10CheckState As Boolean
  Private _chkSumField11CheckState As Boolean
  Private _chkSumField12CheckState As Boolean
  Private _chkSumField13CheckState As Boolean
  Private _chkSumField14CheckState As Boolean
  Private _chkSumField15CheckState As Boolean
  Private _chkSumField16CheckState As Boolean
  Private _chkSumField17CheckState As Boolean
  Private _chkSumField18CheckState As Boolean


  Private _NowStart As Date
  Private _NowEnd As Date
  Private _NowMonthStart As Date
  Private _NowMonthEnd As Date

  Private _WasLoaded As Boolean = False

  Private Sub frmFilter_Load(sender As Object, e As EventArgs) Handles Me.Load
    If Me.DesignMode = True Then Exit Sub
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)

    If Not _WasLoaded Then
      MakeControlRTL(gpbFilter) 'to avoid possibly running it twice 
      MakeControlRTL(pnlButtons)
      'btnClear_Click(sender, e)
      _WasLoaded = True
    Else
      For Each pControl As Control In flpFilter.Controls
        If pControl.GetType.Name = "DateTimePicker" Then
          Dim pdtp As DateTimePicker = CType(pControl, DateTimePicker)
          If pdtp.Name.EndsWith("To") AndAlso pdtp.CustomFormat.IndexOf("dd") < 0 AndAlso pdtp.Checked = True Then
            pdtp.Value = pdtp.Value.Date.AddDays(-(pdtp.Value.Day - 1))
          End If
        End If
      Next
    End If

    Me.Font = MyFont
    Me.PerformAutoScale()

    Me.MaximumSize = Screen.GetWorkingArea(Me.DesktopLocation).Size

    Me.Left = frmMain.Left + ccHelper.ToInteger((frmMain.Width - Me.Width) / 2)
    Me.Top = frmMain.Top + ccHelper.ToInteger((frmMain.Height - Me.Height) / 2)

  End Sub

  Sub New()

    ' This call is required by the Windows Form Designer.
    InitializeComponent()

    ' Add any initialization after the InitializeComponent() call.

    _NowStart = Now.Date
    _NowEnd = _NowStart.AddDays(1).AddSeconds(-1)
    _NowMonthStart = _NowStart.Date.AddDays(-(Now.Day - 1))
    _NowMonthEnd = _NowMonthStart

    _Combo01SelectedIndex = -1
    _Check01CheckState = CheckState.Indeterminate
    _Date01FromValue = _NowStart
    _Date01ToValue = _NowEnd
    _String01TextText = ""
    _String01WCTypeSelectedIndex = -1
    _Text01FromText = ""
    _Text01ToText = ""

    _Combo02SelectedIndex = -1
    _Check02CheckState = CheckState.Indeterminate
    _Date02FromValue = _NowStart
    _Date02ToValue = _NowEnd
    _String02TextText = ""
    _String02WCTypeSelectedIndex = -1
    _Text02FromText = ""
    _Text02ToText = ""

    _Combo03SelectedIndex = -1
    _Check03CheckState = CheckState.Indeterminate
    _Date03FromValue = _NowStart
    _Date03ToValue = _NowEnd
    _String03TextText = ""
    _String03WCTypeSelectedIndex = -1
    _Text03FromText = ""
    _Text03ToText = ""

    _Combo04SelectedIndex = -1
    _Check04CheckState = CheckState.Indeterminate
    _Date04FromValue = _NowStart
    _Date04ToValue = _NowEnd
    _String04TextText = ""
    _String04WCTypeSelectedIndex = -1
    _Text04FromText = ""
    _Text04ToText = ""

    _Combo05SelectedIndex = -1
    _Check05CheckState = CheckState.Indeterminate
    _Date05FromValue = _NowStart
    _Date05ToValue = _NowEnd
    _String05TextText = ""
    _String05WCTypeSelectedIndex = -1
    _Text05FromText = ""
    _Text05ToText = ""

    _Combo06SelectedIndex = -1
    _Check06CheckState = CheckState.Indeterminate
    _Date06FromValue = _NowStart
    _Date06ToValue = _NowEnd
    _String06TextText = ""
    _String06WCTypeSelectedIndex = -1
    _Text06FromText = ""
    _Text06ToText = ""

    _Combo07SelectedIndex = -1
    _Check07CheckState = CheckState.Indeterminate
    _Date07FromValue = _NowStart
    _Date07ToValue = _NowEnd
    _String07TextText = ""
    _String07WCTypeSelectedIndex = -1
    _Text07FromText = ""
    _Text07ToText = ""

    _Combo08SelectedIndex = -1
    _Check08CheckState = CheckState.Indeterminate
    _Date08FromValue = _NowStart
    _Date08ToValue = _NowEnd
    _String08TextText = ""
    _String08WCTypeSelectedIndex = -1
    _Text08FromText = ""
    _Text08ToText = ""

    _Combo09SelectedIndex = -1
    _Check09CheckState = CheckState.Indeterminate
    _Date09FromValue = _NowStart
    _Date09ToValue = _NowEnd
    _String09TextText = ""
    _String09WCTypeSelectedIndex = -1
    _Text09FromText = ""
    _Text09ToText = ""

    _Combo10SelectedIndex = -1
    _Check10CheckState = CheckState.Indeterminate
    _Date10FromValue = _NowStart
    _Date10ToValue = _NowEnd
    _String10TextText = ""
    _String10WCTypeSelectedIndex = -1
    _Text10FromText = ""
    _Text10ToText = ""

    _Combo11SelectedIndex = -1
    _Check11CheckState = CheckState.Indeterminate
    _Date11FromValue = _NowStart
    _Date11ToValue = _NowEnd
    _String11TextText = ""
    _String11WCTypeSelectedIndex = -1
    _Text11FromText = ""
    _Text11ToText = ""

    _Combo12SelectedIndex = -1
    _Check12CheckState = CheckState.Indeterminate
    _Date12FromValue = _NowStart
    _Date12ToValue = _NowEnd
    _String12TextText = ""
    _String12WCTypeSelectedIndex = -1
    _Text12FromText = ""
    _Text12ToText = ""

    _chkGroupBy01CheckState = False
    _chkGroupBy02CheckState = False
    _chkGroupBy03CheckState = False
    _chkGroupBy04CheckState = False
    _chkGroupBy05CheckState = False
    _chkGroupBy06CheckState = False
    _chkGroupBy07CheckState = False
    _chkGroupBy08CheckState = False
    _chkGroupBy09CheckState = False
    _chkGroupBy10CheckState = False
    _chkGroupBy11CheckState = False
    _chkGroupBy12CheckState = False
    _chkGroupBy13CheckState = False
    _chkGroupBy14CheckState = False
    _chkGroupBy15CheckState = False
    _chkGroupBy16CheckState = False
    _chkGroupBy17CheckState = False
    _chkGroupBy18CheckState = False



    _chkSumField01CheckState = False
    _chkSumField02CheckState = False
    _chkSumField03CheckState = False
    _chkSumField04CheckState = False
    _chkSumField05CheckState = False
    _chkSumField06CheckState = False
    _chkSumField07CheckState = False
    _chkSumField08CheckState = False
    _chkSumField09CheckState = False
    _chkSumField10CheckState = False
    _chkSumField11CheckState = False
    _chkSumField12CheckState = False
    _chkSumField13CheckState = False
    _chkSumField14CheckState = False
    _chkSumField15CheckState = False
    _chkSumField16CheckState = False
    _chkSumField17CheckState = False
    _chkSumField18CheckState = False

  End Sub

  Private Sub btnGo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGo.Click
    Me.DialogResult = System.Windows.Forms.DialogResult.OK


    _Combo01SelectedIndex = Combo01.SelectedIndex
    _Check01CheckState = Check01.CheckState
    _Date01FromValue = Date01From.Value
    _Date01ToValue = Date01To.Value
    If Date01From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date01FromValue = _Date01FromValue.Date
      If Date01From.Checked Then Date01From.Value = _Date01FromValue
    End If
    If Date01To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date01ToValue = _Date01ToValue.AddMonths(1).AddSeconds(-1)
      If Date01To.Checked Then Date01To.Value = _Date01ToValue
    End If
    _String01TextText = String01Text.Text
    _String01WCTypeSelectedIndex = String01WCType.SelectedIndex
    _Text01FromText = Text01From.Text
    _Text01ToText = Text01To.Text

    _Combo02SelectedIndex = Combo02.SelectedIndex
    _Check02CheckState = Check02.CheckState
    _Date02FromValue = Date02From.Value
    _Date02ToValue = Date02To.Value
    If Date02From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date02FromValue = _Date02FromValue.Date
      If Date02From.Checked Then Date02From.Value = _Date02FromValue
    End If
    If Date02To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date02ToValue = _Date02ToValue.AddMonths(1).AddSeconds(-1)
      If Date02To.Checked Then Date02To.Value = _Date02ToValue
    End If
    _String02TextText = String02Text.Text
    _String02WCTypeSelectedIndex = String02WCType.SelectedIndex
    _Text02FromText = Text02From.Text
    _Text02ToText = Text02To.Text

    _Combo03SelectedIndex = Combo03.SelectedIndex
    _Check03CheckState = Check03.CheckState
    _Date03FromValue = Date03From.Value
    _Date03ToValue = Date03To.Value
    If Date03From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date03FromValue = _Date03FromValue.Date
      If Date03From.Checked Then Date03From.Value = _Date03FromValue
    End If
    If Date03To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date03ToValue = _Date03ToValue.AddMonths(1).AddSeconds(-1)
      If Date03To.Checked Then Date03To.Value = _Date03ToValue
    End If
    _String03TextText = String03Text.Text
    _String03WCTypeSelectedIndex = String03WCType.SelectedIndex
    _Text03FromText = Text03From.Text
    _Text03ToText = Text03To.Text

    _Combo04SelectedIndex = Combo04.SelectedIndex
    _Check04CheckState = Check04.CheckState
    _Date04FromValue = Date04From.Value
    _Date04ToValue = Date04To.Value
    If Date04From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date04FromValue = _Date04FromValue.Date
      If Date04From.Checked Then Date04From.Value = _Date04FromValue
    End If
    If Date04To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date04ToValue = _Date04ToValue.AddMonths(1).AddSeconds(-1)
      If Date04To.Checked Then Date04To.Value = _Date04ToValue
    End If
    _String04TextText = String04Text.Text
    _String04WCTypeSelectedIndex = String04WCType.SelectedIndex
    _Text04FromText = Text04From.Text
    _Text04ToText = Text04To.Text

    _Combo05SelectedIndex = Combo05.SelectedIndex
    _Check05CheckState = Check05.CheckState
    _Date05FromValue = Date05From.Value
    _Date05ToValue = Date05To.Value
    If Date05From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date05FromValue = _Date05FromValue.Date
      If Date05From.Checked Then Date05From.Value = _Date05FromValue
    End If
    If Date05To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date05ToValue = _Date05ToValue.AddMonths(1).AddSeconds(-1)
      If Date05To.Checked Then Date05To.Value = _Date05ToValue
    End If
    _String05TextText = String05Text.Text
    _String05WCTypeSelectedIndex = String05WCType.SelectedIndex
    _Text05FromText = Text05From.Text
    _Text05ToText = Text05To.Text

    _Combo06SelectedIndex = Combo06.SelectedIndex
    _Check06CheckState = Check06.CheckState
    _Date06FromValue = Date06From.Value
    _Date06ToValue = Date06To.Value
    If Date06From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date06FromValue = _Date06FromValue.Date
      If Date06From.Checked Then Date06From.Value = _Date06FromValue
    End If
    If Date06To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date06ToValue = _Date06ToValue.AddMonths(1).AddSeconds(-1)
      If Date06To.Checked Then Date06To.Value = _Date06ToValue
    End If
    _String06TextText = String06Text.Text
    _String06WCTypeSelectedIndex = String06WCType.SelectedIndex
    _Text06FromText = Text06From.Text
    _Text06ToText = Text06To.Text

    _Combo07SelectedIndex = Combo07.SelectedIndex
    _Check07CheckState = Check07.CheckState
    _Date07FromValue = Date07From.Value
    _Date07ToValue = Date07To.Value
    If Date07From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date07FromValue = _Date07FromValue.Date
      If Date07From.Checked Then Date07From.Value = _Date07FromValue
    End If
    If Date07To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date07ToValue = _Date07ToValue.AddMonths(1).AddSeconds(-1)
      If Date07To.Checked Then Date07To.Value = _Date07ToValue
    End If
    _String07TextText = String07Text.Text
    _String07WCTypeSelectedIndex = String07WCType.SelectedIndex
    _Text07FromText = Text07From.Text
    _Text07ToText = Text07To.Text

    _Combo08SelectedIndex = Combo08.SelectedIndex
    _Check08CheckState = Check08.CheckState
    _Date08FromValue = Date08From.Value
    _Date08ToValue = Date08To.Value
    If Date08From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date08FromValue = _Date08FromValue.Date
      If Date08From.Checked Then Date08From.Value = _Date08FromValue
    End If
    If Date08To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date08ToValue = _Date08ToValue.AddMonths(1).AddSeconds(-1)
      If Date08To.Checked Then Date08To.Value = _Date08ToValue
    End If
    _String08TextText = String08Text.Text
    _String08WCTypeSelectedIndex = String08WCType.SelectedIndex
    _Text08FromText = Text08From.Text
    _Text08ToText = Text08To.Text

    _Combo09SelectedIndex = Combo09.SelectedIndex
    _Check09CheckState = Check09.CheckState
    _Date09FromValue = Date09From.Value
    _Date09ToValue = Date09To.Value
    If Date09From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date09FromValue = _Date09FromValue.Date
      If Date09From.Checked Then Date09From.Value = _Date09FromValue
    End If
    If Date09To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date09ToValue = _Date09ToValue.AddMonths(1).AddSeconds(-1)
      If Date09To.Checked Then Date09To.Value = _Date09ToValue
    End If
    _String09TextText = String09Text.Text
    _String09WCTypeSelectedIndex = String09WCType.SelectedIndex
    _Text09FromText = Text09From.Text
    _Text09ToText = Text09To.Text

    _Combo10SelectedIndex = Combo10.SelectedIndex
    _Check10CheckState = Check10.CheckState
    _Date10FromValue = Date10From.Value
    _Date10ToValue = Date10To.Value
    If Date10From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date10FromValue = _Date10FromValue.Date
      If Date10From.Checked Then Date10From.Value = _Date10FromValue
    End If
    If Date10To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date10ToValue = _Date10ToValue.AddMonths(1).AddSeconds(-1)
      If Date10To.Checked Then Date10To.Value = _Date10ToValue
    End If
    _String10TextText = String10Text.Text
    _String10WCTypeSelectedIndex = String10WCType.SelectedIndex
    _Text10FromText = Text10From.Text
    _Text10ToText = Text10To.Text

    _Combo11SelectedIndex = Combo11.SelectedIndex
    _Check11CheckState = Check11.CheckState
    _Date11FromValue = Date11From.Value
    _Date11ToValue = Date11To.Value
    If Date11From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date11FromValue = _Date11FromValue.Date
      If Date11From.Checked Then Date11From.Value = _Date11FromValue
    End If
    If Date11To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date11ToValue = _Date11ToValue.AddMonths(1).AddSeconds(-1)
      If Date11To.Checked Then Date11To.Value = _Date11ToValue
    End If
    _String11TextText = String11Text.Text
    _String11WCTypeSelectedIndex = String11WCType.SelectedIndex
    _Text11FromText = Text11From.Text
    _Text11ToText = Text11To.Text

    _Combo12SelectedIndex = Combo12.SelectedIndex
    _Check12CheckState = Check12.CheckState
    _Date12FromValue = Date12From.Value
    _Date12ToValue = Date12To.Value
    If Date12From.CustomFormat.IndexOf("h", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date12FromValue = _Date12FromValue.Date
      If Date12From.Checked Then Date12From.Value = _Date12FromValue
    End If
    If Date12To.CustomFormat.IndexOf("d", StringComparison.OrdinalIgnoreCase) < 0 Then
      _Date12ToValue = _Date12ToValue.AddMonths(1).AddSeconds(-1)
      If Date12To.Checked Then Date12To.Value = _Date12ToValue
    End If
    _String12TextText = String12Text.Text
    _String12WCTypeSelectedIndex = String12WCType.SelectedIndex
    _Text12FromText = Text12From.Text
    _Text12ToText = Text12To.Text


    _chkGroupBy01CheckState = chkGroupBy01.Checked
    _chkGroupBy02CheckState = chkGroupBy02.Checked
    _chkGroupBy03CheckState = chkGroupBy03.Checked
    _chkGroupBy04CheckState = chkGroupBy04.Checked
    _chkGroupBy05CheckState = chkGroupBy05.Checked
    _chkGroupBy06CheckState = chkGroupBy06.Checked
    _chkGroupBy07CheckState = chkGroupBy07.Checked
    _chkGroupBy08CheckState = chkGroupBy08.Checked
    _chkGroupBy09CheckState = chkGroupBy09.Checked
    _chkGroupBy10CheckState = chkGroupBy10.Checked
    _chkGroupBy11CheckState = chkGroupBy11.Checked
    _chkGroupBy12CheckState = chkGroupBy12.Checked
    _chkGroupBy13CheckState = chkGroupBy13.Checked
    _chkGroupBy14CheckState = chkGroupBy14.Checked
    _chkGroupBy15CheckState = chkGroupBy15.Checked
    _chkGroupBy16CheckState = chkGroupBy16.Checked
    _chkGroupBy17CheckState = chkGroupBy17.Checked
    _chkGroupBy18CheckState = chkGroupBy18.Checked

    _chkSumField01CheckState = chkSumField01.Checked
    _chkSumField02CheckState = chkSumField02.Checked
    _chkSumField03CheckState = chkSumField03.Checked
    _chkSumField04CheckState = chkSumField04.Checked
    _chkSumField05CheckState = chkSumField05.Checked
    _chkSumField06CheckState = chkSumField06.Checked
    _chkSumField07CheckState = chkSumField07.Checked
    _chkSumField08CheckState = chkSumField08.Checked
    _chkSumField09CheckState = chkSumField09.Checked
    _chkSumField10CheckState = chkSumField10.Checked
    _chkSumField11CheckState = chkSumField11.Checked
    _chkSumField12CheckState = chkSumField12.Checked
    _chkSumField13CheckState = chkSumField13.Checked
    _chkSumField14CheckState = chkSumField14.Checked
    _chkSumField15CheckState = chkSumField15.Checked
    _chkSumField16CheckState = chkSumField16.Checked
    _chkSumField17CheckState = chkSumField17.Checked
    _chkSumField18CheckState = chkSumField18.Checked

    Me.Close()
  End Sub

  Private Sub btnClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClear.Click
    For Each pControl As Control In flpFilter.Controls
      If pControl.GetType.Name = "TextBox" Then
        Dim ptxt As TextBox = CType(pControl, TextBox)
        ptxt.Text = ""
      ElseIf pControl.GetType.Name = "DateTimePicker" Then
        Dim pdtp As DateTimePicker = CType(pControl, DateTimePicker)
        If pdtp.CustomFormat.IndexOf("dd") >= 0 Then
          If pdtp.Name.EndsWith("From") Then
            pdtp.Value = _NowStart
          Else
            pdtp.Value = _NowEnd
          End If
        Else
          If pdtp.Name.EndsWith("From") Then
            pdtp.Value = _NowMonthStart
          Else
            pdtp.Value = _NowMonthStart
          End If
        End If
        'pdtp.Value = _DefaultTime
      ElseIf pControl.GetType.Name = "ComboBox" Then
        Dim pcbo As ComboBox = CType(pControl, ComboBox)
        pcbo.SelectedIndex = 0
      ElseIf pControl.GetType.Name = "IntelliCombo" Then
        Dim pcbo As IntelliCombo = CType(pControl, IntelliCombo)
        pcbo.ValueClear()
        'pcbo.ValueSelect(0)
        pcbo.cbo.SelectedIndex = -1
      ElseIf pControl.GetType.Name = "CheckBox" Then
        Dim pchk As CheckBox = CType(pControl, CheckBox)
        pchk.CheckState = CheckState.Indeterminate
      End If
    Next
    For Each pControl As Control In flpFilter.Controls
      If pControl.GetType.Name = "DateTimePicker" Then
        Dim pdtp As DateTimePicker = CType(pControl, DateTimePicker)
        pdtp.Checked = False
      End If
    Next
    txtMaxRowsToReturn.Text = ""
    rbtnNewestFirst.Checked = False
    rbtnOldestFirst.Checked = False
    For Each pControl As Control In flpGroupBy.Controls
      If pControl.GetType.Name = "TextBox" Then
        Dim ptxt As TextBox = CType(pControl, TextBox)
        ptxt.Text = ""
      ElseIf pControl.GetType.Name = "DateTimePicker" Then
        Throw New Exception("Cannot happen TRGT-230331-1336")
        'Dim pdtp As DateTimePicker = CType(pControl, DateTimePicker)
        'pdtp.Value = _DefaultTime
      ElseIf pControl.GetType.Name = "ComboBox" Then
        Dim pcbo As ComboBox = CType(pControl, ComboBox)
        pcbo.SelectedIndex = 0
      ElseIf pControl.GetType.Name = "IntelliCombo" Then
        Dim pcbo As IntelliCombo = CType(pControl, IntelliCombo)
        pcbo.ValueClear()
        'pcbo.ValueSelect(0)
        pcbo.cbo.SelectedIndex = -1
      ElseIf pControl.GetType.Name = "CheckBox" Then
        Dim pchk As CheckBox = CType(pControl, CheckBox)
        pchk.Checked = False
      End If
    Next
    For Each pControl As Control In flpSumColumns.Controls
      If pControl.GetType.Name = "TextBox" Then
        Dim ptxt As TextBox = CType(pControl, TextBox)
        ptxt.Text = ""
      ElseIf pControl.GetType.Name = "DateTimePicker" Then
        Throw New Exception("Cannot happen TRGT-230331-1337")
        'Dim pdtp As DateTimePicker = CType(pControl, DateTimePicker)
        'pdtp.Value = _DefaultTime
      ElseIf pControl.GetType.Name = "ComboBox" Then
        Dim pcbo As ComboBox = CType(pControl, ComboBox)
        pcbo.SelectedIndex = 0
      ElseIf pControl.GetType.Name = "IntelliCombo" Then
        Dim pcbo As IntelliCombo = CType(pControl, IntelliCombo)
        pcbo.ValueClear()
        'pcbo.ValueSelect(0)
        pcbo.cbo.SelectedIndex = -1
      ElseIf pControl.GetType.Name = "CheckBox" Then
        Dim pchk As CheckBox = CType(pControl, CheckBox)
        pchk.Checked = False
      End If
    Next
  End Sub

  Private Sub String01Text_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles _
         Text01From.KeyPress, Text01To.KeyPress _
       , Text02From.KeyPress, Text02To.KeyPress _
       , Text03From.KeyPress, Text03To.KeyPress _
       , Text04From.KeyPress, Text04To.KeyPress _
       , Text05From.KeyPress, Text05To.KeyPress _
       , Text06From.KeyPress, Text06To.KeyPress _
       , Text07From.KeyPress, Text07To.KeyPress _
       , Text08From.KeyPress, Text08To.KeyPress _
       , Text09From.KeyPress, Text09To.KeyPress _
       , Text10From.KeyPress, Text10To.KeyPress _
       , Text11From.KeyPress, Text11To.KeyPress _
       , Text12From.KeyPress, Text12To.KeyPress
    TextBoxHandleNumericalKeyPress(sender, e)
  End Sub

  Private Sub txtMaxRowsToReturn_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles txtMaxRowsToReturn.KeyPress
    If e.KeyChar = vbBack Then Return
    Dim p As String = e.KeyChar

    If ccHelper.IsNumeric(p) = False Then
      e.Handled = True
    End If
  End Sub

  Private Sub Date01From_ValueChanged(sender As Object, e As EventArgs) Handles Date01From.ValueChanged
    If Date01From.Checked <> Date01To.Checked Then Date01To.Checked = Date01From.Checked
  End Sub
  Private Sub Date01To_ValueChanged(sender As Object, e As EventArgs) Handles Date01To.ValueChanged
    If Date01From.Checked <> Date01To.Checked Then Date01From.Checked = Date01To.Checked
  End Sub
  Private Sub Date02From_ValueChanged(sender As Object, e As EventArgs) Handles Date02From.ValueChanged
    If Date02From.Checked <> Date02To.Checked Then Date02To.Checked = Date02From.Checked
  End Sub
  Private Sub Date02To_ValueChanged(sender As Object, e As EventArgs) Handles Date02To.ValueChanged
    If Date02From.Checked <> Date02To.Checked Then Date02From.Checked = Date02To.Checked
  End Sub
  Private Sub Date03From_ValueChanged(sender As Object, e As EventArgs) Handles Date03From.ValueChanged
    If Date03From.Checked <> Date03To.Checked Then Date03To.Checked = Date03From.Checked
  End Sub
  Private Sub Date03To_ValueChanged(sender As Object, e As EventArgs) Handles Date03To.ValueChanged
    If Date03From.Checked <> Date03To.Checked Then Date03From.Checked = Date03To.Checked
  End Sub
  Private Sub Date04From_ValueChanged(sender As Object, e As EventArgs) Handles Date04From.ValueChanged
    If Date04From.Checked <> Date04To.Checked Then Date04To.Checked = Date04From.Checked
  End Sub
  Private Sub Date04To_ValueChanged(sender As Object, e As EventArgs) Handles Date04To.ValueChanged
    If Date04From.Checked <> Date04To.Checked Then Date04From.Checked = Date04To.Checked
  End Sub
  Private Sub Date05From_ValueChanged(sender As Object, e As EventArgs) Handles Date05From.ValueChanged
    If Date05From.Checked <> Date05To.Checked Then Date05To.Checked = Date05From.Checked
  End Sub
  Private Sub Date05To_ValueChanged(sender As Object, e As EventArgs) Handles Date05To.ValueChanged
    If Date05From.Checked <> Date05To.Checked Then Date05From.Checked = Date05To.Checked
  End Sub
  Private Sub Date06From_ValueChanged(sender As Object, e As EventArgs) Handles Date06From.ValueChanged
    If Date06From.Checked <> Date06To.Checked Then Date06To.Checked = Date06From.Checked
  End Sub
  Private Sub Date06To_ValueChanged(sender As Object, e As EventArgs) Handles Date06To.ValueChanged
    If Date06From.Checked <> Date06To.Checked Then Date06From.Checked = Date06To.Checked
  End Sub
  Private Sub Date07From_ValueChanged(sender As Object, e As EventArgs) Handles Date07From.ValueChanged
    If Date07From.Checked <> Date07To.Checked Then Date07To.Checked = Date07From.Checked
  End Sub
  Private Sub Date07To_ValueChanged(sender As Object, e As EventArgs) Handles Date07To.ValueChanged
    If Date07From.Checked <> Date07To.Checked Then Date07From.Checked = Date07To.Checked
  End Sub
  Private Sub Date08From_ValueChanged(sender As Object, e As EventArgs) Handles Date08From.ValueChanged
    If Date08From.Checked <> Date08To.Checked Then Date08To.Checked = Date08From.Checked
  End Sub
  Private Sub Date08To_ValueChanged(sender As Object, e As EventArgs) Handles Date08To.ValueChanged
    If Date08From.Checked <> Date08To.Checked Then Date08From.Checked = Date08To.Checked
  End Sub
  Private Sub Date09From_ValueChanged(sender As Object, e As EventArgs) Handles Date09From.ValueChanged
    If Date09From.Checked <> Date09To.Checked Then Date09To.Checked = Date09From.Checked
  End Sub
  Private Sub Date09To_ValueChanged(sender As Object, e As EventArgs) Handles Date09To.ValueChanged
    If Date09From.Checked <> Date09To.Checked Then Date09From.Checked = Date09To.Checked
  End Sub
  Private Sub Date10From_ValueChanged(sender As Object, e As EventArgs) Handles Date10From.ValueChanged
    If Date10From.Checked <> Date10To.Checked Then Date10To.Checked = Date10From.Checked
  End Sub
  Private Sub Date10To_ValueChanged(sender As Object, e As EventArgs) Handles Date10To.ValueChanged
    If Date10From.Checked <> Date10To.Checked Then Date10From.Checked = Date10To.Checked
  End Sub
  Private Sub Date11From_ValueChanged(sender As Object, e As EventArgs) Handles Date11From.ValueChanged
    If Date11From.Checked <> Date11To.Checked Then Date11To.Checked = Date11From.Checked
  End Sub
  Private Sub Date11To_ValueChanged(sender As Object, e As EventArgs) Handles Date11To.ValueChanged
    If Date11From.Checked <> Date11To.Checked Then Date11From.Checked = Date11To.Checked
  End Sub
  Private Sub Date12From_ValueChanged(sender As Object, e As EventArgs) Handles Date12From.ValueChanged
    If Date12From.Checked <> Date12To.Checked Then Date12To.Checked = Date12From.Checked
  End Sub
  Private Sub Date12To_ValueChanged(sender As Object, e As EventArgs) Handles Date12To.ValueChanged
    If Date12From.Checked <> Date12To.Checked Then Date12From.Checked = Date12To.Checked
  End Sub

  Private Sub Combo01_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo01.evtComboListMemberChosen
    If Combo01.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo01.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo02.Clear()
    If vComboListMember Is Nothing Then
      Combo02.LoadControl(pChildCodes, "1st Choose a " & Combo02Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo02.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo02_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo02.evtComboListMemberChosen
    If Combo02.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo02.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo03.Clear()
    If vComboListMember Is Nothing Then
      Combo03.LoadControl(pChildCodes, "1st Choose a " & Combo03Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo03.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo03_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo03.evtComboListMemberChosen
    If Combo03.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo03.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo04.Clear()
    If vComboListMember Is Nothing Then
      Combo04.LoadControl(pChildCodes, "1st Choose a " & Combo04Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo04.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo04_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo04.evtComboListMemberChosen
    If Combo04.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo04.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo05.Clear()
    If vComboListMember Is Nothing Then
      Combo05.LoadControl(pChildCodes, "1st Choose a " & Combo05Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo05.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo05_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo05.evtComboListMemberChosen
    If Combo05.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo05.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo06.Clear()
    If vComboListMember Is Nothing Then
      Combo06.LoadControl(pChildCodes, "1st Choose a " & Combo06Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo06.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo06_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo06.evtComboListMemberChosen
    If Combo06.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo06.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo07.Clear()
    If vComboListMember Is Nothing Then
      Combo07.LoadControl(pChildCodes, "1st Choose a " & Combo07Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo07.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo07_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo07.evtComboListMemberChosen
    If Combo07.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo07.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo08.Clear()
    If vComboListMember Is Nothing Then
      Combo08.LoadControl(pChildCodes, "1st Choose a " & Combo08Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo08.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo08_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo08.evtComboListMemberChosen
    If Combo08.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo08.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo09.Clear()
    If vComboListMember Is Nothing Then
      Combo09.LoadControl(pChildCodes, "1st Choose a " & Combo09Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo09.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo09_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo09.evtComboListMemberChosen
    If Combo09.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo09.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo10.Clear()
    If vComboListMember Is Nothing Then
      Combo10.LoadControl(pChildCodes, "1st Choose a " & Combo10Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo10.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo10_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo10.evtComboListMemberChosen
    If Combo10.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo10.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo11.Clear()
    If vComboListMember Is Nothing Then
      Combo11.LoadControl(pChildCodes, "1st Choose a " & Combo11Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo11.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub Combo11_evtComboListMemberChosen(vComboListMember As clsComboListMember) Handles Combo11.evtComboListMemberChosen
    If Combo11.Tag Is Nothing Then Exit Sub

    Dim pParentComboList As clsComboList = CType(Combo11.Tag, clsComboList)

    Dim pChildCodes As New clsComboList

    Combo12.Clear()
    If vComboListMember Is Nothing Then
      Combo12.LoadControl(pChildCodes, "1st Choose a " & Combo12Label.Text)
    Else
      Dim pParentCode As String = CStr(vComboListMember.Key) 'always use String, so that we can replace it in the "game" below
      'Retrieve if the Key is String or Integer
      Dim pKeyType As clsEnums.enmComboListKeyType = vComboListMember.KeyType
      For Each l As clsComboListMember In pParentComboList
        If l.KeyString.StartsWith(pParentCode & "#", StringComparison.OrdinalIgnoreCase) Then
          Dim pKeyString As String = l.KeyString.Replace(pParentCode & "#", "")
          If pKeyType = clsEnums.enmComboListKeyType.String Then
            pChildCodes.Add(New clsComboListMember(pKeyString, l.Text))
          Else
            pChildCodes.Add(New clsComboListMember(ccHelper.ToInteger(pKeyString), l.Text))
          End If
        End If
      Next
      pChildCodes.SortByText()
      Combo12.LoadControl(pChildCodes, pParentComboList(0).Text)
    End If
  End Sub

  Private Sub frmFilter_FontChanged(sender As Object, e As EventArgs) Handles Me.FontChanged
    Dim pSize As Single = CSng(9 * MyFont.Size / 9)
    lblFilterBy.Font = New Font(MyFont.Name, pSize, FontStyle.Italic Or FontStyle.Bold) 'FontStyle.Bold Or 
    lblGroupBy.Font = New Font(MyFont.Name, pSize, FontStyle.Italic Or FontStyle.Bold) 'FontStyle.Bold Or 
    lblSumColumns.Font = New Font(MyFont.Name, pSize, FontStyle.Italic Or FontStyle.Bold) 'FontStyle.Bold Or 
  End Sub

End Class