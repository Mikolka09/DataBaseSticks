--Если все сразу таблицы не создадутся, то создавайте каждую по отдельности (коментируя их по очереди)!!!

CREATE TABLE [User](
	[id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[login] NVARCHAR(50) NOT NULL UNIQUE,
	[password] NVARCHAR(MAX) NOT NULL
);

CREATE TABLE [Friend](
	[id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[id_stick] INT NOT NULL,
	[login] NVARCHAR(50) NOT NULL UNIQUE
);

CREATE TABLE [Stick](
	[id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[title] NVARCHAR(MAX) NOT NULL,
	[id_creator] INT NOT NULL,
	[date] DATETIME NOT NULL,
	[color] NVARCHAR(MAX) NOT NULL
);

CREATE TABLE [Tags](
	[id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[id_stick] INT NOT NULL,
	[tag] NVARCHAR(MAX) NOT NULL
);

CREATE TABLE [TextCheck](
	[id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[id_stick] INT NOT NULL,
	[text] NVARCHAR(MAX) NOT NULL,
	[is_checked] INT NOT NULL
)