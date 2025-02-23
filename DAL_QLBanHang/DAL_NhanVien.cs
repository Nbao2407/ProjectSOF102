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
    public class DAL_NhanVien : DBConnect
    {
        public DataTable getNhanVien()
        {
            /*
            //Direct sql query
            SqlDataAdapter da = new SqlDataAdapter("SELECT email, tenNv, diachi,vaitro, tinhtrang FROM tblNhanVien", _conn);
            DataTable dtNhanVien = new DataTable();
            da.Fill(dtNhanVien);
            return dtNhanVien;
            */
            //Store Procedure
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DanhSachNV]";
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
        public bool insertNhanVien(DTO_NhanVien nv)
        {
            /*
            try
            {
                // Ket noi
                _conn.Open();

                string SQL = string.Format("INSERT INTO tblNhanVien(email,TenNv, diaChi, vaiTro,tinhtrang) " +
                    "VALUES ('{0}', '{1}','{2}',{3},{4})", nv.EmailNV, nv.TenNhanVien,nv.DiaChi,nv.VaiTro,nv.TinhTrang);
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
                cmd.CommandText = "[InsertDataIntoTblNhanVien]";
                cmd.Parameters.AddWithValue("email", nv.EmailNV);
                cmd.Parameters.AddWithValue("tennv", nv.TenNhanVien);
                cmd.Parameters.AddWithValue("diachi", nv.DiaChi);
                cmd.Parameters.AddWithValue("vaiTro", nv.VaiTro);
                cmd.Parameters.AddWithValue("tinhTrang", nv.TinhTrang);              
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
        public bool UpdateNhanVien(DTO_NhanVien nv)
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
                cmd.CommandText = "UpdateDataIntoTblNhanVien";
                cmd.Parameters.AddWithValue("email", nv.EmailNV);
                cmd.Parameters.AddWithValue("Tennv", nv.TenNhanVien);
                cmd.Parameters.AddWithValue("diaChi", nv.DiaChi);
                cmd.Parameters.AddWithValue("vaiTro", nv.VaiTro);
                cmd.Parameters.AddWithValue("tinhTrang", nv.TinhTrang);
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

        public bool DeleteNhanVien(string email)
        {
            // using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[DeleteDataFromtblNhanVien]";
                cmd.Parameters.AddWithValue("email", email);
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
        public DataTable SearchNhanVien(string tenNhanvien)
        {
            // using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[SearchNhanVien]";
                cmd.Parameters.AddWithValue("tenNV", tenNhanvien);
                cmd.Connection = _conn;
                DataTable dtNhanVien = new DataTable();
                dtNhanVien.Load(cmd.ExecuteReader());
                return dtNhanVien;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }

        public bool NhanVienDangNhap(DTO_NhanVien nv)
        {
            /*//Use sql statement
            try
            {
                // Ket noi
                _conn.Open();

                string SQL = "select * from tblNhanVien where email='" 
                    + nv.EmailNV +"' and  matKhau='"+ nv.MatKhau + "'" ;
                    
                SqlCommand cmd = new SqlCommand(SQL, _conn);
                // Query và kiểm tra
                if (Convert.ToInt16(cmd.ExecuteScalar()) > 0)
                    return true;
            }
            catch(Exception e)
            { }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
            return false;*/

            /*//Use sql statement with param
            try
            {
                // Ket noi
                _conn.Open();

                string SQL = "select * from tblNhanVien where email=@email and matkhau = @matkhau";
                SqlParameter emailParam = new SqlParameter();
                emailParam.ParameterName = "@email";
                //emailParam.Value = email;
                emailParam.Value = nv.EmailNV;

                SqlParameter matKhauParam = new SqlParameter();
                matKhauParam.ParameterName = "@matkhau";
                // matKhauParam.Value = matKhau;
                matKhauParam.Value = nv.MatKhau;
                SqlCommand cmd = new SqlCommand(SQL, _conn);

                cmd.Parameters.Add(emailParam);
                cmd.Parameters.Add(matKhauParam);
                // Query và kiểm tra
                if (Convert.ToInt16(cmd.ExecuteScalar()) > 0)
                    return true;
            }
            catch(Exception e)
            { }
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
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "DangNhap";
                //cmd.Parameters.AddWithValue("email", email);
                //cmd.Parameters.AddWithValue("matKhau", matKhau);
                cmd.Parameters.AddWithValue("email", nv.EmailNV);
                cmd.Parameters.AddWithValue("matKhau", nv.MatKhau);
                // Query và kiểm tra
                if (Convert.ToInt16(cmd.ExecuteScalar()) > 0)
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

        //Lay vai tro khi nhan vien login
        public DataTable VaiTroNhanVien(string email)
        {
            // using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "[LayVaiTroNV]";
                cmd.Parameters.AddWithValue("email", email);
                cmd.Connection = _conn;
                DataTable dtNhanVien = new DataTable();
                dtNhanVien.Load(cmd.ExecuteReader());
                return dtNhanVien;
            }
            finally
            {
                // Dong ket noi
                _conn.Close();
            }
        }

        //Cập nhật thong tin nhan viên, câp nhạt mạt khau
        public bool UpdateMatKhau(string email, string matKhauCu, string matKhauMoi)
        {
            //using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "chgpwd";
                cmd.Parameters.AddWithValue("email",email);
                cmd.Parameters.AddWithValue("@opwd", matKhauCu);
                cmd.Parameters.AddWithValue("@npwd", matKhauMoi);
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

        //Quen Mat Khau
        public bool NhanVienQuenMatKhau(string email)
        {
            //using store procedure
            try
            {
                // Ket noi
                _conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = _conn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "QuenMatKhau";
                cmd.Parameters.AddWithValue("email", email);
                // Query và kiểm tra
                if (Convert.ToInt16(cmd.ExecuteScalar()) > 0)
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

        //Tao moi mat khau khi quen mat khau
        //Cập nhật thong tin nhan viên, câp nhạt mạt khau
        public bool TaoMatKhau(string email, string matKhauMoi)
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
                cmd.CommandText = "TaoMatKhauMoi";
                cmd.Parameters.AddWithValue("email", email);
                cmd.Parameters.AddWithValue("matkhau", matKhauMoi);
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
    }
}
