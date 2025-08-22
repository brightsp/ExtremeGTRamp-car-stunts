using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Upgradepage : MonoBehaviour
{


    public GameObject[] UnlockWatcObj;
    [System.Serializable]
    public class Vehicles
    {
        [Header("Specs")]
        [Range(0, 1)]
        public float speed;
        [Range(0, 1)]
        public float acceleration;
        [Range(0, 1)]
        public float handling;
        [Range(0, 1)]
        public float braking;
    }


    [Header("Vehicle settings")]
    public Vehicles[] Upgradevehicles;



    public static Upgradepage Obj;

    public GameObject[] Allvehicles;
    public Animator[] Allanimators;
    public static int playingCarNum = 0;
    public int currentvehicle;
    public GameObject Parentobj;
    public Image Playbtn, Buybtn,WatchBtn, LockImage, Lockbackgrounimg;
    public int[] Vehicleprices;
    public Text Costtext;
    public GameObject Loadingset;

    public Image Speedimg, Accelimg, Handlingimg, Brakingimg, Prevbtn, Nextbtn;

    public string[] Vehiclenames;


    public GameObject Unlockallvehilcespopup, Unlockallvehilcebutton;
    char[] chr;
    void Awake()
    {
        Obj = this;

        
        gameObject.SetActive(false);


    }
    // Use this for initialization
    void Start()
    {
        UnlockWatcObj[0].SetActive(false);
        UnlockWatcObj[1].SetActive(false);
    }


    public void ShowUpgradepagenow()
    {
        currentvehicle = playingCarNum;

        gameObject.SetActive(true);
        chr = PlayerPrefs.GetString(Menupage.Vehiclesunlocked).ToCharArray();
        
        if (chr[9] == '1')
        {
            currentvehicle = 9;
        }
                
        
        CheckVehicle();
        CheckCash();

        Menupage.Obj.Playupgradesound();

#if UNITY_EDITOR
       // Menupage.UnlockallVehicles();
#endif






        //  if (AdManager.instance)
        //{
        //    //sr AdManager.instance.RunActions(AdManager.PageType.Upgrade, Levelselection.Currentlevel);
        //}

      

    }


    public void ShowUpgradepagenowSession()
    {

        gameObject.SetActive(true);
        CheckVehicle();
        CheckCash();

        Menupage.Obj.Playupgradesound();

    }

    public void ShowLevelselection()
    {
        //Menupage.ShowFadeimage();
        //Invoke("Hidepage", 0.5f);
        //Levelselection.Obj.Invoke("ShowLevelselectionpagenow", 0.5f);


        Invoke("Hidepage", 0.5f);
        Menupage.Obj.Invoke("ShowMenupagenow", 0.5f);
        Menupage.ShowFadeimage();
    }


    public void ShowStorepage()
    {
        Menupage.ShowFadeimage();
        Invoke("Hidepage", 0.5f);
        Storepage.Obj.Invoke("ShowStorepage", 0.5f);
    }

    void Hidepage()
    {
        gameObject.SetActive(false);
    }


    IEnumerator DisableObj(GameObject aa)
    {
        yield return new WaitForSeconds(0.2f);
        aa.SetActive(false);
    }

    public void Nextclicked()
    {
       // Allvehicles[currentvehicle].SetActive(false);
        StartCoroutine(DisableObj(Allvehicles[currentvehicle]));
        if (currentvehicle < Allvehicles.Length)
        {
            currentvehicle++;
            Debug.Log("car num " + currentvehicle);

        }
        


        CheckVehicle();
    }

    public void Prevclicked()
    {
       // Allvehicles[currentvehicle].SetActive(false);
        StartCoroutine(DisableObj(Allvehicles[currentvehicle]));

        if (currentvehicle > 0)
        {
            currentvehicle--;

        }
        CheckVehicle();
    }

    public void CheckVehicleWatch()
    {
        Menupage.ShowFadeimage();

        CheckVehicle();

        Nextbtn.gameObject.SetActive(false);
        Prevbtn.gameObject.SetActive(false);
        UnlockWatcObj[0].SetActive(true);
        UnlockWatcObj[1].SetActive(true);

       // iTween.MoveTo(UnlockWatcObj[0].gameObject,iTween.Hash("y",-72,"easetype",iTween.EaseType.linear));
       // iTween.MoveTo(UnlockWatcObj[1].gameObject,iTween.Hash("y",62,"easetype",iTween.EaseType.linear));

    }

    public void HideDataaa()
    {
        Nextbtn.gameObject.SetActive(true);
        Prevbtn.gameObject.SetActive(true);


        UnlockWatcObj[0].SetActive(false);
        UnlockWatcObj[1].SetActive(false);
       // iTween.MoveTo(UnlockWatcObj[0].gameObject, iTween.Hash("y", 232, "easetype", iTween.EaseType.linear));
      //  iTween.MoveTo(UnlockWatcObj[1].gameObject, iTween.Hash("y", -185, "easetype", iTween.EaseType.linear));


    }
    public void CheckVehicle()
    {
        Buybtn.enabled = false;
        Playbtn.enabled = false;
        LockImage.enabled = false;
      
        Allvehicles[currentvehicle].SetActive(true);
        Buybtn.GetComponent<Button>().enabled = false;
        //sr  Playbtn.GetComponent<Button>().enabled = false;
        Playbtn.gameObject.SetActive(false);
        WatchBtn.gameObject.SetActive(false);
        Buybtn.gameObject.SetActive(false);

        LockImage.GetComponent<Button>().enabled = false;
        Lockbackgrounimg.gameObject.SetActive(false);
        chr = PlayerPrefs.GetString(Menupage.Vehiclesunlocked).ToCharArray();
        if (chr.Length<11)
        {
            string aa = PlayerPrefs.GetString(Menupage.Vehiclesunlocked);
           // Debug.Log("before "+aa);
           //sr aa += "0000000000";
          //  Debug.Log("after " + aa);

            PlayerPrefs.SetString(Menupage.Vehiclesunlocked,aa);
            chr = PlayerPrefs.GetString(Menupage.Vehiclesunlocked).ToCharArray();
            Debug.Log(chr.Length+ " aa-" + aa);

        }
        Debug.Log("currentvehicle " + currentvehicle );
    
        if (chr[currentvehicle] == '1')
        {
            Playbtn.enabled = true;
            Costtext.text = "";


            //sr Playbtn.GetComponent<Button>().enabled = true;
            Playbtn.gameObject.SetActive(true);

            Buybtn.gameObject.SetActive(false);
            WatchBtn.gameObject.SetActive(false);
        }
        else
        {
            if (currentvehicle==1)
            {
                WatchBtn.gameObject.SetActive(true);

            }
            else
            {
                Buybtn.enabled = true;
                Buybtn.gameObject.SetActive(true);
                Buybtn.GetComponent<Button>().enabled = true;

                LockImage.enabled = true;



                LockImage.GetComponent<Button>().enabled = true;

                Costtext.text = Vehicleprices[currentvehicle].ToString();
                Lockbackgrounimg.gameObject.SetActive(true);


            }
            
        }


        Speedimg.fillAmount = Upgradevehicles[currentvehicle].speed;
        Accelimg.fillAmount = Upgradevehicles[currentvehicle].acceleration;
        Handlingimg.fillAmount = Upgradevehicles[currentvehicle].handling;
        Brakingimg.fillAmount = Upgradevehicles[currentvehicle].braking;


        col = Prevbtn.color;
        col.a = 1;
        Prevbtn.color = col;
        Nextbtn.color = col;
        Nextbtn.GetComponent<Button>().interactable = true;
        //Debug.Log(Nextbtn.gameObject.transform.GetChild(0).gameObject.name+"----@");
        Nextbtn.gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;

        Prevbtn.GetComponent<Button>().interactable = true;
        Prevbtn.gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = true;
        //Debug.Log("currentvehicle -1- " + Allvehicles.Length);
        if (currentvehicle == 0)
        {
            col.a = 0.5f;
            Prevbtn.color = col;
            Prevbtn.GetComponent<Button>().interactable = false;
            Prevbtn.gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
        }

        if (currentvehicle == Allvehicles.Length-1)
        {
            //Debug.Log("current car -- " + currentvehicle);
            col.a = 0.5f;
            Nextbtn.color = col;
            Nextbtn.GetComponent<Button>().interactable = false;
            Nextbtn.gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;

        }


    }

    Color col;

    void Showcurrentvehicle()
    {

        Allanimators[currentvehicle].gameObject.SetActive(true);
        Allanimators[currentvehicle].Rebind();
        Allanimators[currentvehicle].SetTrigger("Inanim");

    }



    public void Unlockallbuttonclicked()
    {

        Debug.Log("btn IAP");

    }


    int Availablecash = 0;
    public void Purchasebuttonclicked()
    {

        if (Menupage.GetAvailablecash(Vehicleprices[currentvehicle]))
        {

            PurchaseSucces();
            
        }
        else
        {
            Debug.Log("btn IAP 2");

            Purchaser.mee.BuyConsumableItem(1);
          


        }
    }
    public void PurchaseSucces(bool deductcash=true)
    {
        if (deductcash)
        {
            Menupage.Deductcash(Vehicleprices[currentvehicle]);

        }
        char[] chr = PlayerPrefs.GetString(Menupage.Vehiclesunlocked).ToCharArray();
        chr[currentvehicle] = '1';
        PlayerPrefs.SetString(Menupage.Vehiclesunlocked, new string(chr));
        CheckVehicle();
        CheckCash();
    }
    public void WatchUnlock()
    {
        //AdData.mee.ShowIronSourceRewardVideo(RewardAdCall.RewardType_enum.Car_reward);
        SKAds.mee.ShowReward_order(SKAds.RewardType_enum.Car_reward);
    }

    public void Playbuttonclicked()
    {

        Menupage.ShowFadeimage();
        Invoke("Hidepage", 0.5f);
        Levelselection.Obj.Invoke("ShowLevelselectionpagenow", 0.5f);

        HideDataaa();
        return;

        Debug.Log("maingame levels");
        Hidepage();
        Menupage.ShowFadeimage();
        Loadingset.SetActive(true);
        Instantiateplayervehilce.Selectedvehiclename = Vehiclenames[currentvehicle];

        //		#if UNITY_EDITOR
        //		Levelselection.Currentlevel=14;
        //		#endif
        SceneManager.LoadSceneAsync("FinalScene");

        //        SceneManager.LoadSceneAsync("LEVEL" + Levelselection.Currentlevel);
        //sr Debug.Log("--- Level to Load :::: " + Levelselection.Currentlevel);
        //MultiPlayerManager_Btm.StartMatch_Btm();//after scene loads..
    }


    public void PlaYnow()
    {

        //Debug.LogError("levels scene ");
        Hidepage();
        Menupage.ShowFadeimage();
        Loadingset.SetActive(true);
        Instantiateplayervehilce.Selectedvehiclename = Vehiclenames[currentvehicle];


        //Debug.LogError("levelscene with vehicle selected "+ Instantiateplayervehilce.Selectedvehiclename);

        playingCarNum = currentvehicle;
        //		#if UNITY_EDITOR
        //		Levelselection.Currentlevel=14;
        //		#endif
        SceneManager.LoadSceneAsync("Ingame");// ("FinalScene");
       // SceneManager.LoadSceneAsync("LvlContainer");

        //        SceneManager.LoadSceneAsync("LEVEL" + Levelselection.Currentlevel);
   //sr Debug.Log("--- Level number--- " + Levelselection.Currentlevel);
        //MultiPlayerManager_Btm.StartMatch_Btm();//after scene loads..
    }

    void Hideall()
    {
        for (int i = 0; i < Allanimators.Length; i++)
        {
            Allanimators[i].gameObject.SetActive(false);
        }
    }

    Vector3 pos;
    void LateUpdate()
    {
        pos = Parentobj.transform.position;

        pos.x = currentvehicle * -20;
        Parentobj.transform.position = Vector3.Lerp(Parentobj.transform.position, pos, 5 * Time.deltaTime);
    }


    public Text Cashtext;

    public void CheckCash()
    {
        Cashtext.text = Menupage.Getcash().ToString();
    }

    public static int Upgradecount;
    void CheckUnlockalllVehiclespopoup()
    {
        Unlockallvehilcespopup.SetActive(false);
        if (Upgradecount % 3 == 0)
        {
            Upgradecount = 0;
            Unlockallvehilcespopup.SetActive(true);
            char[] chr = PlayerPrefs.GetString(Menupage.Vehiclesunlocked).ToCharArray();
            int tempcout = 0;
            for (int i = 0; i < chr.Length; i++)
            {
                if (chr[i] == '1')
                    tempcout++;
            }


            if (tempcout >= 40)
                Unlockallvehilcespopup.SetActive(false);
        }
    }

}
