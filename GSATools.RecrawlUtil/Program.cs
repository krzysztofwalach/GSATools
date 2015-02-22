using System;
using GSATools.Core.Models;

namespace GSATools.RecrawlUtil
{
    public class Program
    {
        public static void Main()
        {
            var gsaConnectionParams =
                GSAConnectionParams.DeserializeFromFile(@"d:\doc\!!!Gsa7ConnectionParams.txt");

            string testUrl = "";

            var util = new RecrawlUtil(gsaConnectionParams);
            //util.SumbitRecrawlRequest(testUrl);
            GSADocumentStatus status = util.GetDocumentStatus(testUrl);
            status.WriteToConsole();

            Console.ReadKey();
        }
    }
}
