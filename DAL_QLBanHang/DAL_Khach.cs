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
    public class DAL_Khach : DBConnect
    {
        public DataTable getKhach()
        {

           /* //Direct sql query
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblKhach", _conn);
            DataTable dtKhach = new DataTable();
            da.Fill(dtKhach);
            return dtKhach;
            */
            //Store Procedure
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DanhSachKhach";
                cmd.Connection = _conn;
                DataTable dtKhach = new DataTable();
                dtKhach.Load(cmd.ExecuteReader());
                return dtKhach;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }

        public bool InsertKhach(DTO_Khach khach)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("InsertDataIntoTblKhach", _conn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("Dienthoai", khach.SoDienThoai);
                cmd.Parameters.AddWithValue("TenKhach", khach.TenKhach);
                cmd.Parameters.AddWithValue("DiaChi", khach.DiaChi);
                cmd.Parameters.AddWithValue("phai", khach.Phai);
                cmd.Parameters.AddWithValue("Email", khach.EmailKhach);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm khách hàng: {ex.Message}");
                return false;
            }
            finally
            {
                _conn.Close();
            }
        }

        public bool UpdateKhach(DTO_Khach khach)
        {
            //using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                // Command (mặc định command type = text nên chúng ta khỏi fải làm gì nhiều).
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UpdateDataIntoTblKhach";
                cmd.Parameters.AddWithValue("Dienthoai", khach.SoDienThoai);
                cmd.Parameters.AddWithValue("TenKhach", khach.TenKhach);
                cmd.Parameters.AddWithValue("DiaChi", khach.DiaChi);
                cmd.Parameters.AddWithValue("phai", khach.Phai);
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

        public bool DeleteKhach(string soDT)
        {
            // using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DeleteDataFromtblKhach";
                cmd.Parameters.AddWithValue("dienthoai", soDT);
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

        public DataTable SearchKhach(string soDT)
        {
            // using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SearchKhach";
                cmd.Parameters.AddWithValue("Dienthoai", soDT);
                cmd.Connection = _conn;
                DataTable dtKhach = new DataTable();
                dtKhach.Load(cmd.ExecuteReader());
                return dtKhach;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }
    }
}
