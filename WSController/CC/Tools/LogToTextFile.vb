Option Strict On

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO

Namespace Tools

  ''' <summary>
  ''' Writes to the location defined in LogLocation. Use local or "" or actual folder. Local writes to MyDocuments. "" writes to 'CurrentUserApplicationData' folder. actual folder writes to actual folder :-)
  ''' </summary>
  ''' <remarks></remarks>
  Public Class LogToTextFile

    Public Enum NewLogTimePeriod
      Daily
      Monthly
    End Enum

    Private Shared _NewLogTimePeriod As NewLogTimePeriod = NewLogTimePeriod.Daily

    Private Shared _LogLocation As String = ""

    Private Shared _WriteObj As New Object

    ''' <summary>
    ''' Write message to file (done since C# doesn't like Optional ByRefs)
    ''' </summary>
    ''' <param name="vMsg"></param>
    ''' <param name="vLogPrefix"></param>
    ''' <returns></returns>
    Public Shared Function WriteMessage(vMsg As String, vLogPrefix As String) As String
      Return WriteMessage(vMsg, vLogPrefix, vNewLogTimePeriod:=NewLogTimePeriod.Daily)
    End Function

    ''' <summary>
    ''' rFileName returns the filename used. You cannot set the filename with it.
    ''' </summary>
    ''' <param name="vMsg"></param>
    ''' <param name="vLogPrefix"></param>
    ''' <param name="vSkipLineBefore"></param>
    ''' <param name="vNewLogTimePeriod"></param>
    ''' <param name="rFileName"></param>
    ''' <returns></returns>
    Public Shared Function WriteMessage(vMsg As String, vLogPrefix As String, Optional vSkipLineBefore As Boolean = False, Optional vNewLogTimePeriod As NewLogTimePeriod = NewLogTimePeriod.Daily, Optional ByRef rFileName As String = "") As String

      Dim pFileName As String = ""

      SyncLock _WriteObj

        _LogLocation = MyController.LogLocation

        'Save the response
        Dim w As StreamWriter
        Dim lcntr As Long
        Dim pLogPrefix As String = vLogPrefix
        Do
          Try
            Try
              If System.Reflection.Assembly.GetEntryAssembly IsNot Nothing Then
                pLogPrefix = (System.Reflection.Assembly.GetEntryAssembly.GetName.Name) & "." & (System.Reflection.Assembly.GetCallingAssembly.GetName.Name) & "~" & vLogPrefix
                Dim pTopLevel As String = pLogPrefix.Split("."c)(0)
                pLogPrefix = ($"{pTopLevel}.{pLogPrefix.Replace(pTopLevel, "")}").Replace("..", ".")
              Else
                pLogPrefix = (System.Reflection.Assembly.GetCallingAssembly.GetName.Name)
                Dim pTopLevel As String = pLogPrefix.Split("."c)(0)
                pLogPrefix = ($"{pTopLevel}.WS.{pLogPrefix.Replace(pTopLevel, "")}").Replace("..", ".") & "~" & vLogPrefix
              End If
            Catch ex As Exception
              pLogPrefix = "Ex_" & vLogPrefix
            End Try
            Dim LogTime As String = String.Empty
            If vNewLogTimePeriod = NewLogTimePeriod.Monthly Then
              LogTime = DateTime.Now.ToString("yyyyMM")
            Else
              LogTime = DateTime.Now.ToString("yyyyMMdd")
            End If
            pFileName = _LogLocation & pLogPrefix & "_" & LogTime & ".txt"
            rFileName = pFileName
            w = File.AppendText(pFileName)
            Exit Do
          Catch ex As Exception
            'wait up to 10 seconds to free the file
            System.Threading.Thread.Sleep(1000)
            If lcntr = 10 Then
              'open a file that includes the seconds
              Try
                w = File.AppendText(_LogLocation & pLogPrefix & "_" & DateTime.Now.ToString("yyyyMMddHHmmss") & ".txt")
                Exit Do 'the chances of this already existing is super small
              Catch exx As Exception
                Return exx.Message
              End Try
            End If
          End Try
          lcntr += 1
        Loop
        'System.Threading.Thread.Sleep(5000) 'for tests
        If vSkipLineBefore Then
          w.WriteLine("")
        End If
        w.WriteLine(DateTime.Now.ToString("dd/MM/yy HH:mm:ss.ffff") & " " & vMsg.Replace(Environment.NewLine, Environment.NewLine & New String(" "c, 23)))
        ' Update the underlying file.
        w.Flush()
        ' Close the writer and underlying file.
        w.Close()
      End SyncLock

      Return "OK"
    End Function

    Private Shared Function RemovePrefix(vFullString As String) As String
      If String.IsNullOrEmpty(vFullString) Then Return ""

      Dim pIndex = vFullString.IndexOf(".", StringComparison.OrdinalIgnoreCase)
      If pIndex = -1 Then Return vFullString

      Return vFullString.Substring(pIndex + 1)

    End Function

    Public Shared Function WriteException(vMessagePrefix As String, vEx As Exception, vLogPrefix As String, Optional vNewLogTimePeriod As NewLogTimePeriod = NewLogTimePeriod.Daily) As String
      Dim pMessage As String
      If vMessagePrefix.Length > 0 Then
        pMessage = vMessagePrefix & Environment.NewLine & GetExceptionString(vEx)
      Else
        pMessage = GetExceptionString(vEx)
      End If

      Return WriteMessage(pMessage.Replace("~", Environment.NewLine), vLogPrefix, vNewLogTimePeriod:=vNewLogTimePeriod)
    End Function

    Public Shared Function GetExceptionString(vException As Exception) As String
      Dim pString As String
      Dim pEx As Exception = vException
      Dim iCntr As Integer = 1

      pString = "Exception: ~" & iCntr & ". " & pEx.Message & "~" & vException.StackTrace


      Do Until pEx.InnerException Is Nothing
        iCntr += 1
        pEx = pEx.InnerException
        pString &= " ~" & iCntr & ". " & pEx.Message & "~" & pEx.StackTrace
      Loop

      Return pString

    End Function

  End Class
End Namespace