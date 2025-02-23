CREATE PROCEDURE [dbo].[DangNhap] @email varchar(50),@matKhau nvarchar(50)
AS
BEGIN
      declare @status int

if exists(select * from tblNhanVien where email=@email and matKhau=@matKhau)
       set @status=1
else
       set @status=0
select @status
END
go
CREATE PROCEDURE QuenMatKhau @email varchar(50)
AS
BEGIN
      declare @status int
if exists(select MaNv from tblNhanVien where email=@email )
       set @status=1
else
       set @status=0
select @status
END
go
Create PROCEDURE TaoMatKhauMoi
@email nvarchar(50),
@matkhau nvarchar(20)
AS
BEGIN
UPDATE tblnhanvien SET matKhau = @matkhau
where email  =  @email
END
go
Create PROCEDURE [dbo].[DanhSachNV]
AS
BEGIN
      SELECT email, tenNv, diachi,vaitro, tinhtrang FROM tblNhanVien
END
go
CREATE procedure [dbo].[chgpwd]
(
@email Varchar(50),
@opwd nVarchar(50),
@npwd nVarchar(50)
)
AS
declare @op varchar(50)
select @op= matKhau from tblNhanVien where email=@email
if @op=@opwd
begin
update tblNhanVien set matKhau=@npwd where email=@email
return 1
end
else
return -1
go
Create PROCEDURE [dbo].[LayVaiTroNV] @email varchar(50)
AS
BEGIN
      SELECT vaitro FROM tblNhanVien
	  where email = @email
END
go
CREATE PROCEDURE [dbo].[InsertDataIntoTblNhanVien]
@email nvarchar(50),
@tennv varchar(50),
@diachi nvarchar(100),
@vaiTro tinyint,
@tinhTrang tinyint
AS
BEGIN
DECLARE @Manv VARCHAR(20);
DECLARE @Id INT;

SELECT @Id = ISNULL(MAX(ID),0) + 1 FROM tblNhanVien
SELECT @Manv = 'NV' + RIGHT('0000' + CAST(@Id AS VARCHAR(4)), 4)
INSERT INTO tblNhanVien(Manv,email,tenNv, diaChi,vaiTro,tinhTrang) 
VALUES (@Manv, @email, @tennv, @diachi,@vaiTro,@tinhTrang) 
END
go
CREATE PROCEDURE [dbo].[DeleteDataFromtblNhanVien]
@email varchar(50)
AS
BEGIN
DELETE FROM tblNhanVien
WHERE email = @email
END
go
CREATE PROCEDURE [dbo].[UpdateDataIntoTblNhanVien]
@email nvarchar(50),
@tenNv nvarchar(50),
@diaChi nvarchar(50),
@vaiTro tinyint,
@tinhTrang tinyint
AS
BEGIN
UPDATE tblnhanvien SET TenNv=@tenNv, diaChi=@diaChi,vaiTro=@vaiTro, tinhTrang =@tinhTrang
where email  =  @email
END
go
CREATE PROCEDURE [dbo].[SearchNhanVien]
@tenNv nvarchar(50)
AS
BEGIN
      SET NOCOUNT ON;
      SELECT email, tenNv, diachi,vaitro, tinhtrang 
      FROM tblnhanvien where tennv like '%' + @tenNv + '%'
END
go
