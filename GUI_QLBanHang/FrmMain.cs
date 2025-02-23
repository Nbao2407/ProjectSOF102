using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TemplateProject1_QLBanHang
{
    public partial class FrmMain : Form
    {
        public static int session =0;//kiem tra tình trạng login
        public static int profile = 0;// 
        public static string mail;// truyên email từ frmMain cho các form khác thong qua bien static
        Thread th;//using System.Threading;
        FrmDangNhap dn  = new FrmDangNhap();
     
        public FrmMain()
        {
            InitializeComponent();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(FrmMain_KeyDown);
        }
        private void FrmMain_KeyDown(object sender, KeyEventArgs e)
        {

         
        }
  
        public void FrmMain_Load(object sender, EventArgs e)
        {         
            Resetvalue();
            if (profile == 1)// nếu vừa câp nhật mật khẩu thì 
                             //phải login lại, mục 'thong tin nhan vien' ẩn
            {
                thongtinnvToolStripMenuItem.Text = null;
                profile = 0; //ẩn mục 'thong tin nhan vien'
            }
        }
        //show form KhachHang
        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Nếu chưa mở
            if (!CheckExistForm("FrmKhach"))
            {
                FrmKhach nv = new FrmKhach();
                nv.MdiParent = this;
                nv.Show();
            }
            else//hiển thị focus
                ActiveChildForm("FrmKhach");
        }

        //show form sản phẩm
        private void SảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = System.Windows.Forms.Application.OpenForms["frmHang"];

            if (frm == null) // Nếu form chưa tồn tại
            {
                frmHang newForm = new frmHang();
                newForm.MdiParent = this; // Nếu dùng MDI
                newForm.Show();
            }
            else
            {
                frm.Activate(); // Kích hoạt lại form nếu đã mở
            }
        }



        //show form nhanvien
        private void NhanVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExistForm("frmNhanVien"))
            {
                frmNhanVien nv = new frmNhanVien();
                nv.MdiParent = this;
                nv.Show();
            }
            else
                ActiveChildForm("frmNhanVien");
        }

        //show form đăng nhập
        public void ĐăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Check if the form already exists
            if (!CheckExistForm("FrmDangNhap"))
            {
                // Instantiate the form if it doesn't exist
                dn = new FrmDangNhap();
                dn.MdiParent = this;
                dn.Show();
                dn.FormClosed += new FormClosedEventHandler(FrmDangNhap_FormClosed);
            }
            else
            {
                // Check if the form is disposed before accessing it
                if (dn != null && !dn.IsDisposed)
                {
                    ActiveChildForm("FrmDangNhap");
                }
                else
                {
                    // If the form is disposed, create a new instance
                    dn = new FrmDangNhap();
                    dn.MdiParent = this;
                    dn.Show();
                    dn.FormClosed += new FormClosedEventHandler(FrmDangNhap_FormClosed);
                }
            }
        }


        //show form thông tin nv
        private void ProfileNvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmThongTinNV profilenv = new FrmThongTinNV(FrmMain.mail);// khơi tạo FrmThongTinNV với email nv
            
            if (!CheckExistForm("frmThongTinNV"))
            {            
                profilenv.MdiParent = this;
                profilenv.FormClosed += new FormClosedEventHandler(FrmThongTinNV_FormClosed);
                profilenv.Show();
            }
            else
                ActiveChildForm("frmThongTinNV");
        }

        //form thống kê sản phâm theo nhân viên nhập và thống kê số lượng sp tồn kho
        private void ThongKeSPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!CheckExistForm("FrmThongKe"))
            {
                FrmThongKe nv = new FrmThongKe();
                nv.MdiParent = this;
                nv.Show();
            }
            else
                ActiveChildForm("FrmThongKe");
        }

        //show file hương dẫn phan mem
        private void HuongDanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", "HDSD.pdf");
            if (File.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                Console.WriteLine($"File not found at: {path}");
            }
        }

        //CheckExistForm để kiểm tra xem 1 Form với tên nào đó đã hiển thị trong Form Cha (frmMain) chưa? => Trả về True (đã hiển thị) hoặc False (nếu chưa hiển thị).
        private bool CheckExistForm(string name)
        {
            bool check = false;
            foreach(Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    check = true;
                    break;
                }
            }
            return check;
        }

        //ActiveChildForm dùng để “Kích hoạt”  – hiển thị lên trên cùng các trong số các Form Con nếu nó đã hiện mà không phải tạo thể hiện mới.
        private void ActiveChildForm(string name)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.Name == name)
                {
                    frm.Activate();
                    break;
                }
            }
        }

        

        //chuc nang nhan vien bình thường ko hiên thị quan lý nhan vien và thống kê
        private void VaiTroNV()
        {
            NhanVienToolStripMenuItem.Visible = false;
            thongkeToolStripMenuItem.Visible = false;
        }

        //Thiệt lap phan quyên khi load frmMain
        private void Resetvalue()
        {
            if (session == 1)
            {

                thongtinnvToolStripMenuItem.Text = "Chào " + (FrmMain.mail ?? "Khách");
                NhanVienToolStripMenuItem.Visible = true;
                danhMụcToolStripMenuItem.Visible = true;
                LoOutToolStripMenuItem1.Enabled = true;
                thongkeToolStripMenuItem.Visible = true;
                ThongKeSPToolStripMenuItem.Visible = true;
                ProfileNvToolStripMenuItem.Visible = true;
                đăngNhậpToolStripMenuItem.Enabled = false;
                if (!string.IsNullOrEmpty(dn.vaitro) && int.TryParse(dn.vaitro, out int role) && role == 0)
                {
                    VaiTroNV(); // Chức năng nhân viên bình thường
                }
            }
            else
            {
                NhanVienToolStripMenuItem.Visible = false;
                danhMụcToolStripMenuItem.Visible = false;
                LoOutToolStripMenuItem1.Enabled = false;
                ProfileNvToolStripMenuItem.Visible = false;
                ThongKeSPToolStripMenuItem.Visible = false;
                thongkeToolStripMenuItem.Visible = false;
                đăngNhậpToolStripMenuItem.Enabled = true;
            }
        }
       
        void FrmDangNhap_FormClosed(object sender, FormClosedEventArgs e)
        {
            //when child form is closed, this code is executed        
            this.Refresh();
            FrmMain_Load(sender,e);// load form main again
        }

        
        void FrmThongTinNV_FormClosed(object sender, FormClosedEventArgs e)
        {            
            //when child form is closed, this code is executed
            this.Refresh();
            
            FrmMain_Load(sender, e);// load form main again
        }
        private void LoOutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            thongtinnvToolStripMenuItem.Text = null;
            session = 0;
            Resetvalue();
        }
        private void OpenNewForm()
        {
            System.Windows.Forms.Application.Run(new FrmMain());

        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hướngDẫnToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void giớiThiệuPhầnMềmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Images", "Gioi thieu phan men.png");
            if (File.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                MessageBox.Show($"File not found at: {path}");
            }
        }
    }
}
