using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetVehicle : MonoBehaviour
{

    // Use this for initialization
    public GameObject Vehilceobj;
    public string Vehiclename = "Bike1";
    public GameObject Vehiclepos;
    private float Instantiatedelay = 11;
	Vector3 pos;
    void OnEnable()
    {

		pos = transform.position;
		pos.x += Random.Range (-10, 10);
		pos.z += Random.Range (-10, 10);
		transform.position = pos;
        Invoke("Createvehiclenow", Instantiatedelay);

    }


    void Createvehiclenow()
    {


//        if (PhotonNetwork.room != null)
//        {
//            if (!Instantiateplayervehilce.Selectedvehiclename.Contains("Bike") )
//            {
//                Debug.LogError("Loading Car2 for Photon=" + "Vehicles/" + Instantiateplayervehilce.Selectedvehiclename);
////                Vehilceobj = PhotonNetwork.Instantiate("Vehicles/" + Instantiateplayervehilce.Selectedvehiclename, Vehiclepos.transform.position, Vehiclepos.transform.rotation, 0, new object[] { });
//                Vehilceobj.name = PlayerPrefs.GetString("myName") + " " + Instantiateplayervehilce.Selectedvehiclename;
//            }
//            else
//            {
//                Vehilceobj = Instantiate(Resources.Load("Vehicles/" + Instantiateplayervehilce.Selectedvehiclename) as GameObject, Vehiclepos.transform.position, Vehiclepos.transform.rotation);
//            }
//        }
//        else
        {
            Debug.LogError(" vehicle instantiate--");

            Vehilceobj = Instantiate(Resources.Load("Vehicles/" + Instantiateplayervehilce.Selectedvehiclename) as GameObject, Vehiclepos.transform.position, Vehiclepos.transform.rotation);
          
        }
        Invoke("Makeitfalse", 2);
    }

    void Makeitfalse()
    {
        Vehilceobj.SetActive(false);

    }



}
