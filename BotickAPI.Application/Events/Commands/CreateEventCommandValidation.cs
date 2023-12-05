using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Application.Common.Interfaces;
using FluentValidation;

namespace BotickAPI.Application.Events.Commands
{
    public class CreateEventCommandValidation : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidation(IDateTime dateTime)
        {
            RuleFor(p => p.CreateEventVm.Name).NotEmpty()
                .WithMessage("Name for event is required.");
            RuleFor(p => p.CreateEventVm.OrganizerEmail).NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email address.");
            RuleFor(p => p.CreateEventVm.StartTime).GreaterThan(dateTime.Now.AddDays(14))
                .WithMessage("Event should be planned at least two weeks in advance");
            RuleFor(p => p.CreateEventVm.Description).NotEmpty().WithMessage("Description is required")
                .MinimumLength(100).WithMessage("Minimum length of description is 100 letters");
            RuleFor(p => p.CreateEventVm.EventType).NotEmpty()
                .WithMessage("Event type is required.");
            RuleFor(p => p.CreateEventVm.EndTime).GreaterThan(p => p.CreateEventVm.StartTime)
                .WithMessage("The event should end after the start, this field is not required");
            RuleFor(p => p.CreateEventVm.Image).NotNull().NotEmpty()
                .WithMessage("Image is required");
        }
    }
}
