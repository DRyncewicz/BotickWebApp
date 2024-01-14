using BotickAPI.Application.Common.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Persistence.Context
{
    internal sealed class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
    {
        public SqlConnection CreateConnection()
        {
            return new SqlConnection(
                configuration.GetConnectionString("DefaultConnectionString"));
        }
    }
}
