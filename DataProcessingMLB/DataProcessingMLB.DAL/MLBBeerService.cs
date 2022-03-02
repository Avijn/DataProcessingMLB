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
            path = @"..\Datasets\MLBBeerPrices\MLBBeerPrices.json";
        }

        public BeerPriceObj GetMLBBeerPriceFromClub(string name)
        {
            //Read from JSON file
            //If clubname === name

            using(StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                List<BeerPriceObj> beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);
                
                foreach(BeerPriceObj beerPriceObj in beerPriceObjList)
                {
                    if(beerPriceObj.Team == name)
                    {
                        return beerPriceObj;
                    }
                }
            }
            //return 
        }
    }
}
