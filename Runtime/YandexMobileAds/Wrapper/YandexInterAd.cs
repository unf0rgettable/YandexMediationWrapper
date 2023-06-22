using System;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;
using YandexMobileAds.Base;

namespace YandexMobileAds.Wrapper
{
    public class YandexInterAd : AdUnitLogic
    {
        private readonly Interstitial _interstitial;

        public YandexInterAd(IAdUnitKey key, ICoroutineRunner coroutineRunner) : 
            base(key, GetInterEvent(key, out Interstitial interstitial), coroutineRunner)
        {
            _interstitial = interstitial;
        }

        public static YandexSdkInterEvents GetInterEvent(IAdUnitKey key, out Interstitial interstitial)
        {
            interstitial = new Interstitial(key.StringValue);

            return new YandexSdkInterEvents(interstitial);
        }

        protected override bool IsAdReady()
        {
            return _interstitial.IsLoaded();
        }

        protected override void ShowAd()
        {
            _interstitial.Show();
        }

        public override void Load()
        {
            AdRequest adRequest = new AdRequest.Builder().Build();
            
            _interstitial.LoadAd(adRequest);
        }
    }
}