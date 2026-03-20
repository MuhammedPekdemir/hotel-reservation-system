CREATE DATABASE OtelDB;
GO
USE OtelDB;
GO

CREATE TABLE Odalar
(
    OdaNo INT PRIMARY KEY,
    Tip NVARCHAR(10) NOT NULL,
    GecelikFiyat DECIMAL(10,2) NOT NULL,
    MüsaitMi BIT NOT NULL DEFAULT 1
);

CREATE TABLE Musteriler 
(
    MusteriID INT PRIMARY KEY IDENTITY(1,1),
    Ad NVARCHAR(50) NOT NULL,
    Soyad NVARCHAR(50) NOT NULL,
    Telefon NVARCHAR(15)
);

CREATE TABLE Rezervasyonlar 
(
    RezervasyonID INT PRIMARY KEY IDENTITY(1,1),
    OdaNo INT FOREIGN KEY REFERENCES Odalar(OdaNo),
    MusteriID INT FOREIGN KEY REFERENCES Musteriler(MusteriID),
    GirisTarih DATE NOT NULL,
    CikisTarih DATE NOT NULL,
    ToplamUcret DECIMAL(10,2)
);

CREATE TABLE Yoneticiler 
(
    YoneticiID INT PRIMARY KEY IDENTITY(1,1),
    Ad NVARCHAR(50) NOT NULL,
    Sifre NVARCHAR(50) NOT NULL
);
GO

CREATE TRIGGER trg_RezAl
ON Rezervasyonlar
AFTER INSERT
AS
BEGIN
    UPDATE Odalar SET MüsaitMi = 0 WHERE OdaNo = (SELECT OdaNo FROM inserted);
END;
GO

CREATE TRIGGER trg_RezIptal
ON Rezervasyonlar
AFTER DELETE
AS
BEGIN
    UPDATE Odalar SET MüsaitMi = 1 WHERE OdaNo = (SELECT OdaNo FROM deleted);
END;
GO

CREATE PROCEDURE proc_DoluOdalariGetir
AS
BEGIN
    SELECT OdaNo, Tip, GecelikFiyat FROM Odalar WHERE MüsaitMi = 0;
END;
GO

CREATE PROCEDURE OdaMusaitMi
AS
BEGIN
    SELECT OdaNo, Tip, GecelikFiyat FROM Odalar WHERE MüsaitMi = 1;
END;
GO

CREATE PROCEDURE proc_RezGecmisi
    @MusteriID INT
AS
BEGIN
    SELECT r.RezervasyonID, r.GirisTarih, r.CikisTarih, r.ToplamUcret,
           m.Ad + ' ' + m.Soyad AS Musteri, o.OdaNo, o.Tip
    FROM Rezervasyonlar r
    INNER JOIN Odalar o ON r.OdaNo = o.OdaNo
    INNER JOIN Musteriler m ON r.MusteriID = m.MusteriID
    WHERE r.MusteriID = @MusteriID;
END;
GO

CREATE PROCEDURE proc_TarihAraligindaMüsaitOdalar
    @GirisTarih DATE,
    @CikisTarih DATE
AS
BEGIN
    SELECT OdaNo, Tip, GecelikFiyat FROM Odalar
    WHERE OdaNo NOT IN 
    (
        SELECT OdaNo FROM Rezervasyonlar
        WHERE GirisTarih < @CikisTarih
        AND CikisTarih > @GirisTarih
    );
END;
GO
