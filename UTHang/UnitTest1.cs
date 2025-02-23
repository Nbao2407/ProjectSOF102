using BUS_QLBanHang;
using DAL_QLBanHang;
using DTO_QLBanHang;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace UTHang
{
    [TestClass]
    public class UnitTest1
    {
        private DAL_Hang dalHang;
        private DTO_Hang testHang;

        [TestInitialize]
        public void Setup()
        {
            dalHang = new DAL_Hang();
            testHang = new DTO_Hang("Test Product", 10, 5000, 7000, "\\Images\\Huy béo.jpg", "Test note","fpoly@fe.edu.vn");
    }
        [TestMethod]
            public void ConstructorsetTrue()
            {

            int maHang = 1;
                string tenHang = "Sản phẩm A";
                int soLuong = 10;
                float donGiaNhap = 1000f;
                float donGiaBan = 1500f;
                string hinhAnh = "image.jpg";
                string ghiChu = "Ghi chú sản phẩm";

            DTO_Hang hang = new DTO_Hang(maHang, tenHang, soLuong, donGiaNhap, donGiaBan, hinhAnh, ghiChu);

            Assert.AreEqual(maHang, hang.MaHang);
                Assert.AreEqual(tenHang, hang.TenHang);
                Assert.AreEqual(soLuong, hang.SoLuong);
                Assert.AreEqual(donGiaNhap, hang.DonGiaNhap);
                Assert.AreEqual(donGiaBan, hang.DonGiaBan);
                Assert.AreEqual(hinhAnh, hang.HinhAnh);
                Assert.AreEqual(ghiChu, hang.GhiChu);
            }

            [TestMethod]
            public void WithEmailsCorrectly()
            {
                string tenHang = "Sản phẩm B";
                int soLuong = 5;
                float donGiaNhap = 2000f;
                float donGiaBan = 2500f;
                string hinhAnh = "imageB.jpg";
                string ghiChu = "Ghi chú sản phẩm B";
                string emailNv = "test@example.com";

                DTO_Hang hang = new DTO_Hang(tenHang, soLuong, donGiaNhap, donGiaBan, hinhAnh, ghiChu, emailNv);

                Assert.AreEqual(soLuong, hang.SoLuong);
                Assert.AreEqual(donGiaNhap, hang.DonGiaNhap);
                Assert.AreEqual(donGiaBan, hang.DonGiaBan);
                Assert.AreEqual(hinhAnh, hang.HinhAnh);
                Assert.AreEqual(ghiChu, hang.GhiChu);
                Assert.AreEqual(emailNv, hang.EmailNV);
            }

        [TestMethod]
        public void TestCreate()
        {
            bool result = dalHang.insertHang(testHang);
            Assert.IsTrue(result, "Thêm sản phẩm thất bại");

            testHang.MaHang = dalHang.GetMaHangByTenHang(testHang.TenHang);
            Assert.IsTrue(testHang.MaHang > 0, "Không lấy được MaHang sau khi thêm");
        }


        [TestMethod]
        public void TestRead()
        {
            DataTable result = dalHang.getHang();
            Assert.IsNotNull(result, "Không lấy được danh sách hàng");
            Assert.IsTrue(result.Rows.Count > 0, "Danh sách hàng trống");
        }

        [TestMethod]
        public void TestUpdate()
        {
            string oldName = "Test Product";  // Tên sản phẩm cũ
            string newName = "Updated Test"; // Tên mới để cập nhật

            int maHang = dalHang.GetMaHangByTenHang(oldName);
            Assert.IsTrue(maHang > 0, $"Không tìm thấy sản phẩm '{oldName}' để cập nhật");

            testHang.MaHang = maHang;
            testHang.TenHang = newName; // Cập nhật tên mới

            // Gọi phương thức cập nhật
            bool result = dalHang.UpdateHang(testHang);
            Assert.IsTrue(result, "Cập nhật sản phẩm thất bại");

        }

        [TestMethod]
        public void TestDelete()
        {
            string tenCanXoa = "Updated Test"; // Chỉ xóa sản phẩm có tên này

            int maHang = dalHang.GetMaHangByTenHang(tenCanXoa);
            Assert.IsTrue(maHang > 0, $"Không tìm thấy sản phẩm có tên {tenCanXoa} để xóa");

            Console.WriteLine($"Mã Hàng cần xóa: {maHang}");

            // Tiến hành xóa
            bool result = dalHang.DeleteHang(maHang);
            Assert.IsTrue(result, $"Xóa sản phẩm '{tenCanXoa}' thất bại");
        }
        [TestMethod]
        public void SearchHang()
        {
            var ten = "h1"; 
            var result = dalHang.SearchHang(ten);
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Rows.Count, "Không tìm thấy hàng với với " + ten);
        }

        [TestMethod]
        public void SearchHangfaild()
        {
            var ten = "b"; 

            var result = dalHang.SearchHang(ten);

            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Rows.Count, "Dữ liệu trả về không rỗng mặc dù.");
        }

    }
}

