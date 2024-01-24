using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotickAPI.Infrastructure.Helpers
{
    public static class GetFileExtensionHelper
    {
        public static string GetFileExtension(byte[] fileData)
        {
            if (fileData == null || fileData.Length < 4)
            {
                return null;
            }

            if (fileData[0] == 0xff && fileData[1] == 0xd8)
            {
                return ".jpeg";
            }

            if (fileData[0] == 0x89 && fileData[1] == 0x50 && fileData[2] == 0x4e && fileData[3] == 0x47)
            {
                return ".png";
            }

            if (fileData[0] == 0x47 && fileData[1] == 0x49 && fileData[2] == 0x46)
            {
                return ".gif";
            }

            if (fileData[0] == 0x25 && fileData[1] == 0x50 && fileData[2] == 0x44 && fileData[3] == 0x46)
            {
                return ".pdf";
            }

            if (fileData[0] == 0x42 && fileData[1] == 0x4D)
            {
                return ".bmp";
            }

            return null;
        }

    }
}
