select * from MessageAPIConfig
Insert into MessageAPIConfig(HotelId,ApiUrl,APIKEY,APIPWD,MSGPRODUCT,MsgType,MSGSender,Status)
values(999999,'smtpout.secureserver.net','infor@logicfinders.com','2k25$','E','B','infor@logicfinders.com',1)

select * from TemplateMaster;
INSERT INTO TemplateMaster(TempMSG,Status,Hotelid,DateAdded,DateModified,MsgProduct,MsgType)
VALUES('Dear @@Name    Your Booking has beed confirmed for @RoomName dated @CheckinDate to @CheckourDate.  We are Expecting @totalPax passangers under @BookingRef.',
		1,999999,GETDATE(),GETDATE(),'E','B')