using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Models
{
    public class Brand
    {
        public Brand()
        {
            Products = new List<Product>();
            ProductParents = new List<ProductParent>();
        }

        public Guid Id { get; set; }

        [Display(Name = "Fornecedor")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public Guid? ProviderId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Name { get; set; }

        [Display(Name = "URL para Compra")]
        [StringLength(500, ErrorMessage = "O campo '{0}' deve ter entre até {1} caracteres")]
        [DataType(DataType.Url, ErrorMessage = "O campo '{0}' deve ser uma URL válida")]
        public string SiteUrl { get; set; }

        public virtual Provider Provider { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<ProductParent> ProductParents { get; set; }
    }
}
