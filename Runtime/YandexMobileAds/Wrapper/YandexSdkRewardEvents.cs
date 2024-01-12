using System;
using LittleBitGames.Ads.AdUnits;
using UnityEngine;

namespace YandexMobileAds.Wrapper
{
    public class YandexSdkRewardEvents : IAdUnitEvents
    {
        public event Action<string, IAdInfo> OnAdRevenuePaid;
        public event Action<string, IAdInfo> OnAdLoaded;
        public event Action<string, IAdErrorInfo> OnAdLoadFailed;
        public event Action<string, IAdInfo> OnAdFinished;
        public event Action<string, IAdInfo> OnAdClicked;
        public event Action<string, IAdInfo> OnAdHidden;
        public event Action<string, IAdErrorInfo, IAdInfo> OnAdDisplayFailed;
        
        public YandexSdkRewardEvents(RewardedAdLoader rewardedAdLoader)
        {
            rewardedAdLoader.OnAdLoaded += (s, info) =>
            {
                info.RewardedAd.OnAdClicked += (s, info) => OnAdClicked?.Invoke(null, null);
                info.RewardedAd.OnAdImpression += (s, info) =>
                {
                    Debug.LogError("OnInterstitialShown");
                    OnAdFinished?.Invoke(null, null);
                    Debug.LogError("OnImpression");
                    OnAdRevenuePaid?.Invoke(null, new AdInfo(info));
                };
                
                info.RewardedAd.OnAdDismissed += (s, info) => OnAdHidden?.Invoke(null, null);
                info.RewardedAd.OnAdFailedToShow +=
                    (s, error) => OnAdDisplayFailed?.Invoke(null, new AdErrorInfo(error), null);
                
                info.RewardedAd.OnAdDismissed += (s, info) =>
                {
#if UNITY_EDITOR
                    OnAdFinished?.Invoke(null, null);
#endif
                };
                
                OnAdLoaded?.Invoke(null, null);
            };
            
            rewardedAdLoader.OnAdFailedToLoad += (s, info) => OnAdLoadFailed?.Invoke(null, new AdErrorInfo(info));
        }
    }
}