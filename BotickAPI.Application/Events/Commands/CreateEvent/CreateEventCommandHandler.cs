using AutoMapper;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Common;
using BotickAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BotickAPI.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler(IMapper mapper,
        IFileSaver fileSaver,
        ICurrentUserService currentUserService,
        ILogger<CreateEventCommandHandler> log,
        IBotickDbContext dbContext) : IRequestHandler<CreateEventCommand, int>
    {
        public async Task<int> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var newEvent = mapper.Map<Event>(request);
            newEvent.Status = "Inactive";
            newEvent.OrganizerEmail = currentUserService.Email;

            try
            {
                newEvent.ImagePath = fileSaver.SaveImageFile(request.Image,
                    request.Name);
            }
            catch
            {
                throw new NullReferenceException("Event file was recognized as null file type object");
            }

            newEvent.Artists = await GetCollectionsFromIdListAsync<Artist>(request.ArtistsId);
            await dbContext.Events.AddAsync(newEvent);

            await dbContext.SaveChangesAsync(cancellationToken);
            log.LogInformation($"New event from user:{currentUserService.Email} was succesfully created");

            return newEvent.Id;
        }


        private async Task<List<T>> GetCollectionsFromIdListAsync<T>(List<int> ids) where T : Artist
        {
            var entityListObject = await dbContext.Set<T>().Where(e => ids.Contains(e.Id)).ToListAsync();
            return entityListObject;
        }
    }
}