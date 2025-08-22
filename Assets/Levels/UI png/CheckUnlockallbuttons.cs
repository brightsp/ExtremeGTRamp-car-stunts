using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUnlockallbuttons : MonoBehaviour {

	// Use this for initialization
	public enum Buttontype{unlockalllevels,unlockallvehicles,Unlockalllevelandvehicles};
	public Buttontype _Currentbutton;

	void OnEnable () 
	{
		CheckButtonStatus ();
	}
	
	// Update is called once per frame
	char[] chr;
	int count=0;
	public void CheckButtonStatus() 
	{
		count=0;
		if (_Currentbutton == Buttontype.unlockalllevels) 
		{
			if (PlayerPrefs.GetInt (Menupage.Levelsunlocked) >= 10) 
			{
				//gameObject.SetActive (false);
			}
		}



		if (_Currentbutton == Buttontype.unlockallvehicles) 
		{
			chr = PlayerPrefs.GetString (Menupage.Vehiclesunlocked).ToCharArray ();
			for (int i = 0; i < chr.Length; i++) 
			{
				if (chr [i] == '1')
					count++;	
			}

            if (count >= 20)
            {
                //gameObject.SetActive (false);
            }
		}



		if (_Currentbutton == Buttontype.Unlockalllevelandvehicles) 
		{


			chr = PlayerPrefs.GetString (Menupage.Vehiclesunlocked).ToCharArray ();
			for (int i = 0; i < chr.Length; i++) 
			{
				if (chr [i] == '1')
					count++;	
			}

			if (PlayerPrefs.GetInt (Menupage.Levelsunlocked) >= 10 && count>=20) 
			{
				//gameObject.SetActive (false);
			}
		}
	}
}
