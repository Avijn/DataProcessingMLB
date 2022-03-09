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
            path = @".\Datasets\MLBBeerPrices\MLBBeerPrices.json";
        }

        public List<BeerPriceObj> GetMLBBeerPriceFromClub(string name)
        {
            //Read from JSON file
            //If clubname === name

            using(StreamReader stream = new StreamReader(path))
            {
                string json = stream.ReadToEnd();
                List<BeerPriceObj> beerPriceObjList = JsonConvert.DeserializeObject<List<BeerPriceObj>>(json);
                List<BeerPriceObj> beerPriceObjListWithName = new List<BeerPriceObj>();
                foreach(BeerPriceObj beerPriceObj in beerPriceObjList)
                {
                    Console.WriteLine(beerPriceObj.Nickname);
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
