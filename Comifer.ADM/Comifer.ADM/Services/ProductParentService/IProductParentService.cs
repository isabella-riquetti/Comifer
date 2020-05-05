using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IProductParentService
    {
        List<DetailedProductParentViewModel> GetAll(Guid? brandId, Guid? categoryId);
        DetailedProductParentViewModel GetDetailed(Guid id);
        ProductParentWithFileViewModel GetDetailedWithFiles(Guid id);
        ProductParent Get(Guid id);
        NotificationViewModel Edit(ProductParent productParent);
        NotificationViewModel Create(ProductParent productParent);
        List<SelectListItem> GetSelectList();
        List<SelectListItem> GetSelectListWithAll();
    }
}
