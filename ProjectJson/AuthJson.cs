using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectJson
{
    internal class AuthJson
    {
        public string clientId { get; set; }
        public string clientSecret { get; set; }

        public static List<AuthJson> ClientD = new List<AuthJson>();
    }

    internal class TokenClass
    {
        public string token { get; set; }

        public static List<TokenClass> TokenC = new List<TokenClass>();
    }
}
