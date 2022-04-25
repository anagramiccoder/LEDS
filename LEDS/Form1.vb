Imports MySql.Data.MySqlClient
Imports System.Data.Sql
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports Excel = Microsoft.Office.Interop.Excel
Public Class Form1
    Public sauce = "localhost"
    Dim cs As String = "Database=oos;Data Source=" & sauce & ";" _
            & "User Id=ledsilocos;Password=ilocos2020"
    Dim con As New MySqlConnection(cs)
    Dim search() As String = {"user", "ordered", "address", "status", "TN", "ordnum"}
    Dim index, index2 As Integer
    Public go As Boolean = False
    Public lvl As Integer
    Dim Rowind As List(Of Integer) = New List(Of Integer)
    Dim Tab As DataTable = New DataTable()
    Public Account As String = ""
    Dim pord As String = ""
    Public Sub Fetch()
        Try
            con.Open()
            dg1ref()
            dg2ref()
            dg3ref()
            dg5ref()
        Catch ex As InvalidOperationException
            con.Close()
            Fetch()
        Catch ex As MySqlException
            MessageBox.Show("Database not Loaded")
            MessageBox.Show(ex.ToString)
            Me.Refresh()
        End Try
    End Sub
    Private Sub Src_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Src.TextChanged
        Dim cmd As New MySqlCommand
        Try
            connect()
            With cmd
                .Connection = con
                .CommandText = "Select `ordnum`,`user`,`ordered`,`address`,`status` FROM `ord` WHERE LOWER(`" & search(ComboBox1.SelectedIndex) & "`) LIKE '%" & Src.Text.ToLower & "%' ORDER BY `ordnum` DESC"
                .Prepare()
            End With
            Dim adpt As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            adpt.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
        Catch ex As InvalidOperationException
            con.Close()
            Fetch()
        Catch ex As MySqlException
            MessageBox.Show(ex.ToString, "Database not Loaded")
        End Try
    End Sub
    Private Sub DataGridView1_CellMouseDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseDoubleClick
        Button2_Click(sender, e)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If lvl = 3 Then
            connect()
            Dim f As New Admin
            Dim cmd As New MySqlCommand
            With cmd
                .Connection = con
                .CommandText = "SELECT `ordered`,`ordnum`,`accname` FROM `ord` WHERE `ordnum`=@a"
                .Prepare()
                .Parameters.AddWithValue("@a", DataGridView1.Rows(index).Cells(0).Value)
                Dim reader As MySqlDataReader
                reader = .ExecuteReader
                reader.Read()
                f.code.Text = reader.GetString(1)
                f.details.Text = reader.GetString(0)
                Dim names() As String = reader.GetString(2).Split(";")
                f.ode.Text = names(0)
                f.te.Text = names(1)
                f.Show()
            End With
        Else
            Dim f As New Order
            With DataGridView1.Rows(index)
                If .Cells(0).Value.ToString.Length > 0 Then
                    f.oc.Text = .Cells(0).Value.ToString
                    f.lvl = lvl
                    f.acc = Account
                    f.Show()
                End If
            End With
            dg1ref()
        End If
    End Sub
    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        index = e.RowIndex
    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If lvl = 3 Then
            Button1_Click(sender, e)
        Else
            Dim f As New Order
            With DataGridView1.Rows(index)
                f.oc.Text = .Cells(0).Value.ToString
            End With
            f.lvl = lvl
            f.acc = Account
            f.Button2_Click(sender, e)
            f.Show()
        End If

    End Sub

    Public Sub dg1ref()
        connect()
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            If lvl = 1 Then
                .CommandText = "Select `ordnum`,`user`,`ordered`,`address`,`status` FROM `ord` WHERE `accname` LIKE '" & Account & "%' or `accname` LIKE 'none%' ORDER BY `ordnum` DESC"
            ElseIf lvl = 2 Then
                .CommandText = "Select `ordnum`,`user`,`ordered`,`address`,`status` FROM `ord` WHERE `accname` LIKE '%" & Account & "' or `accname` LIKE '%none'ORDER BY `ordnum` DESC"
            Else
                .CommandText = "Select `ordnum`,`user`,`ordered`,`address`,`status` FROM `ord` WHERE 1 ORDER BY `ordnum` DESC"
            End If
            Dim Adpt As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            Adpt.Fill(ds)
            DataGridView1.DataSource = ds.Tables(0)
            DataGridView1.ClearSelection()
        End With
    End Sub
    Public Sub dg2ref()
        connect()
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            .CommandText = "SELECT `ID`, `s16`, `ts`, `xs`, `s`, `m`, `l`, `xl`, `2xl`, `3xl`, `4xl`,`sp`,`key` FROM `shirt` WHERE 1 ORDER BY `key`"
            Dim Adpt As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            Adpt.Fill(ds)
            DataGridView2.DataSource = ds.Tables(0)
            DataGridView2.ClearSelection()
        End With
    End Sub
    Public Sub dg3ref()
        connect()
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            .CommandText = "Select `ordnum`,`user`,`ordered`,`address`,`status` FROM `hist` WHERE 1 ORDER BY `ordnum`"
            Dim Adpt As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            Adpt.Fill(ds)
            DataGridView3.DataSource = ds.Tables(0)
            DataGridView3.ClearSelection()
        End With
    End Sub
    Public Sub dg4ref()
        Tab.Clear()
        Dim slist() As String = {"s16", "ts", "xs", "s", "m", "l", "xl", "2xl", "3xl", "4xl", "sp"}
        connect()
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            .CommandText = "SELECT `ordered` FROM `ord` WHERE (`ord`.`dt` BETWEEN @a AND @b) AND LOWER(`ord`.`ordered`) LIKE '%" & srcb.Text.ToLower & "%' UNION SELECT `ordered` FROM `hist` WHERE (`hist`.`dt` BETWEEN @a and @b) AND LOWER(`hist`.`ordered`) LIKE '%" & srcb.Text.ToLower & "%' "
            .Prepare()
            .Parameters.AddWithValue("@a", sdate.Value.ToString("yyyy-MM-dd"))
            .Parameters.AddWithValue("@b", edate.Value.ToString("yyyy-MM-dd"))
            Dim adpt As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            adpt.Fill(ds)
            Dim dict As New Dictionary(Of String, Integer())
            For Each row As DataRow In ds.Tables(0).Rows
                Dim ord() As String = row.ItemArray(0).split(";")
                For Each shirt In ord
                    Dim code As String = shirt.Split(":")(0)
                    Try
                        dict.Add(code, {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0})
                    Catch ex As ArgumentException

                    End Try
                    Dim var() As String = shirt.Split(":")(1).Split(",")
                    For Each sh In var
                        Dim count As Integer = Integer.Parse(sh.Split("-")(0))
                        Dim size As String = sh.Split("-")(1).ToLower
                        If size.Contains("upsized") Then
                            size = slist(Array.IndexOf(slist, size.Split("(")(0)) + 1)
                        End If
                        dict(code)(Array.IndexOf(slist, size)) += count
                        dict(code)(11) += count
                    Next sh
                Next shirt
            Next row
            For Each pair In dict
                Dim frow As DataRow = Tab.NewRow
                frow(0) = pair.Key
                frow(1) = pair.Value(0)
                frow(2) = pair.Value(1)
                frow(3) = pair.Value(2)
                frow(4) = pair.Value(3)
                frow(5) = pair.Value(4)
                frow(6) = pair.Value(5)
                frow(7) = pair.Value(6)
                frow(8) = pair.Value(7)
                frow(9) = pair.Value(8)
                frow(10) = pair.Value(9)
                frow(11) = pair.Value(10)
                frow(12) = pair.Value(11)

                Tab.Rows.Add(frow)
            Next pair
        End With
        Tab.DefaultView.Sort = "TOTAL DESC"
        Tab = Tab.DefaultView.ToTable()
        dgv4.DataSource = Tab
        With dgv4
            .Columns(0).Width = 145
            For x As Integer = 1 To 12
                .Columns(x).Width = 36
            Next
            .Columns(0).HeaderCell.Value = "ID"
            .Columns(1).HeaderCell.Value = "S16"
            .Columns(2).HeaderCell.Value = "TS"
            .Columns(3).HeaderCell.Value = "XS"
            .Columns(4).HeaderCell.Value = "S"
            .Columns(5).HeaderCell.Value = "M"
            .Columns(6).HeaderCell.Value = "L"
            .Columns(7).HeaderCell.Value = "XL"
            .Columns(8).HeaderCell.Value = "2XL"
            .Columns(9).HeaderCell.Value = "3XL"
            .Columns(10).HeaderCell.Value = "4XL"
            .Columns(11).HeaderCell.Value = "SP"
            .Columns(12).HeaderCell.Value = "TOT"
        End With
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim cmd As New MySqlCommand
        connect()
        With cmd
            .Connection = con
            .CommandText = "INSERT INTO `ord`(`user`, `ordered`, `address`, `status`, `TN`, `cost`, `sf`, `cn`,`dt`,`accname`) VALUES(@a,@b,@c,@d,@e,@f,@g,@h,@i,@j)"
            .Prepare()
            Try
                .Parameters.AddWithValue("@a", nameid.Text)
                .Parameters.AddWithValue("@b", ord.Text)
                .Parameters.AddWithValue("@c", address.Text)
                .Parameters.AddWithValue("@d", "Processing")
                .Parameters.AddWithValue("@e", "0")
                .Parameters.AddWithValue("@f", Integer.Parse(tots.Text))
                .Parameters.AddWithValue("@g", Integer.Parse(shipp.Text))
                .Parameters.AddWithValue("@h", cont.Text)
                .Parameters.AddWithValue("@i", Now.ToString("yyyy-MM-dd"))
                .Parameters.AddWithValue("@j", Account + ";none")
                If Check(ord.Text) Then
                    If Sdes(ord.Text) Then
                        Update_inventory(ord.Text)
                        connect()
                        .Connection = con
                        .ExecuteNonQuery()
                        nameid.Text = ""
                        ord.Text = ""
                        address.Text = ""
                        tots.Text = ""
                        shipp.Text = ""
                        cont.Text = ""
                    End If
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                MessageBox.Show("PLEASE ENTER VALID INFORMATION")
            End Try
        End With
        dg1ref()
    End Sub
    Public Function Sdes(ByVal Order As String)
        Dim des() As String = Order.Split(";")
        For Each shirt In des
            Dim code As String = shirt.Split(":")(0)
            connect()
            Dim cmd As New MySqlCommand
            With cmd
                .Connection = con
                .CommandText = "SELECT COUNT(*) FROM `shirt` WHERE `ID`=@a"
                .Prepare()
                .Parameters.AddWithValue("@a", code)
                If .ExecuteScalar < 1 Then
                    MessageBox.Show("The design '" + code + "' does not exist in the inventory")
                    Return False
                End If
            End With
        Next shirt
        Return True
    End Function
    'functions and subs that are non event
    Public Sub connect()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
    End Sub

    Private Sub srch()
        If ss.SelectedIndex = -1 Then
            ss.SelectedIndex = 0
        End If
        If so.SelectedIndex = -1 Then
            so.SelectedIndex = 0
        End If
        connect()
        Dim key() As String = {"ID", "s16", "ts", "xs", "s", "m", "l", "xl", "2xl", "3xl", "4xl"}
        Dim ord() As String = {"ASC", "DESC"}
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            .CommandText = "SELECT `ID`, `s16`, `ts`, `xs`, `s`, `m`, `l`, `xl`, `2xl`, `3xl`, `4xl`,`key` FROM `shirt` WHERE LOWER(`ID`) LIKE '%" & ssrc.Text.ToLower & "%' ORDER BY `" & key(ss.SelectedIndex) & "` " & ord(so.SelectedIndex) & ""
            .Prepare()
            Dim Adpt As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            Adpt.Fill(ds)
            DataGridView2.DataSource = ds.Tables(0)
        End With
    End Sub
    Private Sub cont_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles cont.KeyPress, tots.KeyPress, shipp.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If

    End Sub

    Private Sub cont_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cont.LostFocus
        If cont.Text.Length < 11 Then
        End If
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ssrc.TextChanged
        srch()
    End Sub

    Private Sub ss_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ss.SelectedIndexChanged
        srch()
    End Sub

    Private Sub so_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles so.SelectedIndexChanged
        srch()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        connect()
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            .CommandText = "SELECT COUNT(*) FROM `shirt` WHERE `ID`=@id"
            .Prepare()
            .Parameters.AddWithValue("@id", sid.Text)
            If .ExecuteScalar > 0 Then
                Dim result As Integer = MessageBox.Show(sid.Text + " already exists, do you want to add to the new stack?", "WARNING", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    .CommandText = "SELECT `ID`, `s16`, `ts`, `xs`, `s`, `m`, `l`, `xl`, `2xl`, `3xl`, `4xl`,`sp`,`key` FROM `shirt` WHERE `ID`=@id"
                    Dim reader As MySqlDataReader
                    reader = cmd.ExecuteReader
                    reader.Read()
                    s16.Value += reader.GetInt32(1)
                    ts.Value += reader.GetInt32(2)
                    xs.Value += reader.GetInt32(3)
                    s.Value += reader.GetInt32(4)
                    m.Value += reader.GetInt32(5)
                    l.Value += reader.GetInt32(6)
                    xl.Value += reader.GetInt32(7)
                    xl2.Value += reader.GetInt32(8)
                    xl3.Value += reader.GetInt32(9)
                    xl4.Value += reader.GetInt32(10)
                    SP.Value += reader.GetInt16(11)
                    reader.Close()
                    .CommandText = "UPDATE `shirt`  SET `s16`=@a, `ts`=@b, `xs`=@c,`s`=@d, `m`=@e, `l`=@f, `xl`=@g, `2xl`=@h, `3xl`=@i, `4xl`=@j, `sp`=@k WHERE `ID`=@id"
                    .Prepare()
                    .Parameters.AddWithValue("@a", s16.Value)
                    .Parameters.AddWithValue("@b", ts.Value)
                    .Parameters.AddWithValue("@c", xs.Value)
                    .Parameters.AddWithValue("@d", s.Value)
                    .Parameters.AddWithValue("@e", m.Value)
                    .Parameters.AddWithValue("@f", l.Value)
                    .Parameters.AddWithValue("@g", xl.Value)
                    .Parameters.AddWithValue("@h", xl2.Value)
                    .Parameters.AddWithValue("@i", xl3.Value)
                    .Parameters.AddWithValue("@j", xl4.Value)
                    .Parameters.AddWithValue("@k", SP.Value)
                    .ExecuteNonQuery()
                    dg2ref()
                    sid.Text = ""
                    s16.Value = 0
                    ts.Value = 0
                    xs.Value = 0
                    s.Value = 0
                    m.Value = 0
                    l.Value = 0
                    xl.Value = 0
                    xl2.Value = 0
                    xl3.Value = 0
                    xl4.Value = 0
                    SP.Value = 0
                    sid.Focus()
                Else
                    MessageBox.Show("Please Change the ID")
                End If
            Else
                .CommandText = "INSERT INTO `shirt`(`ID`, `s16`, `ts`, `xs`, `s`, `m`, `l`, `xl`, `2xl`, `3xl`, `4xl`,`sp`) VALUES (@id,@a,@b,@c,@d,@e,@f,@g,@h,@i,@j,@k)"
                .Prepare()
                .Parameters.AddWithValue("@a", s16.Value)
                .Parameters.AddWithValue("@b", ts.Value)
                .Parameters.AddWithValue("@c", xs.Value)
                .Parameters.AddWithValue("@d", s.Value)
                .Parameters.AddWithValue("@e", m.Value)
                .Parameters.AddWithValue("@f", l.Value)
                .Parameters.AddWithValue("@g", xl.Value)
                .Parameters.AddWithValue("@h", xl2.Value)
                .Parameters.AddWithValue("@i", xl3.Value)
                .Parameters.AddWithValue("@j", xl4.Value)
                .Parameters.AddWithValue("@k", SP.Value)
                .ExecuteNonQuery()
                sid.Text = ""
                s16.Value = 0
                ts.Value = 0
                xs.Value = 0
                s.Value = 0
                m.Value = 0
                l.Value = 0
                xl.Value = 0
                xl2.Value = 0
                xl3.Value = 0
                xl4.Value = 0
                SP.Value = 0
                sid.Focus()
            End If
        End With
        con.Close()
        dg2ref()
    End Sub
    Public Sub acc()
        If Not (lvl = 3) Then
            tabs.Controls.Remove(TabPage2)
            tabs.Controls.Remove(TabPage3)
            tabs.Controls.Remove(TabPage4)
            tabs.Controls.Remove(TabPage5)
            Button9.Visible = False
        End If
        If Not lvl = 1 Then
            nameid.Enabled = False
            ord.Enabled = False
            cont.Enabled = False
            address.Enabled = False
            Label2.Enabled = False
            Label3.Enabled = False
            Label4.Enabled = False
            Label5.Enabled = False
            Label6.Enabled = False
            Button3.Enabled = False
            tots.Enabled = False
            shipp.Enabled = False
        End If
        ComboBox1.SelectedIndex = 0
        so.SelectedIndex = 0
        ss.SelectedIndex = 0
        Tab.Columns.Add("ID", GetType(String))
        Tab.Columns.Add("S16", GetType(Integer))
        Tab.Columns.Add("TS", GetType(Integer))
        Tab.Columns.Add("XS", GetType(Integer))
        Tab.Columns.Add("S", GetType(Integer))
        Tab.Columns.Add("M", GetType(Integer))
        Tab.Columns.Add("L", GetType(Integer))
        Tab.Columns.Add("XL", GetType(Integer))
        Tab.Columns.Add("2XL", GetType(Integer))
        Tab.Columns.Add("3XL", GetType(Integer))
        Tab.Columns.Add("4XL", GetType(Integer))
        Tab.Columns.Add("SP", GetType(Integer))
        Tab.Columns.Add("TOTAL", GetType(Integer))
        Fetch()
        DataGridView1.EnableHeadersVisualStyles = False
        DataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray
        DataGridView2.EnableHeadersVisualStyles = False
        DataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray
        DataGridView3.EnableHeadersVisualStyles = False
        DataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray
        With DataGridView1
            .Columns(0).HeaderCell.Value = "Code"
            .Columns(1).HeaderCell.Value = "Name"
            .Columns(2).HeaderCell.Value = "Order"
            .Columns(3).HeaderCell.Value = "Address"
            .Columns(4).HeaderCell.Value = "Remarks"
            .Columns(3).Width = 183
        End With
        If lvl = 3 Then
            With DataGridView2
                .Columns(0).Width = 153
                For x As Integer = 1 To 11
                    .Columns(x).Width = 39
                Next
                .Columns(0).HeaderCell.Value = "ID"
                .Columns(1).HeaderCell.Value = "S16"
                .Columns(2).HeaderCell.Value = "TS"
                .Columns(3).HeaderCell.Value = "XS"
                .Columns(4).HeaderCell.Value = "S"
                .Columns(5).HeaderCell.Value = "M"
                .Columns(6).HeaderCell.Value = "L"
                .Columns(7).HeaderCell.Value = "XL"
                .Columns(8).HeaderCell.Value = "2XL"
                .Columns(9).HeaderCell.Value = "3XL"
                .Columns(10).HeaderCell.Value = "4XL"
                .Columns(11).HeaderCell.Value = "SP"
                .Columns(12).Visible = False
            End With
            With DataGridView3
                .Columns(0).HeaderCell.Value = "Code"
                .Columns(1).HeaderCell.Value = "Name"
                .Columns(2).HeaderCell.Value = "Order"
                .Columns(3).HeaderCell.Value = "Address"
                .Columns(4).HeaderCell.Value = "Remarks"
                .Columns(3).Width = 183
            End With
            sdate.Value = Now
            edate.Value = Now
        End If
        If lvl = 3 Then
            Button2.Enabled = False
            Button10.Visible = False
        End If
    End Sub
    Public Sub dg5ref()
        connect()
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            .CommandText = "SELECT * FROM `acc` WHERE 1 ORDER BY `acclvl`"
            Dim adpt As New MySqlDataAdapter(cmd)
            Dim ds As New DataSet()
            adpt.Fill(ds)
            DataGridView5.DataSource = ds.Tables(0)
        End With
    End Sub
    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click
        Dim res As DialogResult = MessageBox.Show("Do you want to Log out?", "ALERT", MessageBoxButtons.YesNo)
        If res = DialogResult.Yes Then
            Dim l As New Loginn
            l.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If sauce = "localhost" Then
            Label30.Text = "Data Source IP:" + System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList(1).ToString()
        Else
            Label30.Text = "Data Source IP:" + sauce
        End If
        sdate.Format = DateTimePickerFormat.Custom
        sdate.CustomFormat = "yyyy-MM-dd"
        edate.Format = DateTimePickerFormat.Custom
        edate.CustomFormat = "yyyy-MM-dd"
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        dg4ref()
    End Sub
    Public Function Check(ByVal Ord As String)
        connect()
        Try
            Dim slist() As String = {"s16", "ts", "xs", "s", "m", "l", "xl", "2xl", "3xl", "4xl", "sp"}
            Dim orders() As String = Ord.Split(";")
            For Each shirt In orders
                Dim cmd As New MySqlCommand
                cmd.Connection = con
                Dim code As String = shirt.Split(":")(0).ToLower
                cmd.CommandText = "SELECT COUNT(*) FROM `shirt` WHERE `ID`=@a"
                cmd.Prepare()
                cmd.Parameters.AddWithValue("@a", code)
                Dim sizes() As String = shirt.Split(":")(1).Split(",")
                For Each var In sizes
                    Dim count As Integer = var.Split("-")(0)
                    Dim size As String = var.Split("-")(1).ToLower
                    If Not slist.Contains(size) Then
                        con.Close()
                        MessageBox.Show("Size not in inventory")
                        Return False
                    End If
                Next var
            Next shirt
            con.Close()
            Return True
        Catch ex As Exception
            con.Close()
            MessageBox.Show("Order is invalid" & vbCrLf & "Please follow the formatting" & vbCrLf & "design:#-size,#-size(upsized);(new)design:#-size")
            Return False
        End Try

    End Function

    Private Sub Srcb_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles srcb.TextChanged
        dg4ref()
    End Sub

    Private Sub DataGridView2_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellDoubleClick
        sid.Text = DataGridView2.Rows(e.RowIndex).Cells(0).Value.ToString
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Tab.DefaultView.Sort = "TOTAL DESC"
        Dim dt As DataTable = Tab.DefaultView.ToTable()
        Dim f As New t5
        Try
            f.d1.Text += dt.Rows(0).Item(0).ToString
            f.s1.Text += dt.Rows(0).Item(12).ToString
            f.d2.Text += dt.Rows(1).Item(0).ToString
            f.s2.Text += dt.Rows(1).Item(12).ToString
            f.d3.Text += dt.Rows(2).Item(0).ToString
            f.s3.Text += dt.Rows(2).Item(12).ToString
            f.d4.Text += dt.Rows(3).Item(0).ToString
            f.s4.Text += dt.Rows(3).Item(12).ToString
            f.d5.Text += dt.Rows(4).Item(0).ToString
            f.s5.Text += dt.Rows(4).Item(12).ToString
        Catch ex As Exception

        End Try
        f.Show()
    End Sub
    Public Sub Update_inventory(ByVal Order As String)
        Dim slist() As String = {"s16", "ts", "xs", "s", "m", "l", "xl", "2xl", "3xl", "4xl", "sp"}
        Dim dex As Integer = 0
        connect()
        Dim cm As New MySqlCommand
        With cm
            Dim des() As String = Order.Split(";")
            For Each var In des
                Dim ncount() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                Dim code As String = var.Split(":")(0)
                Dim shirts() As String = var.Split(":")(1).Split(",")
                For Each shirt In shirts
                    Dim count As Integer = Integer.Parse(shirt.Split("-")(0))
                    Dim size As String = shirt.Split("-")(1).ToLower
                    If size.Contains("upsized") Then
                        size = size.Split("(")(0)
                        dex = Array.IndexOf(slist, size) + 1
                    Else
                        dex = Array.IndexOf(slist, size)
                    End If
                    ncount(dex) += count
                Next shirt
                .Connection = con
                .CommandText = "SELECT * FROM `shirt` WHERE `ID`=@id"
                .Prepare()
                .Parameters.AddWithValue("@id", code)
                Dim reader As MySqlDataReader
                reader = .ExecuteReader
                reader.Read()
                Dim pcount() As Integer = {reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12)}
                reader.Close()
                For x As Integer = 0 To 10
                    pcount(x) -= ncount(x)
                Next x
                .CommandText = "UPDATE `shirt`  SET `s16`=@a, `ts`=@b, `xs`=@c,`s`=@d, `m`=@e, `l`=@f, `xl`=@g, `2xl`=@h, `3xl`=@i, `4xl`=@j, `sp`=@k WHERE `ID`=@id"
                .Prepare()
                .Parameters.AddWithValue("@a", pcount(0))
                .Parameters.AddWithValue("@b", pcount(1))
                .Parameters.AddWithValue("@c", pcount(2))
                .Parameters.AddWithValue("@d", pcount(3))
                .Parameters.AddWithValue("@e", pcount(4))
                .Parameters.AddWithValue("@f", pcount(5))
                .Parameters.AddWithValue("@g", pcount(6))
                .Parameters.AddWithValue("@h", pcount(7))
                .Parameters.AddWithValue("@i", pcount(8))
                .Parameters.AddWithValue("@j", pcount(9))
                .Parameters.AddWithValue("@k", pcount(10))
                .ExecuteNonQuery()
                .Parameters.RemoveAt("@id")
                .Parameters.RemoveAt("@a")
                .Parameters.RemoveAt("@b")
                .Parameters.RemoveAt("@c")
                .Parameters.RemoveAt("@d")
                .Parameters.RemoveAt("@e")
                .Parameters.RemoveAt("@f")
                .Parameters.RemoveAt("@g")
                .Parameters.RemoveAt("@h")
                .Parameters.RemoveAt("@i")
                .Parameters.RemoveAt("@j")
                .Parameters.RemoveAt("@k")

            Next var
        End With
        con.Close()
    End Sub

    Private Sub DataGridView2_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView2.CellFormatting
        If Not e.ColumnIndex = 0 And e.RowIndex < DataGridView2.Rows.Count - 1 Then
            Dim quantity As Integer = Convert.ToInt32(e.Value)
            If quantity < 0 Then
                e.CellStyle.BackColor = Color.IndianRed
            End If
        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        sfd.Filter = "Excel Workbook|*.xlsx"
        sfd.Title = "Export to Excel"
        sfd.ShowDialog()
        If Not sfd.FileName = "" Then
            NI.Visible = True
            NI.BalloonTipText = "Converting Excel File,Please wait..."
            NI.ShowBalloonTip(2000)
            Dim xlApp As Excel.Application
            Dim xlWorkBook As Excel.Workbook
            Dim xlWorkSheet As Excel.Worksheet
            Dim misValue As Object = System.Reflection.Missing.Value
            xlApp = New Excel.Application
            xlWorkBook = xlApp.Workbooks.Add(misValue)
            xlWorkSheet = xlWorkBook.Sheets("sheet1")

            For j As Integer = 0 To dgv4.Columns.Count - 1
                xlWorkSheet.Cells(1, j + 1) = dgv4.Columns(j).HeaderCell.Value
                xlWorkSheet.Cells(1, j + 1).interior.color = Color.DarkGray
            Next
            For i As Integer = 0 To dgv4.Rows.Count - 2
                For j As Integer = 0 To dgv4.Columns.Count - 1
                    xlWorkSheet.Cells(i + 2, j + 1) = dgv4(j, i).Value
                Next

            Next
            xlApp.DisplayAlerts = False
            xlWorkSheet.SaveAs(sfd.FileName.ToString)

            xlWorkBook.Close()
            xlApp.Quit()

            releaseObject(xlApp)
            releaseObject(xlWorkBook)
            releaseObject(xlWorkSheet)
            NI.BalloonTipText = "Excel File Created:" + sfd.FileName
            NI.ShowBalloonTip(1000)
            NI.Visible = False
        End If
    End Sub

    Private Sub menuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.Close()
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

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim cmd As New MySqlCommand
        connect()
        With cmd
            .Connection = con
            Try
                .CommandText = "INSERT INTO `acc`(`accname`,`pass`,`acclvl`) VALUES(@a,@b,@c)"
                .Prepare()
                .Parameters.AddWithValue("@a", un.Text)
                .Parameters.AddWithValue("@b", pw.Text)
                .Parameters.AddWithValue("@c", Convert.ToInt32(acclvl.Text))
                .ExecuteNonQuery()
                un.Text = ""
                pw.Text = ""
            Catch ex As MySqlException
                Dim res As DialogResult = MessageBox.Show("USERNAME already exists, do you want to change the password?", "ERROR", MessageBoxButtons.YesNo)
                If res = DialogResult.Yes Then
                    .CommandText = "UPDATE `acc` SET `pass`=@a WHERE `accname`=@b"
                    .ExecuteNonQuery()
                    un.Text = ""
                    pw.Text = ""
                End If
            End Try
        End With
        dg5ref()
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim f As New Report
        f.Show()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        dg1ref()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Try
            If Not DataGridView2.Rows.Count - 2 < index2 Then
                Dim cd As String = DataGridView2.Rows(index2).Cells(0).Value.ToString
                Dim result As DialogResult = MessageBox.Show("Do you really want to delete" + cd, "WARNING", MessageBoxButtons.YesNo)
                If result = DialogResult.Yes Then
                    connect()
                    Dim cmd As New MySqlCommand
                    With cmd
                        .Connection = con
                        .CommandText = "DELETE FROM `shirt` WHERE `ID`='" & cd & "'"
                        .ExecuteNonQuery()
                    End With
                    dg2ref()
                End If
            End If
        Catch ex As NullReferenceException
        Catch ex As MySqlException
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim sfd As SaveFileDialog = New SaveFileDialog()
        sfd.Filter = "Excel Workbook|*.xlsx"
        sfd.Title = "Export to Excel"
        sfd.FileName = "Shirt Inventory " + Now.Date.ToString("MM-dd-yyy")
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
        ws.Cells(1, 1) = "Shirt Code"
        ws.Cells(1, 2) = "S16"
        ws.Cells(1, 3) = "TS"
        ws.Cells(1, 4) = "XS"
        ws.Cells(1, 5) = "S"
        ws.Cells(1, 6) = "M"
        ws.Cells(1, 7) = "L"
        ws.Cells(1, 8) = "XL"
        ws.Cells(1, 9) = "2XL"
        ws.Cells(1, 10) = "3XL"
        ws.Cells(1, 11) = "4XL"
        ws.Cells(1, 12) = "SP"
        For i As Integer = 0 To DataGridView2.Rows.Count - 2
            For j As Integer = 0 To 11
                ws.Cells(i + 2, j + 1) = DataGridView2.Rows(i).Cells(j).Value
            Next
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

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        index2 = e.RowIndex
    End Sub

    Private Sub s16_GotFocus(sender As Object, e As EventArgs) Handles s16.GotFocus, ts.GotFocus, xs.GotFocus, s.GotFocus, m.GotFocus, l.GotFocus, xl.GotFocus, xl2.GotFocus, xl3.GotFocus, xl4.GotFocus, SP.GotFocus
        sender.Select(0, Me.Text.Length)
    End Sub

    Private Sub xs_KeyDown(sender As Object, e As KeyEventArgs) Handles s16.KeyDown, ts.KeyDown, xs.KeyDown, s.KeyDown, m.KeyDown, l.KeyDown, xl.KeyDown, xl2.KeyDown, xl3.KeyDown, xl4.KeyDown, SP.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button4_Click(sender, e)
        End If
    End Sub
End Class
