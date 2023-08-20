using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Core.DTOs.Media
{
    public class FileInformationDTO
    {
        public FileInformationDTO(string fileName, string url)
        {
            FileName = fileName;
            Url = url;
        }
        public string FileName { get; set; }
        public string Url { get; set; }

    }
}
