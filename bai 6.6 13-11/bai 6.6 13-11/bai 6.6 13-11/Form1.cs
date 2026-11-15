using System;
using System.Data;
using System.Windows.Forms;
using LibraryManagement.BUS;
using LibraryManagement.DTO;

namespace LibraryManagement
{
    public partial class Form1 : Form
    {
        private readonly ThuVienBUS bus = new ThuVienBUS();

        public Form1()
        {
            InitializeComponent();
            GanSuKien();
        }

        private void GanSuKien()
        {
            this.Load += Form1_Load;

            // Sự kiện Tab Đầu sách
            btnThemSach.Click += BtnThemSach_Click;
            btnSuaSach.Click += BtnSuaSach_Click;
            btnXoaSach.Click += BtnXoaSach_Click;
            btnLamMoiSach.Click += (s, e) => { txtMaSach.Clear(); txtTenSach.Clear(); nudSoLuongCon.Value = 0; };
            dgvSach.CellClick += DgvSach_CellClick;

            // Sự kiện Tab Độc giả
            btnThemDG.Click += BtnThemDG_Click;
            btnSuaDG.Click += BtnSuaDG_Click;
            btnXoaDG.Click += BtnXoaDG_Click;
            btnLamMoiDG.Click += (s, e) => { txtMaDG.Clear(); txtHoTen.Clear(); txtEmail.Clear(); dtpHetHan.Value = DateTime.Now; };
            dgvDocGia.CellClick += DgvDocGia_CellClick;

            // Sự kiện Tab Mượn Trả
            btnLapPhieu.Click += BtnLapPhieu_Click;
            btnLocPhieu.Click += (s, e) => LoadPhieuMuon();
            btnTraSach.Click += BtnTraSach_Click;

            // Sự kiện Tab Quá hạn & Thống kê
            btnKiemTraQuaHan.Click += BtnKiemTraQuaHan_Click;
            btnThongKe.Click += BtnThongKe_Click;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dtpHanTra.Value = DateTime.Now.AddDays(7);
            LoadDauSach();
            LoadDocGia();
            LoadPhieuMuon();
            LoadQuaHan();
            LoadThongKe();
        }

        // ==================== 1. ĐẦU SÁCH ====================
        private void LoadDauSach()
        {
            DataTable dt = bus.GetAllDauSach();
            dgvSach.DataSource = dt;

            // Đổ lên ComboBox mượn sách
            cboMuonSach.DataSource = dt.Copy();
            cboMuonSach.DisplayMember = "TenSach";
            cboMuonSach.ValueMember = "MaSach";
        }

        private void DgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvSach.Rows[e.RowIndex];
                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                txtTenSach.Text = row.Cells["TenSach"].Value.ToString();
                cboTheLoai.Text = row.Cells["TheLoai"].Value.ToString();
                nudSoLuongCon.Value = Convert.ToDecimal(row.Cells["SoLuongCon"].Value);
            }
        }

        private void BtnThemSach_Click(object sender, EventArgs e)
        {
            try
            {
                DauSachDTO sach = new DauSachDTO
                {
                    TenSach = txtTenSach.Text.Trim(),
                    TheLoai = cboTheLoai.Text.Trim(),
                    SoLuongCon = (int)nudSoLuongCon.Value
                };
                if (bus.ThemDauSach(sach))
                {
                    MessageBox.Show("Thêm đầu sách thành công!");
                    LoadDauSach();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnSuaSach_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMaSach.Text, out int maSach)) { MessageBox.Show("Chọn sách cần sửa!"); return; }
                DauSachDTO sach = new DauSachDTO
                {
                    MaSach = maSach,
                    TenSach = txtTenSach.Text.Trim(),
                    TheLoai = cboTheLoai.Text.Trim(),
                    SoLuongCon = (int)nudSoLuongCon.Value
                };
                if (bus.SuaDauSach(sach))
                {
                    MessageBox.Show("Cập nhật sách thành công!");
                    LoadDauSach();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnXoaSach_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMaSach.Text, out int maSach)) { MessageBox.Show("Chọn sách cần xóa!"); return; }
                if (MessageBox.Show("Xác nhận xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (bus.XoaDauSach(maSach))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadDauSach();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Không thể xóa sách đang có phiếu mượn!\n" + ex.Message); }
        }

        // ==================== 2. ĐỘC GIẢ ====================
        private void LoadDocGia()
        {
            DataTable dt = bus.GetAllDocGia();
            dgvDocGia.DataSource = dt;

            // ComboBox lập phiếu
            cboMuonDG.DataSource = dt.Copy();
            cboMuonDG.DisplayMember = "HoTen";
            cboMuonDG.ValueMember = "MaDocGia";

            // ComboBox lọc (thêm dòng Tất cả)
            DataTable dtLoc = dt.Copy();
            DataRow dr = dtLoc.NewRow();
            dr["MaDocGia"] = 0;
            dr["HoTen"] = "-- Tất cả độc giả --";
            dtLoc.Rows.InsertAt(dr, 0);
            cboLocDG.DataSource = dtLoc;
            cboLocDG.DisplayMember = "HoTen";
            cboLocDG.ValueMember = "MaDocGia";
        }

        private void DgvDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvDocGia.Rows[e.RowIndex];
                txtMaDG.Text = row.Cells["MaDocGia"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                dtpHetHan.Value = Convert.ToDateTime(row.Cells["NgayHetHanThe"].Value);
            }
        }

        private void BtnThemDG_Click(object sender, EventArgs e)
        {
            try
            {
                DocGiaDTO dg = new DocGiaDTO
                {
                    HoTen = txtHoTen.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    NgayHetHanThe = dtpHetHan.Value
                };
                if (bus.ThemDocGia(dg))
                {
                    MessageBox.Show("Thêm độc giả thành công!");
                    LoadDocGia();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnSuaDG_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMaDG.Text, out int maDG)) { MessageBox.Show("Chọn độc giả cần sửa!"); return; }
                DocGiaDTO dg = new DocGiaDTO
                {
                    MaDocGia = maDG,
                    HoTen = txtHoTen.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    NgayHetHanThe = dtpHetHan.Value
                };
                if (bus.SuaDocGia(dg))
                {
                    MessageBox.Show("Cập nhật độc giả thành công!");
                    LoadDocGia();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void BtnXoaDG_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtMaDG.Text, out int maDG)) { MessageBox.Show("Chọn độc giả cần xóa!"); return; }
                if (MessageBox.Show("Xác nhận xóa độc giả này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (bus.XoaDocGia(maDG))
                    {
                        MessageBox.Show("Xóa thành công!");
                        LoadDocGia();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Không thể xóa độc giả đang có lịch sử mượn!\n" + ex.Message); }
        }

        // ==================== 3. MƯỢN / TRẢ SÁCH ====================
        private void LoadPhieuMuon()
        {
            if (cboLocDG.SelectedValue == null || cboLocTrangThai.SelectedItem == null) return;
            int maDG = Convert.ToInt32(cboLocDG.SelectedValue);
            string trangThai = cboLocTrangThai.SelectedItem.ToString();
            dgvPhieuMuon.DataSource = bus.LocPhieuMuon(maDG, trangThai);
        }

        private void BtnLapPhieu_Click(object sender, EventArgs e)
        {
            try
            {
                int maDG = Convert.ToInt32(cboMuonDG.SelectedValue);
                int maSach = Convert.ToInt32(cboMuonSach.SelectedValue);
                int sl = (int)nudMuonSL.Value;
                DateTime hanTra = dtpHanTra.Value;

                if (bus.LapPhieuMuon(maDG, maSach, sl, hanTra))
                {
                    MessageBox.Show("Lập phiếu mượn thành công!");
                    LoadDauSach();
                    LoadPhieuMuon();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void BtnTraSach_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPhieuMuon.CurrentRow == null) { MessageBox.Show("Vui lòng chọn 1 phiếu trên bảng!"); return; }
                int maPhieu = Convert.ToInt32(dgvPhieuMuon.CurrentRow.Cells["MaPhieu"].Value);
                string trangThai = dgvPhieuMuon.CurrentRow.Cells["TrangThai"].Value.ToString();

                if (trangThai == "Đã trả")
                {
                    MessageBox.Show("Phiếu này đã được trả trước đó!");
                    return;
                }

                if (bus.TraSach(maPhieu))
                {
                    MessageBox.Show("Trả sách thành công! Đã cập nhật lại kho sách.");
                    LoadDauSach();
                    LoadPhieuMuon();
                    LoadQuaHan();
                    LoadThongKe();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi trả sách: " + ex.Message); }
        }

        // ==================== 4. QUÁ HẠN & THỐNG KÊ ====================
        private void LoadQuaHan()
        {
            dgvQuaHan.DataSource = bus.GetDanhSachQuaHan();
        }

        private void BtnKiemTraQuaHan_Click(object sender, EventArgs e)
        {
            LoadQuaHan();
            MessageBox.Show($"Hiện có {dgvQuaHan.Rows.Count} phiếu mượn đang quá hạn chưa trả!", "Cảnh báo quá hạn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void LoadThongKe()
        {
            dgvThongKe.DataSource = bus.ThongKeTheoTheLoai();
            lblTongDangMuon.Text = $"Tổng số phiếu chưa trả (ExecuteScalar): {bus.DemTongPhieuDangMuon()}";
        }

        private void BtnThongKe_Click(object sender, EventArgs e)
        {
            LoadThongKe();
        }
    }
}