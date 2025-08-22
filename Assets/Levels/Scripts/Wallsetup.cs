using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallsetup : MonoBehaviour {

	// Use this for initialization
	public List<Rigidbody> Allbricks;



	public void Enableallbricks()
	{
		for (int i = 0; i < Allbricks.Count; i++) 
		{
			Allbricks [i].isKinematic = false;
//			Allbricks [i].AddForce (Allbricks [i].transform.forward * Allbricks [i].mass * 200, ForceMode.Force);
		}
	}
}
