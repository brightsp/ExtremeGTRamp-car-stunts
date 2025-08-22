using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bodyCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision _col)
    {
        //ContactPoint contactP = _col.contacts[0];
        //UIcontrols.Obj.FireObj.transform.position = contactP.point;
        //UIcontrols.Obj.FireObj.SetActive(true);

    }
}
