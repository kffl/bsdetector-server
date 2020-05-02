using System.Reflection.Emit;
using System.Collections.Generic;
using System.Threading.Tasks;
using BSDetector.Resources;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BSDetector.Analysis.Repos.Uploaded
{
    public class UploadedFile : IRepoFile
    {
        public string fileName { get; set; }

        public string fileContent { get; set; }
    }
}