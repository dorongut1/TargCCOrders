Partial Public Module modWinF 
 
  ''' <summary>  
  ''' If you want to 'PageFromServer", i.e. you don't want to load all the items to the combolist, and want to query the  
  '''   database to get the handful of items at a time, then set the level in MyCache to PageFromServer.   
  ''' If you want it to load all the items to the combolist, set it to AlwaysCache  
  ''' If you don't set it, then it will default to Auto (PageFromServer if over 100 items it, otherwise AlwaysCache) 
  ''' </summary>  
  Private Sub LoadInitialCacheManualAdditions() 
    MyCache.SetLevel(clsEnums.enmComboListType.c_UserDefaultByID, Cache.enmLevel.AlwaysPageFromServer) 
    'MyCache.SetLevel(clsEnums.enmComboListType.c_AlertMessageDefaultByID, Cache.enmLevel.AlwaysCache) 
    'TargCC: Add any combolists you want to be cached or have their cache method set before being 1st called, here. 
  End Sub 
 
End Module 
