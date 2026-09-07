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

    public void SubScribECallBack()
    {
        Menupage.Addcash(1000);
        MyToast.mee.MyShowToastMethod("1000 coins added for subscription");
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

        if (SKAds.mee.CurrentRewardType == SKAds.RewardType_enum.Car_reward)
        {
            Menupage.Initialzedata();
            char[] unlockedVehicles = PlayerPrefs.GetString(Menupage.Vehiclesunlocked).ToCharArray();
            if (unlockedVehicles.Length > 1)
            {
                unlockedVehicles[1] = '1';
                PlayerPrefs.SetString(Menupage.Vehiclesunlocked, new string(unlockedVehicles));
            }
            MyToast.mee.MyShowToastMethod("Car Unlocked");

        }
    }

    public void InAppPurchaseSuccess(string InappData)
    {




        if (InappData == SKAds.mee.StoreIds[0])
        {
            MyToast.mee.MyShowToastMethod("NO ADS BUY SUCCESS");
            PlayerPrefs.SetString("adsbuyed", "yes");

        }

        if (InappData == SKAds.mee.StoreIds[1])
        {
            MyToast.mee.MyShowToastMethod("NO ADS BUY SUCCESS");
            PlayerPrefs.SetString(SKAds.adsBuy_pref, "yes");
        }

        if (InappData == SKAds.mee.StoreIds[2])
        {
            //--unlock all cars
            Menupage.UnlockallVehicles();
            MyToast.mee.MyShowToastMethod("Unlock All Cars Success");
            if (Upgradepage.Obj != null)
            {
                Upgradepage.Obj.CheckVehicle();
            }
        }


        if (InappData == SKAds.mee.StoreIds[3])
        {
            //--unlock all cars
            Menupage.UnlockallVehicles();
            PlayerPrefs.SetString(SKAds.adsBuy_pref, "yes");
            MyToast.mee.MyShowToastMethod("unlock all cars + No Ads success");
            if (Upgradepage.Obj != null)
            {
                Upgradepage.Obj.CheckVehicle();
            }
        }


        if (InappData == SKAds.mee.StoreIds[4])
        {
            //--unlock all levels
            Menupage.UnlockallLevels();
            MyToast.mee.MyShowToastMethod("Unlock All Levels Success");


        }
        if (InappData == SKAds.mee.StoreIds[5])
        {
            //--unlock all levels
            Menupage.UnlockallLevels();
            MyToast.mee.MyShowToastMethod("Unlock All Levels Success");


        }


        if (InappData == SKAds.mee.StoreIds[6])
        {
            //--unlock all levels + cars
            Menupage.UnlockallLevels();
            Menupage.UnlockallVehicles();
            MyToast.mee.MyShowToastMethod("Unlock All Cars + Levels Success");
            if (Upgradepage.Obj != null)
            {
                Upgradepage.Obj.CheckVehicle();
            }


        }


    }
}
