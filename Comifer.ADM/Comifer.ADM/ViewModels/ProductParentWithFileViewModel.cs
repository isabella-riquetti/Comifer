using System;
using System.Collections.Generic;

namespace Comifer.ADM.ViewModels
{
    public class ProductParentWithFileViewModel : DetailedProductParentViewModel
    {
        public ProductParentWithFileViewModel()
        {
            Files = new List<FileInfo>();
        }

        public List<FileInfo> Files { get; set; }
    }

    public class FileInfo
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string MIME { get; set; }
        public string Base64File { get; set; }
    }
}
