using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Demo.BLL.Common.Services.AttachmentService
{
    public interface IAttachmentService
    {
        // Upload , Delete

        public string? Upload(IFormFile file, string folderName);

        public bool Delete(string filepath);


    }
}
