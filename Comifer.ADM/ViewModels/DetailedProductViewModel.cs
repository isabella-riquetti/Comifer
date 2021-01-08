using Comifer.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class DetailedProductViewModel : Product
    {
        public string BrandName => Brand.Name;
        public string ProductParentName => ProductParent?.Name;

        public string ProductMainInGroupCode { get; set; }

        [Display(Name = "Grupo de Compatibilidade")]
        public List<BasicProdutInfo> Compatibility { get; set; }

        [Display(Name = "Arquivos atuais")]
        public List<FileInfo> FilesInfo { get; set; }

        [Display(Name = "Promoções")]
        public List<BasicPromotionInfo> PromotionInfos { get; set; }
    }
}
