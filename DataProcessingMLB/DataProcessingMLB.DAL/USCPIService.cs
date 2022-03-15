using DataProcessingMLB.VM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProcessingMLB.DAL
{
    public class USCPIService
    {
        private readonly string path;

        public USCPIService()
        {
            path = @"..\..\Datasets\USCPI\USCPI.json";
        }

        public List<USCPIModel> GetYear(int year)
        {
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                List<USCPIModel> fullUSCPIList = JsonConvert.DeserializeObject<List<USCPIModel>>(json);
                List<USCPIModel> USCPIList = new List<USCPIModel>();
                foreach(USCPIModel obj in fullUSCPIList)
                {
                    if(obj.Yearmon.Year == year)
                    {
                        USCPIList.Add(obj);
                    }
                }
                return USCPIList;
            }

        }
    }
}
