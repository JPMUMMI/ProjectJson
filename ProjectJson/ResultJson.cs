using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJson
{
    internal class ProcJson
    {
        public string requestID { get; set; }
        public string season { get; set; }
        public string packingCode { get; set; }
        public string packingName { get; set; }
        public string growerCode { get; set; }
        public string growerName { get; set; }
        public string csg { get; set; }
        public string ranchCode { get; set; }
        public string ranchName { get; set; }
        public string commodityCode { get; set; }
        public string commodityName { get; set; }
        public string varietyCode { get; set; }
        public string varietyName { get; set; }
        public string packageCode { get; set; }
        public string sizeCode { get; set; }
        public string colorCode { get; set; }
        public string processGuideNumber { get; set; }
        public string turn { get; set; }
        public string line { get; set; }
        public string harvestDate { get; set; }
        public string packedDate { get; set; }

        public static List<ProcJson> ProcCaja = new List<ProcJson>();
    }

    internal class IdInspecJson
    {
        public string id { get; set; }
        public int registrationId { get; set; }

        public static List<IdInspecJson> ProcCaja = new List<IdInspecJson>();
    }
}
