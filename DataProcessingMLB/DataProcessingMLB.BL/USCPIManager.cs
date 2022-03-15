using DataProcessingMLB.DAL;
using DataProcessingMLB.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingMLB.BL
{
    public class USCPIManager
    {
        public USCPIService _uSCPIService;

        public USCPIManager()
        {
            _uSCPIService = new USCPIService();
        }

        public List<USCPIModel> GetYear(int year)
        {
            return _uSCPIService.GetYear(year);
        }
    }
}
