using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SM.Core.Common.Enums.Media;
using SM.Core.Common.Helpers;
using SM.Core.DTOs.Media;
using SM.Core.Interfaces.Services.Media;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Infrastructre.Services.Media
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FileService(IWebHostEnvironment webHostEnvironment, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
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
            string cdnUrl = _configuration["Common:CDN"];

            if (!String.IsNullOrEmpty(cdnUrl))
                return Path.Combine(cdnUrl, Path.Combine("resources",registeredFileType.ToString()), fileName);


            return Path.Combine(_configuration["Common:Host"], Path.Combine("resources", registeredFileType.ToString()), fileName);
        }

        private async Task<string> FileRenameAsync(string fileName, string path, bool overwrite = false)
        {
            return await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string oldFileName = Path.GetFileNameWithoutExtension(fileName);
                string regulatedFileName = FileHelper.CharacterRegularity(oldFileName);

                var files = Directory.GetFiles(path, regulatedFileName + "*");
                return FileHelper.CharacterRegularity(DateTime.Now.ToString()) + "-" + regulatedFileName + extension;
            });

        }

        public async Task<FileInformationDTO> UploadAsync(IFormFile formFile, RegisteredFileType registeredFileType)
        {
            string filePath = Path.Combine("resources", registeredFileType.ToString());
            string uploadPath = Path.Combine(_webHostEnvironment.WebRootPath,filePath);

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var newFileName = await FileRenameAsync(formFile.FileName, uploadPath);

            await CopyFileAsync(Path.Combine(uploadPath, newFileName), formFile);

            return new FileInformationDTO(newFileName,Path.Combine(filePath,newFileName),GenerateFileUrl(newFileName,registeredFileType));
        }

        public async Task DeleteAsync(string fileName, RegisteredFileType registeredFileType)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "resources", registeredFileType.ToString());
            File.Delete(Path.Combine(path, fileName));
        }
    }
}
