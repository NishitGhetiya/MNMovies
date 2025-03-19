
CREATE TABLE User (
    UserID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    UserName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Password VARCHAR(100) NOT NULL,
    MobileNo VARCHAR(15) NOT NULL,
    Role BIT NOT NULL
);

select * from [dbo].[User]

---------User Login--------------------
create PROCEDURE [dbo].[PR_User_Login]
    @UserName VARCHAR(100),
    @Password VARCHAR(100)
AS
BEGIN
    SELECT 
        [dbo].[User].[UserID], 
        [dbo].[User].[UserName],
        [dbo].[User].[Email],
        [dbo].[User].[Password],
		[dbo].[User].[MobileNo],
		[dbo].[User].[Role]
    FROM 
        [dbo].[User] 
    WHERE 
        [dbo].[User].[UserName] = @UserName 
        AND [dbo].[User].[Password] = @Password;
END

----User Register-----------------------
create PROCEDURE [dbo].[PR_User_Register]
    @UserName VARCHAR(100),
    @Password VARCHAR(100),
    @Email VARCHAR(100),
    @MobileNo VARCHAR(15),
	@Role bit
AS
BEGIN
    INSERT INTO [dbo].[User]
    (
        [UserName],
        [Password],
        [Email],
        [MobileNo],
		[Role]
    )
    VALUES
    (
        @UserName,
        @Password,
        @Email,
        @MobileNo,
		@Role
    );
END

----Select All From User Table--------------
create procedure [dbo].[PR_User_SelectAll]
as
begin
	select
		[dbo].[User].[UserID],
		[dbo].[User].[UserName],
		[dbo].[User].[Email],
		[dbo].[User].[Password],
		[dbo].[User].[MobileNo],
		[dbo].[User].[Role]
	from [dbo].[User]
	order by [dbo].[User].[UserID]
end


----select by Id---------------------------
create procedure [dbo].[PR_User_SelectById]
@UserID int
as
begin
	select
		[dbo].[User].[UserID],
		[dbo].[User].[UserName],
		[dbo].[User].[Email],
		[dbo].[User].[Password],
		[dbo].[User].[MobileNo],
		[dbo].[User].[Role]
	from [dbo].[User]

	where [dbo].[User].[UserID] = @UserID

	order by [dbo].[User].[UserID]
end

----Insert into User Table-------------------
create procedure [dbo].[PR_User_Insert]
@UserName varchar(100),
@Email varchar(100),
@Password varchar(100),
@MobileNo varchar(15),
@Role bit
as
begin
	insert into [dbo].[User]
	(
		[UserName],[Email],[Password],[MobileNo],[Role]
	)
	values
	(
		@UserName,@Email,@Password,@MobileNo,@Role
	)
end


----Update into User Table-----------------
create procedure [dbo].[PR_User_UpdateById]
@UserID int,
@UserName varchar(100),
@Email varchar(100),
@Password varchar(100),
@MobileNo varchar(15),
@Role bit
as
begin
	update [dbo].[User]
	set [UserName] = @UserName,
		[Email] = @Email,
		[Password] = @Password,
		[MobileNo] = @MobileNo,
		[Role] = @Role
	where [dbo].[User].[UserID] = @UserID
end


----Delete Into User Table-------------------
create procedure [dbo].[PR_User_DeleteById]
@UserID int
as
begin
	delete from [dbo].[User]
	where [dbo].[User].[UserID] = @UserID
end

