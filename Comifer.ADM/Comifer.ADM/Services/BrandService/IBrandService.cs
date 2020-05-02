using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IBrandService
    {
        List<DetailedBrandViewModel> GetAll();
        List<DetailedBrandViewModel> GetByProviderId(Guid providerId);
        DetailedBrandViewModel GetDetailed(Guid id);
        Brand Get(Guid id);
        NotificationViewModel Edit(Brand brand);
        NotificationViewModel Create(Brand brand);
    }
}
