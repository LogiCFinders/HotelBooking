using HotelBooking.Model;
using HotelBooking.Model.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Repository.Interface
{
    public interface IMessaging
    {
        MessageAPIConfig GetMessageConfiguration(int? HotelId,string Product,string MessageType);
        MessageTemplate GetMessageTemplate(int? HotelId, string Product, string MessageType);
        bool LogMessageHistory(Messages msg);
    }
}
