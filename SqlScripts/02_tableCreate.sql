
CREATE TABLE Superhero(
	Id int IDENTITY(1, 1) PRIMARY KEY,
	[Name] nvarchar (30),
	Alias nvarchar (30),
	Origin nvarchar (250)
);

CREATE TABLE Assistant(
	Id int IDENTITY(1, 1) PRIMARY KEY,
	[Name] nvarchar (30)
);

CREATE TABLE [Power](
	Id int IDENTITY(1, 1) PRIMARY KEY,
	[Name] nvarchar (30),
	[Description] nvarchar (250)
);