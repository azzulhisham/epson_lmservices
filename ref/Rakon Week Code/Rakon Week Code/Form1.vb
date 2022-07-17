Imports System.Globalization
Imports System.ComponentModel
Imports System.Management
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports System.Math
Imports Microsoft.Win32




Public Class Form1

    Dim WeekDayName() As String = {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"}

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim m_WkCode As String = String.Empty
        Dim m_Today As Date = Today

        Dim Year_D As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim Month_D As String = "123456789XYZ"
        Dim Day_D As String = "123456789ABCDEFGHJKLMNOPQRSTUVWXYZ"
        Dim Day_D_ As String = "123456789ABCDEFGHJKLMNPQRSTUVWXYZ"
        Dim WkNoCd As String = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ"

        Dim myCI As New CultureInfo("en-US")
        Dim myCal As Calendar = myCI.Calendar





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

            If m_WkCode >= prvYrsWeekNo And myCal.GetWeekOfYear(m_Today, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday) = 1 Then
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
        Me.Label1.Text = "Week No. : " & String.Format("{0:D2}", myCal.GetWeekOfYear(m_Today, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday))
        Me.Label3.Text = "Week Code : " & m_WkCode

    End Sub

    Private Sub Form1_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        Me.Timer1.Enabled = False

    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        With Me
            Dim dt As Date = Now
            .Label2.Text = String.Format("{0:D2}-{1:D2}-{2:D4} {3:D2}:{4:D2}:{5:D2}", dt.Day, dt.Month, dt.Year, dt.Hour, dt.Minute, dt.Second) & " (" & WeekDayName(dt.DayOfWeek) & ")"

            With .Timer1
                .Interval = 1000
                .Enabled = True
            End With
        End With

    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        With Me
            Dim dt As Date = Now
            .Label2.Text = String.Format("{0:D2}-{1:D2}-{2:D4} {3:D2}:{4:D2}:{5:D2}", dt.Day, dt.Month, dt.Year, dt.Hour, dt.Minute, dt.Second) & " (" & WeekDayName(dt.DayOfWeek) & ")"
        End With

    End Sub

End Class
