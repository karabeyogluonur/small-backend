using Microsoft.AspNetCore.Http;
using SM.Core.Common.Enums.Media;
using SM.Core.DTOs.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.Interfaces.Services.Media
{
    public interface IFileService
    {
        Task<FileInformationDTO> UploadAsync(IFormFile formFile, RegisteredFileType registeredFileType);
        Task DeleteAsync(string fileName, RegisteredFileType registeredFileType);
        Task DeleteAsync(string fullPath);
    }
}
