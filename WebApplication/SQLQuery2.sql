CREATE TRIGGER md5_sifrele
ON Tbl_Musteriler
AFTER INSERT
AS
BEGIN
    DECLARE @sifre_txt VARCHAR(32), @sifre_md5 VARCHAR(32)
    DECLARE @tc VARCHAR(11)

    SELECT @tc = TcKimlik, @sifre_txt = Sifre FROM inserted

    SET @sifre_md5 = CONVERT(VARCHAR(32), HASHBYTES('MD5', @sifre_txt), 2)

    UPDATE Tbl_Musteriler SET Sifre = @sifre_md5 WHERE TcKimlik = @tc
END
