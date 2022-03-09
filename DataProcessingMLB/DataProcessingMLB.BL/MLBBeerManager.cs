using DataProcessingMLB.DAL;
using DataProcessingMLB.VM;
using System;
using System.Collections.Generic;

namespace DataProcessingMLB.BL
{
    public class MLBBeerManager
    {
        private readonly MLBBeerService _mLBBeerService;
        public MLBBeerManager()
        {
            _mLBBeerService = new MLBBeerService();
        }

        public List<BeerPriceObj> GetMLBBeerPriceAll()
        {
            return _mLBBeerService.GetMLBBeerPriceAll();
        }

        public List<BeerPriceObj> GetMLBBeerPriceFromClub(string name)
        {
            return _mLBBeerService.GetMLBBeerPriceFromClub(name);

        }
    }
}
