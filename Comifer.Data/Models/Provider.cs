using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Models
{
    public class Provider
    {
        public Provider()
        {
            Brands = new List<Brand>();
        }

        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Name { get; set; }

        [Display(Name = "Tempo para Entrega")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "O campo '{0}' deve ter um valor entre {1} e {2}")]
        public int DeliveryTime { get; set; }

        public virtual ICollection<Brand> Brands { get; set; }
    }
}
