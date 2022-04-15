--CREATE DATABASE WebBanHangBTL
GO

USE WebBanHangBTL
GO

CREATE TABLE Administrator
(
  id INT NOT NULL,
  name VARCHAR(50) NOT NULL,
  username VARCHAR(50) NOT NULL,
  password VARCHAR(100) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Users
(
  id INT IDENTITY(1,1) NOT NULL,
  name NVARCHAR(50) NULL,
  email VARCHAR(100) NULL,
  username NVARCHAR(50) NULL,
  password VARCHAR(100) NULL,
  address NVARCHAR(100) NULL,
  time_created DATE DEFAULT GETDATE() NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Catalog
(
  id INT NOT NULL,
  name NVARCHAR(100) NOT NULL,
  PRIMARY KEY (id)
);

CREATE TABLE Product
(
  id INT NOT NULL,
  name VARCHAR(50) NOT NULL,
  price MONEY NOT NULL,
  content VARCHAR(50) NULL,
  discount INT NULL,
  image_link VARCHAR(50) NOT NULL,
  time_created DATETIME NOT NULL,
  views INT NOT NULL,
  catalog_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (catalog_id) REFERENCES Catalog(id)
);

CREATE TABLE Transactions
(
  id INT IDENTITY(1,1) NOT NULL,
  status VARCHAR(10) NOT NULL,
  user_name VARCHAR(50) NOT NULL,
  user_email VARCHAR(50) NOT NULL,
  amount INT NOT NULL,
  payment VARCHAR(30) NULL,
  payment_info VARCHAR(50) NULL,
  message VARCHAR(200) NULL,
  security INT NULL,
  time_created DATE NULL,
  user_id INT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (user_id) REFERENCES Users(id)
);

CREATE TABLE Orders
(
  id INT IDENTITY(1,1) NOT NULL,
  quantity INT NOT NULL,
  amount INT NOT NULL,
  data VARCHAR(50) NOT NULL,
  status INT NOT NULL,
  transaction_id INT NOT NULL,
  product_id INT NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (transaction_id) REFERENCES Transactions(id),
  FOREIGN KEY (product_id) REFERENCES Product(id)
)
GO

CREATE OR ALTER PROC SP_ADMIN_LOGIN @userName varchar(50), @password varchar(20)
AS
BEGIN
	DECLARE @count int
	DECLARE @res bit
	SELECT @count = COUNT(*) FROM Admin WHERE username = @userName AND password = @password
	IF @count > 0
		SET @res = 1
	ELSE
		SET @res = 0
	SELECT @res
END
GO

CREATE OR ALTER PROC SP_CATALOG_LISTALL
AS
SELECT * FROM Catalog
ORDER BY [name] ASC
GO

CREATE OR ALTER PROC SP_INSERT_CATEGORY @id INT, @name NVARCHAR(100)
AS
BEGIN 
	INSERT INTO Catalog(id, [name])
	VALUES (@id, @name)
END
GO

CREATE OR ALTER PROC SP_INSERT_ADMIN @id INT, @name VARCHAR(100), @username VARCHAR(50), @password VARCHAR(12)
AS
BEGIN 
	INSERT INTO Administrator(id, [name], username, [password])
	VALUES (@id, @name, @username, @password)
END
GO
