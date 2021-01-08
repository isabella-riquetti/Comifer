using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Models
{
    public class Product
    {
        public Product()
        {
            Promotions = new List<Promotion>();
        }

        public Guid Id { get; set; }

        [Display(Name = "Máquina")]
        public Guid? ProductParentId { get; set; }

        [Display(Name = "Compatibilidade")]
        public Guid? ProductGroupId { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public Guid BrandId { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Código")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Code { get; set; }

        [Display(Name = "Estoque")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "O campo '{0}' deve ter um valor entre {1} e {2}")]
        public int Supply { get; set; }

        [Display(Name = "Custo")]
        [Range(0, Int32.MaxValue, ErrorMessage = "O campo '{0}' deve ter um valor entre {1} e {2}")]
        public decimal? Cost { get; set; }

        [Display(Name = "Preço")]
        [Range(0, Int32.MaxValue, ErrorMessage = "O campo '{0}' deve ter um valor entre {1} e {2}")]
        public decimal? Price { get; set; }

        [Display(Name = "Peso")]
        [Range(0, Int32.MaxValue, ErrorMessage = "O campo '{0}' deve ter um valor entre {1} e {2}")]
        public decimal? Weight { get; set; }

        [Display(Name = "Produto atual é o principal do grupo de compatibilidade")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public bool IsMainInGroup { get; set; }

        public virtual ProductParent ProductParent { get; set; }
        public virtual Brand Brand { get; set; }

        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
