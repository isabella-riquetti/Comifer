using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comifer.Data.Models
{
    public class File
    {
        public Guid Id { get; set; }

        [Display(Name = "Referência")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        public Guid? ReferId { get; set; }

        [Display(Name = "Nome da Tabela")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string TableName { get; set; }

        [Display(Name = "Tipo de Imagem")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatório.")]
        [StringLength(255, MinimumLength = 2, ErrorMessage = "O campo '{0}' deve ter entre {1} e {2} caracteres")]
        public string Type { get; set; }

        [Display(Name = "Prioridade")]
        [Required(ErrorMessage = "O campo '{0}' é obrigatória.")]
        [Range(0, Int32.MaxValue, ErrorMessage = "O campo '{0}' deve ter um valor entre {1} e {2}")]
        public int Priority { get; set; }

        [Display(Name = "Logradouro")]
        [DataType(DataType.Upload)]
        public byte[] FileBytes { get; set; }

        public string FileName { get; set; }
        public string MIME { get; set; }
    }
}
