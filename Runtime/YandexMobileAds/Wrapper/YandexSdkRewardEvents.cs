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
        
        public YandexSdkRewardEvents(RewardedAd rewardedAd)
        {
            rewardedAd.OnRewardedAdLoaded += (s, info) => OnAdLoaded?.Invoke(null, null);
            rewardedAd.OnRewardedAdFailedToLoad += (s, info) => OnAdLoadFailed?.Invoke(null, new AdErrorInfo(info));
            rewardedAd.OnAdClicked += (s, info) => OnAdClicked?.Invoke(null, null);
            rewardedAd.OnRewardedAdDismissed += (s, info) => OnAdHidden?.Invoke(null, null);
            rewardedAd.OnRewardedAdFailedToShow +=
                (s, error) => OnAdDisplayFailed?.Invoke(null, new AdErrorInfo(error), null);
            
            rewardedAd.OnRewardedAdDismissed += (s, info) =>
            {
#if UNITY_EDITOR
                OnAdFinished?.Invoke(null, null);
#endif
            };
            
            rewardedAd.OnRewardedAdShown += (s, info) =>
            {
                OnAdFinished?.Invoke(null, null);
                Debug.LogError("OnRewardedAdShown");
            };
            
            rewardedAd.OnImpression += (s, info) =>
            {
                OnAdRevenuePaid?.Invoke(null, new AdInfo(info));
                Debug.LogError("OnImpression");
            };
        }
    }
}