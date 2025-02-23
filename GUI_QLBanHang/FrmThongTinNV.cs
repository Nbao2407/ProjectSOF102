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

namespace TemplateProject1_QLBanHang
{
    public partial class FrmThongTinNV : Form
    {
        Thread th;//using System.Threading;
        string stremail;//nhận email tư FrmMain
        BUS_NhanVien busNhanVien = new BUS_QLBanHang.BUS_NhanVien();
        public FrmThongTinNV(string email)
        {
            InitializeComponent();
            stremail = email;
            txtemail.Text = stremail;
            txtemail.Enabled = false;
            
        }
        
        private void Btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //mã hóa pass
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

        private void Btndoimatkhau_Click(object sender, EventArgs e)
        {
            if (txtmatkhaucu.Text.Trim().Length == 0)// kiem tra phai nhap data
            {
                MessageBox.Show("Bạn phải nhập mật khẩu cũ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmatkhaucu.Focus();
                return;
            }
            else if (txtmatkhaumoi.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmatkhaumoi.Focus();
                return;
            }
            else if(txtmatkhaumoi2.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập lại mật khẩu mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmatkhaumoi2.Focus();
                return;
            }
            else if (txtmatkhaumoi2.Text.Trim()!= txtmatkhaumoi.Text.Trim())
            {
                MessageBox.Show("Bạn phải nhập mật khẩu và nhập lại mật khẩu giống nhau", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtmatkhaumoi.Focus();
                return;
            }
            else
            {              
                if (MessageBox.Show("Bạn có chắc muốn cập nhật mật khẩu", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //do something if YES
                    string matkhaumoi = encryption(txtmatkhaumoi.Text);
                    string matkhaucu = encryption(txtmatkhaucu.Text);
                    if (busNhanVien.UpdateMatKhau(txtemail.Text,matkhaucu, matkhaumoi))
                    {
                        FrmMain.profile = 1;// cập nhật pass thành cong
                        FrmMain.session = 0;//đưa về tình trạng chưa đăng nhâp
                        SendMail(stremail, txtmatkhaumoi2.Text);
                        MessageBox.Show("Cập nhật mật khẩu thành công, bạn cần phải đăng nhập lại");                      
                        this.Close();                      
                    }
                    else
                    {
                        MessageBox.Show("Mât khẩu cũ không đúng,Cập nhật mật khẩu không thành công");
                        txtmatkhaucu.Text = null;
                        txtmatkhaumoi.Text = null;
                        txtmatkhaumoi2.Text = null;
                    }
                }
                else
                {
                    //do something if NO
                    txtmatkhaucu.Text = null;
                    txtmatkhaumoi.Text = null;
                    txtmatkhaumoi2.Text = null;
                }
            }          
        }

        public void SendMail(string email, string matkhau)
        {
            try
            {

                //Sending the email.
                //Now we must create a new Smtp client to send our email.

                SmtpClient client = new SmtpClient("smtp.gmail.com", 25);   //smtp.gmail.com // For Gmail
                                                                            //smtp.live.com // Windows live / Hotmail
                                                                            //smtp.mail.yahoo.com // Yahoo
                                                                            //smtp.aim.com // AIM
                                                                            //my.inbox.com // Inbox

                //Authentication.
                //This is where the valid email account comes into play. You must have a valid email account(with password) to give our program a place to send the mail from.

                NetworkCredential cred = new NetworkCredential("duoichon1@gmail.com", "chonduoi");

                //To send an email we must first create a new mailMessage(an email) to send.
                MailMessage Msg = new MailMessage();

                // Sender e-mail address.
                Msg.From = new MailAddress("duoichon1@gmail.com");//Nothing But Above Credentials or your credentials (*******@gmail.com)

                // Recipient e-mail address.
                Msg.To.Add(email);

                // Assign the subject of our message.
                Msg.Subject = "Ban da thay doi Mat khau";

                // Create the content(body) of our message.
                Msg.Body = "Chào anh/chị. Mật khẩu truy cập phần mềm là " + matkhau;

                // Send our account login details to the client.
                client.Credentials = cred;

                //Enabling SSL(Secure Sockets Layer, encyription) is reqiured by most email providers to send mail
                client.EnableSsl = true;

                // Send our email.
                client.Send(Msg);
                //Confirmation After Click the Button
                MessageBox.Show("Mot Email cap nhat mat khau da duoc goi toi ban!");
            }
            catch (Exception ex)
            {
                // If Mail Doesnt Send Error Mesage Will Be Displayed
                MessageBox.Show(ex.Message);
            }
        }
        private void OpenNewForm()
        {
            //Application.Exit();
            Application.Run(new FrmDangNhap());
        }
    }
}
