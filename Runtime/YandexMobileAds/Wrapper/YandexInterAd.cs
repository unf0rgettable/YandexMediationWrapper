using System;
using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;
using YandexMobileAds.Base;

namespace YandexMobileAds.Wrapper
{
    public class YandexInterAd : AdUnitLogic
    {
        private readonly InterstitialAdLoader _interstitialAdLoader;
        private readonly string _adUnitId;
        private AdRequestConfiguration _adRequestConfiguration;
        private Interstitial _interstitial;
        public YandexInterAd(IAdUnitKey key, ICoroutineRunner coroutineRunner) : 
            base(key, GetInterEvent(out InterstitialAdLoader interstitial), coroutineRunner)
        {
            _adUnitId = key.StringValue;
            _interstitialAdLoader = interstitial;
            _interstitialAdLoader.OnAdLoaded += InterstitialAdLoaderOnOnAdLoaded;
        }

        private static YandexSdkInterEvents GetInterEvent(out InterstitialAdLoader interstitial)
        {
            interstitial = new InterstitialAdLoader();
            
            return new YandexSdkInterEvents(interstitial);
        }

        protected override bool IsAdReady()
        {
            return _interstitial != null;
        }

        protected override void ShowAd()
        {
            _interstitial.OnAdDismissed += (sender, args) =>
            {
                _interstitial.Destroy();
                _interstitial = null;
            };
            
            _interstitial.Show();
        }

        public override void Load()
        {
            //Ограничение для сбора данных пользователя. По умолчанию выключно
            //TODO: подключить к вопросу о сборе данных
            //MobileAds.SetAgeRestrictedUser(true);

            if (_interstitial != null)
            {
                _interstitial.Destroy();
            }
            
            _interstitialAdLoader.LoadAd(new AdRequestConfiguration.Builder(_adUnitId).Build());
        }

        private void InterstitialAdLoaderOnOnAdLoaded(object sender, InterstitialAdLoadedEventArgs e)
        {
            _interstitial = e.Interstitial;
        }
    }
}