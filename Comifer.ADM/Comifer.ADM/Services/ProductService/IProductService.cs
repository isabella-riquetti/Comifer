using Comifer.ADM.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IProductService
    {
        List<DetailedProductViewModel> GetAll(Guid? productParentId, Guid? brandId);
        DetailedProductViewModel GetDetailed(Guid id);
        NotificationViewModel Create(ProductViewModel product);
        ProductEditViewModel Get(Guid id);
        NotificationViewModel Edit(ProductEditViewModel product);
        List<SelectListItem> GetSelectList(Guid brandId, Guid? id);
    }
}
