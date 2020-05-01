using Comifer.Data.UnitOfWork;
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

        public int GetCustomersCount()
        {
            return _unitOfWork.Customer.Get().Count();
        }
    }
}
