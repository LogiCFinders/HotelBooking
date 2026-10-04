

select * from HODB_Admin.MessageAPIConfig
select * from HODB_Admin.TemplateMaster
INSERT INTO HODB_Admin.MessageAPIConfig(Hotelid,ApiUrl,APIKEY,APIPWD,MSGPRODUCT,MsgType,Status)
VALUES(Null, 'https://graph.facebook.com/v22.0/606483765891831/messages',
'EAAKaNjNEzksBOz5ZBHZBZCU8wFcN0e42qRzDUO39LcAFmqj1ObEmJgssdlcvbdFPipzYZC56XESx4fZBPlZAf5SxwoXBPStBxdPWzDZAcj9bGdVoh5HXC1CeUV0t1Eyj2IN0yGWpoetItcsug2NDBNtTeuMD4QTmv6vhmd1XOQUBmq1vzLnaZAKH2DJddFbHevZATSmZASAvCO7LLZBcTMB2bVbPsJSWsjZCxRKZCZAlZBGzsWCmgZDZD',
null,'W','B',1);

UPDATE HODB_Admin.MessageAPIConfig
set MSGSender='9748235062'