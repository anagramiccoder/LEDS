Imports MySql.Data.MySqlClient
Imports System.Data.Sql
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class simula
    Dim sauce As String = "localhost"
    Dim con As New MySqlConnection("Server=" & sauce & ";User Id=ledsilocos;Password=ilocos2020;Database=oos")
    Private Sub loading_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Hide()
        Try
            con.Open()
            con.Close()
            Dim f As New Loginn
            f.sauce = sauce
            f.Show()
            Me.Close()
        Catch ex As MySqlException
            MessageBox.Show(ex.ToString)
            MessageBox.Show("DATABASE NOT LOCATED")
            Me.Show()
            Me.TextBox1.Focus()
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Not TextBox1.Text = "" Then
            sauce = TextBox1.Text
            Try
                con.Open()
                con.Close()
                Dim f As New Loginn
                f.sauce = sauce
                f.Show()
                Me.Close()
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
                MessageBox.Show("DATABASE NOT LOCATED")
                Me.TextBox1.Focus()
            End Try
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            Button1_Click(sender, e)
        End If
    End Sub
End Class