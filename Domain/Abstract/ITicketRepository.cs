﻿using TicketManager.Domain.Entities;
using System.Linq;

namespace TicketManager.Domain.Abstract
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> Tickets { get; }
        void SaveTicket(Ticket tick);
        Ticket DeleteTicket(int tickId);
        int GetSize();
        IQueryable<Ticket> GetSpecificPage(int pageSize, int pageNumber, string orderByWhat = "EventTime", string asc = "False");
    }
}
