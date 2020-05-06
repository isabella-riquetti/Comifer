using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class ProductEditViewModel : ProductViewModel
    {
        [Display(Name = "Arquivos atuais")]
        public List<FileInfo> FilesInfo { get; set; }
    }
}
