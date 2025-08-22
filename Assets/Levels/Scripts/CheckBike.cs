using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBike : MonoBehaviour {

	// Use this for initialization
	public WheelCollider Frontwheel,Rearwheel;
	public Transform Mainobj;
	public Rigidbody Rigidbodyobj;
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	Vector3 Angles;
	void FixedUpdate () 
	{

		Angles = Mainobj.transform.eulerAngles;
//		if (Frontwheel.isGrounded || Rearwheel.isGrounded) 
//		{
			Angles.z = 0;
			Mainobj.transform.eulerAngles = Angles;
			Rigidbodyobj.constraints = RigidbodyConstraints.FreezeRotationZ;
//		} 
//		else 
//		{
//			
//			Rigidbodyobj.constraints = RigidbodyConstraints.None;
//		}
		
	}
}
