using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Entities;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BotickAPI.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler(IBaseCommandRepository<Event> eventRepository,
        IMapper mapper,
        IFileSaver fileSaver,
        ICurrentUserService currentUserService,
        IDbQueryService dbQueryService,
        ILogger<CreateEventCommandHandler> log) : IRequestHandler<CreateEventCommand, int>
    {
        public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var eventEntity = mapper.Map<Event>(request);
            eventEntity.Status = "Inactive";
            eventEntity.OrganizerEmail = currentUserService.Email;

            try
            {
                eventEntity.ImagePath = fileSaver.SaveImageFile(request.Image,
                    request.Name);
            }
            catch
            {
                throw new NullReferenceException("Event file was recognized as null file type object");
            }

            await eventRepository.AddAsync(eventEntity, cancellationToken);
            await CreateRelationRecordsForNewEvent(eventEntity.Id, request.ArtistsId, request.LocationsId);
            log.LogInformation($"New event from user:{currentUserService.Email} was succesfully created");

            return eventEntity.Id;
        }

        private async Task CreateRelationRecordsForNewEvent(int eventId, List<int> artistIds, List<int> locationIds)
        {
            foreach (var artistId in artistIds)
            {
                var artistEventQuery = "INSERT INTO ArtistEvent (ArtistsId, EventsId) VALUES (@ArtistId, @EventId)";
                await dbQueryService.Execute(artistEventQuery, new { ArtistId = artistId, EventId = eventId });
            }

            foreach (var locationId in locationIds)
            {
                var locationEventQuery = "INSERT INTO LocationEvent (LocationId, EventId) VALUES (@LocationId, @EventId)";
                await dbQueryService.Execute(locationEventQuery, new { LocationId = locationId, EventId = eventId });
            }
        }
    }
}