using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SM.Core.Common.Enums.Media;
using SM.Core.Common.Helpers;
using SM.Core.DTOs.Media;
using SM.Core.Interfaces.Services.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructre.Services.Media
{
    public class FileService : IFileService
    {
        public readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        public FileService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        private async Task CopyFileAsync(string path, IFormFile file)
        {
            try
            {
                await using FileStream fileStream = new(path, FileMode.Create, FileAccess.Write, FileShare.None, 1024 * 1024, useAsync: false);
                await file.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
            }
            catch (Exception ex)
            {
                throw new SystemException(ex.Message);
            }

        }

        private string GenerateFileUrl(string fileName,RegisteredFileType registeredFileType)
        {
            string CDN = _configuration["Common:CDN"];

            if (!String.IsNullOrEmpty(CDN))
                return Path.Combine(CDN, Path.Combine("resources",registeredFileType.ToString()), fileName);
            else
                return Path.Combine(_configuration["Common:Host"],Path.Combine("resources", registeredFileType.ToString(),fileName));
        }

        private async Task<string> FileRenameAsync(string fileName, string path, bool overwrite = false)
        {
            return await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldFileName = Path.GetFileNameWithoutExtension(fileName);
                string regulatedFileName = FileHelper.CharacterRegularity(oldFileName);

                var files = Directory.GetFiles(path, regulatedFileName + "*");

                if (files.Length == 0) return regulatedFileName + extension;

                if (files.Length == 1) return regulatedFileName + "-2" + extension;

                int[] fileNumbers = new int[files.Length];
                int lastHyphenIndex;
                for (int i = 0; i < files.Length; i++)
                {
                    lastHyphenIndex = files[i].LastIndexOf("-");
                    if (lastHyphenIndex == -1)
                        fileNumbers[i] = 1;
                    else
                        fileNumbers[i] = int.Parse(files[i].Substring(lastHyphenIndex + 1, files[i].Length - extension.Length - lastHyphenIndex - 1));
                }
                var biggestNumber = fileNumbers.Max();
                biggestNumber++;
                return regulatedFileName + "-" + biggestNumber + extension;
            });

        }

        public async Task<FileInformationDTO> UploadAsync(IFormFile formFile, RegisteredFileType registeredFileType)
        {
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,"resources", registeredFileType.ToString());

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath); 

            var newFileName = await FileRenameAsync(formFile.FileName, uploadPath);

            await CopyFileAsync(Path.Combine(uploadPath, newFileName), formFile);

            return new FileInformationDTO(newFileName,GenerateFileUrl(newFileName,registeredFileType));
        }
    }
}
