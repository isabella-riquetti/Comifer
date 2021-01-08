using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IProductParentService
    {
        DashboardItemViewModel GetCount();
        List<DetailedProductParentViewModel> GetAll(Guid? brandId, Guid? categoryId);
        DetailedProductParentViewModel GetDetailed(Guid id);
        ProductParentEditViewModel GetWithFiles(Guid id);
        ProductParent Get(Guid id);
        NotificationViewModel Edit(ProductParentEditViewModel product);
        NotificationViewModel Create(ProductParentViewModel product);
        List<SelectListItem> GetSelectList();
        List<SelectListItem> GetSelectListWithAll();
    }
}
