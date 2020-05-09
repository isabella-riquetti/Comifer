using Comifer.Data.Models;
using Comifer.Data.UnitOfWork;
using System;

namespace Comifer.ADM.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork _unitOfWork;
        public PromotionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Create(Guid productId, decimal percentage, DateTime? expiresOn = null)
        {
            var promotion = new Promotion()
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                ExpiresOn = expiresOn,
                Percentage = percentage,
                Status = true
            };
            _unitOfWork.Promotion.Add(promotion);
            _unitOfWork.Commit();
            return true;
        }
    }
}
