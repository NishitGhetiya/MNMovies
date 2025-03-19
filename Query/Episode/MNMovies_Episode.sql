
CREATE TABLE Episode (
    EpisodeID INT NOT NULL PRIMARY KEY IDENTITY(1,1),
    EpisodeName VARCHAR(100) NULL,
    EpisodeNumber INT NOT NULL,
    EpisodeImage VARCHAR(Max) NOT NULL,
    EpisodeVideo VARCHAR(Max) NOT NULL,
    EpisodeDescription VARCHAR(Max) NULL,
    EpisodeLength VARCHAR(100) NOT NULL,
    EpisodeDate DATETIME NOT NULL,
    SeasonID INT NOT NULL CONSTRAINT FK_Season FOREIGN KEY (SeasonID) REFERENCES Season(SeasonID),
	SeriesID INT NOT NULL CONSTRAINT FK_Episode_Series FOREIGN KEY (SeriesID) REFERENCES Series(SeriesID)
);

select * from Episode


----Select All From Episode Table--------------
alter procedure [dbo].[PR_Episode_SelectAll]
as
begin
	select
		[dbo].[Episode].[EpisodeID],
		[dbo].[Episode].[EpisodeName],
		[dbo].[Episode].[EpisodeNumber],
		[dbo].[Episode].[EpisodeImage],
		[dbo].[Episode].[EpisodeVideo],
		[dbo].[Episode].[EpisodeDescription],
		[dbo].[Episode].[EpisodeLength],
		[dbo].[Episode].[EpisodeDate],
		[dbo].[Season].[SeasonID],
		[dbo].[Season].[SeasonNumber],
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName]
	from [dbo].[Episode]
	LEFT OUTER JOIN [dbo].[Season]
		ON [dbo].[Episode].[SeasonID] = [dbo].[Season].[SeasonID]
	LEFT OUTER JOIN [dbo].[Series]
		ON [dbo].[Episode].[SeriesID] = [dbo].[Series].[SeriesID]
	ORDER BY [dbo].[Series].[SeriesID] DESC,
			 [dbo].[Season].[SeasonNumber] DESC,
			 [dbo].[Episode].[EpisodeNumber] DESC
end

----Select By Pk From Episode Table--------------
alter procedure [dbo].[PR_Episode_SelectByPk]
@EpisodeID int
as
begin
	select
		[dbo].[Episode].[EpisodeID],
		[dbo].[Episode].[EpisodeName],
		[dbo].[Episode].[EpisodeNumber],
		[dbo].[Episode].[EpisodeImage],
		[dbo].[Episode].[EpisodeVideo],
		[dbo].[Episode].[EpisodeDescription],
		[dbo].[Episode].[EpisodeLength],
		[dbo].[Episode].[EpisodeDate],
		[dbo].[Season].[SeasonID],
		[dbo].[Season].[SeasonNumber],
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName]
	from [dbo].[Episode]
	LEFT OUTER JOIN [dbo].[Season]
		ON [dbo].[Episode].[SeasonID] = [dbo].[Season].[SeasonID]
	LEFT OUTER JOIN [dbo].[Series]
		ON [dbo].[Episode].[SeriesID] = [dbo].[Series].[SeriesID]
	where [dbo].[Episode].[EpisodeID] = @EpisodeID
end

----Insert into Episode Table-------------------
alter procedure [dbo].[PR_Episode_Insert]
@EpisodeName varchar(100),
@EpisodeNumber int,
@EpisodeImage varchar(Max),
@EpisodeVideo varchar(Max),
@EpisodeDescription varchar(Max),
@EpisodeLength varchar(100),
@SeasonID int,
@SeriesID int
as
begin
	insert into [dbo].[Episode]
	(
		[EpisodeName],[EpisodeNumber],[EpisodeImage],[EpisodeVideo],[EpisodeDescription],[EpisodeLength],[EpisodeDate],[SeasonID],[SeriesID]
	)
	values
	(
		@EpisodeName,@EpisodeNumber,@EpisodeImage,@EpisodeVideo,@EpisodeDescription,@EpisodeLength,GETDATE(),@SeasonID,@SeriesID
	)
end


-------------Update into Episode Table -----------------
alter procedure [dbo].[PR_Episode_Update]
@EpisodeID int,
@EpisodeName varchar(100),
@EpisodeNumber int,
@EpisodeImage varchar(Max),
@EpisodeVideo varchar(Max),
@EpisodeDescription varchar(Max),
@EpisodeLength varchar(100),
@SeasonID int,
@SeriesID int
as
begin
	update [dbo].[Episode]
	SET
	[EpisodeName] = @EpisodeName,
	[EpisodeNumber] = @EpisodeNumber,
	[EpisodeImage] = @EpisodeImage,
	[EpisodeVideo] = @EpisodeVideo,
	[EpisodeDescription] = @EpisodeDescription,
	[EpisodeLength] = @EpisodeLength,
	[EpisodeDate] = GETDATE(),
	[SeasonID] = @SeasonID,
	[SeriesID] = @SeriesID
	where [dbo].[Episode].[EpisodeID] = @EpisodeID
end


----Delete Into Episode Table-------------------
alter procedure [dbo].[PR_Episode_Delete]
@EpisodeID int
as
begin
	delete from [dbo].[Episode]
	where [dbo].[Episode].[EpisodeID] = @EpisodeID
end



----Select By Season ID From Episode Table--------------
alter procedure [dbo].[PR_Episode_SelectBySeasonID]
@SeasonID int
as
begin
	select
		[dbo].[Episode].[EpisodeID],
		[dbo].[Episode].[EpisodeName],
		[dbo].[Episode].[EpisodeNumber],
		[dbo].[Episode].[EpisodeImage],
		[dbo].[Episode].[EpisodeVideo],
		[dbo].[Episode].[EpisodeDescription],
		[dbo].[Episode].[EpisodeLength],
		[dbo].[Episode].[EpisodeDate],
		[dbo].[Season].[SeasonID],
		[dbo].[Season].[SeasonNumber],
		[dbo].[Series].[SeriesID],
		[dbo].[Series].[SeriesName]
	from [dbo].[Episode]
	LEFT OUTER JOIN [dbo].[Season]
		ON [dbo].[Episode].[SeasonID] = [dbo].[Season].[SeasonID]
	LEFT OUTER JOIN [dbo].[Series]
		ON [dbo].[Episode].[SeriesID] = [dbo].[Series].[SeriesID]

	where [dbo].[Season].[SeasonID] = @SeasonID

	ORDER BY [dbo].[Episode].[EpisodeNumber]
end