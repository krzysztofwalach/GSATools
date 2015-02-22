using System;
using System.Collections.Generic;
using Google.GData.Gsa;
using GSATools.Core.Models;

namespace GSATools.Core
{
    public class GSAConnector
    {
        private readonly GsaService _gsaService;

        public GSAConnector(GSAConnectionParams gsaConnectionParams)
        {
            _gsaService = new GsaService(
                gsaConnectionParams.Protocol,
                gsaConnectionParams.Hostname,
                gsaConnectionParams.Port,
                gsaConnectionParams.Username,
                gsaConnectionParams.Password);
        }

        public GSACrawlUrlsConfig GetCrawlUrlsConfig()
        {
            GsaEntry entry = _gsaService.GetEntry("config", "crawlURLs");
            var content = entry.GetAllGsaContents();
            return new GSACrawlUrlsConfig
            {
                StartUrls = content["startURLs"],
                FollowUrls = content["followURLs"],
                DoNotCrawlUrls = content["doNotCrawlURLs"]
            };
        }

        public void ReplaceCrawlUrlsConfig(GSACrawlUrlsConfig newConfig)
        {
            var replaceEntry = new GsaEntry();
            replaceEntry.AddGsaContent("updateMethod", "replace");
            replaceEntry.AddGsaContent("startURLs", newConfig.StartUrls);
            replaceEntry.AddGsaContent("followURLs", newConfig.FollowUrls);
            replaceEntry.AddGsaContent("doNotCrawlURLs", newConfig.DoNotCrawlUrls);

            _gsaService.UpdateEntry("config", "crawlURLs", replaceEntry);
        }

        //private void DoTestUpdate()
        //{
        //    var crawlConfig = _gsaConnector.GetCrawlURLsConfig();
        //    crawlConfig.StartUrls += "\nhttp://newteststarturl.com/";
        //    crawlConfig.FollowUrls += "\nhttp://newteststarturl.com/";
        //    crawlConfig.DoNotCrawlUrls += "\nhttp://newtestdonotcrawlurl.com/";
        //    _gsaConnector.ReplaceCrawlURLsConfig(crawlConfig);
        //}

        public void RecrawlUrl(string url)
        {
            var updateEntry = new GsaEntry();
            updateEntry.AddGsaContent("recrawlURLs", url);
            _gsaService.UpdateEntry("command", "recrawlNow", updateEntry);
        }

        public GSADocumentStatus GetDocumentStatus(string url)
        {
            GsaEntry entry = _gsaService.GetEntry("diagnostics", url);

            var status = new GSADocumentStatus
            {
                CollectionList = entry.GetGsaContent("collectionList"),
                ForwardLinks = entry.GetGsaContent("forwardLinks"),
                BackwardLinks = entry.GetGsaContent("backwardLinks"),
                IsCached = entry.GetGsaContent("isCached"),
                DocumentDate = entry.GetGsaContent("date"),
                LastModifiedDate = entry.GetGsaContent("lastModifiedDate"),
                LatestServingVersionTimestamp = entry.GetGsaContent("latestOnDisk"),
                CurrentlyInProcess = entry.GetGsaContent("currentlyInflight"),
                ContentSize = entry.GetGsaContent("contentSize"),
                ContentType = entry.GetGsaContent("contentType"),
                CrawlFrequency = entry.GetGsaContent("CrawlFrequency"),
                CrawlHistory = entry.GetGsaContent("crawlHistory")
            };
            return status;
        }

        public void FetchAllDocuments(string parentUrl)
        {
            var queries = new Dictionary<string, string>();
            queries.Add("collectionName", "default_collection");
            queries.Add("uriAt", parentUrl);
            GsaFeed myFeed = _gsaService.QueryFeed("diagnostics", queries);

            foreach (GsaEntry entry in myFeed.Entries)
            {
                Console.WriteLine(entry.GetGsaContent("entryID"));
                if (entry.GetGsaContent("entryID").Equals("description"))
                {
                    Console.WriteLine("Number of Pages: " + entry.GetGsaContent("numPages"));
                    Console.WriteLine("URI At: " + entry.GetGsaContent("uriAt"));
                }
                else if (entry.GetGsaContent("type").Equals("DirectoryContentData") ||
                  entry.GetGsaContent("type").Equals("HostContentData"))
                {
                    Console.WriteLine("Type: " + entry.GetGsaContent("type"));
                    Console.WriteLine("Number of Crawled URLs: " +
                      entry.GetGsaContent("numCrawledURLs"));
                    Console.WriteLine("Number of Retrieval Errors: " +
                      entry.GetGsaContent("numRetrievalErrors"));
                    Console.WriteLine("Number of Excluded URLs: " +
                      entry.GetGsaContent("numExcludedURLs"));
                }
                else if (entry.GetGsaContent("type").Equals("FileContentData"))
                {
                    Console.WriteLine("Type: " + entry.GetGsaContent("type"));
                    Console.WriteLine("Time Stamp: " + entry.GetGsaContent("timeStamp"));
                    Console.WriteLine("Document State: " + entry.GetGsaContent("docState"));
                    Console.WriteLine("Is Cookie Server Error: " +
                      entry.GetGsaContent("isCookieServerError"));
                }
                Console.ReadKey();
                var results = entry.GetAllGsaContents();
            }
        }
    }
}
