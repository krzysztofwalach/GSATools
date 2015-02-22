using System;
using System.IO;
using GSATools.Core.Models;
using GSATools.VersioningUtil.Models;

namespace GSATools.VersioningUtil
{
    public class SourceConnector
    {
        private readonly DirectoryInfo _repoPath;

        public SourceConnector(DirectoryInfo localRepoPath)
        {
            if (!localRepoPath.Exists)
            {
                throw new Exception(string.Format("{0} does not exist!", localRepoPath.FullName));
            }
            _repoPath = localRepoPath;
        }

        public GSACrawlUrlsConfig GetGSACrawlUrlsConfig(out GitInfo gitInfo)
        {
            string basePath = Path.Combine(_repoPath.FullName, "config", "crawlURLs");
            string startUrlsPath =  Path.Combine(basePath, "startURLs.txt");
            string startUrlsContent = File.ReadAllText(startUrlsPath);

            string followUrlsPath = Path.Combine(basePath, "followURLs.txt");
            string followUrlsContent = File.ReadAllText(followUrlsPath);

            string doNotCrawlUrlsPath = Path.Combine(basePath, "doNotCrawlURLs.txt");
            string doNotCrawlUrlsContent = File.ReadAllText(doNotCrawlUrlsPath);

            gitInfo = FetchGitInfoForWorkingCopy();

            return new GSACrawlUrlsConfig
            {
                StartUrls = startUrlsContent,
                FollowUrls = followUrlsContent,
                DoNotCrawlUrls = doNotCrawlUrlsContent
            };
        }

        private GitInfo FetchGitInfoForWorkingCopy()
        {
            return new GitInfo
            {
                BranchName = "tbd",
                Revision = "tbd"
            };
        }
    }
}
