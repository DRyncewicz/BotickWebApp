using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BotickAPI.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler(IBaseCommandRepository<Event> eventRepository,
        IMapper mapper,
        IFileSaver fileSaver,
        ICurrentUserService currentUserService) : IRequestHandler<CreateEventCommand, int>
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

            return eventEntity.Id;
        }
    }
}
