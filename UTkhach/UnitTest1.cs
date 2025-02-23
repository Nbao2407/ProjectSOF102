using BUS_QLBanHang;
using DAL_QLBanHang;
using DTO_QLBanHang;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;
namespace UTkhach
{
    [TestClass]
    public class UnitTest1
    {
        private BUS_Khach busKhach;

        [TestInitialize]
        public void TestInitialize()
        {
            busKhach = new BUS_Khach();
        }

        [TestMethod]
        public void InsertKhachTrue()
        {
            // Arrange
            var khach = new DTO_Khach("0123456789", "Nguyen Van A", "Hanoi", "Nam", "fpoly@fe.edu.vn");

            // Act
            var result = busKhach.InsertKhach(khach);

            // Log để kiểm tra giá trị trả về
            Console.WriteLine($"Kết quả trả về khi thêm khách hàng: {result}");

            // Assert
            Assert.IsTrue(result, "Thêm khách hàng thất bại");
        }



        [TestMethod]
        public void UpdateKhach_ValidData_ShouldReturnTrue()
        {
            // Arrange
            var khach = new DTO_Khach("0123456789", "testUPdate", "HCM", "Nam", "fpoly@fe.edu.vn");

            // Act
            var result = busKhach.UpdateKhach(khach);

            // Assert
            Assert.IsTrue(result, "Cập nhật khách hàng không thành công!");
        }


     
        [TestMethod]
        public void DeleteKhach_ValidPhoneNumberTrue()
        {
            var phoneNumber = "0123456789";

            var result = busKhach.DeleteKhach(phoneNumber);

            Assert.IsTrue(result, "Xóa khách hàng không thành công với số điện thoại hợp lệ.");

        }
        [TestMethod]
        public void SearchKhach_ValidPhoneNumber()
        {
            // Arrange
            var soDT = "57697987987"; // Một số điện thoại đã có trong cơ sở dữ liệu

            // Act
            var result = busKhach.SearchKhach(soDT);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(0, result.Rows.Count, "Không tìm thấy khách hàng với số điện thoại " + soDT);
        }

        [TestMethod]
        public void SearchKhach_InvalidPhoneNumber()
        {
            // Arrange
            var soDT = "b"; // Một số điện thoại không tồn tại trong cơ sở dữ liệu

            // Act
            var result = busKhach.SearchKhach(soDT);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Rows.Count, "Dữ liệu trả về không rỗng mặc dù không có khách hàng với số điện thoại này.");
        }
    }
}

    