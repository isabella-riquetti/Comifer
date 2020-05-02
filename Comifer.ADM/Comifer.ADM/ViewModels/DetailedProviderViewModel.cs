using Comifer.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class DetailedProviderViewModel : Provider
    {
        [Display(Name = "Qtd. de Marcas do Provedor")]
        public int BrandsCount => this.Brands.Count;
    }
}
