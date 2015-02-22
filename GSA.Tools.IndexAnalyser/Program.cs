using GSATools.Core.Models;

namespace GSATools.IndexAnalyser
{
    class Program
    {
        static void Main()
        {
            var gsaConnectionParams =
                GSAConnectionParams.DeserializeFromFile(@"d:\doc\!!!Gsa7ConnectionParams.txt");

            var indexAnalyser = new IndexAnalyser(gsaConnectionParams);
            indexAnalyser.FetchAllDocuments();
        }
    }
}
