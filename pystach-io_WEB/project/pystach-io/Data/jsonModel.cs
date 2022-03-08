using System.Collections.Generic;

namespace pystach_io.Models
{
    public class jsonModel
    {
        //The input as a list
        public List<string> inputAddresses { get; set; }

        //The output as a list
        public List<List<string>> sortedAddresses { get; set; }

        //The Google Maps optimized itinerary URL
        public string gmapURL { get; set; }

        //The time take by the script
        public string time { get; set; }
    }

    public class jsonModelPost
    {
        //The input as a list
        public List<string> inputAddresses { get; set; }

        public string ApiKey { get; set; }
    }

    public class apiKeyCreatorPost
    {
        public string Name { get; set; }
    }

    public class apiKeyDeletePost
    {
        public string ApiKey { get; set; }
    }

    public class apiCounter
    {
        public Dictionary<string, int> ApiKeyCounter { get; set; }
    }

    public class apiKeyList
    {
        public List<string> apiKeyLs { get; set; }
    }
}
