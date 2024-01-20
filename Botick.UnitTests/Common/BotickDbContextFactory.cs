using AutoMapper;
using Azure.Core;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Entities;
using BotickAPI.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.Common
{
    public static class BotickDbContextFactory
    {
        public static Mock<BotickDbContext> Create()
        {
            var dateTime = new DateTime(2000, 1, 1);
            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.Now).Returns(dateTime);

            var currentUserMock = new Mock<ICurrentUserService>();
            currentUserMock.Setup(m => m.Email).Returns("user@user.pl");
            currentUserMock.Setup(m => m.IsAuthenticated).Returns(true);

            var options = new DbContextOptionsBuilder<BotickDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var mock = new Mock<BotickDbContext>(options, dateTimeMock.Object, currentUserMock.Object) { CallBase = true };

            var context = mock.Object;

            context.Database.EnsureCreated();

            var artist = new Artist() {CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, Name = "TestName", Surname = "TestSurname", Age = 22, ArtName = "TestArtName", BirthCity = "CityTest", Discipline = "TestDis", Description = "TestDesc", Likes = 150};
            context.Artists.Add(artist);

            var location = new Location() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, City = "TestCity", Venue = "TestVenue", Capacity = 5000 };
            context.Locations.Add(location);

            var eventObj = new BotickAPI.Domain.Entities.Event() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, OrganizerEmail = "user@user.pl", Name = "TestEvent", EventType = "TestType", ImagePath = "TestDirectory", Description = "TestDesc", StartTime = DateTime.Now.AddDays(15), EndTime = DateTime.Now.AddDays(16), Status = "in progress" };
            context.Events.Add(eventObj);   

            var booking = new Booking() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, UserEmail = "user@user.pl", EventId = 1 , TotalPrice = 250, BookingTime = DateTime.Now, Status = "Paid"};
            context.Bookings.Add(booking);

            var ticket = new Ticket() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, EventId = 1, Price = 250, Quantity = 100, TicketType = "Normal" };
            context.Tickets.Add(ticket);

            var bookingDetails = new BookingDetail() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, BookingId = 1, TicketId = 1, Quantity = 1 };
            context.BookingDetails.Add(bookingDetails);

            var eventReview = new EventReview() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, EventId = 1, UserEmail = "user@user.pl", Rating = 10, Description = "TestDesc" };
            context.EventReviews.Add(eventReview);

            context.SaveChanges();

            return mock;
        }

        public static void Destroy(BotickDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
