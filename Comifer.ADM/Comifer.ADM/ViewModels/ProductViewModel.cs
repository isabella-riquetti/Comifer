using Comifer.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Comifer.ADM.ViewModels
{
    public class ProductViewModel : Product
    {
        public ProductViewModel()
        {
            Supply = 0;
        }

        public List<IFormFile> Files { get; set; }
    }
}
