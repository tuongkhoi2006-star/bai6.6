namespace LibraryManagement
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabSach;
        private System.Windows.Forms.TabPage tabDocGia;
        private System.Windows.Forms.TabPage tabMuonTra;
        private System.Windows.Forms.TabPage tabThongKe;

        // Tab Sách
        private System.Windows.Forms.DataGridView dgvSach;
        private System.Windows.Forms.TextBox txtMaSach, txtTenSach;
        private System.Windows.Forms.ComboBox cboTheLoai;
        private System.Windows.Forms.NumericUpDown nudSoLuongCon;
        private System.Windows.Forms.Button btnThemSach, btnSuaSach, btnXoaSach, btnLamMoiSach;

        // Tab Độc giả
        private System.Windows.Forms.DataGridView dgvDocGia;
        private System.Windows.Forms.TextBox txtMaDG, txtHoTen, txtEmail;
        private System.Windows.Forms.DateTimePicker dtpHetHan;
        private System.Windows.Forms.Button btnThemDG, btnSuaDG, btnXoaDG, btnLamMoiDG;

        // Tab Mượn Trả
        private System.Windows.Forms.ComboBox cboMuonDG, cboMuonSach, cboLocDG, cboLocTrangThai;
        private System.Windows.Forms.NumericUpDown nudMuonSL;
        private System.Windows.Forms.DateTimePicker dtpHanTra;
        private System.Windows.Forms.Button btnLapPhieu, btnLocPhieu, btnTraSach;
        private System.Windows.Forms.DataGridView dgvPhieuMuon;

        // Tab Quá hạn & Thống kê
        private System.Windows.Forms.Button btnKiemTraQuaHan, btnThongKe;
        private System.Windows.Forms.DataGridView dgvQuaHan, dgvThongKe;
        private System.Windows.Forms.Label lblTongDangMuon;

        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabSach = new System.Windows.Forms.TabPage();
            this.tabDocGia = new System.Windows.Forms.TabPage();
            this.tabMuonTra = new System.Windows.Forms.TabPage();
            this.tabThongKe = new System.Windows.Forms.TabPage();

            this.SuspendLayout();
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.tabMain.Controls.AddRange(new System.Windows.Forms.Control[] { tabSach, tabDocGia, tabMuonTra, tabThongKe });

            // ==================== 1. TAB ĐẦU SÁCH ====================
            this.tabSach.Text = "Quản lý Đầu sách";
            var lbl1 = new System.Windows.Forms.Label { Text = "Mã sách:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            this.txtMaSach = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(110, 17), Width = 120, ReadOnly = true };
            var lbl2 = new System.Windows.Forms.Label { Text = "Tên sách:", Location = new System.Drawing.Point(260, 20), AutoSize = true };
            this.txtTenSach = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(340, 17), Width = 280 };
            var lbl3 = new System.Windows.Forms.Label { Text = "Thể loại:", Location = new System.Drawing.Point(20, 58), AutoSize = true };
            this.cboTheLoai = new System.Windows.Forms.ComboBox { Location = new System.Drawing.Point(110, 55), Width = 200, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown };
            this.cboTheLoai.Items.AddRange(new object[] { "Công nghệ thông tin", "Kinh tế", "Văn học", "Ngoại ngữ", "Khoa học" });
            var lbl4 = new System.Windows.Forms.Label { Text = "Số lượng:", Location = new System.Drawing.Point(340, 58), AutoSize = true };
            this.nudSoLuongCon = new System.Windows.Forms.NumericUpDown { Location = new System.Drawing.Point(420, 55), Width = 100, Maximum = 10000 };

            this.btnThemSach = new System.Windows.Forms.Button { Text = "Thêm", Location = new System.Drawing.Point(650, 15), Width = 90 };
            this.btnSuaSach = new System.Windows.Forms.Button { Text = "Sửa", Location = new System.Drawing.Point(750, 15), Width = 90 };
            this.btnXoaSach = new System.Windows.Forms.Button { Text = "Xóa", Location = new System.Drawing.Point(650, 53), Width = 90 };
            this.btnLamMoiSach = new System.Windows.Forms.Button { Text = "Làm mới", Location = new System.Drawing.Point(750, 53), Width = 90 };

            this.dgvSach = new System.Windows.Forms.DataGridView { Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(830, 380), AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect, ReadOnly = true, AllowUserToAddRows = false };

            this.tabSach.Controls.AddRange(new System.Windows.Forms.Control[] { lbl1, txtMaSach, lbl2, txtTenSach, lbl3, cboTheLoai, lbl4, nudSoLuongCon, btnThemSach, btnSuaSach, btnXoaSach, btnLamMoiSach, dgvSach });

            // ==================== 2. TAB ĐỘC GIẢ ====================
            this.tabDocGia.Text = "Quản lý Độc giả";
            var d1 = new System.Windows.Forms.Label { Text = "Mã ĐG:", Location = new System.Drawing.Point(20, 20), AutoSize = true };
            this.txtMaDG = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(110, 17), Width = 120, ReadOnly = true };
            var d2 = new System.Windows.Forms.Label { Text = "Họ tên:", Location = new System.Drawing.Point(260, 20), AutoSize = true };
            this.txtHoTen = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(340, 17), Width = 280 };
            var d3 = new System.Windows.Forms.Label { Text = "Email:", Location = new System.Drawing.Point(20, 58), AutoSize = true };
            this.txtEmail = new System.Windows.Forms.TextBox { Location = new System.Drawing.Point(110, 55), Width = 220 };
            var d4 = new System.Windows.Forms.Label { Text = "Hạn thẻ:", Location = new System.Drawing.Point(350, 58), AutoSize = true };
            this.dtpHetHan = new System.Windows.Forms.DateTimePicker { Location = new System.Drawing.Point(420, 55), Width = 200, Format = System.Windows.Forms.DateTimePickerFormat.Short };

            this.btnThemDG = new System.Windows.Forms.Button { Text = "Thêm", Location = new System.Drawing.Point(650, 15), Width = 90 };
            this.btnSuaDG = new System.Windows.Forms.Button { Text = "Sửa", Location = new System.Drawing.Point(750, 15), Width = 90 };
            this.btnXoaDG = new System.Windows.Forms.Button { Text = "Xóa", Location = new System.Drawing.Point(650, 53), Width = 90 };
            this.btnLamMoiDG = new System.Windows.Forms.Button { Text = "Làm mới", Location = new System.Drawing.Point(750, 53), Width = 90 };

            this.dgvDocGia = new System.Windows.Forms.DataGridView { Location = new System.Drawing.Point(20, 100), Size = new System.Drawing.Size(830, 380), AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect, ReadOnly = true, AllowUserToAddRows = false };

            this.tabDocGia.Controls.AddRange(new System.Windows.Forms.Control[] { d1, txtMaDG, d2, txtHoTen, d3, txtEmail, d4, dtpHetHan, btnThemDG, btnSuaDG, btnXoaDG, btnLamMoiDG, dgvDocGia });

            // ==================== 3. TAB MƯỢN / TRẢ ====================
            this.tabMuonTra.Text = "Mượn / Trả sách";
            var grpMuon = new System.Windows.Forms.GroupBox { Text = "Lập phiếu mượn mới", Location = new System.Drawing.Point(20, 15), Size = new System.Drawing.Size(830, 100) };
            var m1 = new System.Windows.Forms.Label { Text = "Độc giả:", Location = new System.Drawing.Point(15, 30), AutoSize = true };
            this.cboMuonDG = new System.Windows.Forms.ComboBox { Location = new System.Drawing.Point(80, 27), Width = 200, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };
            var m2 = new System.Windows.Forms.Label { Text = "Sách:", Location = new System.Drawing.Point(300, 30), AutoSize = true };
            this.cboMuonSach = new System.Windows.Forms.ComboBox { Location = new System.Drawing.Point(350, 27), Width = 240, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };
            var m3 = new System.Windows.Forms.Label { Text = "SL:", Location = new System.Drawing.Point(610, 30), AutoSize = true };
            this.nudMuonSL = new System.Windows.Forms.NumericUpDown { Location = new System.Drawing.Point(640, 27), Width = 60, Minimum = 1, Value = 1 };
            var m4 = new System.Windows.Forms.Label { Text = "Hạn trả:", Location = new System.Drawing.Point(15, 65), AutoSize = true };
            this.dtpHanTra = new System.Windows.Forms.DateTimePicker { Location = new System.Drawing.Point(80, 62), Width = 200, Format = System.Windows.Forms.DateTimePickerFormat.Short };
            this.btnLapPhieu = new System.Windows.Forms.Button { Text = "Lập phiếu mượn", Location = new System.Drawing.Point(350, 60), Width = 160, Height = 28 };
            grpMuon.Controls.AddRange(new System.Windows.Forms.Control[] { m1, cboMuonDG, m2, cboMuonSach, m3, nudMuonSL, m4, dtpHanTra, btnLapPhieu });

            var f1 = new System.Windows.Forms.Label { Text = "Lọc Độc giả:", Location = new System.Drawing.Point(20, 130), AutoSize = true };
            this.cboLocDG = new System.Windows.Forms.ComboBox { Location = new System.Drawing.Point(110, 127), Width = 200, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };
            var f2 = new System.Windows.Forms.Label { Text = "Trạng thái:", Location = new System.Drawing.Point(330, 130), AutoSize = true };
            this.cboLocTrangThai = new System.Windows.Forms.ComboBox { Location = new System.Drawing.Point(410, 127), Width = 150, DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList };
            this.cboLocTrangThai.Items.AddRange(new object[] { "Tất cả", "Đang mượn", "Đã trả", "Quá hạn" });
            this.cboLocTrangThai.SelectedIndex = 0;

            this.btnLocPhieu = new System.Windows.Forms.Button { Text = "Lọc danh sách", Location = new System.Drawing.Point(580, 125), Width = 120 };
            this.btnTraSach = new System.Windows.Forms.Button { Text = "Trả sách đã chọn", Location = new System.Drawing.Point(710, 125), Width = 140 };

            this.dgvPhieuMuon = new System.Windows.Forms.DataGridView { Location = new System.Drawing.Point(20, 165), Size = new System.Drawing.Size(830, 315), AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect, ReadOnly = true, AllowUserToAddRows = false };

            this.tabMuonTra.Controls.AddRange(new System.Windows.Forms.Control[] { grpMuon, f1, cboLocDG, f2, cboLocTrangThai, btnLocPhieu, btnTraSach, dgvPhieuMuon });

            // ==================== 4. TAB QUÁ HẠN & THỐNG KÊ ====================
            this.tabThongKe.Text = "Cảnh báo Quá hạn & Thống kê";
            this.btnKiemTraQuaHan = new System.Windows.Forms.Button { Text = "Cảnh báo Sách Quá Hạn (SqlDataReader)", Location = new System.Drawing.Point(20, 15), Width = 280, Height = 30 };
            this.dgvQuaHan = new System.Windows.Forms.DataGridView { Location = new System.Drawing.Point(20, 55), Size = new System.Drawing.Size(830, 190), AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, AllowUserToAddRows = false };

            this.btnThongKe = new System.Windows.Forms.Button { Text = "Thống kê lượt mượn theo Thể loại", Location = new System.Drawing.Point(20, 260), Width = 250, Height = 30 };
            this.lblTongDangMuon = new System.Windows.Forms.Label { Text = "Tổng phiếu chưa trả: 0", Location = new System.Drawing.Point(300, 266), AutoSize = true, Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold) };
            this.dgvThongKe = new System.Windows.Forms.DataGridView { Location = new System.Drawing.Point(20, 300), Size = new System.Drawing.Size(830, 180), AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill, ReadOnly = true, AllowUserToAddRows = false };

            this.tabThongKe.Controls.AddRange(new System.Windows.Forms.Control[] { btnKiemTraQuaHan, dgvQuaHan, btnThongKe, lblTongDangMuon, dgvThongKe });

            // ==================== FORM1 ====================
            this.ClientSize = new System.Drawing.Size(880, 530);
            this.Controls.Add(this.tabMain);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Mượn Trả Sách Thư Viện (3-Tier)";
            this.ResumeLayout(false);
        }
    }
}