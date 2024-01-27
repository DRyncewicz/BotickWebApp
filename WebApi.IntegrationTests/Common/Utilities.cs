using BotickAPI.Domain.Entities;
using BotickAPI.Persistence.Context;

namespace WebApi.IntegrationTests.Common
{
    public class Utilities
    {
        public static void InitializeDbForTests(BotickDbContext context)
        {
            var artist = new Artist() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, Name = "TestName", Surname = "TestSurname", Age = 22, ArtName = "TestArtName", BirthCity = "CityTest", Discipline = "TestDis", Description = "TestDesc", Likes = 150 };
            context.Artists.Add(artist);

            var location = new Location() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, City = "TestCity", Venue = "TestVenue", Capacity = 5000 };
            context.Locations.Add(location);

            var eventObj = new BotickAPI.Domain.Entities.Event() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, OrganizerEmail = "user@user.pl", Name = "TestEvent", EventType = "TestType", ImagePath = "TestDirectory", Description = "TestDesc", StartTime = DateTime.Now.AddDays(15), EndTime = DateTime.Now.AddDays(16), Status = "in progress" };
            context.Events.Add(eventObj);

            var booking = new Booking() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, UserEmail = "user@user.pl", EventId = 1, TotalPrice = 250, BookingTime = DateTime.Now, Status = "Paid" };
            context.Bookings.Add(booking);

            var ticket = new Ticket() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, EventId = 1, Price = 250, Quantity = 100, TicketType = "Normal" };
            context.Tickets.Add(ticket);

            var bookingDetails = new BookingDetail() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, Id = 1, BookingId = 1, TicketId = 1, Quantity = 1 };
            context.BookingDetails.Add(bookingDetails);

            var eventReview = new EventReview() { CreatedBy = "admin@admin.pl", Created = DateTime.Now, StatusId = 1, EventId = 1, UserEmail = "user@user.pl", Rating = 10, Description = "TestDesc" };
            context.EventReviews.Add(eventReview);

            context.SaveChanges();
        }
    }
}
