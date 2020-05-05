using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IFileService
    {
        List<File> GetAllByReferId(Guid referId);
        File Get(Guid id);
        NotificationViewModel Edit(File image);
        NotificationViewModel Create(File image);
        NotificationViewModel UploadFiles(List<IFormFile> files, Guid referId, string tableName);
        NotificationViewModel Remove(Guid id);
    }
}
