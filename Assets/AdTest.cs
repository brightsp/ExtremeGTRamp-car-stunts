using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdTest : MonoBehaviour
{
    // Start is called before the first frame update
    public void Menu()
    {
        SKAds.mee.showBannerAd();
    }

    public void LC()
    {
        SKAds.mee.Show_Ad(SKAds.AddDelay,"LC");
    }
    public void LF()
    {
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
