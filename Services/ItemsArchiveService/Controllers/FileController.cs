using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using ItemsArchiveService.Authorization;
using ItemsArchiveService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace ItemsArchiveService.Controllers
{
    [ApiController]
    [Route("v1/api/[controller]/[action]")]
    public class FileController : ControllerBase
    {
        private readonly IFileRepository _fileRepository;
        private readonly string _wwwPath;

        public FileController(IFileRepository fileRepository, IConfiguration configuration)
        {
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
             _wwwPath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")).Root;
        }
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(IEnumerable<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            var paths = new List<string>();
            var time = DateTime.Now.ToString().Replace('-','_').Replace(':','_');
            var path = Path.Combine(_wwwPath,"images", time);
            await _fileRepository.UploadFileAsync(files, path);

            foreach (var image in files)
            {
                paths.Add(Path.Combine(time, image.FileName));
            }
            return Ok(paths);
        }
    }
}