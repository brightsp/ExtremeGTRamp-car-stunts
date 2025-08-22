using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disableit : MonoBehaviour {

	public float timetodisable=2;
	void OnEnable()
	{
		Invoke ("Disableobjectnow", timetodisable);
	}

	void Disableobjectnow()
	{
		gameObject.SetActive (false);
	}
}
