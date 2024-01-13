using LittleBitGames.Ads.AdUnits;
using UnityEngine;
using YandexMobileAds.Base;

namespace YandexMobileAds.Wrapper
{
    public class AdErrorInfo : IAdErrorInfo
    {
        public AdErrorInfo(AdFailureEventArgs adFailureEventArgs)
        {
            Message = adFailureEventArgs.Message;
            MediatedNetworkErrorCode = -1;
            MediatedNetworkErrorMessage = "Undefined";
            
            Debug.LogError("adFailureEventArgs.Message " + adFailureEventArgs.Message);
        }
        
        public AdErrorInfo(AdFailedToLoadEventArgs adFailureEventArgs)
        {
            Message = adFailureEventArgs.Message;
            MediatedNetworkErrorCode = -1;
            MediatedNetworkErrorMessage = "Undefined";

            Debug.LogError("adFailureEventArgs.Message " + adFailureEventArgs.Message);
            Debug.LogError("adFailureEventArgs.AdUnitId " + adFailureEventArgs.AdUnitId);
        }

        public string Message { get; }
        public int MediatedNetworkErrorCode { get; }
        public string MediatedNetworkErrorMessage { get; }
    }
}