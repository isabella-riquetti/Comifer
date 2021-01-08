using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Models
{
    public class CustomerAddress
    {
        public Guid Id { get; set; }

        [Display(Name = "Colaborador")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public Guid? CustomerId { get; set; }

        [Display(Name = "Logradouro")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string AddressName { get; set; }

        [Display(Name = "Número")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public int Number { get; set; }

        [Display(Name = "Complemento")]
        [StringLength(255, ErrorMessage = "O campo '{0}' deve ter entre até {1} caracteres")]
        public string Complement { get; set; }

        [Display(Name = "Bairro")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Neighborhood { get; set; }

        [Display(Name = "Cidade")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string City { get; set; }

        [Display(Name = "Estado")]
        [StringLength(255, ErrorMessage = "O campo '{0}' deve ter entre até {1} caracteres")]
        public string State { get; set; }

        [Display(Name = "País")]
        [StringLength(255, ErrorMessage = "O campo '{0}' deve ter entre até {1} caracteres")]
        public string Country { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
