using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LibraryManagement.DTO;

namespace LibraryManagement.DAL
{
    public class ThuVienDAL
    {
        // ==================== 1. QUẢN LÝ ĐẦU SÁCH ====================
        public DataTable GetAllDauSach()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DauSach", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool ThemDauSach(DauSachDTO sach)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "INSERT INTO DauSach (TenSach, TheLoai, SoLuongCon) VALUES (@TenSach, @TheLoai, @SoLuongCon)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TenSach", sach.TenSach);
                cmd.Parameters.AddWithValue("@TheLoai", sach.TheLoai);
                cmd.Parameters.AddWithValue("@SoLuongCon", sach.SoLuongCon);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SuaDauSach(DauSachDTO sach)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "UPDATE DauSach SET TenSach=@TenSach, TheLoai=@TheLoai, SoLuongCon=@SoLuongCon WHERE MaSach=@MaSach";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSach", sach.MaSach);
                cmd.Parameters.AddWithValue("@TenSach", sach.TenSach);
                cmd.Parameters.AddWithValue("@TheLoai", sach.TheLoai);
                cmd.Parameters.AddWithValue("@SoLuongCon", sach.SoLuongCon);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaDauSach(int maSach)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "DELETE FROM DauSach WHERE MaSach=@MaSach";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // ==================== 2. QUẢN LÝ ĐỘC GIẢ ====================
        public DataTable GetAllDocGia()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM DocGia", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool ThemDocGia(DocGiaDTO dg)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "INSERT INTO DocGia (HoTen, Email, NgayHetHanThe) VALUES (@HoTen, @Email, @NgayHetHan)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@HoTen", dg.HoTen);
                cmd.Parameters.AddWithValue("@Email", dg.Email);
                cmd.Parameters.AddWithValue("@NgayHetHan", dg.NgayHetHanThe);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool SuaDocGia(DocGiaDTO dg)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "UPDATE DocGia SET HoTen=@HoTen, Email=@Email, NgayHetHanThe=@NgayHetHan WHERE MaDocGia=@MaDocGia";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDocGia", dg.MaDocGia);
                cmd.Parameters.AddWithValue("@HoTen", dg.HoTen);
                cmd.Parameters.AddWithValue("@Email", dg.Email);
                cmd.Parameters.AddWithValue("@NgayHetHan", dg.NgayHetHanThe);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool XoaDocGia(int maDocGia)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "DELETE FROM DocGia WHERE MaDocGia=@MaDocGia";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDocGia", maDocGia);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool KiemTraTheConHan(int maDocGia)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM DocGia WHERE MaDocGia=@MaDocGia AND NgayHetHanThe >= CAST(GETDATE() AS DATE)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDocGia", maDocGia);
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        // ==================== 3. MƯỢN / TRẢ & LỌC PHIẾU ====================
        public bool LapPhieuMuon(int maDocGia, int maSach, int soLuong, DateTime hanTra)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. Tạo phiếu mượn và lấy MaPhieu vừa sinh
                    string sqlPM = "INSERT INTO PhieuMuon (MaDocGia, NgayMuon, HanTra, TrangThai) OUTPUT INSERTED.MaPhieu VALUES (@MaDocGia, GETDATE(), @HanTra, N'Đang mượn')";
                    SqlCommand cmdPM = new SqlCommand(sqlPM, conn, trans);
                    cmdPM.Parameters.AddWithValue("@MaDocGia", maDocGia);
                    cmdPM.Parameters.AddWithValue("@HanTra", hanTra);
                    int maPhieu = (int)cmdPM.ExecuteScalar();

                    // 2. Thêm chi tiết mượn
                    string sqlCT = "INSERT INTO ChiTietMuon (MaPhieu, MaSach, SoLuong) VALUES (@MaPhieu, @MaSach, @SoLuong)";
                    SqlCommand cmdCT = new SqlCommand(sqlCT, conn, trans);
                    cmdCT.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    cmdCT.Parameters.AddWithValue("@MaSach", maSach);
                    cmdCT.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmdCT.ExecuteNonQuery();

                    // 3. Trừ số lượng sách còn
                    string sqlSach = "UPDATE DauSach SET SoLuongCon = SoLuongCon - @SoLuong WHERE MaSach = @MaSach AND SoLuongCon >= @SoLuong";
                    SqlCommand cmdSach = new SqlCommand(sqlSach, conn, trans);
                    cmdSach.Parameters.AddWithValue("@SoLuong", soLuong);
                    cmdSach.Parameters.AddWithValue("@MaSach", maSach);
                    int rows = cmdSach.ExecuteNonQuery();
                    if (rows == 0) throw new Exception("Số lượng sách trong kho không đủ!");

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        public DataTable LocPhieuMuon(int maDocGia, string trangThai)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"
                    SELECT pm.MaPhieu, dg.HoTen AS DocGia, ds.TenSach, ct.SoLuong, pm.NgayMuon, pm.HanTra, pm.TrangThai
                    FROM PhieuMuon pm
                    JOIN DocGia dg ON pm.MaDocGia = dg.MaDocGia
                    JOIN ChiTietMuon ct ON pm.MaPhieu = ct.MaPhieu
                    JOIN DauSach ds ON ct.MaSach = ds.MaSach
                    WHERE (@MaDocGia = 0 OR pm.MaDocGia = @MaDocGia)
                      AND (@TrangThai = N'Tất cả' OR pm.TrangThai = @TrangThai)
                    ORDER BY pm.MaPhieu DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@MaDocGia", maDocGia);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public bool TraSach(int maPhieu)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                conn.Open();
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    // 1. Parameterized UPDATE trạng thái phiếu (chỉ cập nhật nếu chưa trả)
                    string sqlPM = "UPDATE PhieuMuon SET TrangThai = N'Đã trả' WHERE MaPhieu = @MaPhieu AND TrangThai != N'Đã trả'";
                    SqlCommand cmdPM = new SqlCommand(sqlPM, conn, trans);
                    cmdPM.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    int updated = cmdPM.ExecuteNonQuery();
                    if (updated == 0) return false;

                    // 2. Cộng lại SoLuongCon cho bảng DauSach
                    string sqlCongSach = @"
                        UPDATE ds
                        SET ds.SoLuongCon = ds.SoLuongCon + ct.SoLuong
                        FROM DauSach ds
                        INNER JOIN ChiTietMuon ct ON ds.MaSach = ct.MaSach
                        WHERE ct.MaPhieu = @MaPhieu";
                    SqlCommand cmdSach = new SqlCommand(sqlCongSach, conn, trans);
                    cmdSach.Parameters.AddWithValue("@MaPhieu", maPhieu);
                    cmdSach.ExecuteNonQuery();

                    trans.Commit();
                    return true;
                }
                catch
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        // ==================== 4. CẢNH BÁO QUÁ HẠN (SqlDataReader) & THỐNG KÊ ====================
        public List<PhieuMuonDTO> GetDanhSachQuaHan()
        {
            List<PhieuMuonDTO> list = new List<PhieuMuonDTO>();
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"
                    SELECT pm.MaPhieu, dg.HoTen, ds.TenSach, ct.SoLuong, pm.NgayMuon, pm.HanTra, pm.TrangThai
                    FROM PhieuMuon pm
                    JOIN DocGia dg ON pm.MaDocGia = dg.MaDocGia
                    JOIN ChiTietMuon ct ON pm.MaPhieu = ct.MaPhieu
                    JOIN DauSach ds ON ct.MaSach = ds.MaSach
                    WHERE pm.HanTra < GETDATE() AND pm.TrangThai != N'Đã trả'";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new PhieuMuonDTO
                        {
                            MaPhieu = reader.GetInt32(0),
                            HoTenDocGia = reader.GetString(1),
                            TenSachMuon = reader.GetString(2),
                            SoLuongMuon = reader.GetInt32(3),
                            NgayMuon = reader.GetDateTime(4),
                            HanTra = reader.GetDateTime(5),
                            TrangThai = reader.GetString(6)
                        });
                    }
                }
            }
            return list;
        }

        public DataTable ThongKeTheoTheLoai()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = @"
                    SELECT ds.TheLoai, COUNT(ct.MaPhieu) AS SoLuotMuon, ISNULL(SUM(ct.SoLuong), 0) AS TongCuonMuon
                    FROM DauSach ds
                    LEFT JOIN ChiTietMuon ct ON ds.MaSach = ct.MaSach
                    GROUP BY ds.TheLoai";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public int DemTongPhieuDangMuon()
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string sql = "SELECT COUNT(*) FROM PhieuMuon WHERE TrangThai != N'Đã trả'";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}