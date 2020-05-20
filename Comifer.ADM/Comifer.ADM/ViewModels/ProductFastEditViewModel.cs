using Comifer.Data.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class ProductFastEditViewModel
    {
        [Display(Name = "Estoque")]
        [Range(0, int.MaxValue, ErrorMessage = "O campo '{0}' deve ter um valor entre {1} e {2}")]
        public int? Supply { get; set; }

        [Display(Name = "Peso")]
        [Range(0, int.MaxValue, ErrorMessage = "O campo '{0}' deve ter um valor entre {1} e {2}")]
        public decimal? Weight { get; set; }

        [Display(Name = "Mudança no Estoque")]
        [Range(int.MinValue, int.MaxValue, ErrorMessage = "O campo '{0}' deve ter um valor entre {1} e {2}")]
        public int? SupplyChange { get; set; }

        [Display(Name = "Novos arquivos")]
        public List<IFormFile> Files { get; set; }

        [Display(Name = "Possui foto?")]
        public bool HasPicture { get; set; }
        [Display(Name = "Marca")]
        public string BrandName { get; set; }
        [Display(Name = "Vista Explodia")]
        public string ProductParentName => ProductParent?.Name;
        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "Código")]
        public string Code { get; set; }

        public Guid Id { get; set; }
        public ProductParent ProductParent { get; set; }
        public string WeightValue { get; set; }
    }
}
