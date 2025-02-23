using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLBanHang
{
    public class DTO_Hang
    {
        private int maHang;
        private string tenHang;      
        private int soLuong;
        private float donGiaNhap;
        private float donGiaBan;
        private string hinhAnh;
        private string ghiChu;
        private string Manv;
        private string emailNv;// ghi nhận nhân viên nào nhập sản phảm
        public int MaHang
        {
            get
            {
                return maHang;
            }
            set
            {
                maHang = value;
            }
        }
        public string TenHang
        {
            get
            {
                return tenHang;
            }
            set
            {
                tenHang = value;
            }
        }
        public int SoLuong
        {
            get
            {
                return soLuong;
            }
            set
            {
                soLuong = value;
            }
        }
        public float DonGiaNhap
        {
            get
            {
                return donGiaNhap;
            }
            set
            {
                donGiaNhap = value;
            }
        }
        public float DonGiaBan
        {
            get
            {
                return donGiaBan;
            }
            set
            {
                donGiaBan = value;
            }
        }
        public string HinhAnh
        {
            get
            {
                return hinhAnh;
            }
            set
            {
                hinhAnh = value;
            }
        }
        public string GhiChu
        {
            get
            {
                return ghiChu;
            }
            set
            {
                ghiChu = value;
            }
        }
        public string MaNV
        {
            get
            {
                return Manv;
            }
            set
            {
                Manv = value;
            }
        }
        public string EmailNV
        {
            get
            {
                return emailNv;
            }
            set
            {
                emailNv = value;
            }
        }
        /*
        private string maHang { get; set; };
        private string tenHang { get; set; }
        private int soLuong { get; set; }
        private float donGiaNhap { get; set; }
        private float donGiaBan { get; set; }
        private string hinhAnh { get; set; }
        private string ghiChu { get; set; }
        */

        public DTO_Hang(int maHang,string tenHang, int soLuong, float donGiaNhap, float donGiaBan,
                        string hinhAnh, string ghiChu)
        {
            this.maHang = maHang;
            this.tenHang = tenHang;
            this.soLuong = soLuong;
            this.donGiaNhap = donGiaNhap;
            this.donGiaBan = donGiaBan;
            this.hinhAnh = hinhAnh;
            this.ghiChu = ghiChu;
        }
        public DTO_Hang(string tenHang, int soLuong, float donGiaNhap, float donGiaBan,
                        string hinhAnh, string ghiChu, string emailnv)
        {
            this.tenHang = tenHang;
            this.soLuong = soLuong;
            this.donGiaNhap = donGiaNhap;
            this.donGiaBan = donGiaBan;
            this.hinhAnh = hinhAnh;
            this.ghiChu = ghiChu;
            this.EmailNV = emailnv;
        }
    }
}
