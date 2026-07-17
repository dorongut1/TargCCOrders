Partial Public Class frmMain 
  Private Sub frmMain_evtBeforeMenuLoad(ByRef rMenu As clsMenu) Handles Me.evtBeforeMenuLoad 
    Me.Icon = New Icon(My.Computer.FileSystem.CurrentDirectory & "\" & "TargetIcon32.ico") 
    LoadPrtMenu(rMenu) 
    btnMails.Parent.Controls.Remove(btnMails) 
    btnMails.Visible = False 
  End Sub 
  Private Sub LoadPrtMenu(ByRef rMenu As clsMenu) 
    If rMenu.FindByCode("Line01") IsNot Nothing Then rMenu.Remove("Line01") 'If you change the menu instead of recreating it, take advantage of the separator I added 
    'Build or adjust menu here 
    'rMenu.Clear() 
 
    'here's a sample 
    'Dim pOrd2 As Integer 
    'Dim pLevel1Code As String 
    ''LoadLevels  
    'pLevel1Code = "Task" 
    ''change the text if needed 
    'rMenu.FindByCode("Task").Text_L1 = UITranslate("Main", _Requester) 
    'pOrd2 = 1 
    ''now handle the children 
    'rMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Person", "ctlPnlccPerson", True, TableNameTranslate("Person", _Requester)) : pOrd2 += 1 
    'rMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_LoanRequester", "ctlPnlccLoanRequester", True, TableNameTranslate("LoanRequester", _Requester)) : pOrd2 += 1 
    'rMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_LoanOffer", "ctlPnlccLoanOffer", True, TableNameTranslate("LoanOffer", _Requester)) : pOrd2 += 1 
    'rMenu.Add(2, pLevel1Code, pOrd2, pLevel1Code & "_Loan", "ctlPnlccLoan", True, TableNameTranslate("Loan", _Requester)) : pOrd2 += 1 
 
  End Sub 
End Class 
