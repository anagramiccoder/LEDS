Imports MySql.Data.MySqlClient
Imports System.Data.Sql
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class Order
    Public sauce = "localhost"
    Dim cs As String = "Database=oos;Data Source=" & sauce & ";" _
            & "User Id=ledsilocos;Password=ilocos2020"
    Dim con As New MySqlConnection(cs)
    Dim reader As MySqlDataReader
    Dim stats() As String = {"Processing", "Ready to Ship", "Shipped", "Delivered", "Cancelled", "Returned"}
    Dim cos, shi As Integer
    Dim go As Boolean = False
    Public lvl As Integer
    Dim account() As String = {"", ""}
    Public acc As String = ""
    Dim pord As String = ""
    Private Sub Order_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Text = Form1.Account
        Try
            con.Open()
        Catch ex As Exception

        End Try
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            .CommandText = "SELECT * FROM `ord` WHERE `ordnum`=@a"
            .Prepare()
            .Parameters.AddWithValue("@a", Integer.Parse(oc.Text))
            reader = .ExecuteReader
            reader.Read()
            user.Text = reader.GetString(1)
            cn.Text = reader.GetString(8)
            od.Text = reader.GetString(2)
            pord = od.Text
            add.Text = reader.GetString(3)
            stat.SelectedIndex = Array.IndexOf(stats, reader.GetString(4))
            tn.Text = reader.GetString(5)
            cos = reader.GetString(6)
            shi = reader.GetString(7)
            cost.Text = "Php " + cos.ToString + " + " + shi.ToString + " (Shipping Fee)"
            sf.Text = shi
            account = reader.GetString(11).Split(";")
            TextBox1.Text = reader.GetString(12)
        End With

    End Sub

    Public Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If Not stat.SelectedIndex = 0 Then
            tn.Enabled = True
        End If
        Button1.Text = "Update"
        Button1.TabIndex = 13
        Button2.Visible = False
        Label9.Visible = True
        sf.Visible = True
        If lvl = 2 Then
            stat.Enabled = True
        ElseIf lvl = 1 Then
            od.Enabled = True
            user.Enabled = True
            cn.Enabled = True
            add.Enabled = True
            cost.Enabled = True
        End If
    End Sub

    Public Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Return" Then
            Me.Close()
        Else
            Dim g As Boolean = True
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            If Not pord = od.Text Then
                If Form1.Check(od.Text) Then
                    Form1.Update_inventory(od.Text)
                    Retinv(pord)
                Else
                    g = False
                End If
            End If
            If g Then
                If con.State = ConnectionState.Closed Then
                    con.Open()
                End If
                Dim cmd As New MySqlCommand
                With cmd
                    .Connection = con
                    .CommandText = "UPDATE `ord` SET `user`=@a,`ordered`=@b,`address`=@c,`status`=@d,`TN`=@e,`cost`=@f,`sf`=@g,`cn`=@h ,`accname`=@j, `remark`=@z WHERE `ordnum`=@i"
                    .Prepare()
                    .Parameters.AddWithValue("@a", user.Text)
                    .Parameters.AddWithValue("@b", od.Text)
                    .Parameters.AddWithValue("@c", add.Text)
                    .Parameters.AddWithValue("@d", stat.Text)
                    .Parameters.AddWithValue("@e", tn.Text)
                    .Parameters.AddWithValue("@f", cos)
                    .Parameters.AddWithValue("@z", TextBox1.Text)
                    If sf.Text = "" Then
                        sf.Text = 0
                    End If
                    .Parameters.AddWithValue("@g", Integer.Parse(sf.Text))
                    .Parameters.AddWithValue("@h", cn.Text)
                    .Parameters.AddWithValue("@i", Integer.Parse(oc.Text))
                    If lvl = 1 Then
                        .Parameters.AddWithValue("@j", acc + ";" + account(1))
                    Else
                        .Parameters.AddWithValue("@j", account(0) + ";" + acc)
                    End If
                    .ExecuteNonQuery()
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
                    .Parameters.RemoveAt("@z")
                End With
                If stat.Text = "Delivered" Then
                    With cmd
                        .Connection = con
                        .CommandText = "INSERT INTO `hist` SELECT * FROM `ord` WHERE `ordnum`=@num"
                        .Prepare()
                        .Parameters.AddWithValue("@num", Int32.Parse(oc.Text))
                        Try
                            .ExecuteNonQuery()
                        Catch ex As Exception
                        End Try
                        .CommandText = "DELETE FROM `ord` WHERE `ordnum`=@num"
                        Try
                            .ExecuteNonQuery()
                        Catch ex As Exception
                        End Try
                    End With
                ElseIf stat.SelectedIndex > 3 Then
                    Retinv(od.Text)
                    If con.State = ConnectionState.Closed Then
                        con.Open()
                    End If
                    With cmd
                        .Connection = con
                        .CommandText = "INSERT INTO `hist` SELECT * FROM `ord` WHERE `ordnum`=@num"
                        .Prepare()
                        .Parameters.AddWithValue("@num", Int32.Parse(oc.Text))
                        Try
                            .ExecuteNonQuery()
                        Catch ex As Exception
                        End Try
                        .CommandText = "DELETE FROM `ord` WHERE `ordnum`=@num"
                        Try
                            .ExecuteNonQuery()

                        Catch ex As Exception
                        End Try
                    End With
                End If
                con.Close()
                Form1.dg1ref()
                Me.Close()
            End If
        End If
    End Sub

    Private Sub stat_SelectedIndexChanged(sender As Object, e As EventArgs) Handles stat.SelectedIndexChanged
        If Not (stat.SelectedIndex = 0 Or stat.SelectedIndex = 1) Then
            tn.Enabled = True
        Else
            tn.Enabled = False
        End If
    End Sub

    Private Sub sf_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles sf.KeyPress
        If Asc(e.KeyChar) <> 13 AndAlso Asc(e.KeyChar) <> 8 AndAlso Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
    Public Sub Retinv(ByVal order As String)
        Dim slist() As String = {"s16", "ts", "xs", "s", "m", "l", "xl", "2xl", "3xl", "4xl", "sp"}
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
        For Each shirt In order.Split(";")
            Dim ncount() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
            Dim cmd As New MySqlCommand
            Dim code As String = shirt.Split(":")(0)
            For Each sz As String In shirt.Split(":")(1).Split(",")
                Dim count As Integer = Convert.ToInt32(sz.Split("-")(0))
                Dim sizes As String = sz.Split("-")(1)
                If sizes.Contains("upsized") Then
                    sizes = slist(Array.IndexOf(slist, sizes.Split("(")(0)) + 1)
                End If
                ncount(Array.IndexOf(slist, sizes)) = count
            Next sz
            With cmd
                .Connection = con
                .CommandText = " SELECT * FROM `shirt` WHERE `ID`= @id"
                .Prepare()
                .Parameters.AddWithValue("@id", code)
                reader = .ExecuteReader
                reader.Read()
                Dim pcount() As Integer = {reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9), reader.GetInt32(10), reader.GetInt32(11), reader.GetInt32(12)}
                reader.Close()
                .CommandText = "UPDATE `shirt`  SET `s16`=@a, `ts`=@b, `xs`=@c,`s`=@d, `m`=@e, `l`=@f, `xl`=@g, `2xl`=@h, `3xl`=@i, `4xl`=@j, `sp`=@k WHERE `ID`=@id"
                .Prepare()
                .Parameters.AddWithValue("@a", (ncount(0) + pcount(0)))
                .Parameters.AddWithValue("@b", (ncount(1) + pcount(1)))
                .Parameters.AddWithValue("@c", (ncount(2) + pcount(2)))
                .Parameters.AddWithValue("@d", (ncount(3) + pcount(3)))
                .Parameters.AddWithValue("@e", (ncount(4) + pcount(4)))
                .Parameters.AddWithValue("@f", (ncount(5) + pcount(5)))
                .Parameters.AddWithValue("@g", (ncount(6) + pcount(6)))
                .Parameters.AddWithValue("@h", (ncount(7) + pcount(7)))
                .Parameters.AddWithValue("@i", (ncount(8) + pcount(8)))
                .Parameters.AddWithValue("@j", (ncount(9) + pcount(9)))
                .Parameters.AddWithValue("@k", (ncount(10) + pcount(10)))
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
            End With
        Next shirt
    End Sub
End Class