
CREATE TABLE Season (
    SeasonID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    SeasonNumber INT NOT NULL,
    SeasonImage VARCHAR(Max) NOT NULL,
    SeasonDescription VARCHAR(Max) NOT NULL,
    SeasonDate DATETIME NOT NULL,
    SeriesID INT NOT NULL CONSTRAINT FK_Series FOREIGN KEY (SeriesID) REFERENCES Series(SeriesID)
);

select * from Season

ALTER PROCEDURE [dbo].[PR_Season_SeriesWiseCount]
AS
BEGIN
    SELECT TOP(10)
		SR.SeriesID,
        SR.SeriesName,
        COUNT(S.SeasonID) AS Seasons
    FROM [dbo].[Season] S
    INNER JOIN [dbo].[Series] SR ON S.SeriesID = SR.SeriesID
    GROUP BY SR.SeriesID , SR.SeriesName
    ORDER BY Seasons DESC;
END


----Select All From Season Table--------------
alter procedure [dbo].[PR_Season_SelectAll]
as
begin
	select
		[dbo].[Season].[SeasonID],
		[dbo].[Season].[SeasonNumber],
		[dbo].[Season].[SeasonImage],
		[dbo].[Season].[SeasonDescription],
		[dbo].[Season].[SeasonDate],
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName]
	from [dbo].[Season]
	LEFT OUTER JOIN [dbo].[Series]
	ON [dbo].[Season].[SeriesID] = [dbo].[Series].[SeriesID]
	ORDER BY [dbo].[Series].[SeriesID] DESC,
			 [dbo].[Season].[SeasonNumber] DESC
end


----Select By Pk From Season Table--------------
alter procedure [dbo].[PR_Season_SelectByPk]
@SeasonID int
as
begin
	select
		[dbo].[Season].[SeasonID],
		[dbo].[Season].[SeasonNumber],
		[dbo].[Season].[SeasonImage],
		[dbo].[Season].[SeasonDescription],
		[dbo].[Season].[SeasonDate],
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName]
	from [dbo].[Season]
	LEFT OUTER JOIN [dbo].[Series]
	ON [dbo].[Season].[SeriesID] = [dbo].[Series].[SeriesID]
	where [dbo].[Season].[SeasonID] = @SeasonID
end

----Insert into Season Table-------------------
alter procedure [dbo].[PR_Season_Insert]
@SeasonNumber int,
@SeasonImage varchar(Max),
@SeasonDescription varchar(Max),
@SeriesID int
as
begin
	insert into [dbo].[Season]
	(
		[SeasonNumber],[SeasonImage],[SeasonDescription],[SeasonDate],[SeriesID]
	)
	values
	(
		@SeasonNumber,@SeasonImage,@SeasonDescription,GETDATE(),@SeriesID
	)
end


-------------Update into Season Table -----------------
alter procedure [dbo].[PR_Season_Update]
@SeasonID int,
@SeasonNumber int,
@SeasonImage varchar(Max),
@SeasonDescription varchar(Max),
@SeriesID int
as
begin
	update [dbo].[Season]
	SET
		[SeasonNumber] = @SeasonNumber,
		[SeasonImage] = @SeasonImage,
		[SeasonDescription] = @SeasonDescription,
		[SeasonDate] = GETDATE(),
		[SeriesID] = @SeriesID
	where
		[dbo].[Season].[SeasonID] = @SeasonID
end


----Delete Into Season Table-------------------
alter procedure [dbo].[PR_Season_Delete]
@SeasonID int
as
begin
	delete from [dbo].[Season]
	where [dbo].[Season].[SeasonID] = @SeasonID
end


----Select By Series ID From Season Table--------------
alter procedure [dbo].[PR_Season_SelectBySeriesID]
@SeriesID int
as
begin
	select
		[dbo].[Season].[SeasonID],
		[dbo].[Season].[SeasonNumber],
		[dbo].[Season].[SeasonImage],
		[dbo].[Season].[SeasonDescription],
		[dbo].[Season].[SeasonDate],
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName]
	from [dbo].[Season]
	LEFT OUTER JOIN [dbo].[Series]
	ON [dbo].[Season].[SeriesID] = [dbo].[Series].[SeriesID]
	where [dbo].[Series].[SeriesID] = @SeriesID
	ORDER BY [dbo].[Season].[SeasonNumber]
end
