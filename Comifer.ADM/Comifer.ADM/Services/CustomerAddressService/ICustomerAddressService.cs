using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface ICustomerAddressService
    {
        List<CustomerAddress> GetAll();
        CustomerAddress Get(Guid id);
        NotificationViewModel Edit(CustomerAddress customerAddress);
        NotificationViewModel Create(CustomerAddress customerAddress);
    }
}
