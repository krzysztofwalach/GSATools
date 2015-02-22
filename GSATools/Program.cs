using System.IO;
using GSATools.Core.Models;

namespace GSATools.VersioningUtil
{
    internal class Program
    {
        private static void Main()
        {
            var gsaConnectionParams = 
                GSAConnectionParams.DeserializeFromFile(@"d:\doc\!!!Gsa7ConnectionParams.txt");
            
            var localGitDir =
                new DirectoryInfo(@"e:\priv_workspace\GSAVersioningTestRepo\");

            const string sqlConnectionString =
                "Server=localhost;Database=GSAVersioningUtil;Trusted_Connection=True;";

            var gsaVersionUtil = new GSAVersionUtil(gsaConnectionParams, localGitDir, sqlConnectionString);
            gsaVersionUtil.UpdateCrawlUrlsFromLocalGitWorkingCopy();
        }
    }
}
