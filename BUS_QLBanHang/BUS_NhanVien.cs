using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DAL_QLBanHang;
using DTO_QLBanHang;
namespace BUS_QLBanHang
{
    public class BUS_NhanVien
    {
        public bool NhanVienDangNhap(DTO_NhanVien nv)
        {
            return dalNhanVien.NhanVienDangNhap(nv);
        }

        public string encryption(string password)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encrypt;
            UTF8Encoding encode = new UTF8Encoding();
            //encrypt the given password string into Encrypted data  
            encrypt = md5.ComputeHash(encode.GetBytes(password));
            StringBuilder encryptdata = new StringBuilder();
            //Create a new string by using the encrypted data  
            for (int i = 0; i < encrypt.Length; i++)
            {
                encryptdata.Append(encrypt[i].ToString());
            }
            return encryptdata.ToString();
        }

        DAL_NhanVien dalNhanVien = new DAL_NhanVien();
        public DataTable getNhanVien()
        {
            return dalNhanVien.getNhanVien();
        }
        public bool insertNhanVien(DTO_NhanVien Nv)
        {
            return dalNhanVien.insertNhanVien(Nv);
        }
        public bool UpdateNhanVien(DTO_NhanVien Nv)
        {
            return dalNhanVien.UpdateNhanVien(Nv);
        }
        public bool DeleteNnhanVien(string tenDangNhap)
        {
            return dalNhanVien.DeleteNhanVien(tenDangNhap);
        }
        public DataTable SearchNhanVien(string tenNhanvien)
        {
            return dalNhanVien.SearchNhanVien(tenNhanvien);
        }
        /*public bool NhanVienDangNhap(string email, string matKhau)
        {
            return dalNhanVien.NhanVienDangNhap(email, matKhau);
        }*/
       

        //tra ve vai tro nhan vien dua vao email dang nhap
        public DataTable VaiTroNhanVien(string email)
        {
            return dalNhanVien.VaiTroNhanVien(email);
        }
        public bool UpdateMatKhau(string email, string matKhauCu, string matKhauMoi)
        {
            return dalNhanVien.UpdateMatKhau(email,matKhauCu,matKhauMoi);
        }
        public bool NhanVienQuenMatKhau(string email)
        {
            return dalNhanVien.NhanVienQuenMatKhau(email);
        }
        public bool TaoMatKhau(string email, string matKhauMoi)
        {
            return dalNhanVien.TaoMatKhau(email,matKhauMoi);
        }
    }
}
