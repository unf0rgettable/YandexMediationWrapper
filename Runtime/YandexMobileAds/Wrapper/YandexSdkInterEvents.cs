using System;
using LittleBitGames.Ads.AdUnits;

namespace YandexMobileAds.Wrapper
{
    public class YandexSdkInterEvents : IAdUnitEvents
    {
        public event Action<string, IAdInfo> OnAdRevenuePaid;
        public event Action<string, IAdInfo> OnAdLoaded;
        public event Action<string, IAdErrorInfo> OnAdLoadFailed;
        public event Action<string, IAdInfo> OnAdFinished;
        public event Action<string, IAdInfo> OnAdClicked;
        public event Action<string, IAdInfo> OnAdHidden;
        public event Action<string, IAdErrorInfo, IAdInfo> OnAdDisplayFailed;

        public YandexSdkInterEvents(Interstitial interstitial)
        {
            interstitial.OnInterstitialLoaded += (s, info) => OnAdLoaded?.Invoke(null, null);
            interstitial.OnInterstitialFailedToLoad += (s, info) => OnAdLoadFailed?.Invoke(null, new AdErrorInfo(info));
            interstitial.OnAdClicked += (s, info) => OnAdClicked?.Invoke(null, null);
            interstitial.OnInterstitialDismissed += (s, info) => OnAdHidden?.Invoke(null, null);
            interstitial.OnInterstitialFailedToShow +=
                (s, error) => OnAdDisplayFailed?.Invoke(null, new AdErrorInfo(error), null);
            
            interstitial.OnInterstitialDismissed += (s, info) =>
            {
#if UNITY_EDITOR
                OnAdFinished?.Invoke(null, null);
#endif
            };
            
            interstitial.OnInterstitialShown += (s, info) =>
            {
                OnAdFinished?.Invoke(null, null);
            };
            
            interstitial.OnImpression += (s, info) =>
            {
                OnAdRevenuePaid?.Invoke(null, new AdInfo(info));
            };
        }
        
        
    }
}