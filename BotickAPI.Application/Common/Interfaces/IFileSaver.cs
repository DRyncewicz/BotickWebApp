using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Application.Common.Interfaces
{
    public interface IFileSaver
    {
        string SaveFile(byte[] fileData, string name, string[] acceptableExtensions, string folderPath);
    }
}
