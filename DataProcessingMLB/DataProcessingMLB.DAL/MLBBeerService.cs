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
    }
}
