using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface ICategoryService
    {
        List<DetailedCategoryViewModel> GetAll();
        DetailedCategoryViewModel GetDetailed(Guid id);
        Category Get(Guid id);
        NotificationViewModel Edit(Category category);
        NotificationViewModel Create(Category category);
        //NotificationViewModel Delete(Guid id);
        List<SelectListItem> GetSelectList();
        List<SelectListItem> GetSelectListWithAll();
    }
}
