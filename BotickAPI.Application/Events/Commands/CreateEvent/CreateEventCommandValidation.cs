﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Application.Common.Interfaces;
using FluentValidation;

namespace BotickAPI.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandValidation : AbstractValidator<CreateEventCommand>
    {
        public CreateEventCommandValidation(IDateTime dateTime)
        {
            RuleFor(p => p.Name).NotEmpty()
                .WithMessage("Name for event is required.");
            RuleFor(p => p.StartTime).GreaterThan(dateTime.Now.AddDays(14))
                .WithMessage("Event should be planned at least two weeks in advance");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required")
                .MinimumLength(100).WithMessage("Minimum length of description is 100 letters");
            RuleFor(p => p.EventType).NotEmpty()
                .WithMessage("Event type is required.");
            RuleFor(p => p.EndTime).GreaterThan(p => p.StartTime)
                .WithMessage("The event should end after the start, this field is not required");
            RuleFor(p => p.Image).NotNull().NotEmpty()
                .WithMessage("Image is required");
            RuleFor(p => p.ArtistsId).NotEmpty()
                .WithMessage("At least one artist should be chosen");
            RuleFor(p => p.LocationId).NotEmpty()
                .WithMessage("Location should be chosen");
        }
    }
}
