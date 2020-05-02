using Comifer.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class DetailedBrandViewModel : Brand
    {
        [Display(Name = "Nome do Provedor")]
        public string ProviderName => Provider.Name;

        [Display(Name = "Qtd. de Máquinas da Categoria")]
        public int ProductParentCount => ProductParents.Count;

        [Display(Name = "Qtd. de Peças da Categoria")]
        public int ProductCount => Products.Count;
    }
}
