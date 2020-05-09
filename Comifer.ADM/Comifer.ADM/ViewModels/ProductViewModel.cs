using Comifer.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class ProductViewModel : Product
    {
        public ProductViewModel()
        {
            Supply = 0;
        }

        [Display(Name = "Produto Compatível")]
        public Guid? ProductInGroupId { get; set; }

        [Display(Name = "Marca do Produto Compatível")]
        public Guid? BrandOfMainProductInGroupId { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
