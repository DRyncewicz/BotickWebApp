﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace BotickAPI.Application.Artists.Queries.GetArtistsForCreateEventForm
{
    public class GetArtistsForCreateEventFormQuery : IRequest<List<ArtistsForCreateEventFormVm>>
    {
        public string SearchString { get; set; }
    }
}
