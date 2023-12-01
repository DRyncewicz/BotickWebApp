﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BotickAPI.Application.Common.Interfaces
{
    public interface IBotickDbContext
    {
         DbSet<Booking> Bookings { get; set; }

         DbSet<BookingDetail> BookingDetails { get; set; }

         DbSet<Ticket> Tickets { get; set; }

         DbSet<Event> Events { get; set; }

         DbSet<EventReview> EventReviews { get; set; }
    }
}
