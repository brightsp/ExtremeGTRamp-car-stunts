using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenObject : MonoBehaviour {

    public iTween.EaseType eastypee=iTween.EaseType.easeInOutCubic;
    public iTween.LoopType loptype = iTween.LoopType.pingPong;
    public float Xamount, Yamount, Zamount;
    public float Timme=3;
    public float Dellay;
    // Use this for initialization
    void Start () {

        iTween.MoveTo(this.gameObject,iTween.Hash("x",this.transform.position.x+Xamount,"y", this.transform.position.y + Yamount,"z", this.transform.position.z + Zamount,"easetype", eastypee,"delay",Dellay, "time", Timme, "looptype",loptype));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
