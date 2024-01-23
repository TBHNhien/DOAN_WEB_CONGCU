--CREATE DATABASE OnlineShop_Text
--GO

--USE OnlineShop_Text
--GO

--CREATE TABLE Account 
--(
--	UserName VARCHAR(20) PRIMARY KEY,
--	Password VARCHAR(50)

--)
--GO

---- Xóa khoá chính hiện tại
--ALTER TABLE Account
--DROP CONSTRAINT PK_Account_UserName;

-- Thêm lại khoá chính với tên mới
--ALTER TABLE Account
--ADD CONSTRAINT PK_Account_UserName PRIMARY KEY (UserName);

--CREATE proc Sp_Account_Login @UserName VARCHAR(20), @Password VARCHAR(50)
--AS 
--	BEGIN
--	Declare @count int 
--	Declare @res bit
--	select @count = count(*) from Account where UserName = @UserName and Password = @Password 
--	if @count > 0
--		set @res = 1
--	else
--		set @res = 0

--	select @res
--	END






