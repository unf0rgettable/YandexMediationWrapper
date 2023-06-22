using System;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;
using YandexMobileAds.Wrapper;

namespace LittleBitGames.Ads.Configs
{
    public class YandexSdkAdUnitsFactory
    {
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly AdsConfig _adsConfig;

        public YandexSdkAdUnitsFactory(ICoroutineRunner coroutineRunner, AdsConfig adsConfig)
        {
            _adsConfig = adsConfig;

            _coroutineRunner = coroutineRunner;
        }

        public IAdUnit CreateInterAdUnit() =>
            new YandexInterAd(GetKey(_adsConfig.YandexSettings.PlatformSettings.YandexInterAdUnitKey), _coroutineRunner);

        public IAdUnit CreateRewardedAdUnit() =>
            new YandexRewardAd(GetKey(_adsConfig.YandexSettings.PlatformSettings.YandexRewardedAdUnitKey), _coroutineRunner);

        private IAdUnitKey GetKey(string s)
        {
            var key = new AdUnitKey(s);

            if (!key.Validate()) throw new Exception($"Yandex ad unit key is invalid! Key: {s}");

            return key;
        }
    }
}