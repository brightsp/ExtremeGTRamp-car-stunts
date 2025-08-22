using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchVehicle : MonoBehaviour {

    public GameObject[] UnlockWatcObj;
    public GameObject CloseBtn;
    public static WatchVehicle mee;
	// Use this for initialization
	void Awake () {
        //Debug.Log("hello");
        mee = this;

        UnlockWatcObj[0].SetActive(false);
        UnlockWatcObj[1].SetActive(false);
      //  gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Open()
    {
        UnlockWatcObj[0].SetActive(true);
        UnlockWatcObj[1].SetActive(true);

        //iTween.MoveTo(UnlockWatcObj[0].gameObject, iTween.Hash("y", -72, "easetype", iTween.EaseType.linear));
      //  iTween.MoveTo(UnlockWatcObj[1].gameObject, iTween.Hash("y", 62, "easetype", iTween.EaseType.linear));

        Debug.Log("Unlock Data "+PlayerPrefs.GetString(Menupage.Vehiclesunlocked));
        gameObject.SetActive(true);
        // iTween.ScaleFrom(CloseBtn, iTween.Hash("x", 0, "y", 0, "time", 0.5, "delay", 3f, "easetype", iTween.EaseType.spring));


        Upgradepage.Obj.HideDataaa();

        Invoke("enableClose",5);
        PickRandomPos(PlayerPrefs.GetString(Menupage.Vehiclesunlocked));

     

        Upgradepage.Obj.Nextbtn.gameObject.SetActive(false);
        Upgradepage.Obj.Prevbtn.gameObject.SetActive(false);
    }

    void enableClose()
    {

        CloseBtn.SetActive(true);

        
    }
    public List<int> StoredIndex;
    private string Dataa;
    private int MVal;
    private void PickRandomPos(string Val)
    {
        Dataa = Val;
        StoredIndex = new List<int>();
        for (int i=0;i<Val.Length;i++)
        {
            if (Val[i]=='0')
            {
                StoredIndex.Add(i);
               
            }

          
        }

        int randoMVal = Random.Range(0, Mathf.Clamp(StoredIndex.Count, 0, 5));

        Dataa = Dataa.Remove(StoredIndex[randoMVal], 1);
        Dataa = Dataa.Insert(StoredIndex[randoMVal], "1");

        MVal = StoredIndex[randoMVal];

        Debug.Log(randoMVal + "#### string  " + Dataa);

        Upgradepage.playingCarNum = MVal;
        Upgradepage.Obj.currentvehicle = MVal;
        Upgradepage.Obj.CheckVehicle();
    }
    public void WatchVideoToUnlock()
    {

        Debug.Log(" Car to play "+ MVal);
        

        ////sr AdManager.instance.ShowRewardVideoWithCallback((result) =>
        //{
        //    if (result)
        //    {
        //       // CancelInvoke("enableClose");
        //        PlayerPrefs.SetString(Menupage.Vehiclesunlocked, Dataa);

        //Upgradepage.playingCarNum = MVal;

        //        Upgradepage.Obj.currentvehicle = MVal;
        //        Menupage.Obj.ShowUpgradepage();
               

        //        if (Upgradepage.Obj)
        //        {
        //            Upgradepage.playingCarNum = MVal;

        //            Upgradepage.Obj.currentvehicle = MVal;
        //            Upgradepage.Obj.CheckVehicleWatch();
        //            if (Upgradepage.Obj.Unlockallvehilcebutton.GetComponent<CheckUnlockallbuttons>())
        //                Upgradepage.Obj.Unlockallvehilcebutton.GetComponent<CheckUnlockallbuttons>().CheckButtonStatus();
        //        }
        //        AdManager.CurrenTTime = 2;
        //        CloseBtn.SetActive(false);
        //        UnlockWatcObj[0].SetActive(false);
        //        UnlockWatcObj[1].SetActive(false);

        //        //iTween.MoveTo(UnlockWatcObj[0].gameObject, iTween.Hash("y", -72, "easetype", iTween.EaseType.linear));
        //      //  iTween.MoveTo(UnlockWatcObj[1].gameObject, iTween.Hash("y", -185, "easetype", iTween.EaseType.linear));
        //sr AdManager.instance.ShowToast(" Congratulations New Vehicle Unlocked");
        //    }
        //});

        
    }

    public void Close()
    {
        //AdManager.CurrenTTime = 2;

        CloseBtn.SetActive(false);

       // iTween.MoveTo(UnlockWatcObj[0].gameObject, iTween.Hash("y", 232, "easetype", iTween.EaseType.linear));
       // iTween.MoveTo(UnlockWatcObj[1].gameObject, iTween.Hash("y", -185, "easetype", iTween.EaseType.linear));
        UnlockWatcObj[0].SetActive(false);
        UnlockWatcObj[1].SetActive(false);

        Upgradepage.Obj.currentvehicle = 0;

        Upgradepage.playingCarNum = 0;
        Menupage.Obj.ShowUpgradepage();

           Upgradepage.Obj.Nextbtn.gameObject.SetActive(true);
        Upgradepage.Obj.Prevbtn.gameObject.SetActive(true);
    
    }
}
