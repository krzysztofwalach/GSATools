using System;
using GSATools.Core;
using GSATools.Core.Models;

namespace GSATools.RecrawlUtil
{
    public class RecrawlUtil
    {
        private readonly GSAConnector _gsaConnector;

        public RecrawlUtil(GSAConnectionParams connectionParams)
        {
            _gsaConnector = new GSAConnector(connectionParams);
        }

        public GSADocumentStatus GetDocumentStatus(string url)
        {
            try
            {
                GSADocumentStatus document = _gsaConnector.GetDocumentStatus(url);
                return document;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        
        public void SumbitRecrawlRequest(string url)
        {
            try
            {
                _gsaConnector.RecrawlUrl(url);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
