using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bikecontrolinputcontrol : MonoBehaviour {

	// Use this for initialization

	public static Bikecontrolinputcontrol Obj;

	public BikeControl bikeScript;
	void Awake()
	{
		Obj = this;
	}
	void Start () {
		
	}
	public void BikeAccelForward (float amount)
	{
		if(bikeScript)
			bikeScript.accelFwd = amount;
	}

	public void BikeAccelBack (float amount)
	{
		if(bikeScript)
			bikeScript.accelBack = amount;
	}

	public void BikeSteer (float amount)
	{
		if(bikeScript)
			bikeScript.steerAmount = amount;
	}

	public void BikeHandBrake (bool HBrakeing)
	{
		if(bikeScript)
			bikeScript.brake = HBrakeing;
	}

	public void BikeShift (bool Shifting)
	{
		if(bikeScript)
			bikeScript.shift = Shifting;
	}

}
