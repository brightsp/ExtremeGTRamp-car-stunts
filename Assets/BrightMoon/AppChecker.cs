using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AppChecker : MonoBehaviour {

	public static AppChecker mee;
	public static string GiftGamePackage="com.brightmoon.rushhour3";
	public static string GiftGameName="Rush Hour 3";
	// Use this for initialization
	void Awake () {
		mee = this;

		if(PlayerPrefs.GetString ("GiftGame_ex")=="yes"){
			PlayerPrefs.SetString ("GiftInstall","nottried");


		}

		ClaimGift ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void ClaimGift(bool NeedDialog=false){
		

		//NativeDialog dialog = new NativeDialog("TheAppGuruz", "Do you wants to know about TheAppGuruz");

		//AndroidDialog dialog = AndroidDialog.Create ("Claim Gift", "Install the app " + GiftGameName + " and get reward 1000 Gems");
		//dialog.init();
	//	dialog.
	//	AndroidDialog.onDialogPopupComplete += OnDialogPopupComplete;
		//dialog.onDialogPopupComplete += OnDialogPopupComplete;
	//	dialog.ActionComplete += OnDialogClose;

		//dialog.onDialogPopupComplete += OnDialogClose;

	}

//	private void OnDialogClose(MessageState result) {

//		switch (result)
//		{
//		case MessageState.YES:
//			Debug.Log("Yes button pressed");
//			break;
//		case MessageState.NO:
//			Debug.Log("No button pressed");
//			break;
//		}
////
////		//parsing result
////		switch(result) {
////		case AndroidDialogResult.YES:
////			Application.OpenURL ("market://details?id=" + GiftGamePackage);
////			PlayerPrefs.SetString ("Giftpackage", GiftGamePackage);
////			PlayerPrefs.SetString ("GiftInstall", "tried");
////			//StartCoroutine (CheckAppinstalled(1000));
////
////			break;
////		case AndroidDialogResult.NO:
////			PlayerPrefs.SetString ("GiftInstall","nottried");
////			break;
////
////		}
//	}




//	private void OnDialogCloseExternal(AndroidDialogResult result) {
//
//		//parsing result
//		switch(result) {
//		case AndroidDialogResult.YES:
//			PlayerPrefs.SetString ("GiftInstallExternal", "tried");
//			StartCoroutine (CheckAppinstalledExternal(PlayerPrefs.GetString ("GiftpackageExternal")));
//			PlayerPrefs.SetString ("GiftpackageExternal", aa);
//			Application.OpenURL ("market://details?id=" + bb[0]);
//
//
//			break;
//		case AndroidDialogResult.NO:
//			PlayerPrefs.SetString ("GiftInstallExternal","nottried");
//			break;
//
//		}
//	}


	private string[] bb;
	private string aa;



	public void crossCheckExternal(string PackageIDD){
		 aa=PackageIDD;
		bb = aa.Split ('_');
		PlayerPrefs.SetString ("Giftpackage", bb[0]);
		StartCoroutine( ExternalCheckAppinstalled (-1));
	}


	public IEnumerator CheckAppinstalledExternal(string PackageIDD){
		yield return new WaitForSeconds (1f);
		PlayerPrefs.SetString ("GiftpackageExternal", PackageIDD);
		aa=PackageIDD;
		bb = aa.Split ('_');
		PlayerPrefs.SetString ("Giftpackage", bb[0]);
		StartCoroutine( ExternalCheckAppinstalled (int.Parse(bb[1])));
	}

	public IEnumerator ExternalCheckAppinstalled(int coinns){
		yield return new WaitForSeconds (0.5f);
		if (coinns <= 0) {
			if (IsAppInstalled (PlayerPrefs.GetString ("Giftpackage"))) {
			//	AndroidToast.ShowToastNotification (("App already installed..Thank you!"), AndroidToast.LENGTH_LONG);
				PlayerPrefs.SetString ("GiftInstallExternal", "done");

			} else {
				PlayerPrefs.SetString ("GiftInstallExternal", "nottried");

				//AndroidDialog dialog = AndroidDialog.Create ("Claim the offer", "Install the app " + bb [2] + " and get reward " + bb [1] + " Gems");
				//dialog.ActionComplete += OnDialogCloseExternal;
			}
		} 

		else {


			if (IsAppInstalled (PlayerPrefs.GetString ("Giftpackage"))) {


				PlayerPrefs.SetString ("GiftInstallExternal", "done");

				//CoinManager.Instance.AddCoins (coinns);
				//Menu.mee.GoldTxt.text = CoinManager.Instance.Coins.ToString ();
				//AndroidToast.ShowToastNotification (("Added " + coinns + "gems reward for downloading app"), AndroidToast.LENGTH_LONG);

			} else {

				PlayerPrefs.SetString ("GiftInstallExternal", "notdone");

				//AndroidToast.ShowToastNotification (("App is not installed ! claim reward after installing the app "), AndroidToast.LENGTH_LONG);

			}
		}
	}

	public bool CheckGiftObj(){

		bool aaa = false;
		if (IsAppInstalled (GiftGamePackage)) {
			aaa = true;
		} else {
			aaa =false;
		}
		//AndroidDialog dialog = AndroidDialog.Create ("Onenable", ("test"+aaa));

		//AndroidToast.ShowToastNotification (("App is installed ? "+aaa), AndroidToast.LENGTH_LONG);

		return aaa;
	}
	public IEnumerator CheckAppinstalled(int coinns){
		yield return new WaitForSeconds (1);
		if (IsAppInstalled (PlayerPrefs.GetString ("Giftpackage"))) {

			PlayerPrefs.SetString ("Giftpackage", "done");
			PlayerPrefs.SetString ("GiftInstall","done");

			//CoinManager.Instance.AddCoins (coinns);
			//Menu.mee.giftbox.SetActive (false);
			//Menu.mee.GoldTxt.text = CoinManager.Instance.Coins.ToString();
			//AndroidToast.ShowToastNotification (("Added 1000 gems reward for downloading app"), AndroidToast.LENGTH_LONG);

		} else {

			PlayerPrefs.SetString ("GiftInstall","nottried");
			//AndroidToast.ShowToastNotification (("App is not installed ! claim reward after installing the app "), AndroidToast.LENGTH_LONG);

			//AndroidToast.ShowToastNotification (("is installed " + AppChecker.mee.IsAppInstalled ("com.brightmoon.rushhour3")), AndroidToast.LENGTH_LONG);
		}
	}

	public  bool IsAppInstalled(string bundleID){
		#if UNITY_ANDROID
		AndroidJavaClass up = new AndroidJavaClass ("com.unity3d.player.UnityPlayer");
		AndroidJavaObject ca = up.GetStatic<AndroidJavaObject> ("currentActivity");
		AndroidJavaObject packageManager = ca.Call<AndroidJavaObject> ("getPackageManager");
		Debug.Log (" ********LaunchOtherApp ");
		AndroidJavaObject launchIntent = null;
		//if the app is installed, no errors. Else, doesn't get past next line
		try {
			launchIntent = packageManager.Call<AndroidJavaObject> ("getLaunchIntentForPackage", bundleID);
			//        
			//        ca.Call("startActivity",launchIntent);
		} catch (Exception ex) {
			Debug.Log ("exception" + ex.Message);
		}
		if (launchIntent == null)
			return false;
		return true;
		#else
		return false;
		#endif
	}
}
