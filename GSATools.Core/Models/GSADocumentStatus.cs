using System;

namespace GSATools.Core.Models
{
    public class GSADocumentStatus
    {
        public string CollectionList { get; set; }
        public string ForwardLinks { get; set; }
        public string BackwardLinks { get; set; }
        public string IsCached { get; set; }
        public string DocumentDate { get; set; }
        public string LastModifiedDate { get; set; }
        public string LatestServingVersionTimestamp { get; set; }
        public string CurrentlyInProcess { get; set; }
        public string ContentSize { get; set; }
        public string ContentType { get; set; }
        public string CrawlFrequency { get; set; }
        public string CrawlHistory { get; set; }

        public void WriteToConsole()
        {
            Console.WriteLine("Collection List: " + CollectionList);
            Console.WriteLine("Forward Links: " + ForwardLinks);
            Console.WriteLine("Backward Links: " + BackwardLinks);
            Console.WriteLine("Is Cached: " + IsCached);
            Console.WriteLine("Document Date: " + DocumentDate);
            Console.WriteLine("Last Modified Date: " + LastModifiedDate);
            Console.WriteLine("Latest Serving Version Timestamp: " + LatestServingVersionTimestamp);
            Console.WriteLine("Currently In Process: " + CurrentlyInProcess);
            Console.WriteLine("Content Size: " + ContentSize);
            Console.WriteLine("Content Type: " + ContentType);
            Console.WriteLine("Crawl Frequency: " + CrawlFrequency);
            Console.WriteLine("Crawl History:\n" + CrawlHistory);
        }
    }
}