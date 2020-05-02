using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IProductParentService
    {
        List<ProductParent> GetAll();
        ProductParent Get(Guid id);
        NotificationViewModel Edit(ProductParent productParent);
        NotificationViewModel Create(ProductParent productParent);
    }
}
