using DataProcessingMLB.VM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
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

        public bool ValidateReturnValuesAsJson(List<USCPIModel> list)
        {
            JSchema schema = JSchema.Parse(@"
                {
                  'title': 'inflation',
                  'type': 'object',
                  'items': {
                    'title': 'inflation',
                    'type': 'object',
                    'required': [
                      'year',
                      'cpi'
                    ]
                  },
                  'properties': {
                    'beercost':{
                      'beercostList': [
                        {
                          'year': {
                            '$id': '#root/items/year',
                            'title': 'year',
                            'type': 'integer',
                            'default': 0
                          },
                          'team': {
                            '$id': '#root/items/team',
                            'title': 'team',
                            'type': 'string',
                            'default': '',
                            'pattern': '^.*$'
                          },
                          'nickName': {
                            '$id': '#root/items/nickName',
                            'title': 'nickname',
                            'type': 'string',
                            'default': '',
                            'pattern': '^.*$'
                          },
                          'city': {
                            '$id': '#root/items/city',
                            'title': 'city',
                            'type': 'string',
                            'default': '',
                            'pattern': '^.*$'
                          },
                          'price': {
                            '$id': '#root/items/price',
                            'title': 'price',
                            'type': 'number',
                            'default': 0
                          },
                          'size': {
                            '$id': '#root/items/size',
                            'title': 'size',
                            'type': 'number',
                            'default': 0
                          },
                          'pricePerOunce': {
                            '$id': '#root/items/pricePerOunce',
                            'title': 'price_per_Ounce',
                            'type': 'number',
                            'default': 0.0
                          }
                        }
                      ]
                    }
                  }
                }
            ");

            foreach (USCPIModel cpi in list)
            {
                string temp = JsonConvert.SerializeObject(cpi);
                JObject jObject = JObject.Parse(temp);
                bool valid = jObject.IsValid(schema);

                if (!valid)
                {
                    return false;
                }

            }
            return true;
        }
    }
}
