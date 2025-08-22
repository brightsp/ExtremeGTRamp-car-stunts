using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menupage : MonoBehaviour {

	// Use this for initialization

	public static Menupage Obj;
	public GameObject Title,Playbtn_tween, Fadeobj;
	public static string Levelsunlocked="Levelsunlocked";
	public static string Vehiclesunlocked="Vehiclesunlocked";
	public static string Totalcash="Totalcash";
	public static string Resumegameunlimited="Resumegameunlimited";

	public static bool Isfromelevelfail=false;
	public static bool Isfromlevelcomplete=false;

	public AudioSource Menuaudiosouce;
	public AudioClip Menuaudioclip;
	void Awake () 
	{
		Obj = this;
		Initialzedata ();
		CheckCash ();
	}

    public List<int> StoredIndex;
    public List<string> Dataa;
    void Test()
    {
        string Val = "111010";
        string Dataa = Val;
        StoredIndex = new List<int>();
        for (int i = 0; i < Val.Length; i++)
        {
            if (Val[i] == '0')
            {
                StoredIndex.Add(i);

            }


        }


        int storerndm = Random.Range(0, Mathf.Clamp(StoredIndex.Count,0,10000));

        Dataa = Dataa.Remove(StoredIndex[storerndm], 1);
        Dataa = Dataa.Insert(StoredIndex[storerndm], "1");

     

      //sr  Debug.Log(randoMVal + "rnadom val to data string " + Dataa);
    }
	void Start()
	{



      //  Test();


        Menuaudiosouce.clip = Menuaudioclip;
		Menuaudiosouce.loop = true;
		Menuaudiosouce.Play ();
		
		if (Isfromlevelcomplete) 
		{
            //ShowLevelSelection ();
            Levelselection.Obj.ShowUpgradepagenow();
            Hidepage ();
		}
		if (Isfromelevelfail) 
		{
			Levelselection.Obj.ShowUpgradepagenow ();
		
			Hidepage ();
		}

		
		
		

		Isfromelevelfail = false;
		Isfromlevelcomplete = false;


		//if ( AdManager.instance)
		// AdData.mee.Show_Ad(AdData.MAddDelay);
		// AdData.mee.ShowBanner();

		//sr AdManager.instance.RunActions(AdManager.PageType.Menu, Levelselection.Currentlevel);
		//}
		Invoke("Playtween",2.2f);//3.5
	}
void Playtween()
	{
		iTween.ScaleTo(Title, iTween.Hash("x", 1.02f, "y", 1.02f, "z", 1.02f, "easeType", iTween.EaseType.linear, "loopType", "pingPong", "time", 1.1f));

		// iTween.MoveTo(Playbtn_tween, iTween.Hash("x", Playbtn_tween.transform.position.x+290f, "easeType", iTween.EaseType.linear, "loopType", "loop", "time", 3));
		
	}

	public void Playmenusound()
	{

		Menuaudiosouce.clip = Menuaudioclip;
		Menuaudiosouce.loop = true;
		Menuaudiosouce.volume=1;

		if(!Menuaudiosouce.isPlaying)
		Menuaudiosouce.Play ();

	}

	public void Playupgradesound()
	{

		Menuaudiosouce.clip = Menuaudioclip;
		Menuaudiosouce.loop = true;
		Menuaudiosouce.volume=0.6f;
		if(!Menuaudiosouce.isPlaying)
		Menuaudiosouce.Play ();
	}

	public void PlayLevelselectionsound()
	{

		Menuaudiosouce.clip = Menuaudioclip;
		Menuaudiosouce.loop = true;
		Menuaudiosouce.volume=0.6f;
		if(!Menuaudiosouce.isPlaying)
		Menuaudiosouce.Play ();
	}


	// Update is called once per frame
	public void ShowLevelSelection()
	{
		Invoke("Hidepage",0.5f);
		ShowFadeimage ();
		Levelselection.Obj.Invoke ("ShowLevelselectionpagenow", 0.5f);
	}


    public void ShowUpgradepage()
    {
        Invoke("Hidepage", 0.5f);
        ShowFadeimage();
        Upgradepage.Obj.Invoke("ShowUpgradepagenow", 0.5f);

    }


    public void ShowSettingspage()
	{
		Invoke("Hidepage",0.5f);
		ShowFadeimage ();
		Settingspage.Obj.Invoke ("ShowSettingsPage", 0.5f);

	}


	public void PrivacyLink()
	{
		// Application.OpenURL(AdData.policylink);
	}
	public void Ratebtn()
	{
		Application.OpenURL("market://details?id=" + Application.identifier);
	}

	public void ShowMenupagenow()
	{
		gameObject.SetActive (true);
		CheckCash ();
		Playmenusound ();
	}
	public void Hidepage()
	{
		gameObject.SetActive (false);
	}


	public static void ShowFadeimage()
	{
		Obj.Fadeobj.SetActive (true);
	}


	public static void Initialzedata()
	{
		
		if (PlayerPrefs.HasKey (Levelsunlocked) == false) 
		{
			PlayerPrefs.SetInt (Levelsunlocked, 1);
		}

		if (PlayerPrefs.HasKey (Resumegameunlimited) == false) 
		{
			PlayerPrefs.SetString (Resumegameunlimited, "false");
		}

		if (PlayerPrefs.HasKey (Vehiclesunlocked) == false) 
		{
			PlayerPrefs.SetString (Vehiclesunlocked, "1000000000");
		}


		if (PlayerPrefs.HasKey (Totalcash) == false) 
		{
			PlayerPrefs.SetInt (Totalcash, 100);
		}


		#if UNITY_EDITOR

//		PlayerPrefs.SetInt (Levelsunlocked, 5);

		#endif

//		UnlockallLevels ( ); 
//		UnlockallVehicles ();
	}


	public static void Addcash(int cashtobeadded)
	{
		Initialzedata ();
		PlayerPrefs.SetInt (Totalcash, PlayerPrefs.GetInt (Totalcash) + cashtobeadded);
	}


	public static void Deductcash(int cashtobeadded)
	{
		PlayerPrefs.SetInt (Totalcash, PlayerPrefs.GetInt (Totalcash) - cashtobeadded);
		Obj.CheckCash ();
	}

	public static int Getcash()
	{
		return	PlayerPrefs.GetInt (Totalcash);
		 
	}


	public static bool GetAvailablecash(int Checkcash)
	{
		if (Getcash ()>=Checkcash)
			return true;
		return false;
	}
	public Text Cashtext;

	public void CheckCash()
	{
		Cashtext.text = Getcash ().ToString ();
	}


	public static void UnlockallLevels()
	{
		
		PlayerPrefs.SetInt (Levelsunlocked, 20);
	}

	public static void UnlockallVehicles()
	{
		//	PlayerPrefs.SetString (Vehiclesunlocked, "11111111111111111111111111111111111111111111111111");

			PlayerPrefs.SetString (Vehiclesunlocked, "1111111111");

	}

	public static void Unlockallvehiclesandlevels()
	{
		UnlockallLevels ();
		UnlockallVehicles ();
	}
	public static void Unlimitedresumes()
	{
		PlayerPrefs.SetString (Resumegameunlimited, "true");

	}

	public void Showachievements()
	{
		#if Adsetup_On
//		AdSetupController.instance.ShowAchievements ();
		#endif
	}



	public void ShowLeaderboard()
	{
		#if Adsetup_On
//		AdSetupController.instance.ShowLeaderBoards ();
		#endif
	}

}
