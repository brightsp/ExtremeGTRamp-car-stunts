using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHighLightScript : MonoBehaviour
{
	
	[SerializeField]float changeAmount = 0.1f;
	[SerializeField]float totalTime = 0.25f;
	[SerializeField]float delayTime = 2.0f;
	[SerializeField]bool isLocal = false;

	void Start ()
	{
		StopCoroutine ("StartWithDelay");
		StartCoroutine (StartWithDelay ());

	}

	IEnumerator StartWithDelay ()
	{
		yield return new WaitForSeconds (delayTime);
		if (gameObject.transform.localScale == Vector3.one)
			// iTween.PunchScale (gameObject, iTween.Hash ("amount", Vector3.one * (changeAmount), "time", totalTime * 2, "islocal", true, "looptype", iTween.LoopType.loop));
			iTween.ShakeScale(gameObject, iTween.Hash("amount", Vector3.one * (changeAmount), "time", totalTime ,"delay",delayTime, "islocal", true, "looptype", iTween.LoopType.loop));

		//		yield return new WaitForSeconds (delayTime);
		//		StartCoroutine (StartWithDelay ());
	}
}
