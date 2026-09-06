using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdTest : MonoBehaviour
{
    /*    public GameObject Sale_Popup;

        // Start is called before the first frame update

        void Start()
        {
            string isProUSer = PlayerPrefs.GetString("pro_USER");
            Debug.Log("Menu == isProUSer-> " + isProUSer + " ,isinSalePopup-> " + SKAds.isinSalePopup + " ,adsbuyed-> " + PlayerPrefs.GetString("adsbuyed"));

            if (isProUSer == "no" && SKAds.isinSalePopup == "yes" && PlayerPrefs.GetString("adsbuyed") == "no")
            {
                Sale_Popup.SetActive(true);
            }
        }*/
    public void Menu()
    {
        SKAds.mee.showBannerAd();





    }

    public void LC()
    {
        UnityAnalyticsManager.instance.CustomEvent(SKAds.complete_anlytics, PlayerPrefs.GetInt("currentLevel"));
        SKAds.mee.Show_Ad(SKAds.AddDelay, "LC");
    }
    public void LF()
    {
        UnityAnalyticsManager.instance.CustomEvent(SKAds.fail_anlytics, PlayerPrefs.GetInt("currentLevel"));
        SKAds.mee.Show_Ad(SKAds.AddDelay_lf, "LF");
    }

    public void rewardCoins()
    {
        SKAds.mee.ShowReward_order(SKAds.RewardType_enum.Coins_reward);
    }
    public void rewardHealth()
    {
        SKAds.mee.ShowReward_order(SKAds.RewardType_enum.Health_reward);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
