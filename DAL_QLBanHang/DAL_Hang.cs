using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QLBanHang;

namespace DAL_QLBanHang
{
   public class DAL_Hang : DBConnect
    {
        public DataTable getHang()
        {           
            //Direct sql query
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblHang", _conn);
            DataTable dtHang = new DataTable();
            da.Fill(dtHang);
            return dtHang;
            /*
            //Store Procedure
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DanhSachHang";
                cmd.Connection = _conn;
                DataTable dtHang = new DataTable();
                dtHang.Load(cmd.ExecuteReader());
                return dtHang;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }*/
            
        }
        public bool insertHang(DTO_Hang hang)
        {
            try
            {
                if (hang == null || string.IsNullOrEmpty(hang.TenHang) || hang.SoLuong <= 0 || hang.DonGiaNhap <= 0 || hang.DonGiaBan <= 0)
                {
                    Console.WriteLine("Dữ liệu đầu vào không hợp lệ.");
                    return false;
                }

                // Kết nối
                _conn.Open();

                using (SqlCommand cmd = new SqlCommand("InsertDataIntoTblHang", _conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TenHang", hang.TenHang);
                    cmd.Parameters.AddWithValue("@SoLuong", hang.SoLuong);
                    cmd.Parameters.AddWithValue("@DonGiaNhap", hang.DonGiaNhap);
                    cmd.Parameters.AddWithValue("@DonGiaBan", hang.DonGiaBan);
                    cmd.Parameters.AddWithValue("@HinhAnh", hang.HinhAnh ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", hang.GhiChu ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", hang.EmailNV ?? (object)DBNull.Value);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Số dòng bị ảnh hưởng: {rowsAffected}");

                    return rowsAffected > 0;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Lỗi khi thêm sản phẩm: {e.Message}");
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool UpdateHang(DTO_Hang hang)
        {
            /*
            try
            {
                // Ket noi
                _conn.Open();
                string SQL = string.Format("UPDATE tblhang SET TenHang = '{0}', SoLuong = {1}, DonGiaNhap={2}, DonGiaBan={3},"+
                    "HinhAnh = '{4}', GhiChu='{5}' WHERE MaHang = {6}", hang.TenHang, hang.SoLuong, hang.DonGiaNhap,
                    hang.DonGiaBan, hang.HinhAnh, hang.GhiChu, hang.MaHang);
                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;*/

            //using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateDataIntoTblHang";
                cmd.Parameters.AddWithValue("MaHang", hang.MaHang);
                cmd.Parameters.AddWithValue("TenHang", hang.TenHang);
                cmd.Parameters.AddWithValue("SoLuong", hang.SoLuong);
                cmd.Parameters.AddWithValue("DonGiaNhap", hang.DonGiaNhap);
                cmd.Parameters.AddWithValue("DonGiaBan", hang.DonGiaBan);
                cmd.Parameters.AddWithValue("HinhAnh", hang.HinhAnh);
                cmd.Parameters.AddWithValue("GhiChu", hang.GhiChu);
                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;
        }

        public bool DeleteHang(int maHang)
        {
            /* //Direct sql query
            try
            {
                // Ket noi
                _conn.Open();
                // Query string - vì xóa chỉ cần ID nên chúng ta ko cần 1 DTO, ID là đủ
                string SQL = string.Format("DELETE FROM tblHang WHERE MaHang = {0}",  maHang);
                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;  */
            // using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteDataFromtblHang";
                cmd.Parameters.AddWithValue("MaHang",maHang);
                cmd.Connection = _conn;
                // Query và kiểm tra
                if (cmd.ExecuteNonQuery() > 0)
                    return true;
            }
            catch (Exception e)
            {

            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;
        }

        public DataTable SearchHang(string tenHang)
        {         
            //Direct sql query
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblHang where TenHang like '%" + tenHang + "%'", _conn);
            DataTable dtHang = new DataTable();
            da.Fill(dtHang);
            return dtHang;
            /*
            //Store Procedure
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DanhSachHang";
                cmd.Connection = _conn;
                DataTable dtHang = new DataTable();
                dtHang.Load(cmd.ExecuteReader());
                return dtHang;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }*/
        }
        public int GetMaHangByTenHang(string tenHang)
        {
            int maHang = -1;

            try
            {
                _conn.Open();
                string query = "SELECT TOP 1 MaHang FROM tblHang WHERE TenHang = @tenHang ORDER BY MaHang DESC";
                SqlCommand cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@tenHang", tenHang);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    maHang = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy MaHang: " + ex.Message);
            }
            finally
            {
                _conn.Close();
            }

            return maHang;
        }

        public DataTable ThongKeHang()
        {           
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ThongKeSP";
                cmd.Connection = _conn;
                DataTable dtHang = new DataTable();
                dtHang.Load(cmd.ExecuteReader());
                return dtHang;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }

        public DataTable ThongKeTonKho()
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "ThongKeTonKho";
                cmd.Connection = _conn;
                DataTable dtHang = new DataTable();
                dtHang.Load(cmd.ExecuteReader());
                return dtHang;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }
    }
}
