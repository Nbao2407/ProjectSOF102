using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QLBanHang
{
   public class DTO_Khach
    {
        private string soDienThoai;
        private string tenKhach;
        private string diaChi;
        private string phai;
        private string emailKhach;// ghi nhận nhân viên nào nhập Khách Hàng
        private string Manv;

        public string SoDienThoai
        {
            get
            {
                return soDienThoai;
            }
            set
            {
                soDienThoai = value;
            }
        }
        public string TenKhach
        {
            get
            {
                return tenKhach;
            }
            set
            {
                tenKhach = value;
            }
        }
        public string DiaChi
        {
            get
            {
                return diaChi;
            }
            set
            {
                diaChi = value;
            }
        }
        public string Phai
        {
            get
            {
                return phai;
            }
            set
            {
                phai = value;
            }
        }
        public string EmailKhach
        {
            get
            {
                return emailKhach;
            }
            set
            {
                emailKhach = value;
            }
        }
        public DTO_Khach(string dienThoai, string tenKhach, string diaChi,string phai, string email=null)
        {
            this.soDienThoai = dienThoai;
            this.tenKhach = tenKhach;
            this.diaChi = diaChi;
            this.phai = phai;
            this.emailKhach = email;
        }
    }
}
