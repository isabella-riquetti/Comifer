using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IProductGroupService
    {
        List<ProductGroup> GetAll();
        ProductGroup Get(Guid id);
        NotificationViewModel Edit(ProductGroup productGroup);
        NotificationViewModel Create(ProductGroup productGroup);
    }
}
