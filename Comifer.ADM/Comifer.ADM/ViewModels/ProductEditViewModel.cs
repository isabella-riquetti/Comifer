using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Comifer.ADM.ViewModels
{
    public class ProductEditViewModel : ProductViewModel
    {
        [Display(Name = "Arquivos atuais")]
        public List<FileInfo> FilesInfo { get; set; }

        [Display(Name = "Grupo de Compatibilidade")]
        public List<BasicProdutInfo> Compatibility { get; set; }
    }

    public class BasicProdutInfo
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsMainInGroup { get; internal set; }
    }
}
