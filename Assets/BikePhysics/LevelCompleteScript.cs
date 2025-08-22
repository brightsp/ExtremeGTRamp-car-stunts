using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteScript : MonoBehaviour 
{
	public static LevelCompleteScript mee;
	public GameObject UnlockAllLevelsBtn;
	void Awake()
	{
		mee = this;
	}

	void OnEnable()
	{
		//if (StorePageScript.allevelsunlocked == true) 
		//{
		//	UnlockAllLevelsBtn.SetActive (false);
		//}
	}


}
