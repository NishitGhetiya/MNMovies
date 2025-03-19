
CREATE TABLE Movie (
    MovieID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    MovieName VARCHAR(100) NOT NULL,
    MovieImage VARCHAR(Max) NOT NULL,
    MovieVideo VARCHAR(Max) NOT NULL,
    TypeOfMovie VARCHAR(100) NOT NULL,
    MovieCategory VARCHAR(100) NOT NULL,
    MovieType VARCHAR(100) NOT NULL,
    MovieLanguage VARCHAR(100) NOT NULL,
    MovieDescription VARCHAR(Max) NOT NULL,
    MovieLength VARCHAR(100) NOT NULL,
    MovieDate DATETIME NOT NULL,
    MovieViews INT NULL
);

select * from Movie

CREATE PROCEDURE [dbo].[PR_Movie_Count]
AS
Begin
	SELECT COUNT(*) AS TotalMovies FROM [dbo].[Movie];
End

----Select All From Movie Table--------------
alter procedure [dbo].[PR_Movie_SelectAll]
as
begin
	select
		[dbo].[Movie].[MovieID],
		[dbo].[Movie].[MovieName],
		[dbo].[Movie].[MovieImage],
		[dbo].[Movie].[MovieVideo],
		[dbo].[Movie].[TypeOfMovie],
		[dbo].[Movie].[MovieCategory],
		[dbo].[Movie].[MovieType],
		[dbo].[Movie].[MovieLanguage],
		[dbo].[Movie].[MovieDescription],
		[dbo].[Movie].[MovieLength],
		[dbo].[Movie].[MovieDate],
		[dbo].[Movie].[MovieViews]
	from [dbo].[Movie]
	ORDER BY [dbo].[Movie].[MovieID] DESC
end

----Select By Pk From Movie Table--------------
alter procedure [dbo].[PR_Movie_SelectByPk]
@MovieID int
as
begin
	select
		[dbo].[Movie].[MovieID],
		[dbo].[Movie].[MovieName],
		[dbo].[Movie].[MovieImage],
		[dbo].[Movie].[MovieVideo],
		[dbo].[Movie].[TypeOfMovie],
		[dbo].[Movie].[MovieCategory],
		[dbo].[Movie].[MovieType],
		[dbo].[Movie].[MovieLanguage],
		[dbo].[Movie].[MovieDescription],
		[dbo].[Movie].[MovieLength],
		[dbo].[Movie].[MovieDate],
		[dbo].[Movie].[MovieViews]
	from [dbo].[Movie]
	where [dbo].[Movie].[MovieID] = @MovieID
end

----Insert into Movie Table-------------------
alter procedure [dbo].[PR_Movie_Insert]
@MovieName varchar(100),
@MovieImage varchar(Max),
@MovieVideo varchar(Max),
@TypeOfMovie varchar(100),
@MovieCategory varchar(100),
@MovieType varchar(100),
@MovieLanguage varchar(100),
@MovieDescription varchar(Max),
@MovieLength varchar(100)
as
begin
	insert into [dbo].[Movie]
	(
		[MovieName],[MovieImage],[MovieVideo],[TypeOfMovie],[MovieCategory],[MovieType],[MovieLanguage],[MovieDescription],[MovieLength],[MovieDate]
	)
	values
	(
		@MovieName,@MovieImage,@MovieVideo,@TypeOfMovie,@MovieCategory,@MovieType,@MovieLanguage,@MovieDescription,@MovieLength,GETDATE()
	)
end


-------------Update into Movie Table -----------------
alter procedure [dbo].[PR_Movie_Update]
@MovieID int,
@MovieName varchar(100),
@MovieImage varchar(Max),
@MovieVideo varchar(Max),
@TypeOfMovie varchar(100),
@MovieCategory varchar(100),
@MovieType varchar(100),
@MovieLanguage varchar(100),
@MovieDescription varchar(Max),
@MovieLength varchar(100)
as
begin
	update [dbo].[Movie]
	SET
	[MovieName] = @MovieName,
	[MovieImage] = @MovieImage,
	[MovieVideo] = @MovieVideo,
	[TypeOfMovie] = @TypeOfMovie,
	[MovieCategory] = @MovieCategory,
	[MovieType] = @MovieType,
	[MovieLanguage] = @MovieLanguage,
	[MovieDescription] = @MovieDescription,
	[MovieLength] = @MovieLength,
	[MovieDate] = GETDATE()
	where [dbo].[Movie].[MovieID] = @MovieID
end


----Delete Into Movie Table-------------------
alter procedure [dbo].[PR_Movie_Delete]
@MovieID int
as
begin
	delete from [dbo].[Movie]
	where [dbo].[Movie].[MovieID] = @MovieID
end

--------select top 10 record from Movie table---------
alter procedure [dbo].[PR_Movie_SelectTopTen]
as
begin
	select top(10)
		[dbo].[Movie].[MovieID],
		[dbo].[Movie].[MovieName],
		[dbo].[Movie].[MovieImage],
		[dbo].[Movie].[MovieVideo],
		[dbo].[Movie].[TypeOfMovie],
		[dbo].[Movie].[MovieCategory],
		[dbo].[Movie].[MovieType],
		[dbo].[Movie].[MovieLanguage],
		[dbo].[Movie].[MovieDescription],
		[dbo].[Movie].[MovieLength],
		[dbo].[Movie].[MovieDate],
		[dbo].[Movie].[MovieViews]
	from [dbo].[Movie]
	ORDER BY [dbo].[Movie].[MovieViews] DESC
end


--------select New Releas from Movie table---------
alter procedure [dbo].[PR_Movie_SelectLatest]
as
begin
	select top(10)
		[dbo].[Movie].[MovieID],
		[dbo].[Movie].[MovieName],
		[dbo].[Movie].[MovieImage],
		[dbo].[Movie].[MovieVideo],
		[dbo].[Movie].[TypeOfMovie],
		[dbo].[Movie].[MovieCategory],
		[dbo].[Movie].[MovieType],
		[dbo].[Movie].[MovieLanguage],
		[dbo].[Movie].[MovieDescription],
		[dbo].[Movie].[MovieLength],
		[dbo].[Movie].[MovieDate],
		[dbo].[Movie].[MovieViews]
	from [dbo].[Movie]
	ORDER BY [dbo].[Movie].[MovieDate] DESC
end

---------------Increment Views in Movie----------------
ALTER PROCEDURE [dbo].[PR_Movie_IncrementViews]
@MovieID INT
AS
BEGIN
    UPDATE [dbo].[Movie]
    SET
	[MovieViews] = ISNULL(MovieViews, 0) + 1
    WHERE [dbo].[Movie].[MovieID] = @MovieID;
END


--------select New Releas from Movie table---------
alter procedure [dbo].[PR_Movie_SelectLatestForHomeSlider]
as
begin
	select top(1)
		[dbo].[Movie].[MovieID],
		[dbo].[Movie].[MovieName],
		[dbo].[Movie].[MovieImage],
		[dbo].[Movie].[MovieVideo],
		[dbo].[Movie].[TypeOfMovie],
		[dbo].[Movie].[MovieCategory],
		[dbo].[Movie].[MovieType],
		[dbo].[Movie].[MovieLanguage],
		[dbo].[Movie].[MovieDescription],
		[dbo].[Movie].[MovieLength],
		[dbo].[Movie].[MovieDate],
		[dbo].[Movie].[MovieViews]
	from [dbo].[Movie]
	ORDER BY [dbo].[Movie].[MovieDate] DESC
end


--------select top record from Movie table---------
alter procedure [dbo].[PR_Movie_SelectTopForHomeSlider]
as
begin
	select top(1)
		[dbo].[Movie].[MovieID],
		[dbo].[Movie].[MovieName],
		[dbo].[Movie].[MovieImage],
		[dbo].[Movie].[MovieVideo],
		[dbo].[Movie].[TypeOfMovie],
		[dbo].[Movie].[MovieCategory],
		[dbo].[Movie].[MovieType],
		[dbo].[Movie].[MovieLanguage],
		[dbo].[Movie].[MovieDescription],
		[dbo].[Movie].[MovieLength],
		[dbo].[Movie].[MovieDate],
		[dbo].[Movie].[MovieViews]
	from [dbo].[Movie]
	ORDER BY [dbo].[Movie].[MovieViews] DESC
end
