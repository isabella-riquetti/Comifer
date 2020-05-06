using Comifer.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class DetailedProductViewModel : Product
    {
        public string BrandName => Brand.Name;
        public string ProductParentName => ProductParent?.Name;
        
        [Display(Name = "Qtd. Compatíveis")]
        public int ProductGroupCount => ProductGroup?.Products.Count ?? 0;

        [Display(Name = "Arquivos atuais")]
        public List<FileInfo> FilesInfo { get; set; }
    }
}
