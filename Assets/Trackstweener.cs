using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackstweener : MonoBehaviour {

	public GameObject[] movefrom_obj;
	public GameObject[] fromLeft_obj;
	[SerializeField]
	public GameObject[] scalefrom_obj;
	public GameObject[] fromRight_obj;

	public GameObject[] fromDown_obj;
	[SerializeField]
	public GameObject tweenimage;




	private int val = 1500;
	// Use this for initialization
	void OnEnable () {
		

		float delayVal = 0.2f;
		foreach (GameObject gob in movefrom_obj) {
			if(gob!=null)
				iTween.MoveFrom (gob, iTween.Hash ("y", gob.transform.position.y + val, "time", 0.5f, "delay", delayVal));
			delayVal += 0.1f;
		}

		//  delayVal = 0.8f;
		foreach (GameObject gob in fromLeft_obj) {
			if(gob!=null)
				iTween.MoveFrom (gob, iTween.Hash ("x", gob.transform.position.x - val, "time", 0.5f, "delay", delayVal));
			delayVal += 0.1f;
		}


		delayVal = 0.8f;
		foreach (GameObject gob in scalefrom_obj) {
			iTween.ScaleFrom (gob, iTween.Hash ("Scale", Vector3.zero, "time", 0.5f, "delay", delayVal,"easetype",iTween.EaseType.easeOutBack));
		//	iTween.ScaleTo(gob, iTween.Hash("Scale", Vector3.one, "time", 0.5f, "delay", delayVal));

			delayVal += 0.2f;
		}



		delayVal = 1.2f;
		foreach (GameObject gob in fromDown_obj) {
			// print (gob.name+" :a: "+gob.transform.localPosition);	
			if(gob!=null)
				iTween.MoveFrom (gob, iTween.Hash ("y", gob.transform.position.y - val, "time", 0.5f, "delay", delayVal));
			delayVal += 0.1f;
		}

		delayVal = 0.6f;

		foreach (GameObject gob in fromRight_obj) {
			if(gob!=null)
				iTween.MoveFrom (gob, iTween.Hash ("x", gob.transform.position.x + val, "time", 0.5f, "delay", delayVal));
			delayVal += 0.2f;
		}


		
	}
	private void Start()
	{
		Invoke("Playtween", 2.5f);//3.5
	}
	void Playtween()
	{
		if (tweenimage != null)
			iTween.MoveTo(tweenimage, iTween.Hash("x", (tweenimage.transform.position.x ) + 340f, "easeType", iTween.EaseType.linear, "loopType", "loop", "time", 2));
	}


		void OnDisable()
	{

		foreach (GameObject gob in scalefrom_obj) 
		{
			gob.transform.localScale = Vector3.one;
		}

		/*

		foreach (GameObject gob in movefrom_obj) {
			if (gob != null)
				iTween.Stop (gob);
			
		}

		//  delayVal = 0.8f;
		foreach (GameObject gob in fromLeft_obj) {
			if(gob!=null)
				iTween.Stop (gob);
		
		}






		foreach (GameObject gob in fromDown_obj) {
			print (gob.name+" :a: "+gob.transform.localPosition);	
			if(gob!=null)
				iTween.Stop (gob);
		
		}

	

		foreach (GameObject gob in fromRight_obj) {
			if(gob!=null)
				iTween.Stop (gob);
			
		}
		*/
	}
}

