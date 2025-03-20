using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace Demo.BLL.Common.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        //Allowed Extensions [.png , .jpg , .jpeg]

        public List<string> allowedExtensions=[".png", ".jpg",".jpeg"];

        //Max size //2MB

        public const int maxAllowedSize = 2_097_152;
        
        public string? Upload(IFormFile file, string folderName)
        {
            //1] Validate for extension [".png",.jpg",".jpeg"]
            var extension= Path.GetExtension(file.FileName); //Mariam.png
            if (!allowedExtensions.Contains(extension)) { 
                  return null;
            }

            //2] Validate for Max size [ 2_097_152]
            if (file.Length>maxAllowedSize)
                return null;

            // 3] Get located folder path
                                       //"D:\Backend Assignments\MVC\Demo.PL\wwwroot\files\Images\"
            var folderpath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot//files",folderName);

            // 4] set unique file Name
            //12545854.png
            var fileName = $"{Guid.NewGuid()}{extension}";

            // 5] Get File path [FolderPath + File Name]
            var FilePath= Path.Combine(folderpath,fileName);

            //6] Save file as a stream [Data per time]

            using var filestream = new FileStream(FilePath, FileMode.Create);

            // 7] Copy file to the stream

            file.CopyTo(filestream);

            // 8]   Return file Name
            return fileName;

        }


        public bool Delete(string filepath)
        {
            if (File.Exists(filepath)) {
                File.Delete(filepath);
                return true; }
            return false;
        }
       

        

       
    }
}
