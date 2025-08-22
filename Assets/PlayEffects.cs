using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffects : MonoBehaviour {
    public static PlayEffects instance;
    public GameObject woodeneffect;
	public List<GameObject> _meshWood;
	void Start () {
        instance = this;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void playwoodeneffect()
    {
		for (int i = 0; i < _meshWood.Count; i++)
			_meshWood [i].SetActive (false);
        woodeneffect.SetActive(true);
		GetComponent<Collider> ().enabled = false;
    }
}
