Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports Schedule_Management.ConnectionString
Public Class Form1
    Dim cmd As SqlCommand
    Dim dp As SqlDataAdapter
    Dim ds As DataSet
    Dim bulider As SqlCommandBuilder
    Public Sub load_DataGrid(ByVal _list As GridControl, ByVal _sql As String, ByVal _value As String)
        Try
            If connStr.State <> ConnectionState.Open Then
                connStr.Open()
            End If
            cmd = New SqlCommand(_sql, connStr)
            dp = New SqlDataAdapter(cmd)
            bulider = New SqlCommandBuilder(dp)
            ds = New DataSet()
            dp.Fill(ds, _value)
            Dim dtable As DataTable = ds.Tables(_value)
            If connStr.State <> ConnectionState.Closed Then
                connStr.Close()
            End If
            _list.DataSource = ds.Tables(_value)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If connStr.State <> ConnectionState.Closed Then
                connStr.Close()
            End If
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        load_DataGrid(controlShift, "Select * from tbl_Shift", "tbl_shift")
        If cboShift.Text = "" Then
            cboDay.Enabled = False
            cboTime.Enabled = False
        ElseIf cboShift.Text IsNot "" Then
            cboDay.Enabled = False
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Try
            If connStr.State <> ConnectionState.Open Then
                connStr.Open()
            End If
            Dim queryInsert = "INSERT INTO tbl_shift VALUES ('" + cboShift.SelectedItem + "','" + cboDay.SelectedItem + "','" + cboTime.Text + "','" + txtSDT.Text + "')"
            cmd = New SqlCommand(queryInsert, connStr)

            If txtSDT.TextLength < 3 Then
                MsgBox("Somthing Worng !")
            ElseIf txtSDT.TextLength = 3 Then
                cmd.ExecuteNonQuery()
                MsgBox("Sub")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If connStr.State <> ConnectionState.Closed Then
                connStr.Close()
            End If
        End Try
    End Sub

    Private Sub txtSDT_Click(sender As Object, e As EventArgs) Handles txtSDT.Click

    End Sub

    Private Sub cboShift_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboShift.SelectedIndexChanged
        If cboShift.Text IsNot "" Then
            cboDay.Enabled = True
            txtSDT.Clear()
            txtSDT.Text = cboShift.Text.Substring(0, 1)
            cboTime.Items.Clear()
            If cboShift.Text = "Afternoon" Then
                cboTime.Items.Add("1h:00-2h:25")
                cboTime.Items.Add("2h:30-15h:50")
                cboTime.Items.Add("15:55-17h:15")
            ElseIf cboShift.Text = "Evening" Then
                cboTime.Items.Add("17h:30-18h:50")
                cboTime.Items.Add("19h:00-20h:30")
            ElseIf cboShift.Text = "Morning" Then
                cboTime.Items.Add("7h:00-8h:30")
                cboTime.Items.Add("8h:45- 9h:30")
                cboTime.Items.Add("10h:45-11h:55")
            End If
        End If
        If cboDay.Text IsNot "" And cboTime.Text = "" Then
            txtSDT.Clear()
            txtSDT.Text = cboShift.Text.Substring(0, 1) + cboDay.Text.Substring(0, 1)
        ElseIf cboDay.Text IsNot "" And cboTime.Text IsNot "" Then
            Dim _str As String
            If cboTime.SelectedItem = "7h:00-8h:30" Then
                _str = "1"
            ElseIf (cboTime.SelectedItem = "8h:45-9h:30") Then
                _str = "2"
            ElseIf (cboTime.SelectedItem = "10h:45-11h:55") Then
                _str = "3"
            ElseIf (cboTime.SelectedItem = "1h:00-2h:25") Then
                _str = "1"
            ElseIf (cboTime.SelectedItem = "2h:30-15h:50") Then
                _str = "2"
            ElseIf (cboTime.SelectedItem = "15:55-17h:15") Then
                _str = "3"

            ElseIf (cboTime.SelectedItem = "17h:30-18h:50") Then
                _str = "1"

            ElseIf (cboTime.SelectedItem = "19h:00-20h:30") Then
                _str = "2"
            End If
            txtSDT.Clear()
            txtSDT.Text = cboShift.Text.Substring(0, 1) + cboDay.Text.Substring(0, 1)
        End If



    End Sub
    Private Sub cboDay_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDay.SelectedIndexChanged
        If cboDay.Text IsNot "" Then
            cboTime.Enabled = True
        End If
        txtSDT.Clear()
        Dim SESSION As String = cboShift.Text.Substring(0, 1)
        txtSDT.Text = cboShift.Text.Substring(0, 1) + cboDay.Text.Substring(0, 1)
    End Sub


    Private Sub cboTime_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTime.SelectedIndexChanged
        Dim _str As String
        If cboTime.SelectedItem = "7h:00-8h:30" Then
            _str = "1"
        ElseIf (cboTime.SelectedItem = "8h:45-9h:30") Then
            _str = "2"
        ElseIf (cboTime.SelectedItem = "10h:45-11h:55") Then
            _str = "3"
        ElseIf (cboTime.SelectedItem = "1h:00-2h:25") Then
            _str = "1"
        ElseIf (cboTime.SelectedItem = "2h:30-15h:50") Then
            _str = "2"
        ElseIf (cboTime.SelectedItem = "15:55-17h:15") Then
            _str = "3"

        ElseIf (cboTime.SelectedItem = "17h:30-18h:50") Then
            _str = "1"

        ElseIf (cboTime.SelectedItem = "19h:00-20h:30") Then
            _str = "2"
        End If
        txtSDT.Clear()
        Dim SESSION As String = cboShift.Text.Substring(0, 1)
        txtSDT.Text = cboShift.Text.Substring(0, 1) + cboDay.Text.Substring(0, 1) + _str
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Call clearText(Me)
    End Sub


    Public Sub clearText(ByVal Frm As Form)
        Dim Ctl As Control
        For Each Ctl In Frm.Controls
            If TypeOf Ctl Is TextBox Then Ctl.Text = ""
            If TypeOf Ctl Is GroupBox Then
                Dim Ctl1 As Control
                For Each Ctl1 In Ctl.Controls
                    If TypeOf Ctl1 Is TextBox Then
                        Ctl1.Text = ""
                    End If
                Next
            End If
        Next
    End Sub
End Class
