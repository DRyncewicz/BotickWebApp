using BotickAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Data.SqlClient;
using Dapper;

namespace BotickAPI.Application.Helpers
{
    public static class MapDapperQueryForEventListToReceiveLocationAndArtistEntity
    {
        public static async Task<List<Event>> MapAllAsync(SqlConnection connection, string query, string searchValue)
        {
            var eventDictionary = new Dictionary<int, Event>();

            var eventList = (await connection.QueryAsync<Event, Location, Artist, Event>(
                query,
                (eventObj, location, artist) =>
                {
                    if (!eventDictionary.TryGetValue(eventObj.Id, out var eventEntry))
                    {
                        eventEntry = eventObj;
                        eventEntry.Locations = new List<Location>();
                        eventEntry.Artists = new List<Artist>();
                        eventDictionary.Add(eventEntry.Id, eventEntry);
                    }

                    if (location != null && !eventEntry.Locations.Any(l => l.Id == location.Id))
                    {
                        eventEntry.Locations.Add(location);
                    }

                    if (artist != null && !eventEntry.Artists.Any(a => a.Id == artist.Id))
                    {
                        eventEntry.Artists.Add(artist);
                    }

                    return eventEntry;
                },
                new { SearchValue = searchValue },
                splitOn: "Id,Id"
            )).Distinct().ToList();

            return eventList;
        }
    }
}
