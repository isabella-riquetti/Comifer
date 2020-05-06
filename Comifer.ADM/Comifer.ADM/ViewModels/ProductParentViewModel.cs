using Comifer.Data.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Comifer.ADM.ViewModels
{
    public class ProductParentViewModel : ProductParent
    {
        public List<IFormFile> Files { get; set; }
    }
}
