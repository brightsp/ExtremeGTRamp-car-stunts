using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parachute : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void OnCollisionEnter(Collision _col)
	{
//		Debug.Log ("---- Col Name::::" + _col.transform.name);
		this.GetComponent<Rigidbody> ().useGravity = true;
	}
}
