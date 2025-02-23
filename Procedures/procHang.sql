CREATE PROCEDURE DanhSachHang
AS
BEGIN
      SET NOCOUNT ON;
      SELECT *
      FROM tblHang
END
go
CREATE PROCEDURE [dbo].[InsertDataIntoTblHang]
@tenHang nvarchar(50),
@soLuong int,
@donGiaNhap float,
@donGiaBan float,
@hinhAnh nvarchar(400),
@ghiChu nvarchar(50),
@email varchar(20)
AS
BEGIN
DECLARE @Manv VARCHAR(20);
select @Manv = manv from tblNhanVien where email=@email
INSERT INTO tblHang(TenHang, SoLuong, DonGiaNhap, DonGiaBan,HinhAnh,GhiChu,Manv) 
VALUES ( @tenHang, @soLuong, @donGiaNhap,@donGiaBan,@hinhAnh,@ghiChu,@Manv) 
END
go
CREATE PROCEDURE DeleteDataFromtblHang
@maHang int
AS
BEGIN
DELETE FROM tblHang
WHERE MaHang = @maHang
END
go
CREATE PROCEDURE [dbo].[UpdateDataIntoTblHang]
@maHang int,
@tenHang nvarchar(50),
@soLuong int,
@donGiaNhap float,
@donGiaBan float,
@hinhAnh nvarchar(400),
@ghiChu nvarchar(50)
AS
BEGIN
UPDATE tblhang SET TenHang=@tenHang, SoLuong=@soLuong,
DonGiaNhap=@donGiaNhap,DonGiaBan=@donGiaBan,HinhAnh=@hinhAnh,GhiChu=@ghiChu
where MaHang  =  @maHang
END
go
CREATE PROCEDURE [dbo].[SearchHang]
@tenHang nvarchar(50)
AS
BEGIN
      SET NOCOUNT ON;
      SELECT TenHang,SoLuong,DonGiaNhap,DonGiaBan,HinhAnh,GhiChu
      FROM tblHang where TenHang like '%' + @tenHang + '%'
END
go