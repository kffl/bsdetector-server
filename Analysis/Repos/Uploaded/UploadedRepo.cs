using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BSDetector.Analysis.Repos.Uploaded
{
    public class UploadedRepo : IRepoSource
    {
        public List<UploadedFile> files;

        public UploadedRepo()
        {
            files = new List<UploadedFile>();
        }

        public async Task ReadUploadedFiles(List<IFormFile> formFiles)
        {
            foreach (var formFile in formFiles)
            {
                using (var stream = formFile.OpenReadStream())
                using (var reader = new StreamReader(stream))
                {
                    var fileContent = await reader.ReadToEndAsync();
                    files.Add(new UploadedFile() { fileName = formFile.FileName, fileContent = fileContent });
                }
            }
        }

        public IEnumerable<IRepoFile> GetFiles()
        {
            return files;
        }
    }
}