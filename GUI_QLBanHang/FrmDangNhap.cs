using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS_QLBanHang;
using DTO_QLBanHang;
using System.Configuration;
namespace TemplateProject1_QLBanHang
{
    public partial class FrmDangNhap : Form
    {
        //su dụng các thành phần từ BUS_NhanVien class
        BUS_NhanVien busNhanVien = new BUS_QLBanHang.BUS_NhanVien();

        //Các giá trị pass cho FrmMain phân quyền
        public string email { set; get; }
        public string matkhau { get; set; }
        public string vaitro { set; get; }//đang nhạp thành công, kiem tra vai tro
        public FrmDangNhap()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }
     
        public static string GetSetting(string key)
        {
            return ConfigurationManager.AppSettings[key] ?? "";
        }
        public static void SetSetting(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            // Check if setting exists, add if it doesn't
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
            FrmMain.session = 0;
            if (FrmDangNhap.GetRememberMe())
            {
                txtemail.Text = FrmDangNhap.GetSetting("Email");
                txtmatkhau.Text = FrmDangNhap.GetSetting("Password");
                checkBox1.Checked = true;
            }
        }
        public static bool GetRememberMe()
        {
            return bool.TryParse(GetSetting("RememberMe"), out bool rememberMe) && rememberMe;
        }

        public static void SaveLoginInfo(string email, string password, bool remember)
        {
            SetSetting("Email", remember ? email : "");
            SetSetting("Password", remember ? password : "");
            SetSetting("RememberMe", remember.ToString().ToLower());
        }
    private void Button3_Click(object sender, EventArgs e)// thoat
        {
            this.Close();
        }
        //Boolean login = false;//chưa đang nhap
        public void AutoFillEmailAndPassword()
        {

            Clipboard.SetText("fpoly@fe.edu.vn");
            Thread.Sleep(100); // Small delay to ensure focus is set
            SendKeys.Send("^v"); // Paste the email using Ctrl+V
            Thread.Sleep(100); // Small delay between actions

            // Move focus to the password field
            SendKeys.Send("{TAB}");
            Thread.Sleep(100); // Small delay to ensure focus is set

            // Set the clipboard text to the password
            Clipboard.SetText("123");
            SendKeys.Send("^v");
        }
        private void Btndangnhap_Click(object sender, EventArgs e)
        {
            DTO_NhanVien nv = new DTO_NhanVien();
            nv.EmailNV = txtemail.Text;
            nv.MatKhau = encryption(txtmatkhau.Text);// ma mat khau de so sanh voi mat khau da ma hoa trong csdl
            if (busNhanVien.NhanVienDangNhap(nv))//successfull login
            {
                //login = true;
                FrmMain.mail = nv.EmailNV; // truyen email dang nhap cho frmMain
                DataTable dt = busNhanVien.VaiTroNhanVien(nv.EmailNV);
                vaitro = dt.Rows[0][0].ToString();// lây vai tro cua nhan vien, hien thi cac chuc nang ma nhan vien co the thao tac
                MessageBox.Show("Đăng nhập thành công");
                FrmMain.session = 1; // cap nhat trang thai da dang nhap thanh cong
                //LoginSettings.SaveLoginInfo(txtemail.Text, txtmatkhau.Text, checkBox1.Checked);
                FrmDangNhap.SaveLoginInfo(txtemail.Text, txtmatkhau.Text, checkBox1.Checked);
                this.Close();
            }
            else
            {
                MessageBox.Show("Đăng nhập không thành công, kiểm tra lại email hoặc mật khẩu");
                txtmatkhau.Text = null;
                txtmatkhau.Focus();
            }
        }

        //ma hóa md5 password
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

        //xu ly quen mat khau
        private void btnQuenmk_Click(object sender, EventArgs e)
        {
            if (txtemail.Text != "")
            {
                if (busNhanVien.NhanVienQuenMatKhau(txtemail.Text))//show form input email. If true will send pass random
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(RandomString(4, true));
                    builder.Append(RandomNumber(1000, 9999));
                    builder.Append(RandomString(2, false));

                    Clipboard.SetText(builder.ToString()); 
                    MessageBox.Show(builder.ToString()); 

                    string matkhaumoi = encryption(builder.ToString());
                    busNhanVien.TaoMatKhau(txtemail.Text,matkhaumoi);// update new pass to database
                }
                else
                {
                    MessageBox.Show("Email khong ton tai, vui long nhap lai email!");
                }
            }
            else
            {
                MessageBox.Show("Ban can nhap email nhan thong tin phuc hoi mat khau!");
                txtemail.Focus();
            }
        }

        //Tao chuoi ngau nhien
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        //Tao so ngau nhien
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        // send mail
   public void SendMail(string email, string matkhau)
    {
        try
        {
            // Tạo client SMTP
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("sender@gmail.com", "chonduoi"),
                EnableSsl = true // Bật mã hóa SSL/TLS

            };

            // Tạo nội dung email
            MailMessage Msg = new MailMessage
            {
                From = new MailAddress("sender@gmail.com"),
                Subject = "Bạn đã sử dụng tính năng quên mật khẩu",
                Body = $"Chào anh/chị,\nMật khẩu mới truy cập phần mềm là: {matkhau}",
                IsBodyHtml = false
            };
            Msg.To.Add(email); // Thêm người nhận

            // Gửi email
            client.Send(Msg);
            MessageBox.Show("Một email phục hồi mật khẩu đã được gửi!");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Lỗi gửi email: {ex.Message}");
                Clipboard.SetText(ex.Message);
            }
    }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

