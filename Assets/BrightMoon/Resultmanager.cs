using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resultmanager : MonoBehaviour
{
    public static Resultmanager mee;
    // Use this for initialization
    void Start()
    {
        mee = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void coinsVideoReward()
    {
        MyToast.mee.MyShowToastMethod("Coins_reward");

    }

    public void healthVideoReward()
    {
        MyToast.mee.MyShowToastMethod("Health_reward");

    }
    public void VideoReward()
    {

        if (SKAds.mee.CurrentRewardType == SKAds.RewardType_enum.Coins_reward)
        {
            MyToast.mee.MyShowToastMethod("Coins_reward");
            //GameManager.instance.OnRewardedCoin(200);

        }

        if (SKAds.mee.CurrentRewardType == SKAds.RewardType_enum.Health_reward)
        {
            MyToast.mee.MyShowToastMethod("Health_reward");

        }
    }

    public void InAppPurchaseSuccess(string InappData)
    {


        PlayerPrefs.SetString("adsbuyed", "yes");

        if (InappData == SKAds.mee.StoreIds[0])
        {
            MyToast.mee.MyShowToastMethod("Success");

        }

        if (InappData == SKAds.mee.StoreIds[1])
        {
            MyToast.mee.MyShowToastMethod("Noads + 1000 coins added");
        //    GameManager.instance.OnRewardedCoin(1000);
        }

        if (InappData == SKAds.mee.StoreIds[2])
        {
            MyToast.mee.MyShowToastMethod("10,000 Coins Added");
        //    GameManager.instance.OnRewardedCoin(10000);
        }


        //if (InappData == BmData.mee.Subscribe_StoreIds[0])
        //{
        //    MyToast.mee.MyShowToastMethod("sub suces 0");
        //    DiscountPopHandler._instance.Close();
        //}

    }
}
