using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ItemsArchiveService.Repository
{
    public interface IFileRepository
    {
          Task UploadFileAsync(IEnumerable<IFormFile> files,string path);

          Task ResizeUploadFileAsync(IFormFile file,string path,int width, int height, string fileExtension = "", string fileName= "");
    }
}