using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;
using YandexMobileAds.Base;

namespace YandexMobileAds.Wrapper
{
    public class YandexRewardAd : AdUnitLogic
    {
        private readonly RewardedAd _rewardedAd;
        
        public YandexRewardAd(IAdUnitKey key, ICoroutineRunner coroutineRunner) : base(key, GetInterEvent(key, out RewardedAd rewardedAd), coroutineRunner)
        {
            _rewardedAd = rewardedAd;
        }

        public static YandexSdkRewardEvents GetInterEvent(IAdUnitKey key, out RewardedAd rewardedAd)
        {
            rewardedAd = new RewardedAd(key.StringValue);

            return new YandexSdkRewardEvents(rewardedAd);
        }
        
        protected override bool IsAdReady()
        {
            return _rewardedAd.IsLoaded();
        }

        protected override void ShowAd()
        {
            _rewardedAd.Show();
        }

        public override void Load()
        {
            AdRequest adRequest = new AdRequest.Builder().Build();
            
            _rewardedAd.LoadAd(adRequest);
        }
    }
}