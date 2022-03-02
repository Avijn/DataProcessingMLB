using DataProcessingMLB.DAL;
using DataProcessingMLB.VM;
using System;

namespace DataProcessingMLB.BL
{
    public class MLBBeerManager
    {
        private readonly MLBBeerService _mLBBeerService;
        public MLBBeerManager()
        {
            _mLBBeerService = new MLBBeerService();
        }

        public BeerPriceObj GetMLBBeerPriceFromClub(string name)
        {
            return _mLBBeerService.GetMLBBeerPriceFromClub(name);

        }
    }
}
