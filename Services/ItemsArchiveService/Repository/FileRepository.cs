using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ItemsArchiveService.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly IConfiguration _configuration;

        public FileRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public async  Task UploadFileAsync(IEnumerable<IFormFile> files, string path)
        {

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            foreach (var file in files)
            {
                using (var image = Image.Load(file.OpenReadStream()))
                {
                    await image.SaveAsync(Path.Combine(path, file.FileName));
                }
            }
        }

        public async Task ResizeUploadFileAsync(IFormFile file, string path, int width, int height, string fileExtension = "", string fileName = "")
        {
            if (string.IsNullOrEmpty(fileName))
            {
                fileName = file.FileName;
            }

            if (!string.IsNullOrEmpty(fileExtension))
            {
                fileName = fileName.Replace(Path.GetExtension(fileName), fileExtension);
            }
            
            using (var image = Image.Load(file.OpenReadStream()))
            {
                image.Mutate(x => x.Resize(width, height));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                await image.SaveAsync(Path.Combine(path, fileName));
            }
        }
    }
}