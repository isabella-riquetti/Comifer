using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product Get(Guid id);
        NotificationViewModel Edit(Product product);
        NotificationViewModel Create(Product product);
    }
}
