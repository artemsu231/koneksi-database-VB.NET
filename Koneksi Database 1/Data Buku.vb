Imports System.Data.OleDb
Public Class Data_Buku
    Sub Kosong()
        TextBox1.Clear()
        ComboBox1.Text = ""
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        TextBox8.Clear()
        TextBox1.Focus()
    End Sub

    Sub Isi()
        ComboBox1.Text = ""
        TextBox2.Clear()
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox7.Clear()
        ComboBox1.Focus()
    End Sub


    Sub TampilBuku()
        da = New OleDbDataAdapter("SELECT * FROM Buku", Conn)
        ds = New DataSet
        ds.Clear()
        da.Fill(ds, "Buku")
        DataGridView1.DataSource = ds.Tables("Buku")
        DataGridView1.Refresh()
    End Sub

    Sub TampilJenis()
        cmd = New OleDbCommand("SELECT kodeJenis From jenis", Conn)
        rd = cmd.ExecuteReader
        Do While rd.Read
            ComboBox1.Items.Add(rd.Item(0))
        Loop
    End Sub

    Sub AturGrid()
        'untuk mengatur luas Columns pada DataGridView'
        DataGridView1.Columns(0).Width = 60
        DataGridView1.Columns(1).Width = 50
        DataGridView1.Columns(2).Width = 300
        DataGridView1.Columns(3).Width = 100
        DataGridView1.Columns(4).Width = 100
        DataGridView1.Columns(5).Width = 100
        DataGridView1.Columns(6).Width = 100
        DataGridView1.Columns(7).Width = 300

        'Untuk menampilkan judul header pada DataGridView'
        DataGridView1.Columns(0).HeaderText = "KODE BUKU"
        DataGridView1.Columns(1).HeaderText = "KODE JENIS"
        DataGridView1.Columns(2).HeaderText = "JUDUL"
        DataGridView1.Columns(3).HeaderText = "PENGARANG"
        DataGridView1.Columns(4).HeaderText = "PENERBIT"
        DataGridView1.Columns(5).HeaderText = "JUMLAH"
        DataGridView1.Columns(6).HeaderText = "HARGA"
        DataGridView1.Columns(7).HeaderText = "DESKRIPSI"
    End Sub

    Private Sub Data_Buku_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call Koneksi()
        Call TampilJenis()
        Call TampilBuku()
        Call Kosong()
        Call AturGrid()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        cmd = New OleDbCommand("Select * From Jenis where KodeJenis='" & ComboBox1.Text & "'", Conn)
        rd = cmd.ExecuteReader
        rd.Read()
        If rd.HasRows = True Then
            TextBox8.Text = rd.Item(1)
        Else
            MsgBox("Kode Jenis ini tidak terdaftar")
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or ComboBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
            MsgBox("Data Belum Lengkap...!")
            TextBox1.Focus()
            Exit Sub
        Else
            cmd = New OleDbCommand("Select * From Buku where KodeBuku='" & TextBox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                Dim Simpan As String = "insert into Buku(KodeBuku,KodeJenis,Judul,Pengarang,Penerbit,JumlahBuku,Harga,Deskripsi)values" & "('" & TextBox1.Text & "','" & ComboBox1.Text & "','" & TextBox2.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "','" & TextBox6.Text & "','" & TextBox7.Text & "')"
                cmd = New OleDbCommand(Simpan, Conn)
                cmd.ExecuteNonQuery()
                MsgBox("Simpan data sukses...!", MsgBoxStyle.Information, "Perhatian")
            End If
            Call TampilBuku()
            Call Kosong()
            TextBox1.Focus()
        End If
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        TextBox1.MaxLength = 12
        If e.KeyChar = Chr(13) Then
            Call Koneksi()
            cmd = New OleDbCommand("Select * From Buku where kodeBuku='" & TextBox1.Text & "'", Conn)
            rd = cmd.ExecuteReader
            rd.Read()
            If Not rd.HasRows Then
                MsgBox("Kode Barang Tidak Ada...!")
                TextBox1.Focus()
            Else
                TextBox2.Text = rd.Item("judul")
                TextBox3.Text = rd.Item("pengarang")
                TextBox4.Text = rd.Item("penerbit")
                TextBox5.Text = rd.Item("jumlahBuku")
                TextBox6.Text = rd.Item("harga")
                TextBox7.Text = rd.Item("deskripsi")
                TextBox2.Focus()

            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Or ComboBox1.Text = "" Or TextBox2.Text = "" Or TextBox3.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Or TextBox6.Text = "" Then
            MsgBox("Data Belum Lengkap...!")
            TextBox1.Focus()
            Exit Sub
        Else
            Call Koneksi()
            Dim Ubah As String = "update Buku set judul='" & TextBox2.Text & "',kodeJenis='" & ComboBox1.Text & "',pengarang='" & TextBox3.Text & "',penerbit='" & TextBox4.Text & "',jumlahBuku='" & TextBox5.Text & "',harga='" & TextBox6.Text & "',deskripsi='" & TextBox7.Text & "' where kodeBuku='" & TextBox1.Text & "'"
            cmd = New OleDbCommand(Ubah, Conn)
            cmd.ExecuteNonQuery()
            MsgBox("data berhasil di update")
            Call TampilBuku()
            Call Kosong()
            TextBox1.Focus()
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If TextBox1.Text = "" Then
            MsgBox("silahkan pilih data yang ingin dihapus")
        Else
            If MessageBox.Show("Yakin akan dihapus ?", "", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
                Call Koneksi()
                Dim Hapus As String = "delete From Buku where kodeBuku='" & TextBox1.Text & "'"
                cmd = New OleDbCommand(Hapus, Conn)
                cmd.ExecuteNonQuery()
                MsgBox("data berhasil di hapus")
                Call TampilBuku()
                Call Kosong()
            End If
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Call Kosong()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Me.Close()
    End Sub
End Class