using System;

namespace GSATools.VersioningUtil.Models
{
    public class ConfigUpdateLogEntry
    {
        public DateTime Time { get; set; }
        public string GSAHostInfo { get; set; }
        public string GSAUpdateKey { get; set; }
        public string GitBranchName { get; set; }
        public string GitRevision { get; set; }
        public string ExceptionMessage { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
}