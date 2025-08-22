using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGSK;
public class LevelFailScript : MonoBehaviour
{
	public GameObject UnlockAllCarsBtn;
	void OnEnable()
	{
		CallAd ();
		//if (StorePageScript.allcarsunlocked == true) 
		//{
		//	UnlockAllCarsBtn.SetActive (false);
		//}
	}

	void CallAd()
	{
		
//		GameConfigs2018.mee.showRotationAds (MenuManager.CurrentLevel,AdsPageType.lf);
	}

}
