using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJson
{
    internal class SendJson
    {
        public string id { get; set; }
        public string parentId { get; set; }
        public int companyId { get; set; }
        public string name { get; set; }
        public int creationTime { get; set; }
        public DateTime creationTimeISO { get; set; }
        public string externalProduceId { get; set; }
        public int varietyId { get; set; }
        public string externalVarietyId { get; set; }
        public string varietyName { get; set; }
        public int processId { get; set; }
        public string processName { get; set; }
        public int standardId { get; set; }
        public string standardName { get; set; }
        public string grade { get; set; }
        public string username { get; set; }
        public List<PropJson> properties { get; set; }
        public List<AtrJson> attributes { get; set; }
        public List<DefJson> defects { get; set; }

        public SendJson()
        {
            properties = new List<PropJson>();
            attributes = new List<AtrJson>();
            defects = new List<DefJson>();
        }
    }

    internal class PropJson
    {
        public string id { get; set; }
        public string externalId { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string jsonpropdefault { get; set; }
        public string value { get; set; }
    }

    internal class AtrJson
    {
        public int id { get; set; }
        public string externalId { get; set; }
        public string name { get; set; }
        public int value { get; set; }
        public string valueName { get; set; }
        public int count { get; set; }
        public string grade { get; set; }
        public List<ValuesAtr> values { get; set; }

        public AtrJson()
        {
            values = new List<ValuesAtr>();
        }
    }

    internal class ValuesAtr
    {
        public string valueName { get; set; }
        public int value { get; set; }
        public int count { get; set; }
    }

    internal class DefJson
    {
        public int id { get; set; }
        public string externalId { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public int percent { get; set; }
        public string grade { get; set; }
    }
}
