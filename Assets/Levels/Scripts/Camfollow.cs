using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camfollow : MonoBehaviour {

	// Use this for initialization

	public Transform Targetobj,Driverpos,LongViewPos,TopViewPos;
	public float Speed=5;
	public int Camview=0;
	public GameObject Cameraforwardpos;
	public Rigidbody _Thisrigidobj;
	public static Camfollow Obj;
	public Charactercontrol _charcontrolref;

	void Awake()
	{
		Obj = this;
	
	}
	void OnEnable () {

        Invoke("Dealy",0.5f);
	
//		_charcontrolref = Charactercontrol.Obj;
//        if(Charactercontrol.Obj!=null)
//		    Playerobj = Charactercontrol.Obj.Lookobj;
	}

    void Dealy()
    {
        Cameraforwardpos = new GameObject();
        Cameraforwardpos.name = "Camerforward";

      //  this.transform.position = Followobj.transform.position;

        ResetData();
    }

    void ResetData()
    {

        dontfollownow = false;
		distance = 1.0f;
    height = 2.0f;//5
  //  heightDamping = 2.0f;
    rotationDamping = 3.0f;
     camchangeangle = 30;
     angle = 0;

       Playercharheight = 5;
    PlayercharDistance = 5;
    playercharXvalue = 0;


     Speed = 5;
     Camview = 0;
  
    
}
    public void FollowPlayerNow()
    {
        //        if(Obj == null)
        //        {
        //            Debug.LogError("CamFollow null...??");
        //        }

        Debug.LogError("CamFollow null...?? "+ Obj);
        Obj._charcontrolref = Charactercontrol.Obj;
        Obj.Playerobj = Charactercontrol.Obj.Lookobj;
    }

	Vector3 targetpos;

	void LateUpdate()
	{
//        if (Charactercontrol.Obj == null)
//            return;

//        if (_charcontrolref.PlayerInsidevehicle)
			UpdateVehiclecam ();
//		else
//			LateUpdateMeChar ();
		
	}

	float xrotvalue;

	void UpdateVehiclecam () 
	{

//        if (Charactercontrol.Obj == null)
//            return;
		if (Followobj)
		{

			if (_Thisrigidobj) 
			{
				Followobj.transform.position = _Thisrigidobj.transform.position+new Vector3(0, heightDamping,0);
				xrotvalue = _Thisrigidobj.transform.eulerAngles.x;
				if (xrotvalue < -40)
					xrotvalue = -40;

				Followobj.transform.rotation = Quaternion.Lerp (Followobj.transform.rotation,
					Quaternion.Euler (new Vector3 (xrotvalue, _Thisrigidobj.transform.eulerAngles.y, 0)),
					2 * Time.deltaTime);
			}
//			Followobj.transform.rotation = Quaternion.Euler(new Vector3(_Thisrigidobj.transform.eulerAngles.x,_Thisrigidobj.transform.eulerAngles.y,0));
		}

		if (Camview == -1) 
		{
			Targetobj =PlayercontrolUI._CheckVehilceref.Campos.transform;
			Camview = 0;

		}
		if (Camview == 0) 
		{
			Cam ();
		}
		if (Camview == 1) 
		{
			Targetobj =PlayercontrolUI._CheckVehilceref.CamposLong.transform;
			Cam ();
		}
		if (Camview == 3) 
		{
			Targetobj =PlayercontrolUI._CheckVehilceref.CamposTop.transform;
			Cam ();
		}

		if (Camview == 2) 
		{
			transform.LookAt (Targetobj.transform);
//			GetComponent<Camera> ().fieldOfView -= Time.deltaTime*20;
//			GetComponent<Camera> ().fieldOfView = Mathf.Clamp (GetComponent<Camera> ().fieldOfView, 40, 60);
		}

		if (Input.GetKey (KeyCode.R)) 
		{
			

			//Cameraforwardpos.transform.position = Targetobj.transform.forward * 5;

		}


		if (Input.GetKey (KeyCode.T)) 
		{
			angle--;
		}


	}

	void MakeDefaultcam()
	{
		Camview = 0;
		Time.timeScale = 1;
	}


	public void Changecameranow()
	{
		Camview = 2;
		Vector3 temppos = Targetobj.transform.position;
		temppos.z += 15;
		temppos.x += 3;
		temppos.y -=0 ;


		Cameraforwardpos.transform.position = temppos;
		transform.position = Cameraforwardpos.transform.position;
		Invoke ("MakeDefaultcam", 0.75f);
		Time.timeScale = 0.15f;
	}


	public void FinishCam()
	{
		Debug.Log("Camview--- "+ Camview);
		Targetobj = null;
		//Camview =2;//2
		//Vector3 temppos = Targetobj.transform.position;
		//temppos.z += 10;
		//temppos.x += 1;
		//temppos.y += 2f;


		//Cameraforwardpos.transform.position = temppos;
		//transform.position = Cameraforwardpos.transform.position;

		//sr..

		Invoke ("MakeDefaulttime", 0.30f);//0.05
		// Time.timeScale = 0.25f;
	}

	void MakeDefaulttime()
	{
		//sr CheckVehilcestatusnow.obj.gameObject.GetComponent<Rigidbody>().isKinematic = true;

		Time.timeScale = 1;
		//Camview = 0;
		//distance = 10;
	}


	Vector3 Targetpos;

	public float distance= 1.0f;
	// the height we want the camera to be above the target
	public float height= 2.0f;//5
	// How much we 
	public float heightDamping= 2.0f;
	public float rotationDamping= 3.0f;
	public static int camchangeangle = 30;
	public float angle=0;
	public bool dontfollownow;

	public GameObject Followobj,Lookobj;
	void Cam () {
		// Early out if we don't have a target
		if (!Targetobj)
			return;


		float  wantedRotationAngle = Targetobj.eulerAngles.y+angle;// for monster truck
		//	print("target.eulerAngles"+target.eulerAngles);
		float wantedHeight = Targetobj.position.y + height;


		float currentRotationAngle = transform.eulerAngles.y;
		float currentHeight = transform.position.y;

		// Damp the rotation around the y-axis
		currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);

		// Damp the height

		//currentHeight=wantedHeight;
		// Convert the angle into a rotation
		var currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);


	//	transform.position = Targetobj.position;
	//	transform.position -= currentRotation * Vector3.forward * distance;


		Targetpos = Targetobj.position;
		Targetpos-= currentRotation * Vector3.forward * distance;
		//Debug.Log(Targetpos + " cam target pos" + distance + " camview "+ Camview);
		if (_Thisrigidobj.velocity.magnitude > 5) 
		{
			Speed = _Thisrigidobj.velocity.magnitude*2;////2
		} else 
		{
			Speed = 50;
		}

		if (!dontfollownow)
		{
			currentHeight = wantedHeight;
//			transform.position =Targetobj.transform.position;




//			currentHeight = Mathf.Lerp (currentHeight, wantedHeight, Speed * Time.deltaTime);
//
//			transform.position=Vector3.MoveTowards(transform.position,  new Vector3(Targetpos.x,currentHeight,Targetpos.z),Speed*Time.deltaTime);


			transform.position=Vector3.MoveTowards(transform.position,  Targetobj.transform.position,Speed*Time.deltaTime);
		}

//		transform.position =Targetobj.transform.position;






		rot = Lookobj.transform.position - transform.position;
		Newdir = Quaternion.LookRotation (rot).eulerAngles;
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.Euler (Newdir), Speed * Time.deltaTime);
//		transform.rotation=Quaternion.Euler(Newdir);
	}

	Vector3 rot,Newdir;

	// Use this for initialization
	public GameObject Playerobj;
	public float Playercharheight=5;
	public float PlayercharDistance=5;
	public float playercharXvalue=0;

	Vector3 Newpos;
	// Update is called once per frame
	void LateUpdateMeChar () 
	{
		
		Newpos = Playerobj.transform.position;
		Newpos.y += Playercharheight;
		Newpos.x += playercharXvalue;
		Newpos.z -= PlayercharDistance;
		transform.position =  Newpos;
		transform.LookAt (Playerobj.transform);
		/*
		if (PlayercharDistance > 0 ) 
		{
			Charactercontrol.Obj._Directionenum = Charactercontrol.Directionenum.forward;
		}
		if (PlayercharDistance < 0) 
		{
			Charactercontrol.Obj._Directionenum = Charactercontrol.Directionenum.Reverse;

		}

		if (playercharXvalue < 0) 
		{
			Charactercontrol.Obj._Directionenum = Charactercontrol.Directionenum.Right;

		}

		if (playercharXvalue > 0) 
		{
			Charactercontrol.Obj._Directionenum = Charactercontrol.Directionenum.left;

		}
		*/

	}


}
