create trigger IptalTarihi_Olustur
on Tbl_Rezervasyonlar
after update
as
begin
	declare @iptal_txt varchar(10)
	declare @rezId int

	select @rezId= RezervasyonId, @iptal_txt=IptalMi from inserted

	if @iptal_txt='evet'
	begin 
		update Tbl_Rezervasyonlar set IptalTarihi=GETDATE() where RezervasyonId=@rezId
	end
end