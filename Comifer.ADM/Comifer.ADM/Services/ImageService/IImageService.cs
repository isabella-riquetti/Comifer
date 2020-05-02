using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IImageService
    {
        List<Image> GetAllByReferId(Guid referId);
        Image Get(Guid id);
        NotificationViewModel Edit(Image image);
        NotificationViewModel Create(Image image);
    }
}
