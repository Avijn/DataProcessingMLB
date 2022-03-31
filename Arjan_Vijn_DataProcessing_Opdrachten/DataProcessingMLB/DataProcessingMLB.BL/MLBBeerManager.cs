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

        public List<BeerPriceObj> GetMLBBeerPriceAll(string value)
        {
            List<BeerPriceObj> list = _mLBBeerService.GetMLBBeerPriceAll();
            if (value != "application/xml")
            {
                //if (_mLBBeerService.ValidateReturnValuesAsJson(list))
                //{
                    return list;
                //}
            }

            if (value == "application/xml")
            {
                return list;
            }
            throw new Exception("Something went wrong!");
        }

        public List<BeerPriceObj> GetMLBBeerPriceFromClub(string name, string value)
        {
            List<BeerPriceObj> list = _mLBBeerService.GetMLBBeerPriceAll();
            if (value != "application/xml")
            {
                //if (_mLBBeerService.ValidateReturnValuesAsJson(list))
                //{
                    return _mLBBeerService.GetMLBBeerPriceFromClub(name);
                //}
            }

            if (value == "application/xml")
            {
                return list;
            }
            throw new Exception("Something went wrong!");
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
