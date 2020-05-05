using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IBrandService
    {
        List<DetailedBrandViewModel> GetAll(Guid? providerId);
        DetailedBrandViewModel GetDetailed(Guid id);
        Brand Get(Guid id);
        NotificationViewModel Edit(Brand brand);
        NotificationViewModel Create(Brand brand);
        List<SelectListItem> GetSelectList();
        List<SelectListItem> GetSelectListWithAll();
    }
}
