using System.Threading.Tasks;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System.Collections;
using UnityEngine.Networking;

public class UnityRemoteConfig : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }
    public static string remoteDataStr = "";
    async Task InitializeRemoteConfigAsync()
    {
        // initialize handlers for unity game services
        await UnityServices.InitializeAsync();

        // remote config requires authentication for managing environment information
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }
    }

    async Task Start_Delay()
    {
        // initialize Unity's authentication and core services, however check for internet connection
        // in order to fail gracefully without throwing exception if connection does not exist
        if (Utilities.CheckForInternetConnection())
        {
            await InitializeRemoteConfigAsync();
        }

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
    }


    public bool isNETAVAILABLE = false;

    void Start()
    {

        StartCoroutine(CheckInternet((connected) =>
    {
        Debug.Log(connected ? " CHECK1  :: Internet Available" : "No Internet");

        isNETAVAILABLE = connected;

        if (isNETAVAILABLE)
        {
            Start_Delay();
        }
        else
        {
            SKAds.mee.logo.SetActive(false);
            Application.LoadLevel(1);

            SKAds.mee.StartIntilizeAds();
        }
    }));
    }




    public IEnumerator CheckInternet(System.Action<bool> callback)
    {
        using (UnityWebRequest request =
               UnityWebRequest.Head("https://www.google.com"))
        {
            request.timeout = 5;

            yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            bool connected = request.result == UnityWebRequest.Result.Success;
#else
            bool connected = !request.isNetworkError && !request.isHttpError;
#endif

            callback?.Invoke(connected);
        }
    }




    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        Debug.Log("Data: RemoteConfigService.Instance.appConfig fetched: " + RemoteConfigService.Instance.appConfig.config.ToString());
        remoteDataStr = RemoteConfigService.Instance.appConfig.config.ToString();


        JObject parsedJson = JObject.Parse(UnityRemoteConfig.remoteDataStr);


        foreach (var pair in parsedJson)
        {
            Debug.Log("uuu => Key: " + pair.Key + " | Value: " + pair.Value);

            // Debug.Log(jsonvale[0][i]["mkey"] + "  == " + jsonvale[0][i]["mvalues"]);

            if (pair.Key == "needMenuAd")
            {
                SKAds.NeedMenuAd = pair.Value.ToString();
            }


            if (pair.Key.ToString() == "needBannerAd")
            {
                SKAds.NeedBannerAd = pair.Value.ToString();
            }


            if (pair.Key.ToString() == "forceOpen")
            {
                SKAds.forceOpen = pair.Value.ToString();
            }



            if (pair.Key.ToString() == ("showads"))
            {
                SKAds.showads = pair.Value.ToString();
            }


            //----

            if (pair.Key.ToString() == ("NeedRate"))
            {
                SKAds.NeedRate = pair.Value.ToString();
            }
            if (pair.Key.ToString() == "Ratenum")
            {
                SKAds.RateAtNum = int.Parse(pair.Value.ToString());
            }





            if (pair.Key.ToString() == ("isPaidUser"))
            {
                SKAds.isPAID_USER = pair.Value.ToString();
            }

            if (pair.Key.ToString() == ("is_needSalePop"))
            {
                SKAds.isinSalePopup = pair.Value.ToString();
            }



            if (pair.Key.ToString() == ("subscribeUrl"))
            {
                SKAds.subscribeUrl = pair.Value.ToString();
            }


            //if (pair.Key.ToString() == "playbtn")
            //{
            //    SKAds.Needplaybtn = (pair.Value.ToString());
            //}

            //if (pair.Key.ToString() == "policylink")
            //{
            //    SKAds.policylink = (pair.Value.ToString());
            //}

            if (pair.Key.ToString() == "ProirtyOrder")
            {
                SKAds.ProirtyOrder = (pair.Value.ToString());
            }
            if (pair.Key.ToString() == "RewardProirtyOrder")
            {
                SKAds.Reward_ProirtyOrder = (pair.Value.ToString());
            }
            //if (pair.Key.ToString() == "DiscPopNums")
            //{
            //    SKAds.DiscPopNums = (pair.Value.ToString());
            //}


            //if (SKAds.NeedRate == "yes")
            //{

            //}
            //  if (SKAds.showads == "yes")
            //{
            if (pair.Key.ToString() == "AddtoAddDelay")
            {
                SKAds.mee.Addtoadddelay = float.Parse(pair.Value.ToString());
                SKAds.MAddDelay = float.Parse(pair.Value.ToString());

            }

            if (pair.Key.ToString() == "AddDelay_lc")
            {
                SKAds.AddDelay = float.Parse(pair.Value.ToString());
            }
            if (pair.Key.ToString() == "AddDelay_ls")
            {
                SKAds.AddDelay_ls = float.Parse(pair.Value.ToString());
            }
            if (pair.Key.ToString() == "AddDelay_lf")
            {
                SKAds.AddDelay_lf = float.Parse(pair.Value.ToString());
            }


            //if (SKAds.NeedMenuAd == "yes")
            // {
            if (pair.Key.ToString() == "showexternalad")
            {
                SKAds.MenuAdUrl = pair.Value.ToString();
            }
            //if (pair.Key.ToString() == "showexternalad2")
            //{
            //    SKAds.MenuAd2Url = pair.Value.ToString();
            //}
            if (pair.Key.ToString() == "externalimage")
            {
                SKAds.MenuAdImage = pair.Value.ToString();
            }
            if (pair.Key.ToString() == "addtitle")
            {
                SKAds.addtitle = pair.Value.ToString();
            }

            if (pair.Key.ToString() == "admobBanner")
            {
                SKAds.admobBanner = pair.Value.ToString();
            }
            if (pair.Key.ToString() == "unityBanner")
            {
                SKAds.unityBanner = pair.Value.ToString();
            }
            //if (pair.Key.ToString() == "ironBanner")
            //{
            //    SKAds.ironBanner = pair.Value.ToString();
            //}
            //  }
        }

        Debug.LogError("---------------------------" + SKAds.NeedMenuAd);

        if (SKAds.NeedMenuAd == "yes")
        {
            LoadImageData.mee.LoadImageNow(SKAds.MenuAdImage, SKAds.MenuAd_Image);
            SKAds.mee.StartIntilizeAds();

        }
        else
        {
            SKAds.mee.logo.SetActive(false);
            Application.LoadLevel(1);

            SKAds.mee.StartIntilizeAds();
        }




    }
}





