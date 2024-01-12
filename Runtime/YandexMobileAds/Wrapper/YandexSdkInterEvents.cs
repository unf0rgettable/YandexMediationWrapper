using System;
using LittleBitGames.Ads.AdUnits;
using UnityEngine;

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

        public YandexSdkInterEvents(InterstitialAdLoader interstitialAdLoader)
        {
            interstitialAdLoader.OnAdLoaded += (s, info) =>
            {
                info.Interstitial.OnAdClicked += (s, info) => OnAdClicked?.Invoke(null, null);
                info.Interstitial.OnAdImpression += (s, info) =>
                {
                    Debug.LogError("OnInterstitialShown");
                    OnAdFinished?.Invoke(null, null);
                    Debug.LogError("OnImpression");
                    OnAdRevenuePaid?.Invoke(null, new AdInfo(info));
                };
                
                info.Interstitial.OnAdDismissed += (s, info) => OnAdHidden?.Invoke(null, null);
                info.Interstitial.OnAdFailedToShow +=
                    (s, error) => OnAdDisplayFailed?.Invoke(null, new AdErrorInfo(error), null);
                
                info.Interstitial.OnAdDismissed += (s, info) =>
                {
#if UNITY_EDITOR
                    OnAdFinished?.Invoke(null, null);
#endif
                };
                
                OnAdLoaded?.Invoke(null, null);
            };
            
            interstitialAdLoader.OnAdFailedToLoad += (s, info) => OnAdLoadFailed?.Invoke(null, new AdErrorInfo(info));
        }
    }
}