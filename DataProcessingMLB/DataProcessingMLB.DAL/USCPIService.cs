using DataProcessingMLB.VM;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
                foreach (USCPIModel obj in fullUSCPIList)
                {
                    if (obj.Yearmon.Year == year)
                    {
                        USCPIList.Add(obj);
                    }
                }
                return USCPIList;
            }
        }



        public void Post(USCPIModel model)
        {
            List<USCPIModel> USCPIList = new List<USCPIModel>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                USCPIList = JsonConvert.DeserializeObject<List<USCPIModel>>(json);
                USCPIList.Add(model);
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(USCPIList));
        }

        public bool Put(USCPIModel model)
        {
            bool objFound = false;
            //DateTime dateTime;
            //DateTime.TryParse(date, out dateTime);
            List<USCPIModel> USCPIList = new List<USCPIModel>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                USCPIList = JsonConvert.DeserializeObject<List<USCPIModel>>(json);

                foreach (USCPIModel obj in USCPIList)
                {
                    if (obj.Yearmon.Equals(model.Yearmon))
                    {
                        obj.CPI = model.CPI;
                        objFound = true;
                    }
                }
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(USCPIList));
            return objFound;
        }

        public bool Delete(string date)
        {
            bool objFound = false;
            DateTime dateTime;
            DateTime.TryParse(date, out dateTime);
            List<USCPIModel> USCPIList = new List<USCPIModel>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                USCPIList = JsonConvert.DeserializeObject<List<USCPIModel>>(json);

                foreach (USCPIModel obj in USCPIList)
                {
                    if (obj.Yearmon.Equals(dateTime))
                    {
                        USCPIList.Remove(obj);
                        objFound = true;
                    }
                }
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(USCPIList));
            return objFound;
        }
    }
}
