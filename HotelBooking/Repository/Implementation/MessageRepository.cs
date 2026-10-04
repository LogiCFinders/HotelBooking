using HotelBooking.Context;
using HotelBooking.Model;
using HotelBooking.Model.Messaging;
using HotelBooking.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;

namespace HotelBooking.Repository.Implementation
{
    public class MessageRepository : IMessaging
    {
        private readonly CompanyContext _context;

        public MessageRepository()
        {
            _context=new CompanyContext();
        }
        public MessageAPIConfig GetMessageConfiguration(int? HotelId, string Product, string MessageType)
        {
          return  _context.MessageAPIConfig.Where(c=>c.HotelId== HotelId && c.MSGPRODUCT==Product && c.MsgType==MessageType).SingleOrDefault();
        }
        public MessageTemplate GetMessageTemplate(int? HotelId, string Product, string MessageType)
        {
            return _context.MessageTemplate.Where(c => c.Hotelid == HotelId && c.MsgProduct == Product && c.MsgType == MessageType).SingleOrDefault(); 
        }
        public bool LogMessageHistory(Messages msg)
        {
            bool rtnVal = false;
            if (msg != null) {
                _context.Messages.Add(msg);
                _context.SaveChanges();
                rtnVal = true; 
            }

            return rtnVal;
        }

    }
}