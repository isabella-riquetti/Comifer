using Comifer.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class DetailedProductParentViewModel : ProductParent
    {
        public string CategoryName => Category?.Name;
        public string BrandName => Brand.Name;

        [Display(Name = "Qtd. de Produtos")]
        public int ProductCount => Products.Count;

        [Display(Name = "Arquivos atuais")]
        public List<FileInfo> FilesInfo { get; set; }
    }
}
