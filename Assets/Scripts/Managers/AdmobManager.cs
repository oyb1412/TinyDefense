using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using UnityEngine.Events;

public class AdmobManager {

    // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
    private string _adUnitId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
  private string _adUnitId = "ca-app-pub-3940256099942544/1712485313";
#else
  private string _adUnitId = "unused";
#endif

    private RewardedAd _rewardedAd;

    private float _currentTimeScale;

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    private void LoadRewardedAd() {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null) {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) => {
                // if error is not null, the load request failed.
                if (error != null || ad == null) {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }

    public void ShowRewardedAd(UnityAction callBack) {
        const string rewardMsg =
            "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        _currentTimeScale = Time.timeScale;

        if (_rewardedAd != null && _rewardedAd.CanShowAd()) {
            _rewardedAd.Show((Reward reward) => {
                callBack.Invoke();
                Time.timeScale = _currentTimeScale;
                Debug.Log("광고를 모두 시청하였으므로 보상 지급");
                //Debug.Log(string.Format(rewardMsg, reward.Type, reward.Amount));
                LoadRewardedAd();
            });
        } else {
            LoadRewardedAd();
        }
    }

    public void Init() {
        MobileAds.Initialize((InitializationStatus initStatus) => {
            LoadRewardedAd();
        });
    }

}