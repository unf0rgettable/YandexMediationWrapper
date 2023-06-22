using LittleBitGames.Ads.AdUnits;
using UnityEngine;
using YandexMobileAds.Base;

namespace YandexMobileAds.Wrapper
{
    public class AdInfo : IAdInfo
    {
        public AdInfo(ImpressionData impressionData)
        {
            var yandexAdInfoModel = JsonUtility.FromJson<YandexAdInfoModel>(impressionData.rawData);

            AdUnitIdentifier = yandexAdInfoModel.blockId;
            AdFormat = yandexAdInfoModel.adType;
            Revenue = yandexAdInfoModel.revenueUSD;
            RevenuePrecision = yandexAdInfoModel.precision;
            NetworkName = "Undefined";
            NetworkPlacement = "Undefined";
            Placement = "Undefined";
            CreativeIdentifier = "Undefined";
            DspName = "Undefined";
        }
        
        public string AdUnitIdentifier { get; }
        public string AdFormat { get; }
        public string NetworkName { get; }
        public string NetworkPlacement { get; }
        public string Placement { get; }
        public string CreativeIdentifier { get; }
        public double Revenue { get; }
        public string RevenuePrecision { get; }
        public string DspName { get; }
    }
}