using DataProcessingMLB.DAL;
using DataProcessingMLB.VM;
using System.Collections.Generic;

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

        public void Post(USCPIModel model)
        {
            _uSCPIService.Post(model);
        }

        public bool Put(USCPIModel model)
        {
            return _uSCPIService.Put(model);
        }

        public bool Delete(string date)
        {
            return _uSCPIService.Delete(date);
        }
    }
}
