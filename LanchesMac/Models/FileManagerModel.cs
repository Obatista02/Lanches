﻿namespace LanchesMac.Models
{
    public class FileManagerModel
    {
        public FileInfo[]? Files { get; set; }
        public IFormFile? IFormFIle { get; set; }
        public List<IFormFile>? IFormFiles { get; set; }
        public string? PathImagesProduto { get; set; }
    }
}
