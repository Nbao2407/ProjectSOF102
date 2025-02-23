CREATE PROCEDURE [dbo].[DanhSachKhach]
AS
BEGIN
      SET NOCOUNT ON;
      SELECT *
      FROM tblKhach
END
go

CREATE PROCEDURE [dbo].[InsertDataIntoTblKhach]
@dienThoai varchar(15),@tenKhach nvarchar(50),
@diaChi nvarchar(100),@phai nvarchar(5),@email varchar(20)
AS
BEGIN
DECLARE @Manv VARCHAR(20);
select @Manv = manv from tblNhanVien where email=@email
INSERT INTO tblKhach(DienThoai, TenKhach,DiaChi,phai,Manv) 
VALUES ( @dienThoai,@tenKhach,@diaChi,@phai,@Manv) 
END
go

CREATE PROCEDURE [dbo].[DeleteDataFromtblKhach]
@dienthoai varchar(15)
AS
BEGIN
DELETE FROM tblKhach
WHERE DienThoai = @dienthoai
END
go
CREATE PROCEDURE [dbo].[UpdateDataIntoTblKhach]
@dienThoai varchar(15),
@tenKhach nvarchar(50),
@diaChi nvarchar(100),
@phai nvarchar(5)
AS
BEGIN
UPDATE tblKhach SET TenKhach=@tenKhach, DiaChi=@diaChi, phai=@phai
where dienThoai  =  @dienThoai
END
go
CREATE PROCEDURE [dbo].[SearchKhach]
@dienthoai varchar(15)
AS
BEGIN
      SET NOCOUNT ON;
      SELECT *
      FROM tblKhach where DienThoai like + '%' + @dienthoai + '%'
END
go