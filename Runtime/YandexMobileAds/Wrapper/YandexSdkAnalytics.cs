using System;
using System.Collections.Generic;
using LittleBitGames.Ads;
using LittleBitGames.Ads.AdUnits;
using LittleBitGames.Ads.Collections.Extensions;
using LittleBitGames.Environment.Ads;
using LittleBitGames.Environment.Events;
using UnityEngine.Scripting;

namespace YandexMobileAds.Wrapper
{
    public class YandexSdkAnalytics : IMediationNetworkAnalytics
    {
        private const string SdkSourceName = "yandex_sdk";
        private const string Currency = "USD";

        private readonly IReadOnlyList<IAdUnit> _adUnits;

        public event Action<IDataEventAdImpression, AdType> OnAdRevenuePaidEvent;

        [Preserve]
        public YandexSdkAnalytics(IAdsService adsService)
        {
            _adUnits = adsService.AdUnits;
            
            if (!_adUnits.Validate()) ThrowException();
            
            adsService.Initializer.OnMediationInitialized += Subscribe;
        }

        private static void ThrowException() =>
            throw new Exception("Invalid list of ad units was provided to MaxSdkAnalytics");

        private void Subscribe()
        {
            foreach (var adUnit in _adUnits)
            {
                if (adUnit is YandexInterAd)
                {
                    adUnit.Events.OnAdRevenuePaid += delegate(string s, IAdInfo info)
                    {
                        OnAdRevenuePaid(s, info, AdType.Inter);
                    };
                }
                
                if (adUnit is YandexRewardAd)
                {
                    adUnit.Events.OnAdRevenuePaid += delegate(string s, IAdInfo info)
                    {
                        OnAdRevenuePaid(s, info, AdType.Rewarded);
                    };
                }
                
            }
        }

        private void OnAdRevenuePaid(string adUnitId, IAdInfo adInfo, AdType adType)
        {
            var ad = _adUnits.FindByKey(adUnitId);
            
            var adImpressionEvent = new DataEventAdImpression(
                new SdkSource(SdkSourceName),
                adInfo.NetworkName,
                adInfo.AdFormat,
                ad.UnitPlace.StringValue,
                Currency,
                adInfo.Revenue);

            OnAdRevenuePaidEvent?.Invoke(adImpressionEvent, adType);
        }
    }
}