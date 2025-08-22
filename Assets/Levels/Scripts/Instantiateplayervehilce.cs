using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiateplayervehilce : MonoBehaviour
{

//    public static string Selectedvehiclename = "Bike1";
    	public static string Selectedvehiclename="Car1";
    public static GameObject MyPhotonVehicle;


	public GameObject TempCar;

	Vector3 pos;
	public RCC_Camera scr_Cam;
    void OnEnable()
    {
        myTrans = gameObject.transform;
//		pos = myTrans.position;
//		pos.x += Random.Range (-10, 10);
//		pos.z += Random.Range (-10, 10);
//		myTrans.position = pos;
		createMyNormalCar ();

        //GameObject vehicle = Instantiate (Resources.Load ("Vehicles/"+Selectedvehiclename) as GameObject, transform.position, transform.rotation);
        Invoke("Dealy", 0.5f);

    }

    private void Dealy()
    {
//        MultiPlayerManager_Btm.StartMatch_Btm();
		scr_Cam=GameObject.FindObjectOfType<RCC_Camera>() as RCC_Camera;
    }

    private static Transform myTrans;

	private  GameObject vehicle;
    public  void createMyNormalCar()
    {
        if (Selectedvehiclename.Contains("Bike") == true)
        {
            GameObject bike = Instantiate(Resources.Load("Vehicles/" + Selectedvehiclename) as GameObject, myTrans.position, myTrans.rotation);
            Debug.LogError("Instantiate selected vehilce ---");
			PlayercontrolUI._CheckVehilceref = bike.GetComponent<CheckVehilcestatusnow> ();
            return;
        }

		bool CarCreated = false;
//        Debug.LogError("first car Normal car.");
		#if UNITY_EDITOR
		if(TempCar!=null){
		//vehicle = Instantiate((TempCar) as GameObject, myTrans.position, myTrans.rotation);
	//	CarCreated=true;
		}

#endif
        //Debug.LogError("---------------------------------Goto ingame  " + Selectedvehiclename);

        if (!CarCreated) {
			vehicle = Instantiate (Resources.Load ("Vehicles/" + Selectedvehiclename) as GameObject, myTrans.position, myTrans.rotation);
//			RCC_Camera.
//			Invoke("SetCar",0.1f);
		}

		PlayercontrolUI._CheckVehilceref = vehicle.GetComponent<CheckVehilcestatusnow> ();
//		PlayercontrolUI.Obj.StartGame ();
     
    }

	void SetCar()
	{
		scr_Cam.SetPlayerCar(vehicle);

	}
}
