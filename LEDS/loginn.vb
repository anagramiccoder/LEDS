Imports MySql.Data.MySqlClient
Imports System.Data.Sql
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class Loginn
    Public sauce = "localhost"
    Dim Cs As String = "Database=oos;Data Source=localhost;" _
            & "User Id=ledsilocos;Password=ilocos2020"
    Dim con As New MySqlConnection(Cs)
    Dim choose As Boolean = True
    Dim strin() As String = {"show", "hide"}
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        connect()
        Dim cmd As New MySqlCommand
        With cmd
            .Connection = con
            .CommandText = "SELECT `pass` FROM `acc` WHERE `accname`=@a"
            .Prepare()
            .Parameters.AddWithValue("@a", user.Text)
            Try
                If pass.Text = "" Or user.Text = "" Then
                    MessageBox.Show("Items can not " & vbCrLf & " be blank", "ERROR")
                    user.Focus()
                ElseIf .ExecuteScalar = pass.Text Then
                    Dim f As New Form1
                    .CommandText = "SELECT `acclvl` FROM `acc` WHERE `accname`=@a"
                    f.lvl = Integer.Parse(.ExecuteScalar)
                    f.Account = user.Text
                    .CommandText = "UPDATE `acc` SET `lastlog`=@b WHERE `accname`=@a"
                    .Prepare()
                    .Parameters.AddWithValue("@b", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"))
                    .ExecuteNonQuery()
                    f.acc()
                    f.Show()
                    Me.Close()
                Else
                    MessageBox.Show("ACCOUNT CREDENTIALS DO NOT MATCH OR ACCOUNT DOES NOT EXIST", "ERROR")
                    user.Focus()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End With
    End Sub
    Private Sub connect()
        If con.State = ConnectionState.Open Then
            con.Close()
        End If
        con.Open()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Loginn_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pass.PasswordChar = "*"
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        choose = Not choose
        If choose Then
            pass.PasswordChar = ""
            PictureBox1.BackgroundImage = My.Resources.hide
        Else
            pass.PasswordChar = "*"
            PictureBox1.BackgroundImage = My.Resources.show
        End If
    End Sub

    Private Sub user_KeyDown(sender As Object, e As KeyEventArgs) Handles user.KeyDown, pass.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)
        End If
    End Sub
End Class