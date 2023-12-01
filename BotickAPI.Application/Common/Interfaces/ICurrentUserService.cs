using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string Email { get; set; }

        bool IsAuthenticated { get; set; }
    }
}
