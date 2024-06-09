using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.Events;

public class AdmobManager {
    private string _adUnitId;
    private float _currentTimeScale;
    private RewardedAd _rewardedAd;

    /// <summary>
    /// Loads the rewarded ad.
    /// </summary>
    private void LoadRewardedAd() {
        // Clean up the old ad before loading a new one.
        if (_rewardedAd != null) {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }
        DebugWrapper.Log("Loading the rewarded ad.");

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

                DebugWrapper.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                _rewardedAd = ad;
            });
    }

    public void ShowRewardedAd(UnityAction callBack) {
        _currentTimeScale = Time.timeScale;

        if (_rewardedAd != null && _rewardedAd.CanShowAd()) {
            _rewardedAd.Show((Reward reward) => {
                callBack.Invoke();
                Time.timeScale = _currentTimeScale;
                LoadRewardedAd();
            });
        } else {
            LoadRewardedAd();
        }
    }

    public void Init(string id) {
        _adUnitId = id;
        MobileAds.Initialize((InitializationStatus initStatus) => {
            LoadRewardedAd();
        });
    }
}