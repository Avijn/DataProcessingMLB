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

        public void CreateBeerPrice(BeerPriceObj beerpriceobj)
        {
            _mLBBeerService.CreateBeerPrice(beerpriceobj);
        }

        public bool ChangeBeerPrice(BeerPriceObj beerpriceobj)
        {
            return _mLBBeerService.ChangeBeerPrice(beerpriceobj);
        }

        public bool DeleteBeerPrice(string team, int year)
        {
            return _mLBBeerService.DeleteBeerPrice(team, year);
        }
    }
}
