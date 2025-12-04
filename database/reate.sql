-- CreateDatabase.sql
USE master;
GO

-- 1. Создаем базу данных с явным указанием пути
IF NOT EXISTS(SELECT name FROM sys.databases WHERE name = 'SaleHousesDB')
BEGIN
    -- Указываем путь к вашей папке
    CREATE DATABASE [SaleHousesDB]
    ON PRIMARY 
    (
        NAME = N'SaleHousesDB',
        FILENAME = N'D:\Project1\WinFormsSaleHouses\SaleHousesDB.mdf',
        SIZE = 10MB,
        MAXSIZE = UNLIMITED,
        FILEGROWTH = 5MB
    )
    LOG ON 
    (
        NAME = N'SaleHousesDB_log',
        FILENAME = N'D:\Project1\WinFormsSaleHouses\SaleHousesDB_log.ldf',
        SIZE = 5MB,
        MAXSIZE = UNLIMITED,
        FILEGROWTH = 10%
    )
    PRINT 'База данных SaleHousesDB создана.';
END
ELSE
BEGIN
    PRINT 'База данных SaleHousesDB уже существует.';
END
GO

-- 2. Переключаемся на созданную базу
USE [SaleHousesDB];
GO

-- 3. Создаем таблицу Houses (Дома/Квартиры)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Houses' AND xtype='U')
BEGIN
    CREATE TABLE Houses (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        [Address] NVARCHAR(200) NOT NULL,
        Price DECIMAL(18,2) NOT NULL,
        Area DECIMAL(10,2) NOT NULL,
        Rooms INT NOT NULL DEFAULT 1,
        [Type] NVARCHAR(50) NOT NULL DEFAULT 'Квартира',
        [Floor] INT,
        TotalFloors INT,
        [Description] NVARCHAR(500),
        IsSold BIT NOT NULL DEFAULT 0,
        CreatedDate DATETIME DEFAULT GETDATE(),
        SaleDate DATETIME NULL
    )
    
    -- Создаем индексы для быстрого поиска
    CREATE INDEX IX_Houses_Address ON Houses([Address]);
    CREATE INDEX IX_Houses_Price ON Houses(Price);
    CREATE INDEX IX_Houses_IsSold ON Houses(IsSold);
    CREATE INDEX IX_Houses_Type ON Houses([Type]);
    
    PRINT 'Таблица Houses создана.';
END
GO

-- 4. Создаем таблицу Clients (Клиенты)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clients' AND xtype='U')
BEGIN
    CREATE TABLE Clients (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        Phone NVARCHAR(20),
        Email NVARCHAR(100),
        [Address] NVARCHAR(200),
        RegistrationDate DATETIME DEFAULT GETDATE()
    )
    
    -- Создаем уникальный индекс для email
    CREATE UNIQUE INDEX IX_Clients_Email ON Clients(Email) WHERE Email IS NOT NULL;
    CREATE INDEX IX_Clients_FullName ON Clients(FullName);
    
    PRINT 'Таблица Clients создана.';
END
GO

-- 5. Создаем таблицу Sales (Продажи)
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Sales' AND xtype='U')
BEGIN
    CREATE TABLE Sales (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        HouseId INT NOT NULL,
        ClientId INT NOT NULL,
        SalePrice DECIMAL(18,2) NOT NULL,
        Commission DECIMAL(18,2) DEFAULT 0,
        SaleDate DATETIME DEFAULT GETDATE(),
        Notes NVARCHAR(500),
        
        -- Внешние ключи
        CONSTRAINT FK_Sales_Houses FOREIGN KEY (HouseId) 
            REFERENCES Houses(Id) ON DELETE CASCADE,
        CONSTRAINT FK_Sales_Clients FOREIGN KEY (ClientId) 
            REFERENCES Clients(Id) ON DELETE CASCADE
    )
    
    -- Индексы
    CREATE INDEX IX_Sales_SaleDate ON Sales(SaleDate);
    CREATE INDEX IX_Sales_HouseId ON Sales(HouseId);
    CREATE INDEX IX_Sales_ClientId ON Sales(ClientId);
    
    PRINT 'Таблица Sales создана.';
END
GO

-- 6. Добавляем тестовые данные
PRINT 'Добавляем тестовые данные...';
GO

-- Проверяем, есть ли уже данные в Houses
IF (SELECT COUNT(*) FROM Houses) = 0
BEGIN
    -- Добавляем тестовые дома/квартиры
    INSERT INTO Houses ([Address], Price, Area, Rooms, [Type], [Floor], TotalFloors, [Description])
    VALUES
    (N'ул. Ленина, 10, кв. 5', 3500000.00, 65.50, 3, N'Квартира', 2, 5, N'Квартира в центре города, свежий ремонт'),
    (N'пр. Победы, 25', 7500000.00, 120.00, 5, N'Дом', 1, 2, N'Кирпичный дом с участком 6 соток'),
    (N'ул. Садовая, 15, кв. 12', 4200000.00, 55.00, 2, N'Квартира', 4, 9, N'Евроремонт, вид на парк'),
    (N'ул. Лесная, 8', 9200000.00, 180.00, 6, N'Дом', 1, 2, N'Загородный дом с баней и гаражом'),
    (N'ул. Центральная, 45, кв. 7', 2800000.00, 48.50, 2, N'Квартира', 3, 5, N'Квартира в новостройке');
    
    PRINT 'Добавлено 5 записей в таблицу Houses.';
END
GO

-- Добавляем тестовых клиентов
IF (SELECT COUNT(*) FROM Clients) = 0
BEGIN
    INSERT INTO Clients (FullName, Phone, Email, [Address])
    VALUES
    (N'Иванов Иван Иванович', '+79161234567', 'ivanov@mail.ru', N'ул. Примерная, 1'),
    (N'Петрова Мария Сергеевна', '+79162345678', 'petrova@gmail.com', N'пр. Строителей, 25'),
    (N'Сидоров Алексей Владимирович', '+79163456789', 'sidorov@yandex.ru', N'ул. Зеленая, 15'),
    (N'Козлова Екатерина Андреевна', '+79164567890', 'kozlova@mail.ru', N'ул. Цветочная, 8');
    
    PRINT 'Добавлено 4 записи в таблицу Clients.';
END
GO

-- Добавляем тестовые продажи
IF (SELECT COUNT(*) FROM Sales) = 0
BEGIN
    -- Продаем первый дом первому клиенту
    INSERT INTO Sales (HouseId, ClientId, SalePrice, Commission, Notes)
    VALUES (1, 1, 3400000.00, 85000.00, N'Продажа состоялась через месяц после размещения');
    
    -- Отмечаем дом как проданный
    UPDATE Houses SET IsSold = 1, SaleDate = GETDATE() WHERE Id = 1;
    
    PRINT 'Добавлена тестовая продажа.';
END
GO

-- 7. Создаем хранимые процедуры для удобства
PRINT 'Создаем хранимые процедуры...';
GO

-- Процедура для получения всех доступных домов
CREATE OR ALTER PROCEDURE GetAvailableHouses
AS
BEGIN
    SELECT 
        Id,
        [Address],
        Price,
        Area,
        Rooms,
        [Type],
        [Floor],
        TotalFloors,
        [Description],
        CreatedDate
    FROM Houses
    WHERE IsSold = 0
    ORDER BY Price;
END
GO

-- Процедура для добавления нового дома
CREATE OR ALTER PROCEDURE AddNewHouse
    @Address NVARCHAR(200),
    @Price DECIMAL(18,2),
    @Area DECIMAL(10,2),
    @Rooms INT = 1,
    @Type NVARCHAR(50) = 'Квартира',
    @Floor INT = NULL,
    @TotalFloors INT = NULL,
    @Description NVARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO Houses ([Address], Price, Area, Rooms, [Type], [Floor], TotalFloors, [Description])
    VALUES (@Address, @Price, @Area, @Rooms, @Type, @Floor, @TotalFloors, @Description);
    
    SELECT SCOPE_IDENTITY() AS NewId;
END
GO

-- Процедура для поиска домов по цене
CREATE OR ALTER PROCEDURE SearchHousesByPriceRange
    @MinPrice DECIMAL(18,2) = 0,
    @MaxPrice DECIMAL(18,2) = 999999999
AS
BEGIN
    SELECT * FROM Houses
    WHERE Price BETWEEN @MinPrice AND @MaxPrice
    AND IsSold = 0
    ORDER BY Price;
END
GO

PRINT '=== БАЗА ДАННЫХ УСПЕШНО СОЗДАНА И НАСТРОЕНА ===';
PRINT 'Файлы базы данных:';
PRINT ' - D:\Project1\WinFormsSaleHouses\SaleHousesDB.mdf';
PRINT ' - D:\Project1\WinFormsSaleHouses\SaleHousesDB_log.ldf';
PRINT '';
PRINT 'Таблицы:';
PRINT ' - Houses (Дома/Квартиры)';
PRINT ' - Clients (Клиенты)';
PRINT ' - Sales (Продажи)';
PRINT '';
PRINT 'Для подключения используйте строку:';
PRINT 'Server=(localdb)\mssqllocaldb;Database=SaleHousesDB;Trusted_Connection=True;';
GO