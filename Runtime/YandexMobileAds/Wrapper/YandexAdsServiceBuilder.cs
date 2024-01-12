using System;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads;
using LittleBitGames.Ads.AdUnits;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Environment.Ads;

namespace YandexMobileAds.Wrapper
{
    public class YandexAdsServiceBuilder : IAdsServiceBuilder
    {
        private readonly YandexSdkAdUnitsFactory _adUnitsFactory;
        private readonly YandexInitializer _initializer;

        private IAdUnit _inter, _rewarded;
        private AdsConfig _adsConfig;

        public IMediationNetworkInitializer Initializer => _initializer;

        public YandexAdsServiceBuilder(AdsConfig adsConfig, ICoroutineRunner coroutineRunner)
        {
            _adsConfig = adsConfig;
            _adUnitsFactory = new YandexSdkAdUnitsFactory(coroutineRunner, adsConfig);
            _initializer = new YandexInitializer(adsConfig);
        }

        public IAdsService QuickBuild()
        {
            if (!string.IsNullOrEmpty(_adsConfig.YandexSettings.PlatformSettings.YandexInterAdUnitKey) && _adsConfig.IsInter) 
                BuildInterAdUnit();
            if (!string.IsNullOrEmpty(_adsConfig.YandexSettings.PlatformSettings.YandexRewardedAdUnitKey) && _adsConfig.IsRewarded) 
                BuildRewardedAdUnit();

            return GetResult();
        }

        public void BuildInterAdUnit() =>
            _inter = _adUnitsFactory.CreateInterAdUnit();

        public void BuildRewardedAdUnit() =>
            _rewarded = _adUnitsFactory.CreateRewardedAdUnit();

        //заглушил баннер!!!
        public IAdsService GetResult() => new AdsService(_initializer, _inter, _rewarded, null);
    }
}