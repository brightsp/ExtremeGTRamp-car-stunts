using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.XPath;
using UnityEngine;
using UnityEngine.Events;

public class SKAds : MonoBehaviour
{
    public static SKAds mee;

    public GameObject logo;
    public string[] StoreIds;
    public string[] Subscribe_StoreIds;

    public RewardType_enum CurrentRewardType;
    internal static string addtitle;
    internal static string forceOpen;
    internal static string isPAID_USER;
    internal static string isinSalePopup;
    internal static string infomsg;
    internal static string showads;
    internal static string policylink;

    public static string adsBuy_pref = "adsbuyed";

    public static string NeedMenuAd { get; internal set; }
    public static Sprite MenuAd_Image { get; internal set; }
    public static string MenuAdUrl { get; internal set; }
    public static string MenuAd2Url { get; internal set; }
    public static string NeedBannerAd { get; internal set; }
    public static string NeedRate { get; internal set; }
    public static string subscribeUrl { get; internal set; }
    public static string Needplaybtn { get; internal set; }
    public static string ProirtyOrder { get; internal set; }
    public static string Reward_ProirtyOrder { get; internal set; }
    public static string DiscPopNums { get; internal set; }
    public static int RateAtNum = 100;
    public float Addtoadddelay { get; internal set; }
    public static float MAddDelay { get; internal set; }
    public static float AddDelay { get; internal set; }
    public static float AddDelay_ls { get; internal set; }
    public static float AddDelay_lf { get; internal set; }
    public static string MenuAdImage { get; internal set; }

    public static string admobBanner = "yes_tm";
    public static string ironBanner;
    public static string unityBanner;

    public string[] AdType { get; private set; }
    public string[] Reward_AdType { get; private set; }

    public static string complete_anlytics = "level_complete";
    public static string fail_anlytics = "level_fail";
    public static string start_anlytics = "level_start";
    public static string noadsOffer_anlytics = "noads_offer";

    public enum RewardType_enum
    {
        Coins_reward,
        Health_reward,
        Car_reward,
        ResetCar

    }

    // Start is called before the first frame update
    void Awake()
    {
        mee = this;
    }
    private void Start()
    {
        JsonReader._instance.StartData();


    }


    internal void StartIntilizeAds()
    {

        if (PlayerPrefs.HasKey("Mfrstinstall") == false)
        {
            Debug.Log("<-- FirstTime Install --> ");

            PlayerPrefs.SetString("Mfrstinstall", "done");
            PlayerPrefs.SetString("rated", "false");

            if (isPAID_USER == "yes")
            {
                PlayerPrefs.SetString("pro_USER", "yes");
                PlayerPrefs.SetString(SKAds.adsBuy_pref, "yes");
            }
            else
            {
                PlayerPrefs.SetString("pro_USER", "no");
                PlayerPrefs.SetString(SKAds.adsBuy_pref, "no");

            }

        }
        isProUSer = PlayerPrefs.GetString("pro_USER");
        Debug.Log("Menu aaa == isProUSer-> " + isProUSer + " ,isinSalePopup-> " + SKAds.isinSalePopup + " ,adsbuyed-> " + PlayerPrefs.GetString("adsbuyed"));


        Purchaser.mee.InitializePurchasing();

        Invoke("CallAfterLoad", 6);

    }


    private string isProUSer = "no";
    void CallAfterLoad()
    {
        Debug.Log("Afterload");

        Debug.Log("@ show daily reawrds");

        AdType = ProirtyOrder.Split('_');
        // _DiscPopAt = DiscPopNums.Split('_');
        Reward_AdType = Reward_ProirtyOrder.Split('_');

    }

    string Ad_Mpage = "LS";
    private int LS_TempCount = 0;
    private int TempCount = 0;
    private bool CanShowAd = true;
    private int CCount = 0;

    public void Show_Ad(float Ad_delay = 0.1f, string Ad_page = "LC")
    {
        Ad_Mpage = Ad_page;
        //Debug.LogError("show add "+ TempCount +"::"+ DiscPopAt);
        if (Ad_delay >= 0.1f)
        {
            Invoke("CallMyAds", Ad_delay);
        }

        if (Ad_page == "LF")
        {
            LS_TempCount++;
        }

    }

    public void CallMyAds()
    {
        if (Ad_Mpage == "LC")
        {
            TempCount++;
        }
        Debug.Log(Addtoadddelay);
        if (NeedRate == "yes" && TempCount == RateAtNum && PlayerPrefs.GetString("rated") == "false" && Ad_Mpage == "LC")
        {

            RateManager.instance.showRatePopup();
            //MyToast.mee.MyShowToastMethod ("RateAtNum :  "+RateAtNum);

            // RatePopup.SetActive(true);
            // PlatformDialog.SetButtonLabel("Yes", "No");
            // PlatformDialog.Show(
            //     "Rate US",
            //     "Like this game?, Please rate to support future updates!",
            //     PlatformDialog.Type.OKCancel,
            //     () =>
            //     {
            //         Debug.Log("Yes");
            //         Application.OpenURL("market://details?id=" + Application.identifier);
            //     },
            //     () =>
            //     {
            //         Debug.Log("No");
            //     }
            // );
        }


        else if (isProUSer == "no")
        {
            if (showads == "yes" && CanShowAd == true && PlayerPrefs.GetString(SKAds.adsBuy_pref) == "no")
            {
                CCount++;
                Addtoadddelay = MAddDelay;

                CanShowAd = false;

                ProrityAds();
            }
        }
    }
    private int InPrority = 0;
    void ProrityAds()
    {
        //MyToast.mee.MyShowToastMethod("Proirity Ad :  " + AdType[InPrority]);
        try
        {
            switch (AdType[InPrority])
            {
                case "A":
                    Debug.Log("Admob ad");
                    ADManager.Instance.ShowAdmobInterstitial();
                    break;

                case "I":
                    Debug.Log("Iron ad");
                    //   ADManager.Instance.ShowIronsourceInterstitial();
                    break;


                case "U":
                    Debug.Log("Unity ad");
                    ADManager.Instance.ShowUnityInterstitialAd();
                    break;


            }
        }
        catch (AndroidJavaException e)
        {
            Debug.Log("error came " + e);
        }

        InPrority++;
        if (InPrority >= ProirtyOrder.Length)
        {
            InPrority = 0;
        }
    }


    public static int Reward_InPrority = 0;

    public void ShowReward_order(RewardType_enum R_type, Action SuccesReward = null)
    {
        CurrentRewardType = R_type;
        UnityAction callBackFun = new UnityAction(Resultmanager.mee.VideoReward);


        if (SuccesReward == null)
        {
            callBackFun = Resultmanager.mee.VideoReward;
        }
        else
        {
            callBackFun = new UnityAction(SuccesReward);
        }


        try
        {
            switch (Reward_AdType[Reward_InPrority])
            {
                case "A":
                    Debug.Log("Reward Admob ad");
                    ADManager.Instance.ShowAdmobRewardedVideo(callBackFun);// Resultmanager.mee.VideoReward);
                    //Show_Admob_reward(R_type);
                    break;

                case "I":
                    Debug.Log("Reward Iron ad");
                    //  ADManager.Instance.ShowIronsourceRewarded(Resultmanager.mee.VideoReward);

                    break;


                case "U":
                    Debug.Log("Reward Unity ad");
                    ADManager.Instance.ShowUnityRewardedVideo(callBackFun);// Resultmanager.mee.VideoReward);

                    break;
            }
        }
        catch (AndroidJavaException e)
        {
            Debug.Log("error came " + e);
        }

        Reward_InPrority++;
        if (Reward_InPrority >= Reward_ProirtyOrder.Length)
        {
            Reward_InPrority = 0;
        }
    }

    public void showBannerAd()
    {
        if (NeedBannerAd == "yes")
        {
            string[] val = admobBanner.Split('_');
            if (val[0] == "yes")
            {
                ADManager.Instance.RequestAdmobBanner(val[1]);
            }

            // val = ironBanner.Split('_');
            //if (val[0] == "yes")
            //{
            //    ADManager.Instance.ShowIronsourceBanner(val[1]);
            //}

            val = unityBanner.Split('_');
            if (val[0] == "yes")
            {
                ADManager.Instance.ShowUnityBanner(val[1]);
            }

        }
    }


    void Update()
    {
        if (Addtoadddelay >= 0)
        {
            Addtoadddelay -= Time.deltaTime;
        }

        if (Addtoadddelay <= 0)
        {
            CanShowAd = true;
        }
    }
}
