using GSATools.Core;
using GSATools.Core.Models;

namespace GSATools.IndexAnalyser
{
    public class IndexAnalyser
    {
        public GSAConnector _gsaConnector;

        public IndexAnalyser(GSAConnectionParams connectionParams)
        {
            _gsaConnector = new GSAConnector(connectionParams);
        }

        public void FetchAllDocuments()
        {
            _gsaConnector.FetchAllDocuments("/");
        }
    }
}
