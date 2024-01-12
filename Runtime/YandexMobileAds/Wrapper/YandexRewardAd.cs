using LittleBit.Modules.CoreModule;
using LittleBitGames.Ads.AdUnits;
using YandexMobileAds.Base;

namespace YandexMobileAds.Wrapper
{
    public class YandexRewardAd : AdUnitLogic
    {
        private readonly RewardedAdLoader _rewardedAdLoader;
        private readonly string _adUnitId;
        private AdRequestConfiguration _adRequestConfiguration;
        private RewardedAd _rewarded;
        
        public YandexRewardAd(IAdUnitKey key, ICoroutineRunner coroutineRunner) : 
            base(key, GetInterEvent(out RewardedAdLoader rewardedAdLoader), coroutineRunner)
        {
            _adUnitId = key.StringValue;
            _rewardedAdLoader = rewardedAdLoader;
            _rewardedAdLoader.OnAdLoaded += RewardedAdLoaderOnOnAdLoaded;
        }

        private void RewardedAdLoaderOnOnAdLoaded(object sender, RewardedAdLoadedEventArgs e)
        {
            _rewarded = e.RewardedAd;
        }

        private static YandexSdkRewardEvents GetInterEvent(out RewardedAdLoader rewardedAdLoader)
        {
            rewardedAdLoader = new RewardedAdLoader();

            return new YandexSdkRewardEvents(rewardedAdLoader);
        }
        
        protected override bool IsAdReady()
        {
            return _rewarded != null;
        }

        protected override void ShowAd()
        {
            _rewarded.OnAdDismissed += (sender, args) =>
            {
                _rewarded.Destroy();
                _rewarded = null;
            };
            
            _rewarded.Show();
        }

        public override void Load()
        {
            //Ограничение для сбора данных пользователя. По умолчанию выключно
            //TODO: подключить к вопросу о сборе данных
            //MobileAds.SetAgeRestrictedUser(true);

            if (_rewarded != null)
            {
                _rewarded.Destroy();
            }
            
            _rewardedAdLoader.LoadAd(new AdRequestConfiguration.Builder(_adUnitId).Build());

        }
    }
}