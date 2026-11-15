using System;

namespace LibraryManagement.DTO
{
    public class DocGiaDTO
    {
        public int MaDocGia { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public DateTime NgayHetHanThe { get; set; }
    }

    public class DauSachDTO
    {
        public int MaSach { get; set; }
        public string TenSach { get; set; }
        public string TheLoai { get; set; }
        public int SoLuongCon { get; set; }
    }

    public class PhieuMuonDTO
    {
        public int MaPhieu { get; set; }
        public int MaDocGia { get; set; }
        public string HoTenDocGia { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime HanTra { get; set; }
        public string TrangThai { get; set; }
        public string TenSachMuon { get; set; }
        public int SoLuongMuon { get; set; }
    }
}