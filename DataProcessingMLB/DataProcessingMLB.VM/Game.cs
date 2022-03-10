using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingMLB.VM
{
    public class Game
    {
        public string team { get; set; }
        public string home_away { get; set; }
        public string opp { get; set; }
        public string win_or_lose { get; set; }
        public int rank { get; set; }
        public int year { get; set; }
        public DateTime date { get; set; }
        public int g { get; set; }
    }
}
