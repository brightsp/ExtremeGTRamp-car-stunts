using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePromotion : MonoBehaviour
{

	public GameObject[] GameInfo;
	public string[] menugameUrl;
	public GameObject GamePromotion_obj;
	public static GamePromotion mee;

	public Text AddTitleTxt;
	// Use this for initialization
	void Awake()
	{
		mee = this;
	}
	private string MenuadUrl;
	private int randomGame = 0;

	public void ShowMenuAd()
	{
		Debug.Log("##  w ---- Menu Ad " + JsonReader.IsNetAvailable);
		//MyToast.mee.MyShowToastMethod ("Show  "+BmData.NeedMenuAd);
		SKAds.mee.logo.SetActive(false);
		if (SKAds.NeedMenuAd == "yes")
		{



			GamePromotion_obj.SetActive(true);



			//GameInfo [3].SetActive (true);
			GameInfo[0].GetComponent<Image>().sprite = SKAds.MenuAd_Image;
			MenuadUrl = SKAds.MenuAdUrl;


			AddTitleTxt.text = SKAds.addtitle;


		}
		else
		{

			CallNextScene();
		}

		//if (JsonReader.IsNetAvailable)
		//{
		//    BmData.mee.AdInit();
		//    BmData.mee.InitIron();
		//    BmData.mee.InitUnityAds();
		//    Purchaser.mee.InitializePurchasing();

		//}

	}

	public void OpenUrl()
	{
		//	CloseMenuad ();
		GamePromotion_obj.SetActive(false);
		CallNextScene();
		Application.OpenURL(MenuadUrl);
		//Application.OpenURL ("market://details?id=com.brightmoon.megajumppro"); 


	}
	public void CloseMenuad()
	{
		GamePromotion_obj.SetActive(false);
		CallNextScene();
		if (SKAds.forceOpen == "yes")
		{
			Application.OpenURL(MenuadUrl);

		}


	}
	void CallNextScene()
	{
		//Debug.Log (BmData.mee.logo.name);
		SKAds.mee.logo.SetActive(false);

		Application.LoadLevel(1);

	}
	// Update is called once per frame
	void Update()
	{

	}




}
