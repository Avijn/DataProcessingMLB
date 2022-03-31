using DataProcessingMLB.VM;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.IO;

namespace DataProcessingMLB.DAL
{
    public class MLBBeerService
    {
        private readonly string path;

        public MLBBeerService()
        {
            path = @"..\..\Datasets\MLBBeerPrices\MLBBeerPrices.json";
        }

        public List<BeerPriceObj> GetMLBBeerPriceAll()
        {
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                List<BeerPriceObj> beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);
                return beerPriceObjList;
            }
        }

        public List<BeerPriceObj> GetMLBBeerPriceFromClub(string name)
        {
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                List<BeerPriceObj> beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);
                List<BeerPriceObj> beerPriceObjListWithName = new List<BeerPriceObj>();
                foreach (BeerPriceObj beerPriceObj in beerPriceObjList)
                {
                    if (beerPriceObj.team == name)
                    {
                        beerPriceObjListWithName.Add(beerPriceObj);
                    }
                }
                if (beerPriceObjListWithName.Count > 0)
                {
                    return beerPriceObjListWithName;
                }
            }
            throw new Exception("This team doesn't exsist");
        }

        public void CreateBeerPrice(BeerPriceObj beerpriceobj)
        {
            List<BeerPriceObj> beerPriceObjList = new List<BeerPriceObj>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);
                beerPriceObjList.Add(beerpriceobj);
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(beerPriceObjList));
        }

        public bool ChangeBeerPrice(BeerPriceObj beerpriceobj)
        {
            bool objFound = false;
            List<BeerPriceObj> beerPriceObjList = new List<BeerPriceObj>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);

                foreach (BeerPriceObj obj in beerPriceObjList)
                {
                    if (obj.team == beerpriceobj.team && obj.year == beerpriceobj.year)
                    {
                        obj.size = beerpriceobj.size;
                        obj.price = beerpriceobj.price;
                        obj.price_per_Ounce = beerpriceobj.price_per_Ounce;
                        objFound = true;
                    }
                }
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(beerPriceObjList));
            return objFound;
        }

        public bool DeleteBeerPrice(string team, int year)
        {
            bool objFound = false;
            List<BeerPriceObj> beerPriceObjList = new List<BeerPriceObj>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);

                foreach (BeerPriceObj obj in beerPriceObjList)
                {
                    if (obj.team == team && obj.year == year)
                    {
                        beerPriceObjList.Remove(obj);
                        objFound = true;
                        break;
                    }
                }
            }
            File.WriteAllText(path, JsonConvert.SerializeObject(beerPriceObjList));
            return objFound;
        }

        public bool ValidateReturnValuesAsJson(List<BeerPriceObj> list)
        {
            JSchema schema = JSchema.Parse(@"
                {
                  'title': 'BeerCost',
                  'type': 'object',
                  'items': {
                    'title': 'BeerCost',
                    'type': 'object',
                    'required': [
                      'year',
                      'team',
                      'nickName',
                      'city',
                      'size',
                      'pricePerOunce'
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

            foreach (BeerPriceObj beerPrice in list)
            {
                string temp = JsonConvert.SerializeObject(beerPrice);
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
