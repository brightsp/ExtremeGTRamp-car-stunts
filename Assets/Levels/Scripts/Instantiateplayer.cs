using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiateplayer : MonoBehaviour {


	Vector3  pos;
	void Awake()
	{
        myTrans = transform;
		pos = transform.position;
		pos.x += Random.Range (-10, 10);
		pos.z += Random.Range (-10, 10);
		transform.position = pos;

        //GameObject Player = Instantiate (Resources.Load ("Playersetup") as GameObject, transform.position, transform.rotation);
	}
	void Start()
	{
//		CreateNormalPlayer ();
//		GetVehicleNow();
	}
    private static Transform myTrans;
    public static void CreatePhotonPlayer()
    {
        if(myTrans == null)
        {
          //  Debug.LogError("No Trans for Char.");
        }
//        GameObject Player = PhotonNetwork.Instantiate("Playersetup", myTrans.position, myTrans.rotation,(byte)0,null);
//        Player.name = PlayerPrefs.GetString("myName") + " Playersetup";
//        Camfollow.fallowPlayerNow();
    }

    public static void CreateNormalPlayer()
    {
        if (myTrans == null)
        {
         //   Debug.LogError("No Trans for Char.");
        }

		Debug.LogError("Instantiate player...");

        GameObject Player = Instantiate(Resources.Load("Playersetup") as GameObject, myTrans.position, myTrans.rotation);
		Camfollow.Obj.FollowPlayerNow();
    }
}
