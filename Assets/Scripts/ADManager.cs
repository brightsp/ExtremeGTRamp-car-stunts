using UnityEngine;
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
    public string ADMOB_bannerID, ADMOB_interstitialID, ADMOB_rewardedVideoID, IRONSOURCE_AppKey, UNITY_Key;
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
        InitilizeIronSource();
        InitilizeUnityAd();
    }
    private void InitilizeAdmob()
    {
        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        MobileAds.Initialize(HandleInitCompleteAction);
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

    void InitilizeIronSource()
    {
        IronSourceConfig.Instance.setClientSideCallbacks(true);
        IronSource.Agent.validateIntegration();
        IronSource.Agent.setUserId(SystemInfo.deviceUniqueIdentifier);
        IronSource.Agent.init(IRONSOURCE_AppKey);
        IronSource.Agent.loadInterstitial();
        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent;
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
    }
    void InitilizeUnityAd()
    {
        Advertisement.Initialize(UNITY_Key, false, this);
    }
    public void ShowAdmobRe()
    {
        ShowAdmobRewardedVideo(null);
    }
    public void ShowIRRe()
    {
        ShowIronsourceRewarded(null);
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
        LoadUnityBanner();
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
        Advertisement.Load("Interstitial_Android");
    }

    // Show the loaded content in the Ad Unit: 
    public void ShowUnityInterstitialAd()
    {
        if (PlayerPrefs.GetInt("RemoveAD") == 0)
        {

            Advertisement.Show("Interstitial_Android");

            Advertisement.Load("Interstitial_Android");

        }
    }
    private void LoadUnityRewarded()
    {
        Advertisement.Load("Rewarded_Android");
    }
    public void ShowUnityRewardedVideo(UnityAction callback)
    {

        Advertisement.Show("Rewarded_Android");

        Advertisement.Load("Rewarded_Android");

    }
    public void RequestAdmobBanner(string adPoss)
    {
        if (bannerView != null)
            return;

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

        this.bannerView = new BannerView(ADMOB_bannerID, AdSize.Banner, newPos);
        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);
        this.bannerView.Hide();

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
        this.interstitial = new InterstitialAd(ADMOB_interstitialID);

        // Called when an ad request has successfully loaded.
        this.interstitial.OnAdLoaded += HandleInterstitialOnAdLoaded;
        // Called when an ad request failed to load.
        this.interstitial.OnAdFailedToLoad += HandleInterstitialOnAdFailedToLoad;
        // Called when an ad is shown.
        this.interstitial.OnAdOpening += HandleInterstitialOnAdOpened;
        // Called when the ad is closed.
        this.interstitial.OnAdClosed += HandleInterstitialOnAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void ShowAdmobInterstitial()
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
            RequestAdmobInterstitial();
        }
        else
        {
            RequestAdmobInterstitial();
        }
    }
    public void RequestAdmobRewardBasedVideo()
    {
        this.rewardedAd = new RewardedAd(ADMOB_rewardedVideoID);

        // Called when an ad request has successfully loaded.
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }


    public bool isRewardedVideoAvailable()
    {
        return rewardedAd.IsLoaded();
    }
    public void ShowAdmobRewardedVideo(UnityAction callback)
    {
        if (rewardedAd.IsLoaded())
        {
            localCallback = callback;
            rewardedAd.Show();
        }
        else
        {
            RequestAdmobRewardBasedVideo();
        }
    }
    public void ShowIronsourceBanner(string adPoss = "tm")
    {
        if (IronBannerLoaded == true)
        {
            return;
        }
        IronSourceBannerPosition newPos = new IronSourceBannerPosition();
        if (adPoss == "tl")
        {
            newPos = IronSourceBannerPosition.TOP;
        }
        if (adPoss == "tm")
        {
            newPos = IronSourceBannerPosition.TOP;
        }
        if (adPoss == "tr")
        {
            newPos = IronSourceBannerPosition.TOP;
        }
        if (adPoss == "bl")
        {
            newPos = IronSourceBannerPosition.BOTTOM;
        }
        if (adPoss == "bm")
        {
            newPos = IronSourceBannerPosition.BOTTOM;
        }
        if (adPoss == "br")
        {
            newPos = IronSourceBannerPosition.BOTTOM;
        }

        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, newPos);
        IronSource.Agent.displayBanner();
    }
    public void HideIronsourceBanner()
    {
        IronSource.Agent.hideBanner();
    }
    public void ShowIronsourceInterstitial()
    {
        if (IronSource.Agent.isInterstitialReady())
        {
            IronSource.Agent.showInterstitial();
        }
        else
        {
            IronSource.Agent.loadInterstitial();
        }

    }
    public void ShowIronsourceRewarded(UnityAction callback)
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            localCallback = callback;
            IronSource.Agent.showRewardedVideo();
        }
    }
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        string[] val = SKAds.admobBanner.Split('_');

        RequestAdmobBanner(val[1]);
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.ToString());
    }

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

    public void HandleInterstitialOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        RequestAdmobInterstitial();
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.ToString());
    }

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

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: "
                             + args.ToString());
    }

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
    //Invoked when the banner loading process has failed.
    //@param description - string - contains information about the failure.
    void BannerAdLoadFailedEvent(IronSourceError error)
    {
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

    //Invoked when the initialization process has failed.
    //@param description - string - contains information about the failure.
    void InterstitialAdLoadFailedEvent(IronSourceError error)
    {
        if (!IronSource.Agent.isInterstitialReady())
            IronSource.Agent.loadInterstitial();
    }
    //Invoked right before the Interstitial screen is about to open.
    void InterstitialAdShowSucceededEvent()
    {
    }
    //Invoked when the ad fails to show.
    //@param description - string - contains information about the failure.
    void InterstitialAdShowFailedEvent(IronSourceError error)
    {
    }
    // Invoked when end user clicked on the interstitial ad
    void InterstitialAdClickedEvent()
    {
    }
    //Invoked when the interstitial ad closed and the user goes back to the application screen.
    void InterstitialAdClosedEvent()
    {
        if (!IronSource.Agent.isInterstitialReady())
            IronSource.Agent.loadInterstitial();
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
    //Invoked when the user completed the video and should be rewarded. 
    //If using server-to-server callbacks you may ignore this events and wait for the callback from the  ironSource server.
    //
    //@param - placement - placement object which contains the reward data
    //
    void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
    {
        if (localCallback != null)
            localCallback.Invoke();
    }
    //Invoked when the Rewarded Video failed to show
    //@param description - string - contains information about the failure.
    void RewardedVideoAdShowFailedEvent(IronSourceError error)
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
        LoadUnityBanner();
        LoadUnityInterstitial();
        LoadUnityRewarded();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        throw new NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        throw new NotImplementedException();
    }
}
