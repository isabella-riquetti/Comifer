using System;
namespace Comifer.ADM.Services
{
    public interface IPromotionService
    {
        bool Create(Guid productId, decimal percentage, DateTime? expiresOn = null);
    }
}
