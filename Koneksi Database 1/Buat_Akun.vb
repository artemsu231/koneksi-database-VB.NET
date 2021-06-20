Imports System.Data
Imports System.Data.OleDb
Public Class Buat_Akun
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Then
            MsgBox("Data Belum Lengkap...!!")
            TextBox1.Focus()
            Exit Sub
        Else
            cmd = New OleDbCommand("Select * From Pemakai where SKode_Pmk='" & TextBox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                Dim Simpan As String = "insert into Pemakai(Kode_Pmk,Nama_Pmk,Status_Pmk,Password_Pmk)values" & "('" & _
                    TextBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "')"
                cmd = New OleDbCommand(Simpan, Conn)
                cmd.ExecuteNonQuery()
                MsgBox("Simpan Data Sukses...!", MsgBoxStyle.Information, "Perhatian")
            End If
            TextBox1.Focus()
        End If
    End Sub
End Class