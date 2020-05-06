using Comifer.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class DetailedCategoryViewModel : Category
    {
        [Display(Name = "Qtd. de Vistas Explodidas")]
        public int ProductParentCount => ProductParents.Count;
    }
}
