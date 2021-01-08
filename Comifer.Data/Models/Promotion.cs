using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Models
{
    public class Promotion
    {
        public Guid Id { get; set; }

        [Display(Name = "Produto")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public Guid ProductId { get; set; }

        [Display(Name = "Data de Excpiração")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ExpiresOn { get; set; }

        [Display(Name = "Valor")]
        public decimal? Value { get; set; }

        [Display(Name = "Porcentagem")]
        public decimal? Percentage { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public bool Status { get; set; }

        public virtual Product Product { get; set; }
    }
}
