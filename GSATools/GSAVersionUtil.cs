using System;
using System.IO;
using GSATools.Core;
using GSATools.Core.Models;
using GSATools.VersioningUtil.Models;

namespace GSATools.VersioningUtil
{
    public class GSAVersionUtil
    {
        private readonly GSAConnectionParams _gsaConnectionParams;
        private readonly GSAConnector _gsaConnector;
        private readonly SourceConnector _sourceConnector;
        private readonly IEventLogger _eventLogger;

        private const string UpdateKeyConfigCrawlUrLs = "UpdateKey_ConfigCrawlURLs";

        public GSAVersionUtil(GSAConnectionParams gsaConnectionParams, DirectoryInfo localGitRepoDir, string logDBConnectionString)
        {
            _gsaConnectionParams = gsaConnectionParams;
            _gsaConnector = new GSAConnector(gsaConnectionParams);
            _sourceConnector = new SourceConnector(localGitRepoDir);
            _eventLogger = new SqlEventLogger(logDBConnectionString);
        }

        public void UpdateCrawlUrlsFromLocalGitWorkingCopy()
        {
            GitInfo gitInfo;
            GSACrawlUrlsConfig gsaCrawlConfig = _sourceConnector.GetGSACrawlUrlsConfig(out gitInfo);
            var logEntry = new ConfigUpdateLogEntry
            {
                GSAHostInfo = _gsaConnectionParams.GetFullHostInfo(),
                GitBranchName = gitInfo.BranchName,
                GitRevision = gitInfo.Revision,
                GSAUpdateKey = UpdateKeyConfigCrawlUrLs
            };

            try
            {
                _gsaConnector.ReplaceCrawlUrlsConfig(gsaCrawlConfig);
                logEntry.Time = DateTime.Now;
                _eventLogger.LogConfigUpdate(logEntry);
            }
            catch (Exception ex)
            {
                logEntry.ExceptionMessage = ex.Message;
                logEntry.ExceptionStackTrace = ex.StackTrace;
                _eventLogger.LogConfigUpdate(logEntry);
            }
        }
    }
}
