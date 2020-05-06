using Comifer.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class DetailedBrandViewModel : Brand
    {
        public string ProviderName => Provider.Name;

        [Display(Name = "Qtd. Vistas Explodidas")]
        public int ProductParentCount => ProductParents.Count;

        [Display(Name = "Qtd. Produtos")]
        public int ProductCount => Products.Count;
    }
}
