using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;




public class JsonReader : MonoBehaviour
{
    public static JsonReader _instance;

    public string Json_CommonUrl = "https://script.googleusercontent.com/macros/echo?user_content_key=LkOZZD_IdGgt6tC1ZIL4Pyyo4E9Sehh2mNyU0JPlVHDtrk7tfsIRczTsFb81-nWlNcYX2lqLgggUvx9EP4rJobAzNJjVv9OZOJmA1Yb3SEsKFZqtv3DaNYcMrmhZHmUMWojr9NvTBuBLhyHCd5hHa1GhPSVukpSQTydEwAEXFXgt_wltjJcH3XHUaaPC1fv5o9XyvOto09QuWI89K6KjOu0SP2F-BdwUn42KP4JVVcqRT8b6Cf2UCwr9OYnKzHkYzGYdhRbN_jX_c47YFf-G_KNDS-Ujl6TgW0Ar8EAym28EaxhftIHqgE62M6ascyA-&lib=MnrE7b2I2PjfH799VodkCPiQjIVyBAxva";
    public string Json_MGameUrl = "https://script.googleusercontent.com/macros/echo?user_content_key=Nho0cmmNrxra3U4s2Fm7jTHYrzjAYDAIgiPuDX6Krf5uI0oxb9Vttv7d6b2Gn4ux4ZOpuO0Tlf_OLJ_SNLcYXI5qXJ6qjh78OJmA1Yb3SEsKFZqtv3DaNYcMrmhZHmUMWojr9NvTBuBLhyHCd5hHa1GhPSVukpSQTydEwAEXFXgt_wltjJcH3XHUaaPC1fv5o9XyvOto09QuWI89K6KjOu0SP2F-BdwUn42KP4JVVcqRT8b6Cf2UCwr9OYnKzHkYzGYdhRbN_jX_c47YFf-G_KNDS-Ujl6Tgzup67gDRLyJ22frI-XDRho2qrCO0PzE3&lib=MnrE7b2I2PjfH799VodkCPiQjIVyBAxva";
    public static bool IsNetAvailable = false;

    public void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    public void StartData()
    {
        StartCoroutine(DownloadGameData());

    }



    IEnumerator DownloadGameData()
    {
        string url = Json_MGameUrl;


        yield return new WaitForEndOfFrame();

        string downloadData = null;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                IsNetAvailable = false;
                Debug.Log("Download Error: " + webRequest.error);
                downloadData = PlayerPrefs.GetString("LastDataDownloaded", null);
                string versionText = PlayerPrefs.GetString("LastDataDownloaded", null);
                Debug.Log("Using stale data version: " + versionText);

                GamePromotion.mee.ShowMenuAd();
                //Application.LoadLevel(1);
            }
            else
            {
                IsNetAvailable = true;
                Debug.Log("Download Json success");
                Debug.Log("Data: " + webRequest.downloadHandler.text);

                string jsonString = webRequest.downloadHandler.text;

                // JSONObject jsonObject = JSONParser.getDataFromWeb();
                JsonData jsonvale = JsonMapper.ToObject(jsonString);

                //  Debug.Log(jsonvale[0][0]["mkey"]);
                for (int i = 0; i < jsonvale[0].Count; i++)
                {
                    Debug.Log(jsonvale[0][i]["mkey"] + "  == " + jsonvale[0][i]["mvalues"]);

                    if (jsonvale[0][i]["mkey"].ToString() == "needMenuAd")
                    {
                        SKAds.NeedMenuAd = jsonvale[0][i]["mvalues"].ToString();
                    }


                    if (jsonvale[0][i]["mkey"].ToString() == "needBannerAd")
                    {
                        SKAds.NeedBannerAd = jsonvale[0][i]["mvalues"].ToString();
                    }


                    if (jsonvale[0][i]["mkey"].ToString() == "forceOpen")
                    {
                        SKAds.forceOpen = jsonvale[0][i]["mvalues"].ToString();
                    }

                    if (jsonvale[0][i]["mkey"].ToString() == ("isinSale"))
                    {
                        SKAds.isinSale = jsonvale[0][i]["mvalues"].ToString();
                    }


                    if (jsonvale[0][i]["mkey"].ToString() == ("infomsg"))
                    {
                        SKAds.infomsg = jsonvale[0][i]["mvalues"].ToString();
                    }


                    if (jsonvale[0][i]["mkey"].ToString() == ("showads"))
                    {
                        SKAds.showads = jsonvale[0][i]["mvalues"].ToString();
                    }

                    if (jsonvale[0][i]["mkey"].ToString() == ("NeedRate"))
                    {
                        SKAds.NeedRate = jsonvale[0][i]["mvalues"].ToString();
                    }


                    //if (jsonvale[0][i]["mkey"].ToString() == "DiscPopAt")
                    //{
                    //    SKAds.DiscPopAt = int.Parse(jsonvale[0][i]["mvalues"].ToString());
                    //}

                    if (jsonvale[0][i]["mkey"].ToString() == "playbtn")
                    {
                        SKAds.Needplaybtn = (jsonvale[0][i]["mvalues"].ToString());
                    }

                    if (jsonvale[0][i]["mkey"].ToString() == "policylink")
                    {
                        SKAds.policylink = (jsonvale[0][i]["mvalues"].ToString());
                    }

                    if (jsonvale[0][i]["mkey"].ToString() == "ProirtyOrder")
                    {
                        SKAds.ProirtyOrder = (jsonvale[0][i]["mvalues"].ToString());
                    }
                    if (jsonvale[0][i]["mkey"].ToString() == "RewardProirtyOrder")
                    {
                        SKAds.Reward_ProirtyOrder = (jsonvale[0][i]["mvalues"].ToString());
                    }
                    if (jsonvale[0][i]["mkey"].ToString() == "DiscPopNums")
                    {
                        SKAds.DiscPopNums = (jsonvale[0][i]["mvalues"].ToString());
                    }


                    //if (SKAds.NeedRate == "yes")
                    //{
                    if (jsonvale[0][i]["mkey"].ToString() == "Ratenum")
                    {
                        SKAds.RateAtNum = int.Parse(jsonvale[0][i]["mvalues"].ToString());
                    }
                    //}
                    //  if (SKAds.showads == "yes")
                    //{
                    if (jsonvale[0][i]["mkey"].ToString() == "AddtoAddDelay")
                    {
                        SKAds.mee.Addtoadddelay = float.Parse(jsonvale[0][i]["mvalues"].ToString());
                        SKAds.MAddDelay = float.Parse(jsonvale[0][i]["mvalues"].ToString());

                    }

                    if (jsonvale[0][i]["mkey"].ToString() == "AddDelay_lc")
                    {
                        SKAds.AddDelay = float.Parse(jsonvale[0][i]["mvalues"].ToString());
                    }
                    if (jsonvale[0][i]["mkey"].ToString() == "AddDelay_ls")
                    {
                        SKAds.AddDelay_ls = float.Parse(jsonvale[0][i]["mvalues"].ToString());
                    }
                    if (jsonvale[0][i]["mkey"].ToString() == "AddDelay_lf")
                    {
                        SKAds.AddDelay_lf = float.Parse(jsonvale[0][i]["mvalues"].ToString());
                    }
                    //if (jsonvale[0][i]["mkey"].ToString() == "AddDelay_menu")
                    //{
                    //    SKAds.MAddDelay = float.Parse(jsonvale[0][i]["mvalues"].ToString());
                    //}
                    //}

                    //if (SKAds.NeedMenuAd == "yes")
                    // {
                    if (jsonvale[0][i]["mkey"].ToString() == "showexternalad")
                    {
                        SKAds.MenuAdUrl = jsonvale[0][i]["mvalues"].ToString();
                    }
                    if (jsonvale[0][i]["mkey"].ToString() == "showexternalad2")
                    {
                        SKAds.MenuAd2Url = jsonvale[0][i]["mvalues"].ToString();
                    }
                    if (jsonvale[0][i]["mkey"].ToString() == "externalimage")
                    {
                        SKAds.MenuAdImage = jsonvale[0][i]["mvalues"].ToString();
                    }
                    if (jsonvale[0][i]["mkey"].ToString() == "addtitle")
                    {
                        SKAds.addtitle = jsonvale[0][i]["mvalues"].ToString();
                    }

                    if (jsonvale[0][i]["mkey"].ToString() == "admobBanner")
                    {
                        SKAds.admobBanner = jsonvale[0][i]["mvalues"].ToString();
                    }
                    if (jsonvale[0][i]["mkey"].ToString() == "unityBanner")
                    {
                        SKAds.unityBanner = jsonvale[0][i]["mvalues"].ToString();
                    }
                    if (jsonvale[0][i]["mkey"].ToString() == "ironBanner")
                    {
                        SKAds.ironBanner = jsonvale[0][i]["mvalues"].ToString();
                    }
                    //  }
                }

                Debug.LogError("---------------------------");

                StartCoroutine(DownloadCommonGameData());
                if (SKAds.NeedMenuAd == "yes")
                {


                }
                else
                {
                    SKAds.mee.logo.SetActive(false);
                    Application.LoadLevel(1);

                    SKAds.mee.StartIntilizeAds();
                }





            }
        }


    }


    IEnumerator DownloadCommonGameData()
    {
        string url = Json_CommonUrl;


        yield return new WaitForEndOfFrame();

        string downloadData = null;
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {

            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log("Download Error: " + webRequest.error);
                downloadData = PlayerPrefs.GetString("LastDataDownloaded", null);
                string versionText = PlayerPrefs.GetString("LastDataDownloaded", null);
                Debug.Log("Using stale data version: " + versionText);
            }
            else
            {
                Debug.Log("Download Json successF " + SKAds.MenuAdUrl + " , " + SKAds.NeedMenuAd);
                // Debug.Log("Data: " + webRequest.downloadHandler.text);

                string jsonString = webRequest.downloadHandler.text;

                // JSONObject jsonObject = JSONParser.getDatanmnmjb 1v  Web();
                JsonData jsonvale = JsonMapper.ToObject(jsonString);

                //  Debug.Log(jsonvale[0][0]["mkey"]);
                if (SKAds.MenuAdUrl == "null")
                {
                    for (int i = 0; i < jsonvale[0].Count; i++)
                    {
                        Debug.Log("vall B" + jsonvale[0][i]["mkey"] + "  == " + jsonvale[0][i]["mvalues"]);


                        Debug.Log(jsonvale[0][i]["mkey"] + "  == " + jsonvale[0][i]["mvalues"]);

                        if (jsonvale[0][i]["mkey"].ToString() == "showexternalad")
                        {
                            SKAds.MenuAdUrl = jsonvale[0][i]["mvalues"].ToString();
                        }
                        if (jsonvale[0][i]["mkey"].ToString() == "showexternalad2")
                        {
                            SKAds.MenuAd2Url = jsonvale[0][i]["mvalues"].ToString();
                        }
                        if (jsonvale[0][i]["mkey"].ToString() == "externalimage")
                        {
                            SKAds.MenuAdImage = jsonvale[0][i]["mvalues"].ToString();
                        }
                        if (jsonvale[0][i]["mkey"].ToString() == "addtitle")
                        {
                            SKAds.addtitle = jsonvale[0][i]["mvalues"].ToString();
                        }
                    }

                }
                Debug.Log("Download Json success  " + SKAds.MenuAdImage);
                if (SKAds.NeedMenuAd == "yes")
                {

                    LoadImageData.mee.LoadImageNow(SKAds.MenuAdImage, SKAds.MenuAd_Image);
                    SKAds.mee.StartIntilizeAds();

                }
                //else
                //{


                //    SKAds.mee.logo.SetActive(false);
                //    Application.LoadLevel(1);
                //}

            }
        }


    }

}
