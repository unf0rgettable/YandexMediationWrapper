using LittleBitGames.Ads.AdUnits;
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
        }

        public string Message { get; }
        public int MediatedNetworkErrorCode { get; }
        public string MediatedNetworkErrorMessage { get; }
    }
}