Partial Public Class ctlc_MFACol
 
  'for dependant fields (like 'Type' and 'ID in Type' in the User Table), copy code from 
  '  the ctlc_UserCol.vb file. Look for 'For ID in XXX' 
 
  Private Sub ctlc_MFACol_evtBeforeLoad() Handles Me.evtBeforeLoad 
    'Sample 
    '_LoadParameters.SummarizeGrid = False 
    '_LoadParameters.ReadOnly = False 
    'colMyColumn.ReadOnly = True 'for specific columns that you want to be read-only 
 
    'Hide columns 
    '_LoadParameters.ColumnsHide.Add(csMFA.enmProperty.ID) 
 
    'Change Header Texts as (if) needed 
    '_LoadParameters.ColumnsHeaderText.Add(csMFA.enmProperty.MainText, "The Main Text") 
 
    'AutoRefresh make visible and change interval (default is 30 sec) 
    'chkAutoRefresh.Visible = True 
    'AutoRefresh make visible and change interval (default is 30 sec) 
 
    'Set cache to AlwaysCache if needed (can also set it application-wide in 'LoadInitialCache' in modWinF) 
    'MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.AlwaysCache) 
 
  End Sub 
 
  'Private _isFirstLoad As Boolean = True 
  'Private Sub ctlc_MFACol_evtLoaded() Handles Me.evtLoaded 
  '  'Sample initial sort. This sample sorts on Columns 1st, 2nd then 3rd. Use XButton1 as a fake flag, so I know it came from here 
  '  If _isFirstLoad Then 
  '    Dim pE As New System.Windows.Forms.DataGridViewCellMouseEventArgs(col3rd.Index, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton1, 0, 0, 0, 0)) 
  '    dgvMFA_ColumnHeaderMouseClick(Me, pE) 
  '    pE = New System.Windows.Forms.DataGridViewCellMouseEventArgs(col2nd.Index, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton1, 0, 0, 0, 0)) 
  '    dgvMFA_ColumnHeaderMouseClick(Me, pE) 
  '    pE = New System.Windows.Forms.DataGridViewCellMouseEventArgs(col1st.Index, 0, 0, 0, New System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.XButton1, 0, 0, 0, 0)) 
  '    dgvMFA_ColumnHeaderMouseClick(Me, pE) 
  '    _isFirstLoad = False 
  '  End If 
  'End Sub 
End Class
