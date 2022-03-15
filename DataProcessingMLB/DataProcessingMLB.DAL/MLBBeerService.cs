using DataProcessingMLB.VM;
using Newtonsoft.Json;
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
            using(StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                List<BeerPriceObj> beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);
                return beerPriceObjList;
            }
        }

        public List<BeerPriceObj> GetMLBBeerPriceFromClub(string name)
        {
            using(StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                List<BeerPriceObj> beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);
                List<BeerPriceObj> beerPriceObjListWithName = new List<BeerPriceObj>();
                foreach(BeerPriceObj beerPriceObj in beerPriceObjList)
                {
                    if(beerPriceObj.Team == name)
                    {
                        beerPriceObjListWithName.Add(beerPriceObj);
                    }
                }
                if(beerPriceObjListWithName.Count > 0)
                {
                    return beerPriceObjListWithName;
                }
            }
            throw new Exception("This team doesn't exsist"); 
        }

        public void CreateBeerPrice(BeerPriceObj beerpriceobj)
        {
            //TODO Check if the beerprice is made or not
            List<BeerPriceObj> beerPriceObjList = new List<BeerPriceObj>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);
                beerPriceObjList.Add(beerpriceobj);
            }
            File.WriteAllText(@"C:\DataprocessingMLB\DataProcessingMLB\Datasets\MLBBeerPrices\MLBBeerPrices.json", JsonConvert.SerializeObject(beerPriceObjList));
        }

        public bool ChangeBeerPrice(BeerPriceObj beerpriceobj)
        {
            bool objFound = false;
            List<BeerPriceObj> beerPriceObjList = new List<BeerPriceObj>();
            using (StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);
                
                foreach(BeerPriceObj obj in beerPriceObjList)
                {
                    if (obj.Team == beerpriceobj.Team && obj.Year == beerpriceobj.Year)
                    {
                        obj.Size = beerpriceobj.Size;
                        obj.Price = beerpriceobj.Price;
                        obj.PricePerOunce = beerpriceobj.PricePerOunce;
                        objFound = true;
                    }
                }
            }
            File.WriteAllText(@"C:\DataprocessingMLB\DataProcessingMLB\Datasets\MLBBeerPrices\MLBBeerPrices.json", JsonConvert.SerializeObject(beerPriceObjList));
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
                    if (obj.Team == team && obj.Year == year)
                    {
                        beerPriceObjList.Remove(obj);
                        objFound = true;
                        break;
                    }
                }
            }
            File.WriteAllText(@"C:\DataprocessingMLB\DataProcessingMLB\Datasets\MLBBeerPrices\MLBBeerPrices.json", JsonConvert.SerializeObject(beerPriceObjList));
            return objFound;
        }
    }
}
