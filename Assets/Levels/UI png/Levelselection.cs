using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levelselection : MonoBehaviour {

	// Use this for initialization
	public static Levelselection Obj;

    public GameObject MainScroller;
    public GameObject[] All_levelObj;
	public Image Trackimage;
	public Image Lockimage;
	public Text Levelnumber,Rewardtext;

	public Sprite[] Alllevelimgs;
	public static int Currentlevel=0,Levelreward;
	public Text Cashtext;
	public int[] Levelrewards;

	public GameObject Unlockalllevelspopup,Unlockalllevelbutton;

	public Image Nextbtn, Prevbtn;

	public static int Levelcount;

	void Awake()
	{
		Obj = this;
		gameObject.SetActive (false);
	}

	
	public void ShowLevelselectionpagenow()
	{

       // LevelContainer.mee.Disablelevels();

        gameObject.SetActive (true);
		Levelcount++;
		Currentlevel = PlayerPrefs.GetInt (Menupage.Levelsunlocked);
		CheckLevelstatus ();

		Checkcash ();

		Menupage.Obj.PlayLevelselectionsound ();

	//sr 	AdData.mee.Show_Ad(AdData.AddDelay_ls, "LS");
		



	}


    public void PlayGame()
    {

        Upgradepage.Obj.PlaYnow();

    }

    public void PlayGameLvl(int lvlNum)
    {

        if (lvlNum<=PlayerPrefs.GetInt(Menupage.Levelsunlocked))
        {
            Currentlevel = lvlNum;
            Upgradepage.Obj.PlaYnow();
        }
           //else
           //{
           // //sr AdManager.instance.BuyItem(2, true, this.gameObject);

           //}



    }


    public void ShowUpgradepagenow()
	{

		Invoke ("Hidepage", 0.5f);
	//	Debug.Log ("---- My LEVEL :::: " + Currentlevel);
//		Currentlevel = PlayerPrefs.GetInt (Menupage.Levelsunlocked);

		Upgradepage.Obj.Invoke("ShowUpgradepagenow",0.5f);
		Menupage.ShowFadeimage ();
//		Debug.Log ("---- Level Num :::: "+Currentlevel  + "----- Unlocked Levels :::: "+PlayerPrefs.GetInt (Menupage.Levelsunlocked));
//
//		if (Currentlevel <= PlayerPrefs.GetInt (Menupage.Levelsunlocked)) 
//		{
//			Invoke ("Hidepage", 0.5f);
//			Debug.Log ("---- My LEVEL :::: " + Currentlevel);
//			Upgradepage.Obj.Invoke("ShowUpgradepagenow",0.5f);
//			Menupage.ShowFadeimage ();
//
//		}
//		else 
//		{
//			Debug.Log ("---- Call Inapp");
////			#if Adsetup_On
//			if(AdSetupController.instance)
//				AdSetupController.instance.BuyItem (1, true);
////			#endif
//		}
	}

	void Hidepage()
	{
		gameObject.SetActive (false);
	}
	public void ShowMenupage()
	{
        //back
		Invoke ("Hidepage", 0.5f);
        Upgradepage.Obj.Invoke("ShowUpgradepagenow", 0.5f);
        Menupage.ShowFadeimage();


  //      Menupage.Obj.Invoke("ShowMenupagenow",0.5f);
		//Menupage.ShowFadeimage ();


	}

	public void Prevclicked()
	{
		if (Currentlevel >1) 
		{
			Currentlevel--;
			Trackimage.sprite = Alllevelimgs [Currentlevel - 1];
			Levelnumber.text = "LEVEL " + Currentlevel;
			CheckLevelstatus ();

		}

	}

	public void Nextclicked()
	{

		if (Currentlevel <20) 
		{
			Currentlevel++;
			Trackimage.sprite = Alllevelimgs [Currentlevel - 1];
			Levelnumber.text = "LEVEL " + Currentlevel;
			CheckLevelstatus ();

		}
	}

	public void CheckLevelstatus()
	{
        //  PlayerPrefs.SetInt(Menupage.Levelsunlocked, 8);
      
        float xx = 0;
        if (PlayerPrefs.GetInt(Menupage.Levelsunlocked)>=17)
        {
            xx = -4362;
        }
        else if (PlayerPrefs.GetInt(Menupage.Levelsunlocked) >= 14)
        {
            xx = -3400;
        }
        else if (PlayerPrefs.GetInt(Menupage.Levelsunlocked) >= 11)
        {
            xx = -2550;
        }
        else if (PlayerPrefs.GetInt(Menupage.Levelsunlocked) >= 8)
        {
            xx = -1700;
        }
        else if (PlayerPrefs.GetInt(Menupage.Levelsunlocked) >= 5)
        {
            xx = -850;
        }
        MainScroller.transform.localPosition = new Vector3(xx, MainScroller.transform.localPosition.y, 0);
        for (int i=0;i<All_levelObj.Length;i++)
        {

            if (i < PlayerPrefs.GetInt(Menupage.Levelsunlocked))
            {

                All_levelObj[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }

		if (Currentlevel <= PlayerPrefs.GetInt (Menupage.Levelsunlocked)) 
		{
			Lockimage.gameObject.SetActive (false);
			Trackimage.GetComponent<Button> ().enabled = true;
		}
		else 
		{
			Lockimage.gameObject.SetActive (true);
			Trackimage.GetComponent<Button> ().enabled = false;
		}

	//	Trackimage.sprite = Alllevelimgs [Currentlevel - 1];
	//	Levelnumber.text = "LEVEL " + Currentlevel;
	//	Rewardtext.text ="$ "+ Levelrewards [Currentlevel - 1].ToString ();
		Levelreward = Levelrewards [Currentlevel - 1];


		col = Prevbtn.color;
		col.a = 1;
		Prevbtn.color = col;
		Nextbtn.color = col;

		if (Currentlevel == 1) 
		{
			col.a = 0.5f;
			Prevbtn.color = col;
		}

		if (Currentlevel == 20) 
		{
			col.a = 0.5f;
			Nextbtn.color = col;
		}

	}

	Color col;

	void Checkcash()
	{
		Cashtext.text = Menupage.Getcash ().ToString ();
	}


	void Checkunlockallpopup()
	{
		Unlockalllevelspopup.SetActive (false);
		if (Levelcount %4==0)
		{
			Levelcount = 0;
			if (PlayerPrefs.GetInt (Menupage.Levelsunlocked) < 10) 
			{
				Unlockalllevelspopup.SetActive (true);
			}
		}
	}

}
