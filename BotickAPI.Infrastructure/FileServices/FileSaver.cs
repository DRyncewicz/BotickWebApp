using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotickAPI.Application.Common.Interfaces;
using BotickAPI.Infrastructure.Helpers;
using Microsoft.Extensions.Options;

namespace BotickAPI.Infrastructure.FileServices
{
    public class FileSaver : IFileSaver
    {
        private readonly IDateTime _dateTime;

        public FileSaver(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        public string SaveFile(byte[] fileData, string name, string[] acceptableExtensions, string folderPath)
        {
            string fileExtension = GetFileExtensionHelper.GetFileExtension(fileData);
            if (acceptableExtensions.Contains(fileExtension))
            {
                throw new ArgumentException("Unauthorized file extension.");
            }
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string fileName = $"{_dateTime.Now:yyyyMMddHHmmss}_{name}{fileExtension}";
            string filePath = Path.Combine(folderPath, fileName);
            File.WriteAllBytes(filePath, fileData);

            return filePath;
        }
    }
}