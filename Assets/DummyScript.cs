using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyScript : MonoBehaviour
{

	public GameObject[] obj;
	void Start () 
	{
		Tween ();
	}
	
	void Tween()
	{
		for (int i = 0; i < obj.Length; i++)
		{
			iTween.MoveTo (obj[i], iTween.Hash ("x", -300,"islocal",true, "time", 0.5,"delay",(0.1f*i)+0.0005f, "easetype", iTween.EaseType.easeOutBack));//515

		}
	}
}
