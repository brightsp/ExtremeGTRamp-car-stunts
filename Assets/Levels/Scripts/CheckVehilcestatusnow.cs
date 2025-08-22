using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RGSK;

public class CheckVehilcestatusnow : MonoBehaviour {

	// Use this for initialization
	public static CheckVehilcestatusnow obj;
	public RCC_CarControllerV3 _RccController;
	public BikeControl _Bikecontrolref;
	public Motorbike_Controller _MotorbikeController;

	public GameObject Finisheffect,Playerpos,Dropposition,Followobj,Campos,CamposLong,CamposTop;
	public enum Vehicletype{Car,Bike};
	public Vehicletype _Currentvehicle;
	public GameObject Colliderobj,Lookobject;
	public Transform Lefthandlepos,Righthandlepos;
	public Animator Dooranim;
	public float CameraHeightvalue=2,CameraDistancevalue=3;

	public GameObject Dummyplayerobj;

	void Start () 
	{
		obj = this;
//		if (_RccController) {
//			_RccController.enabled = false;
//			if (PhotonNetwork.room != null && _RccController.GetComponent<CheckTrigger> ().Myview.isMine == false) 
//			{
//				GetComponent<Collider> ().enabled = false;
//				Colliderobj.SetActive (false);
//			}
//		}
//		if (_Bikecontrolref) {
//			_Bikecontrolref.enabled = false;
//			Dummyplayerobj.SetActive (false);
//
////			if (PhotonNetwork.room != null && _Bikecontrolref.GetComponent<CheckTrigger> ().Myview.isMine == false) 
////			{
////				GetComponent<Collider> ().enabled = false;
////				Colliderobj.SetActive (false);
////
////			}
//		}
//
//		if (_MotorbikeController) {
//			_MotorbikeController.enabled = false;
//			_MotorbikeController.GetComponent<Rigidbody> ().isKinematic = true;
//		}

//		Invoke ("Makeiskinematic", 1);


	}


	public void EnableVehilcenow()
	{
		if (_Currentvehicle == Vehicletype.Car) {
			_RccController.enabled = true;
		}
		if (_Currentvehicle == Vehicletype.Bike) 
		{
			if (_Bikecontrolref) {
				_Bikecontrolref.enabled = true;
				_Bikecontrolref.GetComponent<Rigidbody>().isKinematic = false;
			}


			if (_MotorbikeController) {
				_MotorbikeController.enabled = true;
				_MotorbikeController.GetComponent<Rigidbody> ().isKinematic = false;
			}

		}



	}

	int count=0;
	void Makeiskinematic()
	{
		if (_Bikecontrolref) {

			_Bikecontrolref.GetComponent<Rigidbody>().isKinematic = true;
		}
	}




}
