
CREATE TABLE Series (
    SeriesID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    SeriesName VARCHAR(100) NOT NULL,
    SeriesImage VARCHAR(Max) NOT NULL,
    TypeOfSeries VARCHAR(100) NOT NULL,
    SeriesCategory VARCHAR(100) NOT NULL,
    SeriesType VARCHAR(100) NOT NULL,
    SeriesLanguage VARCHAR(100) NOT NULL,
    SeriesDescription VARCHAR(Max) NOT NULL,
	SeriesDate DATETIME NOT NULL,
    SeriesView INT NULL
);

select * from Series

CREATE PROCEDURE [dbo].[PR_Series_Count]
AS
Begin
	SELECT COUNT(*) AS TotalSeries FROM [dbo].[Series];
End

----Select All From Series Table--------------
alter procedure [dbo].[PR_Series_SelectAll]
as
begin
	select
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName],
		[dbo].[Series].[SeriesImage],
		[dbo].[Series].[TypeOfSeries],
		[dbo].[Series].[SeriesCategory],
		[dbo].[Series].[SeriesType],
		[dbo].[Series].[SeriesLanguage],
		[dbo].[Series].[SeriesDescription],
		[dbo].[Series].[SeriesDate],
		[dbo].[Series].[SeriesView]
	from [dbo].[Series]
	ORDER BY [dbo].[Series].[SeriesID] DESC
end

----Select BY PK From Series Table--------------
alter procedure [dbo].[PR_Series_SelectByPk]
@SeriesID int
as
begin
	select
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName],
		[dbo].[Series].[SeriesImage],
		[dbo].[Series].[TypeOfSeries],
		[dbo].[Series].[SeriesCategory],
		[dbo].[Series].[SeriesType],
		[dbo].[Series].[SeriesLanguage],
		[dbo].[Series].[SeriesDescription],
		[dbo].[Series].[SeriesDate],
		[dbo].[Series].[SeriesView]
	from [dbo].[Series]
	where [dbo].[Series].[SeriesID] = @SeriesID
end

----Insert into Series Table-------------------
alter procedure [dbo].[PR_Series_Insert]
@SeriesName varchar(100),
@SeriesImage varchar(Max),
@TypeOfSeries varchar(100),
@SeriesCategory varchar(100),
@SeriesType varchar(100),
@SeriesLanguage varchar(100),
@SeriesDescription varchar(Max)
as
begin
	insert into [dbo].[Series]
	(
		[SeriesName],[SeriesImage],[TypeOfSeries],[SeriesCategory],[SeriesType],[SeriesLanguage],[SeriesDescription],[SeriesDate]
	)
	values
	(
		@SeriesName,@SeriesImage,@TypeOfSeries,@SeriesCategory,@SeriesType,@SeriesLanguage,@SeriesDescription,GETDATE()
	)
end

-------------Update into Series Table -----------------
alter procedure [dbo].[PR_Series_Update]
@SeriesID int,
@SeriesName varchar(100),
@SeriesImage varchar(Max),
@TypeOfSeries varchar(100),
@SeriesCategory varchar(100),
@SeriesType varchar(100),
@SeriesLanguage varchar(100),
@SeriesDescription varchar(Max)
as
begin
	update [dbo].[Series]
	SET
		[SeriesName] = @SeriesName,
		[SeriesImage] = @SeriesImage,
		[TypeOfSeries] = @TypeOfSeries,
		[SeriesCategory] = @SeriesCategory,
		[SeriesType] = @SeriesType,
		[SeriesLanguage] = @SeriesLanguage,
		[SeriesDescription] = @SeriesDescription,
		[SeriesDate] = GETDATE()
	where
		[dbo].[Series].[SeriesID] = @SeriesID
end

----Delete Into Series Table-------------------
alter procedure [dbo].[PR_Series_Delete]
@SeriesID int
as
begin
	delete from [dbo].[Series]
	where [dbo].[Series].[SeriesID] = @SeriesID
end


----Select top 10 record From Series Table--------------
alter procedure [dbo].[PR_Series_SelectTopTen]
as
begin
	select top(10)
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName],
		[dbo].[Series].[SeriesImage],
		[dbo].[Series].[TypeOfSeries],
		[dbo].[Series].[SeriesCategory],
		[dbo].[Series].[SeriesType],
		[dbo].[Series].[SeriesLanguage],
		[dbo].[Series].[SeriesDescription],
		[dbo].[Series].[SeriesDate],
		[dbo].[Series].[SeriesView]
	from [dbo].[Series]
	ORDER BY [dbo].[Series].[SeriesView] DESC
end

----------select latest Series--------------------
alter procedure [dbo].[PR_Series_SelectLatest]
as
begin
	select top(10)
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName],
		[dbo].[Series].[SeriesImage],
		[dbo].[Series].[TypeOfSeries],
		[dbo].[Series].[SeriesCategory],
		[dbo].[Series].[SeriesType],
		[dbo].[Series].[SeriesLanguage],
		[dbo].[Series].[SeriesDescription],
		[dbo].[Series].[SeriesDate],
		[dbo].[Series].[SeriesView]
	from [dbo].[Series]
	
	ORDER BY [dbo].[Series].[SeriesDate] DESC
end

-------------Increment Views in Series-------------
alter PROCEDURE [dbo].[PR_Series_IncrementViews]
@SeriesID INT
AS
BEGIN
    UPDATE [dbo].[Series]
    SET
	[SeriesView] = ISNULL(SeriesView, 0) + 1
    WHERE [dbo].[Series].[SeriesID] = @SeriesID;
END


---------select Latest for Home Slider-------------
alter procedure [dbo].[PR_Series_SelectLatestForHomeSlider]
as
begin
	select top(1)
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName],
		[dbo].[Series].[SeriesImage],
		[dbo].[Series].[TypeOfSeries],
		[dbo].[Series].[SeriesCategory],
		[dbo].[Series].[SeriesType],
		[dbo].[Series].[SeriesLanguage],
		[dbo].[Series].[SeriesDescription],
		[dbo].[Series].[SeriesDate],
		[dbo].[Series].[SeriesView]
	from [dbo].[Series]

	ORDER BY [dbo].[Series].[SeriesDate] DESC
end


----Select top record From Series Table--------------
alter procedure [dbo].[PR_Series_SelectTopForHomeSlider]
as
begin
	select top(1)
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName],
		[dbo].[Series].[SeriesImage],
		[dbo].[Series].[TypeOfSeries],
		[dbo].[Series].[SeriesCategory],
		[dbo].[Series].[SeriesType],
		[dbo].[Series].[SeriesLanguage],
		[dbo].[Series].[SeriesDescription],
		[dbo].[Series].[SeriesDate],
		[dbo].[Series].[SeriesView]
	from [dbo].[Series]
	ORDER BY [dbo].[Series].[SeriesView] DESC
end