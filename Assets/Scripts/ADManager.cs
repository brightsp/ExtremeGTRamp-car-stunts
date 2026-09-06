using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using GoogleMobileAds.Common;

public class ADManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener, IUnityAdsLoadListener
{

    UnityAction localCallback;
    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private static ADManager _instance;
    public string ADMOB_bannerID, ADMOB_interstitialID, ADMOB_rewardedVideoID, UNITY_Key;
    public static ADManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ADManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    void Start()
    {
        InitilizeAdmob();
        InitilizeUnityAd();


    }
    private void InitilizeAdmob()
    {
        //RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True).build();
        //MobileAds.SetRequestConfiguration(requestConfiguration);
        //MobileAds.Initialize(HandleInitCompleteAction);

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });
    }

    private void HandleInitCompleteAction(InitializationStatus initstatus)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            // RequestAdmobBanner();
            RequestAdmobInterstitial();
            RequestAdmobRewardBasedVideo();
        });
    }


    void InitilizeUnityAd()
    {
        Advertisement.Initialize(UNITY_Key, false, this);
    }
    public void ShowAdmobRe()
    {
        ShowAdmobRewardedVideo(null);
    }

    public void ShowUnityRe()
    {
        ShowUnityRewardedVideo(null);
    }
    private void LoadUnityBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Load("Banner_Android", options);
    }
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        // LoadUnityBanner();
        Debug.Log($"Banner Error: {message}");
    }
    public void ShowUnityBanner(string adPoss = "bm")
    {
        //if (Advertisement.Banner.isLoaded)
        //{
        //    return;
        //}
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        BannerPosition newPos = new BannerPosition();
        if (adPoss == "tl")
        {
            newPos = BannerPosition.TOP_LEFT;
        }
        if (adPoss == "tm")
        {
            newPos = BannerPosition.TOP_CENTER;
        }
        if (adPoss == "tr")
        {
            newPos = BannerPosition.TOP_RIGHT;
        }
        if (adPoss == "bl")
        {
            newPos = BannerPosition.BOTTOM_LEFT;
        }
        if (adPoss == "bm")
        {
            newPos = BannerPosition.BOTTOM_CENTER;
        }
        if (adPoss == "br")
        {
            newPos = BannerPosition.BOTTOM_RIGHT;
        }


        Advertisement.Banner.SetPosition(newPos);
        Advertisement.Banner.Show("Banner_Android", options);

    }
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    private void LoadUnityInterstitial()
    {
        Advertisement.Load("Interstitial_Android", this);
    }

    // Show the loaded content in the Ad Unit: 
    public void ShowUnityInterstitialAd()
    {
        if (PlayerPrefs.GetInt("RemoveAD") == 0)
        {

            Advertisement.Show("Interstitial_Android", this);

            Advertisement.Load("Interstitial_Android", this);

        }
    }
    private void LoadUnityRewarded()
    {
        Advertisement.Load("Rewarded_Android", this);
    }
    public void ShowUnityRewardedVideo(UnityAction callback)
    {
        localCallback = callback;

        Advertisement.Show("Rewarded_Android", this);

        Advertisement.Load("Rewarded_Android", this);

    }
    public void RequestAdmobBanner(string adPoss)
    {
        if (bannerView != null)
            return;


        Debug.Log("Show banner ==>RequestAdmobBanner bannerView=null");

        AdPosition newPos = new AdPosition();
        if (adPoss == "tl")
        {
            newPos = AdPosition.TopLeft;
        }
        if (adPoss == "tm")
        {
            newPos = AdPosition.Top;
        }
        if (adPoss == "tr")
        {
            newPos = AdPosition.TopRight;
        }
        if (adPoss == "bl")
        {
            newPos = AdPosition.BottomLeft;
        }
        if (adPoss == "bm")
        {
            newPos = AdPosition.Bottom;
        }
        if (adPoss == "br")
        {
            newPos = AdPosition.BottomRight;
        }

        if (bannerView != null)
        {
            bannerView.Destroy();
            bannerView = null;
        }
        bannerView = new BannerView(ADMOB_bannerID, new AdSize(300, 100), newPos);

        bannerView.OnBannerAdLoaded += () =>
        {
            Debug.Log("Banner view loaded an ad with response : "
                + bannerView.GetResponseInfo());
        };
        // Raised when an ad fails to load into the banner view.
        bannerView.OnBannerAdLoadFailed += (LoadAdError error) =>
        {
            string[] val = ("a_a").Split('_'); // SKAds.admobBanner.Split('_');

            RequestAdmobBanner(val[1]);
            Debug.LogError("Banner view failed to load an ad with error : "
                + error);
        };

        // Create an empty ad request.
        // AdRequest request = new AdRequest.Builder().Build();
        var request = new AdRequest();
        // Load the banner with the request.
        bannerView.LoadAd(request);
        bannerView.Hide();

        ShowAdmobBanner();
    }
    public void HideAdmobBanner()
    {
        if (bannerView != null)
        {
            bannerView.Hide();
        }
    }
    public void ShowAdmobBanner()
    {

        if (bannerView != null)
        {
            bannerView.Show();
        }

    }
    void RequestAdmobInterstitial()
    {


        // Clean up the old ad before loading a new one.
        if (this.interstitial != null)
        {
            this.interstitial.Destroy();
            this.interstitial = null;
        }



        //this.interstitial = new InterstitialAd(ADMOB_interstitialID);

        //// Called when an ad request has successfully loaded.
        //this.interstitial.OnAdLoaded += HandleInterstitialOnAdLoaded;
        //// Called when an ad request failed to load.
        //this.interstitial.OnAdFailedToLoad += HandleInterstitialOnAdFailedToLoad;
        //// Called when an ad is shown.
        //this.interstitial.OnAdOpening += HandleInterstitialOnAdOpened;
        //// Called when the ad is closed.
        //this.interstitial.OnAdClosed += HandleInterstitialOnAdClosed;
        // Create an empty ad request.
        // AdRequest request = new AdRequest.Builder().Build();
        AdRequest request = new AdRequest();
        // Load the interstitial with the request.
        InterstitialAd.Load(ADMOB_interstitialID, request,
          (InterstitialAd ad, LoadAdError error) =>
          {
              // if error is not null, the load request failed.
              if (error != null || ad == null)
              {
                  Debug.LogError("interstitial ad failed to load an ad " +
                                 "with error : " + error);
                  return;
              }

              Debug.Log("Interstitial ad loaded with response : "
                        + ad.GetResponseInfo());

              this.interstitial = ad;

              // Register to ad events to extend functionality.
              RegisterReloadHandler(ad);
          });




    }

    private void RegisterReloadHandler(InterstitialAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            RequestAdmobInterstitial();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            RequestAdmobInterstitial();
        };
    }


    public void ShowAdmobInterstitial()
    {

        if (this.interstitial != null && this.interstitial.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            this.interstitial.Show();
        }
        else
        {
            Debug.LogError("Interstitial ad is not ready yet.");
            RequestAdmobInterstitial();
        }


        //if (interstitial.IsLoaded())
        //{
        //    interstitial.Show();
        //    RequestAdmobInterstitial();
        //}
        //else
        //{
        //    RequestAdmobInterstitial();
        //}
    }
    public void RequestAdmobRewardBasedVideo(bool forceAd = false)
    {

        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        RewardedAd.Load(ADMOB_rewardedVideoID, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            // If the operation failed with a reason.
            if (error != null)
            {
                Debug.LogError("Rewarded ad failed to load an ad with error : " + error);
                return;
            }
            // If the operation failed for unknown reasons.
            // This is an unexpected error, please report this bug if it happens.
            if (ad == null)
            {
                Debug.LogError("Unexpected error: Rewarded load event fired with null ad and null error.");
                return;
            }

            // The operation completed successfully.
            Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());
            rewardedAd = ad;

            // Register to ad events to extend functionality.
            RegisterRewardsEventHandlers(ad);

            if (forceAd)
            {
                ShowAdmob_ForceRewardedVideo();
            }


        });



    }

    private void RegisterRewardsEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            RequestAdmobRewardBasedVideo();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);
            RequestAdmobRewardBasedVideo();
        };
    }


    public bool isRewardedVideoAvailable()
    {
        return false;// rewardedAd.IsLoaded();
    }
    public void ShowAdmobRewardedVideo(UnityAction callback)
    {

        //  RequestAdmobRewardBasedVideo();


        localCallback = callback;

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            Debug.Log("Showing rewarded ad.");
            localCallback = callback;
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log(String.Format("Rewarded ad granted a reward: {0} {1}",
                                        reward.Amount,
                                        reward.Type));

                if (localCallback != null)
                    localCallback.Invoke();
            });
        }
        else
        {
            Debug.LogError("Rewarded ad is not ready yet.");
            RequestAdmobRewardBasedVideo(true);

            //  MyToast.mee.MyShowToastMethod("Reward Ad is Not Available");

        }
    }

    void ShowAdmob_ForceRewardedVideo()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            Debug.Log("Showing rewarded ad Forcily.");
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log(String.Format("Rewarded ad granted a reward: {0} {1}",
                                        reward.Amount,
                                        reward.Type));

                if (localCallback != null)
                    localCallback.Invoke();
            });
        }
        else
        {
            Debug.LogError("Rewarded ad is not ready yet twice call.");
            RequestAdmobRewardBasedVideo();

            MyToast.mee.MyShowToastMethod("Reward Ad is Not Available");

        }
    }

    public void HandleOnAdLoaded()
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    //public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    //{
    //    string[] val = ("a_a").Split('_'); // SKAds.admobBanner.Split('_');

    //    RequestAdmobBanner(val[1]);
    //    MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
    //                        + args.ToString());
    //}

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void HandleInterstitialOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    //public void HandleInterstitialOnAdFailedToLoad(object sender,)
    //{
    //    RequestAdmobInterstitial();
    //    MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
    //                        + args.ToString());
    //}

    public void HandleInterstitialOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleInterstitialOnAdClosed(object sender, EventArgs args)
    {
        RequestAdmobInterstitial();
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleInterstitialOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, EventArgs args)
    {
        RequestAdmobRewardBasedVideo();

        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: "
                             + args.ToString());
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    //public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    //{
    //    MonoBehaviour.print(
    //        "HandleRewardedAdFailedToShow event received with message: "
    //                         + args.ToString());
    //}

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RequestAdmobRewardBasedVideo();
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        if (localCallback != null)
            localCallback.Invoke();
    }
    bool IronBannerLoaded = false;
    //Invoked once the banner has loaded
    void BannerAdLoadedEvent()
    {
        IronBannerLoaded = true;

    }

    // Invoked when end user clicks on the banner ad
    void BannerAdClickedEvent()
    {
    }
    //Notifies the presentation of a full screen content following user click
    void BannerAdScreenPresentedEvent()
    {
    }
    //Notifies the presented screen has been dismissed
    void BannerAdScreenDismissedEvent()
    {
    }
    //Invoked when the user leaves the app
    void BannerAdLeftApplicationEvent()
    {
    }


    //Invoked when the Interstitial is Ready to shown after load function is called
    void InterstitialAdReadyEvent()
    {
    }
    //Invoked when the Interstitial Ad Unit has opened
    void InterstitialAdOpenedEvent()
    {
    }


    //Invoked when the RewardedVideo ad view has opened.
    //Your Activity will lose focus. Please avoid performing heavy 
    //tasks till the video ad will be closed.
    void RewardedVideoAdOpenedEvent()
    {
    }
    //Invoked when the RewardedVideo ad view is about to be closed.
    //Your activity will now regain its focus.
    void RewardedVideoAdClosedEvent()
    {
    }
    //Invoked when there is a change in the ad availability status.
    //@param - available - value will change to true when rewarded videos are available. 
    //You can then show the video by calling showRewardedVideo().
    //Value will change to false when no videos are available.
    void RewardedVideoAvailabilityChangedEvent(bool available)
    {
        //Change the in-app 'Traffic Driver' state according to availability.
        bool rewardedVideoAvailability = available;
    }
    //  Note: the events below are not available for all supported rewarded video 
    //   ad networks. Check which events are available per ad network you choose 
    //   to include in your build.
    //   We recommend only using events which register to ALL ad networks you 
    //   include in your build.
    //Invoked when the video ad starts playing.
    void RewardedVideoAdStartedEvent()
    {
    }
    //Invoked when the video ad finishes playing.
    void RewardedVideoAdEndedEvent()
    {
    }



    public void HideUnityBanner()
    {
        Advertisement.Banner.Hide();
    }

    public void OnUnityAdsReady(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsDidError(string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        MyToast.mee.MyShowToastMethod("UnityAd Finish=" + placementId);

        if (placementId == "Rewarded_Android")
        {
            if (showResult == ShowResult.Finished)
            {
                if (localCallback != null)
                    localCallback.Invoke();
            }
        }
    }

    public void OnInitializationComplete()
    {
        // LoadUnityBanner();
        LoadUnityInterstitial();
        LoadUnityRewarded();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure");

        throw new NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart");

        //throw new NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("OnUnityAdsShowComplete");
        if (placementId == "Rewarded_Android")
        {
            if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                if (localCallback != null)
                    localCallback.Invoke();
            }
        }

        // throw new NotImplementedException();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsAdLoaded");

        // throw new NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new NotImplementedException();
    }

}
