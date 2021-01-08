using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Models
{
    public class ProductParent
    {
        public ProductParent()
        {
            Products = new List<Product>();
        }

        public Guid Id { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public Guid? BrandId { get; set; }

        [Display(Name = "Categoria")]
        public Guid? CategoryId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Code { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
