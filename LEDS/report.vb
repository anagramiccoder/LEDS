Imports MySql.Data.MySqlClient
Imports System.Data.Sql
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Report
    Public sauce = "localhost"
    Dim cs As String = "Database=oos;Data Source=" & sauce & ";" _
            & "User Id=ledsilocos;Password=ilocos2020"
    Dim con As New MySqlConnection(cs)
    Dim ds As New DataSet()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            If cb.Text = "Processing" Or cb.Text = "To Ship" Then
                .CommandText = "Select `ordnum`,`user`,`ordered`,`address`,`cn`, `sf`+`cost` FROM `ord` WHERE `status`=@a UNION Select `ordnum`,`user`,`ordered`,`address`,`cn`,`sf`+`cost` FROM `hist` WHERE `status`=@a  ORDER BY `ordnum`"
            Else

                .CommandText = "Select `ordnum`,`user`,`ordered`,`address`,`cn`,`sf`+`cost`,`TN` FROM `ord` WHERE `status`=@a UNION Select `ordnum`,`user`,`ordered`,`address`,`cn`,`sf`+`cost`,`TN` FROM `hist` WHERE `status`=@a ORDER BY `ordnum`"
            End If
            .Prepare()
            .Parameters.AddWithValue("@a", cb.Text)
            Dim adpt As New MySqlDataAdapter(cmd)
            adpt.Fill(ds)
            Conv()
        End With
    End Sub
    Private Sub Conv()
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        sfd.Filter = "Excel Workbook|*.xlsx"
        sfd.Title = "Export to Excel"
        sfd.FileName = cb.Text + Now.Date.ToString("yyyy-MM-dd")
        sfd.ShowDialog()
        NI.Visible = True
        NI.BalloonTipText = "Converting Excel File,Please wait..."
        NI.ShowBalloonTip(2000)
        Dim xlApp As Excel.Application
        Dim xlWorkBook As Excel.Workbook
        Dim ws As Excel.Worksheet
        Dim misValue As Object = System.Reflection.Missing.Value
        xlApp = New Excel.Application
        xlWorkBook = xlApp.Workbooks.Add(misValue)
        ws = xlWorkBook.Sheets("sheet1")
        Dim dt As DataTable = ds.Tables(0)
        ws.Cells(1, 1) = "CODE"
        ws.Cells(1, 2) = "USERNAME"
        ws.Columns("B").columnwidth = 30
        ws.Cells(1, 3) = "ADDRESS"
        ws.Cells(1, 5) = "TOTAL"
        ws.Cells(1, 6) = "PRICE"
        ws.Columns("C").ColumnWidth = 80
        ws.Cells(1, 4) = "CONTACT NUMBER"
        ws.Columns("D").columnwidth = 25
        If cb.SelectedIndex > 1 Then
            ws.Cells(1, 7) = "TN"
            ws.Columns("G").columnwidth = 25
        End If
        For j As Integer = 0 To dt.Rows.Count - 1
            ws.Cells(j + 2, 1) = dt(j)(0).ToString
            ws.Cells(j + 2, 2) = dt(j)(1).ToString
            ws.Cells(j + 2, 3) = dt(j)(3).ToString
            ws.Cells(j + 2, 4) = dt(j)(4).ToString
            ws.Cells(j + 2, 5) = Count(dt(j)(2).ToString).ToString
            ws.Cells(j + 2, 6) = dt(j)(5).ToString
            If cb.SelectedIndex > 1 Then
                ws.Cells(j + 2, 7) = dt(j)(6).ToString
            End If
        Next
        xlApp.DisplayAlerts = False
        ws.SaveAs(sfd.FileName.ToString)
        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(ws)
        NI.BalloonTipText = "Excel File Created:" + sfd.FileName
        NI.ShowBalloonTip(1000)
        NI.Visible = False
    End Sub

    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub
    Private Function Count(ByVal Ord As String)
        Dim Tot As Integer = 0
        For Each des In Ord.Split(";")
            For Each si In des.Split(":")(1).Split(",")
                Tot += si.Split("-")(0)
            Next si
        Next des
        Return Tot
    End Function

    Private Sub Report_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cb.SelectedIndex = 0
    End Sub
End Class