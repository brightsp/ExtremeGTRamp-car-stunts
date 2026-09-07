using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIcontrols : MonoBehaviour
{
    public int levelnum;
    public GameObject LoadingObj;
    public GameObject Fireexplosionobj, Starparticle;
    public static UIcontrols Obj;
    public Canvas RccCanvas, BikercontrolCanvas;
    public BikeControl bikeScript;
    public int CollectedStars = 0;
    public GameObject Pausepage, Showlevelfailobj, Resumepage, Levelcompleteobj, savepointobj, Loadingset, Getintocar, Getoutofcar, LivesPage;
    public bool Showingresumepage;
    public Image Resumefillimg;
    public Text Resumetimertext, Levelrewadtext, Totalavailablerewardtext, Text_Totalcoins, Text_LFTotalCoins, LFpageTotalCoins;
    public int Mlevel = -1;
    public GameObject[] Alllevels;
    public int Targetcount = 0;

    public GameObject FireObj;

    public AudioSource Playbg, Playbgambience, WinBg, FailBg;

    public GameObject[] Stars, Levelcompletestars;
    public Animator Staranimator;
    public bool Savepointreached;

    public GameObject mg_BtnReset;
    public static int _iHelpStatus
    {
        get
        {
            return PlayerPrefs.GetInt("Help", 0);
        }
        set
        {
            PlayerPrefs.SetInt("Help", value);
        }
    }
    public static int _iLivesCount
    {
        get
        {
            return PlayerPrefs.GetInt("Lives", 3);
        }
        set
        {
            PlayerPrefs.SetInt("Lives", value);
        }
    }



    void Awake()
    {
        Obj = this;
        //		Alllevels [0].SetActive (true);
        //		Levelselection.Currentlevel=0;

        Debug.Log("level active--- Start==> " + Mlevel);

#if UNITY_EDITOR
        if (Mlevel != -1)
            Levelselection.Currentlevel = Mlevel;
#endif


        UnityAnalyticsManager.instance.CustomEvent(SKAds.start_anlytics, Levelselection.Currentlevel);


        //Alllevels[Levelselection.Currentlevel - 1].SetActive(true);

        //   GameObject MlevelObj = Instantiate(Alllevels[Levelselection.Currentlevel - 1].gameObject)as GameObject;
        //   Invoke("EnableLevel", 1);
        //if (Bikecontrolinputcontrol.Obj)
        //    BikercontrolCanvas = Bikecontrolinputcontrol.Obj.GetComponent<Canvas>();

        //if (Rccvehilceinputcontrol.Obj)
        //    RccCanvas = Rccvehilceinputcontrol.Obj.GetComponent<Canvas>();


        //Loadingset.SetActive(false);

        //RccCanvas.gameObject.SetActive(false);
        //BikercontrolCanvas.gameObject.SetActive(false);


    }

    void EnableLevel()
    {

        LoadingObj.SetActive(false);
        Debug.Log("level Enable " + levelnum + " ,Currentlevel-> " + Levelselection.Currentlevel);

        levelnum = Levelselection.Currentlevel - 1;

        Debug.Log("lvl " + levelnum);
        LevelContainer.mee.AllLevels[levelnum].SetActive(true);
        LevelContainer.mee.playerpos.SetActive(true);

        //Debug.Log("comingg......");
        if (Bikecontrolinputcontrol.Obj)
            BikercontrolCanvas = Bikecontrolinputcontrol.Obj.GetComponent<Canvas>();

        if (Rccvehilceinputcontrol.Obj)
            RccCanvas = Rccvehilceinputcontrol.Obj.GetComponent<Canvas>();



        Loadingset.SetActive(false);

        RccCanvas.gameObject.SetActive(false);
        BikercontrolCanvas.gameObject.SetActive(false);


        Playbg.Play();
        Playbgambience.Play();
        Invoke("Showhelp", 0.2f);


        PlayercontrolUI.Obj.StartGame();

        //narj GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start,("lvl"+levelnum));

    }
    // Use this for initialization
    void Start()
    {
        Debug.Log("level active----ss");
        Invoke("EnableLevel", 1f);


        // GameObject MlevelObj = Instantiate(Alllevels[Levelselection.Currentlevel - 1].gameObject) as GameObject;




    }

    public GameObject mg_Help;
    void Showhelp()
    {
        if (Levelselection.Currentlevel == 1 && _iHelpStatus == 0)
        {
            //			Getintocar.SetActive (true);
            mg_Help.SetActive(true);
        }
    }

    public void CloseHelp()
    {
        _iHelpStatus = 1;
        mg_Help.SetActive(false);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayercontrolUI.Obj.Showcontrols();
        }


        if (Showingresumepage)
        {
            timervalue -= Time.deltaTime;
            Resumefillimg.fillAmount = timervalue / 7;
            Resumetimertext.text = Mathf.CeilToInt(timervalue).ToString();
            if (timervalue <= 0)
            {
                Showingresumepage = false;
                Hideresumepage();
                ShowLevelfail();
            }
        }

    }


    public void SetFireexplsionnow(Vector3 pos)
    {
        //Fireexplosionobj.SetActive(true);
        // Fireexplosionobj.transform.position = pos;
    }



    public void BikeAccelForward(float amount)
    {
        if (bikeScript)
            bikeScript.accelFwd = amount;
    }

    public void BikeAccelBack(float amount)
    {
        if (bikeScript)
            bikeScript.accelBack = amount;
    }

    public void BikeSteer(float amount)
    {
        if (bikeScript)
            bikeScript.steerAmount = amount;
    }

    public void BikeHandBrake(bool HBrakeing)
    {
        if (bikeScript)
            bikeScript.brake = HBrakeing;
    }

    public void BikeShift(bool Shifting)
    {
        if (bikeScript)
            bikeScript.shift = Shifting;
    }




    public void Showpausepopup()
    {
        Time.timeScale = 0;
        Pausepage.SetActive(true);



    }

    public void Hidepausepopup()
    {
        Time.timeScale = 1;
        Pausepage.SetActive(false);
        SKAds.mee.Show_Ad(SKAds.AddDelay_lf);

    }

    public void Showlevelfailnow()
    {
        ShowLevelfail();

    }


    public void ShowMenu()
    {
        // LevelContainer.mee.AllLevels[levelnum].SetActive(false);
        // LevelContainer.mee.playerpos.SetActive(false);

        Time.timeScale = 1;
        Getoutofcar.SetActive(false);
        Loadingset.SetActive(true);
        SceneManager.LoadScene("Menu");

        //		#if Adsetup_On
        //		if (AdSetupController.instance) {
        //			
        //			AdSetupController.instance.HideLCMoreGames ();
        //		}
        //		#endif
        WinBg.Stop();
        FailBg.Stop();
    }

    public void Nextbuttonclicked()
    {
        Time.timeScale = 1;
        ShowMenu();
        Menupage.Isfromlevelcomplete = true;

    }

    public void ShowLevelfail()
    {

        //LevelContainer.mee.AllLevels[levelnum].SetActive(false);
        //LevelContainer.mee.playerpos.SetActive(false);
        //		Stopplayeractiviyties ();
        Time.timeScale = 1;
        Hidepausepopup();
        Showlevelfailobj.SetActive(true);
        Menupage.Isfromelevelfail = true;

        Text_LFTotalCoins.text = Menupage.Getcash().ToString();
        LFpageTotalCoins.text = Menupage.Getcash().ToString();
        Playbg.Stop();

        Playbgambience.Stop();
        FailBg.gameObject.SetActive(true);
        FailBg.Play();

        Debug.Log("Failed == " + Levelselection.Currentlevel);
        //narj GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, ("lvl" + levelnum));
        UnityAnalyticsManager.instance.CustomEvent(SKAds.fail_anlytics, Levelselection.Currentlevel);

        SKAds.mee.Show_Ad(SKAds.AddDelay_lf, "LF");

    }

    float timervalue = 7;
    public void ShowResumepage()
    {
        if (!Savepointreached)
        {
            ShowLevelfail();
            return;
        }
        timervalue = 7;
        Showingresumepage = true;
        Resumepage.SetActive(true);


        //if (AdSetupController.instance)
        //	AdSetupController.instance.RunActions (AdSetupController.PageType.PreLF, Levelselection.Currentlevel, Levelselection.Levelreward);

    }



    public void Showlevelcomplte()
    {

        // LevelContainer.mee.AllLevels[levelnum].SetActive(false);
        // LevelContainer.mee.playerpos.SetActive(false);

        //		Stopplayeractiviyties ();
        Time.timeScale = 1;

        Levelcompleteobj.SetActive(true);

        Levelrewadtext.text = Levelselection.Levelreward.ToString();
        Menupage.Addcash(Levelselection.Levelreward);
        Totalavailablerewardtext.text = Menupage.Getcash().ToString();
        Text_Totalcoins.text = Menupage.Getcash().ToString();

        if (Levelselection.Currentlevel >= PlayerPrefs.GetInt(Menupage.Levelsunlocked))
        {
            PlayerPrefs.SetInt(Menupage.Levelsunlocked, PlayerPrefs.GetInt(Menupage.Levelsunlocked) + 1);

            if (PlayerPrefs.GetInt(Menupage.Levelsunlocked) == 2)
            {
                //sr AdManager.CurrenTTime = 10;

            }
        }



        Playbg.Stop();
        Playbgambience.Stop();
        WinBg.gameObject.SetActive(true);

        WinBg.Play();


        for (int i = 0; i < CollectedStars; i++)
        {
            Levelcompletestars[i].SetActive(true);
        }



        UnityAnalyticsManager.instance.CustomEvent(SKAds.complete_anlytics, Levelselection.Currentlevel);

        SKAds.mee.Show_Ad(SKAds.AddDelay, "LC");

        //narj GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, ("lvl" + levelnum));

    }

    public void Watchvideonow()
    {
#if UNITY_EDITOR
        Resumegamenow();
#endif

        SKAds.mee.ShowReward_order(SKAds.RewardType_enum.Health_reward);


    }

    public Button Watch500Btn;
    public void WatchVideo5000()
    {
        Watch500Btn.interactable = false;
        //sr AdManager.instance.ShowRewardVideoWithCallback((result) =>
        //{


        //    int MyCoins = Menupage.Getcash();// PlayerPrefs.GetInt("MyCoins");

        //    MyCoins += 5000;

        //    if (result)
        //    {
        //       // PlayerPrefs.SetInt("MyCoins", MyCoins);
        //        Menupage.Addcash(MyCoins);
        //        Text_Totalcoins.text = "" + Menupage.Getcash().ToString();
        //    }


        //    //sr AdManager.instance.ShowToast(" Congratulations 5000 coins added!");

        //});

    }

    public void Resumegamenow()
    {
        Showingresumepage = false;
        Hideresumepage();
        PlayercontrolUI.Obj.Showcontrols();



        //if (AdManager.instance)
        //sr AdManager.instance.RunActions(AdManager.PageType.InGame, Levelselection.Currentlevel, Levelselection.Levelreward);

    }
    void Hideresumepage()
    {

        Resumetimertext.text = timervalue.ToString(); ;

        Resumepage.SetActive(false);

    }

    void Stopplayeractiviyties()
    {

        if (PlayercontrolUI._CheckVehilceref._Bikecontrolref.GetComponent<Rigidbody>())
            PlayercontrolUI._CheckVehilceref._Bikecontrolref.GetComponent<Rigidbody>().isKinematic = true;

        if (PlayercontrolUI._CheckVehilceref._RccController.GetComponent<Rigidbody>())
            PlayercontrolUI._CheckVehilceref._RccController.GetComponent<Rigidbody>().isKinematic = true;


    }

    public void EnableStars(int count)
    {
        Stars[count - 1].SetActive(true);
        // Staranimator.SetTrigger("Star" + count);
    }
    int resetcount = 0;
    public void ResetCar()
    {
        PlayercontrolUI._CheckVehilceref._RccController.GetComponent<Rigidbody>().isKinematic = true;
        if (Savepointreached)
        {

            PlayercontrolUI.Obj.Showcontrols();
            SKAds.mee.ShowReward_order(SKAds.RewardType_enum.ResetCar);
            // AdData.mee.ShowIronSourceRewardVideo();
            return;
        }
        resetcount++;
        if (resetcount % 2 == 0)
        {
            PlayercontrolUI.Obj.Showcontrols();
            SKAds.mee.Show_Ad();

        }
        else
        {
            PlayercontrolUI.Obj.Showcontrols();
        }
        //sr.. if (PlayercontrolUI._CheckVehilceref._RccController)
        //   PlayercontrolUI._CheckVehilceref._RccController.ResetCar(true);
    }
    public Text Text_LivesCount;
    public void ShowLivesPage()
    {
        LivesPage.SetActive(true);
        Text_LivesCount.text = "You have " + (_iLivesCount + 1) + " Lives";
    }
    public void ResumeGame()
    {
        //		Hideresumepage ();
        LivesPage.SetActive(false);
        PlayercontrolUI.Obj.Showcontrols();
    }
}
