using Comifer.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class DetailedBrandViewModel : Brand
    {
        public string ProviderName => Provider.Name;

        [Display(Name = "Qtd. de Máquinas")]
        public int ProductParentCount => ProductParents.Count;

        [Display(Name = "Qtd. de Peças")]
        public int ProductCount => Products.Count;
    }
}
