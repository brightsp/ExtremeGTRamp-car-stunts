using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPurchaser : MonoBehaviour {
	public Button Mybutton;
	public Text Pricetxt;
	public int productIndex;
	public bool NeedPrice = true;
	public bool SubscribeBtn = false;
	// Use this for initialization
	void Start () {
		//MyToast.mee.MyShowToastMethod (" prices :  "+Purchaser.IdPrices.Count);
		if (NeedPrice)
		{
			Pricetxt.text = "" + Purchaser.IdPrices[productIndex];
		}
	}
	
	// Update is called once per frame
	public void BuyItem () {
		//	MyToast.mee.MyShowToastMethod (" prices :  "+Purchaser.IdPrices.Count);
		//  Pricetxt.text = "" + Purchaser.IdPrices[productIndex];
		if (SubscribeBtn == false)
		{
			Purchaser.mee.BuyConsumableItem(productIndex);
		}
		else
		{
			Purchaser.mee.BuySubscription(productIndex);

		}
	}

	public void SUbscribeUrl()
	{
			Application.OpenURL(SKAds.subscribeUrl);
		Invoke(nameof(SUbscribeReward), 2f);
    }

	void SUbscribeReward()
	{
		if (PlayerPrefs.HasKey("urlSubscribe") == false)
		{
			PlayerPrefs.SetString("urlSubscribe", "yes");
			Resultmanager.mee.SubScribECallBack();
		}
		else
		{
			MyToast.mee.MyShowToastMethod("Already Subscribed");
        }
    }
}
