using System;
using System.Collections.Generic;
using System.Data;
using LibraryManagement.DAL;
using LibraryManagement.DTO;

namespace LibraryManagement.BUS
{
    public class ThuVienBUS
    {
        private readonly ThuVienDAL dal = new ThuVienDAL();

        // Đầu sách
        public DataTable GetAllDauSach() => dal.GetAllDauSach();
        public bool ThemDauSach(DauSachDTO sach)
        {
            if (string.IsNullOrWhiteSpace(sach.TenSach)) throw new Exception("Tên sách không được để trống!");
            if (sach.SoLuongCon < 0) throw new Exception("Số lượng phải >= 0!");
            return dal.ThemDauSach(sach);
        }
        public bool SuaDauSach(DauSachDTO sach)
        {
            if (sach.MaSach <= 0) throw new Exception("Vui lòng chọn sách cần sửa!");
            if (string.IsNullOrWhiteSpace(sach.TenSach)) throw new Exception("Tên sách không được để trống!");
            return dal.SuaDauSach(sach);
        }
        public bool XoaDauSach(int maSach) => dal.XoaDauSach(maSach);

        // Độc giả
        public DataTable GetAllDocGia() => dal.GetAllDocGia();
        public bool ThemDocGia(DocGiaDTO dg)
        {
            if (string.IsNullOrWhiteSpace(dg.HoTen)) throw new Exception("Họ tên không được để trống!");
            return dal.ThemDocGia(dg);
        }
        public bool SuaDocGia(DocGiaDTO dg)
        {
            if (dg.MaDocGia <= 0) throw new Exception("Vui lòng chọn độc giả cần sửa!");
            return dal.SuaDocGia(dg);
        }
        public bool XoaDocGia(int maDocGia) => dal.XoaDocGia(maDocGia);

        // Mượn - Trả
        public bool LapPhieuMuon(int maDocGia, int maSach, int soLuong, DateTime hanTra)
        {
            if (!dal.KiemTraTheConHan(maDocGia))
                throw new Exception("Thẻ độc giả đã hết hạn! Không thể lập phiếu mượn.");
            if (hanTra.Date < DateTime.Now.Date)
                throw new Exception("Hạn trả phải lớn hơn hoặc bằng ngày hiện tại!");
            if (soLuong <= 0)
                throw new Exception("Số lượng mượn phải > 0!");

            return dal.LapPhieuMuon(maDocGia, maSach, soLuong, hanTra);
        }

        public DataTable LocPhieuMuon(int maDocGia, string trangThai) => dal.LocPhieuMuon(maDocGia, trangThai);

        public bool TraSach(int maPhieu) => dal.TraSach(maPhieu);

        // Cảnh báo & Thống kê
        public List<PhieuMuonDTO> GetDanhSachQuaHan() => dal.GetDanhSachQuaHan();
        public DataTable ThongKeTheoTheLoai() => dal.ThongKeTheoTheLoai();
        public int DemTongPhieuDangMuon() => dal.DemTongPhieuDangMuon();
    }
}