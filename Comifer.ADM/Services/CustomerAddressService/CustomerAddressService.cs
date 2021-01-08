using Comifer.ADM.ViewModels;
using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Comifer.ADM.Services
{
    public class CustomerAddressService : ICustomerAddressService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerAddressService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<CustomerAddress> GetAll()
        {
            var customerAddresses = _unitOfWork.CustomerAddress.Get().ToList();
            return customerAddresses;
        }

        public CustomerAddress Get(Guid id)
        {
            var customerAddress = _unitOfWork.CustomerAddress.FirstOrDefault(b => b.Id == id);
            return customerAddress;
        }

        public NotificationViewModel Edit(CustomerAddress customerAddress)
        {
            _unitOfWork.CustomerAddress.Edit(customerAddress);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Endereço do cliente editado com sucesso."
            };
        }

        public NotificationViewModel Create(CustomerAddress customerAddress)
        {
            customerAddress.Id = Guid.NewGuid();
            _unitOfWork.CustomerAddress.Add(customerAddress);
            _unitOfWork.Commit();
            return new NotificationViewModel()
            {
                Status = true,
                Title = "Sucesso!",
                Message = "Endereço do cliente criado com sucesso."
            };
        }
    }
}
