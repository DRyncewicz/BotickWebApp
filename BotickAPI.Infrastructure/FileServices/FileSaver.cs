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
    public class FileSaver: IFileSaver
    {
        private readonly string _folderPath;
        private readonly IDateTime _dateTime;

        public FileSaver(IOptions<FileSaveConfig> config, IDateTime dateTime)
        {
            _dateTime = dateTime;
            _folderPath = config.Value.FolderPath;

            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
        }

        public string SaveImageFile(byte[] fileData, string name)
        {
            string fileExtension = GetFileExtensionHelper.GetFileExtension(fileData);
            if (fileExtension != ".jpg" && fileExtension != ".png")
            {
                throw new ArgumentException("Unauthorized file extension.");
            }

            string fileName = $"{_dateTime.Now:yyyyMMddHHmmss}_{name}{fileExtension}";
            string filePath = Path.Combine(_folderPath, fileName);
            File.WriteAllBytes(filePath, fileData);

            return filePath;
        }
    }
}