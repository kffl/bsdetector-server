using System.Collections.Generic;
using System.Threading.Tasks;

namespace BSDetector.Analysis.Repos
{
    public interface IRepoSource
    {
        public IEnumerable<IRepoFile> GetFiles();
    }
}