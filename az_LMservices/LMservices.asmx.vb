Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Globalization
Imports System.ComponentModel
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports System.Math
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Win32


' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://zulhisham-tan/az_services/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class az_Services
    Inherits System.Web.Services.WebService

    'Dim ComData As New CommonData.DataCenter
    Dim marking2 As New Marking2.MarkingCode


    <WebMethod(Description:="Returns 'Hello World'... ")> _
    Public Function HelloWorld() As String

        Return "Hello World"

    End Function

    <WebMethod(Description:="Return Server Timestamp...")> _
    Public Function GetServerTimeStamp() As DateTime

        Return Now

    End Function

    <WebMethod(Description:="Return PX Product Code or Name...")> _
    Public Function GetProductCode(ByVal IMINo As String) As String

        Dim RetDat As String = String.Empty


        With My.Computer


            'IMI_Path
            Dim IMIFilePath As String = IMI_Path & "\" & IMINo & ".dat"
            Dim records As Rec = Nothing

            If .FileSystem.FileExists(IMIFilePath) Then
                If Not ParseSpecData(IMIFilePath, records) < 0 Then
                    RetDat = records.Profile
                End If
            End If
        End With

        Return RetDat

    End Function

    <WebMethod(Description:="Returns TRUE/FALSE on detecting the spec. file path... ")> _
    Public Function CheckIMI(ByVal IMINo As String) As String

        'IMI_Path
        Return My.Computer.FileSystem.FileExists(IMI_Path & "\" & IMINo & ".dat")

    End Function

    <WebMethod(Description:="Returns the information about this services... ")> _
    Public Function AboutMe() As String

        Return "This WebServices is designed by Zulhisham @2010."

    End Function

    <WebMethod(Description:="Returns WeekCode... ")> _
    Public Function WeekCode(ByVal sFormat As String) As String

        Dim m_Format As String = sFormat.ToLower.Trim
        Dim m_WkCode As String = String.Empty
        Dim m_Today As Date = Today

        Dim Year_D As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim Month_D As String = "123456789XYZ"
        Dim Day_D As String = "123456789ABCDEFGHJKLMNOPQRSTUVWXYZ"
        Dim Day_D_ As String = "123456789ABCDEFGHJKLMNPQRSTUVWXYZ"
        Dim WkNoCd As String = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ"

        Dim myCI As New CultureInfo("en-US")
        Dim myCal As Calendar = myCI.Calendar


        Select Case m_Format
            Case Is = "ymd"
                m_WkCode = String.Format("{0:D2}", m_Today.Year)
                m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_D.Substring(m_Today.Day - 1, 1)

            Case Is = "ymdd"
                m_WkCode = String.Format("{0:D2}", m_Today.Year)
                m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & String.Format("{0:D2}", m_Today.Day)

            Case Is = "ym"
                m_WkCode = String.Format("{0:D2}", m_Today.Year)
                m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1)

            Case Is = "md"
                Dim m_WeekNo As Integer = myCal.GetWeekOfYear(m_Today, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday)

                If m_WeekNo > 50 Then
                    If m_Today.Month = 1 Then
                        m_WkCode = Year_D.Substring(m_Today.Year - 2001 - 1, 1) & Year_D.Substring(Abs((m_WeekNo + 0.1) / 2) - 1, 1)
                    Else
                        m_WkCode = Year_D.Substring(m_Today.Year - 2001, 1) & "Z"
                    End If
                Else
                    m_WkCode = Year_D.Substring(m_Today.Year - 2001, 1) & Year_D.Substring(Abs((m_WeekNo + 0.1) / 2) - 1, 1)
                End If

            Case Is = "yww"
                m_WkCode = String.Format("{0:D2}", m_Today.Year)
                m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & String.Format("{0:D2}", myCal.GetWeekOfYear(m_Today, CalendarWeekRule.FirstDay, DayOfWeek.Monday))

            Case Else
                m_WkCode = String.Format("{0:D2}", m_Today.Year)
                m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_D.Substring(m_Today.Day - 1, 1)

        End Select

        Return m_WkCode

    End Function

    <WebMethod(Description:="Return A Week Code")> _
    Public Function azWeekCode_FC(ByVal sFormat As String) As String

        Dim sFmt As String = sFormat.ToLower
        Dim WebDate As Date = Now
        Dim sRetVal As String = String.Empty

        Dim WebMonth As String = "123456789XYZ"
        Dim WebDay As String = "123456789ABCDEFGHJKLMNPQRSTUVWXYZ"


        Select Case sFmt
            Case Is = "ymd"
                sRetVal = Right(Trim(Str(Year(WebDate))), 1) & Mid(WebMonth, Month(WebDate), 1) & Mid(WebDay, Day(WebDate), 1)
            Case Is = "ydm"
                sRetVal = Right(Trim(Str(Year(WebDate))), 1) & Mid(WebDay, Day(WebDate), 1) & Mid(WebMonth, Month(WebDate), 1)
            Case Is = "dmy"
                sRetVal = Mid(WebDay, Day(WebDate), 1) & Mid(WebMonth, Month(WebDate), 1) & Right(Trim(Str(Year(WebDate))), 1)
            Case Else
                Dim myCI As New CultureInfo("en-US")
                Dim myCal As Calendar = myCI.Calendar
                Dim YrVal As String = String.Format("{0:D4}", WebDate.Year)

                sRetVal = "A" & YrVal.Substring(3) & String.Format("{0:D2}", myCal.GetWeekOfYear(Now, CalendarWeekRule.FirstDay, DayOfWeek.Monday)) & "L"
        End Select

        Return sRetVal

    End Function

    <WebMethod(Description:="Return A Week Code For FC Package (Advance Version)")> _
    Public Function az_FCweekcode_ad(ByVal Lot_No As String, ByVal SpecNo As String, ByRef RetData() As String) As Integer

        Dim WebDate As Date = Now
        Dim WebDateAdv As Date = WebDate.AddDays(1)
        Dim sRetVal As String = String.Empty

        Dim WebMonth As String = "123456789XYZ"
        Dim WebDay As String = "123456789ABCDEFGHJKLMNOPQRSTUVWXYZ"


        With My.Computer
            Dim SpecFile As String = "D:\MachineNet\MacDB\Marking\FC\IMI\" & SpecNo & ".dat"

            If Not .FileSystem.FileExists(SpecFile) Then
                Return -1
            End If

            Dim FileContent As String = .FileSystem.ReadAllText(SpecFile)
            Dim ContentItems() As String = FileContent.Split(vbCr)

            Dim Freq() As String = ContentItems.Where(Function(n) n.Contains("L001")).ToArray
            Dim Plant() As String = ContentItems.Where(Function(n) n.Contains("L002")).ToArray
            Dim PCode() As String = ContentItems.Where(Function(n) n.Contains("L003")).ToArray
            Dim nVersion() As String = ContentItems.Where(Function(n) n.Contains("L004")).ToArray
            Dim DateFmt() As String = ContentItems.Where(Function(n) n.Contains("L005")).ToArray
            Dim Parameter() As String = ContentItems.Where(Function(n) n.Contains("L006")).ToArray

            If Freq.Length <> 1 Or DateFmt.Length <> 1 Or Plant.Length <> 1 Or Parameter.Length <> 1 Then
                Return -1
            End If

            Dim Freq_() As String = Freq(0).Split(","c)
            Dim Plant_() As String = Plant(0).Split(","c)
            Dim ProdCode_() As String = PCode(0).Split(","c)
            Dim Version_() As String = nVersion(0).Split(","c)
            Dim sFmt() As String = DateFmt(0).Split(","c)
            Dim MrkPrm() As String = Parameter(0).Split(","c)
            Dim MrkPrm_() As String = MrkPrm(2).Split("|"c)

            Dim m_WkCode As String = String.Empty


            Select Case sFmt(2).Trim
                Case Is = "ymd"
                    m_WkCode = String.Format("{0:D2}", WebDate.Year)
                    m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & WebMonth.Substring(WebDate.Month - 1, 1) & WebDay.Substring(WebDate.Day - 1, 1)

                    Try
                        If Not MrkPrm(2).Trim.ToUpper.Contains("|") Then
                            If MrkPrm(2).Trim.ToUpper.Contains("RA") Then
                                If ProdCode_(2).Trim = "-" Then
                                    ProdCode_(2) = ""
                                End If

                                sRetVal = ProdCode_(2).Trim & m_WkCode.Trim & Version_(2).Trim
                            Else
                                If ProdCode_(2).Trim.StartsWith("@") And ProdCode_(2).Trim.EndsWith("@") And ProdCode_.Length >= 3 Then
                                    sRetVal = Freq_(2).Trim & m_WkCode.Trim
                                Else
                                    sRetVal = Freq_(2).Trim & m_WkCode.Trim & Plant_(2).Trim
                                End If
                            End If
                        Else
                            sRetVal = m_WkCode.Trim
                        End If

                    Catch ex As Exception
                        Return -1
                    End Try
                Case Else
                    Dim myCI As New CultureInfo("en-US")
                    Dim myCal As Calendar = myCI.Calendar
                    Dim YrVal As String = String.Format("{0:D4}", WebDate.Year)

                    Try
                        If Not MrkPrm(2).Trim.ToUpper.Contains("|") Then
                            sRetVal = Freq_(2).Trim & YrVal.Substring(3) & String.Format("{0:D2}", myCal.GetWeekOfYear(Now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday)) & Plant_(2).Trim
                        Else
                            sRetVal = YrVal.Substring(3) & String.Format("{0:D2}", myCal.GetWeekOfYear(Now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday))
                        End If
                    Catch ex As Exception
                        Return -1
                    End Try
            End Select

            ReDim RetData(6)
            Dim addPrm As String = ""

            If MrkPrm(2).Trim.ToUpper.Contains("SG") Then
                addPrm = ",3030"
            ElseIf MrkPrm(2).Trim.ToUpper.Contains("RA") Then
                addPrm = "," & Freq_(2).Trim & MrkPrm(2).Trim.ToUpper.Substring(2, 4) & Plant_(2).Trim
            ElseIf MrkPrm(2).Trim.ToUpper.Contains("|") And MrkPrm_.Length >= 3 Then
                If MrkPrm_(0).Contains("ymd") Then
                    sRetVal = MrkPrm_(0).Trim.Replace("ymd", sRetVal)
                ElseIf MrkPrm_(0).Contains("yww") Then
                    sRetVal = MrkPrm_(0).Trim.Replace("yww", sRetVal)
                End If

                If MrkPrm_(1).Contains("#") Then
                    Dim CntSymbol As Integer = 0
                    Dim CntLot As Integer = GetSerialNoBySpec(Lot_No, SpecNo,
                                                              String.Format("{0:D4}-{1:D2}-{2:D2}", WebDate.Year, WebDate.Month, WebDate.Day),
                                                              String.Format("{0:D4}-{1:D2}-{2:D2}", WebDateAdv.Year, WebDateAdv.Month, WebDateAdv.Day))

                    For idx = 0 To MrkPrm_(1).Length - 1
                        If MrkPrm_(1).Substring(idx, 1) = "#" Then
                            CntSymbol += 1
                        End If
                    Next

                    If CntLot <= 0 Then
                        Return -1
                    Else
                        addPrm = "," & CntLot.ToString.PadLeft(CntSymbol, "0"c) & MrkPrm_(1).Replace("#", "")
                    End If
                Else
                    addPrm = ""
                End If

                MrkPrm(2) = MrkPrm_(2).Trim
            ElseIf ProdCode_(2).Trim.StartsWith("@") And ProdCode_(2).Trim.EndsWith("@") And ProdCode_.Length >= 3 Then
                addPrm = "," & marking2.GetMarkingSequenceFC(Lot_No, SpecNo, String.Format("{0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}", WebDate.Year, WebDate.Month, WebDate.Day, WebDate.Hour, WebDate.Minute, WebDate.Second))
            End If

            RetData(0) = Lot_No
            RetData(1) = SpecNo
            RetData(2) = sRetVal & addPrm   'weekcode always comes before comma
            RetData(3) = MrkPrm(2).Trim
            RetData(4) = Freq_(2).Trim
            RetData(5) = sFmt(2).Trim
            RetData(6) = Plant_(2).Trim

            Return RetData.Length
        End With

    End Function

    <WebMethod(Description:="Return A Week Code For FC Package (Advance Version)")>
    Public Function az_FCweekcode_ad_test(ByVal Lot_No As String, ByVal SpecNo As String) As String

        Dim WebDate As Date = Now
        Dim WebDateAdv As Date = WebDate.AddDays(1)
        Dim sRetVal As String = String.Empty

        Dim WebMonth As String = "123456789XYZ"
        Dim WebDay As String = "123456789ABCDEFGHJKLMNOPQRSTUVWXYZ"


        With My.Computer
            Dim SpecFile As String = "C:\tmp\Test\" & SpecNo & ".dat"

            If Not .FileSystem.FileExists(SpecFile) Then
                Return -1
            End If

            Dim FileContent As String = .FileSystem.ReadAllText(SpecFile)
            Dim ContentItems() As String = FileContent.Split(vbCr)

            Dim Freq() As String = ContentItems.Where(Function(n) n.Contains("L001")).ToArray
            Dim Plant() As String = ContentItems.Where(Function(n) n.Contains("L002")).ToArray
            Dim PCode() As String = ContentItems.Where(Function(n) n.Contains("L003")).ToArray
            Dim nVersion() As String = ContentItems.Where(Function(n) n.Contains("L004")).ToArray
            Dim DateFmt() As String = ContentItems.Where(Function(n) n.Contains("L005")).ToArray
            Dim Parameter() As String = ContentItems.Where(Function(n) n.Contains("L006")).ToArray

            If Freq.Length <> 1 Or DateFmt.Length <> 1 Or Plant.Length <> 1 Or Parameter.Length <> 1 Then
                Return -1
            End If

            Dim Freq_() As String = Freq(0).Split(","c)
            Dim Plant_() As String = Plant(0).Split(","c)
            Dim ProdCode_() As String = PCode(0).Split(","c)
            Dim Version_() As String = nVersion(0).Split(","c)
            Dim sFmt() As String = DateFmt(0).Split(","c)
            Dim MrkPrm() As String = Parameter(0).Split(","c)
            Dim MrkPrm_() As String = MrkPrm(2).Split("|"c)

            Dim m_WkCode As String = String.Empty


            Select Case sFmt(2).Trim
                Case Is = "ymd"
                    m_WkCode = String.Format("{0:D2}", WebDate.Year)
                    m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & WebMonth.Substring(WebDate.Month - 1, 1) & WebDay.Substring(WebDate.Day - 1, 1)

                    Try
                        If Not MrkPrm(2).Trim.ToUpper.Contains("|") Then
                            If MrkPrm(2).Trim.ToUpper.Contains("RA") Then
                                If ProdCode_(2).Trim = "-" Then
                                    ProdCode_(2) = ""
                                End If

                                sRetVal = ProdCode_(2).Trim & m_WkCode.Trim & Version_(2).Trim
                            Else
                                If ProdCode_(2).Trim.StartsWith("@") And ProdCode_(2).Trim.EndsWith("@") And ProdCode_.Length >= 3 Then
                                    sRetVal = Freq_(2).Trim & m_WkCode.Trim
                                Else
                                    sRetVal = Freq_(2).Trim & m_WkCode.Trim & Plant_(2).Trim
                                End If
                            End If
                        Else
                            sRetVal = m_WkCode.Trim
                        End If

                    Catch ex As Exception
                        Return -1
                    End Try
                Case Else
                    Dim myCI As New CultureInfo("en-US")
                    Dim myCal As Calendar = myCI.Calendar
                    Dim YrVal As String = String.Format("{0:D4}", WebDate.Year)

                    Try
                        If Not MrkPrm(2).Trim.ToUpper.Contains("|") Then
                            sRetVal = Freq_(2).Trim & YrVal.Substring(3) & String.Format("{0:D2}", myCal.GetWeekOfYear(Now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday)) & Plant_(2).Trim
                        Else
                            sRetVal = YrVal.Substring(3) & String.Format("{0:D2}", myCal.GetWeekOfYear(Now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday))
                        End If
                    Catch ex As Exception
                        Return -1
                    End Try
            End Select

            'ReDim RetData(6)
            Dim addPrm As String = ""

            If MrkPrm(2).Trim.ToUpper.Contains("SG") Then
                addPrm = ",3030"
            ElseIf MrkPrm(2).Trim.ToUpper.Contains("RA") Then
                addPrm = "," & Freq_(2).Trim & MrkPrm(2).Trim.ToUpper.Substring(2, 4) & Plant_(2).Trim
            ElseIf MrkPrm(2).Trim.ToUpper.Contains("|") And MrkPrm_.Length >= 3 Then
                If MrkPrm_(0).Contains("ymd") Then
                    sRetVal = MrkPrm_(0).Trim.Replace("ymd", sRetVal)
                ElseIf MrkPrm_(0).Contains("yww") Then
                    sRetVal = MrkPrm_(0).Trim.Replace("yww", sRetVal)
                End If

                If MrkPrm_(1).Contains("#") Then
                    Dim CntSymbol As Integer = 0
                    Dim CntLot As Integer = GetSerialNoBySpec(Lot_No, SpecNo,
                                                              String.Format("{0:D4}-{1:D2}-{2:D2}", WebDate.Year, WebDate.Month, WebDate.Day),
                                                              String.Format("{0:D4}-{1:D2}-{2:D2}", WebDateAdv.Year, WebDateAdv.Month, WebDateAdv.Day))

                    For idx = 0 To MrkPrm_(1).Length - 1
                        If MrkPrm_(1).Substring(idx, 1) = "#" Then
                            CntSymbol += 1
                        End If
                    Next

                    If CntLot <= 0 Then
                        Return -1
                    Else
                        addPrm = "," & CntLot.ToString.PadLeft(CntSymbol, "0"c) & MrkPrm_(1).Replace("#", "")
                    End If
                Else
                    addPrm = ""
                End If

                MrkPrm(2) = MrkPrm_(2).Trim
            ElseIf ProdCode_(2).Trim.StartsWith("@") And ProdCode_(2).Trim.EndsWith("@") And ProdCode_.Length >= 3 Then
                addPrm = "," & marking2.GetMarkingSequenceFC(Lot_No, SpecNo, String.Format("{0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}", WebDate.Year, WebDate.Month, WebDate.Day, WebDate.Hour, WebDate.Minute, WebDate.Second))
            End If


            'RetData(0) = Lot_No
            'RetData(1) = SpecNo
            'RetData(2) = sRetVal & addPrm   'weekcode always comes before comma
            'RetData(3) = MrkPrm(2).Trim
            'RetData(4) = Freq_(2).Trim
            'RetData(5) = sFmt(2).Trim
            'RetData(6) = Plant_(2).Trim

            Return sRetVal & addPrm
        End With

    End Function

    <WebMethod(Description:="Return A Week Code For FC Package (Extended Version)")> _
    Public Function azWeekCodeEx_FC(ByVal SpecNo As String, ByVal sFormat As String) As String

        Dim sFmt As String = sFormat.ToLower
        Dim WebDate As Date = Now
        Dim sRetVal As String = String.Empty

        Dim WebMonth As String = "123456789XYZ"
        Dim WebDay As String = "123456789ABCDEFGHJKLMNPQRSTUVWXYZ"


        Select Case sFmt
            Case Is = "ymd"
                sRetVal = Right(Trim(Str(Year(WebDate))), 1) & Mid(WebMonth, Month(WebDate), 1) & Mid(WebDay, Day(WebDate), 1)
            Case Is = "ydm"
                sRetVal = Right(Trim(Str(Year(WebDate))), 1) & Mid(WebDay, Day(WebDate), 1) & Mid(WebMonth, Month(WebDate), 1)
            Case Is = "dmy"
                sRetVal = Mid(WebDay, Day(WebDate), 1) & Mid(WebMonth, Month(WebDate), 1) & Right(Trim(Str(Year(WebDate))), 1)
            Case Else
                Dim myCI As New CultureInfo("en-US")
                Dim myCal As Calendar = myCI.Calendar
                Dim YrVal As String = String.Format("{0:D4}", WebDate.Year)

                With My.Computer
                    'Dim SpecFile As String = "c:\FC_MarkCode\MI\" & SpecNo & ".dat"
                    Dim SpecFile As String = "D:\MachineNet\MacDB\Marking\FC\FC-12M\" & SpecNo & ".dat"

                    If Not .FileSystem.FileExists(SpecFile) Then
                        Return ""
                    End If

                    Dim FileContent As String = .FileSystem.ReadAllText(SpecFile)
                    Dim ContentItems() As String = FileContent.Split(vbCr)

                    Dim Freq() As String = ContentItems.Where(Function(n) n.Contains("L001")).ToArray
                    Dim DateFmt() As String = ContentItems.Where(Function(n) n.Contains("L002")).ToArray
                    Dim Plant() As String = ContentItems.Where(Function(n) n.Contains("L003")).ToArray

                    If Freq.Length <> 1 Or DateFmt.Length <> 1 Or Plant.Length <> 1 Then
                        Return ""
                    End If

                    Dim Freq_() As String = Freq(0).Split(","c)
                    Dim Plant_() As String = Plant(0).Split(","c)

                    sRetVal = Freq_(2).Trim & YrVal.Substring(3) & String.Format("{0:D2}", myCal.GetWeekOfYear(Now, CalendarWeekRule.FirstDay, DayOfWeek.Sunday)) & Plant_(2).Trim
                End With
        End Select

        Return sRetVal

    End Function

    <WebMethod(Description:="Save Marking Records into SQL Server... ")> _
    Public Function UpdateRecords(ByVal MarkingRec() As String) As Integer

        Dim MarkRec As Rec = Nothing

        With MarkRec
            .Lot_No = MarkingRec(0)
            .IMI_No = MarkingRec(1)
            .FreqVal = MarkingRec(2)
            .Opt = MarkingRec(3)
            .RecDate = MarkingRec(4)
            .Profile = MarkingRec(5)
            .CtrlNo = MarkingRec(6)
            .MacNo = MarkingRec(7)
            .MData1 = MarkingRec(8)
            .MData2 = MarkingRec(9)
            .MData3 = MarkingRec(10)
            .MData4 = MarkingRec(11)
            .MData5 = MarkingRec(12)
            .MData6 = MarkingRec(13)

            If Not .Lot_No.IndexOf("MARK") < 0 Then
                Return 1
            End If
        End With


        'Normal         : FuncRet = 1
        'SQL Fail       : FuncRet = -1
        'Text Rec Fail  : FuncRet = -11

        Dim FuncRet As Integer = InsertNewRecord_sql(MarkRec) + (SaveTextRec(MarkingRec) * 10)
        Return FuncRet

    End Function

    <WebMethod(Description:="Insert Or Update Marking Records into SQL Server... ")> _
    Public Function AddOrUpdateRecords(ByVal MarkingRec() As String) As Integer

        Dim MarkRec As Rec = Nothing

        With MarkRec
            .Lot_No = MarkingRec(0)
            .IMI_No = MarkingRec(1)
            .FreqVal = MarkingRec(2)
            .Opt = MarkingRec(3)
            .RecDate = MarkingRec(4)
            .Profile = MarkingRec(5)
            .CtrlNo = MarkingRec(6)
            .MacNo = MarkingRec(7)
            .MData1 = MarkingRec(8)
            .MData2 = MarkingRec(9)
            .MData3 = MarkingRec(10)
            .MData4 = MarkingRec(11)
            .MData5 = MarkingRec(12)
            .MData6 = MarkingRec(13)

            If Not .Lot_No.IndexOf("MARK") < 0 Then
                Return 1
            End If
        End With


        'Normal         : FuncRet = 1
        'SQL Fail       : FuncRet = -1
        'Text Rec Fail  : FuncRet = -11

        Dim FuncRet As Integer = InsertOrUpdateRecord_sql(MarkRec) + (SaveTextRec(MarkingRec) * 10)
        Return FuncRet

    End Function

    <WebMethod(Description:="Upload BCC72 Counting Data into SQL Server & NetTerm Directories... ")> _
    Public Function Upload_BCC72_CntData(ByVal rec As String) As Integer

        Dim LotEndPath As String = String.Empty

        LotEndPath = "D:\MachineNet\MacDB\NetTermData\BCC"

        If Not My.Computer.FileSystem.DirectoryExists(LotEndPath) Then
            My.Computer.FileSystem.CreateDirectory(LotEndPath)
        End If

        Dim RecTime As Date = Now
        Dim FileName As String = String.Format("{0:d2}{1:d2}{2:d4}.rec", RecTime.Day, RecTime.Month, RecTime.Year)

        Dim BckUp As Date = DateAdd(DateInterval.Day, 1, RecTime)
        Dim FileName_ As String = String.Format("{0:d2}{1:d2}{2:d4}.rec", BckUp.Day, BckUp.Month, BckUp.Year)

        Try
            With My.Computer.FileSystem
                .WriteAllText(LotEndPath & "\" & FileName, rec, True, System.Text.Encoding.ASCII)
                '.WriteAllText(LotEndPath & "\" & FileName_, rec, True, System.Text.Encoding.ASCII)
            End With
        Catch ex As Exception

        End Try

    End Function

    <WebMethod(Description:="Returns ETMY Marking Code.")> _
    Public Function GetMarkingCode(ByVal Lot_No As String, ByVal MI_Spec As String, ByRef RetData() As String) As Integer
        'for Debug use the following line and then take away argument
        '
        'Dim RetData() As String

        'Dim IMI_Path As String = "D:\MachineNet\MacDB\Marking\PX\IMI"

        Dim Records As Rec = Nothing
        Dim MarkingData As String = String.Empty
        Dim TargetSpec As String = String.Empty
        Dim FuncRet As Integer = 0
        Dim m_Today As Date = Now


        If Not Lot_No.Contains("-") And Lot_No.Length <> 10 Then
            ReDim RetData(0)
            RetData(0) = "Invalid Lot No. !"
            Return -1
        Else
            'TargetSpec = Lot_No.Substring(0, Lot_No.IndexOf("-")).ToUpper.Trim
            'TargetSpec = IIf(Lot_No.Contains("-"), Lot_No.Substring(0, Lot_No.IndexOf("-")).ToUpper.Trim, Lot_No.ToUpper.Trim)

            If Lot_No.ToUpper.IndexOf("MARK") < 0 Then
                If Not CheckDatabase() < 0 Then
                    'If Lot_No.Trim.ToUpper.StartsWith("P") Then
                    'If MI_Spec.Trim.ToUpper.StartsWith("FA") Or MI_Spec.Trim.ToUpper.StartsWith("D") Then
                    '    Dim lprSpec As String = ComData.getLPR_Det(Lot_No)

                    '    If Not lprSpec = "" Then
                    '        Dim cData() = lprSpec.Split(",")

                    '        If cData(0).Trim.ToUpper <> MI_Spec.Trim.ToUpper Then
                    '            ReDim RetData(0)
                    '            RetData(0) = "Invalid IMI No. ..!"
                    '            Return -1
                    '        End If
                    '    End If
                    'End If

                    FuncRet = GetSqlRecords(Lot_No, Records)

                    If FuncRet < 0 Then
                        ReDim RetData(0)
                        RetData(0) = "SQL error!"
                        Return FuncRet
                    ElseIf FuncRet > 0 Then
                        ReDim RetData(13)

                        With Records
                            RetData(0) = .Lot_No
                            RetData(1) = .IMI_No
                            RetData(2) = .FreqVal
                            RetData(3) = .Opt
                            RetData(4) = .RecDate
                            RetData(5) = .Profile
                            RetData(6) = .CtrlNo
                            RetData(7) = .MacNo
                            RetData(8) = .MData1
                            RetData(9) = .MData2
                            RetData(10) = .MData3
                            RetData(11) = .MData4
                            RetData(12) = .MData5
                            RetData(13) = .MData6
                        End With

                        Return FuncRet
                    End If
                End If
            End If
        End If

        'if TargetSpec.StartsWith("P") then
        If MI_Spec.Trim.ToUpper.StartsWith("FA") Or MI_Spec.Trim.ToUpper.StartsWith("D") Then
            '--- Coding To Run For PX Line ---


            'If Lot_No.ToUpper.IndexOf("DMY") < 0 And Lot_No.ToUpper.IndexOf("TEST") < 0 And Lot_No.ToUpper.IndexOf("ZTAN") < 0 Then
            '    'Validate Lot No. And IMI No.
            '    Dim MatchRslt As Integer = ValidateLotNo(Lot_No.Trim, MI_Spec.Trim)

            '    If MatchRslt < 0 Then
            '        ReDim RetData(1)
            '        RetData(0) = " because it is not established for Lot No. : " & Lot_No
            '        RetData(1) = MI_Spec & RetData(0)
            '        Return -1
            '    End If
            'End If


            '--- Testing Location -> Remark this line for runtime ---
            'IMI_Path = "D:\AI\MyVBProject\VB_Project\ML-7111A\PXFA\Data\IMI\ML-7111A"
            Dim IMIFilePath As String = IMI_Path & "\" & MI_Spec & ".dat"
            Dim ALineCode As String = marking2.GetMarkingCode(Lot_No, MI_Spec, String.Format("{0:D4}-{1:D2}-{2:D2} {3:D2}:{4:D2}:{5:D2}", m_Today.Year, m_Today.Month, m_Today.Day, m_Today.Hour, m_Today.Minute, m_Today.Second))

            If My.Computer.FileSystem.FileExists(IMIFilePath) Or Not ALineCode = "" Then
                If ParseSpecData(IMIFilePath, Records) < 0 Then
                    ReDim RetData(0)
                    RetData(0) = "Parse Spec. File Error!"
                    Return -1
                Else
                    Dim FindMatches As New Regex("[^a-zA-Z0-9_.!]")
                    ReDim RetData(13)

                    With Records
                        RetData(0) = Lot_No
                        RetData(1) = MI_Spec
                        RetData(2) = .FreqVal
                        RetData(3) = ""
                        RetData(4) = ""
                        RetData(5) = .Profile
                        RetData(6) = ""
                        RetData(7) = ""
                        RetData(8) = IIf(ALineCode = "", FormMarkCode1(m_Today), ALineCode)        'Freq. & Plant
                        RetData(9) = FormMarkCode2(m_Today)                                        'Weekcode

                        Dim FoundMatch As Match = FindMatches.Match(RetData(8))

                        If FoundMatch.Length > 0 Then
                            ReDim RetData(0)
                            RetData(0) = "Invalid IMI Spec Content!"
                            Return -1
                        End If


                        With IMIDataItem
                            If Val(.sFreq) = 0 And (.sParameter.ToUpper = "TSX-2016H" Or .sParameter.ToUpper.Contains("TCI_F")) Then
                                RetData(10) = IIf(.sVersion = "##", Lot_No.Substring(6, 2), .sVersion)
                                RetData(11) = "["
                                RetData(12) = "-"
                                RetData(13) = "-"
                            Else
                                RetData(10) = "-"
                                RetData(11) = "-"
                                RetData(12) = "-"
                                RetData(13) = "-"
                            End If
                        End With

                        If IMIDataItem.sParameter.ToUpper = "FA-12T" Or IMIDataItem.sParameter.ToUpper = "TSX-2016H" Then
                            RetData(9) = RetData(9) & RetData(8)
                            RetData(8) = "!"
                        End If


                        Dim sp_LotNo As String = "MARK"
                        Dim ChkLotNo As Integer = Lot_No.ToUpper.IndexOf(sp_LotNo, 4)
                        Dim MarkCharType As String = 0
                        Dim Marks As String = String.Empty

                        If Not ChkLotNo < 0 Then
                            Try
                                MarkCharType = Lot_No.Substring(ChkLotNo + sp_LotNo.Length, 1)
                            Catch ex As Exception
                                MarkCharType = 0
                            End Try

                            For iLp As Integer = 0 To 9
                                If IsNumeric(MarkCharType) Then
                                    Dim charc As Integer = Asc(MarkCharType) + iLp
                                    If charc > 57 Then charc -= 10
                                    Marks &= Chr(charc)
                                Else
                                    Dim charc As Integer = Asc(IIf(Lot_No.ToUpper.EndsWith("S"), MarkCharType.ToLower, MarkCharType.ToUpper)) + iLp
                                    If charc > 90 And charc < 98 Then charc -= 26
                                    If charc > 122 Then charc -= 26
                                    Marks &= Chr(charc)
                                End If
                            Next

                            RetData(8) = Marks.Substring(0, 5)
                            RetData(9) = Marks.Substring(5)
                        End If
                    End With
                End If
            Else
                ReDim RetData(1)
                RetData(0) = "Spec. File Not Found!"
                RetData(1) = IMIFilePath
                Return -1
            End If

        Else
            ReDim RetData(2)
            MarkingData = "A" & FormWeekCode(m_Today, "yww") & "L"
            RetData(0) = Lot_No
            RetData(1) = MI_Spec
            RetData(2) = MarkingData
        End If

        Return RetData.GetUpperBound(0)

    End Function

    <WebMethod(Description:="Return A Marking Code For SD Package (Advance Version)")> _
    Public Function az_SDMarking_ad(ByVal Lot_No As String, ByVal SpecNo As String, ByRef RetData() As String) As Integer

        Dim WebDate As Date = Now
        Dim sRetVal As String = String.Empty

        Dim WebMonth As String = "123456789XYZ"
        Dim WebDay As String = "123456789ABCDEFGHJKLMNOPQRSTUVWXYZ"

        Dim _SpecNo() As String = SpecNo.Split(";")

        With My.Computer
            Dim SpecFile As String = "D:\MachineNet\MacDB\Marking\SD\IMI\" & _SpecNo(0).Trim & ".dat"

            If Not .FileSystem.FileExists(SpecFile) Then
                Return -1
            End If

            Dim FileContent As String = .FileSystem.ReadAllText(SpecFile)
            Dim ContentItems() As String = FileContent.Split(vbCr)

            Dim Freq() As String = ContentItems.Where(Function(n) n.Contains("L001")).ToArray
            Dim Plant() As String = ContentItems.Where(Function(n) n.Contains("L002")).ToArray
            Dim ProdCode() As String = ContentItems.Where(Function(n) n.Contains("L003")).ToArray
            Dim Version() As String = ContentItems.Where(Function(n) n.Contains("L004")).ToArray
            Dim DateFmt() As String = ContentItems.Where(Function(n) n.Contains("L005")).ToArray
            Dim Parameter() As String = ContentItems.Where(Function(n) n.Contains("L006")).ToArray

            If Freq.Length <> 1 Or ProdCode.Length <> 1 Or Version.Length <> 1 Or DateFmt.Length <> 1 Or Plant.Length <> 1 Or Parameter.Length <> 1 Then
                Return -1
            End If


            Dim Freq_() As String = Freq(0).Split(","c)
            Dim Prod_() As String = ProdCode(0).Split(","c)
            Dim Ver_() As String = Version(0).Split(","c)
            Dim Plant_() As String = Plant(0).Split(","c)
            Dim sFmt() As String = DateFmt(0).Split(","c)
            Dim MrkPrm() As String = Parameter(0).Split(","c)

            Dim m_WkCode As String = String.Empty
            ReDim RetData(13)

            RetData(0) = Lot_No
            RetData(1) = _SpecNo(0).Trim
            RetData(2) = Freq_(2)
            RetData(3) = ""
            RetData(4) = ""
            RetData(5) = MrkPrm(2)
            RetData(6) = ""
            RetData(7) = ""
            RetData(8) = Prod_(2)
            RetData(9) = _SpecNo(1).Trim
            RetData(10) = "E"
            RetData(11) = "-"
            RetData(12) = "-"
            RetData(13) = "-"

            'C,I,J,K,L,M
            Dim Distintion() As String = {"C", "I", "J", "K", "L", "M", "S"}
            Dim _freq() As String = {"46.5", "50.3", "54.1", "123", "113", "133", "49.6", "51.0", "53.6", "56.0"}
            Dim _code() As String = {"A", "B", "C", "D", "E", "F", "H", "J", "L", "M"}

            Dim WkCdStart As String = _SpecNo(1).Trim.Substring(0, 1)
            Dim WkCdEnd As String = _SpecNo(1).Trim.Substring(_SpecNo(1).Trim.Length - 1, 1)

            Dim _freqIdx As Integer = Array.IndexOf(_freq, Freq_(2).Trim)
            Dim _distintion As Integer = Array.IndexOf(Distintion, WkCdEnd)

            If _freqIdx < 0 Or _distintion < 0 OrElse WkCdStart <> _code(_freqIdx) Or WkCdStart <> Ver_(2).Replace("!", "").Trim Then
                If Ver_(2).Trim.StartsWith("!") Then
                    If WkCdStart = Ver_(2).Replace("!", "").Trim Then
                        Return RetData.Length
                    Else
                        RetData(0) = "Week Code invalid...!"
                        Return -1
                    End If
                Else
                    RetData(0) = "Week Code invalid...!"
                    Return -1
                End If
            Else
                Return RetData.Length
            End If

        End With

    End Function

    Public Function ValidateLotNo(ByVal sLotNo As String, ByVal sSpecNo As String) As Integer

        With My.Computer
            Dim aa As System.Collections.ObjectModel.ReadOnlyCollection(Of String) = My.Computer.FileSystem.FindInFiles(NetPath, sLotNo, True, FileIO.SearchOption.SearchAllSubDirectories)


            If aa.Count > 0 Then
                For iLp As Integer = 0 To aa.Count - 1
                    'Application.DoEvents()

                    Dim DBContent As String = My.Computer.FileSystem.ReadAllText(aa(iLp)).Replace(Chr(34), "")
                    Dim Records() As String = DBContent.Split(New Char() {vbCrLf, vbCr})

                    Dim WordsToMatch() As String = {sLotNo, sSpecNo}
                    Dim Qry = From Record In Records Let w = Record.Split(",") Where w.Distinct().Intersect(WordsToMatch).Count = WordsToMatch.Count Select Record

                    If Qry.Count > 0 Then
                        For Each Str As String In Qry
                            Dim RecItems() As String = Str.Split(","c)

                            If RecItems.Length > 0 Then
                                If Val(RecItems(9)) <= 40 Then Return 1
                            End If
                        Next
                    Else
                        'MessageBox.Show("No Match were found !!!")
                    End If
                Next

                Return 1
            Else
                'Ignore the Lot that not exist in database.
                Return 1
            End If

            Return -1
        End With

    End Function

    Private Function EvtLog(ByVal msg As String) As Integer

        Try
            Dim LogDate As Date = Now
            Dim FuncRet As Integer = 0
            Dim LogRec_Path As String = "D:\MachineNet\MacDB\Marking\LM\Log"
            Dim LogFilePath As String = LogRec_Path & "\" & String.Format("{0:D2}{1:D2}{2:D4}.dat", LogDate.Day, LogDate.Month, LogDate.Year)
            Dim LogMsg = LogDate.ToShortDateString & " " & LogDate.ToShortTimeString & vbTab & msg & vbCrLf

            Try
                If Not My.Computer.FileSystem.DirectoryExists(LogRec_Path) Then
                    My.Computer.FileSystem.CreateDirectory(LogRec_Path)
                End If
            Catch ex As Exception
                '
            End Try

            My.Computer.FileSystem.WriteAllText(LogFilePath, LogMsg & vbCrLf, True, System.Text.Encoding.ASCII)
        Catch ex As Exception
            '
        End Try

    End Function

    Private Function SaveTextRec(ByVal MarkingRec() As String) As Integer

        Dim MarkingDate As Date
        Dim MarkRec As Rec = Nothing
        Dim FuncRet As Integer = 0
        Dim TextRec_Path As String = "D:\MachineNet\MacDB\Marking\LM\Data"


        With MarkRec
            .Lot_No = MarkingRec(0)
            .IMI_No = MarkingRec(1)
            .FreqVal = MarkingRec(2)
            .Opt = MarkingRec(3)
            .RecDate = MarkingRec(4)
            .Profile = MarkingRec(5)
            .CtrlNo = MarkingRec(6)
            .MacNo = MarkingRec(7)
            .MData1 = MarkingRec(8)
            .MData2 = MarkingRec(9)
            .MData3 = MarkingRec(10)
            .MData4 = MarkingRec(11)
            .MData5 = MarkingRec(12)
            .MData6 = MarkingRec(13)


            Dim Pmf As ParameterProfile = Nothing
            Dim DotMark As Integer = GetProfilesFromServer(.CtrlNo, .Profile, Pmf)
            Dim LabelNoDot As Boolean = False

            If .Profile = "TSX-2016H" Or (.Profile.ToUpper.StartsWith("X") And .IMI_No.ToUpper.StartsWith("U")) Or .IMI_No.Length = 12 Then
                LabelNoDot = True
            End If

            Dim RecDate_Date As String = .RecDate.Substring(0, .RecDate.IndexOf(" "))
            Dim RecDate_Time As String = .RecDate.Substring(.RecDate.IndexOf(" ") + 1)
            Dim SysDateFormat As String = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern.ToLower
            Dim ParseDate As String = String.Empty

            If SysDateFormat = "dd/mm/yyyy" Then
                Try
                    ParseDate = RecDate_Date.Substring(RecDate_Date.IndexOf("-") + 1, 2) & "-" & _
                                RecDate_Date.Substring(0, 2) & "-" & _
                                RecDate_Date.Substring(RecDate_Date.LastIndexOf("-") + 1)
                Catch ex As Exception
                    ParseDate = ""
                End Try

                If Not ParseDate = "" Then
                    RecDate_Date = ParseDate

                    Try
                        MarkingDate = Format(Convert.ToDateTime(RecDate_Date), System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)
                    Catch ex As Exception
                    End Try
                End If
            Else
                Try
                    ParseDate = RecDate_Date.Substring(RecDate_Date.IndexOf("-") + 1, 2) & "-" & _
                                RecDate_Date.Substring(0, 2) & "-" & _
                                RecDate_Date.Substring(RecDate_Date.LastIndexOf("-") + 1)
                Catch ex As Exception
                    ParseDate = ""
                End Try

                If Not ParseDate = "" Then
                    RecDate_Date = ParseDate

                    Try
                        MarkingDate = Format(Convert.ToDateTime(RecDate_Date), System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern)
                    Catch ex As Exception
                    End Try
                End If
            End If


            Dim DateRecSaved As Date = Now
            Dim TextFilePath As String = TextRec_Path & "\" & String.Format("{0:D2}{1:D2}{2:D4}.dat", DateRecSaved.Day, DateRecSaved.Month, DateRecSaved.Year)

            If Not My.Computer.FileSystem.DirectoryExists(TextRec_Path) Then
                My.Computer.FileSystem.CreateDirectory(TextRec_Path)
            End If

            'Prevent generate duplicated record
            Try
                Dim CurMarkRec As String = My.Computer.FileSystem.ReadAllText(TextFilePath, System.Text.Encoding.ASCII)

                If Not CurMarkRec.IndexOf(.Lot_No) < 0 Then
                    'Return FuncRet
                End If
            Catch ex As Exception
                'NOP -> Resume
            End Try


            'Temp. Overwrite new format
            Dim MachineList() As String = {"", "", "M02446", "", "", "", "", "", "", "M02086", ""}
            Dim MachineNo As String = Array.IndexOf(MachineList, .CtrlNo).ToString.Trim

            If .MData1.Contains("!") Then .MData1 = "P"

            .Profile = "0"
            .CtrlNo = MachineNo
            .Opt = IIf(.Opt = "", "PX Line Opt.", .Opt)

            'Create new record
            Dim ProfileIndex As String = IIf(.Profile.IndexOf("238") < 0, "0", "1")
            Dim SaveString As String = .IMI_No.Trim & "," & _
                                        .Lot_No.Trim & "," & _
                                        String.Format("{0:F6} MHz", CType(.FreqVal, Decimal)) & "," & _
                                        IIf(Pmf.UseDot = "1", IIf(LabelNoDot = True, "", "."), "") & .MData2.Trim & "," & _
                                        .MData1.Trim & "," & _
                                        .Opt & "," & _
                                        RecDate_Date & "," & _
                                        RecDate_Time & "," & _
                                        IIf(.CtrlNo.IndexOf("M_GKL_LM") < 0, .Profile, ProfileIndex) & "," & _
                                        IIf(.CtrlNo.IndexOf("M_GKL_LM") < 0, .CtrlNo, .CtrlNo.Replace("M_GKL_LM", "")) & vbCrLf
            '.MacNo & vbCrLf

            'Update record file
            Try
                My.Computer.FileSystem.WriteAllText(TextFilePath, SaveString, True, System.Text.Encoding.ASCII)
            Catch ex As Exception
                FuncRet = -1
            End Try

            Return FuncRet
        End With

    End Function

    Private Function FormMarkCode1(ByVal MarkingDate As Date) As String

        Dim MarkData As String = String.Empty


        With IMIDataItem
            Try
                If .sVersion.Length > 2 Then
                    MarkData = .sVersion
                Else
                    If .sPlant.Length > 1 Then
                        If Val(.sFreq) = 0 Then
                            If .sParameter.ToUpper = "TSX-2016H" Or .sParameter.ToUpper.Contains("TCI_F") Then
                                MarkData = .sPlant
                            Else
                                MarkData = .sVersion
                            End If
                        Else
                            If .sPlant.Contains("#") Then
                                Dim chByte() As Char = .sPlant.ToCharArray

                                For ilp As Integer = 0 To chByte.GetUpperBound(0)
                                    If chByte(ilp) = "#" Then
                                        chByte(ilp) = .sFreq.Replace(".", "").Substring(ilp, 1)
                                    End If
                                Next

                                MarkData = chByte
                            Else
                                MarkData = .sFreq.Replace(".", "").Substring(0, 5 - 1) & .sPlant
                            End If
                        End If
                    Else
                        If Val(.sFreq) = 0 Then
                            MarkData = .sVersion
                        Else
                            If (.sProdCode.ToUpper.StartsWith("R") And .sProdCode.ToUpper.Contains(".")) Then
                                Dim fr As Decimal = Decimal.Parse(.sFreq.Trim)
                                MarkData = String.Format("{0:R 00.000}", fr).Substring(0, 7)
                            Else
                                MarkData = .sFreq.Replace(".", "").Substring(0, 5 - .sPlant.Length) & .sPlant
                            End If
                        End If
                    End If
                End If

                If .sParameter.ToUpper = "FA-12T" Then
                    MarkData = .sProdCode
                End If
            Catch ex As Exception
                MarkData = ""
            End Try
        End With

        Return MarkData

    End Function

    Private Function FormMarkCode2(ByVal MarkingDate As Date) As String

        Dim MarkData As String = String.Empty


        Try
            With IMIDataItem
                If .sWkCdFormat.Contains("=") Then
                    Dim WeekCodeFmt() As String = .sWkCdFormat.Split("="c)

                    If Not WeekCodeFmt.Length <= 0 Then
                        .sWkCdFormat = WeekCodeFmt(0)
                        MarkData = WeekCodeFmt(1)
                        Return MarkData
                    Else
                        MarkData = ""
                    End If
                End If

                If .sVersion.Length > 2 Then
                    If .sProdCode = "T" Or .sProdCode = "E" Then
                        MarkData = .sProdCode & " " & FormWeekCode(MarkingDate, .sWkCdFormat)
                    Else
                        MarkData = .sProdCode & FormWeekCode(MarkingDate, .sWkCdFormat)
                    End If
                Else
                    If .sVersion = "_" Then
                        MarkData = .sPlant & FormWeekCode(MarkingDate, .sWkCdFormat)
                    ElseIf .sVersion = "!" Or .sVersion = "-" Or .sVersion = "##" Then
                        MarkData = FormWeekCode(MarkingDate, .sWkCdFormat)
                    ElseIf .sParameter.ToUpper = "FA-128P" Then
                        MarkData = .sPlant & FormWeekCode(MarkingDate, .sWkCdFormat)
                    Else
                        If .sParameter.ToUpper = "TSX-2016H" Or .sParameter.ToUpper.Contains("TCI_F") Then
                            MarkData = FormWeekCode(MarkingDate, .sWkCdFormat)
                        Else
                            MarkData = .sProdCode & FormWeekCode(MarkingDate, .sWkCdFormat) & .sVersion
                        End If
                    End If
                End If

                If .sParameter.ToUpper = "FA-12T" Then
                    MarkData = FormWeekCode(MarkingDate, .sWkCdFormat)
                End If
            End With
        Catch ex As Exception
            MarkData = ""
        End Try

        Return MarkData

    End Function

    Private Function ParseSpecData(ByVal SpecFile As String, ByRef ParseData As Rec) As Integer

        Dim FuncRet As Integer = 0
        Dim FileDataItem As String = My.Computer.FileSystem.ReadAllText(SpecFile, System.Text.Encoding.ASCII)

        If FileDataItem = "" Then Return -1
        Dim DataItems() As String = FileDataItem.Split(vbCrLf)

        If DataItems.GetUpperBound(0) < 6 Then Return -1
        Dim Record() As String = {}

        With IMIDataItem
            Try
                Record = DataItems(0).Split(",")
                .sFreq = Record(2).Trim

                If IsNumeric(Val(.sFreq)) And Not Val(.sFreq) < 0 Then
                    Dim dFreq As Decimal = Val(.sFreq)
                    .sFreq = String.Format("{0:F6}", dFreq)
                Else
                    Return -1
                End If

                ParseData.FreqVal = .sFreq

                Record = DataItems(1).Split(",")
                .sPlant = Record(2).Trim

                Record = DataItems(2).Split(",")
                .sProdCode = Record(2).Trim

                Record = DataItems(3).Split(",")
                .sVersion = Record(2).Trim

                Record = DataItems(4).Split(",")
                .sWkCdFormat = Record(2).Trim

                Record = DataItems(5).Split(",")
                .sParameter = Record(2).Trim

                'If .sParameter.ToLower = "tci_format" Then
                '    Return -1
                'End If

                Record = DataItems(6).Split(",")
                .sFormat = Record(2).Trim
                ParseData.Profile = .sParameter & "," & .sFormat
            Catch ex As Exception
                FuncRet = -1
            End Try
        End With

        Return FuncRet

    End Function

    Private Function FormWeekCode(ByVal MarkingDate As Date, Optional ByVal strFormat As String = "ymd") As String

        Dim m_Format As String = strFormat.ToLower.Trim
        Dim m_WkCode As String = String.Empty
        Dim m_Today As Date = MarkingDate

        Dim Year_D As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim Month_D As String = "123456789XYZ"
        Dim Day_D As String = "123456789ABCDEFGHJKLMNOPQRSTUVWXYZ"
        Dim Day_Dx8 As String = "11111118888888FFFFFFFNNNNNNNUUUXYZ"
        Dim Day_D_ As String = "123456789ABCDEFGHJKLMNPQRSTUVWXYZ"
        Dim WkNoCd As String = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ"

        Dim myCI As New CultureInfo("en-US")
        Dim myCal As Calendar = myCI.Calendar


        Select Case m_Format
            Case Is = "ymd"
                m_WkCode = String.Format("{0:D2}", m_Today.Year)

                With IMIDataItem
                    If .sParameter.ToUpper.Contains("TCI_F") Then
                        If .sProdCode.ToUpper = "TSX2520H" Then
                            m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_D.Substring(m_Today.Day - 1, 1)
                        Else
                            m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_D_.Substring(m_Today.Day - 1, 1)
                        End If
                    Else
                        m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_D.Substring(m_Today.Day - 1, 1)
                    End If
                End With
            Case Is = "ymw"
                m_WkCode = String.Format("{0:D2}", m_Today.Year)

                With IMIDataItem
                    If .sParameter.ToUpper.Contains("TCI_F") Then
                        If .sProdCode.ToUpper = "TSX2520H" Then
                            m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_Dx8.Substring(m_Today.Day - 1, 1)
                        Else
                            m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_D_.Substring(m_Today.Day - 1, 1)
                        End If
                    Else
                        m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_Dx8.Substring(m_Today.Day - 1, 1)
                    End If
                End With
            Case Is = "amd"
                m_WkCode = "A"

                With IMIDataItem
                    If .sParameter.ToUpper.Contains("TCI_F") Then
                        If .sProdCode.ToUpper = "TSX2520H" Then
                            m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_D.Substring(m_Today.Day - 1, 1)
                        Else
                            m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_D_.Substring(m_Today.Day - 1, 1)
                        End If
                    Else
                        m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & Day_D.Substring(m_Today.Day - 1, 1)
                    End If
                End With
            Case Is = "ymdd"
                m_WkCode = String.Format("{0:D2}", m_Today.Year)
                m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & Month_D.Substring(m_Today.Month - 1, 1) & String.Format("{0:D2}", m_Today.Day)
            Case Is = "md"
                With IMIDataItem
                    If .sParameter.ToUpper = "TSX-2016H" Or .sParameter.ToUpper.Contains("TCI_F") Then
                        If .sProdCode.ToUpper = "TSX2520H" Then
                            m_WkCode = Month_D.Substring(m_Today.Month - 1, 1) & Day_D.Substring(m_Today.Day - 1, 1)
                        Else
                            m_WkCode = Month_D.Substring(m_Today.Month - 1, 1) & Day_D_.Substring(m_Today.Day - 1, 1)
                        End If
                    Else
                        m_WkCode = Month_D.Substring(m_Today.Month - 1, 1) & Day_D.Substring(m_Today.Day - 1, 1)
                    End If
                End With
            Case Is = "yw"
                Dim m_WeekNo As Integer = myCal.GetWeekOfYear(m_Today, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday)

                If m_WeekNo > 50 Then
                    If m_Today.Month = 1 Then
                        m_WkCode = Year_D.Substring(m_Today.Year - 2001 - 1, 1) & Year_D.Substring(Abs((m_WeekNo + 0.1) / 2) - 1, 1)
                    Else
                        m_WkCode = Year_D.Substring(m_Today.Year - 2001, 1) & "Z"
                    End If
                Else
                    m_WkCode = Year_D.Substring(m_Today.Year - 2001, 1) & Year_D.Substring(Abs((m_WeekNo + 0.1) / 2) - 1, 1)
                End If
            Case Is = "yww"
                m_WkCode = String.Format("{0:D2}", m_Today.Year)
                m_WkCode = m_WkCode.Substring(m_WkCode.Length - 1) & String.Format("{0:D2}", myCal.GetWeekOfYear(m_Today, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
            Case Is = "ww"
                Dim YearStart As Integer = 2014
                Dim StartCode As String = "FD"

                Dim DiffYrs As Integer = m_Today.Year - Val(YearStart)
                m_WkCode = String.Format("{0:D2}", myCal.GetWeekOfYear(m_Today, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday))

                If m_WkCode > 50 And m_Today.Month = 1 Then DiffYrs = 0

                Do Until DiffYrs = 0
                    Dim prvYrsWeekNo As Integer = myCal.GetWeekOfYear(myCal.AddDays(m_Today, myCal.GetDayOfYear(m_Today) * -1), CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday)

                    Dim NextWkNoCd As String = WkNoCd.Substring(WkNoCd.IndexOf(StartCode.Substring(StartCode.Length - (StartCode.Length - 1))))
                    Dim Next_WkCode As String = String.Empty

                    If NextWkNoCd.Length >= Val(prvYrsWeekNo) Then
                        Next_WkCode = NextWkNoCd.Substring(Val(prvYrsWeekNo), 1)
                        Next_WkCode = StartCode.Substring(0, StartCode.Length - 1) & Next_WkCode
                    Else
                        Dim WkNoCd_Diff As Integer = Val(prvYrsWeekNo) - NextWkNoCd.Length
                        Dim WkNoCdMajor As Integer = WkNoCd.IndexOf(StartCode.Substring(0, StartCode.Length - 1)) + ((WkNoCd_Diff \ 53) + 1)
                        WkNoCdMajor += WkNoCd_Diff \ WkNoCd.Length

                        Next_WkCode = WkNoCd.Substring(WkNoCdMajor, 1) & WkNoCd.Substring((WkNoCd_Diff Mod WkNoCd.Length), 1)
                    End If

                    YearStart = m_Today.Year
                    StartCode = Next_WkCode

                    If m_WkCode >= prvYrsWeekNo And myCal.GetWeekOfYear(m_Today, CalendarWeekRule.FirstDay, DayOfWeek.Monday) = 1 Then
                        m_WkCode = 0
                    End If

                    DiffYrs -= 1
                Loop


                Dim TrimWkNoCd As String = WkNoCd.Substring(WkNoCd.IndexOf(StartCode.Substring(StartCode.Length - (StartCode.Length - 1))))
                Dim tmp_WkCode As String = String.Empty

                If TrimWkNoCd.Length >= Val(m_WkCode) Then
                    tmp_WkCode = TrimWkNoCd.Substring(Val(m_WkCode) - 1, 1)
                    tmp_WkCode = StartCode.Substring(0, StartCode.Length - 1) & tmp_WkCode
                Else
                    Dim WkNoCd_Diff As Integer = Val(m_WkCode) - TrimWkNoCd.Length
                    Dim WkNoCdMajor As Integer = WkNoCd.IndexOf(StartCode.Substring(0, StartCode.Length - 1)) + ((WkNoCd_Diff \ 53) + 1)
                    WkNoCdMajor += WkNoCd_Diff \ WkNoCd.Length

                    tmp_WkCode = WkNoCd.Substring(WkNoCdMajor, 1) & WkNoCd.Substring((WkNoCd_Diff Mod WkNoCd.Length) - 1, 1)
                End If

                m_WkCode = tmp_WkCode
            Case Else
                m_WkCode = strFormat.ToUpper
        End Select

        Return m_WkCode

    End Function

    Private Function CheckDatabase() As Integer

        Dim RetVal As Integer = 0


        If SQL = 1 Then
            Dim sConnStr As String = _
                "SERVER=" & sqlServer & "; " & _
                "DataBase=" & "; " & _
                "uid=" & sqluid & "; " & _
                "pwd=" & sqlpwd
            '"Integrated Security=SSPI"

            Dim dbConnection As New SqlConnection(sConnStr)
            Dim ch As Char = ChrW(39)
            Dim strSQL As String = _
                "IF NOT EXISTS (SELECT * FROM Sys.DATABASES WHERE Name='" & _
                sqlName & "') " & _
                "CREATE DATABASE [" & sqlName & "]"

            Try
                dbConnection.Open()

                Dim cmd As New SqlCommand(strSQL, dbConnection)
                cmd.ExecuteNonQuery()
            Catch sqlExc As SqlException
                RetVal = -1
            End Try

            dbConnection.Close()
        Else
            If odbcServer.ToLower.Trim = "local" Then
                If Not My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "\" & odbcName) Then
                    RetVal = -1
                End If
            Else
                If Not My.Computer.FileSystem.FileExists(odbcServer & "\" & odbcName) Then
                    RetVal = -1
                End If
            End If
        End If

        Return RetVal

    End Function

    Private Function GetSqlRecords(ByVal Lot_No As String, ByRef RecData As Rec) As Integer

        Dim CreateTblString As String = String.Empty


        CreateTblString = "[Lot_No] [nvarchar](20) NOT NULL CONSTRAINT [DF_Records_Lot_No]  DEFAULT (N'-')," & _
                        "[IMI_No] [nvarchar](20) NOT NULL CONSTRAINT [DF_Records_IMI_No]  DEFAULT (N'-')," & _
                        "[FreqVal] [nvarchar](16) NOT NULL CONSTRAINT [DF_Records_FreqVal]  DEFAULT (N'-')," & _
                        "[Opt] [nvarchar](8) NOT NULL CONSTRAINT [DF_Records_Opt]  DEFAULT (N'-')," & _
                        "[RecDate] [datetime] NOT NULL," & _
                        "[Profile] [nvarchar](12) NOT NULL CONSTRAINT [DF_Records_Profile]  DEFAULT (N'-')," & _
                        "[CtrlNo] [nvarchar](12) NOT NULL CONSTRAINT [DF_Records_CtrlNo]  DEFAULT (N'-')," & _
                        "[MacNo] [nvarchar](2) NOT NULL CONSTRAINT [DF_Records_MacNo]  DEFAULT (N'-')," & _
                        "[MData1] [nvarchar](8) NOT NULL CONSTRAINT [DF_Records_MData1]  DEFAULT (N'-')," & _
                        "[MData2] [nvarchar](8) NOT NULL CONSTRAINT [DF_Records_MData2]  DEFAULT (N'-')," & _
                        "[MData3] [nvarchar](8) NOT NULL CONSTRAINT [DF_Records_MData3]  DEFAULT (N'-')," & _
                        "[MData4] [nvarchar](8) NOT NULL CONSTRAINT [DF_Records_MData4]  DEFAULT (N'-')," & _
                        "[MData5] [nvarchar](8) NOT NULL CONSTRAINT [DF_Records_MData5]  DEFAULT (N'-')," & _
                        "[MData6] [nvarchar](8) NOT NULL CONSTRAINT [DF_Records_MData6]  DEFAULT (N'-')"

        If Not Check_dboTables("Records", CreateTblString) < 0 Then
            Return GetRecordsFromServer(Lot_No, RecData)
        Else
            Return -1
        End If

    End Function

    Private Function GetRecordsFromServer(ByVal Lot_No As String, ByRef RecData As Rec) As Integer

        Dim RetVal As Integer = 0
        Dim sConnStr As String = _
            "SERVER=" & sqlServer & "; " & _
            "DataBase=" & sqlName & "; " & _
            "uid=" & sqluid & ";" & _
            "pwd=" & sqlpwd
        '"Integrated Security=SSPI"

        Dim dbConnection As New SqlConnection(sConnStr)
        Dim ch As Char = ChrW(39)
        Dim strSQL As String = _
            "SELECT * FROM Records WHERE Lot_No='" & Lot_No & "' " & _
            "ORDER BY Lot_No"

        Try
            dbConnection.Open()

            Dim cmd As New SqlCommand(strSQL, dbConnection)
            cmd.ExecuteNonQuery()

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()

            With sqlReader
                Dim iFieldCnt As Integer = .FieldCount
                Dim iRecNo As Integer = 0

                If .HasRows Then
                    Dim sRetData(iFieldCnt - 1) As String

                    Do While .Read()
                        With RecData
                            .Lot_No = sqlReader.GetString(0)
                            .IMI_No = sqlReader.GetString(1)
                            .FreqVal = sqlReader.GetString(2)
                            .Opt = sqlReader.GetString(3)
                            .RecDate = sqlReader.GetDateTime(4).ToString
                            .Profile = sqlReader.GetString(5)
                            .CtrlNo = sqlReader.GetString(6)
                            .MacNo = sqlReader.GetString(7)
                            .MData1 = sqlReader.GetString(8)
                            .MData2 = sqlReader.GetString(9)
                            .MData3 = sqlReader.GetString(10)
                            .MData4 = sqlReader.GetString(11)
                            .MData5 = sqlReader.GetString(12)
                            .MData6 = sqlReader.GetString(13)
                        End With

                        iRecNo += 1
                    Loop

                    RetVal = iRecNo
                Else
                    RetVal = 0
                End If
            End With
        Catch sqlExc As SqlException
            RetVal = 0
        End Try

        dbConnection.Close()
        Return RetVal

    End Function

    Private Function Check_dboTables(ByVal TableName As String, ByVal CreateTblStr As String) As Integer

        Dim RetVal As Integer = 0
        Dim sConnStr As String = _
                "SERVER=" & sqlServer & "; " & _
                "DataBase=" & "; " & _
                "uid=" & sqluid & "; " & _
                "pwd=" & sqlpwd
        '"Integrated Security=SSPI"

        Dim dbConnection As New SqlConnection(sConnStr)
        Dim ch As Char = ChrW(39)
        Dim strSQL As String = _
            "USE [" & sqlName & "]" & vbCrLf & _
            "IF NOT EXISTS (SELECT * FROM sys.objects " & _
            "WHERE object_id=OBJECT_ID(N'[dbo].[" & TableName & "]') AND type in (N'U')) " & _
            "CREATE Table [" & TableName & "] (" & _
            CreateTblStr & ")"

        Try
            dbConnection.Open()

            Dim cmd As New SqlCommand(strSQL, dbConnection)
            cmd.ExecuteNonQuery()
        Catch sqlExc As SqlException
            RetVal = -1
        End Try

        dbConnection.Close()
        Return RetVal

    End Function

    Private Function InsertNewRecord_sql(ByVal NewRecData As Rec) As Integer

        '
        Dim RetVal As Integer = 0
        Dim sConnStr As String = _
                "SERVER=" & sqlServer & "; " & _
                "DataBase=" & sqlName & "; " & _
                "uid=" & sqluid & "; " & _
                "pwd=" & sqlpwd
        '"Integrated Security=SSPI"

        Dim dbConnection As New SqlConnection(sConnStr)
        Dim ch As Char = ChrW(39)
        Dim strSQL As String = String.Empty


        '--- Add to insert dummy data ---
        'Dim NewRecData As Rec

        'With NewRecData
        '    .Lot_No = "PAZ-00001"
        '    .IMI_No = "D0110001"
        '    .FreqVal = "--.------"
        '    .Opt = "S1609"
        '    .RecDate = String.Format("{0:D2}-{1:D2}-{2:D4} {3:D2}:{4:D2}:{5:D2}", Now.Month, Now.Day, Now.Year, Now.Hour, Now.Minute, Now.Second)
        '    .Profile = "TSX"
        '    .CtrlNo = "M00000"
        '    .MacNo = "0"
        '    .MData1 = "5888"
        '    .MData2 = "Tymdd"
        '    .MData3 = "-"
        '    .MData4 = "-"
        '    .MData5 = "-"
        '    .MData6 = "-"
        'End With

        'FuncRet = InsertNewProfile_sql(Records)


        With NewRecData
            'Dim _opt As String = IIf(.Opt.Length > 8, .Opt.Substring(0, 8).ToUpper.Trim, .Opt.ToUpper.Trim)

            If .Opt.Length > 8 Then
                .Opt = .Opt.Substring(0, 8)
            End If

            strSQL = "IF NOT EXISTS (SELECT * FROM Records WHERE Lot_No='" & .Lot_No & "' AND IMI_No='" & .IMI_No & "') INSERT INTO Records " & _
                "(Lot_No, IMI_No, FreqVal, Opt, RecDate, [Profile], CtrlNo, MacNo, MData1, MData2, MData3, MData4, MData5, MData6) VALUES (" & _
                ch & .Lot_No & ch & ", " & _
                ch & .IMI_No & ch & ", " & _
                ch & .FreqVal & ch & ", " & _
                ch & .Opt & ch & ", " & _
                "GETDATE()" & ", " & _
                ch & .Profile & ch & ", " & _
                ch & .CtrlNo & ch & ", " & _
                ch & .MacNo & ch & ", " & _
                ch & .MData1 & ch & ", " & _
                ch & .MData2 & ch & ", " & _
                ch & .MData3 & ch & ", " & _
                ch & .MData4 & ch & ", " & _
                ch & .MData5 & ch & ", " & _
                ch & .MData6 & ch & ") "
        End With

        Try
            dbConnection.Open()

            Dim cmd As New SqlCommand(strSQL, dbConnection)
            'cmd.ExecuteNonQuery()

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()
            RetVal = sqlReader.RecordsAffected
        Catch sqlExc As SqlException
            EvtLog(strSQL & " -> " & sqlExc.Message)
            RetVal = -1
        End Try

        dbConnection.Close()
        Return RetVal

    End Function

    Private Function InsertOrUpdateRecord_sql(ByVal NewRecData As Rec) As Integer

        '
        Dim RetVal As Integer = 0
        Dim sConnStr As String = _
                "SERVER=" & sqlServer & "; " & _
                "DataBase=" & sqlName & "; " & _
                "uid=" & sqluid & "; " & _
                "pwd=" & sqlpwd
        '"Integrated Security=SSPI"

        Dim dbConnection As New SqlConnection(sConnStr)
        Dim ch As Char = ChrW(39)
        Dim strSQL As String = String.Empty


        '--- Add to insert dummy data ---
        'Dim NewRecData As Rec

        'With NewRecData
        '    .Lot_No = "PAZ-00001"
        '    .IMI_No = "D0110001"
        '    .FreqVal = "--.------"
        '    .Opt = "S1609"
        '    .RecDate = String.Format("{0:D2}-{1:D2}-{2:D4} {3:D2}:{4:D2}:{5:D2}", Now.Month, Now.Day, Now.Year, Now.Hour, Now.Minute, Now.Second)
        '    .Profile = "TSX"
        '    .CtrlNo = "M00000"
        '    .MacNo = "0"
        '    .MData1 = "5888"
        '    .MData2 = "Tymdd"
        '    .MData3 = "-"
        '    .MData4 = "-"
        '    .MData5 = "-"
        '    .MData6 = "-"
        'End With

        'FuncRet = InsertNewProfile_sql(Records)


        With NewRecData
            'Dim _opt As String = IIf(.Opt.Length > 8, .Opt.Substring(0, 8).ToUpper.Trim, .Opt.ToUpper.Trim)

            If .Opt.Length > 8 Then
                .Opt = .Opt.Substring(0, 8)
            End If

            strSQL = "IF NOT EXISTS (SELECT * FROM Records WHERE Lot_No='" & .Lot_No & "') INSERT INTO Records " & _
                "(Lot_No, IMI_No, FreqVal, Opt, RecDate, [Profile], CtrlNo, MacNo, MData1, MData2, MData3, MData4, MData5, MData6) VALUES (" & _
                ch & .Lot_No & ch & ", " & _
                ch & .IMI_No & ch & ", " & _
                ch & .FreqVal & ch & ", " & _
                ch & .Opt & ch & ", " & _
                "GETDATE()" & ", " & _
                ch & .Profile & ch & ", " & _
                ch & .CtrlNo & ch & ", " & _
                ch & .MacNo & ch & ", " & _
                ch & .MData1 & ch & ", " & _
                ch & .MData2 & ch & ", " & _
                ch & .MData3 & ch & ", " & _
                ch & .MData4 & ch & ", " & _
                ch & .MData5 & ch & ", " & _
                ch & .MData6 & ch & ") ELSE UPDATE Records SET Opt='" & .Opt & "', RecDate=GETDATE(), " & _
                "IMI_No='" & .IMI_No & "', FreqVal='" & .FreqVal & "', [Profile]='" & .Profile & "', CtrlNo='" & .CtrlNo & "', MacNo='" & .MacNo & "', MData1='" & .MData1 & "', MData2='" & .MData2 & "', MData3='" & .MData3 & "', MData4='" & .MData4 & "', MData5='" & .MData5 & "', MData6='" & .MData6 & "' " & _
                "WHERE Lot_No='" & .Lot_No & "' "
        End With

        Try
            dbConnection.Open()

            Dim cmd As New SqlCommand(strSQL, dbConnection)
            'cmd.ExecuteNonQuery()

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()
            RetVal = sqlReader.RecordsAffected
        Catch sqlExc As SqlException
            EvtLog(strSQL & " -> " & sqlExc.Message)
            RetVal = -1
        End Try

        dbConnection.Close()
        Return RetVal

    End Function

    Private Function GetSerialNoBySpec(ByVal LotNo As String, ByVal SpecNo As String, ByVal DateVal1 As String, ByVal DateVal2 As String) As Integer

        Dim RetVal As Integer = 0
        Dim Result As Integer = 0

        Dim sConnStr As String = _
                "SERVER=" & sqlServer & "; " & _
                "DataBase=" & sqlName & "; " & _
                "uid=" & sqluid & "; " & _
                "pwd=" & sqlpwd
        '"Integrated Security=SSPI"

        Dim dbConnection As New SqlConnection(sConnStr)
        Dim ch As Char = ChrW(39)
        Dim strSQL As String = _
                "IF NOT EXISTS ( " & vbCrLf & _
                "SELECT * FROM RecordSerNo " & vbCrLf & _
                "WHERE lot_no='" & LotNo & "' AND IMI_No='" & SpecNo & "' AND RecDate>='" & DateVal1 & "' AND RecDate<'" & DateVal2 & "') " & vbCrLf & _
                "BEGIN " & vbCrLf & _
                "DECLARE @SerNo Int; " & vbCrLf & _
                "IF NOT EXISTS (" & vbCrLf & _
                "SELECT * FROM Records " & vbCrLf & _
                "WHERE lot_no='" & LotNo & "') " & vbCrLf & _
                "BEGIN " & vbCrLf & _
                "SELECT @SerNo=(ISNULL(MAX(SerNo), 0) + 1) FROM RecordSerNo " & vbCrLf & _
                "WHERE IMI_No='" & SpecNo & "' AND RecDate>='" & DateVal1 & "' AND RecDate<'" & DateVal2 & "' " & vbCrLf & _
                "GROUP BY IMI_No " & vbCrLf & _
                "INSERT INTO RecordSerNo VALUES ('" & LotNo & "', '" & SpecNo & "', GETDATE(), ISNULL(@SerNo, 1)) " & vbCrLf & _
                "SELECT ISNULL(@SerNo, 1) " & vbCrLf & _
                "END " & vbCrLf & _
                "ELSE" & vbCrLf & _
                "BEGIN " & vbCrLf & _
                "SELECT ISNULL(@SerNo, 0) " & vbCrLf & _
                "END " & vbCrLf & _
                "END " & vbCrLf & _
                "ELSE " & vbCrLf & _
                "BEGIN " & vbCrLf & _
                "SELECT TOP 1 @SerNo=SerNo FROM RecordSerNo " & vbCrLf & _
                "WHERE lot_no='" & LotNo & "' AND IMI_No='" & SpecNo & "' AND RecDate>='" & DateVal1 & "' AND RecDate<'" & DateVal2 & "' ORDER BY RecDate DESC " & vbCrLf & _
                "SELECT ISNULL(@SerNo, 1) " & vbCrLf & _
                "END "

        If LotNo.EndsWith("NW") Then
            strSQL = _
                "IF NOT EXISTS ( " & vbCrLf & _
                "SELECT * FROM RecordSerNo " & vbCrLf & _
                "WHERE lot_no='" & LotNo.Substring(0, LotNo.Length - 2) & "' AND IMI_No='" & SpecNo & "' AND RecDate>='" & DateVal1 & "' AND RecDate<'" & DateVal2 & "') " & vbCrLf & _
                "BEGIN " & vbCrLf & _
                "DECLARE @SerNo Int; " & vbCrLf & _
                "SELECT @SerNo=(ISNULL(MAX(SerNo), 0) + 1) FROM RecordSerNo " & vbCrLf & _
                "WHERE IMI_No='" & SpecNo & "' AND RecDate>='" & DateVal1 & "' AND RecDate<'" & DateVal2 & "' " & vbCrLf & _
                "GROUP BY IMI_No " & vbCrLf & _
                "INSERT INTO RecordSerNo VALUES ('" & LotNo.Substring(0, LotNo.Length - 2) & "', '" & SpecNo & "', GETDATE(), ISNULL(@SerNo, 1)) " & vbCrLf & _
                "SELECT ISNULL(@SerNo, 1) " & vbCrLf & _
                "END " & vbCrLf & _
                "ELSE " & vbCrLf & _
                "BEGIN " & vbCrLf & _
                "SELECT TOP 1 @SerNo=ISNULL(SerNo, 1) FROM RecordSerNo " & vbCrLf & _
                "WHERE lot_no='" & LotNo.Substring(0, LotNo.Length - 2) & "' AND IMI_No='" & SpecNo & "' AND RecDate>='" & DateVal1 & "' AND RecDate<'" & DateVal2 & "' ORDER BY RecDate DESC " & vbCrLf & _
                "SELECT ISNULL(@SerNo, 1) " & vbCrLf & _
                "END "
        End If

        If LotNo.EndsWith("EX") Then
            strSQL = _
                "IF NOT EXISTS ( " & vbCrLf & _
                "SELECT * FROM RecordSerNo " & vbCrLf & _
                "WHERE lot_no='" & LotNo.Substring(0, LotNo.Length - 2) & "') " & vbCrLf & _
                "BEGIN " & vbCrLf & _
                "DECLARE @SerNo Int; " & vbCrLf & _
                "SELECT @SerNo=(ISNULL(MAX(SerNo), 0) + 1) FROM RecordSerNo " & vbCrLf & _
                "WHERE IMI_No='" & SpecNo & "' AND RecDate>='" & DateVal1 & "' AND RecDate<'" & DateVal2 & "' " & vbCrLf & _
                "GROUP BY IMI_No " & vbCrLf & _
                "INSERT INTO RecordSerNo VALUES ('" & LotNo.Substring(0, LotNo.Length - 2) & "', '" & SpecNo & "', GETDATE(), ISNULL(@SerNo, 1)) " & vbCrLf & _
                "SELECT ISNULL(@SerNo, 1) " & vbCrLf & _
                "END " & vbCrLf & _
                "ELSE " & vbCrLf & _
                "BEGIN " & vbCrLf & _
                "SELECT TOP 1 @SerNo=ISNULL(SerNo, 0) FROM RecordSerNo " & vbCrLf & _
                "WHERE lot_no='" & LotNo.Substring(0, LotNo.Length - 2) & "' AND IMI_No='" & SpecNo & "' ORDER BY RecDate DESC " & vbCrLf & _
                "SELECT ISNULL(@SerNo, 0) " & vbCrLf & _
                "END "
        End If


        Try
            ' Open the connection, execute the command. Do not close the
            ' connection yet as it will be used in the next Try...Catch blocl.
            dbConnection.Open()

            ' A SqlCommand object is used to execute the SQL commands.
            Dim cmd As New SqlCommand(strSQL, dbConnection)
            'cmd.ExecuteNonQuery()

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()

            With sqlReader
                If .HasRows Then
                    Do While .Read()
                        RetVal = sqlReader.GetInt32(0)
                    Loop
                Else
                    RetVal = -1
                End If
            End With
        Catch sqlExc As SqlException
            RetVal = -1
        End Try

        dbConnection.Close()

        If RetVal >= 0 Then
            'If Integer.TryParse(Result, RetVal) Then
            '    RetVal += 1
            'Else
            '    RetVal = 1
            'End If
        Else
            'RetVal = 1
        End If

        Return RetVal

    End Function

    Private Function GetCountingBySpec(ByVal SpecNo As String, ByVal DateValue As String) As Integer

        Dim RetVal As Integer = 0
        Dim Result As String = "0"

        Dim sConnStr As String = _
                "SERVER=" & sqlServer & "; " & _
                "DataBase=" & sqlName & "; " & _
                "uid=" & sqluid & "; " & _
                "pwd=" & sqlpwd
        '"Integrated Security=SSPI"

        Dim dbConnection As New SqlConnection(sConnStr)
        Dim ch As Char = ChrW(39)
        Dim strSQL As String = _
            "SELECT MAX(LEFT(MData1, 3)) FROM Records WHERE IMI_No='" & SpecNo & "' AND RecDate>='" & DateValue & "' " & _
            "GROUP BY IMI_No"

        Try
            ' Open the connection, execute the command. Do not close the
            ' connection yet as it will be used in the next Try...Catch blocl.
            dbConnection.Open()

            ' A SqlCommand object is used to execute the SQL commands.
            Dim cmd As New SqlCommand(strSQL, dbConnection)
            'cmd.ExecuteNonQuery()

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()

            With sqlReader
                If .HasRows Then
                    Do While .Read()
                        Result = sqlReader.GetString(0)
                    Loop
                Else
                    RetVal = -1
                End If
            End With
        Catch sqlExc As SqlException
            RetVal = -1
        End Try

        dbConnection.Close()

        If RetVal >= 0 Then
            If Integer.TryParse(Result, RetVal) Then
                RetVal += 1
            Else
                RetVal = 1
            End If
        Else
            RetVal = 1
        End If

        Return RetVal

    End Function

    Private Function GetProfilesFromServer(ByVal MacCtrlNo As String, ByVal ProfileName As String, ByRef RetData As ParameterProfile) As Integer

        Dim RetVal As Integer = 0
        Dim sConnStr As String = _
                "SERVER=" & sqlServer & "; " & _
                "DataBase=" & sqlName & "; " & _
                "uid=" & sqluid & "; " & _
                "pwd=" & sqlpwd
        '"Integrated Security=SSPI"

        Dim dbConnection As New SqlConnection(sConnStr)
        Dim ch As Char = ChrW(39)
        Dim strSQL As String = _
            "SELECT * FROM Setting WHERE CtrlNo='" & MacCtrlNo & "' " & _
            "AND Spec='" & ProfileName & "'"

        Try
            ' Open the connection, execute the command. Do not close the
            ' connection yet as it will be used in the next Try...Catch blocl.
            dbConnection.Open()

            ' A SqlCommand object is used to execute the SQL commands.
            Dim cmd As New SqlCommand(strSQL, dbConnection)
            'cmd.ExecuteNonQuery()

            Dim sqlReader As SqlDataReader = cmd.ExecuteReader()

            With sqlReader
                Dim iFieldCnt As Integer = .FieldCount
                Dim iRecNo As Integer = 0

                If .HasRows Then
                    Dim sRetData(iFieldCnt - 1) As String

                    Do While .Read()
                        With RetData
                            .UseDot = sqlReader.GetString(16)
                            .UseBlock = sqlReader.GetString(17)
                        End With

                        iRecNo += 1
                    Loop
                Else
                    RetVal = -1
                End If
            End With
        Catch sqlExc As SqlException
            RetVal = -1
        End Try

        dbConnection.Close()
        Return RetVal

    End Function

End Class