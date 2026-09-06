using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour {

	// Use this for initialization


	public float Camheightbeforeramp=2,OnGround=4;
	public Vector3 Savepos, Saverot;
	//	public PhotonView Myview;
	private GameObject animstareffect;
	void Start()
	{
		Savepos = transform.position;
		Saverot = transform.eulerAngles;

//		Debug.Log ("Owner name ::: " + Myview.owner + " Name :: " + transform.root.name);
//        if(Myview != null && !Myview.isMine){
//			if(Myview.owner!=null)
//            	gameObject.name = Myview.owner.NickName+" vehicle";
//        }

    }
	void OnTriggerEnter(Collider col)
	{
		//Debug.Log ("Room :: " + PhotonNetwork.room + "My view :: " + Myview+" Character ::"+Charactercontrol.Obj);
		if ((Charactercontrol.Obj && !Charactercontrol.Obj.PlayerInsidevehicle))
			return;
			
		if (col.CompareTag ("Camerachange")) 		
		{
			if (Camfollow.Obj)
				Camfollow.Obj.Changecameranow ();

		}


		if (col.CompareTag ("Finishcam")) 		
		{
			if (Camfollow.Obj) {
				Camfollow.Obj.FinishCam ();
			//sr	Camfollow.Obj.Targetobj = this.transform;
				//Camfollow.Obj.dontfollownow = true;
			}

			//CheckVehilcestatusnow.obj.Finisheffect.SetActive(true);
			LevelContainer.mee.Levelend_effect[Levelselection.Currentlevel].SetActive(true);
			//GetComponentInParent<RCC_CarControllerV3>()._gasInput = 0f;
			UIcontrols.Obj.RccCanvas.enabled = false;
			UIcontrols.Obj.BikercontrolCanvas.enabled = false;
			PlayercontrolUI.Obj.Hidecontrols ();
			Invoke ("CheckLevelstatus", 2);
			col.gameObject.SetActive (false);

		}


		if (col.CompareTag ("Rampstart")) 		
		{
			

			if (Camfollow.Obj)
				Camfollow.Obj.height=Camheightbeforeramp;

		}



		if (col.CompareTag ("Rampend")) 		
		{
			if (Camfollow.Obj)
				Camfollow.Obj.height=OnGround;

		}

		if (col.tag == "releaserigid")
		{
			col.GetComponent<RigidRelease>().OpenRigid();
		}

		//if (col.transform.name.Contains ("Walltrigger")) 		
		//{
		//	if (col.GetComponent<Wallsetup> ())
		//		col.GetComponent<Wallsetup> ().Enableallbricks ();
		//}
		if (col.transform.tag.Contains ("Walltrigger")) 		
		if (col.transform.tag.Contains ("Walltrigger")) 		
		{
			if (col.GetComponent<Wallsetup> ())
				col.GetComponent<Wallsetup> ().Enableallbricks ();
		}

		if (col.CompareTag ("Crash") && !Camfollow.Obj.dontfollownow) 		
		{
			Debug.Log(col.transform.name);
			GetComponent<Rigidbody> ().isKinematic = true;

//			if(UIcontrols._iLivesCount>0)
//			{
//				UIcontrols._iLivesCount -= 1;
//				UIcontrols.Obj.ShowLivesPage ();
////				UIcontrols.Obj.Resumegamenow ();
//			}
//			else
//			{
//				PlayercontrolUI.Obj.Hidecontrols ();
//				UIcontrols.Obj.ShowResumepage ();
//			}


            PlayercontrolUI.Obj.Hidecontrols();
            UIcontrols.Obj.ShowResumepage();
        }

		//Debug.Log ("Collider name..." + col.transform.name);
		if (col.CompareTag ("Star")) 		
		{
			 animstareffect = col.gameObject.GetComponent<RotateScript>().animstar;
			//col.gameObject.SetActive (false);
			UIcontrols.Obj.Starparticle.transform.position = col.transform.position;
			UIcontrols.Obj.Starparticle.SetActive (true);
			UIcontrols.Obj.CollectedStars++;
			UIcontrols.Obj.EnableStars (UIcontrols.Obj.CollectedStars);
			Debug.Log ("star effect enable ..." );
			//col.gameObject.GetComponent<RotateScript>().stareffect.transform.position = this.transform.forward * 10;
			col.gameObject.GetComponent<RotateScript>().stareffect.SetActive(true);
			col.gameObject.GetComponent<RotateScript>().starobj.SetActive(false);
			col.gameObject.GetComponent<RotateScript>().animstar.SetActive(true);

			

			Invoke("Disablestar",0.15f);
			//PlayercontrolUI.Obj.Hidecontrols ();
		}


		if (col.CompareTag ("Savepoint")) 		
		{
			Debug.Log("Savepoint trigg ...");
			SaveVehicleposition (col.transform);
			PlayercontrolUI.Obj.Saveplayerpostion (col.transform);	
//			Charactercontrol.Obj.Saveplayerpostion (col.transform);	

			UIcontrols.Obj.savepointobj.SetActive (true);
			UIcontrols.Obj.Savepointreached = true;
			col.gameObject.SetActive(false);
			//			if (Levelselection.Currentlevel == 1) 
			//			{
			//				UIcontrols.Obj.Getoutofcar.SetActive (true);
			//			}


		}
		if (col.CompareTag ("woodenbox")) {
			col.GetComponent<PlayEffects> ().playwoodeneffect ();
		}
		if (col.CompareTag ("CamChange")) 
		{
			Camfollow.Obj.Camview = 1;
		}
		if (col.CompareTag ("CamChangetoTop")) 
		{
			Camfollow.Obj.Camview = 3;
		}
		if (col.CompareTag ("SetNoramalView")) 
		{
			Camfollow.Obj.Camview = -1;
		}
//		if (col.transform.name== "Level1help") 		
//		{
//			col.transform.gameObject.SetActive (false);
//			//Time.timeScale = 0;
//			GetComponent<Rigidbody>().isKinematic=true;
//			UIcontrols.Obj.Getoutofcar.SetActive (true);
//		}

	



	}

    
    void Disablestar()
	{
		animstareffect.gameObject.SetActive(false);
	}
	public void SaveVehicleposition(Transform obj)
	{
		Savepos = obj.position;
		Saverot = obj.eulerAngles;
	}


	void CheckLevelstatus()
	{
		CheckVehilcestatusnow.obj.Finisheffect.SetActive(false);

		if (UIcontrols.Obj.Targetcount >= 0) {
			UIcontrols.Obj.Showlevelcomplte();
		} else {
			UIcontrols.Obj.ShowLevelfail();

		}
	}


	void OnCollisionEnter(Collision col1)
	{
		//Debug.Log ("Colname ::: " + col1.collider.tag);




		if (col1.collider.CompareTag ("MakeKinematicfalse")) 		
		{

//			Debug.Log ("Colname ::: " + col1.collider.tag);

			if (col1.collider.GetComponent<Rigidbody> ())
			{
				col1.collider.GetComponent<Rigidbody> ().linearDamping = 0;
				//col1.collider.GetComponent<Rigidbody> ().AddForce (transform.forward*15, ForceMode.Impulse);
			}

			ContactPoint contact = col1.contacts [0];
			UIcontrols.Obj.SetFireexplsionnow (contact.point);

			UIcontrols.Obj.Targetcount++;

		}


	}




}
