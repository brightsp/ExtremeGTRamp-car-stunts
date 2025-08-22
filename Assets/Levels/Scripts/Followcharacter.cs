using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Followcharacter : MonoBehaviour {

	// Use this for initialization
	public GameObject Targetobj;
	public float height=5;
	public float Distance=5;
	void Start () {
		
	}


	Vector3 pos;
	// Update is called once per frame
	void LateUpdate () 
	{
		pos = Targetobj.transform.position;
		pos.y += height;
		pos.z -= Distance;
		transform.position =  pos;
		transform.LookAt (Targetobj.transform);
		
	}
}
