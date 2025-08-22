using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactercontrol : MonoBehaviour
{

    // Use this for initialization
    public Rigidbody Playerobj;
    public float Walkspeed = 2;
    public float Runspeed = 5;
    public Virtualjoystick _Virtualjoystick;
    public Animator _Thisanim;


    public static CheckVehilcestatusnow _CheckVehilceref = null;

    public GameObject GetInVehilcebtn, GetOutfromVehiclebtn, Raycastobj, Lookobj;
    public CapsuleCollider Thiscollider;
    public bool PlayerInsidevehicle;
    bool Enterintovehicle;

    public static Charactercontrol Obj;

    public SkinnedMeshRenderer Charactermesh, Jacketmesh;
    public enum Currentvehicle { None, car, bike };
    public Currentvehicle _Currentvehicle;
    public LayerMask _Maskval;

    public enum Directionenum { forward, Reverse, left, Right };
    public Directionenum _Directionenum;
//    public PhotonView Myview;
    public TextMesh Playername;
    void Awake()
    {

//        if (Myview != null && !Myview.isMine)
//        {
//            if (Myview.owner != null)
//                gameObject.name = Myview.owner.NickName + " Player";
//        }
//
//
//        if (PhotonNetwork.room == null || Myview.isMine)
//        {
            Obj = this;

//        }
//        else
        {
            Charactermesh.receiveShadows = false;
            Jacketmesh.receiveShadows = false;

            Charactermesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            Jacketmesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;


        }
//        if (Myview.owner != null)
//            Playername.text = Myview.owner.NickName;
//
//		if (Myview.owner == null) {
//			if(PhotonNetwork.room !=null && !MultiPlayerManager_Btm.netWorkError)
//				Destroy (gameObject);
//		} else {
//			MultiPlayerManager_Btm.allPlayers[Myview.owner.ID] = Myview;
//		}

        
    }
    void Start()
    {
		Savepos = transform.position;
		Saverot = transform.eulerAngles;

		_Maskval = 1 << 13 | 1<<11 ;
		_Maskval = ~_Maskval;
		_Virtualjoystick = PlayercontrolUI.Obj.Joystickcontrol.GetComponent<Virtualjoystick>();
		PlayercontrolUI.Obj._currentplayercontroller = this;
//        Debug.Log("Char room="+PhotonNetwork.room);
//        if (PhotonNetwork.room == null || Myview.isMine)
//        {
//           
//        }
//        //		GetInVehilcebtn.SetActive (false);
//        //		GetOutfromVehiclebtn.SetActive (false);
//
//
//
//		if (Myview && Myview.isMine)
//		{
//			Myview.RPC("Resetcharacternow", PhotonTargets.Others);
//		}
    }

    Vector3 Rotaionvalue;
    float Yrot = 0;
    float Movexval, Moveyval;
    float applyspeed = 0;
    float animXval, animYval;
    float speedlimitvalue = 1;
    // Update is called once per frame
    RaycastHit hit;
    public bool Isground;

    public CharacterController controller;
    Vector3 moveDirection;
    float speed = 10, jumpSpeed = 3.5f;
    float gravity = 5;


    public static Vector3 Savepos, Saverot;
    public int Xvalue = 1, Zvalue = 1;
    void FixedUpdate()
    {

		if (PlayerInsidevehicle || Enterintovehicle || Menupage.Isfromelevelfail || Menupage.Isfromlevelcomplete)
            return;



        Movexval = _Virtualjoystick.Horizontal();
        Moveyval = _Virtualjoystick.Vertical();

        Rotaionvalue = transform.eulerAngles;
        //if (Mathf.Abs(Movexval )> 0.25f || Mathf.Abs( Moveyval) > 0.25f) 
        {
            Yrot = Mathf.Atan2(Movexval, Moveyval) * (180 / Mathf.PI) + Camfollow.Obj.transform.eulerAngles.y;
        }
        Rotaionvalue.y = Yrot;//  _Virtualjoystick.Horizontal ();


        if ((Mathf.Abs(Moveyval) > 0.15f && Moveyval < 0.5f) || (Mathf.Abs(Movexval) > 0.15f && Movexval < 0.5f))
        {

            applyspeed = Walkspeed;
            speedlimitvalue = 2.5f;
        }
        if (Mathf.Abs(Moveyval) > 0.5f || Mathf.Abs(Movexval) > 0.5f)
        {

            applyspeed = Runspeed;
            speedlimitvalue = 5;
        }

        if (Mathf.Abs(Moveyval) <= 0.15f && Mathf.Abs(Movexval) <= 0.15f)
        {

            applyspeed = 0;
            //speedlimitvalue=0;
        }

        //		Debug.Log ("Movexval " + Movexval+"::applyspeed "+applyspeed);

        animXval = Mathf.Lerp(animXval, Mathf.Abs(Movexval), 5f * Time.deltaTime);
        animYval = Mathf.Lerp(animYval, Mathf.Abs(Moveyval), 5f * Time.deltaTime);






        //Debug.DrawRay (Raycastobj.transform.position, Raycastobj.transform.forward * 5, Color.red);


        if (Physics.Raycast(Raycastobj.transform.position, Raycastobj.transform.forward, out hit, 0.5f, _Maskval))
        {
            if (hit.transform != null)
            {
                _Thisanim.SetFloat("Horizontal", 0);
                _Thisanim.SetFloat("Vertical", 0);
                applyspeed = 0;


            }

            //Debug.Log ("Hit transform name " + hit.transform.name);
        }

        //Debug.Log ("applyspeed :: " + controller.isGrounded);


        if (controller.isGrounded)
        {
            //			

            if (_Directionenum == Directionenum.forward)
                moveDirection = new Vector3(_Virtualjoystick.Horizontal() * 1, 0, _Virtualjoystick.Vertical() * 1);

            if (_Directionenum == Directionenum.Reverse)
                moveDirection = new Vector3(_Virtualjoystick.Horizontal() * -1, 0, _Virtualjoystick.Vertical() * -1);
            if (_Directionenum == Directionenum.Right)
                moveDirection = new Vector3(_Virtualjoystick.Vertical() * 1, 0, _Virtualjoystick.Horizontal() * -1);
            if (_Directionenum == Directionenum.left)
                moveDirection = new Vector3(_Virtualjoystick.Vertical() * -1, 0, _Virtualjoystick.Horizontal() * 1);

            //moveDirection = Camfollow.Obj.transform.TransformDirection (new Vector3( moveDirection.x,0,moveDirection.z));
            moveDirection *= applyspeed;
            if (Input.GetKeyDown(KeyCode.T))
            {
                //				JumpNow ();
                PlayercontrolUI.Obj.Jumpfunc();
            }

            _Thisanim.SetFloat("Horizontal", animXval * Xvalue);
            _Thisanim.SetFloat("Vertical", animYval * Zvalue);



            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Rotaionvalue), 10 * Time.deltaTime);



            if (_Thisanim.GetBool("Fall") == true)
            {
                _Thisanim.Rebind();
                _Thisanim.SetBool("Fall", false);
            }


        }
        else
        {


            if (Physics.Raycast(Raycastobj.transform.position, Raycastobj.transform.up * -1, out hit, 3, _Maskval))
            {
                if (hit.transform == null)
                {
               //sr     Debug.Log("Hit transform name ");
                }

            }
            else
            {
                if (_Thisanim.GetBool("Fall") == false)
                {
                    _Thisanim.SetTrigger("Falltrigger");
                    _Thisanim.SetBool("Fall", true);
                }

            }



        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);


        //Debug.Log ("Speed ::" + Playerobj.velocity.magnitude);
    }

    public void JumpNow()
    {
        moveDirection.y = jumpSpeed;
        _Thisanim.SetTrigger("IdleJumpTrigger");
        //		Playerobj.AddForce (transform.up * Playerobj.mass * 200);

    }


    void OnTriggerEnter(Collider col)
    {

//        if (PhotonNetwork.room != null && !Myview.isMine)
//        {
//            return;
//        }
        //Debug.Log ("Col transforname " + col.transform.root.name);
        if (col.CompareTag("CheckVehicle") && _CheckVehilceref == null)
        {

            _CheckVehilceref = col.GetComponent<CheckVehilcestatusnow>();
//            PlayercontrolUI.Obj.GetInButton.SetActive(true);

            //Debug.Log ("Exists in OnTriggerEnter..."+_CheckVehilceref.name);
        }

        if (col.CompareTag("Camerachange"))
        {
            if (Camfollow.Obj)
                Camfollow.Obj.Changecameranow();

        }

        if (col.CompareTag("GetVehicle"))
        {
            _Getvehicleref = col.GetComponent<GetVehicle>();
            GetVehicleNow();
            //PlayercontrolUI.Obj.GetVehiclebutton.SetActive (true);
        }

        if (col.CompareTag("Crash") && !PlayerInsidevehicle)
        {
            controller.enabled = false;
            //			Debug.Log ("Showlevelfail");
            PlayercontrolUI.Obj.Hidecontrols();

            UIcontrols.Obj.ShowResumepage();


        }


        if (col.CompareTag("Savepoint"))
        {

            Saveplayerpostion(col.transform);
            if (PlayerInsidevehicle)
            {
                if (_Currentvehicle == Currentvehicle.bike)
                {
                    _CheckVehilceref._Bikecontrolref.GetComponent<CheckTrigger>().SaveVehicleposition(col.transform);
                }


                if (_Currentvehicle == Currentvehicle.car)
                {
                    _CheckVehilceref._RccController.GetComponent<CheckTrigger>().SaveVehicleposition(col.transform);
                }
            }

            UIcontrols.Obj.savepointobj.SetActive(true);
            UIcontrols.Obj.Savepointreached = true;
        }

        if (col.CompareTag("Star"))
        {
            col.gameObject.SetActive(false);
            UIcontrols.Obj.Starparticle.transform.position = col.transform.position;
            UIcontrols.Obj.Starparticle.SetActive(true);
            UIcontrols.Obj.CollectedStars++;
            UIcontrols.Obj.EnableStars(UIcontrols.Obj.CollectedStars);
            //PlayercontrolUI.Obj.Hidecontrols ();
        }

        if (col.CompareTag("CharacterForward"))
        {
            _Directionenum = Directionenum.forward;
            Camfollow.Obj.PlayercharDistance = 4;
            Camfollow.Obj.playercharXvalue = 0;

        }


        if (col.CompareTag("CharacterReverse"))
        {
            _Directionenum = Directionenum.Reverse;
            Camfollow.Obj.PlayercharDistance = -4;
            Camfollow.Obj.playercharXvalue = 0;

        }

        if (col.CompareTag("CharacterLeft"))
        {
            _Directionenum = Directionenum.left;
            Camfollow.Obj.PlayercharDistance = 0;
            Camfollow.Obj.playercharXvalue = 4;

        }


        if (col.CompareTag("CharacterRight"))
        {
            _Directionenum = Directionenum.Right;
            Camfollow.Obj.PlayercharDistance = 0;
            Camfollow.Obj.playercharXvalue = -4;

        }
    }


    void OnTriggerExit(Collider col)
    {

//        if (PhotonNetwork.room != null && !Myview.isMine)
//        {
//            return;
//        }


        if (col.CompareTag("CheckVehicle") && !Enterintovehicle)
        {

            _CheckVehilceref = null;
            //			Debug.Log ("Exists in exit...");
//            PlayercontrolUI.Obj.GetInButton.SetActive(false);


        }

        if (col.CompareTag("GetVehicle"))
        {
            _Getvehicleref = null;
//            PlayercontrolUI.Obj.GetVehiclebutton.SetActive(false);
        }
    }

    public void Saveplayerpostion(Transform obj)
    {
        Savepos = obj.position;
        Saverot = obj.eulerAngles;
    }


    public void GetInVehiclenow()
    {

        Enterintovehicle = true;
//        PlayercontrolUI.Obj.GetOutbutton.SetActive(false);
//        PlayercontrolUI.Obj.GetInButton.SetActive(false);
//        PlayercontrolUI.Obj.Jumpbtn.SetActive(false);


        PlayercontrolUI.Obj.Joystickcontrol.SetActive(false);
        //		Debug.Log ("_CheckVehilceref :: " + _CheckVehilceref);
        transform.position = _CheckVehilceref.Playerpos.transform.position;
        transform.eulerAngles = _CheckVehilceref.Playerpos.transform.eulerAngles;

        controller.enabled = false;
        Thiscollider.enabled = false;

        //		Physics.IgnoreLayerCollision (11, 12, true);



        if (_CheckVehilceref._Currentvehicle == CheckVehilcestatusnow.Vehicletype.Car)
        {

            _CheckVehilceref.Colliderobj.gameObject.SetActive(false);
            /*
			if(_CheckVehilceref.Dooranim)
			_CheckVehilceref.Dooranim.SetTrigger ("OpenDoor");
			_Thisanim.SetTrigger ("GetInCar");
			Invoke ("EnableCarnow", 5);
			*/

            Invoke("EnableCarnow", 1);
        }
        else
        {
            _CheckVehilceref.Colliderobj.gameObject.SetActive(false);
            _Thisanim.SetTrigger("GetOnBike");
            Invoke("EnableBikenow", 2);

        }

        controller.radius = 0;
        controller.height = 0;




    }

    void EnableCarnow()
    {
        _Currentvehicle = Currentvehicle.car;

        PlayerInsidevehicle = true;
        _CheckVehilceref._RccController.enabled = true;


        Camfollow.Obj.Targetobj = _CheckVehilceref._RccController.transform;
        Camfollow.Obj._Thisrigidobj = _CheckVehilceref._RccController.GetComponent<Rigidbody>();
        Camfollow.Obj.height = _CheckVehilceref.CameraHeightvalue;
        Camfollow.Obj.distance = _CheckVehilceref.CameraDistancevalue;
        _CheckVehilceref.Followobj.transform.parent = null;
        Camfollow.Obj.Followobj = _CheckVehilceref.Followobj;
        Camfollow.Obj.Targetobj = _CheckVehilceref.Campos.transform;
		Camfollow.Obj.LongViewPos = _CheckVehilceref.CamposLong.transform;
		Camfollow.Obj.TopViewPos = _CheckVehilceref.CamposTop.transform;
        Camfollow.Obj.Lookobj = _CheckVehilceref.Lookobject;

        Charactermesh.enabled = false;
        Jacketmesh.enabled = false;
        //		Camfollow.Obj.transform.position = PlayercontrolUI.Obj.Cam.transform.position;
        //		Camfollow.Obj.transform.eulerAngles = PlayercontrolUI.Obj.Cam.transform.eulerAngles;

        //		PlayercontrolUI.Obj.Cam.enabled = false;
        //		Camfollow.Obj.gameObject.SetActive(true);


        Thiscollider.radius = 0;//2.75f
        Thiscollider.height = 0;//20f;
        transform.parent = _CheckVehilceref.transform;

//        PlayercontrolUI.Obj.GetOutbutton.SetActive(true);
//        PlayercontrolUI.Obj.Jumpbtn.SetActive(false);

        UIcontrols.Obj.RccCanvas.gameObject.SetActive(true);

        //		_CheckVehilceref._RccController.GetComponent<CheckTrigger>().Savepos = _CheckVehilceref._RccController.transform.position;
        //		_CheckVehilceref._RccController.GetComponent<CheckTrigger>().Saverot= _CheckVehilceref._RccController.transform.eulerAngles;

        Playername.transform.localPosition = new Vector3(0, 25, 23);
        Playername.transform.localEulerAngles = new Vector3(0, -90, 0);
        Playername.fontSize = 200;



    }



    void EnableBikenow()
    {
        _Currentvehicle = Currentvehicle.bike;
        Bikecontrolinputcontrol.Obj.bikeScript = _CheckVehilceref._Bikecontrolref;
        UIcontrols.Obj.BikercontrolCanvas.gameObject.SetActive(true);

        PlayerInsidevehicle = true;
        _CheckVehilceref.EnableVehilcenow();


        if (_CheckVehilceref._Bikecontrolref)
        {
            Camfollow.Obj.Targetobj = _CheckVehilceref._Bikecontrolref.transform;
            Camfollow.Obj._Thisrigidobj = _CheckVehilceref._Bikecontrolref.GetComponent<Rigidbody>();
            _CheckVehilceref._Bikecontrolref.GetComponent<Rigidbody>().isKinematic = false;
            _CheckVehilceref.Dummyplayerobj.SetActive(true);

//            if (Myview && Myview.isMine)
//            {
//                _CheckVehilceref._Bikecontrolref.GetComponent<Bikeghostshader>().myview.RPC("ActivateDummycharacterForBike", PhotonTargets.Others);
//                MultiPlayerManager_Btm.Dummycharacteractivatedforbike = true;
//
//            }

            Charactermesh.enabled = false;
            Jacketmesh.enabled = false;
        }


        if (_CheckVehilceref._MotorbikeController)
        {
            Camfollow.Obj.Targetobj = _CheckVehilceref._MotorbikeController.transform;
            Camfollow.Obj._Thisrigidobj = _CheckVehilceref._MotorbikeController.GetComponent<Rigidbody>();
        }

        Camfollow.Obj.height = _CheckVehilceref.CameraHeightvalue;
        Camfollow.Obj.distance = _CheckVehilceref.CameraDistancevalue;
        //		Camfollow.Obj.Targetobj= _CheckVehilceref.Lookobject.transform;

        _CheckVehilceref.Followobj.transform.parent = null;
        Camfollow.Obj.Followobj = _CheckVehilceref.Followobj;
        Camfollow.Obj.Targetobj = _CheckVehilceref.Campos.transform;
		Camfollow.Obj.LongViewPos = _CheckVehilceref.CamposLong.transform;
		Camfollow.Obj.TopViewPos = _CheckVehilceref.CamposTop.transform;

        Camfollow.Obj.Lookobj = _CheckVehilceref.Lookobject;



        //		Camfollow.Obj.transform.position = PlayercontrolUI.Obj.Cam.transform.position;
        //		Camfollow.Obj.transform.eulerAngles = PlayercontrolUI.Obj.Cam.transform.eulerAngles;

        //		PlayercontrolUI.Obj.Cam.enabled = false;
        //		Camfollow.Obj.gameObject.SetActive(true);


        Thiscollider.radius = 0;//2.75f
        Thiscollider.height = 0;//20f;
        transform.parent = _CheckVehilceref.transform;
//        PlayercontrolUI.Obj.GetOutbutton.SetActive(true);
//        PlayercontrolUI.Obj.Jumpbtn.SetActive(false);


        //		_CheckVehilceref._Bikecontrolref.GetComponent<CheckTrigger>().Savepos = _CheckVehilceref._Bikecontrolref.transform.position;
        //		_CheckVehilceref._Bikecontrolref.GetComponent<CheckTrigger>().Saverot= _CheckVehilceref._Bikecontrolref.transform.eulerAngles;
    }



    public void GetOutFromVehilcenow()
    {

        _Currentvehicle = Currentvehicle.None;

        Charactermesh.enabled = true;
        Jacketmesh.enabled = true;

//        UIcontrols.Obj.BikercontrolCanvas.gameObject.SetActive(false);
//        UIcontrols.Obj.RccCanvas.gameObject.SetActive(false);



        if (_CheckVehilceref._Bikecontrolref)
        {
            _CheckVehilceref._Bikecontrolref.enabled = false;
            _CheckVehilceref.Dummyplayerobj.SetActive(false);


//            if (Myview && Myview.isMine)
//            {
//                _CheckVehilceref._Bikecontrolref.GetComponent<Bikeghostshader>().myview.RPC("DeactivateDummycharacterForBike", PhotonTargets.Others);
//                MultiPlayerManager_Btm.Dummycharacteractivatedforbike = false;
//               
//            }
        }
        if (_CheckVehilceref._RccController)
        {
            _CheckVehilceref._RccController.enabled = false;
        }


//		if (Myview && Myview.isMine)
//		{
//			Myview.RPC("Resetcharacternow", PhotonTargets.Others);
//		}


        controller.radius = 2;
        controller.height = 20;
        controller.enabled = true;
        Thiscollider.radius = 2.75f;//2.75f
        Thiscollider.height = 20;//20f;
        transform.parent = null;
        PlayerInsidevehicle = false;
        transform.position = _CheckVehilceref.Dropposition.transform.position;
        transform.eulerAngles = Vector3.zero;

        Physics.IgnoreLayerCollision(11, 12, false);
        _Thisanim.Rebind();
//        PlayercontrolUI.Obj.GetOutbutton.SetActive(false);
//        PlayercontrolUI.Obj.Joystickcontrol.SetActive(true);
//        PlayercontrolUI.Obj.Jumpbtn.SetActive(true);


        _CheckVehilceref = null;
        Enterintovehicle = false;
        PlayerInsidevehicle = false;


        Playername.transform.localPosition = new Vector3(0, 21, 0);
        Playername.transform.localEulerAngles = new Vector3(0, 0, 0);
        Playername.fontSize = 150;
    }

    public GetVehicle _Getvehicleref;
    public void GetVehicleNow()
    {
        _Getvehicleref.Vehilceobj.SetActive(true);
        _Getvehicleref.gameObject.SetActive(false);
//        PlayercontrolUI.Obj.GetVehiclebutton.SetActive(false);
    }
    public bool ikActive;

    public Transform Lefthandpos, Righthandpos;
    void OnAnimatorIK()
    {




        if (_Thisanim.enabled != true)
            return;




        if (_Thisanim)
        {

            //if the IK is active, set the position and rotation directly to the goal. 
            if (ikActive)
            {


                _Thisanim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1.0f);
                _Thisanim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1.0f);

                _Thisanim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
                _Thisanim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1.0f);

                _Thisanim.SetIKPosition(AvatarIKGoal.LeftHand, Lefthandpos.position);
                _Thisanim.SetIKRotation(AvatarIKGoal.LeftHand, Lefthandpos.rotation);

                _Thisanim.SetIKPosition(AvatarIKGoal.RightHand, Righthandpos.position);
                _Thisanim.SetIKRotation(AvatarIKGoal.RightHand, Righthandpos.rotation);

            }

        }
    }


//    [PunRPC]
    public void Resetcharacternow()
    {
        _Thisanim.Rebind();
    }





}
