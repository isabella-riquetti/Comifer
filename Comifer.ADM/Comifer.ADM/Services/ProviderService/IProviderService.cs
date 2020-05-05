using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IProviderService
    {
        List<DetailedProviderViewModel> GetAll();
        DetailedProviderViewModel GetDetailed(Guid id);
        Provider Get(Guid id);
        NotificationViewModel Edit(Provider provider);
        NotificationViewModel Create(Provider provider);
        List<SelectListItem> GetSelectList();
        List<SelectListItem> GetSelectListWithAll();
    }
}
