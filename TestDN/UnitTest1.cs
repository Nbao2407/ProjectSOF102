using BUS_QLBanHang;
using DAL_QLBanHang;
using DTO_QLBanHang;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Data;
using System.Threading;
using System.Transactions;
using System.Windows.Forms;
using static TemplateProject1_QLBanHang.frmNhanVien;

namespace UTNV
{
    [TestClass]
    public class UnitTest_QLBanHang
    {
        private BUS_NhanVien _busNhanVien;

        [TestInitialize]
        public void Setup()
        {
            _busNhanVien = new BUS_NhanVien();
           
        }
        [TestMethod]
        public void NhanVienDangNhap_TC01() // Test đăng nhập thất bại
        {
            DTO_NhanVien nv = new DTO_NhanVien
            {
                EmailNV = "fpoly@fe.edu.vn",
                MatKhau = "SaiMatKhau"
            };
            DAL_NhanVien login = new DAL_NhanVien();

            bool result = login.NhanVienDangNhap(nv);
            Console.WriteLine("Kết quả test case: " + result);
            Assert.IsFalse(result, "Đăng nhập với mật khẩu sai nhưng lại thành công!");
        }

        [TestMethod]
        public void NhanVienDangNhap_TC02() // Test đăng nhập thành công
        {
            DTO_NhanVien nv = new DTO_NhanVien
            {
                EmailNV = "fpoly@fe.edu.vn",
                MatKhau = "87010491662331191334" // Cần kiểm tra cách lưu mật khẩu
            };
            DAL_NhanVien login = new DAL_NhanVien();

            bool result = login.NhanVienDangNhap(nv);
            Console.WriteLine("Kết quả test case: " + result);
            Assert.IsTrue(result, "Đăng nhập thất bại dù đúng thông tin!");
        }

        [TestMethod]
        public void TestEmailValidation()
        {
            string[] validEmails = { "test@example.com", "user.name+tag@domain.co" };
            string[] invalidEmails = { "invalid-email", "@missingusername.com" };

            foreach (var email in validEmails)
            {
                bool result = EmailValidator.IsValid(email);
                Assert.IsTrue(result, $"Email hợp lệ '{email}' nhưng bị báo lỗi.");
            }

            foreach (var email in invalidEmails)
            {
                bool result = EmailValidator.IsValid(email);
                Assert.IsFalse(result, $"Email không hợp lệ '{email}' nhưng lại được chấp nhận.");
            }
        }
        [TestMethod]
        [Priority(1)]
        public void CreateNhanVienTrue()
        {
            var dal = new DAL_NhanVien();
            var nhanVien = new DTO_NhanVien
            {
                EmailNV = "test@gmail.com",
                TenNhanVien = "Test User",
                DiaChi = "123 test",
                VaiTro = 1,
                TinhTrang = 1
            };
            bool result = dal.insertNhanVien(nhanVien);
            Assert.IsTrue(result, "Thêm nhân viên thất bại mặc dù dữ liệu hợp lệ.");
        }
        [TestMethod]
        public void CreateNhanVieNFalse()
        {
            bool result = _busNhanVien.insertNhanVien(null);

            Assert.IsFalse(result, "Không thể tạo nhân viên null.");
        }
        [TestMethod]
        [Priority(2)]

        public void Test_UpdateNhanVien()
        {
            var dal = new DAL_NhanVien();
            var testEmail = "test@gmail.com";

            // 🟢 Kiểm tra nhân viên có tồn tại không
            DataTable dtOriginal = dal.VaiTroNhanVien(testEmail);
            Assert.IsTrue(dtOriginal.Rows.Count > 0, "Không tìm thấy nhân viên để cập nhật!");

            // 🔹 Cập nhật thông tin nhân viên (KHÔNG đổi email)
            DTO_NhanVien nv = new DTO_NhanVien
            {
                EmailNV = testEmail,  // Giữ nguyên email
                TenNhanVien = "Tên mới",
                DiaChi = "TP.HCM",
                VaiTro = 2,
                TinhTrang = 0
            };

            bool isUpdated = dal.UpdateNhanVien(nv);
            Assert.IsTrue(isUpdated, "Cập nhật nhân viên thất bại!");
            DataTable dtAfterUpdate = dal.VaiTroNhanVien(testEmail);
            Assert.IsTrue(dtAfterUpdate.Rows.Count > 0, "Không tìm thấy nhân viên sau cập nhật!");
            Thread.Sleep(400);
            dal.DeleteNhanVien(testEmail);

        }


        [TestMethod]
        [Priority(3)]

        public void Test_DeleteNhanVien()
        {
            var dal = new DAL_NhanVien();
            var nhanVien = new DTO_NhanVien
            {
                EmailNV = "deletetest@gmail.com",
                TenNhanVien = "TestDelete User",
                DiaChi = "123 test",
                VaiTro = 0,
                TinhTrang = 0
            };
            var testEmail = "deletetest@gmail.com";
            bool result = dal.insertNhanVien(nhanVien);
            DataTable dtOriginal = dal.VaiTroNhanVien(testEmail);
            Assert.IsTrue(dtOriginal.Rows.Count > 0, "Không tìm thấy nhân viên để test xóa!");

            bool isDeleted = dal.DeleteNhanVien(testEmail);
            Assert.IsTrue(isDeleted, "Xóa nhân viên thất bại!");

            DataTable dtAfterDelete = dal.VaiTroNhanVien(testEmail);
            Assert.AreEqual(0, dtAfterDelete.Rows.Count, "Nhân viên vẫn còn tồn tại sau khi xóa!");
        }
        [TestMethod]
        public void SearchNv()
        {
           
            var ten = "bao"; 

         
            var result = _busNhanVien.SearchNhanVien(ten);


            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Rows.Count, "Không tìm thấy nv với tên " + ten);
        }

        [TestMethod]
        public void SearchNvfaild()
        {
            // Arrange
            var ten = "dat";

            // Act
            var result = _busNhanVien.SearchNhanVien(ten);


            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Rows.Count, "Dữ liệu trả về không rỗng mặc dù không có nv với tên.");
        }

    }

}
