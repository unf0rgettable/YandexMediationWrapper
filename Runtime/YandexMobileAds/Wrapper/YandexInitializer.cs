using System;
using LittleBitGames.Ads.Configs;
using LittleBitGames.Environment;
using LittleBitGames.Environment.Ads;

namespace YandexMobileAds.Wrapper
{
    public class YandexInitializer : IMediationNetworkInitializer
    {
        public event Action OnMediationInitialized;
        
        private readonly AdsConfig _config;
        
        public YandexInitializer(AdsConfig config) => _config = config;
        
        private bool IsDebugMode => _config.Mode is ExecutionMode.Debug;
        
        public bool IsInitialized { get; private set; }

        public void Initialize()
        {
            IsInitialized = true;
            OnMediationInitialized?.Invoke();
        }
    }
}