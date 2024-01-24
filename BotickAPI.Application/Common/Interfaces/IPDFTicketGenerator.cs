using BotickAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Common.Interfaces
{
    public interface IPDFTicketGenerator
    {
        public byte[] GenerateTicket(Event @event);
    }
}
