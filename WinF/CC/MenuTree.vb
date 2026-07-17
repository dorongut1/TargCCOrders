Public Class MenuTree

  Private _SleepTime As Integer = 1

  Private _Menu As clsMenu

  Private _ActiveMenuItem As clsMenu.clsMenuItem

  Public Enum enmSplitterStatus
    UD
    Open
    Closed
    Pinned
  End Enum
  Private _SplitterStatus As enmSplitterStatus
  Public Property SplitterStatus() As enmSplitterStatus
    Get
      Return _SplitterStatus
    End Get
    Set(ByVal value As enmSplitterStatus)
      _SplitterStatus = value
    End Set
  End Property

  Public Event evtLinkClicked(ByVal vMenuItem As clsMenu.clsMenuItem)
  Public Event evtHelpClicked()
  Public Event evtMadeVisible()

  'Properties
  Private _ColourBack As Drawing.Color
  Private _ColourHover As Drawing.Color
  Private _ColourChosenBack As Drawing.Color
  Private _ColourChosenFore As Drawing.Color
  Private _ColourDefaultFore As Drawing.Color = Drawing.Color.Black

  'Properties
  Public WriteOnly Property ColourBack() As Drawing.Color
    Set(ByVal value As Drawing.Color)
      _ColourBack = value
    End Set
  End Property
  Public WriteOnly Property ColourHover() As Drawing.Color
    Set(ByVal value As Drawing.Color)
      _ColourHover = value
    End Set
  End Property
  Public WriteOnly Property ColourChosenBack() As Drawing.Color
    Set(ByVal value As Drawing.Color)
      _ColourChosenBack = value
    End Set
  End Property
  Public WriteOnly Property ColourChosenFore() As Drawing.Color
    Set(ByVal value As Drawing.Color)
      _ColourChosenFore = value
    End Set
  End Property
  Public WriteOnly Property ColourDefaultFore() As Drawing.Color
    Set(ByVal value As Drawing.Color)
      _ColourDefaultFore = value
    End Set
  End Property

  Public ReadOnly Property ActiveMenuItem() As clsMenu.clsMenuItem
    Get
      Return _ActiveMenuItem
    End Get
  End Property

  'Public Subs
  Public Sub SetSplitterStatus(ByVal vStatus As enmSplitterStatus)
    If vStatus = enmSplitterStatus.Pinned Then
      lblSplitter.Text = "6"
      _SplitterStatus = vStatus
    ElseIf vStatus = enmSplitterStatus.Open Then
      lblSplitter.Text = "3"
      _SplitterStatus = vStatus
    ElseIf vStatus = enmSplitterStatus.Closed Then
      If _SplitterStatus = enmSplitterStatus.Open Then
        _SplitterStatus = enmSplitterStatus.Closed
      End If
    End If
  End Sub

  Public Function ActivateMenuItemByControlName(ByVal vControlName As String) As Boolean
    For Each pItem As clsMenu.clsMenuItem In _Menu
      If pItem.ControlName = vControlName Then
        Return ActivateMenuItem(pItem)
      End If
    Next
    Return False
  End Function
  Public Function ActivateMenuItem(ByVal vMenuItemCode As String) As Boolean
    For Each pItem As clsMenu.clsMenuItem In _Menu
      If pItem.Code = vMenuItemCode Then
        Return ActivateMenuItem(pItem)
      End If
    Next
    Return False
  End Function
  Public Function ActivateMenuItem(ByVal vMenuItem As clsMenu.clsMenuItem) As Boolean
    For Each pItem As clsMenu.clsMenuItem In _Menu
      If pItem.Code = vMenuItem.Code Then
        'Find the Parent Ordinate
        Dim pLevel01Ordinate As Integer = _Menu.FindByCode(pItem.ParentCode).OrdinalPosition
        Dim pLevel02Ordinate As Integer = pItem.OrdinalPosition
        If _ActiveMenuItem IsNot Nothing Then
          If pItem.ParentCode = _ActiveMenuItem.ParentCode Then
            MakeAllLinksBlack()
          Else
            HideSecondaryLinks()
            'show them 
            For iCntr = 1 To 29
              Dim pLnkVis As Label = GetLink(pLevel01Ordinate, iCntr)
              If pLnkVis.Tag IsNot Nothing Then pLnkVis.Visible = True : Application.DoEvents() : Threading.Thread.Sleep(_SleepTime)
            Next
          End If
        End If
        Dim pLnk As Label = GetLink(pLevel01Ordinate, pLevel02Ordinate)
        ActivateLink(pLnk)
        _ActiveMenuItem = CType(pLnk.Tag, clsMenu.clsMenuItem)
        Return True
      End If
    Next
    Return False
  End Function
  Public Sub EnableMenuItem(ByVal vMenuItem As clsMenu.clsMenuItem, ByVal vEnable As Boolean)
    For Each pItem As clsMenu.clsMenuItem In _Menu
      If pItem.Code = vMenuItem.Code Then
        'Find the Parent Ordinate
        Dim pLevel01Ordinate As Integer = _Menu.FindByCode(pItem.ParentCode).OrdinalPosition
        Dim pLevel02Ordinate As Integer = pItem.OrdinalPosition
        Dim pLnk As Label = GetLink(pLevel01Ordinate, pLevel02Ordinate)
        pLnk.Enabled = vEnable
        Exit Sub
      End If
    Next
    Throw New Exception("Link not found for MenuCode " & vMenuItem.Code)
  End Sub
  Public Sub EnableMenuItem(ByVal vMenuCode As String, ByVal vEnable As Boolean)
    For Each pItem As clsMenu.clsMenuItem In _Menu
      If pItem.Code = vMenuCode Then
        'Find the Parent Ordinate
        Dim pLevel01Ordinate As Integer = 0
        Dim pLevel02Ordinate As Integer = 0
        If pItem.Level = 1 Then
          pLevel01Ordinate = pItem.OrdinalPosition
          pLevel02Ordinate = 0
        Else
          pLevel01Ordinate = _Menu.FindByCode(pItem.ParentCode).OrdinalPosition
          pLevel02Ordinate = pItem.OrdinalPosition
        End If
        Dim pLnk As Label = GetLink(pLevel01Ordinate, pLevel02Ordinate)
        pLnk.Enabled = vEnable
        Exit Sub
      End If
    Next
    Throw New Exception("Link not found for MenuCode " & vMenuCode)
  End Sub
  'Loads menu
  Public Sub LoadControl(ByVal vMenu As clsMenu)

    Dim pStart As Date = Now

    ResetLinkBorders()

    _Menu = vMenu

    'Load the menu
    Dim pMenuItemsLevel01 As clsMenu = _Menu.CloneByLevelAndParentCode(1, "")
    For Each pMenuItem01 As clsMenu.clsMenuItem In pMenuItemsLevel01
      'Load the primaries
      Dim pLnk As Label = GetLink(pMenuItem01.OrdinalPosition, 0)
      If pLnk Is Nothing Then Throw New Exception("Too many Level 1 items in menu!")
      pLnk.Text = pMenuItem01.Text_L1
      pLnk.Tag = pMenuItem01
      pLnk.Enabled = pMenuItem01.Enabled
      Dim pMenuItemsLevel02 As clsMenu = _Menu.CloneByLevelAndParentCode(2, pMenuItem01.Code)
      For Each pMenuItem02 As clsMenu.clsMenuItem In pMenuItemsLevel02
        'Load the secondaries
        If pMenuItem02.OrdinalPosition = 30 Then
          Dim pMessage As String = $"There are too many Level 2 items in menu for '{pMenuItem02.ParentCode}' ({pMenuItem02.OrdinalPosition - 1} max) !"
          frmMessageOrInputBox.ShowMsg(pMessage, frmMessageOrInputBox.enmIconType.CriticalError)
          Exit For
        End If
        pLnk = GetLink(pMenuItem01.OrdinalPosition, pMenuItem02.OrdinalPosition)
        pLnk.Text = pMenuItem02.Text_L1
        pLnk.Tag = pMenuItem02
        pLnk.Enabled = pMenuItem02.Enabled
      Next
    Next

    For i = 1 To 15
      Dim pLnk As Label = GetLink(i, 0)
      If pLnk.Tag Is Nothing Then pLnk.Visible = False
    Next


  End Sub

  'Handles tree
  Private Sub MenuTree_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    If Me.DesignMode = True Then Exit Sub
    MyFont = New Font("Segoe UI", My.Settings.FontSize, FontStyle.Regular)
    Me.Font = MyFont
    Me.PerformAutoScale()

    'Apply menu background and foreground to FlowLayoutPanel and labels
    Me.BackColor = _ColourBack
    flpMenu.BackColor = _ColourBack
    Me.ForeColor = _ColourDefaultFore
    flpMenu.ForeColor = _ColourDefaultFore
    pnlTop.BackColor = _ColourBack
    lblSplitter.ForeColor = _ColourDefaultFore
    lblHelp.ForeColor = _ColourDefaultFore

  End Sub

  Private _LastRootLinkClicked As Label

  'Handles clicks
  Private Sub lnk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles _
        lnk0100.Click, lnk0101.Click, lnk0102.Click, lnk0103.Click, lnk0104.Click, lnk0105.Click, lnk0106.Click, lnk0107.Click, lnk0108.Click, lnk0109.Click, lnk0110.Click, lnk0111.Click, lnk0112.Click, lnk0113.Click, lnk0114.Click, lnk0115.Click, lnk0116.Click, lnk0117.Click, lnk0118.Click, lnk0119.Click, lnk0120.Click, lnk0121.Click, lnk0122.Click, lnk0123.Click, lnk0124.Click, lnk0125.Click, lnk0126.Click, lnk0127.Click, lnk0128.Click, lnk0129.Click,
        lnk0200.Click, lnk0201.Click, lnk0202.Click, lnk0203.Click, lnk0204.Click, lnk0205.Click, lnk0206.Click, lnk0207.Click, lnk0208.Click, lnk0209.Click, lnk0210.Click, lnk0211.Click, lnk0212.Click, lnk0213.Click, lnk0214.Click, lnk0215.Click, lnk0216.Click, lnk0217.Click, lnk0218.Click, lnk0219.Click, lnk0220.Click, lnk0221.Click, lnk0222.Click, lnk0223.Click, lnk0224.Click, lnk0225.Click, lnk0226.Click, lnk0227.Click, lnk0228.Click, lnk0229.Click,
        lnk0300.Click, lnk0301.Click, lnk0302.Click, lnk0303.Click, lnk0304.Click, lnk0305.Click, lnk0306.Click, lnk0307.Click, lnk0308.Click, lnk0309.Click, lnk0310.Click, lnk0311.Click, lnk0312.Click, lnk0313.Click, lnk0314.Click, lnk0315.Click, lnk0316.Click, lnk0317.Click, lnk0318.Click, lnk0319.Click, lnk0320.Click, lnk0321.Click, lnk0322.Click, lnk0323.Click, lnk0324.Click, lnk0325.Click, lnk0326.Click, lnk0327.Click, lnk0328.Click, lnk0329.Click,
        lnk0400.Click, lnk0401.Click, lnk0402.Click, lnk0403.Click, lnk0404.Click, lnk0405.Click, lnk0406.Click, lnk0407.Click, lnk0408.Click, lnk0409.Click, lnk0410.Click, lnk0411.Click, lnk0412.Click, lnk0413.Click, lnk0414.Click, lnk0415.Click, lnk0416.Click, lnk0417.Click, lnk0418.Click, lnk0419.Click, lnk0420.Click, lnk0421.Click, lnk0422.Click, lnk0423.Click, lnk0424.Click, lnk0425.Click, lnk0426.Click, lnk0427.Click, lnk0428.Click, lnk0429.Click,
        lnk0500.Click, lnk0501.Click, lnk0502.Click, lnk0503.Click, lnk0504.Click, lnk0505.Click, lnk0506.Click, lnk0507.Click, lnk0508.Click, lnk0509.Click, lnk0510.Click, lnk0511.Click, lnk0512.Click, lnk0513.Click, lnk0514.Click, lnk0515.Click, lnk0516.Click, lnk0517.Click, lnk0518.Click, lnk0519.Click, lnk0520.Click, lnk0521.Click, lnk0522.Click, lnk0523.Click, lnk0524.Click, lnk0525.Click, lnk0526.Click, lnk0527.Click, lnk0528.Click, lnk0529.Click,
        lnk0600.Click, lnk0601.Click, lnk0602.Click, lnk0603.Click, lnk0604.Click, lnk0605.Click, lnk0606.Click, lnk0607.Click, lnk0608.Click, lnk0609.Click, lnk0610.Click, lnk0611.Click, lnk0612.Click, lnk0613.Click, lnk0614.Click, lnk0615.Click, lnk0616.Click, lnk0617.Click, lnk0618.Click, lnk0619.Click, lnk0620.Click, lnk0621.Click, lnk0622.Click, lnk0623.Click, lnk0624.Click, lnk0625.Click, lnk0626.Click, lnk0627.Click, lnk0628.Click, lnk0629.Click,
        lnk0700.Click, lnk0701.Click, lnk0702.Click, lnk0703.Click, lnk0704.Click, lnk0705.Click, lnk0706.Click, lnk0707.Click, lnk0708.Click, lnk0709.Click, lnk0710.Click, lnk0711.Click, lnk0712.Click, lnk0713.Click, lnk0714.Click, lnk0715.Click, lnk0716.Click, lnk0717.Click, lnk0718.Click, lnk0719.Click, lnk0720.Click, lnk0721.Click, lnk0722.Click, lnk0723.Click, lnk0724.Click, lnk0725.Click, lnk0726.Click, lnk0727.Click, lnk0728.Click, lnk0729.Click,
        lnk0800.Click, lnk0801.Click, lnk0802.Click, lnk0803.Click, lnk0804.Click, lnk0805.Click, lnk0806.Click, lnk0807.Click, lnk0808.Click, lnk0809.Click, lnk0810.Click, lnk0811.Click, lnk0812.Click, lnk0813.Click, lnk0814.Click, lnk0815.Click, lnk0816.Click, lnk0817.Click, lnk0818.Click, lnk0819.Click, lnk0820.Click, lnk0821.Click, lnk0822.Click, lnk0823.Click, lnk0824.Click, lnk0825.Click, lnk0826.Click, lnk0827.Click, lnk0828.Click, lnk0829.Click,
        lnk0900.Click, lnk0901.Click, lnk0902.Click, lnk0903.Click, lnk0904.Click, lnk0905.Click, lnk0906.Click, lnk0907.Click, lnk0908.Click, lnk0909.Click, lnk0910.Click, lnk0911.Click, lnk0912.Click, lnk0913.Click, lnk0914.Click, lnk0915.Click, lnk0916.Click, lnk0917.Click, lnk0918.Click, lnk0919.Click, lnk0920.Click, lnk0921.Click, lnk0922.Click, lnk0923.Click, lnk0924.Click, lnk0925.Click, lnk0926.Click, lnk0927.Click, lnk0928.Click, lnk0929.Click,
        lnk1000.Click, lnk1001.Click, lnk1002.Click, lnk1003.Click, lnk1004.Click, lnk1005.Click, lnk1006.Click, lnk1007.Click, lnk1008.Click, lnk1009.Click, lnk1010.Click, lnk1011.Click, lnk1012.Click, lnk1013.Click, lnk1014.Click, lnk1015.Click, lnk1016.Click, lnk1017.Click, lnk1018.Click, lnk1019.Click, lnk1020.Click, lnk1021.Click, lnk1022.Click, lnk1023.Click, lnk1024.Click, lnk1025.Click, lnk1026.Click, lnk1027.Click, lnk1028.Click, lnk1029.Click,
        lnk1100.Click, lnk1101.Click, lnk1102.Click, lnk1103.Click, lnk1104.Click, lnk1105.Click, lnk1106.Click, lnk1107.Click, lnk1108.Click, lnk1109.Click, lnk1110.Click, lnk1111.Click, lnk1112.Click, lnk1113.Click, lnk1114.Click, lnk1115.Click, lnk1116.Click, lnk1117.Click, lnk1118.Click, lnk1119.Click, lnk1120.Click, lnk1121.Click, lnk1122.Click, lnk1123.Click, lnk1124.Click, lnk1125.Click, lnk1126.Click, lnk1127.Click, lnk1128.Click, lnk1129.Click,
        lnk1200.Click, lnk1201.Click, lnk1202.Click, lnk1203.Click, lnk1204.Click, lnk1205.Click, lnk1206.Click, lnk1207.Click, lnk1208.Click, lnk1209.Click, lnk1210.Click, lnk1211.Click, lnk1212.Click, lnk1213.Click, lnk1214.Click, lnk1215.Click, lnk1216.Click, lnk1217.Click, lnk1218.Click, lnk1219.Click, lnk1220.Click, lnk1221.Click, lnk1222.Click, lnk1223.Click, lnk1224.Click, lnk1225.Click, lnk1226.Click, lnk1227.Click, lnk1228.Click, lnk1229.Click,
        lnk1300.Click, lnk1301.Click, lnk1302.Click, lnk1303.Click, lnk1304.Click, lnk1305.Click, lnk1306.Click, lnk1307.Click, lnk1308.Click, lnk1309.Click, lnk1310.Click, lnk1311.Click, lnk1312.Click, lnk1313.Click, lnk1314.Click, lnk1315.Click, lnk1316.Click, lnk1317.Click, lnk1318.Click, lnk1319.Click, lnk1320.Click, lnk1321.Click, lnk1322.Click, lnk1323.Click, lnk1324.Click, lnk1325.Click, lnk1326.Click, lnk1327.Click, lnk1328.Click, lnk1329.Click,
        lnk1400.Click, lnk1401.Click, lnk1402.Click, lnk1403.Click, lnk1404.Click, lnk1405.Click, lnk1406.Click, lnk1407.Click, lnk1408.Click, lnk1409.Click, lnk1410.Click, lnk1411.Click, lnk1412.Click, lnk1413.Click, lnk1414.Click, lnk1415.Click, lnk1416.Click, lnk1417.Click, lnk1418.Click, lnk1419.Click, lnk1420.Click, lnk1421.Click, lnk1422.Click, lnk1423.Click, lnk1424.Click, lnk1425.Click, lnk1426.Click, lnk1427.Click, lnk1428.Click, lnk1429.Click,
        lnk1500.Click, lnk1501.Click, lnk1502.Click, lnk1503.Click, lnk1504.Click, lnk1505.Click, lnk1506.Click, lnk1507.Click, lnk1508.Click, lnk1509.Click, lnk1510.Click, lnk1511.Click, lnk1512.Click, lnk1513.Click, lnk1514.Click, lnk1515.Click, lnk1516.Click, lnk1517.Click, lnk1518.Click, lnk1519.Click, lnk1520.Click, lnk1521.Click, lnk1522.Click, lnk1523.Click, lnk1524.Click, lnk1525.Click, lnk1526.Click, lnk1527.Click, lnk1528.Click, lnk1529.Click

    _ActiveMenuItem = Nothing

    Dim plnkChosen As Label = CType(sender, Label)

    plnkChosen.Cursor = Cursors.WaitCursor : Application.DoEvents()

    If GetLevel02Ordinate(plnkChosen) = 0 Then
      'Hide them 
      HideSecondaryLinks()

      If _LastRootLinkClicked Is Nothing OrElse _LastRootLinkClicked IsNot plnkChosen Then
        'show them 
        Dim pLevel01Ordinate As Integer = GetLevel01Ordinate(plnkChosen)
        If pLevel01Ordinate > 0 Then
          For iCntr = 1 To 29
            Dim pLnk As Label = GetLink(pLevel01Ordinate, iCntr)
            If pLnk.Tag IsNot Nothing Then pLnk.Visible = True : Application.DoEvents() : Threading.Thread.Sleep(_SleepTime)
          Next
        End If
        _LastRootLinkClicked = plnkChosen
      Else
        _LastRootLinkClicked = Nothing
      End If

    Else
      MakeAllLinksBlack()
      ActivateLink(plnkChosen)
      _ActiveMenuItem = CType(plnkChosen.Tag, clsMenu.clsMenuItem)
      RaiseEvent evtLinkClicked(_ActiveMenuItem)
    End If

    plnkChosen.Cursor = Cursors.Default : Application.DoEvents()

  End Sub
  Private Sub lblSplitter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblSplitter.Click
    If _SplitterStatus = enmSplitterStatus.Pinned Then
      SetSplitterStatus(enmSplitterStatus.Open)
    ElseIf _SplitterStatus = enmSplitterStatus.Open Then
      SetSplitterStatus(enmSplitterStatus.Pinned)
    End If
  End Sub
  Private Sub lblHelp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblHelp.Click
    RaiseEvent evtHelpClicked()
  End Sub

  'Handlers Mouse moves
  Private Sub lnk_MouseEnter(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         lblHelp.GotFocus, lblHelp.MouseEnter, lblSplitter.GotFocus, lblSplitter.MouseEnter,
        lnk0100.MouseEnter, lnk0101.MouseEnter, lnk0102.MouseEnter, lnk0103.MouseEnter, lnk0104.MouseEnter, lnk0105.MouseEnter, lnk0106.MouseEnter, lnk0107.MouseEnter, lnk0108.MouseEnter, lnk0109.MouseEnter, lnk0110.MouseEnter, lnk0111.MouseEnter, lnk0112.MouseEnter, lnk0113.MouseEnter, lnk0114.MouseEnter, lnk0115.MouseEnter, lnk0116.MouseEnter, lnk0117.MouseEnter, lnk0118.MouseEnter, lnk0119.MouseEnter, lnk0120.MouseEnter, lnk0121.MouseEnter, lnk0122.MouseEnter, lnk0123.MouseEnter, lnk0124.MouseEnter, lnk0125.MouseEnter, lnk0126.MouseEnter, lnk0127.MouseEnter, lnk0128.MouseEnter, lnk0129.MouseEnter,
        lnk0200.MouseEnter, lnk0201.MouseEnter, lnk0202.MouseEnter, lnk0203.MouseEnter, lnk0204.MouseEnter, lnk0205.MouseEnter, lnk0206.MouseEnter, lnk0207.MouseEnter, lnk0208.MouseEnter, lnk0209.MouseEnter, lnk0210.MouseEnter, lnk0211.MouseEnter, lnk0212.MouseEnter, lnk0213.MouseEnter, lnk0214.MouseEnter, lnk0215.MouseEnter, lnk0216.MouseEnter, lnk0217.MouseEnter, lnk0218.MouseEnter, lnk0219.MouseEnter, lnk0220.MouseEnter, lnk0221.MouseEnter, lnk0222.MouseEnter, lnk0223.MouseEnter, lnk0224.MouseEnter, lnk0225.MouseEnter, lnk0226.MouseEnter, lnk0227.MouseEnter, lnk0228.MouseEnter, lnk0229.MouseEnter,
        lnk0300.MouseEnter, lnk0301.MouseEnter, lnk0302.MouseEnter, lnk0303.MouseEnter, lnk0304.MouseEnter, lnk0305.MouseEnter, lnk0306.MouseEnter, lnk0307.MouseEnter, lnk0308.MouseEnter, lnk0309.MouseEnter, lnk0310.MouseEnter, lnk0311.MouseEnter, lnk0312.MouseEnter, lnk0313.MouseEnter, lnk0314.MouseEnter, lnk0315.MouseEnter, lnk0316.MouseEnter, lnk0317.MouseEnter, lnk0318.MouseEnter, lnk0319.MouseEnter, lnk0320.MouseEnter, lnk0321.MouseEnter, lnk0322.MouseEnter, lnk0323.MouseEnter, lnk0324.MouseEnter, lnk0325.MouseEnter, lnk0326.MouseEnter, lnk0327.MouseEnter, lnk0328.MouseEnter, lnk0329.MouseEnter,
        lnk0400.MouseEnter, lnk0401.MouseEnter, lnk0402.MouseEnter, lnk0403.MouseEnter, lnk0404.MouseEnter, lnk0405.MouseEnter, lnk0406.MouseEnter, lnk0407.MouseEnter, lnk0408.MouseEnter, lnk0409.MouseEnter, lnk0410.MouseEnter, lnk0411.MouseEnter, lnk0412.MouseEnter, lnk0413.MouseEnter, lnk0414.MouseEnter, lnk0415.MouseEnter, lnk0416.MouseEnter, lnk0417.MouseEnter, lnk0418.MouseEnter, lnk0419.MouseEnter, lnk0420.MouseEnter, lnk0421.MouseEnter, lnk0422.MouseEnter, lnk0423.MouseEnter, lnk0424.MouseEnter, lnk0425.MouseEnter, lnk0426.MouseEnter, lnk0427.MouseEnter, lnk0428.MouseEnter, lnk0429.MouseEnter,
        lnk0500.MouseEnter, lnk0501.MouseEnter, lnk0502.MouseEnter, lnk0503.MouseEnter, lnk0504.MouseEnter, lnk0505.MouseEnter, lnk0506.MouseEnter, lnk0507.MouseEnter, lnk0508.MouseEnter, lnk0509.MouseEnter, lnk0510.MouseEnter, lnk0511.MouseEnter, lnk0512.MouseEnter, lnk0513.MouseEnter, lnk0514.MouseEnter, lnk0515.MouseEnter, lnk0516.MouseEnter, lnk0517.MouseEnter, lnk0518.MouseEnter, lnk0519.MouseEnter, lnk0520.MouseEnter, lnk0521.MouseEnter, lnk0522.MouseEnter, lnk0523.MouseEnter, lnk0524.MouseEnter, lnk0525.MouseEnter, lnk0526.MouseEnter, lnk0527.MouseEnter, lnk0528.MouseEnter, lnk0529.MouseEnter,
        lnk0600.MouseEnter, lnk0601.MouseEnter, lnk0602.MouseEnter, lnk0603.MouseEnter, lnk0604.MouseEnter, lnk0605.MouseEnter, lnk0606.MouseEnter, lnk0607.MouseEnter, lnk0608.MouseEnter, lnk0609.MouseEnter, lnk0610.MouseEnter, lnk0611.MouseEnter, lnk0612.MouseEnter, lnk0613.MouseEnter, lnk0614.MouseEnter, lnk0615.MouseEnter, lnk0616.MouseEnter, lnk0617.MouseEnter, lnk0618.MouseEnter, lnk0619.MouseEnter, lnk0620.MouseEnter, lnk0621.MouseEnter, lnk0622.MouseEnter, lnk0623.MouseEnter, lnk0624.MouseEnter, lnk0625.MouseEnter, lnk0626.MouseEnter, lnk0627.MouseEnter, lnk0628.MouseEnter, lnk0629.MouseEnter,
        lnk0700.MouseEnter, lnk0701.MouseEnter, lnk0702.MouseEnter, lnk0703.MouseEnter, lnk0704.MouseEnter, lnk0705.MouseEnter, lnk0706.MouseEnter, lnk0707.MouseEnter, lnk0708.MouseEnter, lnk0709.MouseEnter, lnk0710.MouseEnter, lnk0711.MouseEnter, lnk0712.MouseEnter, lnk0713.MouseEnter, lnk0714.MouseEnter, lnk0715.MouseEnter, lnk0716.MouseEnter, lnk0717.MouseEnter, lnk0718.MouseEnter, lnk0719.MouseEnter, lnk0720.MouseEnter, lnk0721.MouseEnter, lnk0722.MouseEnter, lnk0723.MouseEnter, lnk0724.MouseEnter, lnk0725.MouseEnter, lnk0726.MouseEnter, lnk0727.MouseEnter, lnk0728.MouseEnter, lnk0729.MouseEnter,
        lnk0800.MouseEnter, lnk0801.MouseEnter, lnk0802.MouseEnter, lnk0803.MouseEnter, lnk0804.MouseEnter, lnk0805.MouseEnter, lnk0806.MouseEnter, lnk0807.MouseEnter, lnk0808.MouseEnter, lnk0809.MouseEnter, lnk0810.MouseEnter, lnk0811.MouseEnter, lnk0812.MouseEnter, lnk0813.MouseEnter, lnk0814.MouseEnter, lnk0815.MouseEnter, lnk0816.MouseEnter, lnk0817.MouseEnter, lnk0818.MouseEnter, lnk0819.MouseEnter, lnk0820.MouseEnter, lnk0821.MouseEnter, lnk0822.MouseEnter, lnk0823.MouseEnter, lnk0824.MouseEnter, lnk0825.MouseEnter, lnk0826.MouseEnter, lnk0827.MouseEnter, lnk0828.MouseEnter, lnk0829.MouseEnter,
        lnk0900.MouseEnter, lnk0901.MouseEnter, lnk0902.MouseEnter, lnk0903.MouseEnter, lnk0904.MouseEnter, lnk0905.MouseEnter, lnk0906.MouseEnter, lnk0907.MouseEnter, lnk0908.MouseEnter, lnk0909.MouseEnter, lnk0910.MouseEnter, lnk0911.MouseEnter, lnk0912.MouseEnter, lnk0913.MouseEnter, lnk0914.MouseEnter, lnk0915.MouseEnter, lnk0916.MouseEnter, lnk0917.MouseEnter, lnk0918.MouseEnter, lnk0919.MouseEnter, lnk0920.MouseEnter, lnk0921.MouseEnter, lnk0922.MouseEnter, lnk0923.MouseEnter, lnk0924.MouseEnter, lnk0925.MouseEnter, lnk0926.MouseEnter, lnk0927.MouseEnter, lnk0928.MouseEnter, lnk0929.MouseEnter,
        lnk1000.MouseEnter, lnk1001.MouseEnter, lnk1002.MouseEnter, lnk1003.MouseEnter, lnk1004.MouseEnter, lnk1005.MouseEnter, lnk1006.MouseEnter, lnk1007.MouseEnter, lnk1008.MouseEnter, lnk1009.MouseEnter, lnk1010.MouseEnter, lnk1011.MouseEnter, lnk1012.MouseEnter, lnk1013.MouseEnter, lnk1014.MouseEnter, lnk1015.MouseEnter, lnk1016.MouseEnter, lnk1017.MouseEnter, lnk1018.MouseEnter, lnk1019.MouseEnter, lnk1020.MouseEnter, lnk1021.MouseEnter, lnk1022.MouseEnter, lnk1023.MouseEnter, lnk1024.MouseEnter, lnk1025.MouseEnter, lnk1026.MouseEnter, lnk1027.MouseEnter, lnk1028.MouseEnter, lnk1029.MouseEnter,
        lnk1100.MouseEnter, lnk1101.MouseEnter, lnk1102.MouseEnter, lnk1103.MouseEnter, lnk1104.MouseEnter, lnk1105.MouseEnter, lnk1106.MouseEnter, lnk1107.MouseEnter, lnk1108.MouseEnter, lnk1109.MouseEnter, lnk1110.MouseEnter, lnk1111.MouseEnter, lnk1112.MouseEnter, lnk1113.MouseEnter, lnk1114.MouseEnter, lnk1115.MouseEnter, lnk1116.MouseEnter, lnk1117.MouseEnter, lnk1118.MouseEnter, lnk1119.MouseEnter, lnk1120.MouseEnter, lnk1121.MouseEnter, lnk1122.MouseEnter, lnk1123.MouseEnter, lnk1124.MouseEnter, lnk1125.MouseEnter, lnk1126.MouseEnter, lnk1127.MouseEnter, lnk1128.MouseEnter, lnk1129.MouseEnter,
        lnk1200.MouseEnter, lnk1201.MouseEnter, lnk1202.MouseEnter, lnk1203.MouseEnter, lnk1204.MouseEnter, lnk1205.MouseEnter, lnk1206.MouseEnter, lnk1207.MouseEnter, lnk1208.MouseEnter, lnk1209.MouseEnter, lnk1210.MouseEnter, lnk1211.MouseEnter, lnk1212.MouseEnter, lnk1213.MouseEnter, lnk1214.MouseEnter, lnk1215.MouseEnter, lnk1216.MouseEnter, lnk1217.MouseEnter, lnk1218.MouseEnter, lnk1219.MouseEnter, lnk1220.MouseEnter, lnk1221.MouseEnter, lnk1222.MouseEnter, lnk1223.MouseEnter, lnk1224.MouseEnter, lnk1225.MouseEnter, lnk1226.MouseEnter, lnk1227.MouseEnter, lnk1228.MouseEnter, lnk1229.MouseEnter,
        lnk1300.MouseEnter, lnk1301.MouseEnter, lnk1302.MouseEnter, lnk1303.MouseEnter, lnk1304.MouseEnter, lnk1305.MouseEnter, lnk1306.MouseEnter, lnk1307.MouseEnter, lnk1308.MouseEnter, lnk1309.MouseEnter, lnk1310.MouseEnter, lnk1311.MouseEnter, lnk1312.MouseEnter, lnk1313.MouseEnter, lnk1314.MouseEnter, lnk1315.MouseEnter, lnk1316.MouseEnter, lnk1317.MouseEnter, lnk1318.MouseEnter, lnk1319.MouseEnter, lnk1320.MouseEnter, lnk1321.MouseEnter, lnk1322.MouseEnter, lnk1323.MouseEnter, lnk1324.MouseEnter, lnk1325.MouseEnter, lnk1326.MouseEnter, lnk1327.MouseEnter, lnk1328.MouseEnter, lnk1329.MouseEnter,
        lnk1400.MouseEnter, lnk1401.MouseEnter, lnk1402.MouseEnter, lnk1403.MouseEnter, lnk1404.MouseEnter, lnk1405.MouseEnter, lnk1406.MouseEnter, lnk1407.MouseEnter, lnk1408.MouseEnter, lnk1409.MouseEnter, lnk1410.MouseEnter, lnk1411.MouseEnter, lnk1412.MouseEnter, lnk1413.MouseEnter, lnk1414.MouseEnter, lnk1415.MouseEnter, lnk1416.MouseEnter, lnk1417.MouseEnter, lnk1418.MouseEnter, lnk1419.MouseEnter, lnk1420.MouseEnter, lnk1421.MouseEnter, lnk1422.MouseEnter, lnk1423.MouseEnter, lnk1424.MouseEnter, lnk1425.MouseEnter, lnk1426.MouseEnter, lnk1427.MouseEnter, lnk1428.MouseEnter, lnk1429.MouseEnter,
        lnk1500.MouseEnter, lnk1501.MouseEnter, lnk1502.MouseEnter, lnk1503.MouseEnter, lnk1504.MouseEnter, lnk1505.MouseEnter, lnk1506.MouseEnter, lnk1507.MouseEnter, lnk1508.MouseEnter, lnk1509.MouseEnter, lnk1510.MouseEnter, lnk1511.MouseEnter, lnk1512.MouseEnter, lnk1513.MouseEnter, lnk1514.MouseEnter, lnk1515.MouseEnter, lnk1516.MouseEnter, lnk1517.MouseEnter, lnk1518.MouseEnter, lnk1519.MouseEnter, lnk1520.MouseEnter, lnk1521.MouseEnter, lnk1522.MouseEnter, lnk1523.MouseEnter, lnk1524.MouseEnter, lnk1525.MouseEnter, lnk1526.MouseEnter, lnk1527.MouseEnter, lnk1528.MouseEnter, lnk1529.MouseEnter,
        lnk0100.GotFocus, lnk0101.GotFocus, lnk0102.GotFocus, lnk0103.GotFocus, lnk0104.GotFocus, lnk0105.GotFocus, lnk0106.GotFocus, lnk0107.GotFocus, lnk0108.GotFocus, lnk0109.GotFocus, lnk0110.GotFocus, lnk0111.GotFocus, lnk0112.GotFocus, lnk0113.GotFocus, lnk0114.GotFocus, lnk0115.GotFocus, lnk0116.GotFocus, lnk0117.GotFocus, lnk0118.GotFocus, lnk0119.GotFocus, lnk0120.GotFocus, lnk0121.GotFocus, lnk0122.GotFocus, lnk0123.GotFocus, lnk0124.GotFocus, lnk0125.GotFocus, lnk0126.GotFocus, lnk0127.GotFocus, lnk0128.GotFocus, lnk0129.GotFocus,
        lnk0200.GotFocus, lnk0201.GotFocus, lnk0202.GotFocus, lnk0203.GotFocus, lnk0204.GotFocus, lnk0205.GotFocus, lnk0206.GotFocus, lnk0207.GotFocus, lnk0208.GotFocus, lnk0209.GotFocus, lnk0210.GotFocus, lnk0211.GotFocus, lnk0212.GotFocus, lnk0213.GotFocus, lnk0214.GotFocus, lnk0215.GotFocus, lnk0216.GotFocus, lnk0217.GotFocus, lnk0218.GotFocus, lnk0219.GotFocus, lnk0220.GotFocus, lnk0221.GotFocus, lnk0222.GotFocus, lnk0223.GotFocus, lnk0224.GotFocus, lnk0225.GotFocus, lnk0226.GotFocus, lnk0227.GotFocus, lnk0228.GotFocus, lnk0229.GotFocus,
        lnk0300.GotFocus, lnk0301.GotFocus, lnk0302.GotFocus, lnk0303.GotFocus, lnk0304.GotFocus, lnk0305.GotFocus, lnk0306.GotFocus, lnk0307.GotFocus, lnk0308.GotFocus, lnk0309.GotFocus, lnk0310.GotFocus, lnk0311.GotFocus, lnk0312.GotFocus, lnk0313.GotFocus, lnk0314.GotFocus, lnk0315.GotFocus, lnk0316.GotFocus, lnk0317.GotFocus, lnk0318.GotFocus, lnk0319.GotFocus, lnk0320.GotFocus, lnk0321.GotFocus, lnk0322.GotFocus, lnk0323.GotFocus, lnk0324.GotFocus, lnk0325.GotFocus, lnk0326.GotFocus, lnk0327.GotFocus, lnk0328.GotFocus, lnk0329.GotFocus,
        lnk0400.GotFocus, lnk0401.GotFocus, lnk0402.GotFocus, lnk0403.GotFocus, lnk0404.GotFocus, lnk0405.GotFocus, lnk0406.GotFocus, lnk0407.GotFocus, lnk0408.GotFocus, lnk0409.GotFocus, lnk0410.GotFocus, lnk0411.GotFocus, lnk0412.GotFocus, lnk0413.GotFocus, lnk0414.GotFocus, lnk0415.GotFocus, lnk0416.GotFocus, lnk0417.GotFocus, lnk0418.GotFocus, lnk0419.GotFocus, lnk0420.GotFocus, lnk0421.GotFocus, lnk0422.GotFocus, lnk0423.GotFocus, lnk0424.GotFocus, lnk0425.GotFocus, lnk0426.GotFocus, lnk0427.GotFocus, lnk0428.GotFocus, lnk0429.GotFocus,
        lnk0500.GotFocus, lnk0501.GotFocus, lnk0502.GotFocus, lnk0503.GotFocus, lnk0504.GotFocus, lnk0505.GotFocus, lnk0506.GotFocus, lnk0507.GotFocus, lnk0508.GotFocus, lnk0509.GotFocus, lnk0510.GotFocus, lnk0511.GotFocus, lnk0512.GotFocus, lnk0513.GotFocus, lnk0514.GotFocus, lnk0515.GotFocus, lnk0516.GotFocus, lnk0517.GotFocus, lnk0518.GotFocus, lnk0519.GotFocus, lnk0520.GotFocus, lnk0521.GotFocus, lnk0522.GotFocus, lnk0523.GotFocus, lnk0524.GotFocus, lnk0525.GotFocus, lnk0526.GotFocus, lnk0527.GotFocus, lnk0528.GotFocus, lnk0529.GotFocus,
        lnk0600.GotFocus, lnk0601.GotFocus, lnk0602.GotFocus, lnk0603.GotFocus, lnk0604.GotFocus, lnk0605.GotFocus, lnk0606.GotFocus, lnk0607.GotFocus, lnk0608.GotFocus, lnk0609.GotFocus, lnk0610.GotFocus, lnk0611.GotFocus, lnk0612.GotFocus, lnk0613.GotFocus, lnk0614.GotFocus, lnk0615.GotFocus, lnk0616.GotFocus, lnk0617.GotFocus, lnk0618.GotFocus, lnk0619.GotFocus, lnk0620.GotFocus, lnk0621.GotFocus, lnk0622.GotFocus, lnk0623.GotFocus, lnk0624.GotFocus, lnk0625.GotFocus, lnk0626.GotFocus, lnk0627.GotFocus, lnk0628.GotFocus, lnk0629.GotFocus,
        lnk0700.GotFocus, lnk0701.GotFocus, lnk0702.GotFocus, lnk0703.GotFocus, lnk0704.GotFocus, lnk0705.GotFocus, lnk0706.GotFocus, lnk0707.GotFocus, lnk0708.GotFocus, lnk0709.GotFocus, lnk0710.GotFocus, lnk0711.GotFocus, lnk0712.GotFocus, lnk0713.GotFocus, lnk0714.GotFocus, lnk0715.GotFocus, lnk0716.GotFocus, lnk0717.GotFocus, lnk0718.GotFocus, lnk0719.GotFocus, lnk0720.GotFocus, lnk0721.GotFocus, lnk0722.GotFocus, lnk0723.GotFocus, lnk0724.GotFocus, lnk0725.GotFocus, lnk0726.GotFocus, lnk0727.GotFocus, lnk0728.GotFocus, lnk0729.GotFocus,
        lnk0800.GotFocus, lnk0801.GotFocus, lnk0802.GotFocus, lnk0803.GotFocus, lnk0804.GotFocus, lnk0805.GotFocus, lnk0806.GotFocus, lnk0807.GotFocus, lnk0808.GotFocus, lnk0809.GotFocus, lnk0810.GotFocus, lnk0811.GotFocus, lnk0812.GotFocus, lnk0813.GotFocus, lnk0814.GotFocus, lnk0815.GotFocus, lnk0816.GotFocus, lnk0817.GotFocus, lnk0818.GotFocus, lnk0819.GotFocus, lnk0820.GotFocus, lnk0821.GotFocus, lnk0822.GotFocus, lnk0823.GotFocus, lnk0824.GotFocus, lnk0825.GotFocus, lnk0826.GotFocus, lnk0827.GotFocus, lnk0828.GotFocus, lnk0829.GotFocus,
        lnk0900.GotFocus, lnk0901.GotFocus, lnk0902.GotFocus, lnk0903.GotFocus, lnk0904.GotFocus, lnk0905.GotFocus, lnk0906.GotFocus, lnk0907.GotFocus, lnk0908.GotFocus, lnk0909.GotFocus, lnk0910.GotFocus, lnk0911.GotFocus, lnk0912.GotFocus, lnk0913.GotFocus, lnk0914.GotFocus, lnk0915.GotFocus, lnk0916.GotFocus, lnk0917.GotFocus, lnk0918.GotFocus, lnk0919.GotFocus, lnk0920.GotFocus, lnk0921.GotFocus, lnk0922.GotFocus, lnk0923.GotFocus, lnk0924.GotFocus, lnk0925.GotFocus, lnk0926.GotFocus, lnk0927.GotFocus, lnk0928.GotFocus, lnk0929.GotFocus,
        lnk1000.GotFocus, lnk1001.GotFocus, lnk1002.GotFocus, lnk1003.GotFocus, lnk1004.GotFocus, lnk1005.GotFocus, lnk1006.GotFocus, lnk1007.GotFocus, lnk1008.GotFocus, lnk1009.GotFocus, lnk1010.GotFocus, lnk1011.GotFocus, lnk1012.GotFocus, lnk1013.GotFocus, lnk1014.GotFocus, lnk1015.GotFocus, lnk1016.GotFocus, lnk1017.GotFocus, lnk1018.GotFocus, lnk1019.GotFocus, lnk1020.GotFocus, lnk1021.GotFocus, lnk1022.GotFocus, lnk1023.GotFocus, lnk1024.GotFocus, lnk1025.GotFocus, lnk1026.GotFocus, lnk1027.GotFocus, lnk1028.GotFocus, lnk1029.GotFocus,
        lnk1100.GotFocus, lnk1101.GotFocus, lnk1102.GotFocus, lnk1103.GotFocus, lnk1104.GotFocus, lnk1105.GotFocus, lnk1106.GotFocus, lnk1107.GotFocus, lnk1108.GotFocus, lnk1109.GotFocus, lnk1110.GotFocus, lnk1111.GotFocus, lnk1112.GotFocus, lnk1113.GotFocus, lnk1114.GotFocus, lnk1115.GotFocus, lnk1116.GotFocus, lnk1117.GotFocus, lnk1118.GotFocus, lnk1119.GotFocus, lnk1120.GotFocus, lnk1121.GotFocus, lnk1122.GotFocus, lnk1123.GotFocus, lnk1124.GotFocus, lnk1125.GotFocus, lnk1126.GotFocus, lnk1127.GotFocus, lnk1128.GotFocus, lnk1129.GotFocus,
        lnk1200.GotFocus, lnk1201.GotFocus, lnk1202.GotFocus, lnk1203.GotFocus, lnk1204.GotFocus, lnk1205.GotFocus, lnk1206.GotFocus, lnk1207.GotFocus, lnk1208.GotFocus, lnk1209.GotFocus, lnk1210.GotFocus, lnk1211.GotFocus, lnk1212.GotFocus, lnk1213.GotFocus, lnk1214.GotFocus, lnk1215.GotFocus, lnk1216.GotFocus, lnk1217.GotFocus, lnk1218.GotFocus, lnk1219.GotFocus, lnk1220.GotFocus, lnk1221.GotFocus, lnk1222.GotFocus, lnk1223.GotFocus, lnk1224.GotFocus, lnk1225.GotFocus, lnk1226.GotFocus, lnk1227.GotFocus, lnk1228.GotFocus, lnk1229.GotFocus,
        lnk1300.GotFocus, lnk1301.GotFocus, lnk1302.GotFocus, lnk1303.GotFocus, lnk1304.GotFocus, lnk1305.GotFocus, lnk1306.GotFocus, lnk1307.GotFocus, lnk1308.GotFocus, lnk1309.GotFocus, lnk1310.GotFocus, lnk1311.GotFocus, lnk1312.GotFocus, lnk1313.GotFocus, lnk1314.GotFocus, lnk1315.GotFocus, lnk1316.GotFocus, lnk1317.GotFocus, lnk1318.GotFocus, lnk1319.GotFocus, lnk1320.GotFocus, lnk1321.GotFocus, lnk1322.GotFocus, lnk1323.GotFocus, lnk1324.GotFocus, lnk1325.GotFocus, lnk1326.GotFocus, lnk1327.GotFocus, lnk1328.GotFocus, lnk1329.GotFocus,
        lnk1400.GotFocus, lnk1401.GotFocus, lnk1402.GotFocus, lnk1403.GotFocus, lnk1404.GotFocus, lnk1405.GotFocus, lnk1406.GotFocus, lnk1407.GotFocus, lnk1408.GotFocus, lnk1409.GotFocus, lnk1410.GotFocus, lnk1411.GotFocus, lnk1412.GotFocus, lnk1413.GotFocus, lnk1414.GotFocus, lnk1415.GotFocus, lnk1416.GotFocus, lnk1417.GotFocus, lnk1418.GotFocus, lnk1419.GotFocus, lnk1420.GotFocus, lnk1421.GotFocus, lnk1422.GotFocus, lnk1423.GotFocus, lnk1424.GotFocus, lnk1425.GotFocus, lnk1426.GotFocus, lnk1427.GotFocus, lnk1428.GotFocus, lnk1429.GotFocus,
        lnk1500.GotFocus, lnk1501.GotFocus, lnk1502.GotFocus, lnk1503.GotFocus, lnk1504.GotFocus, lnk1505.GotFocus, lnk1506.GotFocus, lnk1507.GotFocus, lnk1508.GotFocus, lnk1509.GotFocus, lnk1510.GotFocus, lnk1511.GotFocus, lnk1512.GotFocus, lnk1513.GotFocus, lnk1514.GotFocus, lnk1515.GotFocus, lnk1516.GotFocus, lnk1517.GotFocus, lnk1518.GotFocus, lnk1519.GotFocus, lnk1520.GotFocus, lnk1521.GotFocus, lnk1522.GotFocus, lnk1523.GotFocus, lnk1524.GotFocus, lnk1525.GotFocus, lnk1526.GotFocus, lnk1527.GotFocus, lnk1528.GotFocus, lnk1529.GotFocus

    Dim lnk As Label = CType(sender, Label)
    If lnk.ForeColor = _ColourChosenFore Then Exit Sub

    lnk.BackColor = _ColourHover
    lnk.Cursor = Cursors.Hand
  End Sub
  Private Sub lnk_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
         lblHelp.LostFocus, lblHelp.MouseLeave, lblSplitter.LostFocus, lblSplitter.MouseLeave,
        lnk0100.MouseLeave, lnk0101.MouseLeave, lnk0102.MouseLeave, lnk0103.MouseLeave, lnk0104.MouseLeave, lnk0105.MouseLeave, lnk0106.MouseLeave, lnk0107.MouseLeave, lnk0108.MouseLeave, lnk0109.MouseLeave, lnk0110.MouseLeave, lnk0111.MouseLeave, lnk0112.MouseLeave, lnk0113.MouseLeave, lnk0114.MouseLeave, lnk0115.MouseLeave, lnk0116.MouseLeave, lnk0117.MouseLeave, lnk0118.MouseLeave, lnk0119.MouseLeave, lnk0120.MouseLeave, lnk0121.MouseLeave, lnk0122.MouseLeave, lnk0123.MouseLeave, lnk0124.MouseLeave, lnk0125.MouseLeave, lnk0126.MouseLeave, lnk0127.MouseLeave, lnk0128.MouseLeave, lnk0129.MouseLeave,
        lnk0200.MouseLeave, lnk0201.MouseLeave, lnk0202.MouseLeave, lnk0203.MouseLeave, lnk0204.MouseLeave, lnk0205.MouseLeave, lnk0206.MouseLeave, lnk0207.MouseLeave, lnk0208.MouseLeave, lnk0209.MouseLeave, lnk0210.MouseLeave, lnk0211.MouseLeave, lnk0212.MouseLeave, lnk0213.MouseLeave, lnk0214.MouseLeave, lnk0215.MouseLeave, lnk0216.MouseLeave, lnk0217.MouseLeave, lnk0218.MouseLeave, lnk0219.MouseLeave, lnk0220.MouseLeave, lnk0221.MouseLeave, lnk0222.MouseLeave, lnk0223.MouseLeave, lnk0224.MouseLeave, lnk0225.MouseLeave, lnk0226.MouseLeave, lnk0227.MouseLeave, lnk0228.MouseLeave, lnk0229.MouseLeave,
        lnk0300.MouseLeave, lnk0301.MouseLeave, lnk0302.MouseLeave, lnk0303.MouseLeave, lnk0304.MouseLeave, lnk0305.MouseLeave, lnk0306.MouseLeave, lnk0307.MouseLeave, lnk0308.MouseLeave, lnk0309.MouseLeave, lnk0310.MouseLeave, lnk0311.MouseLeave, lnk0312.MouseLeave, lnk0313.MouseLeave, lnk0314.MouseLeave, lnk0315.MouseLeave, lnk0316.MouseLeave, lnk0317.MouseLeave, lnk0318.MouseLeave, lnk0319.MouseLeave, lnk0320.MouseLeave, lnk0321.MouseLeave, lnk0322.MouseLeave, lnk0323.MouseLeave, lnk0324.MouseLeave, lnk0325.MouseLeave, lnk0326.MouseLeave, lnk0327.MouseLeave, lnk0328.MouseLeave, lnk0329.MouseLeave,
        lnk0400.MouseLeave, lnk0401.MouseLeave, lnk0402.MouseLeave, lnk0403.MouseLeave, lnk0404.MouseLeave, lnk0405.MouseLeave, lnk0406.MouseLeave, lnk0407.MouseLeave, lnk0408.MouseLeave, lnk0409.MouseLeave, lnk0410.MouseLeave, lnk0411.MouseLeave, lnk0412.MouseLeave, lnk0413.MouseLeave, lnk0414.MouseLeave, lnk0415.MouseLeave, lnk0416.MouseLeave, lnk0417.MouseLeave, lnk0418.MouseLeave, lnk0419.MouseLeave, lnk0420.MouseLeave, lnk0421.MouseLeave, lnk0422.MouseLeave, lnk0423.MouseLeave, lnk0424.MouseLeave, lnk0425.MouseLeave, lnk0426.MouseLeave, lnk0427.MouseLeave, lnk0428.MouseLeave, lnk0429.MouseLeave,
        lnk0500.MouseLeave, lnk0501.MouseLeave, lnk0502.MouseLeave, lnk0503.MouseLeave, lnk0504.MouseLeave, lnk0505.MouseLeave, lnk0506.MouseLeave, lnk0507.MouseLeave, lnk0508.MouseLeave, lnk0509.MouseLeave, lnk0510.MouseLeave, lnk0511.MouseLeave, lnk0512.MouseLeave, lnk0513.MouseLeave, lnk0514.MouseLeave, lnk0515.MouseLeave, lnk0516.MouseLeave, lnk0517.MouseLeave, lnk0518.MouseLeave, lnk0519.MouseLeave, lnk0520.MouseLeave, lnk0521.MouseLeave, lnk0522.MouseLeave, lnk0523.MouseLeave, lnk0524.MouseLeave, lnk0525.MouseLeave, lnk0526.MouseLeave, lnk0527.MouseLeave, lnk0528.MouseLeave, lnk0529.MouseLeave,
        lnk0600.MouseLeave, lnk0601.MouseLeave, lnk0602.MouseLeave, lnk0603.MouseLeave, lnk0604.MouseLeave, lnk0605.MouseLeave, lnk0606.MouseLeave, lnk0607.MouseLeave, lnk0608.MouseLeave, lnk0609.MouseLeave, lnk0610.MouseLeave, lnk0611.MouseLeave, lnk0612.MouseLeave, lnk0613.MouseLeave, lnk0614.MouseLeave, lnk0615.MouseLeave, lnk0616.MouseLeave, lnk0617.MouseLeave, lnk0618.MouseLeave, lnk0619.MouseLeave, lnk0620.MouseLeave, lnk0621.MouseLeave, lnk0622.MouseLeave, lnk0623.MouseLeave, lnk0624.MouseLeave, lnk0625.MouseLeave, lnk0626.MouseLeave, lnk0627.MouseLeave, lnk0628.MouseLeave, lnk0629.MouseLeave,
        lnk0700.MouseLeave, lnk0701.MouseLeave, lnk0702.MouseLeave, lnk0703.MouseLeave, lnk0704.MouseLeave, lnk0705.MouseLeave, lnk0706.MouseLeave, lnk0707.MouseLeave, lnk0708.MouseLeave, lnk0709.MouseLeave, lnk0710.MouseLeave, lnk0711.MouseLeave, lnk0712.MouseLeave, lnk0713.MouseLeave, lnk0714.MouseLeave, lnk0715.MouseLeave, lnk0716.MouseLeave, lnk0717.MouseLeave, lnk0718.MouseLeave, lnk0719.MouseLeave, lnk0720.MouseLeave, lnk0721.MouseLeave, lnk0722.MouseLeave, lnk0723.MouseLeave, lnk0724.MouseLeave, lnk0725.MouseLeave, lnk0726.MouseLeave, lnk0727.MouseLeave, lnk0728.MouseLeave, lnk0729.MouseLeave,
        lnk0800.MouseLeave, lnk0801.MouseLeave, lnk0802.MouseLeave, lnk0803.MouseLeave, lnk0804.MouseLeave, lnk0805.MouseLeave, lnk0806.MouseLeave, lnk0807.MouseLeave, lnk0808.MouseLeave, lnk0809.MouseLeave, lnk0810.MouseLeave, lnk0811.MouseLeave, lnk0812.MouseLeave, lnk0813.MouseLeave, lnk0814.MouseLeave, lnk0815.MouseLeave, lnk0816.MouseLeave, lnk0817.MouseLeave, lnk0818.MouseLeave, lnk0819.MouseLeave, lnk0820.MouseLeave, lnk0821.MouseLeave, lnk0822.MouseLeave, lnk0823.MouseLeave, lnk0824.MouseLeave, lnk0825.MouseLeave, lnk0826.MouseLeave, lnk0827.MouseLeave, lnk0828.MouseLeave, lnk0829.MouseLeave,
        lnk0900.MouseLeave, lnk0901.MouseLeave, lnk0902.MouseLeave, lnk0903.MouseLeave, lnk0904.MouseLeave, lnk0905.MouseLeave, lnk0906.MouseLeave, lnk0907.MouseLeave, lnk0908.MouseLeave, lnk0909.MouseLeave, lnk0910.MouseLeave, lnk0911.MouseLeave, lnk0912.MouseLeave, lnk0913.MouseLeave, lnk0914.MouseLeave, lnk0915.MouseLeave, lnk0916.MouseLeave, lnk0917.MouseLeave, lnk0918.MouseLeave, lnk0919.MouseLeave, lnk0920.MouseLeave, lnk0921.MouseLeave, lnk0922.MouseLeave, lnk0923.MouseLeave, lnk0924.MouseLeave, lnk0925.MouseLeave, lnk0926.MouseLeave, lnk0927.MouseLeave, lnk0928.MouseLeave, lnk0929.MouseLeave,
        lnk1000.MouseLeave, lnk1001.MouseLeave, lnk1002.MouseLeave, lnk1003.MouseLeave, lnk1004.MouseLeave, lnk1005.MouseLeave, lnk1006.MouseLeave, lnk1007.MouseLeave, lnk1008.MouseLeave, lnk1009.MouseLeave, lnk1010.MouseLeave, lnk1011.MouseLeave, lnk1012.MouseLeave, lnk1013.MouseLeave, lnk1014.MouseLeave, lnk1015.MouseLeave, lnk1016.MouseLeave, lnk1017.MouseLeave, lnk1018.MouseLeave, lnk1019.MouseLeave, lnk1020.MouseLeave, lnk1021.MouseLeave, lnk1022.MouseLeave, lnk1023.MouseLeave, lnk1024.MouseLeave, lnk1025.MouseLeave, lnk1026.MouseLeave, lnk1027.MouseLeave, lnk1028.MouseLeave, lnk1029.MouseLeave,
        lnk1100.MouseLeave, lnk1101.MouseLeave, lnk1102.MouseLeave, lnk1103.MouseLeave, lnk1104.MouseLeave, lnk1105.MouseLeave, lnk1106.MouseLeave, lnk1107.MouseLeave, lnk1108.MouseLeave, lnk1109.MouseLeave, lnk1110.MouseLeave, lnk1111.MouseLeave, lnk1112.MouseLeave, lnk1113.MouseLeave, lnk1114.MouseLeave, lnk1115.MouseLeave, lnk1116.MouseLeave, lnk1117.MouseLeave, lnk1118.MouseLeave, lnk1119.MouseLeave, lnk1120.MouseLeave, lnk1121.MouseLeave, lnk1122.MouseLeave, lnk1123.MouseLeave, lnk1124.MouseLeave, lnk1125.MouseLeave, lnk1126.MouseLeave, lnk1127.MouseLeave, lnk1128.MouseLeave, lnk1129.MouseLeave,
        lnk1200.MouseLeave, lnk1201.MouseLeave, lnk1202.MouseLeave, lnk1203.MouseLeave, lnk1204.MouseLeave, lnk1205.MouseLeave, lnk1206.MouseLeave, lnk1207.MouseLeave, lnk1208.MouseLeave, lnk1209.MouseLeave, lnk1210.MouseLeave, lnk1211.MouseLeave, lnk1212.MouseLeave, lnk1213.MouseLeave, lnk1214.MouseLeave, lnk1215.MouseLeave, lnk1216.MouseLeave, lnk1217.MouseLeave, lnk1218.MouseLeave, lnk1219.MouseLeave, lnk1220.MouseLeave, lnk1221.MouseLeave, lnk1222.MouseLeave, lnk1223.MouseLeave, lnk1224.MouseLeave, lnk1225.MouseLeave, lnk1226.MouseLeave, lnk1227.MouseLeave, lnk1228.MouseLeave, lnk1229.MouseLeave,
        lnk1300.MouseLeave, lnk1301.MouseLeave, lnk1302.MouseLeave, lnk1303.MouseLeave, lnk1304.MouseLeave, lnk1305.MouseLeave, lnk1306.MouseLeave, lnk1307.MouseLeave, lnk1308.MouseLeave, lnk1309.MouseLeave, lnk1310.MouseLeave, lnk1311.MouseLeave, lnk1312.MouseLeave, lnk1313.MouseLeave, lnk1314.MouseLeave, lnk1315.MouseLeave, lnk1316.MouseLeave, lnk1317.MouseLeave, lnk1318.MouseLeave, lnk1319.MouseLeave, lnk1320.MouseLeave, lnk1321.MouseLeave, lnk1322.MouseLeave, lnk1323.MouseLeave, lnk1324.MouseLeave, lnk1325.MouseLeave, lnk1326.MouseLeave, lnk1327.MouseLeave, lnk1328.MouseLeave, lnk1329.MouseLeave,
        lnk1400.MouseLeave, lnk1401.MouseLeave, lnk1402.MouseLeave, lnk1403.MouseLeave, lnk1404.MouseLeave, lnk1405.MouseLeave, lnk1406.MouseLeave, lnk1407.MouseLeave, lnk1408.MouseLeave, lnk1409.MouseLeave, lnk1410.MouseLeave, lnk1411.MouseLeave, lnk1412.MouseLeave, lnk1413.MouseLeave, lnk1414.MouseLeave, lnk1415.MouseLeave, lnk1416.MouseLeave, lnk1417.MouseLeave, lnk1418.MouseLeave, lnk1419.MouseLeave, lnk1420.MouseLeave, lnk1421.MouseLeave, lnk1422.MouseLeave, lnk1423.MouseLeave, lnk1424.MouseLeave, lnk1425.MouseLeave, lnk1426.MouseLeave, lnk1427.MouseLeave, lnk1428.MouseLeave, lnk1429.MouseLeave,
        lnk1500.MouseLeave, lnk1501.MouseLeave, lnk1502.MouseLeave, lnk1503.MouseLeave, lnk1504.MouseLeave, lnk1505.MouseLeave, lnk1506.MouseLeave, lnk1507.MouseLeave, lnk1508.MouseLeave, lnk1509.MouseLeave, lnk1510.MouseLeave, lnk1511.MouseLeave, lnk1512.MouseLeave, lnk1513.MouseLeave, lnk1514.MouseLeave, lnk1515.MouseLeave, lnk1516.MouseLeave, lnk1517.MouseLeave, lnk1518.MouseLeave, lnk1519.MouseLeave, lnk1520.MouseLeave, lnk1521.MouseLeave, lnk1522.MouseLeave, lnk1523.MouseLeave, lnk1524.MouseLeave, lnk1525.MouseLeave, lnk1526.MouseLeave, lnk1527.MouseLeave, lnk1528.MouseLeave, lnk1529.MouseLeave,
        lnk0100.LostFocus, lnk0101.LostFocus, lnk0102.LostFocus, lnk0103.LostFocus, lnk0104.LostFocus, lnk0105.LostFocus, lnk0106.LostFocus, lnk0107.LostFocus, lnk0108.LostFocus, lnk0109.LostFocus, lnk0110.LostFocus, lnk0111.LostFocus, lnk0112.LostFocus, lnk0113.LostFocus, lnk0114.LostFocus, lnk0115.LostFocus, lnk0116.LostFocus, lnk0117.LostFocus, lnk0118.LostFocus, lnk0119.LostFocus, lnk0120.LostFocus, lnk0121.LostFocus, lnk0122.LostFocus, lnk0123.LostFocus, lnk0124.LostFocus, lnk0125.LostFocus, lnk0126.LostFocus, lnk0127.LostFocus, lnk0128.LostFocus, lnk0129.LostFocus,
        lnk0200.LostFocus, lnk0201.LostFocus, lnk0202.LostFocus, lnk0203.LostFocus, lnk0204.LostFocus, lnk0205.LostFocus, lnk0206.LostFocus, lnk0207.LostFocus, lnk0208.LostFocus, lnk0209.LostFocus, lnk0210.LostFocus, lnk0211.LostFocus, lnk0212.LostFocus, lnk0213.LostFocus, lnk0214.LostFocus, lnk0215.LostFocus, lnk0216.LostFocus, lnk0217.LostFocus, lnk0218.LostFocus, lnk0219.LostFocus, lnk0220.LostFocus, lnk0221.LostFocus, lnk0222.LostFocus, lnk0223.LostFocus, lnk0224.LostFocus, lnk0225.LostFocus, lnk0226.LostFocus, lnk0227.LostFocus, lnk0228.LostFocus, lnk0229.LostFocus,
        lnk0300.LostFocus, lnk0301.LostFocus, lnk0302.LostFocus, lnk0303.LostFocus, lnk0304.LostFocus, lnk0305.LostFocus, lnk0306.LostFocus, lnk0307.LostFocus, lnk0308.LostFocus, lnk0309.LostFocus, lnk0310.LostFocus, lnk0311.LostFocus, lnk0312.LostFocus, lnk0313.LostFocus, lnk0314.LostFocus, lnk0315.LostFocus, lnk0316.LostFocus, lnk0317.LostFocus, lnk0318.LostFocus, lnk0319.LostFocus, lnk0320.LostFocus, lnk0321.LostFocus, lnk0322.LostFocus, lnk0323.LostFocus, lnk0324.LostFocus, lnk0325.LostFocus, lnk0326.LostFocus, lnk0327.LostFocus, lnk0328.LostFocus, lnk0329.LostFocus,
        lnk0400.LostFocus, lnk0401.LostFocus, lnk0402.LostFocus, lnk0403.LostFocus, lnk0404.LostFocus, lnk0405.LostFocus, lnk0406.LostFocus, lnk0407.LostFocus, lnk0408.LostFocus, lnk0409.LostFocus, lnk0410.LostFocus, lnk0411.LostFocus, lnk0412.LostFocus, lnk0413.LostFocus, lnk0414.LostFocus, lnk0415.LostFocus, lnk0416.LostFocus, lnk0417.LostFocus, lnk0418.LostFocus, lnk0419.LostFocus, lnk0420.LostFocus, lnk0421.LostFocus, lnk0422.LostFocus, lnk0423.LostFocus, lnk0424.LostFocus, lnk0425.LostFocus, lnk0426.LostFocus, lnk0427.LostFocus, lnk0428.LostFocus, lnk0429.LostFocus,
        lnk0500.LostFocus, lnk0501.LostFocus, lnk0502.LostFocus, lnk0503.LostFocus, lnk0504.LostFocus, lnk0505.LostFocus, lnk0506.LostFocus, lnk0507.LostFocus, lnk0508.LostFocus, lnk0509.LostFocus, lnk0510.LostFocus, lnk0511.LostFocus, lnk0512.LostFocus, lnk0513.LostFocus, lnk0514.LostFocus, lnk0515.LostFocus, lnk0516.LostFocus, lnk0517.LostFocus, lnk0518.LostFocus, lnk0519.LostFocus, lnk0520.LostFocus, lnk0521.LostFocus, lnk0522.LostFocus, lnk0523.LostFocus, lnk0524.LostFocus, lnk0525.LostFocus, lnk0526.LostFocus, lnk0527.LostFocus, lnk0528.LostFocus, lnk0529.LostFocus,
        lnk0600.LostFocus, lnk0601.LostFocus, lnk0602.LostFocus, lnk0603.LostFocus, lnk0604.LostFocus, lnk0605.LostFocus, lnk0606.LostFocus, lnk0607.LostFocus, lnk0608.LostFocus, lnk0609.LostFocus, lnk0610.LostFocus, lnk0611.LostFocus, lnk0612.LostFocus, lnk0613.LostFocus, lnk0614.LostFocus, lnk0615.LostFocus, lnk0616.LostFocus, lnk0617.LostFocus, lnk0618.LostFocus, lnk0619.LostFocus, lnk0620.LostFocus, lnk0621.LostFocus, lnk0622.LostFocus, lnk0623.LostFocus, lnk0624.LostFocus, lnk0625.LostFocus, lnk0626.LostFocus, lnk0627.LostFocus, lnk0628.LostFocus, lnk0629.LostFocus,
        lnk0700.LostFocus, lnk0701.LostFocus, lnk0702.LostFocus, lnk0703.LostFocus, lnk0704.LostFocus, lnk0705.LostFocus, lnk0706.LostFocus, lnk0707.LostFocus, lnk0708.LostFocus, lnk0709.LostFocus, lnk0710.LostFocus, lnk0711.LostFocus, lnk0712.LostFocus, lnk0713.LostFocus, lnk0714.LostFocus, lnk0715.LostFocus, lnk0716.LostFocus, lnk0717.LostFocus, lnk0718.LostFocus, lnk0719.LostFocus, lnk0720.LostFocus, lnk0721.LostFocus, lnk0722.LostFocus, lnk0723.LostFocus, lnk0724.LostFocus, lnk0725.LostFocus, lnk0726.LostFocus, lnk0727.LostFocus, lnk0728.LostFocus, lnk0729.LostFocus,
        lnk0800.LostFocus, lnk0801.LostFocus, lnk0802.LostFocus, lnk0803.LostFocus, lnk0804.LostFocus, lnk0805.LostFocus, lnk0806.LostFocus, lnk0807.LostFocus, lnk0808.LostFocus, lnk0809.LostFocus, lnk0810.LostFocus, lnk0811.LostFocus, lnk0812.LostFocus, lnk0813.LostFocus, lnk0814.LostFocus, lnk0815.LostFocus, lnk0816.LostFocus, lnk0817.LostFocus, lnk0818.LostFocus, lnk0819.LostFocus, lnk0820.LostFocus, lnk0821.LostFocus, lnk0822.LostFocus, lnk0823.LostFocus, lnk0824.LostFocus, lnk0825.LostFocus, lnk0826.LostFocus, lnk0827.LostFocus, lnk0828.LostFocus, lnk0829.LostFocus,
        lnk0900.LostFocus, lnk0901.LostFocus, lnk0902.LostFocus, lnk0903.LostFocus, lnk0904.LostFocus, lnk0905.LostFocus, lnk0906.LostFocus, lnk0907.LostFocus, lnk0908.LostFocus, lnk0909.LostFocus, lnk0910.LostFocus, lnk0911.LostFocus, lnk0912.LostFocus, lnk0913.LostFocus, lnk0914.LostFocus, lnk0915.LostFocus, lnk0916.LostFocus, lnk0917.LostFocus, lnk0918.LostFocus, lnk0919.LostFocus, lnk0920.LostFocus, lnk0921.LostFocus, lnk0922.LostFocus, lnk0923.LostFocus, lnk0924.LostFocus, lnk0925.LostFocus, lnk0926.LostFocus, lnk0927.LostFocus, lnk0928.LostFocus, lnk0929.LostFocus,
        lnk1000.LostFocus, lnk1001.LostFocus, lnk1002.LostFocus, lnk1003.LostFocus, lnk1004.LostFocus, lnk1005.LostFocus, lnk1006.LostFocus, lnk1007.LostFocus, lnk1008.LostFocus, lnk1009.LostFocus, lnk1010.LostFocus, lnk1011.LostFocus, lnk1012.LostFocus, lnk1013.LostFocus, lnk1014.LostFocus, lnk1015.LostFocus, lnk1016.LostFocus, lnk1017.LostFocus, lnk1018.LostFocus, lnk1019.LostFocus, lnk1020.LostFocus, lnk1021.LostFocus, lnk1022.LostFocus, lnk1023.LostFocus, lnk1024.LostFocus, lnk1025.LostFocus, lnk1026.LostFocus, lnk1027.LostFocus, lnk1028.LostFocus, lnk1029.LostFocus,
        lnk1100.LostFocus, lnk1101.LostFocus, lnk1102.LostFocus, lnk1103.LostFocus, lnk1104.LostFocus, lnk1105.LostFocus, lnk1106.LostFocus, lnk1107.LostFocus, lnk1108.LostFocus, lnk1109.LostFocus, lnk1110.LostFocus, lnk1111.LostFocus, lnk1112.LostFocus, lnk1113.LostFocus, lnk1114.LostFocus, lnk1115.LostFocus, lnk1116.LostFocus, lnk1117.LostFocus, lnk1118.LostFocus, lnk1119.LostFocus, lnk1120.LostFocus, lnk1121.LostFocus, lnk1122.LostFocus, lnk1123.LostFocus, lnk1124.LostFocus, lnk1125.LostFocus, lnk1126.LostFocus, lnk1127.LostFocus, lnk1128.LostFocus, lnk1129.LostFocus,
        lnk1200.LostFocus, lnk1201.LostFocus, lnk1202.LostFocus, lnk1203.LostFocus, lnk1204.LostFocus, lnk1205.LostFocus, lnk1206.LostFocus, lnk1207.LostFocus, lnk1208.LostFocus, lnk1209.LostFocus, lnk1210.LostFocus, lnk1211.LostFocus, lnk1212.LostFocus, lnk1213.LostFocus, lnk1214.LostFocus, lnk1215.LostFocus, lnk1216.LostFocus, lnk1217.LostFocus, lnk1218.LostFocus, lnk1219.LostFocus, lnk1220.LostFocus, lnk1221.LostFocus, lnk1222.LostFocus, lnk1223.LostFocus, lnk1224.LostFocus, lnk1225.LostFocus, lnk1226.LostFocus, lnk1227.LostFocus, lnk1228.LostFocus, lnk1229.LostFocus,
        lnk1300.LostFocus, lnk1301.LostFocus, lnk1302.LostFocus, lnk1303.LostFocus, lnk1304.LostFocus, lnk1305.LostFocus, lnk1306.LostFocus, lnk1307.LostFocus, lnk1308.LostFocus, lnk1309.LostFocus, lnk1310.LostFocus, lnk1311.LostFocus, lnk1312.LostFocus, lnk1313.LostFocus, lnk1314.LostFocus, lnk1315.LostFocus, lnk1316.LostFocus, lnk1317.LostFocus, lnk1318.LostFocus, lnk1319.LostFocus, lnk1320.LostFocus, lnk1321.LostFocus, lnk1322.LostFocus, lnk1323.LostFocus, lnk1324.LostFocus, lnk1325.LostFocus, lnk1326.LostFocus, lnk1327.LostFocus, lnk1328.LostFocus, lnk1329.LostFocus,
        lnk1400.LostFocus, lnk1401.LostFocus, lnk1402.LostFocus, lnk1403.LostFocus, lnk1404.LostFocus, lnk1405.LostFocus, lnk1406.LostFocus, lnk1407.LostFocus, lnk1408.LostFocus, lnk1409.LostFocus, lnk1410.LostFocus, lnk1411.LostFocus, lnk1412.LostFocus, lnk1413.LostFocus, lnk1414.LostFocus, lnk1415.LostFocus, lnk1416.LostFocus, lnk1417.LostFocus, lnk1418.LostFocus, lnk1419.LostFocus, lnk1420.LostFocus, lnk1421.LostFocus, lnk1422.LostFocus, lnk1423.LostFocus, lnk1424.LostFocus, lnk1425.LostFocus, lnk1426.LostFocus, lnk1427.LostFocus, lnk1428.LostFocus, lnk1429.LostFocus,
        lnk1500.LostFocus, lnk1501.LostFocus, lnk1502.LostFocus, lnk1503.LostFocus, lnk1504.LostFocus, lnk1505.LostFocus, lnk1506.LostFocus, lnk1507.LostFocus, lnk1508.LostFocus, lnk1509.LostFocus, lnk1510.LostFocus, lnk1511.LostFocus, lnk1512.LostFocus, lnk1513.LostFocus, lnk1514.LostFocus, lnk1515.LostFocus, lnk1516.LostFocus, lnk1517.LostFocus, lnk1518.LostFocus, lnk1519.LostFocus, lnk1520.LostFocus, lnk1521.LostFocus, lnk1522.LostFocus, lnk1523.LostFocus, lnk1524.LostFocus, lnk1525.LostFocus, lnk1526.LostFocus, lnk1527.LostFocus, lnk1528.LostFocus, lnk1529.LostFocus

    Dim lnk As Label = CType(sender, Label)
    If lnk.ForeColor = _ColourChosenFore Then Exit Sub

    lnk.BackColor = _ColourBack
    lnk.Cursor = Cursors.Default

  End Sub


  'Handle mouse-overs


  'Supporting functions
  Private Function GetLink(ByVal vLevel01Ordinate As Integer, ByVal vLevel02Ordinate As Integer) As Label
    For Each pControl As Control In Me.flpMenu.Controls
      'If pControl.Name = "lblBack" Then Continue For
      Dim pStrg As String = vLevel01Ordinate.ToString.Trim.PadLeft(2, "0"c) & vLevel02Ordinate.ToString.Trim.PadLeft(2, "0"c)
      If pControl.Name = "lnk" & pStrg Then Return CType(pControl, Label)
    Next
    Return Nothing
  End Function
  Private Function GetLevel01Ordinate(ByVal vLnk As Label) As Integer
    Dim pLinkName As String = vLnk.Name

    Try
      Dim pStrg As String = pLinkName.Substring(3, 2)
      Return ccHelper.ToInteger(pStrg)
    Catch ex As Exception
      Return -999
    End Try

    Return -999
  End Function
  Private Function GetLevel02Ordinate(ByVal vLnk As Label) As Integer
    Dim pLinkName As String = vLnk.Name

    Try
      Dim pStrg As String = pLinkName.Substring(5, 2)
      Return ccHelper.ToInteger(pStrg)
    Catch ex As Exception
      Return -999
    End Try

    Return -999
  End Function

  'Draws links
  Private Sub ResetLinkBorders()
    For i = 1 To 15
      For j = 0 To 29
        Dim pLnk As Label = GetLink(i, j)
        pLnk.BackColor = _ColourBack
      Next
    Next
  End Sub
  Private Sub HideSecondaryLinks()
    For i = 1 To 15
      For j = 1 To 29
        Dim pLnk As Label = GetLink(i, j)
        If pLnk.Visible = True Then pLnk.Visible = False : Application.DoEvents() : Threading.Thread.Sleep(_SleepTime)
      Next
    Next
  End Sub
  Private Sub MakeAllLinksBlack()

    ResetLinkBorders()

    'Make all links Black 
    For i = 1 To 15
      For j = 0 To 29
        Dim pLnk As Label = GetLink(i, j)
        pLnk.ForeColor = _ColourDefaultFore
      Next
    Next
  End Sub
  Private Sub ActivateLink(ByRef lnk As Label)
    lnk.ForeColor = _ColourChosenFore
    lnk.BackColor = _ColourChosenBack
    Application.DoEvents()
  End Sub

  Private Sub MenuTree_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
    pnlTop.Width = flpMenu.Width - 10
    lblHelp.Left = pnlTop.Width - 22
  End Sub

  Private Sub MenuTree_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
    If Me.Visible Then RaiseEvent evtMadeVisible()
  End Sub
End Class

