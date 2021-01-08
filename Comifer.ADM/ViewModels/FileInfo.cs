using System;

namespace Comifer.ADM.ViewModels
{
    public class FileInfo
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string MIME { get; set; }
        public string Base64File { get; set; }
    }
}
