using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public DashboardItemViewModel GetCountAndGrowth()
        {
            var customersRegisteredOn = _unitOfWork.Customer.Get().Select(c => c.RegisteredOn);
            var lastMonth = DateTime.Now.AddMonths(-1);
            var sinceLastMonth = customersRegisteredOn.Where(c => c < lastMonth).Count();
            var fromAllTime = customersRegisteredOn.Count();
            if(sinceLastMonth == 0)
            {
                return new DashboardItemViewModel()
                {
                    CurrentValue = fromAllTime * 1.0m,
                    Growth = 0
                };
            }
            var growth = (fromAllTime - sinceLastMonth) / sinceLastMonth * 100.0m;

            return new DashboardItemViewModel()
            {
                CurrentValue = fromAllTime * 1.0m,
                Growth = growth
            };
        }

        public List<Customer> GetAll()
        {
            var customers = _unitOfWork.Customer.Get().ToList();
            return customers;
        }

        public Customer Get(Guid id)
        {
            var customer = _unitOfWork.Customer.FirstOrDefault(b => b.Id == id);
            return customer;
        }

        public NotificationViewModel Edit(Customer customer)
        {
            _unitOfWork.Customer.Edit(customer);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Cliente editado com sucesso."
            };
        }

        public NotificationViewModel Create(Customer customer)
        {
            customer.Id = Guid.NewGuid();
            _unitOfWork.Customer.Add(customer);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Cliente criado com sucesso."
            };
        }
    }
}
