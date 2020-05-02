using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using System;
using System.Collections.Generic;

namespace Comifer.ADM.Services
{
    public interface ICustomerService
    {
        List<Customer> GetAll();
        Customer Get(Guid id);
        NotificationViewModel Edit(Customer customer);
        NotificationViewModel Create(Customer customer);
    }
}
