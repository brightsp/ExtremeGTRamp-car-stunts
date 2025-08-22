using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayercontrolUI : MonoBehaviour {

	// Use this for initialization
	public static PlayercontrolUI Obj;
	public Charactercontrol _currentplayercontroller;
	public GameObject Joystickcontrol;//,GetInButton,GetOutbutton,GetVehiclebutton,Jumpbtn;
	public Camera Cam;
	void Awake() 
	{
		Obj = this;

//		GetVehiclebutton.SetActive (false);
//		GetOutbutton.SetActive (false);
//		GetInButton.SetActive (false);
	}

	void Start()
	{
		
	}

	public void GetInButtonfun()
	{
		_currentplayercontroller.GetInVehiclenow ();
	}
	public static CheckVehilcestatusnow _CheckVehilceref = null;

	public void StartGame()
	{
//		Enterintovehicle = true;
//		PlayercontrolUI.Obj.GetOutbutton.SetActive(false);
//		PlayercontrolUI.Obj.GetInButton.SetActive(false);
//		PlayercontrolUI.Obj.Jumpbtn.SetActive(false);


		PlayercontrolUI.Obj.Joystickcontrol.SetActive(false);
		//		Debug.Log ("_CheckVehilceref :: " + _CheckVehilceref);
//		transform.position = _CheckVehilceref.Playerpos.transform.position;
//		transform.eulerAngles = _CheckVehilceref.Playerpos.transform.eulerAngles;

//		controller.enabled = false;
//		Thiscollider.enabled = false;

		//		Physics.IgnoreLayerCollision (11, 12, true);

//		Debug.LogError (_CheckVehilceref.name);
		if(_CheckVehilceref._RccController)
		{
			Camfollow.Obj.Targetobj = _CheckVehilceref._RccController.transform;
			Camfollow.Obj._Thisrigidobj = _CheckVehilceref._RccController.GetComponent<Rigidbody>();
			UIcontrols.Obj.RccCanvas.gameObject.SetActive(true);
//			UIcontrols.Obj.mg_BtnReset.SetActive (true);
			//Debug.Log ("---- RCC");
		}
		else
		{
			Camfollow.Obj.Targetobj = _CheckVehilceref._Bikecontrolref.transform;
			Camfollow.Obj._Thisrigidobj = _CheckVehilceref._Bikecontrolref.GetComponent<Rigidbody>();
			UIcontrols.Obj.BikercontrolCanvas.gameObject.SetActive(true);
//			UIcontrols.Obj.mg_BtnReset.SetActive (false);
			Bikecontrolinputcontrol.Obj.bikeScript = _CheckVehilceref._Bikecontrolref;
		}

        //Debug.Log("---- start gameeeee");
        Camfollow.Obj.height = _CheckVehilceref.CameraHeightvalue;
		Camfollow.Obj.distance = _CheckVehilceref.CameraDistancevalue;
		_CheckVehilceref.Followobj.transform.parent = null;
		Camfollow.Obj.Followobj = _CheckVehilceref.Followobj;
		Camfollow.Obj.Targetobj = _CheckVehilceref.Campos.transform;

		Camfollow.Obj.Lookobj = _CheckVehilceref.Lookobject;

		if( _CheckVehilceref.CamposLong!=null){
			Camfollow.Obj.LongViewPos = _CheckVehilceref.CamposLong.transform;
		}
		if( _CheckVehilceref.CamposTop!=null){
			Camfollow.Obj.TopViewPos = _CheckVehilceref.CamposTop.transform;
		}
	}
	public void GetOutButtonfun()
	{
		if (UIcontrols.Obj.Getoutofcar.activeSelf) 
		{
			//Time.timeScale = 1;
			if (Charactercontrol._CheckVehilceref._RccController)
				Charactercontrol._CheckVehilceref._RccController.GetComponent<Rigidbody> ().isKinematic = false;

			if (Charactercontrol._CheckVehilceref._Bikecontrolref)
				Charactercontrol._CheckVehilceref._Bikecontrolref.GetComponent<Rigidbody> ().isKinematic = false;
			UIcontrols.Obj.Getoutofcar.SetActive (false);
		}
		_currentplayercontroller.GetOutFromVehilcenow ();
	}


	public void GetVehilcebuttonfunc()
	{
		_currentplayercontroller.GetVehicleNow ();

	}

	public void Jumpfunc()
	{
		_currentplayercontroller.JumpNow();
	}


	public void Showcontrols()
	{
//		if (_currentplayercontroller.PlayerInsidevehicle) 
		{
//			GetOutbutton.SetActive (true);
			//Debug.Log("--- RCC Controls Enable");
			if (PlayercontrolUI._CheckVehilceref._RccController) 
			{
				PlayercontrolUI._CheckVehilceref._RccController.transform.position = PlayercontrolUI._CheckVehilceref._RccController.GetComponent<CheckTrigger>().Savepos;
				PlayercontrolUI._CheckVehilceref._RccController.transform.eulerAngles=PlayercontrolUI._CheckVehilceref._RccController.GetComponent<CheckTrigger>().Saverot;
				PlayercontrolUI._CheckVehilceref._RccController.GetComponent<Rigidbody> ().isKinematic = false;
				UIcontrols.Obj.RccCanvas.gameObject.SetActive (true);
				Camfollow.Obj.transform.position = PlayercontrolUI._CheckVehilceref._RccController.transform.position;
			}

			if (PlayercontrolUI._CheckVehilceref._Bikecontrolref) 
			{
				PlayercontrolUI._CheckVehilceref._Bikecontrolref.transform.position = PlayercontrolUI._CheckVehilceref._Bikecontrolref.GetComponent<CheckTrigger>().Savepos;
				PlayercontrolUI._CheckVehilceref._Bikecontrolref.transform.eulerAngles=  PlayercontrolUI._CheckVehilceref._Bikecontrolref.GetComponent<CheckTrigger>().Saverot;
				PlayercontrolUI._CheckVehilceref._Bikecontrolref.GetComponent<Rigidbody> ().isKinematic = false;
				UIcontrols.Obj.BikercontrolCanvas.gameObject.SetActive (true);
				Camfollow.Obj.transform.position = PlayercontrolUI._CheckVehilceref._Bikecontrolref.transform.position;
			}
				
		} 
//		else 
//		{
//			Joystickcontrol.SetActive (true);
//			GetInButton.SetActive (false);
//			GetOutbutton.SetActive (false);
//			GetVehiclebutton.SetActive (false);
//			Jumpbtn.SetActive(true);
//			_currentplayercontroller.transform.position = Charactercontrol.Savepos;
//			_currentplayercontroller.transform.eulerAngles = Charactercontrol.Saverot;
//			_currentplayercontroller.controller.enabled = true;
//			UIcontrols.Obj.BikercontrolCanvas.gameObject.SetActive (false);
//			UIcontrols.Obj.RccCanvas.gameObject.SetActive (false);
//		}
	}

	public void Hidecontrols()
	{
		Joystickcontrol.SetActive (false);
//		GetInButton.SetActive (false);
//		GetOutbutton.SetActive (false);
//		GetVehiclebutton.SetActive (false);
//		Jumpbtn.SetActive(false);
	}
	public static Vector3 Savepos, Saverot;
	public void Saveplayerpostion(Transform obj)
	{
		Savepos = obj.position;
		Saverot = obj.eulerAngles;
	}
}
