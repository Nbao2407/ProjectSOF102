using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS_QLBanHang;
using DTO_QLBanHang;

namespace TemplateProject1_QLBanHang
{
    public partial class frmHang : Form
    {
        BUS_Hang busHang = new BUS_QLBanHang.BUS_Hang();
        string stremail = FrmMain.mail;//nhận email tư FrmMain
        string checkUrlImage;//kiểm tra hinh khi câp nhật
        string fileName;  // ten file
        string fileSavePath;//url store image  
        string fileAddress;// url load images
        public frmHang()
        {
            InitializeComponent();
        }
        public frmHang(string email)
        {
           // stremail = email;
        }
        private void FrmHang_Load(object sender, EventArgs e)
        {
            ResetValues();
            LoadGridview_Hang();
            
        }
        private void BtnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            btnMo.Enabled = true;
            txtTenhang.Enabled = true;
            txtSoluong.Enabled = true;
            txtDongianhap.Enabled = true;
            txtDongiaban.Enabled = true;
            txtGhichu.Enabled = true;
            txtMahang.Text = null;
            txtTenhang.Text = null;
            txtSoluong.Text = null;
            txtDongianhap.Text = null;
            txtDongiaban.Text = null;
            txtHinh.Text = null;
            pbHinh.Image = null;
            txtGhichu.Text = null;
            txtTenhang.Focus();
        }
        private void BtnXoa_Click(object sender, EventArgs e)
        {
            int maHang = int.Parse(txtMahang.Text);
            if (MessageBox.Show("Bạn có chắc muốn xóa dữ liệu", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //do something if YES
                if (busHang.DeleteHang(maHang))
                {
                    MessageBox.Show("Xóa dữ liệu thành công");
                    ResetValues();
                    LoadGridview_Hang(); // refresh datagridview
                }
                else
                {
                    MessageBox.Show("Xóa không thành công");
                }
            }
            else
            {
                //do something if NO
                ResetValues();
            }
        }
        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
        }
        private void LoadGridview_Hang()
        {
            dgvhang.DataSource = busHang.getHang();
            dgvhang.Columns[0].HeaderText = "Mã Sản Phẩm";
            dgvhang.Columns[1].HeaderText = "Tên Sản Phẩm";
            dgvhang.Columns[2].HeaderText = "Số Lượng";
            dgvhang.Columns[3].HeaderText = "Đơn Giá Nhập";
            dgvhang.Columns[4].HeaderText = "Đơn Giá Bán";
            dgvhang.Columns[5].HeaderText = "Hình Ảnh";
            dgvhang.Columns[7].Visible = false;
            
        }
        private void ResetValues()
        {
            txtMahang.Text = null;
            txtTenhang.Text =null;
            txtSoluong.Text =null;
            txtDongianhap.Text =null;
            txtDongiaban.Text =null;
            txtHinh.Text = null;
            pbHinh.Image = null;
            txtGhichu.Text = null;

            txtTenhang.Enabled = false;
            txtSoluong.Enabled = false;
            txtDongianhap.Enabled = false;
            txtDongiaban.Enabled = false;
            txtHinh.Enabled = false;
            txtGhichu.Enabled = false;
            btnMo.Enabled = false;
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnDong.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            int intSoLuong;
            bool isInt = int.TryParse(txtSoluong.Text.Trim(), out intSoLuong);
            float floatDonGiaNhap;
            bool isFloatNhap = float.TryParse(txtDongianhap.Text.Trim(), out floatDonGiaNhap);
            float floatDonGiaBan;
            bool isFloatBan = float.TryParse(txtDongiaban.Text.Trim(), out floatDonGiaBan);

            // Validation checks
            if (txtTenhang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenhang.Focus();
                return;
            }
            else if (!isInt || intSoLuong <= 0)
            {
                MessageBox.Show("Bạn phải nhập số lượng sản phẩm > 0, số nguyên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoluong.Focus();
                return;
            }
            else if (!isFloatNhap || floatDonGiaNhap <= 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá nhập > 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongianhap.Focus();
                return;
            }
            else if (!isFloatBan || floatDonGiaBan <= 0)
            {
                MessageBox.Show("Bạn phải nhập đơn giá bán > 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongiaban.Focus();
                return;
            }
            else if (txtHinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải upload hình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnMo.Focus();
                return;
            }
            else
            {
                DTO_Hang h = new DTO_Hang(txtTenhang.Text, int.Parse(txtSoluong.Text), float.Parse(txtDongianhap.Text),
                    float.Parse(txtDongiaban.Text), "\\Images\\" + fileName, txtGhichu.Text, stremail);

                if (busHang.InsertHang(h))
                {
                    MessageBox.Show("Thêm sản phẩm thành công");
                    File.Copy(fileAddress, fileSavePath, true); // Copy file image to application
                    ResetValues();
                    LoadGridview_Hang(); // Refresh DataGridView
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm không thành công");
                }
            }
        }

        private void BtnMo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";        
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                fileAddress = dlgOpen.FileName;
                //pbHinh.Image = Image.FromFile(fileAddress);
                try
                {
                    pbHinh.Image = Image.FromFile(fileAddress);
                }
                catch (Exception)
                {
                    //  Block of code to handle errors
                    Console.WriteLine("Load image wrong !");
                }
                fileName = Path.GetFileName(dlgOpen.FileName);
                string saveDirectory = Application.StartupPath.Substring(0,(Application.StartupPath.Length-10));
                fileSavePath = saveDirectory + "\\Images\\" + fileName;// combine with file name*/
                txtHinh.Text = "\\Images\\" + fileName;
            }
        }
        private void Dgvhang_Click(object sender, EventArgs e)
        {
            string saveDirectory = Application.StartupPath.Substring(0, (Application.StartupPath.Length - 10));
            if (dgvhang.Rows.Count >1)
            {
                btnMo.Enabled = true;
                btnLuu.Enabled = false;
                txtTenhang.Enabled = true;
                txtSoluong.Enabled = true;
                txtDongianhap.Enabled = true;
                txtDongiaban.Enabled = true;
                txtGhichu.Enabled = true;
                txtTenhang.Focus();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtMahang.Text = dgvhang.CurrentRow.Cells["MaHang"].Value.ToString();
                txtTenhang.Text = dgvhang.CurrentRow.Cells["TenHang"].Value.ToString();
                txtSoluong.Text = dgvhang.CurrentRow.Cells["SoLuong"].Value.ToString();
                txtDongianhap.Text = dgvhang.CurrentRow.Cells["DonGiaNhap"].Value.ToString();
                txtDongiaban.Text = dgvhang.CurrentRow.Cells["DonGiaBan"].Value.ToString();
                txtHinh.Text = dgvhang.CurrentRow.Cells["HinhAnh"].Value.ToString();
                checkUrlImage = txtHinh.Text;//giữ đường dẫn file hình
                try
                {
                    pbHinh.Image = Image.FromFile(saveDirectory + dgvhang.CurrentRow.Cells["HinhAnh"].Value.ToString());
                }
                catch (Exception)
                {
                    //  Block of code to handle errors
                    Console.WriteLine("Load image wrong !");
                }

                txtGhichu.Text = dgvhang.CurrentRow.Cells["GhiChu"].Value.ToString();          
            }
            else
            {
                MessageBox.Show("Bảng không tồn tại dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BtnSua_Click(object sender, EventArgs e)
        {
            int intSoLuong;
            bool isInt = int.TryParse(txtSoluong.Text.Trim(), out intSoLuong);
            float floatDonGiaNhap;
            bool isFloatNhap = float.TryParse(txtDongianhap.Text.Trim(), out floatDonGiaNhap);
            float floatDonGiaBan;
            bool isFloatBan = float.TryParse(txtDongiaban.Text.Trim(), out floatDonGiaBan);
            string tenKhach = txtTenhang.Text.Trim();

            Regex regexTen = new Regex(@"^[A-ZÀ-Ỹ][a-zà-ỹ]*(\s[A-ZÀ-Ỹ][a-zà-ỹ]*)*$");
            // Validation checks
            if (txtTenhang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtTenhang.Focus();
                return;
            }
            if (!regexTen.IsMatch(tenKhach) || tenKhach.Length < 2 || tenKhach.Length > 50)
            {
                MessageBox.Show("Tên khách hàng không hợp lệ!\nTên chỉ chứa chữ cái, viết hoa chữ đầu, từ 2 đến 50 ký tự.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenhang.Focus();
                return;
            }
            else if (!isInt || intSoLuong <= 0) // Ensure the quantity is greater than 0
            {
                MessageBox.Show("Bạn phải nhập số lượng sản phẩm >0, số nguyên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoluong.Focus();
                return;
            }
            else if (!isFloatNhap || floatDonGiaNhap <= 0) // Ensure the purchase price is greater than 0
            {
                MessageBox.Show("Bạn phải nhập đơn giá nhập >0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongianhap.Focus();
                return;
            }
            else if (!isFloatBan || floatDonGiaBan <= 0) // Ensure the selling price is greater than 0
            {
                MessageBox.Show("Bạn phải nhập đơn giá bán >0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDongiaban.Focus();
                return;
            }
            else if (txtHinh.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải upload hình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnMo.Focus();
                return;
            }
            else
            {
                DTO_Hang h = new DTO_Hang(int.Parse(txtMahang.Text), txtTenhang.Text, intSoLuong, floatDonGiaNhap, floatDonGiaBan, txtHinh.Text, txtGhichu.Text);

                if (MessageBox.Show("Bạn có chắc muốn chỉnh sửa", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (busHang.UpdateHang(h))
                    {
                        if (txtHinh.Text != checkUrlImage) // If image has changed
                            File.Copy(fileAddress, fileSavePath, true); // Copy new image to application folder

                        MessageBox.Show("Sửa thành công");
                        ResetValues();
                        LoadGridview_Hang(); // Refresh DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Sửa không thành công");
                    }
                }
                else
                {
                    ResetValues();
                }
            }
        }



        private void BtnTimkiem_Click(object sender, EventArgs e)
        {
            string tenHang = txttimKiem.Text;
            DataTable ds  = busHang.SearchHang(tenHang);
            if (ds.Rows.Count > 0)
            {
                dgvhang.DataSource = ds;
                dgvhang.Columns[0].HeaderText = "Mã Sản Phẩm";
                dgvhang.Columns[1].HeaderText = "Tên Sản Phẩm";
                dgvhang.Columns[2].HeaderText = "Số Lượng";
                dgvhang.Columns[3].HeaderText = "Đơn Giá Nhập";
                dgvhang.Columns[4].HeaderText = "Đơn Giá Bán";
            }
            else
            {
                MessageBox.Show("Không tìm thấy san pham", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            txttimKiem.Text = "Nhập tên sản phẩm";
            txttimKiem.BackColor = Color.LightGray;
            ResetValues();
        }
        private void TxttimKiem_Click(object sender, EventArgs e)
        {
            txttimKiem.Text = null;
            txttimKiem.BackColor = Color.White;
        }
        private void BtnDanhsach_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadGridview_Hang();
        }

        private void dgvhang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pbHinh_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
