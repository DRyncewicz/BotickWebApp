using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BotickAPI.Persistence.Configuration
{
    public class EventReviewConfiguration : IEntityTypeConfiguration<EventReview>
    {
        public void Configure(EntityTypeBuilder<EventReview> builder)
        { 

        }
    }
}
