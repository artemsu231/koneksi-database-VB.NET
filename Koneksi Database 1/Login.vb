Imports System.Data.OleDb
Public Class Login

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Koneksi()
        Str = "Select * From Pemakai where Nama_pmk='" & TextBox1.Text & "' and Password_Pmk='" & TextBox2.Text & "'"
        cmd = New OleDbCommand(Str, Conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows Then
            Me.Visible = False
            FMenu.Show()
            FMenu.ToolStripStatusLabel1.Text = rd.GetString(0)
            FMenu.ToolStripStatusLabel2.Text = rd.GetString(1)
            FMenu.ToolStripStatusLabel3.Text = rd.GetString(2)
            If FMenu.ToolStripStatusLabel3.Text <> "ADMINISTRATOR" Then
                FMenu.PemakaiToolStripMenuItem.Enabled = False
            Else
                FMenu.PemakaiToolStripMenuItem.Enabled = True
            End If
        Else
            MsgBox("login salah, periksa kembali Nama Pemakai dan Password Anda...!")
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        If e.KeyChar = Chr(13) Then TextBox2.Focus()
    End Sub

    Private Sub TextBox2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox2.KeyPress
        If e.KeyChar = Chr(13) Then Button1.Focus()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox2.Clear()
    End Sub
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Buat_Akun.Show()
    End Sub
End Class