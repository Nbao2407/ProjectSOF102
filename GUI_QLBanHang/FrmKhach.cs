using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS_QLBanHang;
using DTO_QLBanHang;
using System.Text.RegularExpressions;

namespace TemplateProject1_QLBanHang
{
    public partial class FrmKhach : Form
    {
        BUS_Khach busKhach = new BUS_QLBanHang.BUS_Khach();
        string stremail = FrmMain.mail;//nhận email tư FrmMain
        public FrmKhach()
        {
            InitializeComponent();
        }

        private void FrmKhach_Load(object sender, EventArgs e)
        {
            LoadGridview_Khach();
            ResetValues();
        }

        //Load danh sách san pham len datagridview
        private void LoadGridview_Khach()
        {
            dgvkhach.DataSource = busKhach.getKhach();
            dgvkhach.Columns[0].HeaderText = "Điện Thoại";
            dgvkhach.Columns[1].HeaderText = "Họ và Tên";
            dgvkhach.Columns[2].HeaderText = "Địa Chỉ";
            dgvkhach.Columns[3].HeaderText = "Giới Tính";
            dgvkhach.Columns[4].Visible = false;
        }

        //thiết lập trạng thái control khi form load
        private void ResetValues()
        {
            txtDiachi.Text = null;
            txtDienthoai.Text = null;
            txtTenkhach.Text = null;
            rbnam.Checked = false;
            rbnu.Checked = false;

            txtDiachi.Enabled = false;
            txtDienthoai.Enabled = false;
            txtTenkhach.Enabled = false;
            rbnu.Enabled = false;
            rbnam.Enabled = false;
            dgvkhach.Enabled = true;
            
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnDong.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            txtDienthoai.Focus();
        }

        private void BtnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnThem_Click(object sender, EventArgs e)
        {
            txtDiachi.Text = null;
            txtDienthoai.Text = null;
            txtTenkhach.Text = null;
            rbnam.Checked = true;
            rbnu.Checked = false;

            txtDiachi.Enabled = true;
            txtDienthoai.Enabled = true;
            txtTenkhach.Enabled = true;
            rbnu.Enabled = true;
            rbnam.Enabled = true;
            dgvkhach.Enabled = false;

            btnThem.Enabled = true;
            btnLuu.Enabled = true;
            btnDong.Enabled = true;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
        }

        // Bỏ qua các hành động đang thao tác, gọi trạng thái load form ban dầu
        private void BtnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
        }
        private void BtnLuu_Click(object sender, EventArgs e)
        {
            long intDienThoai;
            bool isInt = long.TryParse(txtDienthoai.Text.Trim(), out intDienThoai);
            string tenKhach = txtTenkhach.Text.Trim();
            string phai = rbnu.Checked ? "Nữ" : "Nam";
            Regex regexTen = new Regex(@"^[A-ZÀ-Ỹ][a-zà-ỹ]*(\s[A-ZÀ-Ỹ][a-zà-ỹ]*)*$");
            if (string.IsNullOrWhiteSpace(txtDienthoai.Text) || string.IsNullOrWhiteSpace(txtTenkhach.Text) || string.IsNullOrWhiteSpace(txtDiachi.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin (Điện thoại, Tên khách, Địa chỉ).", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!isInt || intDienThoai <= 0 || txtDienthoai.Text.Trim().Length < 10 || txtDienthoai.Text.Trim().Length > 11)
            {
                MessageBox.Show("Số điện thoại phải là số nguyên dương 11 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienthoai.Focus();
                return;
            }
            if (!regexTen.IsMatch(tenKhach) || tenKhach.Length < 2 || tenKhach.Length > 50)
            {
                MessageBox.Show("Tên khách hàng không hợp lệ!\nTên chỉ chứa chữ cái, viết hoa chữ đầu, từ 2 đến 50 ký tự.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenkhach.Focus();
                return;
            }
            else
            {
                DTO_Khach kh = new DTO_Khach(txtDienthoai.Text, txtTenkhach.Text, txtDiachi.Text, phai, stremail);

                if (busKhach.InsertKhach(kh))
                {
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetValues(); // Đặt lại các giá trị mặc định
                    LoadGridview_Khach(); // Cập nhật lại DataGridView
                }
                else
                {
                    MessageBox.Show("Thêm khách hàng không thành công. Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Dgvkhach_Click(object sender, EventArgs e)
        {
            if (dgvkhach.Rows.Count > 1)
            {
                btnLuu.Enabled = false;
                txtDiachi.Enabled = true;
                txtDienthoai.Enabled = true;
                txtTenkhach.Enabled = true;
                rbnu.Enabled = true;
                rbnam.Enabled = true;
                txtDienthoai.Focus();
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                txtDienthoai.Text = dgvkhach.CurrentRow.Cells[0].Value.ToString();
                txtTenkhach.Text = dgvkhach.CurrentRow.Cells[1].Value.ToString();
                txtDiachi.Text = dgvkhach.CurrentRow.Cells[2].Value.ToString();
                string phai = dgvkhach.CurrentRow.Cells[3].Value.ToString();
                if (phai == "Nam")
                    rbnam.Checked = true;
                else
                    rbnu.Checked = true;
            }
            else
            {
                MessageBox.Show("Bảng không tồn tại dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtnSua_Click(object sender, EventArgs e)
        {
            Regex regexTen = new Regex(@"^[A-ZÀ-Ỹ][a-zà-ỹ]*(\s[A-ZÀ-Ỹ][a-zà-ỹ]*)*$");
            string tenKhach = txtTenkhach.Text.Trim();

            long intDienThoai;
            bool isInt = long.TryParse(txtDienthoai.Text.Trim(), out intDienThoai);
            string phai = "Nam";
            if (rbnu.Checked == true)
                phai = "Nữ";

            if (!isInt || intDienThoai <= 0 || txtDienthoai.Text.Trim().Length < 10 || txtDienthoai.Text.Trim().Length > 11)
            {
                MessageBox.Show("Số điện thoại phải là số nguyên dương 11 chữ số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDienthoai.Focus();
                return;
            }
            if (!regexTen.IsMatch(tenKhach) || tenKhach.Length < 2 || tenKhach.Length > 50)
            {
                MessageBox.Show("Tên khách hàng không hợp lệ!\nTên chỉ chứa chữ cái, viết hoa chữ đầu, từ 2 đến 50 ký tự.",
                                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenkhach.Focus();
                return;
            }
            else
            {
                // Tạo DTo
                DTO_Khach kh = new DTO_Khach(txtDienthoai.Text, txtTenkhach.Text, txtDiachi.Text, phai); // Vì ID tự tăng nên để ID số gì cũng dc
                if (MessageBox.Show("Bạn có chắc muốn chỉnh sửa", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (busKhach.UpdateKhach(kh))
                    {
                        MessageBox.Show("Cập nhật khách hàng thành công");
                        ResetValues();
                        LoadGridview_Khach(); // refresh datagridview
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật khách hàng không thành công");
                    }
                }
                else
                {
                    //do something if NO
                    ResetValues();
                }
            }
        }

        private void BtnXoa_Click(object sender, EventArgs e)
        {
            string soDT = txtDienthoai.Text;
            if (MessageBox.Show("Bạn có chắc muốn xóa dữ liệu khách hàng", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                //do something if YES
                if (busKhach.DeleteKhach(soDT))
                {
                    MessageBox.Show("Xóa dữ liệu khách hàng thành công");
                    ResetValues();
                    LoadGridview_Khach(); // refresh datagridview
                }
                else
                {
                    MessageBox.Show("Xóa dữ liệu khách hàng không thành công");
                }
            }
            else
            {
                //do something if NO
                ResetValues();
            }
        }

        private void BtnTimkiem_Click(object sender, EventArgs e)
        {
            string soDT = txttimKiem.Text;
            DataTable ds = busKhach.SearchKhach(soDT);

            if (ds.Rows.Count > 0)
            {
                dgvkhach.DataSource = ds;
                dgvkhach.Columns[0].HeaderText = "Điện Thoại";
                dgvkhach.Columns[1].HeaderText = "Họ và Tên";
                dgvkhach.Columns[2].HeaderText = "Địa Chỉ";
                dgvkhach.Columns[3].HeaderText = "Giới Tính";
                dgvkhach.Columns[4].Visible = false;
            }
            else
            {
                MessageBox.Show("Không tìm thấy khách hàng nào phù hợp tiêu chí tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttimKiem.Focus();
            }

            ResetValues(); // Gọi lại Reset form sau khi tìm kiếm xong
        }

        private void TxttimKiem_Click(object sender, EventArgs e)
        {
            txttimKiem.Text = null;
            txttimKiem.BackColor = Color.White;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void rbnam_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void txtTenkhach_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
