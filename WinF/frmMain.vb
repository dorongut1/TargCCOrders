'Created by TargCC Version 4.0.6.3
Public Class frmMain  
 
  Private _Requester As clsRequester 
   
  Private WithEvents _ctlMenu As MenuTree 
  
  Private Event evtBeforeMenuLoad(ByRef rMenu As clsMenu)  
  Private Event evtAfterMenuLoad()  
  Private Event evtPnlControlCleared()  
  
  Private _NestedFormsCount As Integer 
  
  Private _SplitterLocation As Integer 
  
  'Tab Management 
  Private _TabContexts As New Dictionary(Of TabPage, TabContext) 
  Private _PopoutForms As New List(Of Form) 
  Private _RightClickedTabIndex As Integer = -1 
  
  Private Class TabContext 
    Public TabPage As TabPage 
    Public ContentPanel As Panel 
    Public NestedFormsCount As Integer 
    Public ActiveMenuCode As String 
    Public Sub New(tabPage As TabPage) 
      Me.TabPage = tabPage 
      Me.ContentPanel = New Panel() 
      Me.ContentPanel.Dock = DockStyle.Fill 
      Me.ContentPanel.BackColor = Color.AliceBlue 
      tabPage.Controls.Add(Me.ContentPanel) 
      Me.NestedFormsCount = 0 
      Me.ActiveMenuCode = "" 
    End Sub 
  End Class 
  
  'For Timer  
  Friend WithEvents Timer As Timer 
  Private _TimerIntervalMs As Integer = 300000 
 
  'Form Events 
  Private Sub frmMain_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load 
    'set the font 
    Dim pFontSize As Single = My.Settings.FontSize 
    If pFontSize < 6 Then 
      pFontSize = 10 
    ElseIf pFontSize > 30 Then 
      pFontSize = 10 
    End If 
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular) 
    Me.Font = MyFont 
    Me.PerformAutoScale() 
 
 
    'size window    
    If My.Settings.WindowMaximized = True Then 
      Me.WindowState = FormWindowState.Maximized 
      Me.Hide() 
      Application.DoEvents() 
    Else 
      If My.Settings.WindowLocation <> New System.Drawing.Point(0, 0) Then 
        Me.Location = My.Settings.WindowLocation 
        Me.Size = My.Settings.WindowsSize 
        Me.Hide() 
        Application.DoEvents() 
      Else 
        Me.WindowState = FormWindowState.Normal 
        Me.Size = New Size(785, 636) 
        Me.StartPosition = FormStartPosition.CenterScreen 
        Me.Hide() 
        Application.DoEvents() 
 
        My.Settings.WindowMaximized = False 
        My.Settings.WindowLocation = Me.Location 
        My.Settings.WindowsSize = Me.Size 
        My.Settings.Save() 
      End If 
    End If 
    If IsOnScreen(Me) = False Then 
      Me.WindowState = FormWindowState.Normal 
      Me.Size = New Size(785, 636) 
      Me.CenterToScreen() 
      My.Settings.WindowMaximized = False 
      My.Settings.WindowLocation = Me.Location 
      My.Settings.WindowsSize = Me.Size 
      My.Settings.Save() 
    End If 
 
    If My.Application.Info.AssemblyName.EndsWith("Dev") Then 
      pnlBottom.BackColor = System.Drawing.Color.Red 
    ElseIf My.Application.Info.AssemblyName.EndsWith("Stg") Then 
      pnlBottom.BackColor = System.Drawing.Color.Yellow 
      lblDev.Text = "Staging" 
    Else 
      pnlBottom.Visible = False 
    End If 
 
    If Debugger.IsAttached Then 
      'Visual Studio doesn't break on unhandled exception with windows 64-bit. Note - only on form_load 
      'http://social.msdn.microsoft.com/Forums/pl-PL/vsdebug/thread/69a0b831-7782-4bd9-b910-25c85f18bceb 
      Try 
        FormLoad() 
      Catch ex As Exception 
        'Invalid DBController Version. See Logged Alert 
        If ex.Message.IndexOf("See Logged Alert") >= 0 Then 'not a surprise 
          frmMessageOrInputBox.ShowMsg(Me.GetType.Name & "_Load.UnhandledException: TRGT-120801-1556" & Environment.NewLine & Environment.NewLine & ex.Message, frmMessageOrInputBox.enmIconType.CriticalError)
        Else 
          frmMessageOrInputBox.ShowMsg(Me.GetType.Name & "_Load.UnhandledException: TRGT-120801-1555" & Environment.NewLine & Tools.LogToTextFile.GetExceptionString(ex).Replace("~", vbNewLine) & Environment.NewLine & Environment.NewLine & "This Fault could not be sent to the controller." & Environment.NewLine & "Please contact Customer Service" & vbNewLine & Environment.NewLine & "Further Details:" & Environment.NewLine & ex.ToString, frmMessageOrInputBox.enmIconType.CriticalError) 
        End If  
        Application.DoEvents() 
        Environment.Exit(0) 
      End Try 
    Else 
      FormLoad() 
    End If 
    
    Timer_Tick(Nothing, Nothing) 
    
  End Sub 
  Private Sub FormLoad() 
    
    'Login here   
    Try 
      If Not My.Settings.LoginByOTP Then 
        frmLogin.LoadMe(Me) 
      Else 
        frmLoginOTP.LoadMe(Me) 
      End If 
    Catch ex As Exception 
        'Invalid DBController Version. See Logged Alert 
        If ex.Message.IndexOf("See Logged Alert") >= 0 Then 'not a surprise 
          frmMessageOrInputBox.ShowMsg(Me.GetType.Name & "_Load.UnhandledException: TRGT-160303-1857" & Environment.NewLine & Environment.NewLine & ex.Message, frmMessageOrInputBox.enmIconType.CriticalError) 
        Else 
          frmMessageOrInputBox.ShowMsg(Me.GetType.Name & "_Load.UnhandledException: TRGT-160303-1847" & Environment.NewLine & Tools.LogToTextFile.GetExceptionString(ex).Replace("~", vbNewLine) & Environment.NewLine & Environment.NewLine & "This Fault could not be sent to the controller." & Environment.NewLine & "Please contact Customer Service" & vbNewLine & Environment.NewLine & "Further Details: " & Environment.NewLine & ex.ToString, frmMessageOrInputBox.enmIconType.CriticalError) 
          frmMessageOrInputBox.ShowMsg("Have you assigned an icon to the form?" & Environment.NewLine & ex.Message, frmMessageOrInputBox.enmIconType.CriticalError) 
        End If  
      Application.DoEvents() 
      Environment.Exit(0) 
    End Try 
 
    If Not My.Settings.LoginByOTP Then 
      _Requester = frmLogin.Requester 
    Else 
      _Requester = frmLoginOTP.Requester 
    End If 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ": " & System.Reflection.MethodInfo.GetCurrentMethod().Name 
 
    Cursor = Cursors.WaitCursor : Application.DoEvents() 
 
    If _Requester.LoggedLoginID = 0 Then 
      frmMessageOrInputBox.ShowMsg("Login Failed", frmMessageOrInputBox.enmIconType.CriticalError) 
      Application.DoEvents() 
      Environment.Exit(0) 
    Else 
      My.MyApplication.SetRequester(_Requester) 
    End If 
 
    'put title on main screen  
    Me.Text = String.Format("{0} - {1} ({3}) on {2}", My.Application.Info.ProductName, _Requester.UserFullName, MyController.ServerName, _Requester.UserName) 
 
    'Set the splitter 
    Dim pSplitterDistance As Integer = My.Settings.SplitterLocation 
    If pSplitterDistance < 20 OrElse (pSplitterDistance / Me.Width > 0.8) Then 
      pSplitterDistance = 137 
    End If 
    spcMain.SplitterDistance = pSplitterDistance 
 
    Dim pMenu As clsMenu = LoadMenu() 
    _ctlMenu = New MenuTree 
    With _ctlMenu 
      .ColourBack = Color.White 
      .ColourHover = Color.SeaShell 
      .ColourChosenBack = Color.Wheat 
      .ColourChosenFore = Color.Red 
    End With 
 
    pnlTop.Visible = False 
 
    Try 
      RaiseEvent evtBeforeMenuLoad(pMenu) 
    Catch ex As Exception 
      frmMessageOrInputBox.ShowMsg(ex.Message & Environment.NewLine & ex.ToString, frmMessageOrInputBox.enmIconType.CriticalError) 
      Application.DoEvents() 
      Environment.Exit(0) 
    End Try 
 
    'Load the cache async 
    Dim pFault As clsFault = LoadInitialCache(_Requester) 'do it after evtBeforeMenuLoad, in case we have to load some variables  
    If Not pFault.isOK() Then 
      frmMessageOrInputBox.ShowMsg("Load Initial Cache Failed!" & Environment.NewLine & pFault.ShortStringForMessageBox(False), frmMessageOrInputBox.enmIconType.CriticalError) 
      Application.DoEvents() 
      Environment.Exit(0) 
    End If 
 
    _ctlMenu.Dock = DockStyle.Fill 
    spcMain.Panel1.Controls.Add(_ctlMenu) 
    _ctlMenu.BringToFront() 
 
    'now delete Task if no tasks 
    Dim pMenuItem As clsMenu.clsMenuItem = pMenu.FindByCode("Task") 
    If pMenuItem IsNot Nothing Then 
      'Dim pMenuChild As clsMenu.clsMenuItem = pMenu.FindByLevelAndParentCodeAndOrdinalPosition(2, "Task", 1) 
      Dim pMenuChildren As clsMenu = pMenu.CloneByLevelAndParentCode(2, "Task") 
      If pMenuChildren Is Nothing OrElse pMenuChildren.Count = 0 Then 
        pMenu.Remove("Task") 
      End If 
    End If 
 
    Try 
      _ctlMenu.LoadControl(pMenu) 
    Catch ex As Exception 
      frmMessageOrInputBox.ShowMsg(ex.Message, frmMessageOrInputBox.enmIconType.CriticalError) 
      Application.DoEvents() 
      Environment.Exit(0) 
    End Try 
 
    Try 
      RaiseEvent evtAfterMenuLoad() 
    Catch ex As Exception 
      frmMessageOrInputBox.ShowMsg(ex.Message & Environment.NewLine & ex.ToString, frmMessageOrInputBox.enmIconType.CriticalError) 
      Application.DoEvents() 
      Environment.Exit(0) 
    End Try 
 
    _ctlMenu.SetSplitterStatus(MenuTree.enmSplitterStatus.Pinned) 
 
    'Initialize Tabs 
    InitializeTabs() 
 
    'Threading.Thread.Sleep(2000) ' to make it look like it's loading 
 
    frmAbout.Close() 
 
    'set the language if needed 
    If My.Settings.IsLocalized AndAlso clsEnums.TranslateEnmLanguage(My.Settings.Language) <> _Requester.UILang Then 
 
      frmMessageOrInputBox.ShowMsg($"The application's UI Language is '{My.Settings.Language}', while your UI Language is defined as '{_Requester.UILang.FastToString()}'.{Environment.NewLine}Setting myself to {_Requester.UILang.FastToString()}", frmMessageOrInputBox.enmIconType.Exclamation, frmMessageOrInputBox.enmButtons.Yes) 
 
      'Get the language  
      Dim pLanguage As New csLanguage 
      pFault = pLanguage.GetByCode(_Requester.UILang.FastToString(), _Requester, True) : If pFault.isOK = False Then ShowFault(pFault, _Requester) : Exit Sub 
 
      My.Settings.Language = pLanguage.Code 
      My.Settings.Culture = pLanguage.Culture 
      My.Settings.Save() 
 
      RestartMe() 'need to start the time so I can stop it later on 
    End If 
 
    Timer = New Timer 
    Timer.Interval = _TimerIntervalMs 
    If btnMails.Visible Then Timer.Start() 
  End Sub 
  Private Sub pnlTop_VisibleChanged(sender As Object, e As EventArgs) Handles pnlTop.VisibleChanged 
    If Me.Visible = False Then Exit Sub 
    pnlTop.Refresh() 
    _ctlMenu.Enabled = True 
    pnlTop.Enabled = True 
    Cursor = Cursors.Default : Application.DoEvents() 
  End Sub 
  Private Sub ctlMenu_evtMadeVisible() Handles _ctlMenu.evtMadeVisible 
    If Me.Visible = False Then Exit Sub 
    If pnlTop.Visible = True Then Exit Sub 
    pnlTop.Refresh() 
    _ctlMenu.Enabled = True 
    pnlTop.Enabled = True 
    Cursor = Cursors.Default : Application.DoEvents() 
  End Sub 
  Private Sub _ctlMenu_VisibleChanged(sender As Object, e As EventArgs) Handles _ctlMenu.VisibleChanged 
    If Me.Visible = False Then Exit Sub 
    _ctlMenu.Refresh() 
  End Sub 
 
  Private Sub spcMain_MouseEnter(sender As Object, e As EventArgs) Handles spcMain.MouseLeave 
    If spcMain.SplitterDistance = 0 Then 
      spcMain.BackColor = Color.AliceBlue 
      spcMain.SplitterDistance = _SplitterLocation 
      _ctlMenu.SetSplitterStatus(MenuTree.enmSplitterStatus.Open) 
    ElseIf spcMain.SplitterDistance > 0 Then 
      If _ctlMenu.SplitterStatus = MenuTree.enmSplitterStatus.Open Then 
        _SplitterLocation = spcMain.SplitterDistance 
        spcMain.BackColor = Color.DarkGray 
        spcMain.SplitterDistance = 0 
        _ctlMenu.SetSplitterStatus(MenuTree.enmSplitterStatus.Closed) 
      End If 
    End If 
  End Sub 
  Private Sub spcMain_MouseDown(sender As Object, e As MouseEventArgs) Handles spcMain.MouseDown 
    _ctlMenu.SetSplitterStatus(MenuTree.enmSplitterStatus.Pinned) 
  End Sub 
 
  Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing 
    If _Requester IsNot Nothing Then _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    If Me.WindowState = FormWindowState.Maximized Then 
      My.Settings.WindowMaximized = True 
    Else 
      My.Settings.WindowMaximized = False 
      My.Settings.WindowLocation = Me.Location 
      My.Settings.WindowsSize = Me.Size 
    End If 
    My.Settings.Save() 
  End Sub 
 
  'Handle Menu 
  Private Function LoadMenu() As clsMenu 
    Dim pMenu As New clsMenu 
    
    Dim pOrd1 As Integer 
    Dim pOrd2 As Integer 
    Dim pLevel1Code As String 
    'LoadLevels 
    pOrd1 = 1 
    pLevel1Code = "Task" : pMenu.Add(1, "", pOrd1, pLevel1Code, "", True, CCTextTranslate("Tasks", _Requester)) : pOrd1 += 1 : pOrd2 = 1 
    'Add tasks here, in prt. You can also clear the menu and recreate it, using the code below as an example
    'pMenu.FindByCode("Task").Text_L1 = UITranslate("Main", _Requester) 'Find it if you want to change something (text in this case) 
    'pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Person", "ctlPnlccPerson", True, TableNameTranslate("Person", _Requester)) : pOrd2 += 1 
    'pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_LoanRequester", "ctlPnlccLoanRequester", True, TableNameTranslate("LoanRequester", _Requester)) : pOrd2 += 1 
    'pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_LoanOffer", "ctlPnlccLoanOffer", True, TableNameTranslate("LoanOffer", _Requester)) : pOrd2 += 1 
    'pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Loan", "ctlPnlccLoan", True, TableNameTranslate("Loan", _Requester)) : pOrd2 += 1 
    pLevel1Code = "Entity1" : pMenu.Add(1, "", pOrd1, pLevel1Code, "", True, CCTextTranslate("Entities 1", _Requester)) : pOrd1 += 1 : pOrd2 = 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_BeehiveBuyerTracking", "ctlPnlccBeehiveBuyerTracking", True, TableNameTranslate("BeehiveBuyerTracking", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Customer", "ctlPnlccCustomer", True, TableNameTranslate("Customer", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_CustomerDebt", "ctlPnlccCustomerDebt", True, TableNameTranslate("CustomerDebt", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Delivery", "ctlPnlccDelivery", True, TableNameTranslate("Delivery", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_OrderHeader", "ctlPnlccOrderHeader", True, TableNameTranslate("OrderHeader", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_OrderLine", "ctlPnlccOrderLine", True, TableNameTranslate("OrderLine", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Product", "ctlPnlccProduct", True, TableNameTranslate("Product", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_ProductPrice", "ctlPnlccProductPrice", True, TableNameTranslate("ProductPrice", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_ProductPriceHist", "ctlPnlccProductPriceHist", True, TableNameTranslate("ProductPriceHist", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_SupplierOrder", "ctlPnlccSupplierOrder", True, TableNameTranslate("SupplierOrder", _Requester)) : pOrd2 += 1 
 
    pLevel1Code = "Line01" : pMenu.Add(1, "", pOrd1, pLevel1Code, "", True, "----------") : pOrd1 += 1 : pOrd2 = 1 
 
    pLevel1Code = "Security" : pMenu.Add(1, "", pOrd1, pLevel1Code, "", True, CCTextTranslate("Security", _Requester)) : pOrd1 += 1 : pOrd2 = 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_User", "ctlPnlc_User", True, TableNameTranslate("User", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Role", "ctlPnlc_Role", True, TableNameTranslate("Role", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_UserLoginKey", "ctlPnlc_UserLoginKey", True, TableNameTranslate("UserLoginKey", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Permission", "ctlPnlc_Permission", True, TableNameTranslate("Permission", _Requester)) : pOrd2 += 1 
 
    pLevel1Code = "Monitor" : pMenu.Add(1, "", pOrd1, pLevel1Code, "", True, CCTextTranslate("Monitor", _Requester)) : pOrd1 += 1 : pOrd2 = 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_LoggedAlert", "ctlPnlc_LoggedAlert", True, TableNameTranslate("LoggedAlert", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Job", "ctlPnlc_Job", True, TableNameTranslate("Job", _Requester)) : pOrd2 += 1 
 
    pLevel1Code = "Definition" : pMenu.Add(1, "", pOrd1, pLevel1Code, "", True, CCTextTranslate("Definition", _Requester)) : pOrd1 += 1 : pOrd2 = 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Maintenance", "ctlPnlMaintenance", True, "My Settings") : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_SystemDefault", "ctlPnlc_SystemDefault", True, TableNameTranslate("SystemDefault", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_AlertMessage", "ctlPnlc_AlertMessage", True, TableNameTranslate("AlertMessage", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Enumeration", "ctlPnlc_Enumeration", True, TableNameTranslate("Enumeration", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Lookup", "ctlPnlc_Lookup", True, TableNameTranslate("Lookup", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_ObjectToTranslate", "ctlPnlc_ObjectToTranslate", True, TableNameTranslate("ObjectToTranslate", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_ObjectTranslation", "ctlPnlc_ObjectTranslation", True, TableNameTranslate("ObjectTranslation", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Process", "ctlPnlc_Process", True, TableNameTranslate("Process", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Language", "ctlPnlc_Language", True, TableNameTranslate("Language", _Requester)) : pOrd2 += 1 
 
    pLevel1Code = "Audit" : pMenu.Add(1, "", pOrd1, pLevel1Code, "", True, CCTextTranslate("Audit", _Requester)) : pOrd1 += 1 : pOrd2 = 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_AuditIndexed", "ctlPnlc_AuditIndexed", True, TableNameTranslate("AuditIndexed", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_LoggedJob", "ctlPnlc_LoggedJob", True, TableNameTranslate("LoggedJob", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_LoggedLogin", "ctlPnlc_LoggedLogin", True, TableNameTranslate("LoggedLogin", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_LoggedRequest", "ctlPnlc_LoggedRequest", True, TableNameTranslate("LoggedRequest", _Requester)) : pOrd2 += 1 
    pMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_MFA", "ctlPnlc_MFA", True, TableNameTranslate("MFA", _Requester)) : pOrd2 += 1 
 
    Return pMenu 
  End Function 
  Private Sub BackClickedInPanel(ByVal sender As Object, ByVal e As PanelEventArgs) 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pStrg As String = "" 
    Dim pControl As Control = Nothing 
 
    'Get active tab context 
    Dim pActivePanel As Panel = GetActiveTabPanel() 
    Dim pActiveContext As TabContext = GetActiveTabContext() 
    If pActivePanel Is Nothing OrElse pActiveContext Is Nothing Then Exit Sub 
 
    'remove top control 
    If pActiveContext.NestedFormsCount > 0 Then 
      pActivePanel.Controls.RemoveAt(0) 
      pActiveContext.NestedFormsCount -= 1 
      If pActivePanel.Controls.Count > 0 Then 
        _ctlMenu.ActivateMenuItem(pActivePanel.Controls(0).Name) 
        pActiveContext.ActiveMenuCode = pActivePanel.Controls(0).Name 
        UpdateTabTitle(pActiveContext.TabPage, pActiveContext.ActiveMenuCode) 
      End If 
    End If 
  End Sub 
 
  Private Sub _ctlMenu_evtHelpClicked() Handles _ctlMenu.evtHelpClicked 
    Dim pStrg As New System.Text.StringBuilder 
    frmAbout.ShowBorder() 
    frmAbout.ShowDialog(Me) 
  End Sub 
  Private Sub _ctlMenu_evtLinkClicked(ByVal vMenuItem As clsMenu.clsMenuItem) Handles _ctlMenu.evtLinkClicked 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name & ":" & vMenuItem.Code 
    Dim pFault As New clsFault 
 
    Cursor = Cursors.WaitCursor 
    _ctlMenu.Enabled = False 
    Application.DoEvents() 
 
    If vMenuItem.Level = 1 Then 
      RaiseEvent evtPnlControlCleared() 
      pFault.SetOK() 
    Else 
      pnlControl.BackgroundImage = Nothing 
      pFault = LoadControl(vMenuItem.ControlName, vMenuItem.Code, -1) 
    End If 
 
    _ctlMenu.Enabled = True 
    Application.DoEvents() 
 
    Cursor = Cursors.Default 
 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Load control 
  Private Function LoadControl(ByVal vControlName As String, ByVal vMenuCode As String, ByVal vParentID As Long) As clsFault 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name & ":" & vMenuCode 
    Dim pFault As New clsFault 
    Dim pControlType As String = "" 
 
    pnlMessage.Visible = False 
 
    'Build the screen key with ID if applicable 
    Dim pScreenKey As String = vMenuCode 
    If vParentID > 0 Then 
      pScreenKey = vMenuCode & ":" & vParentID.ToString() 
    End If 
 
    'Check if screen is already open 
    Dim pExistingLocation As Object = FindOpenScreen(pScreenKey) 
    If pExistingLocation IsNot Nothing Then 
      If TypeOf pExistingLocation Is TabPage Then 
        tabMain.SelectedTab = CType(pExistingLocation, TabPage) 
      ElseIf TypeOf pExistingLocation Is Form Then 
        Dim pForm As Form = CType(pExistingLocation, Form) 
        pForm.BringToFront() 
        pForm.Focus() 
      End If 
      pFault.SetOK() 
      Return pFault 
    End If 
 
    'Get active tab panel (create one if none exists) 
    Dim pActivePanel As Panel = GetActiveTabPanel() 
    Dim pActiveContext As TabContext = GetActiveTabContext() 
    If pActivePanel Is Nothing OrElse pActiveContext Is Nothing Then 
      AddNewTab() 
      pActivePanel = GetActiveTabPanel() 
      pActiveContext = GetActiveTabContext() 
      If pActivePanel Is Nothing OrElse pActiveContext Is Nothing Then 
        Return pFault.LogFreeTextFault("No active tab found", "", "TRGT-TAB-001", _Requester) 
      End If 
    End If 
 
    'Get Control  
    If Not (vControlName.StartsWith("ctlPnlc_") OrElse vControlName.StartsWith("ctlc_")) Then 
      If vControlName.IndexOf("_") >= 0 Then 
        pControlType = vControlName.Split("_"c)(1).Trim 
        vControlName = vControlName.Split("_"c)(0) 
      End If 
    End If 
 
    If vParentID = -1 Then 
      If pActiveContext.NestedFormsCount > 0 Then 
        For i As Integer = 0 To pActiveContext.NestedFormsCount - 1 
          If pActivePanel.Controls.Count > 0 Then 
            pActivePanel.Controls.RemoveAt(0) 
          End If 
        Next 
        pActiveContext.NestedFormsCount = 0 
      End If 
      For Each pC As Control In pActivePanel.Controls 
        If pC.Name = vMenuCode Then 
          pC.Visible = False 
          pC.Visible = True 
          pC.BringToFront() 
          pFault.SetOK() 
          Return pFault 
        End If 
      Next 
    Else 
      pActiveContext.NestedFormsCount += 1 
    End If 
 
    Dim pAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()  
    Dim pControlName As String = (New StackFrame(0)).GetMethod().DeclaringType.Namespace & "." & vControlName 
    Dim pClassType As Type = pAssembly.GetType(pControlName) 
    Dim pControl As Control 
    pControl = CType(Activator.CreateInstance(pClassType), Control) 
 
    'Find LoadControl 
    Dim pLoad As Reflection.MethodInfo = pClassType.GetMethod("LoadControl") 
 
    pControl.Dock = DockStyle.Fill 
    pControl.Name = vMenuCode 
    'MakeControlRTL(pControl) 'do it in each control, to avoid possibly running it twice 
    pActivePanel.Controls.Add(pControl) 
    pActiveContext.ActiveMenuCode = pScreenKey 
    UpdateTabTitle(pActiveContext.TabPage, vMenuCode) 
 
    'Get Parameter to pass   
    Dim pParam() As Object 
    If pLoad.GetParameters.Length = 1 Then 
      ReDim pParam(0) 
      pParam(0) = _Requester 
    Else 
      ReDim pParam(1) 
      If pControlType <> "" Then 
        pParam(0) = pControlType 
      Else 
        pParam(0) = vParentID 
      End If 
      pParam(1) = _Requester 
    End If 
    'Load the control 
    Try 
      pFault = CType(pLoad.Invoke(pControl, pParam), clsFault) : If Not pFault.isOK Then Return pFault 
    Catch ex As Exception 
      Return pFault.LogException(ex, vControlName, "TRGT-130403-1628", _Requester) 
    End Try 
 
    'Get the event that transfers to the other panel 
    Try 
      Dim pEvent As Reflection.EventInfo = pClassType.GetEvent("evtEntityChosen") 
      If pEvent IsNot Nothing Then 
        Dim pDelegate As [Delegate] = [Delegate].CreateDelegate(pEvent.EventHandlerType, Me, "EntityChosenInPanel") 
        pEvent.AddEventHandler(pControl, pDelegate) 
      End If 
    Catch ex As Exception 
    End Try 
 
    'Get the event that sets the back button 
    Try 
      Dim pEvent As Reflection.EventInfo = pClassType.GetEvent("evtBackClicked") 
      If pEvent IsNot Nothing Then 
        Dim pDelegate As [Delegate] = [Delegate].CreateDelegate(pEvent.EventHandlerType, Me, "BackClickedInPanel") 
        pEvent.AddEventHandler(pControl, pDelegate) 
      End If 
    Catch ex As Exception 
    End Try 
 
    pControl.BringToFront() 
    pControl.Focus() 
 
    Return pFault 
  End Function 
 
  'Panel Entity Chosen 
  Private Sub EntityChosenInPanel(ByVal sender As Object, ByVal e As EntityEventArgs) 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ":" & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault 
    Dim pExists As Boolean = False  
    Dim pControlName As String = "" 
    If e.Object.GetType.Name = "String" Then 
      pControlName = e.Object.ToString 
      pExists = _ctlMenu.ActivateMenuItemByControlName(pControlName) 
    Else 
      pControlName = "ctlPnlcc" & e.Object.GetType.Name.Replace("cls", "") 
      pExists = _ctlMenu.ActivateMenuItemByControlName(pControlName) 
      If pExists = False Then 
        pControlName = "ctlPnlc_" & e.Object.GetType.Name.Replace("cs", "") 
        pExists = _ctlMenu.ActivateMenuItemByControlName(pControlName) 
      End If 
      If pExists = False Then 
        pControlName = e.Object.GetType.Name 
        pExists = _ctlMenu.ActivateMenuItemByControlName(pControlName) 
      End If 
    End If 
    pFault = LoadControl(pControlName, _ctlMenu.ActiveMenuItem?.Code, ccHelper.ToLong(e.UniqueCode)) 
    If pFault.isOK = False Then ShowFault(pFault, _Requester) 
  End Sub 
 
  'Set Splitter 
  Private Sub spcMain_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles spcMain.SplitterMoved 
    If Me.Visible = False Then Exit Sub 
    My.Settings.SplitterLocation = e.SplitX 
    My.Settings.Save() 
  End Sub 
 
  Private Sub btnMessages_Click(sender As Object, e As EventArgs) Handles btnMails.Click 
    'Show Mail 
     
    Cursor = Cursors.WaitCursor 
 
    Dim p As New EntityEventArgs 
    p.Object = "ctlPnlc_Mail" 
    EntityChosenInPanel(New Object, p) 
 
    Cursor = Cursors.Default 
  End Sub 
 
 
  Private _PreviousSMSCount As Integer = 0 
  Private _RecipentEmail As String 
  Private _RecipentSMS As String 
 
  Private Sub Timer_Tick(sender As Object, e As EventArgs) Handles Timer.Tick 
    If _Requester Is Nothing Then Return 
    _Requester.CallingFunctionWithinApplication = System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType.Name & ": " & System.Reflection.MethodInfo.GetCurrentMethod().Name 
    Dim pFault As clsFault = Nothing 
 
    Timer.Stop() 
 
    If String.IsNullOrEmpty(_RecipentEmail) Then 
      'get the user email & SMS 
      Dim pUser As New csUser(_Requester.UserID, clsEnums.enmLoadParent.DoNotLoad, _Requester, pFault, True) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
      _RecipentEmail = pUser.Email 
      pFault = ccHelper.CreateInternationalPhoneNumber(pUser.PhoneNumber, _RecipentSMS, _Requester) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
    End If 
 
    Dim pHowmany As Integer = 11 
    Dim pMails As New csMailCol 
    pFault = pMails.FillByMessagingModeAndRecipientEmailAndWasSeen(clsEnums.enmMessagingMode.Email, _RecipentEmail, False, vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
    Dim pEMailCount As Integer = pMails.Count 
    pMails = New csMailCol 
    pFault = pMails.FillByMessagingModeAndRecipientEmailAndWasSeen(clsEnums.enmMessagingMode.SMS, _RecipentSMS, False, vRequester:=_Requester, vHowMany:=pHowmany, vDir:=clsEnums.enmFillDirection.DESC) : If Not pFault.isOK Then ShowFault(pFault, _Requester) : Return 
    Dim pSMSCount As Integer = pMails.Count 
    If pSMSCount > _PreviousSMSCount Then 
      Console.Beep(1000, 100) 
      System.Media.SystemSounds.Hand.Play() 
    End If 
    _PreviousSMSCount = pSMSCount 
 
    Dim pText As String 
    If pSMSCount + pEMailCount = 0 Then 
      pText = "Mails" 
      btnMails.BackColor = Color.White 
      btnMails.ForeColor = Color.Black 
      btnMails.Font = New Font(btnMails.Font, FontStyle.Regular) 
    Else 
      pText = "" 
      Dim psEMailCount As String 
      Dim psSMSCount As String 
      If pEMailCount = 11 Then psEMailCount = "10+ Email" Else psEMailCount = pEMailCount.ToString() & " Email" 
      If pEMailCount > 1 Then psEMailCount &= "s" 
      If pSMSCount = 11 Then psSMSCount = "10+ SMS" Else psSMSCount = pSMSCount.ToString() & " SMS" 
      If pSMSCount > 1 Then psSMSCount &= "s" 
 
      If pSMSCount > 0 Then 
        pText = psSMSCount 
        btnMails.BackColor = Color.Red 
        btnMails.ForeColor = Color.White 
        btnMails.Font = New Font(btnMails.Font, FontStyle.Regular) 
        If pEMailCount > 0 Then 
          pText &= $" && {psEMailCount}" 
        End If 
      ElseIf pEMailCount > 0 Then 
        pText = psEMailCount 
        btnMails.BackColor = Color.MistyRose 
        btnMails.ForeColor = Color.Black 
        btnMails.Font = New Font(btnMails.Font, FontStyle.Regular) 
      End If 
    End If 
    btnMails.Text = pText 
    btnMails.TextAlign = ContentAlignment.MiddleLeft 
 
    If btnMails.Visible Then Timer.Start() 
 
  End Sub 
 
 
#Region "Tab Management" 
 
  Private Function GetActiveTabContext() As TabContext 
    If tabMain.SelectedTab Is Nothing Then Return Nothing 
    If _TabContexts.ContainsKey(tabMain.SelectedTab) Then 
      Return _TabContexts(tabMain.SelectedTab) 
    End If 
    Return Nothing 
  End Function 
 
  Private Function GetActiveTabPanel() As Panel 
    Dim ctx As TabContext = GetActiveTabContext() 
    If ctx IsNot Nothing Then 
      Return ctx.ContentPanel 
    End If 
    Return Nothing 
  End Function 
 
  Private _PlusTab As TabPage 
 
  Private Sub AddNewTab() 
    Dim pTabPage As New TabPage("New Tab") 
    Dim pContext As New TabContext(pTabPage) 
    _TabContexts.Add(pTabPage, pContext) 
    'Insert before the + tab 
    Dim pInsertIndex As Integer = tabMain.TabPages.Count 
    If _PlusTab IsNot Nothing AndAlso tabMain.TabPages.Contains(_PlusTab) Then 
      pInsertIndex = tabMain.TabPages.IndexOf(_PlusTab) 
    End If 
    tabMain.TabPages.Insert(pInsertIndex, pTabPage) 
    tabMain.SelectedTab = pTabPage 
    UpdateTabCloseButtons() 
  End Sub 
 
  Private Sub CloseTab(ByVal vTabPage As TabPage) 
    If vTabPage Is Nothing Then Exit Sub 
    If vTabPage Is _PlusTab Then Exit Sub 
    'Don't close if it's the only real tab 
    Dim pRealTabCount As Integer = tabMain.TabPages.Count 
    If _PlusTab IsNot Nothing AndAlso tabMain.TabPages.Contains(_PlusTab) Then pRealTabCount -= 1 
    If pRealTabCount <= 1 Then Exit Sub 
    If _TabContexts.ContainsKey(vTabPage) Then 
      _TabContexts.Remove(vTabPage) 
    End If 
    tabMain.TabPages.Remove(vTabPage) 
    vTabPage.Dispose() 
    UpdateTabCloseButtons() 
  End Sub 
 
  Private Sub UpdateTabCloseButtons() 
    'Count real tabs (excluding + tab) 
    Dim pRealTabCount As Integer = tabMain.TabPages.Count 
    If _PlusTab IsNot Nothing AndAlso tabMain.TabPages.Contains(_PlusTab) Then pRealTabCount -= 1 
    Dim pShowX As Boolean = (pRealTabCount > 1) 
    For Each pTabPage As TabPage In tabMain.TabPages 
      If pTabPage Is _PlusTab Then Continue For 
      Dim pText As String = pTabPage.Text 
      Dim pHasX As Boolean = pText.EndsWith(" ×") 
      If pShowX AndAlso Not pHasX Then 
        pTabPage.Text = pText & " ×" 
      ElseIf Not pShowX AndAlso pHasX Then 
        pTabPage.Text = pText.Substring(0, pText.Length - 2) 
      End If 
    Next 
  End Sub 
 
  Private Sub InitializeTabs() 
    tabMain.TabPages.Clear() 
    _TabContexts.Clear() 
    'Create the + tab (4 spaces before to center) 
    _PlusTab = New TabPage("    +") 
    _PlusTab.ToolTipText = "Add New Tab" 
    tabMain.TabPages.Add(_PlusTab) 
    'No initial empty tab - first tab opens when user clicks a menu item 
  End Sub 
 
  Private Sub UpdateTabTitle(ByVal vTabPage As TabPage, ByVal vMenuCode As String, Optional ByVal vDisplayText As String = "") 
    If vTabPage Is Nothing Then Exit Sub 
    Dim pTitle As String = "" 
    Dim pID As String = "" 
    Dim pTableName As String = vMenuCode 
    'Extract ID if present (format: MenuCode:ID) 
    If vMenuCode.Contains(":") Then 
      Dim pParts() As String = vMenuCode.Split(":"c) 
      pTableName = pParts(0) 
      pID = pParts(1) 
    End If 
    'Get last part after underscore (table name) 
    If pTableName.Contains("_") Then 
      pTableName = pTableName.Split("_"c).Last() 
    End If 
    'Use display text if provided, otherwise use table name with ID 
    If vDisplayText <> "" Then 
      Dim pDisplayName As String = vDisplayText 
      If pDisplayName.Contains("(") Then 
        pDisplayName = pDisplayName.Split("("c)(0).Trim() 
      End If 
      pTitle = pTableName & ": " & pDisplayName 
    ElseIf pID <> "" Then 
      pTitle = pTableName & " #" & pID 
    Else 
      pTitle = pTableName 
    End If 
    'Truncate if too long 
    If pTitle.Length > 22 Then pTitle = pTitle.Substring(0, 19) & "..." 
    vTabPage.Text = pTitle 
    UpdateTabCloseButtons() 
  End Sub 
 
  ''' <summary> 
  ''' Updates the ActiveMenuCode for the current tab with a specific entity ID. 
  ''' Call this from a control when a specific entity is selected/loaded. 
  ''' Returns False if entity is already open in another tab (and switches to it). 
  ''' </summary> 
  Public Function UpdateActiveMenuCodeWithID(ByVal vBaseMenuCode As String, ByVal vEntityID As Long, Optional ByVal vDisplayText As String = "") As Boolean 
    Dim pActiveContext As TabContext = GetActiveTabContext() 
    If pActiveContext Is Nothing Then Return False 
    Dim pScreenKey As String = vBaseMenuCode 
    If vEntityID > 0 Then 
      pScreenKey = vBaseMenuCode & ":" & vEntityID.ToString() 
      'Check if already open in another tab 
      Dim pExisting As Object = FindOpenScreenExcludingCurrent(pScreenKey, pActiveContext) 
      If pExisting IsNot Nothing Then 
        'Switch to existing tab/form 
        If TypeOf pExisting Is TabPage Then 
          tabMain.SelectedTab = CType(pExisting, TabPage) 
        ElseIf TypeOf pExisting Is Form Then 
          Dim pForm As Form = CType(pExisting, Form) 
          pForm.BringToFront() 
          pForm.Focus() 
        End If 
        Return False 
      End If 
    End If 
    'Only update if this is a new screen key 
    If pActiveContext.ActiveMenuCode <> pScreenKey Then 
      pActiveContext.ActiveMenuCode = pScreenKey 
      UpdateTabTitle(pActiveContext.TabPage, pActiveContext.ActiveMenuCode, vDisplayText) 
    End If 
    Return True 
  End Function 
 
  Private Function FindOpenScreenExcludingCurrent(ByVal vScreenKey As String, ByVal vExcludeContext As TabContext) As Object 
    'Search in all tabs for exact match, excluding current 
    For Each pKvp As KeyValuePair(Of TabPage, TabContext) In _TabContexts 
      Dim pCtx As TabContext = pKvp.Value 
      If pCtx Is vExcludeContext Then Continue For 
      If pCtx.ActiveMenuCode = vScreenKey Then 
        Return pKvp.Key 
      End If 
    Next 
    'Search in popout forms 
    For Each pForm As Form In _PopoutForms 
      If pForm.Tag IsNot Nothing AndAlso TypeOf pForm.Tag Is TabContext Then 
        Dim pCtx As TabContext = CType(pForm.Tag, TabContext) 
        If pCtx.ActiveMenuCode = vScreenKey Then 
          Return pForm 
        End If 
      End If 
    Next 
    Return Nothing 
  End Function 
 
  Private Function FindOpenScreen(ByVal vScreenKey As String) As Object 
    'Only block if we have a specific ID (format: MenuCode:ID) 
    'List screens (no ID) should always allow opening in new tab 
    If Not vScreenKey.Contains(":") Then 
      Return Nothing 
    End If 
    'Search in all tabs for exact match 
    For Each pKvp As KeyValuePair(Of TabPage, TabContext) In _TabContexts 
      Dim pCtx As TabContext = pKvp.Value 
      If pCtx.ActiveMenuCode = vScreenKey Then 
        Return pKvp.Key 
      End If 
    Next 
    'Search in popout forms for exact match 
    For Each pForm As Form In _PopoutForms 
      If pForm.Tag IsNot Nothing AndAlso TypeOf pForm.Tag Is TabContext Then 
        Dim pCtx As TabContext = CType(pForm.Tag, TabContext) 
        If pCtx.ActiveMenuCode = vScreenKey Then 
          Return pForm 
        End If 
      End If 
    Next 
    Return Nothing 
  End Function 
 
  Private Sub PopOutTab(ByVal vTabPage As TabPage) 
    If vTabPage Is Nothing Then Exit Sub 
    If vTabPage Is _PlusTab Then Exit Sub 
    If Not _TabContexts.ContainsKey(vTabPage) Then Exit Sub 
    Dim pContext As TabContext = _TabContexts(vTabPage) 
    Dim pForm As New Form() 
    'Remove × from title if present 
    Dim pTitle As String = vTabPage.Text 
    If pTitle.EndsWith(" ×") Then pTitle = pTitle.Substring(0, pTitle.Length - 2) 
    pForm.Text = pTitle 
    pForm.Size = New Size(800, 600) 
    pForm.StartPosition = FormStartPosition.CenterScreen 
    pForm.Tag = pContext 
    'Create visible Return to Tab bar at top of popout form 
    Dim pTopPanel As New Panel() 
    pTopPanel.Dock = DockStyle.Top 
    pTopPanel.Height = 28 
    pTopPanel.BackColor = Color.FromArgb(70, 130, 180) 
    pTopPanel.Cursor = Cursors.Hand 
    Dim pReturnLabel As New Label() 
    pReturnLabel.Text = " ↩ Return to Tab" 
    pReturnLabel.ForeColor = Color.White 
    pReturnLabel.Font = New Font("Segoe UI", 9, FontStyle.Bold) 
    pReturnLabel.Dock = DockStyle.Fill 
    pReturnLabel.TextAlign = ContentAlignment.MiddleLeft 
    pReturnLabel.Cursor = Cursors.Hand 
    pTopPanel.Controls.Add(pReturnLabel) 
    AddHandler pTopPanel.Click, Sub(s, ev) ReturnPopoutToTab(pForm) 
    AddHandler pReturnLabel.Click, Sub(s, ev) ReturnPopoutToTab(pForm) 
    'Create content panel for popout 
    Dim pContentPanel As New Panel() 
    pContentPanel.Dock = DockStyle.Fill 
    While pContext.ContentPanel.Controls.Count > 0 
      Dim pCtrl As Control = pContext.ContentPanel.Controls(0) 
      pContext.ContentPanel.Controls.Remove(pCtrl) 
      pContentPanel.Controls.Add(pCtrl) 
    End While 
    'Add content first, then top panel (so Dock.Fill works correctly) 
    pForm.Controls.Add(pContentPanel) 
    pForm.Controls.Add(pTopPanel) 
    _TabContexts.Remove(vTabPage) 
    tabMain.TabPages.Remove(vTabPage) 
    vTabPage.Dispose() 
    _PopoutForms.Add(pForm) 
    AddHandler pForm.FormClosed, Sub(s, ev) _PopoutForms.Remove(pForm) 
    pForm.Show() 
    UpdateTabCloseButtons() 
    If tabMain.TabPages.Count = 1 AndAlso tabMain.TabPages(0) Is _PlusTab Then AddNewTab() 
  End Sub 
 
  Private Sub ReturnPopoutToTab(ByVal vForm As Form) 
    If vForm Is Nothing Then Exit Sub 
    If vForm.Tag Is Nothing OrElse Not TypeOf vForm.Tag Is TabContext Then Exit Sub 
    Dim pOldContext As TabContext = CType(vForm.Tag, TabContext) 
    Dim pNewTabPage As New TabPage(vForm.Text) 
    Dim pNewContext As New TabContext(pNewTabPage) 
    pNewContext.ActiveMenuCode = pOldContext.ActiveMenuCode 
    pNewContext.NestedFormsCount = pOldContext.NestedFormsCount 
    'Find the content panel (the one with Dock=Fill, not the top panel) 
    Dim pPopoutContent As Panel = Nothing 
    For Each pCtrl As Control In vForm.Controls 
      If TypeOf pCtrl Is Panel AndAlso pCtrl.Dock = DockStyle.Fill Then 
        pPopoutContent = CType(pCtrl, Panel) 
        Exit For 
      End If 
    Next 
    If pPopoutContent IsNot Nothing Then 
      While pPopoutContent.Controls.Count > 0 
        Dim pCtrl As Control = pPopoutContent.Controls(0) 
        pPopoutContent.Controls.Remove(pCtrl) 
        pNewContext.ContentPanel.Controls.Add(pCtrl) 
      End While 
    End If 
    _TabContexts.Add(pNewTabPage, pNewContext) 
    'Insert before the + tab 
    Dim pInsertIndex As Integer = tabMain.TabPages.Count 
    If _PlusTab IsNot Nothing AndAlso tabMain.TabPages.Contains(_PlusTab) Then 
      pInsertIndex = tabMain.TabPages.IndexOf(_PlusTab) 
    End If 
    tabMain.TabPages.Insert(pInsertIndex, pNewTabPage) 
    tabMain.SelectedTab = pNewTabPage 
    vForm.Tag = Nothing 
    vForm.Close() 
    UpdateTabCloseButtons() 
  End Sub 
 
  'Open an entity detail control in a new tab (called from collection context menu) 
  Public Function OpenEntityInNewTab(ByVal vControlName As String, ByVal vEntity As Object, ByVal vRequester As clsRequester, ByVal vTabTitle As String) As clsFault 
    Dim pFault As New clsFault 
    'Check if entity already open in a tab - if so, switch to it 
    For Each pCtx As KeyValuePair(Of TabPage, TabContext) In _TabContexts 
      If pCtx.Value.ActiveMenuCode = vTabTitle Then 
        tabMain.SelectedTab = pCtx.Key 
        Return pFault 'Already open - return OK 
      End If 
    Next 
    AddNewTab() 
    Dim pActivePanel As Panel = GetActiveTabPanel() 
    Dim pActiveContext As TabContext = GetActiveTabContext() 
    If pActivePanel Is Nothing OrElse pActiveContext Is Nothing Then 
      Return pFault.LogFreeTextFault("No active tab found", "", "TRGT-TAB-002", vRequester) 
    End If 
    Dim pAssembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly() 
    Dim pNamespace As String = Me.GetType().Namespace 
    Dim pClassType As Type = pAssembly.GetType(pNamespace & "." & vControlName) 
    If pClassType Is Nothing Then 
      Return pFault.LogFreeTextFault("Control not found: " & vControlName, "", "TRGT-TAB-003", vRequester) 
    End If 
    Dim pControl As Control = CType(Activator.CreateInstance(pClassType), Control) 
    pControl.Dock = DockStyle.Fill 
    pControl.Name = vTabTitle 
    pActivePanel.Controls.Add(pControl) 
    pActiveContext.ActiveMenuCode = vTabTitle 
    UpdateTabTitle(pActiveContext.TabPage, vTabTitle) 
    'Use LoadControlForPopup(entity As Object, requester) - all detail controls have this method 
    Dim pLoad As System.Reflection.MethodInfo = pClassType.GetMethod("LoadControlForPopup") 
    If pLoad Is Nothing Then 
      Return pFault.LogFreeTextFault("LoadControlForPopup not found on " & vControlName, "", "TRGT-TAB-004", vRequester) 
    End If 
    Dim pParams() As Object 
    ReDim pParams(1) 
    pParams(0) = vEntity 
    pParams(1) = vRequester 
    pFault = CType(pLoad.Invoke(pControl, pParams), clsFault) 
    Try 
      Dim pEvent As System.Reflection.EventInfo = pClassType.GetEvent("evtEntityChosen") 
      If pEvent IsNot Nothing Then 
        Dim pDelegate As [Delegate] = [Delegate].CreateDelegate(pEvent.EventHandlerType, Me, "EntityChosenInPanel") 
        pEvent.AddEventHandler(pControl, pDelegate) 
      End If 
    Catch ex As Exception 
    End Try 
    Try 
      Dim pEvent As System.Reflection.EventInfo = pClassType.GetEvent("evtBackClicked") 
      If pEvent IsNot Nothing Then 
        Dim pDelegate As [Delegate] = [Delegate].CreateDelegate(pEvent.EventHandlerType, Me, "BackClickedInPanel") 
        pEvent.AddEventHandler(pControl, pDelegate) 
      End If 
    Catch ex As Exception 
    End Try 
    pControl.BringToFront() 
    Return pFault 
  End Function 
 
  'Check if entity is already open in any tab and switch to it if found 
  Public Function IsEntityOpenInTab(ByVal vTabTitle As String) As Boolean 
    For Each pCtx As KeyValuePair(Of TabPage, TabContext) In _TabContexts 
      If pCtx.Value.ActiveMenuCode = vTabTitle Then 
        tabMain.SelectedTab = pCtx.Key 
        Return True 
      End If 
    Next 
    Return False 
  End Function 
 
  Private Sub tabMain_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabMain.SelectedIndexChanged 
    'If + tab is clicked, add a new tab 
    If tabMain.SelectedTab Is _PlusTab Then 
      AddNewTab() 
    End If 
  End Sub 
 
  Private Sub tabMain_MouseUp(sender As Object, e As MouseEventArgs) Handles tabMain.MouseUp 
    'Right click on tab header only - show context menu 
    If e.Button = MouseButtons.Right Then 
      For i As Integer = 0 To tabMain.TabPages.Count - 1 
        If tabMain.GetTabRect(i).Contains(e.Location) Then 
          _RightClickedTabIndex = i 
          cmsTabMenu.Show(tabMain, e.Location) 
          Exit For 
        End If 
      Next 
    End If 
  End Sub 
 
  Private Sub tabMain_MouseClick(sender As Object, e As MouseEventArgs) Handles tabMain.MouseClick 
    For i As Integer = 0 To tabMain.TabPages.Count - 1 
      Dim pTabRect As Rectangle = tabMain.GetTabRect(i) 
      If pTabRect.Contains(e.Location) Then 
        Dim pTabPage As TabPage = tabMain.TabPages(i) 
        'Skip + tab 
        If pTabPage Is _PlusTab Then Exit Sub 
        'Left click on × (right 25 pixels of tab) to close 
        If e.Button = MouseButtons.Left Then 
          If e.X >= pTabRect.Right - 25 Then 
            CloseTab(pTabPage) 
            Exit Sub 
          End If 
        End If 
        'Middle click anywhere on tab to close 
        If e.Button = MouseButtons.Middle Then 
          CloseTab(pTabPage) 
          Exit Sub 
        End If 
        Exit For 
      End If 
    Next 
  End Sub 
 
  Private Sub cmsTabMenu_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles cmsTabMenu.Opening 
    'Only show Return to Tab if there are popout forms 
    tsmiReturnToTab.Visible = (_PopoutForms.Count > 0) 
  End Sub 
 
  Private Sub tsmiPopOut_Click(sender As Object, e As EventArgs) Handles tsmiPopOut.Click 
    If _RightClickedTabIndex < 0 OrElse _RightClickedTabIndex >= tabMain.TabPages.Count Then Return 
    PopOutTab(tabMain.TabPages(_RightClickedTabIndex)) 
  End Sub 
 
  Private Sub tsmiReturnToTab_Click(sender As Object, e As EventArgs) Handles tsmiReturnToTab.Click 
    'Return the first popout form to a tab 
    If _PopoutForms.Count = 0 Then Return 
    ReturnPopoutToTab(_PopoutForms(0)) 
  End Sub 
 
  Private Sub tsmiCloseTab_Click(sender As Object, e As EventArgs) Handles tsmiCloseTab.Click 
    If _RightClickedTabIndex < 0 OrElse _RightClickedTabIndex >= tabMain.TabPages.Count Then Return 
    CloseTab(tabMain.TabPages(_RightClickedTabIndex)) 
  End Sub 
 
#End Region 
 
End Class 
