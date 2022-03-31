using DataProcessingMLB.DAL;
using DataProcessingMLB.VM;
using System;
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

        public List<USCPIModel> GetYear(int year, string value)
        {
            List<USCPIModel> list = _uSCPIService.GetYear(year);
            if(value != "application/xml")
            {
                //if(_uSCPIService.ValidateReturnValuesAsJson(list))
                //{
                    return list;
                //}
            }
            if (value == "application/xml")
            {
                return list;
            }
            throw new Exception("Something went wrong!");
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
